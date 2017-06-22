using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class FeatureSelectionSetCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IFeatureSelection ifeatureSelection_0 = null;
        public IStyleGallery m_pSG = null;

        public FeatureSelectionSetCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.ifeatureSelection_0.SetSelectionSymbol = false;
                    this.ifeatureSelection_0.SelectionColor = null;
                }
                else if (this.radioGroup1.SelectedIndex == 1)
                {
                    this.ifeatureSelection_0.SetSelectionSymbol = true;
                    this.ifeatureSelection_0.SelectionColor = null;
                    this.ifeatureSelection_0.SelectionSymbol = this.btnSymbol.Style as ISymbol;
                }
                else if (this.radioGroup1.SelectedIndex == 2)
                {
                    this.ifeatureSelection_0.SetSelectionSymbol = false;
                    IColor color = new RgbColorClass();
                    this.method_2(this.colorEdit1, color);
                    this.ifeatureSelection_0.SelectionColor = color;
                }
            }
            return true;
        }

        private void btnSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.btnSymbol.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnSymbol.Style = selector.GetSymbol();
                        this.bool_0 = true;
                    }
                }
            }
            catch
            {
            }
        }

 private void FeatureSelectionSetCtrl_Load(object sender, EventArgs e)
        {
            if (!this.ifeatureSelection_0.SetSelectionSymbol)
            {
                if (this.ifeatureSelection_0.SelectionColor == null)
                {
                    this.radioGroup1.SelectedIndex = 0;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 2;
                }
            }
            else
            {
                this.radioGroup1.SelectedIndex = 1;
            }
            if (this.ifeatureSelection_0.SelectionSymbol == null)
            {
                ILineSymbol symbol = new SimpleLineSymbolClass {
                    Width = 2.0,
                    Color = ColorManage.CreatColor(0, 255, 255)
                };
                this.btnSymbol.Style = symbol;
            }
            else
            {
                this.btnSymbol.Style = this.ifeatureSelection_0.SelectionSymbol;
            }
            this.method_3(this.colorEdit1, this.ifeatureSelection_0.SelectionColor);
            this.bool_1 = true;
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
            if (icolor_0 != null)
            {
                icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
                if (!colorEdit_0.Color.IsEmpty)
                {
                    icolor_0.Transparency = colorEdit_0.Color.A;
                    icolor_0.RGB = this.method_1(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
                }
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0 == null)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else if (icolor_0.NullColor)
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
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ifeatureSelection_0 = value as IFeatureSelection;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }
    }
}

