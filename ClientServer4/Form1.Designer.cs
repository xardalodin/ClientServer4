namespace ClientServer4
{
    partial class Form1
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
            this.txtChat = new System.Windows.Forms.TextBox();
            this.txtServerOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ip_This = new System.Windows.Forms.TextBox();
            this.txt_port_this = new System.Windows.Forms.TextBox();
            this.txt_port_client = new System.Windows.Forms.TextBox();
            this.txt_ip_client = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMessageToSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnDiscconect = new System.Windows.Forms.Button();
            this.btnServerDisconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(13, 39);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(404, 282);
            this.txtChat.TabIndex = 0;
            // 
            // txtServerOutput
            // 
            this.txtServerOutput.Location = new System.Drawing.Point(816, 65);
            this.txtServerOutput.Multiline = true;
            this.txtServerOutput.Name = "txtServerOutput";
            this.txtServerOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerOutput.Size = new System.Drawing.Size(394, 256);
            this.txtServerOutput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(886, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server Info ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Chat";
            // 
            // txt_Ip_This
            // 
            this.txt_Ip_This.Location = new System.Drawing.Point(455, 55);
            this.txt_Ip_This.Name = "txt_Ip_This";
            this.txt_Ip_This.Size = new System.Drawing.Size(216, 20);
            this.txt_Ip_This.TabIndex = 4;
            // 
            // txt_port_this
            // 
            this.txt_port_this.Location = new System.Drawing.Point(691, 55);
            this.txt_port_this.Name = "txt_port_this";
            this.txt_port_this.Size = new System.Drawing.Size(77, 20);
            this.txt_port_this.TabIndex = 5;
            // 
            // txt_port_client
            // 
            this.txt_port_client.Location = new System.Drawing.Point(691, 119);
            this.txt_port_client.Name = "txt_port_client";
            this.txt_port_client.Size = new System.Drawing.Size(77, 20);
            this.txt_port_client.TabIndex = 7;
            // 
            // txt_ip_client
            // 
            this.txt_ip_client.Location = new System.Drawing.Point(455, 119);
            this.txt_ip_client.Name = "txt_ip_client";
            this.txt_ip_client.Size = new System.Drawing.Size(216, 20);
            this.txt_ip_client.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ip of this computer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(691, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(455, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Client IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(691, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "client port";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(455, 206);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(551, 206);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(230, 20);
            this.txtUsername.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(551, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Username";
            // 
            // txtMessageToSend
            // 
            this.txtMessageToSend.Location = new System.Drawing.Point(12, 344);
            this.txtMessageToSend.Multiline = true;
            this.txtMessageToSend.Name = "txtMessageToSend";
            this.txtMessageToSend.Size = new System.Drawing.Size(258, 65);
            this.txtMessageToSend.TabIndex = 15;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(292, 344);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(76, 65);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnDiscconect
            // 
            this.btnDiscconect.Location = new System.Drawing.Point(455, 245);
            this.btnDiscconect.Name = "btnDiscconect";
            this.btnDiscconect.Size = new System.Drawing.Size(75, 23);
            this.btnDiscconect.TabIndex = 17;
            this.btnDiscconect.Text = "Disconnect";
            this.btnDiscconect.UseVisualStyleBackColor = true;
            this.btnDiscconect.Click += new System.EventHandler(this.btnDiscconect_Click);
            // 
            // btnServerDisconnect
            // 
            this.btnServerDisconnect.Location = new System.Drawing.Point(816, 328);
            this.btnServerDisconnect.Name = "btnServerDisconnect";
            this.btnServerDisconnect.Size = new System.Drawing.Size(105, 23);
            this.btnServerDisconnect.TabIndex = 18;
            this.btnServerDisconnect.Text = "Disconnect";
            this.btnServerDisconnect.UseVisualStyleBackColor = true;
            this.btnServerDisconnect.Click += new System.EventHandler(this.btnServerDisconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 434);
            this.Controls.Add(this.btnServerDisconnect);
            this.Controls.Add(this.btnDiscconect);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessageToSend);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_port_client);
            this.Controls.Add(this.txt_ip_client);
            this.Controls.Add(this.txt_port_this);
            this.Controls.Add(this.txt_Ip_This);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServerOutput);
            this.Controls.Add(this.txtChat);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.TextBox txtServerOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Ip_This;
        private System.Windows.Forms.TextBox txt_port_this;
        private System.Windows.Forms.TextBox txt_port_client;
        private System.Windows.Forms.TextBox txt_ip_client;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMessageToSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDiscconect;
        private System.Windows.Forms.Button btnServerDisconnect;
    }
}

