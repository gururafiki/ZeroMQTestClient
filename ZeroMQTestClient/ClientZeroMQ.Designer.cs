namespace ZeroMQTestClient
{
    partial class ClientZeroMQ
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLog = new System.Windows.Forms.RichTextBox();
            this.subscribersConfigurations = new System.Windows.Forms.TextBox();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.labelIp = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPublishTopic = new System.Windows.Forms.TextBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.publishersConfigurations = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxLog.Location = new System.Drawing.Point(15, 58);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(460, 216);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.Text = "";
            // 
            // subscribersConfigurations
            // 
            this.subscribersConfigurations.Location = new System.Drawing.Point(80, 6);
            this.subscribersConfigurations.Name = "subscribersConfigurations";
            this.subscribersConfigurations.Size = new System.Drawing.Size(314, 20);
            this.subscribersConfigurations.TabIndex = 1;
            this.subscribersConfigurations.Text = "Test1236|localhost:1236,Test|localhost:1234,|localhost:1235";
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(397, 6);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(0);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(75, 46);
            this.btnInitialize.TabIndex = 3;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Location = new System.Drawing.Point(12, 9);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(62, 13);
            this.labelIp.TabIndex = 5;
            this.labelIp.Text = "Subscribers";
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(480, 55);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(61, 13);
            this.labelText.TabIndex = 12;
            this.labelText.Text = "Publish text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(480, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Publish topic";
            // 
            // textBoxPublishTopic
            // 
            this.textBoxPublishTopic.Location = new System.Drawing.Point(483, 32);
            this.textBoxPublishTopic.Name = "textBoxPublishTopic";
            this.textBoxPublishTopic.Size = new System.Drawing.Size(131, 20);
            this.textBoxPublishTopic.TabIndex = 10;
            this.textBoxPublishTopic.Text = "TestPubSub";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(483, 71);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(131, 177);
            this.textBoxMessage.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(483, 254);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(131, 20);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Enqueue";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Publishers";
            // 
            // publishersConfigurations
            // 
            this.publishersConfigurations.Location = new System.Drawing.Point(80, 32);
            this.publishersConfigurations.Name = "publishersConfigurations";
            this.publishersConfigurations.Size = new System.Drawing.Size(314, 20);
            this.publishersConfigurations.TabIndex = 14;
            this.publishersConfigurations.Text = "*:1235,*:1236,*:1234";
            // 
            // ClientZeroMQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 292);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.publishersConfigurations);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPublishTopic);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.labelIp);
            this.Controls.Add(this.btnInitialize);
            this.Controls.Add(this.subscribersConfigurations);
            this.Controls.Add(this.textBoxLog);
            this.Name = "ClientZeroMQ";
            this.Text = "I am listening and answering";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientZeroMQ_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textBoxLog;
        private System.Windows.Forms.TextBox subscribersConfigurations;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPublishTopic;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox publishersConfigurations;
    }
}

