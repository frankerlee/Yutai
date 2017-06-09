using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class NetworkDirectionGeneralPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public NetworkDirectionGeneralPage()
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "NetworkDirectionGeneralPage";
            base.Size = new Size(390, 0x106);
            base.ResumeLayout(false);
        }
    }
}

