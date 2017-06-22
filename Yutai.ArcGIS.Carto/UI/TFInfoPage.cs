using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.DesignLib;

namespace Yutai.ArcGIS.Carto.UI
{
    [ToolboxItem(false)]
    internal partial class TFInfoPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;

        public TFInfoPage()
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
                num = double.Parse(this.txtXInterval.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.txtYInterval.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num3 = double.Parse(this.txtStartMultiple.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            if (this.jlktkassiatant_0.TKType == TKType.TKStandard)
            {
                if (this.txtTH.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入图幅编号！");
                    return false;
                }
                if (!THTools.ValidateTFNo(this.txtTH.Text.Trim()))
                {
                    MessageBox.Show("图幅编号不正确！");
                    return false;
                }
            }
            this.jlktkassiatant_0.XInterval = num;
            this.jlktkassiatant_0.YInterval = num2;
            this.jlktkassiatant_0.StartCoodinateMultiple = num3;
            this.jlktkassiatant_0.MapTM = this.txtTM.Text;
            this.jlktkassiatant_0.MapTH = this.txtTH.Text;
            this.jlktkassiatant_0.StripType = this.rdo3.Checked ? StripType.STThreeDeg : StripType.STSixDeg;
            return true;
        }

 public void Init()
        {
            if (this.jlktkassiatant_0.TKType == TKType.TKStandard)
            {
                if (!this.groupSR.Visible)
                {
                    this.groupSR.Visible = true;
                    this.panel1.Location = new Point(11, 200);
                }
            }
            else if (this.jlktkassiatant_0.TKType == TKType.TKRand)
            {
                this.groupSR.Visible = false;
                this.panel1.Location = new Point(11, 74);
            }
            this.txtTM.Text = this.jlktkassiatant_0.MapTM;
            this.txtTH.Text = this.jlktkassiatant_0.MapTH;
            this.txtXInterval.Text = this.jlktkassiatant_0.XInterval.ToString();
            this.txtYInterval.Text = this.jlktkassiatant_0.YInterval.ToString();
            this.txtStartMultiple.Text = this.jlktkassiatant_0.StartCoodinateMultiple.ToString();
            if (this.jlktkassiatant_0.StripType == StripType.STThreeDeg)
            {
                this.rdo3.Checked = true;
                this.rdo6.Checked = false;
            }
            else
            {
                this.rdo6.Checked = true;
                this.rdo3.Checked = false;
            }
            if (this.jlktkassiatant_0.SpheroidType == SpheroidType.Xian1980)
            {
                this.cboDataum.SelectedIndex = 1;
            }
            else
            {
                this.cboDataum.SelectedIndex = 0;
            }
        }

 private void TFInfoPage_Load(object sender, EventArgs e)
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

