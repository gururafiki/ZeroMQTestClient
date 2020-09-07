using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeroMQTestClient
{
    class ResponseSocketController
    {
        public Thread ThreadResponse { get; set; }
        public ResponseSocketController(Action<string> writeLog)
        {
            WriteLog = writeLog;
            ThreadResponse = new Thread(new ThreadStart(ResponseSocketListener));
            ThreadResponse.Start();
        }
        private Action<string> WriteLog { get; set; }
        private void ResponseSocketListener()
        {
            using (var responseSocket = new ResponseSocket("@tcp://*:5555"))
            {
                while (true)
                {
                    var message = responseSocket.ReceiveFrameString();
                    WriteLog($"responseSocket : Server Received '{message}'\n");

                    var answer = $"Hey,I received: {message}";
                    WriteLog($"responseSocket Sending '{answer}'\n");
                    responseSocket.SendFrame(answer);
                }
            }
        }
    }
}
