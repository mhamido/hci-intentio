using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Intentio
{
    public partial class Form1 : Form
    {
        private BluetoothSocket socket;
        private Timer timer = new Timer();
        public Form1(BluetoothSocket bluetoothSocket)
        {
            InitializeComponent();
            socket = bluetoothSocket;
            Load += Form1_Load;
            timer.Tick += Timer_Tick;
            timer.Interval = 5000;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DisplayItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
            DisplayItems();
        }

        private void DisplayItems()
        {
            var ui = TaskScheduler.FromCurrentSynchronizationContext();
            var devicesTask = Task.Factory.StartNew(() => socket.Receive());
            devicesTask.ContinueWith(devices => {
                if (devices.Result)
                foreach (var (name, address) in devices)
                {
                    Task.Factory.StartNew(() => {
                    })
                    DisplayDevicesBox.Items.Add(new ListViewItem($"{name} - {address}"));
                }
            }, ui);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
