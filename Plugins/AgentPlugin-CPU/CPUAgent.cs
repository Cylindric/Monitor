using System;
using System.Diagnostics;
using Utils;

namespace AgentPlugin_CPU
{
    public class CPUAgent : IAgentPlugin
    {
        private IAgentPluginHost mHost;
        private PerformanceCounter mPerformance;

        public string Code
        {
            get
            {
                return "cpu_agent";
            }
        }

        public string Name
        {
            get
            {
                return "CPU Poller Agent";
            }
        }

        public void Initialise(IAgentPluginHost host)
        {
            mHost = host;
        }

        public void Update()
        {
            float cpu = 23;
            var v = new CounterValue {
                HostName = mHost.HostName,
                TimeLogged = DateTime.Now,
                Category = "CPU",
                Counter = "Usage",
                Instance = "CPU0",
                Value = cpu
            };
            mHost.ReportValue(v);
        }
    }
}
