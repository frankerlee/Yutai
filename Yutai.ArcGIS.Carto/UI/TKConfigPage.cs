using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class TKConfigPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;

        public TKConfigPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            try
            {
                num = double.Parse(this.txtInOutDis.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.txtOutBorderWidth.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num3 = double.Parse(this.txtTitleSpace.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            if (this.checkBox1.Checked && (this.txtLegendItem.Text.Length == 0))
            {
                MessageBox.Show("请选择图例模板！");
                return false;
            }
            this.jlktkassiatant_0.HasLegend = this.checkBox1.Checked;
            this.jlktkassiatant_0.LegendTemplate = this.checkBox1.Checked ? this.txtLegendItem.Text : "";
            this.jlktkassiatant_0.InOutDist = num;
            this.jlktkassiatant_0.OutBorderWidth = num2;
            this.jlktkassiatant_0.TitleDist = num3;
            this.jlktkassiatant_0.Row1Col1Text = this.txtR1C1.Text.Trim();
            this.jlktkassiatant_0.Row1Col2Text = this.txtR1C2.Text.Trim();
            this.jlktkassiatant_0.Row1Col3Text = this.txtR1C3.Text.Trim();
            this.jlktkassiatant_0.Row2Col1Text = this.txtR2C1.Text.Trim();
            this.jlktkassiatant_0.Row2Col3Text = this.txtR2C3.Text.Trim();
            this.jlktkassiatant_0.Row3Col1Text = this.txtR3C1.Text.Trim();
            this.jlktkassiatant_0.Row3Col2Text = this.txtR3C2.Text.Trim();
            this.jlktkassiatant_0.Row3Col3Text = this.txtR3C3.Text.Trim();
            return true;
        }

        private void btnSelectLegend_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtLegendItem.Text = dialog.FileName;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.btnSelectLegend.Enabled = this.checkBox1.Checked;
            this.txtLegendItem.Enabled = this.checkBox1.Checked;
        }

 public void Init()
        {
            this.txtInOutDis.Text = this.jlktkassiatant_0.InOutDist.ToString();
            this.txtOutBorderWidth.Text = this.jlktkassiatant_0.OutBorderWidth.ToString();
            this.txtTitleSpace.Text = this.jlktkassiatant_0.TitleDist.ToString();
            this.txtR1C1.Text = this.jlktkassiatant_0.Row1Col1Text;
            this.txtR1C2.Text = this.jlktkassiatant_0.Row1Col2Text;
            this.txtR1C3.Text = this.jlktkassiatant_0.Row1Col3Text;
            this.txtR2C1.Text = this.jlktkassiatant_0.Row2Col1Text;
            this.txtR2C3.Text = this.jlktkassiatant_0.Row2Col3Text;
            this.txtR3C1.Text = this.jlktkassiatant_0.Row3Col1Text;
            this.txtR3C2.Text = this.jlktkassiatant_0.Row3Col2Text;
            this.txtR3C3.Text = this.jlktkassiatant_0.Row3Col3Text;
        }

 private void TKConfigPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        internal YTTKAssiatant YTTKAssiatant
        {
            set
            {
                this.jlktkassiatant_0 = value;
            }
        }
    }
}

