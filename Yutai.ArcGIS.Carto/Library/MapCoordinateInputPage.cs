using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class MapCoordinateInputPage : UserControl
    {
        private bool bool_0 = false;
        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private IContainer icontainer_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;

        public MapCoordinateInputPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            try
            {
                this.mapTemplateApplyHelp_0.CoordinateType = this.radioButton1.Checked ? 0 : 1;
                this.mapTemplateApplyHelp_0.X = double.Parse(this.txtX.Text);
                this.mapTemplateApplyHelp_0.Y = double.Parse(this.txtY.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("请检查输入是否正确!");
                return false;
            }
        }

 private void MapCoordinateInputPage_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.radioButton2.Enabled = false;
                this.txtX.Text = this.double_0.ToString();
                this.txtY.Text = this.double_1.ToString();
            }
        }

        public void SetMouseClick(double double_2, double double_3)
        {
            this.double_0 = double_2;
            this.double_1 = double_3;
            this.bool_0 = true;
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }
    }
}

