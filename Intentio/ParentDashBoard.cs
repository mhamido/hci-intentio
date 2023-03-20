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
            for (int i = 0; i < children.Count; i++)
            {
                for (int j = 0; j < children[i].AttentionReports.Count; j++)
                {
                    dataGridView1.Rows.Add(i,
                    children[i].Identifier.Name,
                    children[i].AttentionReports[j].TimesDistracted,
                    children[i].AttentionReports[j].TimeToComplete,
                    children[i].AttentionReports[j].LettersMistaken,
                    children[i].AttentionReports[j].NumbersMistaken,
                    (children[i].AttentionReports[j].LettersMistaken + children[i].AttentionReports[j].NumbersMistaken));
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
