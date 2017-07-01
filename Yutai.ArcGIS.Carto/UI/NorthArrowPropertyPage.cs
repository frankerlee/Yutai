using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class NorthArrowPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private INorthArrow inorthArrow_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private string string_0 = "指北针";

        public event OnValueChangeEventHandler OnValueChange;

        public NorthArrowPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.imapSurroundFrame_0.MapSurround = (this.inorthArrow_0 as IClone).Clone() as IMapSurround;
                this.bool_1 = false;
            }
        }

        private void btnNorthArrorSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.inorthArrow_0);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.inorthArrow_0 = selector.GetSymbol() as INorthArrow;
                        this.bool_0 = false;
                        this.method_1();
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnNorthMarkerSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol((this.inorthArrow_0 as IMarkerNorthArrow).MarkerSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        (this.inorthArrow_0 as IMarkerNorthArrow).MarkerSymbol = selector.GetSymbol() as IMarkerSymbol;
                        this.bool_0 = false;
                        this.method_1();
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
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
                IColor color = this.inorthArrow_0.Color;
                this.method_2(this.colorEdit1, color);
                this.inorthArrow_0.Color = color;
                this.method_0();
            }
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.symbolItem1.Invalidate();
        }

        private void method_1()
        {
            if (this.inorthArrow_0 != null)
            {
                this.symbolItem1.Symbol = this.inorthArrow_0;
                this.txtAngle.Text = this.inorthArrow_0.Angle.ToString();
                this.txtCalibrationAngle.Text = this.inorthArrow_0.CalibrationAngle.ToString();
                this.txtSize.Text = this.inorthArrow_0.Size.ToString();
                this.method_3(this.colorEdit1, this.inorthArrow_0.Color);
            }
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = ColorManage.EsriRGB(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
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
                ColorManage.GetEsriRGB((uint) icolor_0.RGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void NorthArrowPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if (this.imapSurroundFrame_0 != null)
            {
                this.inorthArrow_0 = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as INorthArrow;
            }
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtCalibrationAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.inorthArrow_0.CalibrationAngle = double.Parse(this.txtCalibrationAngle.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.inorthArrow_0.Size = double.Parse(this.txtSize.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}