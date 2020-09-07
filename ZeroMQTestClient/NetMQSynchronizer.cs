using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroMQTestClient
{
    public class NetMQSynchronizer
    {
        private Action<string> _writeLog { get; set; }
        public Task WorkerTask { get; set; }
        private string _subscribersConfigurations { get; set; }
        private string _publishersConfigurations { get; set; }
        public Channel<Message> Messages = new Channel<Message>();
        private CancellationTokenSource _cancellationTokenSrc;
        private List<SubscriberSocket> _subSockets { get; set; }
        private List<PublisherSocket> _pubSockets { get; set; }
        public NetMQSynchronizer(string subscribersConfigurations, string publishersConfigurations, Action<string> writeLog = null)
        {
            _subscribersConfigurations = subscribersConfigurations;
            _publishersConfigurations = publishersConfigurations;
            _writeLog = writeLog;
            _cancellationTokenSrc = new CancellationTokenSource();
            WorkerTask = new Task(InitializeSync);
            WorkerTask.Start();
        }
        public void Stop()
        {
            _cancellationTokenSrc?.Cancel();
            Thread.Sleep(50);
        }
        public void InitializeSync()
        {
            NetMQPoller poller = new NetMQPoller();
            try
            {
                _subSockets = new List<SubscriberSocket>();
                if (!string.IsNullOrEmpty(_subscribersConfigurations))
                {
                    string[] subConfigurations = _subscribersConfigurations.Split(',');
                    foreach (var subConfiguration in subConfigurations)
                    {
                        var topicAndEnpoint = subConfiguration.Split('|');
                        var subSocket = new SubscriberSocket($"tcp://{topicAndEnpoint[1]}");
                        subSocket.Subscribe(topicAndEnpoint[0]);
                        _writeLog?.Invoke($"Subscriber socket connecting to {topicAndEnpoint[1]} with topic: {topicAndEnpoint[0]}\n");
                        subSocket.Options.ReceiveHighWatermark = 1000;
                        _subSockets.Add(subSocket);
                        poller.Add(subSocket);
                        subSocket.ReceiveReady += (s, a) =>
                        {
                            if (subSocket.TryReceiveFrameString(out string topic))
                            {
                                string text = subSocket.ReceiveFrameString();
                                _writeLog?.Invoke($"Receiving message: Topic={topic}, Text={text};\n");
                                if(topic != "response")
                                    Messages.Enqueue(new Message("response", $"Message: {text} successfully received"));
                            }
                        };
                    }
                }
                _pubSockets = new List<PublisherSocket>();
                if (!string.IsNullOrEmpty(_publishersConfigurations))
                {
                    string[] pubConfigurations = _publishersConfigurations.Split(',');
                    foreach (var pubEndpoint in pubConfigurations)
                    {
                        var pubSocket = new PublisherSocket($"tcp://{pubEndpoint}");
                        pubSocket.Options.SendHighWatermark = 1000;
                        _writeLog?.Invoke($"Publisher socket binding to {pubEndpoint}\n");
                        _pubSockets.Add(pubSocket);
                    }
                }

                poller.RunAsync();

                while (true)
                {
                    if (Messages.Count > 0)
                    {
                        var msg = Messages.Dequeue();
                        _pubSockets.ForEach(pubSocket =>
                        {
                            _writeLog?.Invoke($"Sending message: Topic={msg.Topic}, Text={msg.Text};\n");
                            msg.Send(pubSocket);
                        });
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                    _cancellationTokenSrc.Token.ThrowIfCancellationRequested();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is OperationCanceledException))
                    _writeLog?.Invoke($"Exception handled: {ex.ToString()};\n");
            }
            finally
            {
                poller.Dispose();
                _subSockets.ForEach(subSocket =>
                {
                    subSocket.Dispose();
                });
                _pubSockets.ForEach(pubSocket =>
                {
                    pubSocket.Dispose();
                });
            }
        }
    }
}
