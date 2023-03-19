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

    public record Device(string Name, string Address)
    {
        public readonly static Device DebugChild = new Device("Alyaa", "E8:07:BF:A2:58:98");
        public readonly static Device DebugParent = new Device("Omar", "E8:07:BF:A2:58:99");
    }

    public sealed class BluetoothSocket : ISocket<List<Device>>
    {
        private const int BT_SOCKET_PORT = 25595;

        public BluetoothSocket() : base("localhost", BT_SOCKET_PORT) { }

        protected override List<Device> Parse(string input) => JsonSerializer.Deserialize<List<Device>>(input);
    }
}
