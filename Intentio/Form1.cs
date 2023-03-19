using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace Intentio
{
    public partial class Form1 : Form
    {
        private BluetoothSocket socket;
        private Timer timer = new Timer();
        private List<Device> devices = new List<Device>();

        public Form1(BluetoothSocket bluetoothSocket)
        {
            InitializeComponent();
            socket = bluetoothSocket;
            Load += Form1_Load;
            timer.Tick += Timer_Tick;
            timer.Interval = 10_000;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            devices = socket.Receive();
            DisplayItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void DisplayItems()
        {
            DisplayDevicesBox.Items.Clear();

            foreach (var device in devices)
            {
                DisplayDevicesBox.Items.Add($"{device.Name} - {device.Address}");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            devices = socket.Receive();
            DisplayItems();
        }

        private void Selected_Click(object sender, EventArgs e)
        {
            if (DisplayDevicesBox.SelectedItem == null)
            {
                MessageBox.Show("You have not selected a device!", "Device not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            timer.Stop();
            // Transfer to UserActivityForm and exit.
            for (int i = 0; i < devices.Count; i++)
            {
                if (DisplayDevicesBox.GetSelected(i))
                {
                    //MessageBox.Show(devices[i].ToString());
                    // Transfer to user activity forumn
                    Hide();
                    var activity = UserActivityForum.Run(devices[i]);
                    //activity.FormClosed += (s, args) => Close();
                    break;
                }
            }
        }
    }
}
