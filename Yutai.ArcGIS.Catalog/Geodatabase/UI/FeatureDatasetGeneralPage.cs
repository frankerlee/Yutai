using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class FeatureDatasetGeneralPage : UserControl
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;

        public event ValueChangedHandler ValueChanged;

        public FeatureDatasetGeneralPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.txtName.Text.Trim().Length == 0)
                {
                    return false;
                }
                NewObjectClassHelper.m_pObjectClassHelper.Name = this.txtName.Text;
            }
            return true;
        }

 private void FeatureDatasetGeneralPage_Load(object sender, EventArgs e)
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset != null)
            {
                this.txtName.Text = NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset.Name;
                this.txtName.Properties.ReadOnly = true;
            }
        }

 private void method_0()
        {
            this.bool_0 = true;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }
    }
}

