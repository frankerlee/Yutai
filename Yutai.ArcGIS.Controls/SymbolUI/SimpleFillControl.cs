using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class SimpleFillControl : UserControl, CommonInterface
    {
        private NewSymbolButton btnOutline;
        private ComboBoxEdit cboStyle;
        private ColorEdit colorEdit1;
        private ColorEdit colorEditOutline;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private bool m_CanDo = true;
        public IStyleGallery m_pSG;
        public ISimpleFillSymbol m_SimpleFillSymbol;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownWidth;

        public event ValueChangedHandler ValueChanged;

        public SimpleFillControl()
        {
            this.InitializeComponent();
        }

        private void btnOutline_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_SimpleFillSymbol.Outline != null)
                {
                    selector.SetSymbol((ISymbol) this.m_SimpleFillSymbol.Outline);
                }
                else
                {
                    selector.SetSymbol(new SimpleLineSymbolClass());
                }
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_SimpleFillSymbol.Outline = (ILineSymbol) selector.GetSymbol();
                    this.btnOutline.Style = this.m_SimpleFillSymbol.Outline;
                    this.btnOutline.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_SimpleFillSymbol.Style = (esriSimpleFillStyle) this.cboStyle.SelectedIndex;
                if ((this.m_SimpleFillSymbol.Style == esriSimpleFillStyle.esriSFSNull) || (this.m_SimpleFillSymbol.Style == esriSimpleFillStyle.esriSFSNull))
                {
                    this.colorEdit1.Enabled = false;
                }
                else
                {
                    this.colorEdit1.Enabled = true;
                }
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_SimpleFillSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_SimpleFillSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void colorEditOutline_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ILineSymbol outline = this.m_SimpleFillSymbol.Outline;
                IColor pColor = outline.Color;
                this.UpdateColorFromColorEdit(this.colorEditOutline, pColor);
                outline.Color = pColor;
                this.m_SimpleFillSymbol.Outline = outline;
                this.btnOutline.Style = outline;
                this.btnOutline.Invalidate();
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
            this.btnOutline.Style = this.m_SimpleFillSymbol.Outline;
            this.numericUpDownWidth.Value = (decimal) (this.m_SimpleFillSymbol.Outline.Width * this.m_unit);
            this.SetColorEdit(this.colorEdit1, this.m_SimpleFillSymbol.Color);
            this.SetColorEdit(this.colorEditOutline, this.m_SimpleFillSymbol.Outline.Color);
            this.cboStyle.SelectedIndex = (int) this.m_SimpleFillSymbol.Style;
            esriSimpleFillStyle style = this.m_SimpleFillSymbol.Style;
            if ((style == esriSimpleFillStyle.esriSFSNull ? true : style == esriSimpleFillStyle.esriSFSNull))
            {
                this.colorEdit1.Enabled = false;
            }
            this.m_CanDo = true;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.colorEditOutline = new ColorEdit();
            this.numericUpDownWidth = new SpinEdit();
            this.btnOutline = new NewSymbolButton();
            this.label5 = new Label();
            this.cboStyle = new ComboBoxEdit();
            this.label4 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.colorEditOutline.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.cboStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x37);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x4f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "轮廓线颜色";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x6f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "轮廓线宽";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x58, 0x2f);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 4;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.colorEditOutline.EditValue = Color.Empty;
            this.colorEditOutline.Location = new Point(0x58, 0x47);
            this.colorEditOutline.Name = "colorEditOutline";
            this.colorEditOutline.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditOutline.Size = new Size(0x30, 0x15);
            this.colorEditOutline.TabIndex = 5;
            this.colorEditOutline.EditValueChanged += new EventHandler(this.colorEditOutline_EditValueChanged);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(0x58, 0x67);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Size = new Size(0x40, 0x15);
            this.numericUpDownWidth.TabIndex = 0x47;
            this.numericUpDownWidth.EditValueChanged += new EventHandler(this.numericUpDownWidth_EditValueChanged);
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            this.btnOutline.Location = new Point(0x58, 0x87);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(0x58, 40);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 0x4f;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x97);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x47, 12);
            this.label5.TabIndex = 80;
            this.label5.Text = "轮廓线符号:";
            this.cboStyle.EditValue = "颜色填充";
            this.cboStyle.Location = new Point(0x58, 0x11);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            this.cboStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStyle.Properties.Items.AddRange(new object[] { "颜色填充", "不填充", "水平线填充", "竖直线填充", "45度下斜线填充", "45度上斜线填充", "十字丝填充", "X填充" });
            this.cboStyle.Size = new Size(80, 0x15);
            this.cboStyle.TabIndex = 90;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x59;
            this.label4.Text = "样式";
            base.Controls.Add(this.cboStyle);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.colorEditOutline);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "SimpleFillControl";
            base.Size = new Size(0x178, 0xe8);
            base.Load += new EventHandler(this.SimpleFillControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.colorEditOutline.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.cboStyle.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static bool IsNmuber(string str)
        {
            if (str.Length > 0)
            {
                int num2;
                int num = 0;
                if ((str[0] < '0') || (str[0] > '9'))
                {
                    if (str[0] != '.')
                    {
                        if (str[0] != '-')
                        {
                            if (str[0] != '+')
                            {
                                return false;
                            }
                            for (num2 = 1; num2 < str.Length; num2++)
                            {
                                if ((str[num2] < '0') || (str[num2] > '9'))
                                {
                                    if (str[num2] != '.')
                                    {
                                        return false;
                                    }
                                    if (num == 1)
                                    {
                                        return false;
                                    }
                                    num++;
                                }
                            }
                        }
                        else
                        {
                            for (num2 = 1; num2 < str.Length; num2++)
                            {
                                if ((str[num2] < '0') || (str[num2] > '9'))
                                {
                                    if (str[num2] != '.')
                                    {
                                        return false;
                                    }
                                    if (num == 1)
                                    {
                                        return false;
                                    }
                                    num++;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (num2 = 1; num2 < str.Length; num2++)
                        {
                            if ((str[num2] < '0') || (str[num2] > '9'))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    for (num2 = 1; num2 < str.Length; num2++)
                    {
                        if ((str[num2] < '0') || (str[num2] > '9'))
                        {
                            if (str[num2] != '.')
                            {
                                return false;
                            }
                            if (num == 1)
                            {
                                return false;
                            }
                            num++;
                        }
                    }
                }
            }
            return true;
        }

        private void numericUpDownWidth_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownWidth.Value < 0M)
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else if ((this.numericUpDownWidth.Value == 0M) && !IsNmuber(this.numericUpDownWidth.Text))
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownWidth.ForeColor = SystemColors.WindowText;
                    ILineSymbol outline = this.m_SimpleFillSymbol.Outline;
                    outline.Width = ((double) this.numericUpDownWidth.Value) / this.m_unit;
                    this.m_SimpleFillSymbol.Outline = outline;
                    this.refresh(e);
                }
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

        private void SimpleFillControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
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

