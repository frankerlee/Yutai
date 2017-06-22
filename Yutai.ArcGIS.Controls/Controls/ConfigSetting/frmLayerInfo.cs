using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class frmLayerInfo : Form
    {

        public frmLayerInfo()
        {
            this.InitializeComponent();
            this.LayerName = "";
            this.MinScale = 0.0;
            this.MaxScale = 0.0;
        }

 private void frmLayerInfo_Load(object sender, EventArgs e)
        {
            this.txtLayerName.Text = this.LayerName;
            this.txtMinScale.Text = this.MinScale.ToString();
            this.txtMaxScale.Text = this.MaxScale.ToString();
        }

 private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.txtLayerName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入层名");
            }
            else
            {
                try
                {
                    this.MinScale = double.Parse(this.txtMinScale.Text);
                    this.MaxScale = double.Parse(this.txtMaxScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                    return;
                }
                this.LayerName = this.txtLayerName.Text.Trim();
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        public string LayerName { get; set; }

        public double MaxScale { get; set; }

        public double MinScale { get; set; }
    }
}

