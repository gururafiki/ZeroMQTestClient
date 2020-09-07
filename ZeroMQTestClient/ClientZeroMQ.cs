using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeroMQTestClient
{
    public partial class ClientZeroMQ : Form
    {
        public ClientZeroMQ()
        {
            InitializeComponent();
        }
        public static NetMQSynchronizer NetMQSynchronizer { get; set; }
        delegate void AppendLogTextCallback(string value);

        public void AppendLogText(string value)
        {
            if (this.textBoxLog.InvokeRequired)
            {
                AppendLogTextCallback d = new AppendLogTextCallback(AppendLogText);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                this.textBoxLog.AppendText(value);
            }
        }
        private void btnInitialize_Click(object sender, EventArgs e)
        {
            NetMQSynchronizer?.Stop();
            NetMQSynchronizer = new NetMQSynchronizer(subscribersConfigurations.Text, publishersConfigurations.Text, AppendLogText);
        }

        private void ClientZeroMQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetMQSynchronizer?.Stop();
        }
        private void SpamMessages()
        {
            PublishMessage(textBoxPublishTopic.Text, $"{textBoxMessage.Text}");
            return;
            for (var i = 0; i < 1000; i++)
            {
                PublishMessage(textBoxMessage.Text, $"{textBoxMessage.Text}-{i}");
            }

        }
        private static bool PublishMessage(string topic, string data)
        {
            return NetMQSynchronizer?.Messages.Enqueue(new Message(topic, data)) ?? false;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (NetMQSynchronizer == null)
            {
                MessageBox.Show("You must init publisher at first");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxMessage.Text) || string.IsNullOrWhiteSpace(textBoxPublishTopic.Text))
            {
                MessageBox.Show("Enter topic and text");
                return;
            }

            Thread MessageSpamer = new Thread(SpamMessages);
            MessageSpamer.Start();
        }
    }
}
