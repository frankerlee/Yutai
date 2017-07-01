using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmCartoConfig : Form
    {
        private IContainer icontainer_0 = null;
        private IMap imap_0 = null;
        private int int_0 = -1;
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;

        public frmCartoConfig()
        {
            this.InitializeComponent();
            this.textBox_0 = new TextBox();
            this.listView1.Controls.Add(this.textBox_0);
            this.textBox_0.Visible = false;
            this.textBox_0.Leave += new EventHandler(this.textBox_0_Leave);
            this.textBox_0.KeyPress += new KeyPressEventHandler(this.textBox_0_KeyPress);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void btnSetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                SelectedPath = this.txtFloder.Text
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFloder.Text = dialog.SelectedPath;
            }
        }

        private void frmCartoConfig_Load(object sender, EventArgs e)
        {
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem itemAt = this.listView1.GetItemAt(e.X, e.Y);
            if (itemAt != null)
            {
                int left = itemAt.Bounds.Left;
                int num2 = 0;
                while (num2 < this.listView1.Columns.Count)
                {
                    left += this.listView1.Columns[num2].Width;
                    if (left > e.X)
                    {
                        left -= this.listView1.Columns[num2].Width;
                        this.listViewSubItem_0 = itemAt.SubItems[num2];
                        this.int_0 = num2;
                        break;
                    }
                    num2++;
                }
                if (this.int_0 != 0)
                {
                    Control control = this.textBox_0;
                    control.Location = new Point(left,
                        this.listView1.GetItemRect(this.listView1.Items.IndexOf(itemAt)).Y);
                    control.Width = this.listView1.Columns[num2].Width;
                    if (control.Width > this.listView1.Width)
                    {
                        control.Width = this.listView1.ClientRectangle.Width;
                    }
                    control.Text = this.listViewSubItem_0.Text;
                    control.Visible = true;
                    control.BringToFront();
                    control.Focus();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.listViewSubItem_0.Text = this.textBox_0.Text;
                this.textBox_0.Visible = false;
            }
        }

        private void textBox_0_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            this.listViewSubItem_0.Text = control.Text;
            control.Visible = false;
        }

        private void txtFloder_TextChanged(object sender, EventArgs e)
        {
        }

        public IMap Map
        {
            get { return this.imap_0; }
            set { this.imap_0 = value; }
        }
    }
}