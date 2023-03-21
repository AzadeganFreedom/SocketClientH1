using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClientH1
{
    internal class SocketClient
    {
        public SocketClient()
        {
            IPAddress serverIp = GetServerIpAddress();
            IPEndPoint endPoint = new IPEndPoint(serverIp, 11000);
            while (true) StartClient(GetMessage(), endPoint);
            //StartClient(endPoint);
        }

        private void StartClient(string msg, IPEndPoint endPoint)
        {
            Socket sender = new(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            Console.WriteLine($"Connecting to : {endPoint}");
            sender.Connect(endPoint);

            byte[] byteArrayu = System.Text.Encoding.ASCII.GetBytes(msg);
            sender.Send(byteArrayu);

            byte[] msgFromServer = new byte[1024];
            int recieved = sender.Receive(msgFromServer);
            string msgRecieved = System.Text.Encoding.ASCII.GetString(msgFromServer, 0, recieved);
            Console.WriteLine($"Message from server: {msgRecieved}");

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        private string GetMessage()
        {
            Console.Write("Input Message: ");
            return Console.ReadLine()+"<EOM>";
        }

        private IPAddress GetServerIpAddress()
        {
            Console.Write("Input server IP: ");
            IPAddress? ip = IPAddress.TryParse(Console.ReadLine(), out ip) ? ip : IPAddress.Parse("102.168.2.3");
            //do
            //{
            //    Console.Write("Input server IP: ");
            //}
            //while( !IPAddress.TryParse(Console.ReadLine(), out ip));
            
            return ip;
        }
    }
}
