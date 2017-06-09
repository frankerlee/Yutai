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
    public class FractionTextSymbolPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private NewSymbolButton btnDenominatorTextSymbol;
        private NewSymbolButton btnLineSymbol;
        private NewSymbolButton btnNumeratorTextSymbol;
        private IActiveView iactiveView_0 = null;
        private IContainer icontainer_0 = null;
        private ILineSymbol ilineSymbol_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private ITextSymbol itextSymbol_1 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        public IFractionTextElement m_FractionTextSymbol = null;
        public IStyleGallery m_pSG;
        private string string_0 = "分式文字";
        private TextBox txtDenominatorText;
        private TextBox txtNumeratorText;

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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.btnNumeratorTextSymbol = new NewSymbolButton();
            this.btnLineSymbol = new NewSymbolButton();
            this.label2 = new Label();
            this.btnDenominatorTextSymbol = new NewSymbolButton();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtNumeratorText = new TextBox();
            this.txtDenominatorText = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0x67);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分子符号样式";
            this.btnNumeratorTextSymbol.Location = new System.Drawing.Point(0x6a, 0x55);
            this.btnNumeratorTextSymbol.Name = "btnNumeratorTextSymbol";
            this.btnNumeratorTextSymbol.Size = new Size(0x73, 0x31);
            this.btnNumeratorTextSymbol.Style = null;
            this.btnNumeratorTextSymbol.TabIndex = 1;
            this.btnNumeratorTextSymbol.Click += new EventHandler(this.btnNumeratorTextSymbol_Click);
            this.btnLineSymbol.Location = new System.Drawing.Point(0x6a, 0x90);
            this.btnLineSymbol.Name = "btnLineSymbol";
            this.btnLineSymbol.Size = new Size(0x73, 0x22);
            this.btnLineSymbol.Style = null;
            this.btnLineSymbol.TabIndex = 3;
            this.btnLineSymbol.Click += new EventHandler(this.btnLineSymbol_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 0x9b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "分式线符号样式";
            this.btnDenominatorTextSymbol.Location = new System.Drawing.Point(0x6a, 190);
            this.btnDenominatorTextSymbol.Name = "btnDenominatorTextSymbol";
            this.btnDenominatorTextSymbol.Size = new Size(0x73, 50);
            this.btnDenominatorTextSymbol.Style = null;
            this.btnDenominatorTextSymbol.TabIndex = 5;
            this.btnDenominatorTextSymbol.Click += new EventHandler(this.btnDenominatorTextSymbol_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 0xd1);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "分母符号样式";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 0x16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "分子文本";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 0x2f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "分母文本";
            this.txtNumeratorText.Location = new System.Drawing.Point(0x5f, 13);
            this.txtNumeratorText.Name = "txtNumeratorText";
            this.txtNumeratorText.Size = new Size(0xd7, 0x15);
            this.txtNumeratorText.TabIndex = 8;
            this.txtNumeratorText.TextChanged += new EventHandler(this.txtNumeratorText_TextChanged);
            this.txtDenominatorText.Location = new System.Drawing.Point(0x5f, 0x2c);
            this.txtDenominatorText.Name = "txtDenominatorText";
            this.txtDenominatorText.Size = new Size(0xd7, 0x15);
            this.txtDenominatorText.TabIndex = 9;
            this.txtDenominatorText.TextChanged += new EventHandler(this.txtDenominatorText_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtDenominatorText);
            base.Controls.Add(this.txtNumeratorText);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnDenominatorTextSymbol);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnLineSymbol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnNumeratorTextSymbol);
            base.Controls.Add(this.label1);
            base.Name = "FractionTextSymbolPage";
            base.Size = new Size(0x16a, 0x114);
            base.Load += new EventHandler(this.FractionTextSymbolPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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
                IPoint geometry = new PointClass {
                    X = 3.0,
                    Y = 3.0
                };
                IElement fractionTextSymbol = this.m_FractionTextSymbol as IElement;
                IEnvelope envelope = fractionTextSymbol.Geometry.Envelope;
                IPolygon boundary = new PolygonClass();
                ((fractionTextSymbol as IFractionTextElement).NumeratorTextSymbol as ISymbol).QueryBoundary(iactiveView_1.ScreenDisplay.hDC, iactiveView_1.ScreenDisplay.DisplayTransformation, geometry, boundary);
                double width = boundary.Envelope.Width;
                double height = boundary.Envelope.Height;
                ((fractionTextSymbol as IFractionTextElement).DenominatorTextSymbol as ISymbol).QueryBoundary(iactiveView_1.ScreenDisplay.hDC, iactiveView_1.ScreenDisplay.DisplayTransformation, geometry, boundary);
                double num3 = boundary.Envelope.Width;
                double num4 = boundary.Envelope.Height;
                double num5 = (width > num3) ? width : num3;
                double num6 = height + num4;
                iactiveView_1.ScreenDisplay.FinishDrawing();
                envelope.QueryCoords(out num7, out num8, out num9, out num10);
                num7 = (num7 + num9) / 2.0;
                num8 = (num8 + num10) / 2.0;
                num9 = num7 + (1.1 * num5);
                num10 = num8 + (1.1 * num6);
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
            set
            {
                this.iactiveView_0 = value;
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

