using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmLableRotateField : Form
    {
        private Container container_0 = null;
        public bool m_PerpendicularToAngle = false;
        public IFields m_pFields = null;
        public esriLabelRotationType m_RotationType = esriLabelRotationType.esriRotateLabelGeographic;
        public string m_RoteteField = "";

        public frmLableRotateField()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_RoteteField = this.cboFields.Text;
            this.m_PerpendicularToAngle = this.chkPerpendicularToAngle.Checked;
            if (this.rdoRotationType.SelectedIndex == 0)
            {
                this.m_RotationType = esriLabelRotationType.esriRotateLabelGeographic;
            }
            else
            {
                this.m_RotationType = esriLabelRotationType.esriRotateLabelArithmetic;
            }
        }

        private void frmLableRotateField_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_pFields.FieldCount; i++)
            {
                IField field = this.m_pFields.get_Field(i);
                if (((((field.Type == esriFieldType.esriFieldTypeDate) ||
                       (field.Type == esriFieldType.esriFieldTypeDouble)) ||
                      ((field.Type == esriFieldType.esriFieldTypeGlobalID) ||
                       (field.Type == esriFieldType.esriFieldTypeGUID))) ||
                     (((field.Type == esriFieldType.esriFieldTypeInteger) ||
                       (field.Type == esriFieldType.esriFieldTypeOID)) ||
                      ((field.Type == esriFieldType.esriFieldTypeSingle) ||
                       (field.Type == esriFieldType.esriFieldTypeSmallInteger)))) ||
                    (field.Type == esriFieldType.esriFieldTypeString))
                {
                    this.cboFields.Properties.Items.Add(field.Name);
                }
            }
            this.cboFields.Text = this.m_RoteteField;
            if (this.m_RotationType == esriLabelRotationType.esriRotateLabelGeographic)
            {
                this.rdoRotationType.SelectedIndex = 0;
            }
            else
            {
                this.rdoRotationType.SelectedIndex = 1;
            }
            this.chkPerpendicularToAngle.Checked = this.m_PerpendicularToAngle;
        }
    }
}