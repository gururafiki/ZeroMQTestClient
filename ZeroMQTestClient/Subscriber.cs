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
    public class Subscriber
    {
        public Subscriber(string configurations, string topic, Action<string> writeLog = null)
        {
            _configurations = configurations;
            _writeLog = writeLog;
            _cancellationTokenSrc = new CancellationTokenSource();
            Start();
        }
        public void Start()
        {
            _workerTask = new Task(InitializeSubscriber);
            _workerTask.Start();
        }
        public void Stop()
        {
            _cancellationTokenSrc.Cancel();
        }
        private string _topic { get; set; }
        private string _configurations { get; set; }
        private Task _workerTask { get; set; }
        private Action<string> _writeLog { get; set; }
        private CancellationTokenSource _cancellationTokenSrc;
        private List<SubscriberSocket> _subSockets { get; set; }
        public void InitializeSubscriber()
        {
            if (string.IsNullOrEmpty(_configurations))
                return;

            try
            {
                string[] configurations = _configurations.Split(',');
                _subSockets = new List<SubscriberSocket>();
                foreach (var configuration in configurations)
                {
                    var topicAndEnpoint = configuration.Split('|');
                    var subSocket = new SubscriberSocket($"tcp://{topicAndEnpoint[1]}");
                    subSocket.Subscribe(topicAndEnpoint[0]);
                    _writeLog?.Invoke($"Subscriber socket connecting to {topicAndEnpoint[1]} with topic: {topicAndEnpoint[0]}\n");
                    subSocket.Options.ReceiveHighWatermark = 1000;
                    _subSockets.Add(subSocket);
                }
                while (true)
                {

                    _subSockets.ForEach(subSocket =>
                    {
                        if (subSocket.TryReceiveFrameString(out string topic))
                        {
                            string text = subSocket.ReceiveFrameString();
                            _writeLog?.Invoke($"Receiving message: Topic={topic}, Text={text};\n");
                        }
                    });
                    _cancellationTokenSrc.Token.ThrowIfCancellationRequested();
                }
            }
            catch(Exception ex)
            {
                if(!(ex is OperationCanceledException))
                    _writeLog?.Invoke($"Exception handled: {ex.ToString()};\n");
            }
            finally
            {
                _subSockets.ForEach(subSocket =>
                {
                    subSocket.Dispose();
                });
            }
        }
    }
    public class Message
    {
        public string Text { get; set; }
        public string Topic { get; set; }
        public Message(string topic,string text)
        {
            Topic = topic;
            Text = text;
        }
        public void Send(PublisherSocket pubSocket)
        {
            pubSocket.SendMoreFrame(Topic).SendFrame(Text);
        }
    }
    public class Channel<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();

        public bool Enqueue(T item)
        {
            lock (_queue)
            {
                _queue.Enqueue(item);
                if (_queue.Count == 1)
                    Monitor.PulseAll(_queue);
            }
            return true;
        }

        public T Dequeue()
        {
            lock (_queue)
            {
                while (_queue.Count == 0)
                    Monitor.Wait(_queue);

                return _queue.Dequeue();
            }
        }

        public T Peek()
        {
            lock (_queue)
            {
                while (_queue.Count == 0)
                    Monitor.Wait(_queue);

                return _queue.Peek();
            }
        }

        public int Count
        {
            get
            {
                lock (_queue)
                {
                    return _queue.Count;
                }
            }
        }
    }
}
