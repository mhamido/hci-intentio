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
    public partial class ParentDashBoard : Form
    {
        private List<IUser> children = new List<IUser>();

        public ParentDashBoard(IUser user)
        {
            InitializeComponent();
            Load += ParentDashBoard_Load;
        }

        private void ParentDashBoard_Load(object sender, EventArgs e)
        {
            using var db = new Database();
            children = db.Children;
        }
    }
}
