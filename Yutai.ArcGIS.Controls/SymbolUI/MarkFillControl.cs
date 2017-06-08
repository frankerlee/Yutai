using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class MarkFillControl : UserControl
    {
        private NewSymbolButton btnFillMarker;
        private NewSymbolButton btnOutline;
        private ColorEdit colorEdit1;
        private Container components = null;
        private Label label1;
        private Label label4;
        private Label label5;
        private bool m_CanDo = true;
        public IMarkerFillSymbol m_MarkerFillSymbol;
        public IStyleGallery m_pSG;
        private RadioGroup radioGroupFillStyle;

        public event ValueChangedHandler ValueChanged;

        public MarkFillControl()
        {
            this.InitializeComponent();
        }

        private void btnFillMarker_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_MarkerFillSymbol.MarkerSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_MarkerFillSymbol.MarkerSymbol).Clone();
                }
                else
                {
                    pSym = new SimpleMarkerSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_MarkerFillSymbol.MarkerSymbol = (IMarkerSymbol) selector.GetSymbol();
                    this.btnFillMarker.Style = this.m_MarkerFillSymbol.MarkerSymbol;
                    this.btnFillMarker.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void btnOutline_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_MarkerFillSymbol.Outline != null)
                {
                    selector.SetSymbol((ISymbol) this.m_MarkerFillSymbol.Outline);
                }
                else
                {
                    selector.SetSymbol(new SimpleLineSymbolClass());
                }
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_MarkerFillSymbol.Outline = (ILineSymbol) selector.GetSymbol();
                    this.btnOutline.Style = this.m_MarkerFillSymbol.Outline;
                    this.btnOutline.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_MarkerFillSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_MarkerFillSymbol.Color = pColor;
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

        private void InitControl()
        {
            this.m_CanDo = false;
            if (this.m_MarkerFillSymbol.Style == esriMarkerFillStyle.esriMFSGrid)
            {
                this.radioGroupFillStyle.SelectedIndex = 0;
            }
            else
            {
                this.radioGroupFillStyle.SelectedIndex = 1;
            }
            this.SetColorEdit(this.colorEdit1, this.m_MarkerFillSymbol.Color);
            this.btnFillMarker.Style = this.m_MarkerFillSymbol.MarkerSymbol;
            this.btnOutline.Style = this.m_MarkerFillSymbol.Outline;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.btnFillMarker = new NewSymbolButton();
            this.btnOutline = new NewSymbolButton();
            this.radioGroupFillStyle = new RadioGroup();
            this.label5 = new Label();
            this.label4 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.radioGroupFillStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x40, 0x18);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 6;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 5;
            this.label1.Text = "颜色";
            this.btnFillMarker.Location = new Point(0xd0, 0x10);
            this.btnFillMarker.Name = "btnFillMarker";
            this.btnFillMarker.Size = new Size(0x60, 40);
            this.btnFillMarker.Style = null;
            this.btnFillMarker.TabIndex = 11;
            this.btnFillMarker.Click += new EventHandler(this.btnFillMarker_Click);
            this.btnOutline.Location = new Point(0xd0, 0x48);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(0x60, 40);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 12;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            this.radioGroupFillStyle.Location = new Point(0x18, 0x40);
            this.radioGroupFillStyle.Name = "radioGroupFillStyle";
            this.radioGroupFillStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupFillStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupFillStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupFillStyle.Properties.Columns = 2;
            this.radioGroupFillStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "格网"), new RadioGroupItem(null, "随机") });
            this.radioGroupFillStyle.Size = new Size(0x70, 0x18);
            this.radioGroupFillStyle.TabIndex = 13;
            this.radioGroupFillStyle.SelectedIndexChanged += new EventHandler(this.radioGroupFillStyle_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x88, 0x58);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x48, 0x11);
            this.label5.TabIndex = 80;
            this.label5.Text = "轮廓线符号:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x88, 0x1c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x48, 0x11);
            this.label4.TabIndex = 0x4f;
            this.label4.Text = "填充点符号:";
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.radioGroupFillStyle);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.btnFillMarker);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Name = "MarkFillControl";
            base.Size = new Size(360, 0xe0);
            base.Load += new EventHandler(this.MarkFillControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.radioGroupFillStyle.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MarkFillControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void radioGroupFillStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.radioGroupFillStyle.SelectedIndex == 0)
                {
                    this.m_MarkerFillSymbol.Style = esriMarkerFillStyle.esriMFSGrid;
                }
                else
                {
                    this.m_MarkerFillSymbol.Style = esriMarkerFillStyle.esriMFSRandom;
                }
                this.refresh(e);
            }
        }

        private void refresh(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
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

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }
    }
}

