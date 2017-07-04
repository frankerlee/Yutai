using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class MapCoordinateInputPage : UserControl
    {
        private bool _isFreePoint = false;
        private double _x = 0.0;
        private double _y = 0.0;
        private IContainer icontainer_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp = null;

        public MapCoordinateInputPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            try
            {
                this.mapTemplateApplyHelp.CoordinateType = this.radioButton1.Checked ? 0 : 1;
                this.mapTemplateApplyHelp.X = double.Parse(this.txtX.Text);
                this.mapTemplateApplyHelp.Y = double.Parse(this.txtY.Text);
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
            if (this._isFreePoint)
            {
                this.radioButton2.Enabled = false;
                this.txtX.Text = this._x.ToString();
                this.txtY.Text = this._y.ToString();
            }
        }

        public void SetMouseClick(double double_2, double double_3)
        {
            this._x = double_2;
            this._y = double_3;
            this._isFreePoint = true;
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get { return this.mapTemplateApplyHelp; }
            set { this.mapTemplateApplyHelp = value; }
        }
    }
}