using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class TableJoins : Form
    {
        public TableJoins()
        {
            InitializeComponent();
            _joinsGrid1.AddColumns();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            JoinTable frm = new JoinTable();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.Model != null)
                {
                    _joinsGrid1.AddItem(frm.Model);
                }
            }
        }
        
        private void btnStop_Click(object sender, EventArgs e)
        {
            _joinsGrid1.StopJoin();
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            _joinsGrid1.StopAll();
        }
    }
}
