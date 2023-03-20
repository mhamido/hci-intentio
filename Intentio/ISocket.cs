using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Intentio
{
    public abstract class ISocket<T> : IDisposable
    {
        protected TcpClient client;
        protected NetworkStream stream;
        private bool disposedValue;

        protected ISocket(string hostName, int port)
        {
            client = new TcpClient(hostName, port);
            stream = client.GetStream();
        }

        private readonly byte[] buffer = new byte[1024];

        public T Receive()
        {
            try
            {
                Array.Clear(buffer, 0, buffer.Length);
                int bytesReceived = stream.Read(buffer, 0, buffer.Length);
                string contents = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                return Parse(contents);
            }
            catch (SocketException)
            {
                return default;
            }
        }

        public async Task<T> ReceiveAsync()
        {
            try
            {
                Array.Clear(buffer, 0, buffer.Length);
                int bytesReceived = await stream.ReadAsync(buffer);
                string contents = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                return Parse(contents);
            }
            catch (SocketException)
            {
                return default;
            }
        }


        protected abstract T Parse(string input);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stream.Close();
                    client.Close();
                    stream = null;
                    client = null;
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ISocket()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
