using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;

namespace ClientServer4
{
    public class ClientEventArgs : EventArgs
    {
        private Socket socket;

        public IPAddress IP
        {
            get { return ((IPEndPoint)this.socket.RemoteEndPoint).Address; }
        }

        public int Port
        {
            get { return ((IPEndPoint)this.socket.RemoteEndPoint).Port; }
        }

        public ClientEventArgs(Socket clientManagerSocket)
        {
            this.socket = clientManagerSocket;
        }
    }

    class RecivePackage
    {
        #region Attributes

        private Socket socket;
        private string name;
        NetworkStream NS;
        private BackgroundWorker BWClientR;
        System.Threading.Semaphore semaphor = new System.Threading.Semaphore(1, 1);

        public string ClientName
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public IPAddress IP
        {
            get
            {
                if (this.socket != null) return ((IPEndPoint)this.socket.RemoteEndPoint).Address;
                else return IPAddress.None;
            }
        }

        public int Port
        {
            get
            {
                if (this.socket != null) return ((IPEndPoint)this.socket.RemoteEndPoint).Port;
                else return -1;
            }
        }

        public bool Connected
        {
            get
            {
                if (this.socket != null) return this.socket.Connected;
                else return false;
            }
        }
        #endregion


        public RecivePackage(Socket sock)
        {
            this.socket = sock;
            this.NS = new NetworkStream(this.socket);
            this.BWClientR = new BackgroundWorker();
            this.BWClientR.DoWork += new DoWorkEventHandler(StartReceive);
            this.BWClientR.RunWorkerAsync();
        }

        private void StartReceive(object sender, DoWorkEventArgs e)
        {
            while (this.socket.Connected)  // if socket connected 
            {
                //Read packert Type.
                byte[] buffer = new byte[4];
                int readBytes = this.NS.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                PacketCommandType cmdType = (PacketCommandType)(BitConverter.ToInt32(buffer, 0));
                //------------------------------------------------------------------------------------
                
               // read Ip of reciver to confirm this is where its supposed to go 
                string ipthis = "";
                buffer = new byte[4];
                readBytes = this.NS.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                int ipSize = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[ipSize];
                readBytes = this.NS.Read(buffer, 0, ipSize);
                if (readBytes == 0)
                    break;
                ipthis = System.Text.Encoding.ASCII.GetString(buffer);
                //-------------------------------------------------------------------------------
                // read Ip sender  
                string ipofClient = "";
                buffer = new byte[4];
                readBytes = this.NS.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                int ipThisSize = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[ipSize];
                readBytes = this.NS.Read(buffer, 0, ipThisSize);
                if (readBytes == 0)
                    break;
                ipofClient = System.Text.Encoding.ASCII.GetString(buffer);
                //--------------------------------------------------------------------------------
                                
                //Read username 
                string cmdUsername = "";
                buffer = new byte[4];
                readBytes = this.NS.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                int sizeUsername = BitConverter.ToInt32(buffer, 0);

                //Read the command's Meta data.
                buffer = new byte[sizeUsername];
                readBytes = this.NS.Read(buffer, 0, sizeUsername);
                if (readBytes == 0)
                    break;
                cmdUsername = System.Text.Encoding.Unicode.GetString(buffer);

                //--------------------------------------------------------------------------------------------------
                //Read the command's MetaData size.
                string rPacket = "";
                buffer = new byte[4];
                readBytes = this.NS.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                int sizePacket = BitConverter.ToInt32(buffer, 0);

                //Read the command's Meta data.
                buffer = new byte[sizePacket];
                readBytes = this.NS.Read(buffer, 0, sizePacket);
                if (readBytes == 0)
                    break;
                rPacket = System.Text.Encoding.Unicode.GetString(buffer);
                // build packet---------------------------------------------------------------------------------------------------------
                Packet cmd = new Packet(cmdType, IPAddress.Parse(ipofClient), rPacket);
                cmd.IPThisMachine = IPAddress.Parse(ipthis);

                cmd.Username = cmdUsername;

                this.OnCommandReceived(new PacketEA(cmd));
            }
            this.OnDisconnected(new ClientEventArgs(this.socket));
            this.Disconnect();
        }


        public delegate void DisconnectedEventHandler(object sender, ClientEventArgs e);
        public event DisconnectedEventHandler Disconnected;
        protected virtual void OnDisconnected(ClientEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        public delegate void CommandReceivedEventHandler(object sender, PacketEA e);
        public event CommandReceivedEventHandler CommandReceived;
        protected virtual void OnCommandReceived(PacketEA e)
        {
            if (CommandReceived != null)
                CommandReceived(this, e);
        }

        public bool Disconnect()
        {
            if (this.socket != null && this.socket.Connected)
            {
                try
                {
                    this.socket.Shutdown(SocketShutdown.Both);
                    this.socket.Close();
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

    }
}
