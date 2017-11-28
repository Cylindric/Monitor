using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new Agent
            using (Agent agent = new Agent())
            {
                agent.Start();
                Agent.log.Info("Agent is running.");
                Console.ReadLine();
            }
        }


        //private static void StartWCFAgent()
        //{
        //    Uri httpUrl = new Uri("http://localhost:" + System.Configuration.ConfigurationManager.AppSettings["AgentPort"] + "/Monitor/Server");
        //    Uri tcpUrl = new Uri("net.tcp://localhost:" + System.Configuration.ConfigurationManager.AppSettings["AgentPort"] + "/Monitor/Server");

        //    // Create a service host
        //    ServiceHost host = new ServiceHost(typeof(Agent.Core.Agent), httpUrl);

        //    // Add a service endpoint
        //    host.AddServiceEndpoint(typeof(Utils.IServer), new WSHttpBinding(), "");

        //    // Enable metadata exchange
        //    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        //    smb.HttpGetEnabled = true;
        //    host.Description.Behaviors.Add(smb);
        //    host.Open();
        //}

        //private static void Log()
        //{
        //    using (var db = new Data())
        //    {
        //        var v = new CounterValue
        //        {
        //            TimeLogged = DateTime.Now,
        //            HostName = "Test Host",
        //            Category = "Test Category",
        //            Counter = "Test Counter",
        //            Instance = "Test Instance",
        //            Value = 1
        //        };

        //        db.CounterValues.Add(v);
        //        db.SaveChanges();

        //        var q = from cv in db.CounterValues
        //                orderby cv.TimeLogged
        //                select cv;

        //        foreach (var i in q)
        //        {
        //            Console.WriteLine(i.HostName);
        //        }
        //    }
        //}

    }

}
