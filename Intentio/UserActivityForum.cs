using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intentio
{
    public class UserActivityForum
    {
        public static Form Run(Device device)
        {
            // TODO: Associate a user with a specific child/parent
            using var db = new Database();
            var user = db.GetByDevice(device);

            if (user == null)
            {
                var reg = new RegistrationForm(device);
                reg.ShowDialog();
                user = reg.GetRegisteredUser();
                Debug.Assert(user != null);
            }
            // TODO: Start activity
            // User is registered, start activity.
            return user.StartActivity();
        }
    }
}
