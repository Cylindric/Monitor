using Utils;

namespace Plugins.SampleAgentPlugin
{
    public class SampleAgentPlugin : IAgentPlugin
    {
        private IAgentPluginHost mHost;

        public string Code
        {
            get
            {
                return "Sample Agent Plugin";
            }
        }
        public string Name
        {
            get
            {
                return "sample_agent";
            }
        }

        public void Initialise(IAgentPluginHost host)
        {
            mHost = host;
        }

        public void Update()
        {
        }
        
    }
}
