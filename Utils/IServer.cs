using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Utils
{
    [ServiceContract()]
    public interface IServer
    {
        [OperationContract()]
        bool Ack();

        [OperationContract()]
        bool SubmitAgentReport(int num);
    }
}
