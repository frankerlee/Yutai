using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class BorderSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnChangeSymbol;
        private ColorEdit colorEdit1;
        private Container components = null;
        private Label label1;
        private Label label2;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ISymbolBorder m_pOldSymbolBorder = null;
        private ISymbolBorder m_pSymbolBorder = null;
        private string m_Title = "符号";
        private SymbolItem symbolItem1;
        private SpinEdit txtWidth;
        private IAppContext _context;

        public event OnValueChangeEventHandler OnValueChange;

        public BorderSymbolPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
        }

        public BorderSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_pOldSymbolBorder.LineSymbol = this.m_pSymbolBorder.LineSymbol;
                this.m_IsPageDirty = false;
            }
        }

        private void BorderSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(_context.StyleGallery);
                selector.SetSymbol(lineSymbol);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    lineSymbol = selector.GetSymbol() as ILineSymbol;
                    this.m_pSymbolBorder.LineSymbol = lineSymbol;
                    this.m_CanDo = false;
                    this.SetColorEdit(this.colorEdit1, lineSymbol.Color);
                    this.txtWidth.Value = (decimal) lineSymbol.Width;
                    this.m_CanDo = true;
                    this.m_IsPageDirty = true;
                    this.refresh(e);
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
            if (this.m_CanDo)
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                IColor pColor = lineSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                lineSymbol.Color = pColor;
                this.m_pSymbolBorder.LineSymbol = lineSymbol;
                this.m_IsPageDirty = true;
                this.refresh(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
            r = (int) num;
        }

        public void Hide()
        {
        }

        private void Init()
        {
            this.symbolItem1.Symbol = this.m_pSymbolBorder;
            if (this.m_pSymbolBorder != null)
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                if (lineSymbol != null)
                {
                    this.colorEdit1.Enabled = true;
                    this.txtWidth.Properties.ReadOnly = false;
                    this.SetColorEdit(this.colorEdit1, lineSymbol.Color);
                    this.txtWidth.Value = (decimal) lineSymbol.Width;
                }
                else
                {
                    this.colorEdit1.Enabled = false;
                    this.txtWidth.Properties.ReadOnly = true;
                }
            }
        }

        private void InitializeComponent()
        {
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtWidth = new SpinEdit();
            this.symbolItem1 = new SymbolItem();
            this.btnChangeSymbol = new SimpleButton();
            this.colorEdit1.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x38, 0x10);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 0;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "颜色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "宽度:";
            int[] bits = new int[4];
            this.txtWidth.EditValue = new decimal(bits);
            this.txtWidth.Location = new Point(0x38, 0x38);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtWidth.Size = new Size(80, 0x17);
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
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.symbolItem1);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorEdit1);
            base.Name = "BorderSymbolPropertyPage";
            base.Size = new Size(360, 0xc0);
            base.Load += new EventHandler(this.BorderSymbolPropertyPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void refresh(EventArgs e)
        {
            if (this.OnValueChange != null)
            {
                this.symbolItem1.Invalidate();
                this.OnValueChange();
            }
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        public void SetObjects(object @object)
        {
            this.m_pOldSymbolBorder = @object as ISymbolBorder;
            if (this.m_pOldSymbolBorder != null)
            {
                this.m_pSymbolBorder = (this.m_pOldSymbolBorder as IClone).Clone() as ISymbolBorder;
            }
            else
            {
                this.m_pSymbolBorder = null;
            }
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ILineSymbol lineSymbol = this.m_pSymbolBorder.LineSymbol;
                if (this.txtWidth.Value <= 0M)
                {
                    this.txtWidth.Value = (decimal) lineSymbol.Width;
                }
                else
                {
                    lineSymbol.Width = (double) this.txtWidth.Value;
                    this.m_pSymbolBorder.LineSymbol = lineSymbol;
                    this.m_IsPageDirty = true;
                    this.refresh(e);
                }
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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

        public ISymbolBorder SymbolBorder
        {
            set
            {
                this.m_pOldSymbolBorder = value;
                if (this.m_pOldSymbolBorder != null)
                {
                    this.m_pSymbolBorder = (this.m_pOldSymbolBorder as IClone).Clone() as ISymbolBorder;
                }
                else
                {
                    this.m_pSymbolBorder = null;
                }
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

