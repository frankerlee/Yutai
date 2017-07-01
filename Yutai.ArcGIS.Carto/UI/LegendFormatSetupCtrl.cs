using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class LegendFormatSetupCtrl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ILegend ilegend_0 = null;

        public LegendFormatSetupCtrl()
        {
            this.InitializeComponent();
        }

        private void LegendFormatSetupCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.txtGroupGap.Text = this.ilegend_0.Format.GroupGap.ToString();
            this.txtHeadingGap.Text = this.ilegend_0.Format.HeadingGap.ToString();
            this.txtHorizontalItemGap.Text = this.ilegend_0.Format.HorizontalItemGap.ToString();
            this.txtHorizontalPatchGap.Text = this.ilegend_0.Format.HorizontalPatchGap.ToString();
            this.txtTextGap.Text = this.ilegend_0.Format.TextGap.ToString();
            this.txtTitleGap.Text = this.ilegend_0.Format.TitleGap.ToString();
            this.txtVerticalItemGap.Text = this.ilegend_0.Format.VerticalItemGap.ToString();
        }

        private void txtGroupGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtGroupGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtGroupGap.Text);
                    this.ilegend_0.Format.GroupGap = num;
                }
                catch (Exception)
                {
                    this.txtGroupGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtGroupGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = true;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtGroupGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtHeadingGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHeadingGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtHeadingGap.Text);
                    this.ilegend_0.Format.HeadingGap = num;
                }
                catch (Exception)
                {
                    this.txtHeadingGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtHeadingGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = true;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtHeadingGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtHorizontalItemGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHorizontalItemGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtHorizontalItemGap.Text);
                    this.ilegend_0.Format.HorizontalItemGap = num;
                }
                catch (Exception)
                {
                    this.txtHorizontalItemGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtHorizontalItemGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = true;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtHorizontalItemGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtHorizontalPatchGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHorizontalPatchGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtHorizontalPatchGap.Text);
                    this.ilegend_0.Format.HorizontalPatchGap = num;
                }
                catch (Exception)
                {
                    this.txtHorizontalPatchGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtHorizontalPatchGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = true;
            this.pictureEdit8.Visible = false;
        }

        private void txtHorizontalPatchGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtTextGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtTextGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtTextGap.Text);
                    this.ilegend_0.Format.TextGap = num;
                }
                catch (Exception)
                {
                    this.txtTextGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtTextGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = true;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtTextGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtTitleGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtTitleGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtTitleGap.Text);
                    this.ilegend_0.Format.TitleGap = num;
                }
                catch (Exception)
                {
                    this.txtTitleGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtTitleGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = true;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtTitleGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtVerticalItemGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtVerticalItemGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtVerticalItemGap.Text);
                    this.ilegend_0.Format.VerticalItemGap = num;
                }
                catch (Exception)
                {
                    this.txtVerticalItemGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtVerticalItemGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = true;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtVerticalItemGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        public ILegend Legend
        {
            set { this.ilegend_0 = value; }
        }
    }
}