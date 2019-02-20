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
            char E = (char)27;

            // get host information
            entry = Dns.GetHostEntry(host);
            foreach (IPAddress address in entry.AddressList) {
                    Console.WriteLine("[sultan->socutl] {0}[38;2;50;190;250mVERB {0}[38;2;255;255;255mattempting connection -> " + address.ToString() + ":" + pport.ToString() + "{0}[0m", E);
                IPEndPoint ipe = new IPEndPoint(address, pport);
                Socket tmpsocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try {
                    tmpsocket.Connect(ipe);
                } catch (Exception err) {
                    // Console.WriteLine("[socutl] {0}[38;2;230;100;100mBUG! {0}[38;2;255;255;255m500 internal error -> {1}", E, err.Message);
                    Console.WriteLine("[sultan->socutl] {0}[38;2;255;0;0mERR! {0}[38;2;255;255;255m{1:X8} -> {2} {0}[0m", E, err.HResult, err.Message);
                }

                if (tmpsocket.Connected) {
                    socks = tmpsocket;
                    break;
                } else {
                    continue;
                }
            }

            if (socks.Connected) {
                Console.WriteLine("[sultan->socutl] {0}[38;2;10;220;10mYAY! {0}[38;2;255;255;255mconnected -> " + host + ":" + pport.ToString() + "{0}[0m", E);
            } else {
                Console.WriteLine("[sultan->socutl] {0}[38;2;255;0;0mERR! {0}[38;2;255;255;255mnot connected -> " + host + ":" + pport.ToString() + "{0}[0m", E);
            }
            return socks;
        }
    }
}
