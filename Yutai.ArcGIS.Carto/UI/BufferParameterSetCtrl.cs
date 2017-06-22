using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class BufferParameterSetCtrl : UserControl
    {
        private Container container_0 = null;

        public BufferParameterSetCtrl()
        {
            this.InitializeComponent();
        }

        private void BufferParameterSetCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void cboDisField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDisField.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_FieldName = this.cboDisField.Text;
            }
        }

        private void cboUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboUnits.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_Units = (esriUnits) this.cboUnits.SelectedIndex;
            }
        }

 public void Init()
        {
            this.cboDisField.Properties.Items.Clear();
            if (BufferHelper.m_BufferHelper.m_SourceType == 0)
            {
                this.rdoFromProperty.Enabled = false;
                this.cboDisField.Enabled = false;
                if (this.rdoFromProperty.Checked)
                {
                    this.rdoSetDis.Checked = true;
                    this.rdoFromProperty.Checked = false;
                    BufferHelper.m_BufferHelper.m_BufferType = 0;
                }
            }
            else
            {
                IFields fields = BufferHelper.m_BufferHelper.m_pFeatureLayer.FeatureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeSingle)) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)) || (field.Type == esriFieldType.esriFieldTypeInteger))
                    {
                        this.cboDisField.Properties.Items.Add(field.AliasName);
                    }
                }
                this.cboDisField.Enabled = true;
                if (this.cboDisField.Properties.Items.Count > 0)
                {
                    this.cboDisField.SelectedIndex = 0;
                }
                this.rdoFromProperty.Enabled = true;
                if (this.rdoSetDis.Checked)
                {
                    BufferHelper.m_BufferHelper.m_BufferType = 0;
                }
                if (this.rdoFromProperty.Checked)
                {
                    BufferHelper.m_BufferHelper.m_BufferType = 1;
                }
            }
            this.method_0();
        }

 private void method_0()
        {
            this.txtDistances.Enabled = this.rdoSetDis.Checked;
            this.cboDisField.Enabled = this.rdoFromProperty.Checked;
            this.lblRingCount.Enabled = this.rdoMoreBuffer.Checked;
            this.txtCount.Enabled = this.rdoMoreBuffer.Checked;
            this.lblRingCount.Enabled = this.rdoMoreBuffer.Checked;
            this.txtSpace.Enabled = this.rdoMoreBuffer.Checked;
        }

        private void rdoFromProperty_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_BufferType = 1;
            this.method_0();
        }

        private void rdoMoreBuffer_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_BufferType = 2;
            this.method_0();
        }

        private void rdoSetDis_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_BufferType = 0;
            this.method_0();
        }

        private void txtCount_EditValueChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_Count = (int) this.txtCount.EditValue;
        }

        private void txtDistances_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BufferHelper.m_BufferHelper.m_dblRadius = double.Parse(this.txtDistances.Text);
            }
            catch
            {
            }
        }

        private void txtSpace_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BufferHelper.m_BufferHelper.m_dblStep = double.Parse(this.txtSpace.Text);
            }
            catch
            {
            }
        }
    }
}

