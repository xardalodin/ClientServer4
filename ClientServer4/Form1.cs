using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace ClientServer4
{
    public partial class Form1 : Form
    {                
        #region Server that recives packages from clients puts them on txtchat textbox also txtServerOutput textbox for clients connected.

        //server stuff 
        private List<RecivePackage> clients;
        private BackgroundWorker bwListener;
        private Socket listenerSocket;
        private IPAddress serverIP;
        private int serverPort;

        private void StartToListen(object sender, DoWorkEventArgs e)
        {
            this.listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listenerSocket.Bind(new IPEndPoint(this.serverIP, this.serverPort));
            this.listenerSocket.Listen(200);  // backlog The maximum length of the pending connections queue.
            txtServerOutput.Text += "server created at : " + serverIP.ToString() + " Port: " + serverPort.ToString() + Environment.NewLine;
            while (true)
                this.CreateNewClientManager(this.listenerSocket.Accept());
        }
        private void CreateNewClientManager(Socket socket)
        {
            RecivePackage newClientManager = new RecivePackage(socket);
            newClientManager.CommandReceived += new RecivePackage.CommandReceivedEventHandler(CommandReceived);
            newClientManager.Disconnected += new RecivePackage.DisconnectedEventHandler(ClientDisconnected);
            txtServerOutput.Text += "user logged in at : " + newClientManager.IP.ToString() + " Port: " + newClientManager.Port.ToString() + Environment.NewLine;
            this.clients.Add(newClientManager);
            // connected
        }
        void ClientDisconnected(object sender, ClientEventArgs e)
        {
            txtServerOutput.Text += "user Disconnected : " + e.IP.ToString() + " Port: " + e.Port.ToString() + Environment.NewLine;
        }
        private void CommandReceived(object sender, PacketEA e)
        {
        
           txtChat.Text +=e.Packet.Username+ ":>" + e.Packet.PacketBody + Environment.NewLine;
        }

        private void btnServerDisconnect_Click(object sender, EventArgs e)
        {
             
                this.listenerSocket.Close();
                txtServerOutput.Text +=  "server shutting down " +Environment.NewLine;
           
          
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            txt_Ip_This.Text = GetLocalIP();
           txt_port_this.Text = "8020";  // to send messages 
            
            // initialize server at start 
            clients = new List<RecivePackage>();

            serverIP = IPAddress.Parse(GetLocalIP());
            serverPort = 8021; // server port +1

            bwListener = new BackgroundWorker();
            bwListener.WorkerSupportsCancellation = true;
            bwListener.DoWork += new DoWorkEventHandler(StartToListen);
            bwListener.RunWorkerAsync();
            //--------------------------------------------------------------

          

        }


        #region Connect and send messages

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress otherIP = IPAddress.Parse(txt_ip_client.Text);
            int otherPort = int.Parse(txt_port_client.Text);

            this.client = new SendPackage(otherIP, otherPort, "none");

            // events connecting to other server 
            this.client.ConnectingSuccessed += new SendPackage.ConnectingSuccessedEventHandler(client_ConnectingSuccessed);
            this.client.ConnectingFailed += new SendPackage.ConnectingFailedEventHandler(client_ConnectingFailed);
            this.client.DisconnectedFromServer += new SendPackage.DisconnectedEventHandler(server_shutdown);

            LoginToServer();
        }

        private void server_shutdown(object sender, EventArgs e)
        {
            txtChat.Text += "Connection lost" +Environment.NewLine;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }


        private SendPackage client;

        private void client_ConnectingSuccessed(object sender, EventArgs e)
        {
            // send login attempt 
            //this.client.SendCommand(new Packet(PacketCommandType.Message, this.client.IP, this.txtUsername.Text));

            txtChat.Text += "Connected to : " + client.ReciverIP.ToString() + " on port: " + client.Port.ToString() + Environment.NewLine;
        }
        //------
        private void client_ConnectingFailed(object sender, EventArgs e)
        {
            txtChat.Text += "Failed to connect to : " + client.ReciverIP.ToString()+ " on port: " + client.Port.ToString() + Environment.NewLine;
        }
        private void LoginToServer()
        {
            if (this.txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("No user", "enter username", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.client.NetworkName = this.txtUsername.Text.Trim();
                this.client.ConnectToServer();
            }
        }

        private void SendMessage()
        {
            if (this.client.Connected && this.txtMessageToSend.Text.Trim() != "")
            {
                Packet x = new Packet(PacketCommandType.Message, this.client.IP, txtMessageToSend.Text.Trim());
                x.Username = txtUsername.Text.Trim();
                x.IPThisMachine = IPAddress.Parse(txt_Ip_This.Text);
                this.client.SendCommand(x);

                this.txtChat.Text += this.txtUsername.Text + ": " + this.txtMessageToSend.Text + Environment.NewLine;
                this.txtMessageToSend.Text = "";
                this.txtMessageToSend.Focus();
            }

        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.SendMessage();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
#endregion
        private string GetLocalIP()
        {
            

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress x = IPAddress.Parse("127.0.0.1");

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    x = ip; // last one in list plz
                }

            }
            return x.ToString();
        }

        private void btnDiscconect_Click(object sender, EventArgs e)
        {
            this.client.Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
