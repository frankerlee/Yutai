using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerGeneralPropertyCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILayer ilayer_0 = null;

        public LayerGeneralPropertyCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ilayer_0.Name = this.txtLayerName.Text;
                this.ilayer_0.Visible = this.chkVisible.Checked;
                if (this.rdoDisplayScale.SelectedIndex == 0)
                {
                    this.ilayer_0.MinimumScale = 0.0;
                    this.ilayer_0.MaximumScale = 0.0;
                }
                else
                {
                    try
                    {
                        this.ilayer_0.MinimumScale = Convert.ToDouble(this.txtMinScale.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        this.ilayer_0.MaximumScale = Convert.ToDouble(this.txtMaxScale.Text);
                    }
                    catch
                    {
                    }
                }
            }
            return true;
        }

        private void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void LayerGeneralPropertyCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.txtLayerName.Text = this.ilayer_0.Name;
            this.chkVisible.Checked = this.ilayer_0.Visible;
            this.txtMaxScale.Text = this.ilayer_0.MaximumScale.ToString();
            this.txtMinScale.Text = this.ilayer_0.MinimumScale.ToString();
            if ((this.ilayer_0.MaximumScale == 0.0) && (this.ilayer_0.MinimumScale == 0.0))
            {
                this.rdoDisplayScale.SelectedIndex = 0;
            }
            else
            {
                this.rdoDisplayScale.SelectedIndex = 1;
            }
            this.rdoDisplayScale_SelectedIndexChanged(this, null);
        }

        private void rdoDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.txtMaxScale.Enabled = false;
                this.txtMinScale.Enabled = false;
            }
            else
            {
                this.txtMaxScale.Enabled = true;
                this.txtMinScale.Enabled = true;
            }
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtLayerName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtMaxScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtMinScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        public ILayer Layer
        {
            set { this.ilayer_0 = value; }
        }

        public object SelectItem
        {
            set { this.ilayer_0 = value as ILayer; }
        }
    }
}