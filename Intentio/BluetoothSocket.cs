using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intentio
{

    public record Device(string Name, string address) { }

    public class BluetoothSocket : ISocket<IEnumerable<Device>>
    {
        private const int BT_SOCKET_PORT = 25595;

        public BluetoothSocket() : base("localhost", BT_SOCKET_PORT) { }

        protected override IEnumerable<Device> Parse(string input) => JsonSerializer.Deserialize<IEnumerable<Device>>(input);
    }
}
