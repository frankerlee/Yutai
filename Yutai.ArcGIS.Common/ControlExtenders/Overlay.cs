using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    internal sealed class Overlay : Form
    {
        public DockStyle Dock;
        public Control DockHostControl;
        private IContainer icontainer_0 = null;

        public Overlay()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            this.BackColor = SystemColors.ActiveCaption;
            base.ControlBox = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Overlay";
            base.Opacity = 0.3;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Overlay";
            base.ResumeLayout(false);
        }
    }
}

