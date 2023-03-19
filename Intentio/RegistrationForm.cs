using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intentio
{
    public partial class RegistrationForm : Form
    {
        private readonly Device device;

        public RegistrationForm(Device device)
        {
            InitializeComponent();
            this.device = device;
            Load += RegistrationForm_Load;
        }

        public IUser GetRegisteredUser()
        {
            return user;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            DeviceLabel.Text = $"{device.Address} ({device.Name})";
            UserTypeComboBox.Items.Add("Child");
            UserTypeComboBox.Items.Add("Parent");
            UserTypeComboBox.SelectedIndex = 0;
        }

        private IUser user;

        private void SubmitRegistration_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameField.Text))
            {
                MessageBox.Show("Name cannot be empty");
                return;
            }

            using var database = new Database();
            user = database.AddOrUpdate(UserTypeComboBox.SelectedIndex switch
            {
                // Child
                0 => IUser.Child(device),
                // Parent
                _ => IUser.Parent(device),
            });

            Close();
        }
    }
}
