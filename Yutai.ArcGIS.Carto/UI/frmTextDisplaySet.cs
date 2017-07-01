using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmTextDisplaySet : Form
    {
        private Container container_0 = null;
        private IHatchDefinition ihatchDefinition_0 = null;

        public frmTextDisplaySet()
        {
            this.InitializeComponent();
        }

        private void frmTextDisplaySet_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            if (this.ihatchDefinition_0 != null)
            {
                this.chkAdjustTextOrientation.Checked = this.ihatchDefinition_0.AdjustTextOrientation;
                this.chkShowSign.Checked = this.ihatchDefinition_0.ShowSign;
                this.rdoTextDisplay.SelectedIndex = (int) this.ihatchDefinition_0.TextDisplay;
                this.cboDisplayPrecision.SelectedIndex = this.ihatchDefinition_0.DisplayPrecision;
            }
        }

        private void rdoTextDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoTextDisplay.SelectedIndex == 0)
            {
                this.txtPrefix.Enabled = false;
                this.txtSuffix.Enabled = false;
                this.btnExpression.Enabled = false;
                this.cboDisplayPrecision.Enabled = true;
                this.chkShowSign.Enabled = true;
            }
            else if (this.rdoTextDisplay.SelectedIndex == 1)
            {
                this.txtPrefix.Enabled = true;
                this.txtSuffix.Enabled = true;
                this.btnExpression.Enabled = false;
                this.txtPrefix.Text = this.ihatchDefinition_0.Prefix;
                this.txtSuffix.Text = this.ihatchDefinition_0.Suffix;
                this.cboDisplayPrecision.Enabled = true;
                this.chkShowSign.Enabled = true;
            }
            else if (this.rdoTextDisplay.SelectedIndex == 2)
            {
                this.txtPrefix.Enabled = false;
                this.txtSuffix.Enabled = false;
                this.btnExpression.Enabled = true;
                this.cboDisplayPrecision.Enabled = false;
                this.chkShowSign.Enabled = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.ihatchDefinition_0.AdjustTextOrientation = this.chkAdjustTextOrientation.Checked;
            this.ihatchDefinition_0.ShowSign = this.chkShowSign.Checked;
            this.ihatchDefinition_0.TextDisplay = (esriHatchTextDisplay) this.rdoTextDisplay.SelectedIndex;
            this.ihatchDefinition_0.DisplayPrecision = this.cboDisplayPrecision.SelectedIndex;
        }

        public IHatchDefinition HatchDefinition
        {
            get { return this.ihatchDefinition_0; }
            set { this.ihatchDefinition_0 = value; }
        }
    }
}