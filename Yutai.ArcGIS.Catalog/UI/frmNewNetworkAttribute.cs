using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class frmNewNetworkAttribute : Form
    {
        private IContainer icontainer_0 = null;
        private INetworkAttribute inetworkAttribute_0 = null;

        public frmNewNetworkAttribute()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Length != 0)
            {
                this.inetworkAttribute_0 = new EvaluatedNetworkAttributeClass();
                this.inetworkAttribute_0.Name = this.txtName.Text.Trim();
                this.inetworkAttribute_0.UsageType = (esriNetworkAttributeUsageType) this.cboUsageType.SelectedIndex;
                this.inetworkAttribute_0.Units = this.method_1();
                this.inetworkAttribute_0.DataType = this.method_2();
                base.DialogResult = DialogResult.OK;
            }
        }

        private void cboUsageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void frmNewNetworkAttribute_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            this.cboDataType.Properties.Items.Clear();
            if (this.cboUsageType.SelectedIndex == 0)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Integer", "Float", "Double" });
            }
            else if (this.cboUsageType.SelectedIndex == 1)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Integer", "Float", "Double", "Boolean" });
            }
            else if (this.cboUsageType.SelectedIndex == 2)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Boolean" });
            }
            else if (this.cboUsageType.SelectedIndex == 3)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Integer" });
            }
            if (this.cboDataType.Properties.Items.Count > 0)
            {
                this.cboDataType.SelectedIndex = 0;
            }
        }

        private esriNetworkAttributeUnits method_1()
        {
            if ((this.cboUnit.SelectedIndex >= 0) && (this.cboUnit.SelectedIndex <= 1))
            {
                return (esriNetworkAttributeUnits) this.cboUnit.SelectedIndex;
            }
            if ((this.cboUnit.SelectedIndex >= 2) && (this.cboUnit.SelectedIndex <= 11))
            {
                return (esriNetworkAttributeUnits) (this.cboUnit.SelectedIndex + 1);
            }
            if ((this.cboUnit.SelectedIndex >= 12) && (this.cboUnit.SelectedIndex <= 15))
            {
                return (esriNetworkAttributeUnits) (this.cboUnit.SelectedIndex + 8);
            }
            return esriNetworkAttributeUnits.esriNAUUnknown;
        }

        private esriNetworkAttributeDataType method_2()
        {
            if (this.cboUsageType.SelectedIndex == 0)
            {
                switch (this.cboDataType.SelectedIndex)
                {
                    case 0:
                        return esriNetworkAttributeDataType.esriNADTInteger;

                    case 1:
                        return esriNetworkAttributeDataType.esriNADTFloat;

                    case 2:
                        return esriNetworkAttributeDataType.esriNADTDouble;
                }
            }
            else if (this.cboUsageType.SelectedIndex == 1)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Integer", "Float", "Double", "Boolean" });
                switch (this.cboDataType.SelectedIndex)
                {
                    case 0:
                        return esriNetworkAttributeDataType.esriNADTInteger;

                    case 1:
                        return esriNetworkAttributeDataType.esriNADTFloat;

                    case 2:
                        return esriNetworkAttributeDataType.esriNADTDouble;

                    case 3:
                        return esriNetworkAttributeDataType.esriNADTBoolean;
                }
            }
            else if (this.cboUsageType.SelectedIndex == 2)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Boolean" });
                if (this.cboDataType.SelectedIndex == 0)
                {
                    return esriNetworkAttributeDataType.esriNADTBoolean;
                }
            }
            else if (this.cboUsageType.SelectedIndex == 3)
            {
                this.cboDataType.Properties.Items.AddRange(new object[] { "Integer" });
                if (this.cboDataType.SelectedIndex == 0)
                {
                    return esriNetworkAttributeDataType.esriNADTInteger;
                }
            }
            return esriNetworkAttributeDataType.esriNADTBoolean;
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                this.btnOK.Enabled = false;
            }
            else
            {
                this.btnOK.Enabled = true;
            }
        }

        public INetworkAttribute NetworkAttribute
        {
            get
            {
                return this.inetworkAttribute_0;
            }
            set
            {
                this.inetworkAttribute_0 = value;
            }
        }
    }
}

