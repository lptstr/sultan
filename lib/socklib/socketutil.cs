using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace socklib
{
    public class socketutil
    {
        public static Socket connect(string host, int pport) {
            Socket socks = null;
            IPHostEntry entry = null;
            if (pport == 0) {
                pport = 80;
            }

            // get host information
            entry = Dns.GetHostEntry(host);
            foreach (IPAddress address in entry.AddressList) {
                Console.WriteLine("[sultan] verb attempting connection -> " + address.ToString() + ":" + pport.ToString());
                IPEndPoint ipe = new IPEndPoint(address, pport);
                Socket tmpsocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tmpsocket.Connect(ipe);
                if (tmpsocket.Connected) {
                    socks = tmpsocket;
                    break;
                } else {
                    continue;
                }
            }
            Console.WriteLine("[sultan] verb connected -> " + host + ":" + pport.ToString());
            return socks;
        }
    }
}
