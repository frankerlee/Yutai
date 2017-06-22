using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class NewNetworkDatasetConnectivityPage
    {
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
            this.btnConnectivity = new SimpleButton();
            base.SuspendLayout();
            this.btnConnectivity.Location = new Point(43, 64);
            this.btnConnectivity.Name = "btnConnectivity";
            this.btnConnectivity.Size = new Size(108, 23);
            this.btnConnectivity.TabIndex = 0;
            this.btnConnectivity.Text = "连通性设置...";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnConnectivity);
            base.Name = "NewNetworkDatasetConnectivityPage";
            base.Size = new Size(274, 193);
            base.ResumeLayout(false);
        }
    
        private SimpleButton btnConnectivity;
    }
}