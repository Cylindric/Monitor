using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Utils
{
    public class ServerServiceProxy :
        ClientBase<IServer>,
        IServer
    {
        public ServerServiceProxy(BasicHttpBinding binding, EndpointAddress epa) :
            base(binding, epa)
        {
        }

        public bool SubmitAgentReport(int num)
        {
            return base.Channel.SubmitAgentReport(num);
        }

        public bool Ack()
        {
            return base.Channel.Ack();
        }
    }

}
