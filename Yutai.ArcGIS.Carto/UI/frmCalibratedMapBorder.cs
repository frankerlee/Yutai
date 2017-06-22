using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmCalibratedMapBorder : Form
    {
        private bool bool_0 = true;
        private ICalibratedMapGridBorder icalibratedMapGridBorder_0 = null;
        private IContainer icontainer_0 = null;

        public frmCalibratedMapBorder()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IColor foregroundColor = this.icalibratedMapGridBorder_0.ForegroundColor;
            this.method_2(this.colorForegroundColor, foregroundColor);
            this.icalibratedMapGridBorder_0.ForegroundColor = foregroundColor;
            foregroundColor = this.icalibratedMapGridBorder_0.BackgroundColor;
            this.method_2(this.colorEditBackgroundColor, foregroundColor);
            this.icalibratedMapGridBorder_0.BackgroundColor = foregroundColor;
            if (this.txtInterval.Value > 0M)
            {
                this.icalibratedMapGridBorder_0.Interval = (double) this.txtInterval.Value;
            }
            this.icalibratedMapGridBorder_0.BorderWidth = (double) this.txtBorderWidth.Value;
            this.icalibratedMapGridBorder_0.Alternating = this.chkAlternating.Checked;
            base.DialogResult = DialogResult.OK;
        }

        private void colorEditBackgroundColor_EditValueChanged(object sender, EventArgs e)
        {
        }

 private void frmCalibratedMapBorder_Load(object sender, EventArgs e)
        {
            this.bool_0 = false;
            this.txtBorderWidth.Value = (decimal) this.icalibratedMapGridBorder_0.BorderWidth;
            this.txtInterval.Value = (decimal) this.icalibratedMapGridBorder_0.Interval;
            this.chkAlternating.Checked = this.icalibratedMapGridBorder_0.Alternating;
            this.method_3(this.colorEditBackgroundColor, this.icalibratedMapGridBorder_0.BackgroundColor);
            this.method_3(this.colorForegroundColor, this.icalibratedMapGridBorder_0.ForegroundColor);
            this.bool_0 = true;
        }

 private void method_0(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
             int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_1(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_1(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
               this.method_0((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        public ICalibratedMapGridBorder CalibratedMapGridBorder
        {
            get
            {
                return this.icalibratedMapGridBorder_0;
            }
            set
            {
                this.icalibratedMapGridBorder_0 = value;
            }
        }
    }
}

