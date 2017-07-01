using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class ComboBoxTreeView : ComboBox
    {
        private ToolStripControlHost toolStripControlHost_0;
        private ToolStripDropDown toolStripDropDown_0;
        private const int WM_LBUTTONDBLCLK = 515;
        private const int WM_LBUTTONDOWN = 513;

        public ComboBoxTreeView()
        {
            System.Windows.Forms.TreeView c = new System.Windows.Forms.TreeView();
            c.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
            c.BorderStyle = BorderStyle.None;
            this.toolStripControlHost_0 = new ToolStripControlHost(c);
            this.toolStripDropDown_0 = new ToolStripDropDown();
            this.toolStripDropDown_0.Width = base.Width;
            this.toolStripDropDown_0.Items.Add(this.toolStripControlHost_0);
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.toolStripDropDown_0 != null))
            {
                this.toolStripDropDown_0.Dispose();
                this.toolStripDropDown_0 = null;
            }
            base.Dispose(bool_0);
        }

        private void method_0()
        {
            if (this.toolStripDropDown_0 != null)
            {
                this.toolStripControlHost_0.Size = new Size(base.DropDownWidth - 2, base.DropDownHeight);
                this.toolStripDropDown_0.Show(this, 0, base.Height);
            }
        }

        public void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Text = this.TreeView.SelectedNode.Text;
            this.toolStripDropDown_0.Close();
        }

        protected override void WndProc(ref Message message_0)
        {
            if ((message_0.Msg == 515) || (message_0.Msg == 513))
            {
                this.method_0();
            }
            else
            {
                base.WndProc(ref message_0);
            }
        }

        public System.Windows.Forms.TreeView TreeView
        {
            get { return (this.toolStripControlHost_0.Control as System.Windows.Forms.TreeView); }
        }
    }
}