using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class FractionTextSymbolPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IActiveView iactiveView_0 = null;
        private IContainer icontainer_0 = null;
        private ILineSymbol ilineSymbol_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private ITextSymbol itextSymbol_1 = null;
        public IFractionTextElement m_FractionTextSymbol = null;
        public IStyleGallery m_pSG;
        private string string_0 = "分式文字";

        public event OnValueChangeEventHandler OnValueChange;

        public FractionTextSymbolPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.itextSymbol_0 = this.btnNumeratorTextSymbol.Style as ITextSymbol;
                this.ilineSymbol_0 = this.btnLineSymbol.Style as ILineSymbol;
                this.itextSymbol_1 = this.btnDenominatorTextSymbol.Style as ITextSymbol;
                this.m_FractionTextSymbol.NumeratorTextSymbol = (this.itextSymbol_0 as IClone).Clone() as ITextSymbol;
                this.m_FractionTextSymbol.DenominatorTextSymbol = (this.itextSymbol_1 as IClone).Clone() as ITextSymbol;
                this.m_FractionTextSymbol.LineSymbol = (this.ilineSymbol_0 as IClone).Clone() as ILineSymbol;
                this.m_FractionTextSymbol.NumeratorText = this.txtNumeratorText.Text;
                this.m_FractionTextSymbol.DenominatorText = this.txtDenominatorText.Text;
                this.method_1(this.iactiveView_0);
            }
        }

        private void btnDenominatorTextSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_FractionTextSymbol.DenominatorTextSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_FractionTextSymbol.DenominatorTextSymbol).Clone();
                }
                else
                {
                    pSym = new TextSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_FractionTextSymbol.DenominatorTextSymbol = selector.GetSymbol() as ITextSymbol;
                    this.btnDenominatorTextSymbol.Style = this.m_FractionTextSymbol.DenominatorTextSymbol;
                    this.btnDenominatorTextSymbol.Invalidate();
                    this.method_0(e);
                }
            }
            catch
            {
            }
        }

        private void btnLineSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_FractionTextSymbol.LineSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_FractionTextSymbol.LineSymbol).Clone();
                }
                else
                {
                    pSym = new SimpleLineSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_FractionTextSymbol.LineSymbol = selector.GetSymbol() as ILineSymbol;
                    this.btnLineSymbol.Style = this.m_FractionTextSymbol.LineSymbol;
                    this.btnLineSymbol.Invalidate();
                    this.method_0(e);
                }
            }
            catch
            {
            }
        }

        private void btnNumeratorTextSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_FractionTextSymbol.NumeratorTextSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_FractionTextSymbol.NumeratorTextSymbol).Clone();
                }
                else
                {
                    pSym = new TextSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_FractionTextSymbol.NumeratorTextSymbol = selector.GetSymbol() as ITextSymbol;
                    this.btnNumeratorTextSymbol.Style = this.m_FractionTextSymbol.NumeratorTextSymbol;
                    this.btnNumeratorTextSymbol.Invalidate();
                    this.method_0(e);
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        private void FractionTextSymbolPage_Load(object sender, EventArgs e)
        {
            if (this.m_FractionTextSymbol != null)
            {
                this.btnNumeratorTextSymbol.Style = this.itextSymbol_0;
                this.btnLineSymbol.Style = this.ilineSymbol_0;
                this.btnDenominatorTextSymbol.Style = this.itextSymbol_1;
                this.txtNumeratorText.Text = this.m_FractionTextSymbol.NumeratorText;
                this.txtDenominatorText.Text = this.m_FractionTextSymbol.DenominatorText;
            }
            else
            {
                this.btnNumeratorTextSymbol.Enabled = false;
                this.btnLineSymbol.Enabled = false;
                this.btnDenominatorTextSymbol.Enabled = false;
            }
            this.bool_0 = true;
        }

        private void method_0(EventArgs eventArgs_0)
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1(IActiveView iactiveView_1)
        {
            if (iactiveView_1 != null)
            {
                double num7;
                double num8;
                double num9;
                double num10;
                iactiveView_1.ScreenDisplay.StartDrawing(0, 0);
                IPoint geometry = new PointClass
                {
                    X = 3.0,
                    Y = 3.0
                };
                IElement fractionTextSymbol = this.m_FractionTextSymbol as IElement;
                IEnvelope envelope = fractionTextSymbol.Geometry.Envelope;
                IPolygon boundary = new PolygonClass();
                ((fractionTextSymbol as IFractionTextElement).NumeratorTextSymbol as ISymbol).QueryBoundary(
                    iactiveView_1.ScreenDisplay.hDC, iactiveView_1.ScreenDisplay.DisplayTransformation, geometry,
                    boundary);
                double width = boundary.Envelope.Width;
                double height = boundary.Envelope.Height;
                ((fractionTextSymbol as IFractionTextElement).DenominatorTextSymbol as ISymbol).QueryBoundary(
                    iactiveView_1.ScreenDisplay.hDC, iactiveView_1.ScreenDisplay.DisplayTransformation, geometry,
                    boundary);
                double num3 = boundary.Envelope.Width;
                double num4 = boundary.Envelope.Height;
                double num5 = (width > num3) ? width : num3;
                double num6 = height + num4;
                iactiveView_1.ScreenDisplay.FinishDrawing();
                envelope.QueryCoords(out num7, out num8, out num9, out num10);
                num7 = (num7 + num9)/2.0;
                num8 = (num8 + num10)/2.0;
                num9 = num7 + (1.1*num5);
                num10 = num8 + (1.1*num6);
                geometry.X = num7;
                geometry.Y = num8;
                envelope.PutCoords(num7, num8, num9, num10);
                envelope.CenterAt(geometry);
                fractionTextSymbol.Geometry = envelope;
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.m_FractionTextSymbol = object_0 as IFractionTextElement;
            this.itextSymbol_0 = (this.m_FractionTextSymbol.NumeratorTextSymbol as IClone).Clone() as ITextSymbol;
            this.itextSymbol_1 = (this.m_FractionTextSymbol.DenominatorTextSymbol as IClone).Clone() as ITextSymbol;
            this.ilineSymbol_0 = (this.m_FractionTextSymbol.LineSymbol as IClone).Clone() as ILineSymbol;
        }

        private void txtDenominatorText_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0(e);
            }
        }

        private void txtNumeratorText_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0(e);
            }
        }

        public IActiveView ActiveView
        {
            set { this.iactiveView_0 = value; }
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