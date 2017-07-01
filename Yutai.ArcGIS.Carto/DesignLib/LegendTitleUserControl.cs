using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal partial class LegendTitleUserControl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ILegend ilegend_0 = null;
        private ITextSymbol itextSymbol_0 = null;

        public LegendTitleUserControl()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        private void btnBlod_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.itextSymbol_0.Font;
            font.Bold = true;
            this.itextSymbol_0.Font = font;
            this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.itextSymbol_0.Font;
            font.Italic = true;
            this.itextSymbol_0.Font = font;
            this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            stdole.IFontDisp font = this.itextSymbol_0.Font;
            font.Underline = true;
            this.itextSymbol_0.Font = font;
            this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                stdole.IFontDisp font = this.itextSymbol_0.Font;
                font.Name = (string) this.cboFontName.Items[this.cboFontName.SelectedIndex];
                this.itextSymbol_0.Font = font;
                this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor color = this.itextSymbol_0.Color;
                this.method_3(this.colorEdit1, color);
                this.itextSymbol_0.Color = color;
                this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
            }
        }

        public void InitControl()
        {
            this.bool_0 = false;
            if (this.itextSymbol_0 == null)
            {
                this.itextSymbol_0 = new TextSymbolClass();
            }
            this.method_0(this.colorEdit1, this.itextSymbol_0.Color);
            this.numUpDownSize.Value = (decimal) this.itextSymbol_0.Size;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (this.itextSymbol_0.Font.Name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.bool_0 = true;
        }

        private void LegendTitleUserControl_Load(object sender, EventArgs e)
        {
            this.memoEditTitle.Text = this.ilegend_0.Title;
            this.InitControl();
            this.bool_0 = true;
        }

        private void memoEditTitle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ilegend_0.Title = this.memoEditTitle.Text;
            }
        }

        private void method_0(ColorEdit colorEdit_0, IColor icolor_0)
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
                this.method_1((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_1(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
            int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_2(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |= (uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_2(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void numUpDownSize_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.numUpDownSize.Value <= 0M)
                {
                    this.numUpDownSize.Value = (decimal) this.itextSymbol_0.Size;
                }
                else
                {
                    this.itextSymbol_0.Size = (double) this.numUpDownSize.Value;
                    this.ilegend_0.Format.TitleSymbol = this.itextSymbol_0;
                }
            }
        }

        public ILegend Legend
        {
            set
            {
                this.ilegend_0 = value;
                this.itextSymbol_0 = this.ilegend_0.Format.TitleSymbol;
            }
        }
    }
}