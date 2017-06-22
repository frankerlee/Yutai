using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    internal partial class frmViewerOrMagnifierPropertyPage : Form
    {
        private MagnifierOrViewerProperty m_proprty = null;

        public frmViewerOrMagnifierPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoMagnifier.Checked)
            {
                this.m_proprty.m_Formtype = FormType.Magnifier;
            }
            else
            {
                this.m_proprty.m_Formtype = FormType.Viewer;
            }
            if (this.rdoMagnifyBy.Checked)
            {
                this.m_proprty.m_ZoomType = ZoomType.MagnifyBy;
                this.m_proprty.m_magnify = this.cboMagnify.Text;
            }
            else
            {
                this.m_proprty.m_ZoomType = ZoomType.FixedScale;
                this.m_proprty.m_scale = this.cboFixedScale.Text;
            }
        }

 private void frmViewerOrMagnifierPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_proprty.m_Formtype == FormType.Magnifier)
            {
                this.rdoMagnifier.Checked = true;
                this.rdoViewer.Checked = false;
            }
            else
            {
                this.rdoViewer.Checked = true;
                this.rdoMagnifier.Checked = false;
            }
            if (this.m_proprty.m_ZoomType == ZoomType.FixedScale)
            {
                this.rdoFixedScale.Checked = true;
                this.rdoMagnifyBy.Checked = false;
                this.cboFixedScale.Text = this.m_proprty.m_scale;
                this.cboMagnify.Text = this.m_proprty.m_magnify;
                this.cboFixedScale.Enabled = true;
                this.cboMagnify.Enabled = false;
            }
            else
            {
                this.rdoMagnifyBy.Checked = true;
                this.rdoFixedScale.Checked = false;
                this.cboMagnify.Text = this.m_proprty.m_magnify;
                this.cboFixedScale.Text = this.m_proprty.m_scale;
                this.cboFixedScale.Enabled = false;
                this.cboMagnify.Enabled = true;
            }
        }

 private void rdoMagnifier_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoMagnifier.Checked)
            {
                this.rdoMagnifyBy.Enabled = true;
                this.cboMagnify.Enabled = true;
            }
            else
            {
                this.rdoMagnifyBy.Enabled = false;
                this.cboMagnify.Enabled = false;
                if (this.rdoMagnifyBy.Checked)
                {
                    this.rdoFixedScale.Checked = true;
                }
            }
        }

        private void rdoMagnifyBy_CheckedChanged(object sender, EventArgs e)
        {
            this.cboFixedScale.Enabled = !this.rdoMagnifyBy.Checked;
            this.cboMagnify.Enabled = this.rdoMagnifyBy.Checked;
        }

        internal MagnifierOrViewerProperty Property
        {
            set
            {
                this.m_proprty = value;
            }
        }
    }
}

