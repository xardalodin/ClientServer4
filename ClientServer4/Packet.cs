using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ClientServer4
{

    // event args for Packet class 
    public class PacketEA : EventArgs
    {
        private Packet packet;
        public Packet Packet
        {
            get { return packet; }
        }
        public PacketEA(Packet package)
        {
            this.packet = package;
        }
    }


    // add more message types for package class as needed 
    // only using message for now
    public enum PacketCommandType
    {
        Message,
        Login,
        LogOff,
    }
    public class Packet
    {

        public IPAddress IPExternal { get; set; }
        public string Port { get; set; }

        public IPAddress IPThisMachine { get; set; }

        public string Username { get; set; }

        public PacketCommandType  PacketType { get; set; }

        public string PacketBody { get; set; }

        public Packet(PacketCommandType x, IPAddress ip, string body)
        {
            PacketType = x;
            IPExternal = ip;
            PacketBody = body;
        }

        // add more constructors as needed 




    }
}
