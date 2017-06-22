using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmAnnoScaleSet : Form
    {
        private Container container_0 = null;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;

        public frmAnnoScaleSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.iannotateLayerProperties_0.AnnotationMaximumScale = 0.0;
                this.iannotateLayerProperties_0.AnnotationMinimumScale = 0.0;
            }
            else
            {
                double num = 0.0;
                double num2 = 0.0;
                try
                {
                    num = double.Parse(this.txtMaxScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                    return;
                }
                try
                {
                    num2 = double.Parse(this.txtMinScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                    return;
                }
                this.iannotateLayerProperties_0.AnnotationMaximumScale = num;
                this.iannotateLayerProperties_0.AnnotationMinimumScale = num2;
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

 private void frmAnnoScaleSet_Load(object sender, EventArgs e)
        {
            if (this.iannotateLayerProperties_0 != null)
            {
                this.txtMaxScale.Text = this.iannotateLayerProperties_0.AnnotationMaximumScale.ToString();
                this.txtMinScale.Text = this.iannotateLayerProperties_0.AnnotationMinimumScale.ToString();
                if ((this.iannotateLayerProperties_0.AnnotationMinimumScale == 0.0) && (this.iannotateLayerProperties_0.AnnotationMaximumScale == 0.0))
                {
                    this.rdoDisplayScale.SelectedIndex = 0;
                }
                else
                {
                    this.rdoDisplayScale.SelectedIndex = 1;
                }
                if (this.rdoDisplayScale.SelectedIndex == 0)
                {
                    this.panelScaleSet.Enabled = false;
                }
                else
                {
                    this.panelScaleSet.Enabled = true;
                }
            }
        }

 private void rdoDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.panelScaleSet.Enabled = false;
            }
            else
            {
                this.panelScaleSet.Enabled = true;
            }
        }

        public IAnnotateLayerProperties AnnotateLayerProperties
        {
            set
            {
                this.iannotateLayerProperties_0 = value;
            }
        }
    }
}

