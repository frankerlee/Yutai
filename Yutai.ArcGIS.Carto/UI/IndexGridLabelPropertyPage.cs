using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class IndexGridLabelPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IGridLabel igridLabel_0 = null;
        private IIndexGrid iindexGrid_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = new TextSymbolClass();

        public IndexGridLabelPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(this.istyleGallery_0);
                selector.SetSymbol(this.itextSymbol_0);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    IGridLabel labelFormat = this.iindexGrid_0.LabelFormat;
                    labelFormat.Font = this.itextSymbol_0.Font;
                    labelFormat.Color = this.itextSymbol_0.Color;
                    this.iindexGrid_0.LabelFormat = labelFormat;
                }
            }
            catch
            {
            }
        }

        private void cboLabelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                switch (this.cboLabelType.SelectedIndex)
                {
                    case 0:
                        this.igridLabel_0 = new ButtonTabStyleClass();
                        break;

                    case 1:
                        this.igridLabel_0 = new BackgroundTabStyleClass();
                        break;

                    case 2:
                        this.igridLabel_0 = new ContinuousTabStyleClass();
                        break;

                    case 3:
                        this.igridLabel_0 = new RoundedTabStyleClass();
                        break;
                }
                this.iindexGrid_0.LabelFormat = this.igridLabel_0;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor color = this.igridLabel_0.Color;
                this.method_2(this.colorEdit1, color);
                this.igridLabel_0.Color = color;
            }
        }

 private void IndexGridLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.bool_1 = true;
        }

        public void Init()
        {
            this.bool_0 = false;
            if (this.igridLabel_0 != null)
            {
                string displayName = this.igridLabel_0.DisplayName;
                this.cboLabelType.Text = displayName;
                this.method_3(this.colorEdit1, this.igridLabel_0.Color);
                this.itextSymbol_0.Font = this.igridLabel_0.Font;
                this.itextSymbol_0.Color = this.igridLabel_0.Color;
                this.itextSymbol_0.Text = "ABC 123...";
                this.btnFont.Style = this.itextSymbol_0;
            }
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

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.iindexGrid_0 = value as IIndexGrid;
                this.igridLabel_0 = this.iindexGrid_0.LabelFormat;
                if (this.bool_1)
                {
                    this.Init();
                }
            }
        }
    }
}

