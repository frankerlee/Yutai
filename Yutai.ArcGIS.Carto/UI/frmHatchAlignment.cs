using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmHatchAlignment : Form
    {
        private Container container_0 = null;
        private IHatchDefinition ihatchDefinition_0 = null;

        public frmHatchAlignment()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoLeft.Checked)
            {
                this.ihatchDefinition_0.Alignment = esriHatchAlignmentType.esriHatchAlignLeft;
            }
            if (this.rdoCenter.Checked)
            {
                this.ihatchDefinition_0.Alignment = esriHatchAlignmentType.esriHatchAlignCenter;
            }
            if (this.rdoRight.Checked)
            {
                this.ihatchDefinition_0.Alignment = esriHatchAlignmentType.esriHatchAlignRight;
            }
            if (this.ihatchDefinition_0 is IHatchLineDefinition)
            {
                (this.ihatchDefinition_0 as IHatchLineDefinition).SupplementalAngle =
                    (double) this.txtSupplementalAngle.Value;
            }
        }

        private void frmHatchAlignment_Load(object sender, EventArgs e)
        {
            if (this.ihatchDefinition_0 != null)
            {
                if (this.ihatchDefinition_0 is IHatchLineDefinition)
                {
                    this.txtSupplementalAngle.Value =
                        (decimal) (this.ihatchDefinition_0 as IHatchLineDefinition).SupplementalAngle;
                    this.rdoCenter.Enabled = true;
                }
                else
                {
                    this.rdoCenter.Enabled = false;
                    this.txtSupplementalAngle.Enabled = false;
                }
                switch (this.ihatchDefinition_0.Alignment)
                {
                    case esriHatchAlignmentType.esriHatchAlignRight:
                        this.rdoLeft.Checked = false;
                        this.rdoCenter.Checked = false;
                        this.rdoRight.Checked = true;
                        break;

                    case esriHatchAlignmentType.esriHatchAlignCenter:
                        this.rdoLeft.Checked = false;
                        this.rdoCenter.Checked = true;
                        this.rdoRight.Checked = false;
                        break;

                    case esriHatchAlignmentType.esriHatchAlignLeft:
                        this.rdoLeft.Checked = true;
                        this.rdoCenter.Checked = false;
                        this.rdoRight.Checked = false;
                        break;
                }
            }
        }

        public IHatchDefinition HatchDefinition
        {
            get { return this.ihatchDefinition_0; }
            set { this.ihatchDefinition_0 = value; }
        }
    }
}