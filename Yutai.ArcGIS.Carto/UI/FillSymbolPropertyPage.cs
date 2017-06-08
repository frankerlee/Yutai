using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    public class FillSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnChangeSymbol;
        private ColorEdit colorEdit1;
        private ColorEdit colorEdit2;
        private Container container_0 = null;
        private IFillShapeElement ifillShapeElement_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private string string_0 = "符号";
        private SymbolItem symbolItem1;
        private SpinEdit txtWidth;

        public event OnValueChangeEventHandler OnValueChange;

        public FillSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.ifillShapeElement_0.Symbol = this.symbolItem1.Symbol as IFillSymbol;
                this.bool_1 = false;
            }
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IFillSymbol pSym = this.symbolItem1.Symbol as IFillSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(pSym);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as IFillSymbol;
                        this.symbolItem1.Symbol = pSym;
                        this.bool_0 = false;
                        this.method_1(pSym);
                        this.bool_0 = true;
                        this.bool_1 = true;
                        this.method_6(e);
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
                IFillSymbol symbol = this.symbolItem1.Symbol as IFillSymbol;
                IColor color = symbol.Color;
                this.method_5(this.colorEdit1, color);
                symbol.Color = color;
                this.bool_1 = true;
                this.method_6(e);
            }
        }

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFillSymbol symbol = this.symbolItem1.Symbol as IFillSymbol;
                ILineSymbol outline = symbol.Outline;
                if (outline != null)
                {
                    IColor color = outline.Color;
                    this.method_5(this.colorEdit2, color);
                    outline.Color = color;
                    symbol.Outline = outline;
                    this.bool_1 = true;
                    this.method_6(e);
                }
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

        private void FillSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtWidth = new SpinEdit();
            this.symbolItem1 = new SymbolItem();
            this.btnChangeSymbol = new SimpleButton();
            this.label3 = new Label();
            this.colorEdit2 = new ColorEdit();
            this.colorEdit1.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            this.colorEdit2.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(80, 0x10);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "填充色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x54);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x48, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "轮廓线宽度:";
            int[] bits = new int[4];
            this.txtWidth.EditValue = new decimal(bits);
            this.txtWidth.Location = new Point(80, 80);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtWidth.Properties.UseCtrlIncrement = false;
            this.txtWidth.Size = new Size(0x40, 0x17);
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
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 50);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x48, 0x11);
            this.label3.TabIndex = 7;
            this.label3.Text = "轮廓线颜色:";
            this.colorEdit2.EditValue = Color.Empty;
            this.colorEdit2.Location = new Point(80, 0x30);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit2.Size = new Size(0x30, 0x17);
            this.colorEdit2.TabIndex = 6;
            this.colorEdit2.EditValueChanged += new EventHandler(this.colorEdit2_EditValueChanged);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.colorEdit2);
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.symbolItem1);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorEdit1);
            base.Name = "FillSymbolPropertyPage";
            base.Size = new Size(360, 0xb8);
            base.Load += new EventHandler(this.FillSymbolPropertyPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            this.colorEdit2.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (this.ifillShapeElement_0 != null)
            {
                IFillSymbol symbol = this.ifillShapeElement_0.Symbol;
                this.symbolItem1.Symbol = symbol;
                if (symbol != null)
                {
                    this.colorEdit1.Enabled = true;
                    this.txtWidth.Enabled = true;
                    this.method_2(this.colorEdit1, symbol.Color);
                    ILineSymbol outline = symbol.Outline;
                    if (outline != null)
                    {
                        this.colorEdit2.Enabled = true;
                        this.txtWidth.Enabled = true;
                        this.method_2(this.colorEdit2, outline.Color);
                        this.txtWidth.Value = (decimal) outline.Width;
                    }
                    else
                    {
                        this.colorEdit2.Enabled = false;
                        this.txtWidth.Enabled = false;
                    }
                }
                else
                {
                    this.colorEdit1.Enabled = false;
                    this.colorEdit2.Enabled = false;
                    this.txtWidth.Enabled = false;
                }
            }
        }

        private void method_1(IFillSymbol ifillSymbol_0)
        {
            if (ifillSymbol_0 != null)
            {
                this.colorEdit1.Enabled = true;
                this.txtWidth.Enabled = true;
                this.method_2(this.colorEdit1, ifillSymbol_0.Color);
                ILineSymbol outline = ifillSymbol_0.Outline;
                if (outline != null)
                {
                    this.colorEdit2.Enabled = true;
                    this.txtWidth.Enabled = true;
                    this.method_2(this.colorEdit2, outline.Color);
                    this.txtWidth.Value = (decimal) outline.Width;
                }
                else
                {
                    this.colorEdit2.Enabled = false;
                    this.txtWidth.Enabled = false;
                }
            }
            else
            {
                this.colorEdit1.Enabled = false;
                this.colorEdit2.Enabled = false;
                this.txtWidth.Enabled = false;
            }
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0 != null)
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
                    this.method_3((uint) rGB, out num2, out num3, out num4);
                    colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
                }
            }
        }

        private void method_3(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
             int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_4(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_5(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0 != null)
            {
                icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
                if (!colorEdit_0.Color.IsEmpty)
                {
                    icolor_0.Transparency = colorEdit_0.Color.A;
                    icolor_0.RGB = this.method_4(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
                }
            }
        }

        private void method_6(EventArgs eventArgs_0)
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
            this.ifillShapeElement_0 = object_0 as IFillShapeElement;
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFillSymbol symbol = this.symbolItem1.Symbol as IFillSymbol;
                ILineSymbol outline = symbol.Outline;
                if (outline != null)
                {
                    if (this.txtWidth.Value <= 0M)
                    {
                        this.txtWidth.Value = (decimal) outline.Width;
                    }
                    else
                    {
                        outline.Width = (double) this.txtWidth.Value;
                        symbol.Outline = outline;
                        this.bool_1 = true;
                        this.method_6(e);
                    }
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

