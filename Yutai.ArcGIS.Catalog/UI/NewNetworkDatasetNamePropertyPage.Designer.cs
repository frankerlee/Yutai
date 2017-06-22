using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class NewNetworkDatasetNamePropertyPage
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
            this.label1 = new Label();
            this.txtNetworkDatasetName = new TextEdit();
            this.txtNetworkDatasetName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "网络要素集名称:";
            this.txtNetworkDatasetName.Location = new Point(117, 23);
            this.txtNetworkDatasetName.Name = "txtNetworkDatasetName";
            this.txtNetworkDatasetName.Size = new Size(162, 21);
            this.txtNetworkDatasetName.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtNetworkDatasetName);
            base.Controls.Add(this.label1);
            base.Name = "NewNetworkDatasetNamePropertyPage";
            base.Size = new Size(302, 225);
            base.Load += new EventHandler(this.NewNetworkDatasetNamePropertyPage_Load);
            this.txtNetworkDatasetName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label label1;
        private TextEdit txtNetworkDatasetName;
    }
}