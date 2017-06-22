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
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class MarkerElementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMarkerElement imarkerElement_0 = null;
        private string string_0 = "点符号";

        public event OnValueChangeEventHandler OnValueChange;

        public MarkerElementPropertyPage()
        {
            this.InitializeComponent();
            this.symbolItem1.HasDrawLine = false;
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.imarkerElement_0.Symbol = this.symbolItem1.Symbol as IMarkerSymbol;
                this.bool_1 = false;
            }
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IMarkerSymbol pSym = this.symbolItem1.Symbol as IMarkerSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(pSym);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as IMarkerSymbol;
                        this.symbolItem1.Symbol = pSym;
                        this.bool_0 = false;
                        this.method_1(this.colorEdit1, pSym.Color);
                        this.txtWidth.Value = (decimal) pSym.Size;
                        this.txtAngle.Value = (decimal) pSym.Angle;
                        this.bool_0 = true;
                        this.bool_1 = true;
                        this.method_5(e);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void Cancel()
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IMarkerSymbol symbol = this.symbolItem1.Symbol as IMarkerSymbol;
                IColor color = symbol.Color;
                this.method_4(this.colorEdit1, color);
                symbol.Color = color;
                this.bool_1 = true;
                this.method_5(e);
            }
        }

 private void MarkerElementPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void method_0()
        {
            if (this.imarkerElement_0 != null)
            {
                IMarkerSymbol symbol = this.imarkerElement_0.Symbol;
                this.symbolItem1.Symbol = symbol;
                if (symbol != null)
                {
                    this.colorEdit1.Enabled = true;
                    this.txtWidth.Properties.ReadOnly = false;
                    this.txtAngle.Properties.ReadOnly = false;
                    this.method_1(this.colorEdit1, symbol.Color);
                    this.txtWidth.Value = (decimal) symbol.Size;
                    this.txtAngle.Value = (decimal) symbol.Angle;
                }
                else
                {
                    this.colorEdit1.Enabled = false;
                    this.txtWidth.Properties.ReadOnly = true;
                    this.txtAngle.Properties.ReadOnly = true;
                }
            }
        }

        private void method_1(ColorEdit colorEdit_0, IColor icolor_0)
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
                this.method_2((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_2(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
             int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_3(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_4(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_3(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_5(EventArgs eventArgs_0)
        {
            if (this.OnValueChange != null)
            {
                this.symbolItem1.Invalidate();
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imarkerElement_0 = object_0 as IMarkerElement;
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IMarkerSymbol symbol = this.symbolItem1.Symbol as IMarkerSymbol;
                if ((this.txtAngle.Value <= -360M) || (this.txtAngle.Value >= 360M))
                {
                    this.txtAngle.Value = (decimal) symbol.Angle;
                }
                else
                {
                    symbol.Angle = (double) this.txtWidth.Value;
                    this.bool_1 = true;
                    this.method_5(e);
                }
            }
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IMarkerSymbol symbol = this.symbolItem1.Symbol as IMarkerSymbol;
                if (this.txtWidth.Value <= 0M)
                {
                    this.txtWidth.Value = (decimal) symbol.Size;
                }
                else
                {
                    symbol.Size = (double) this.txtWidth.Value;
                    this.bool_1 = true;
                    this.method_5(e);
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

