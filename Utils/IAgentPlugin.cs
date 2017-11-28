using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public interface IAgentPlugin
    {
        string Name { get; }
        string Code { get; }

        void Initialise(IAgentPluginHost host);
        void Update();
    }
}
