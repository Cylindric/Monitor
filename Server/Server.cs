using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace Server
{
    class Server: IServer
    {
        public bool Ack()
        {
            Console.WriteLine("ACK received from client");
            return true;
        }

        public bool SubmitAgentReport(int num)
        {
            throw new NotImplementedException();
        }
    }
}
