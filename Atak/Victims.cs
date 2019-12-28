using System.Net.Sockets;

namespace Atak
{
    public class Victims
    {
        public Socket socket;
        public string id, ip, os, anti;

        public Victims(Socket socket, string id, string ip, string os, string anti)
        {
            this.socket = socket;
            this.id = id;
            this.ip = ip;
            this.os = os;
            this.anti = anti;
        }
    }
}
