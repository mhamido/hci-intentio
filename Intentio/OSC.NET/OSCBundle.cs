using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intentio.OSC.NET
{
    public class OSCBundle : OSCPacket
    {
        protected const string BUNDLE = "#bundle";
        private DateTime timestamp = new DateTime();

        public OSCBundle(DateTime ts, bool extendedMode = false) : base(extendedMode)
        {
            this.address = BUNDLE;
            this.timestamp = ts;
        }

        public OSCBundle(long ts, bool extendedMode = false) : base(extendedMode)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            timestamp = start.AddMilliseconds(ts).ToLocalTime();
        }


        public OSCBundle(bool extendedMode = false) : base(extendedMode)
        {
            this.address = BUNDLE;
            this.timestamp = DateTime.Now;
        }

        override protected void pack()
        {
            ArrayList data = new ArrayList();

            addBytes(data, packString(this.Address));
            padNull(data);
            addBytes(data, packTimeTag(timestamp));  // fixed point, 8 bytes

            foreach (OSCPacket oscPacket in this.Values)
            {
                if (oscPacket != null)
                {
                    byte[] bs = oscPacket.BinaryData;
                    addBytes(data, packInt(bs.Length));
                    addBytes(data, bs);
                }
                else
                {
                    // TODO
                }
            }

            this.binaryData = (byte[])data.ToArray(typeof(byte));
        }

        public static new OSCBundle Unpack(byte[] bytes, ref int start, int end, bool extendedMode = false)
        {

            string address = unpackString(bytes, ref start);
            //Console.WriteLine("bundle: " + address);
            if (!address.Equals(BUNDLE)) return null; // TODO

            DateTime timestamp = unpackTimeTag(bytes, ref start);
            OSCBundle bundle = new OSCBundle(timestamp, extendedMode);

            while (start < end)
            {
                int length = unpackInt(bytes, ref start);
                int sub_end = start + length;
                bundle.Append(OSCPacket.Unpack(bytes, ref start, sub_end, extendedMode));
            }

            return bundle;
        }

        public DateTime getTimeStamp()
        {
            return timestamp;
        }

        override public void Append(object value)
        {
            if (value is OSCPacket)
            {
                values.Add(value);
            }
            else
            {
                // TODO: exception
            }
        }

        override public bool IsBundle() { return true; }
    }
}
