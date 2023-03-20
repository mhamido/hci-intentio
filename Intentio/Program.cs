using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intentio
{
    internal static class Program
    {
        // TODO: Should be \false\ during the discussion.
        private static bool BypassBluetooth = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using var bluetoothSocket = new BluetoothSocket();
                Application.EnableVisualStyles();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(bluetoothSocket));
            }
            catch (SocketException)
            {
                if (!BypassBluetooth)
                {
                    MessageBox.Show("Bluetooth server not started.\nStart it and try again");
                    Main();
                }
                else
                {
                    var result = MessageBox.Show("[Debug] Bypassing bluetooth. What kind of user are you? A child?", "[Debug Bypass]", MessageBoxButtons.YesNo);
                    var device = result switch
                    {
                        DialogResult.Yes => Device.DebugChild,
                        _ => Device.DebugParent
                    };

                     UserActivityForum.Run(device);
                }
            }
        }
    }
}
