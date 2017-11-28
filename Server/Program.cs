using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            StartWCFAgent();
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            Console.ReadLine();
        }

        private static void StartWCFAgent()
        {
            string port = System.Configuration.ConfigurationManager.AppSettings["AgentPort"];

            Uri httpUrl = new Uri("http://localhost:" + port + "/Monitor/Server");

            // Create a service host
            ServiceHost host = new ServiceHost(typeof(Server), httpUrl);

            // Add a service endpoint
            WSHttpBinding binding = new WSHttpBinding();
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(Utils.IServer), binding, "");

            // Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);
            host.Open();
        }

    }
}
