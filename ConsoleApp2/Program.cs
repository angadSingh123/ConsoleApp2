using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp2
{
    class Program
    {

        const int PORT_DEFAULT = 8080;

        static void Main(string[] args)
        {

            if (args.Length > 3 || args.Length < 2) throw new ArgumentException("Usage: ConsoleApp2 <server> [<port>] <message>");

            String server = (args[0]);

            int servPort = (args.Length == 3) ? Int32.Parse(args[1]) : PORT_DEFAULT;

            byte[] buffer = Encoding.ASCII.GetBytes(args[args.Length - 1]);

            UdpClient client = null;

            try
            {

                client = new UdpClient();

                client.Send(buffer, buffer.Length, server, servPort);

                IPEndPoint endp = new IPEndPoint(IPAddress.Any, servPort);

                byte[] recvPacket = client.Receive(ref endp);

                Console.WriteLine("Received {0} bytes from {1}: {2}", recvPacket.Length, endp.Address, Encoding.ASCII.GetString(recvPacket));


            }
            catch (SocketException e) {

                Environment.Exit(e.ErrorCode);

            }

            client.Close();

        }
    }
}
