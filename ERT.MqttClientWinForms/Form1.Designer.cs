namespace ERT.MqttClientWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lblMessage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtLog = new TextBox();
            txtTopic = new TextBox();
            txtMessage = new TextBox();
            btnConnect = new Button();
            btnSend = new Button();
            txtHost = new TextBox();
            txtClientId = new TextBox();
            txtUser = new TextBox();
            txtPass = new TextBox();
            txtPort = new TextBox();
            lblHost = new Label();
            lblClientId = new Label();
            lblUser = new Label();
            lblPass = new Label();
            lblPort = new Label();
            lblTopic = new Label();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.Location = new Point(12, 115);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(776, 323);
            txtLog.TabIndex = 16;
            // 
            // txtTopic
            // 
            txtTopic.Location = new Point(80, 79);
            txtTopic.Name = "txtTopic";
            txtTopic.Size = new Size(300, 23);
            txtTopic.TabIndex = 12;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(440, 79);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(250, 23);
            txtMessage.TabIndex = 14;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = SystemColors.ActiveBorder;
            btnConnect.FlatAppearance.MouseDownBackColor = Color.Lime;
            btnConnect.FlatAppearance.MouseOverBackColor = Color.Green;
            btnConnect.ForeColor = Color.Black;
            btnConnect.Location = new Point(490, 42);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(120, 25);
            btnConnect.TabIndex = 10;
            btnConnect.Text = "Bağlan";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(700, 78);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(90, 25);
            btnSend.TabIndex = 15;
            btnSend.Text = "Gönder";
            btnSend.Click += btnSend_Click;
            // 
            // txtHost
            // 
            txtHost.Location = new Point(80, 12);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(170, 23);
            txtHost.TabIndex = 1;
            // 
            // txtClientId
            // 
            txtClientId.Location = new Point(430, 12);
            txtClientId.Name = "txtClientId";
            txtClientId.Size = new Size(180, 23);
            txtClientId.TabIndex = 5;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(80, 43);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(170, 23);
            txtUser.TabIndex = 7;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(298, 43);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(180, 23);
            txtPass.TabIndex = 9;
            txtPass.UseSystemPasswordChar = true;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(298, 12);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(60, 23);
            txtPort.TabIndex = 3;
            // 
            // lblHost
            // 
            lblHost.AutoSize = true;
            lblHost.Location = new Point(12, 15);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(32, 15);
            lblHost.TabIndex = 0;
            lblHost.Text = "Host";
            // 
            // lblClientId
            // 
            lblClientId.AutoSize = true;
            lblClientId.Location = new Point(370, 15);
            lblClientId.Name = "lblClientId";
            lblClientId.Size = new Size(48, 15);
            lblClientId.TabIndex = 4;
            lblClientId.Text = "ClientId";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(12, 46);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(30, 15);
            lblUser.TabIndex = 6;
            lblUser.Text = "User";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(260, 46);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(30, 15);
            lblPass.TabIndex = 8;
            lblPass.Text = "Pass";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(260, 15);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 2;
            lblPort.Text = "Port";
            // 
            // lblTopic
            // 
            lblTopic.AutoSize = true;
            lblTopic.Location = new Point(12, 82);
            lblTopic.Name = "lblTopic";
            lblTopic.Size = new Size(36, 15);
            lblTopic.TabIndex = 11;
            lblTopic.Text = "Topic";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(390, 82);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(38, 15);
            lblMessage.TabIndex = 13;
            lblMessage.Text = "Mesaj";
            // 
            // Form1
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(lblHost);
            Controls.Add(txtHost);
            Controls.Add(lblPort);
            Controls.Add(txtPort);
            Controls.Add(lblClientId);
            Controls.Add(txtClientId);
            Controls.Add(lblUser);
            Controls.Add(txtUser);
            Controls.Add(lblPass);
            Controls.Add(txtPass);
            Controls.Add(btnConnect);
            Controls.Add(lblTopic);
            Controls.Add(txtTopic);
            Controls.Add(lblMessage);
            Controls.Add(txtMessage);
            Controls.Add(btnSend);
            Controls.Add(txtLog);
            Name = "Form1";
            Text = "ERT MQTT Client (WinForms)";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
