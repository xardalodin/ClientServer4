using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClientServer4
{
    class SendPackage
    {
        #region attributes
        private Socket ThisMachinesSocket;   // a port on this machine

        private NetworkStream networkStream; // the streamer of data ones connecte d

        private BackgroundWorker BWSendPackage; // other thread to send package 

        private IPEndPoint ReciverEndPoint;   // where to send message

        private string Name;


        public bool Connected
        {
            get
            {
                if (this.ThisMachinesSocket != null)
                    return ThisMachinesSocket.Connected;
                else
                    return false;
            }
        }
        public IPAddress ReciverIP
        {
            get
            {
                if (this.Connected) return this.ReciverEndPoint.Address;
                else return IPAddress.None;
            }

        }
        public IPAddress IP
        {
            get
            {
                if (this.Connected)
                    return ((IPEndPoint)this.ThisMachinesSocket.LocalEndPoint).Address;
                else
                    return IPAddress.None;
            }
        }
        public int Port
        {
            get
            {
                if (this.Connected)
                    return ((IPEndPoint)this.ThisMachinesSocket.LocalEndPoint).Port;
                else
                    return -1;
            }
        }

        public string NetworkName
        {
            get { return Name; }
            set { Name = value; }
        }
        #endregion

        #region Constructors
        public SendPackage(IPEndPoint server, string Name)
        {
            this.ReciverEndPoint = server;
            this.Name = Name;
            // region networkstuff 
            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
        }

        public SendPackage(IPAddress serverIP, int port, string Name)
        {
            this.ReciverEndPoint = new IPEndPoint(serverIP, port);
            this.Name = Name;
            // region networkstuff 
            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
        }
        #endregion

        #region network stuff network dead, network alive, desconected 

        private void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (!e.IsAvailable)
            {
                this.OnNetworkDead(new EventArgs());
                this.OnDisconnectedFromServer(new EventArgs());
            }
            else
                this.OnNetworkAlived(new EventArgs());
        }

        public delegate void DisconnectedEventHandler(object sender, EventArgs e);
        public event DisconnectedEventHandler DisconnectedFromServer;
        protected virtual void OnDisconnectedFromServer(EventArgs e)
        {
            if (DisconnectedFromServer != null)
            {
                Control target = DisconnectedFromServer.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(DisconnectedFromServer, new object[] { this, e });
                else
                    DisconnectedFromServer(this, e);
            }
        }
        public delegate void NetworkDeadEventHandler(object sender, EventArgs e);
        public event NetworkDeadEventHandler NetworkDead;
        protected virtual void OnNetworkDead(EventArgs e)
        {
            if (NetworkDead != null)
            {
                Control target = NetworkDead.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(NetworkDead, new object[] { this, e });
                else
                    NetworkDead(this, e);
            }
        }
        public delegate void NetworkAlivedEventHandler(object sender, EventArgs e);
        public event NetworkAlivedEventHandler NetworkAlived;
        protected virtual void OnNetworkAlived(EventArgs e)
        {
            if (NetworkAlived != null)
            {
                Control target = NetworkAlived.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(NetworkAlived, new object[] { this, e });
                else
                    NetworkAlived(this, e);
            }
        }
        #endregion

        #region  Send Commad
        public void SendCommand(Packet cmd)
        {
            if (this.ThisMachinesSocket != null && this.ThisMachinesSocket.Connected)
            {
                BackgroundWorker bwSender = new BackgroundWorker();
                bwSender.DoWork += new DoWorkEventHandler(bwSender_DoWork);
                bwSender.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSender_RunWorkerCompleted);
                bwSender.WorkerSupportsCancellation = true;
                bwSender.RunWorkerAsync(cmd);
            }
            else
                this.OnCommandFailed(new EventArgs());
        }

        private void bwSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && ((bool)e.Result))
                this.OnCommandSent(new EventArgs());
            else
                this.OnCommandFailed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
            GC.Collect();
        }

        private void bwSender_DoWork(object sender, DoWorkEventArgs e)
        {
            Packet cmd = (Packet)e.Argument;
            e.Result = this.SendPacket(cmd);
        }

        System.Threading.Semaphore semaphor = new System.Threading.Semaphore(1, 1);

        private bool SendPacket(Packet cmd)
        {
            try
            {
                semaphor.WaitOne();
                if (cmd.PacketBody == null || cmd.PacketBody == "") // Null Check 
                {
                    cmd.PacketBody = "Null Error";
                    cmd.PacketType = PacketCommandType.Message;
                    cmd.Username = "Error";
                }


                //send packat type 
                byte[] buffer = new byte[4];  // 32 bits 8*4
                buffer = BitConverter.GetBytes((int)cmd.PacketType);
                this.networkStream.Write(buffer, 0, 4);
                this.networkStream.Flush();
                

                //send Command IpExteranal 
                byte[] ipBuffer = Encoding.ASCII.GetBytes(cmd.IPExternal.ToString());
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(ipBuffer.Length);
                this.networkStream.Write(buffer, 0, 4);
                this.networkStream.Flush();
                this.networkStream.Write(ipBuffer, 0, ipBuffer.Length);
                this.networkStream.Flush();

                //send Command IpExteranal 
                byte[] ipThisBuffer = Encoding.ASCII.GetBytes(cmd.IPThisMachine.ToString());
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(ipThisBuffer.Length);
                this.networkStream.Write(buffer, 0, 4);
                this.networkStream.Flush();
                this.networkStream.Write(ipBuffer, 0, ipThisBuffer.Length);
                this.networkStream.Flush();

                //send Packet User 
                byte[] userBuffer = Encoding.Unicode.GetBytes(cmd.Username);
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(userBuffer.Length);
                this.networkStream.Write(buffer, 0, 4);
                this.networkStream.Flush();
                this.networkStream.Write(userBuffer, 0, userBuffer.Length);
                this.networkStream.Flush();

                //send Packet body 
                byte[] bodyBuffer = Encoding.Unicode.GetBytes(cmd.PacketBody);
                buffer = new byte[4];
                buffer = BitConverter.GetBytes(bodyBuffer.Length);
                this.networkStream.Write(buffer, 0, 4);
                this.networkStream.Flush();
                this.networkStream.Write(bodyBuffer, 0, bodyBuffer.Length);
                this.networkStream.Flush();

                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }

        }


        public delegate void CommandSentEventHandler(object sender, EventArgs e);
        public event CommandSentEventHandler CommandSent;
        protected virtual void OnCommandSent(EventArgs e)
        {
            if (CommandSent != null)
            {
                Control target = CommandSent.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(CommandSent, new object[] { this, e });
                else
                    CommandSent(this, e);
            }
        }

        public delegate void CommandSendingFailedEventHandler(object sender, EventArgs e);
        public event CommandSendingFailedEventHandler CommandFailed;
        protected virtual void OnCommandFailed(EventArgs e)
        {
            if (CommandFailed != null)
            {
                Control target = CommandFailed.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(CommandFailed, new object[] { this, e });
                else
                    CommandFailed(this, e);
            }
        }
        #endregion

        #region Establish connection and end it 
        public void ConnectToServer()
        {
            BackgroundWorker bwConnector = new BackgroundWorker();
            bwConnector.DoWork += new DoWorkEventHandler(bwConnector_DoWork);
            bwConnector.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConnector_RunWorkerCompleted);
            bwConnector.RunWorkerAsync();
        }

        private void bwConnector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!((bool)e.Result))
                this.OnConnectingFailed(new EventArgs());
            else
                this.OnConnectingSuccessed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
        }

        private void bwConnector_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.ThisMachinesSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.ThisMachinesSocket.Connect(this.ReciverEndPoint);
                e.Result = true;
                this.networkStream = new NetworkStream(this.ThisMachinesSocket);
                // add background worker fór reciving . ---------------------------------------------<<<<<---look here
            }
            catch
            {
                e.Result = false;
            }
        }

        // connecting to remote endpoint succsess    
        public delegate void ConnectingSuccessedEventHandler(object sender, EventArgs e);
        public event ConnectingSuccessedEventHandler ConnectingSuccessed;
        protected virtual void OnConnectingSuccessed(EventArgs e)
        {
            if (ConnectingSuccessed != null)
            {
                Control target = ConnectingSuccessed.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(ConnectingSuccessed, new object[] { this, e });
                else
                    ConnectingSuccessed(this, e);
            }
        }
        // connecting to endpoint failed
        public delegate void ConnectingFailedEventHandler(object sender, EventArgs e);
        public event ConnectingFailedEventHandler ConnectingFailed;
        protected virtual void OnConnectingFailed(EventArgs e)
        {
            if (ConnectingFailed != null)
            {
                Control target = ConnectingFailed.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(ConnectingFailed, new object[] { this, e });
                else
                    ConnectingFailed(this, e);
            }
        }


        // shut down Socket 
        public bool Disconnect()
        {
            if (this.ThisMachinesSocket != null && this.ThisMachinesSocket.Connected)
            {
                try
                {
                    this.ThisMachinesSocket.Shutdown(SocketShutdown.Both);
                    this.ThisMachinesSocket.Close();
                    this.BWSendPackage.CancelAsync();
                    this.OnDisconnectedFromServer(new EventArgs());
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            else
                return true;
        }
        #endregion
    }
}
