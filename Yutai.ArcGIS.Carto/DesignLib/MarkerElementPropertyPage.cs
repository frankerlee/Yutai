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
    public class MarkerElementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnChangeSymbol;
        private ColorEdit colorEdit1;
        private Container container_0 = null;
        private IMarkerElement imarkerElement_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private string string_0 = "点符号";
        private SymbolItem symbolItem1;
        private SpinEdit txtAngle;
        private SpinEdit txtWidth;

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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtWidth = new SpinEdit();
            this.symbolItem1 = new SymbolItem();
            this.btnChangeSymbol = new SimpleButton();
            this.txtAngle = new SpinEdit();
            this.label3 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x10);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 0;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "颜色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "大小:";
            int[] bits = new int[4];
            this.txtWidth.EditValue = new decimal(bits);
            this.txtWidth.Location = new Point(0x38, 0x38);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtWidth.Size = new Size(80, 0x15);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(160, 0x18);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0xb0, 0x58);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 4;
            this.btnChangeSymbol.Location = new Point(0xb8, 0x80);
            this.btnChangeSymbol.Name = "btnChangeSymbol";
            this.btnChangeSymbol.Size = new Size(0x90, 0x20);
            this.btnChangeSymbol.TabIndex = 5;
            this.btnChangeSymbol.Text = "更改符号...";
            this.btnChangeSymbol.Click += new EventHandler(this.btnChangeSymbol_Click);
            int[] bits2 = new int[4];
            this.txtAngle.EditValue = new decimal(bits2);
            this.txtAngle.Location = new Point(0x38, 0x5c);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtAngle.Size = new Size(80, 0x15);
            this.txtAngle.TabIndex = 7;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 100);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "角度:";
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.symbolItem1);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorEdit1);
            base.Name = "MarkerElementPropertyPage";
            base.Size = new Size(360, 0xc0);
            base.Load += new EventHandler(this.MarkerElementPropertyPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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
            uint num = uint_0 & 0xff0000;
             int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
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

