using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using Utils;

namespace Agent
{
    public class Agent : IDisposable, IAgentPluginHost
    {
        public static ILog log;

        private IServer mServer;
        private PluginManager mPlugins;
        private IScheduler mScheduler;
        private string mServerURI;

        public string HostName {
            get { return System.Configuration.ConfigurationManager.AppSettings["AgentName"]; }
        }

        public Agent()
        {
            mServerURI = System.Configuration.ConfigurationManager.AppSettings["ServerURI"];

            // Initialise logging
            Agent.log = LogManager.GetLogger(typeof(Program));

            // Initialise the database
            Database.SetInitializer(new DataInitialiser());

            // Initialise the scheduler
            SchedulerStoreCreator.CreateDbIfMissing();
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            mScheduler = schedFact.GetScheduler();

            // Initialise the server connection
            ChannelFactory<IServer> scf = new ChannelFactory<IServer>(
                new WSHttpBinding(),
                mServerURI);
            mServer = scf.CreateChannel();

            // Load plugins
            mPlugins = new PluginManager();
            var section = PluginSettings.GetConfig();
            if (section != null)
            {
                foreach (PluginSetting setting in section.Plugins)
                {
                    Console.WriteLine("Loading plugins with code {0}", setting.Code);
                    mPlugins.LoadPlugins(setting.Code);
                }
            }
            mPlugins.InitialisePlugins(this);
        }


        public void Start()
        {
            // Ping the server
            try
            {
                Console.WriteLine("Attempting to contact the server at {0}", mServerURI);
                bool result = mServer.Ack();
                Console.WriteLine("Server responsed");
            }
            catch (EndpointNotFoundException)
            {
                Console.WriteLine("The server is not listening. Will try again later.");
            }

            mScheduler.Start();

            // Poll all plugins for data
            Poll();
        }


        private void Poll()
        {
            foreach (var p in mPlugins.Plugins)
            {
                p.Plugin.Update();
            }
        }


        private int GetPendingReportCount()
        {
            int c = 0;
            using (var db = new Data())
            {
                c = db.CounterValues.Count(); 
            }
            return c;
        }


        public void ReportValue(CounterValue v)
        {
            using (var db = new Data())
            {
                db.CounterValues.Add(v);
                db.SaveChanges();
            }
        }


        public void Dispose()
        {
            mScheduler.Shutdown();

            // http://caspershouse.com/post/Using-IDisposable-on-WCF-Proxies-(or-any-ICommunicationObject-implementation).aspx
            // http://msdn.microsoft.com/en-us/library/aa355056.aspx
            try
            {
                (mServer as ICommunicationObject).Close();
            }
            catch (CommunicationException)
            {
                (mServer as ICommunicationObject).Abort();
            }
            catch (TimeoutException)
            {
                (mServer as ICommunicationObject).Abort();
            }
            catch(Exception)
            {
                (mServer as ICommunicationObject).Abort();
                throw;
            }
        }


    }
}
