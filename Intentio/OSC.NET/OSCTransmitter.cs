using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Intentio.OSC.NET
{
    public class OSCTransmitter
    {
        protected UdpClient udpClient;
        protected string remoteHost;
        protected int remotePort;

        public OSCTransmitter(string remoteHost, int remotePort)
        {
            this.remoteHost = remoteHost;
            this.remotePort = remotePort;
            Connect();
        }

        public void Connect()
        {
            if (this.udpClient != null) Close();
            this.udpClient = new UdpClient(this.remoteHost, this.remotePort);
        }

        public void Close()
        {
            this.udpClient.Close();
            this.udpClient = null;
        }

        public int Send(OSCPacket packet)
        {
            int byteNum = 0;
            byte[] data = packet.BinaryData;
            try
            {
                byteNum = this.udpClient.Send(data, data.Length);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }

            return byteNum;
        }
    }
}
