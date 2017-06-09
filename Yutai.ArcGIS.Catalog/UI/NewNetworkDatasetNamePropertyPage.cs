using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class NewNetworkDatasetNamePropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private Label label1;
        private TextEdit txtNetworkDatasetName;

        public NewNetworkDatasetNamePropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.txtNetworkDatasetName.Text.Trim().Length == 0)
            {
                return false;
            }
            if (NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset == null)
            {
                return false;
            }
            if (NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset.Subsets == null)
            {
                return false;
            }
            IEnumDataset subsets = NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset.Subsets;
            subsets.Reset();
            if (subsets.Next() == null)
            {
                return false;
            }
            NewNetworkDatasetHelper.NewNetworkDataset.NetworkDatasetName = this.txtNetworkDatasetName.Text.Trim();
            return true;
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
            this.label1 = new Label();
            this.txtNetworkDatasetName = new TextEdit();
            this.txtNetworkDatasetName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "网络要素集名称:";
            this.txtNetworkDatasetName.Location = new Point(0x75, 0x17);
            this.txtNetworkDatasetName.Name = "txtNetworkDatasetName";
            this.txtNetworkDatasetName.Size = new Size(0xa2, 0x15);
            this.txtNetworkDatasetName.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtNetworkDatasetName);
            base.Controls.Add(this.label1);
            base.Name = "NewNetworkDatasetNamePropertyPage";
            base.Size = new Size(0x12e, 0xe1);
            base.Load += new EventHandler(this.NewNetworkDatasetNamePropertyPage_Load);
            this.txtNetworkDatasetName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void NewNetworkDatasetNamePropertyPage_Load(object sender, EventArgs e)
        {
            this.txtNetworkDatasetName.Text = NewNetworkDatasetHelper.NewNetworkDataset.NetworkDatasetName;
        }
    }
}

