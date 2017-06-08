using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class GradientFillControl : UserControl
    {
        private NewSymbolButton btnOutline;
        private ComboBoxEdit cboGradientFillStyle;
        private ColorRampComboBox colorRampComboBox;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private bool m_CanDo = true;
        public IGradientFillSymbol m_GradientFillSymbol;
        public IStyleGallery m_pSG;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownAngle;
        private SpinEdit numericUpDownIntervalCount;
        private SpinEdit numericUpDownPrecent;

        public event ValueChangedHandler ValueChanged;

        public GradientFillControl()
        {
            this.InitializeComponent();
        }

        private void btnOutline_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_GradientFillSymbol.Outline != null)
                {
                    selector.SetSymbol((ISymbol) this.m_GradientFillSymbol.Outline);
                }
                else
                {
                    selector.SetSymbol(new SimpleLineSymbolClass());
                }
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_GradientFillSymbol.Outline = (ILineSymbol) selector.GetSymbol();
                    this.btnOutline.Style = this.m_GradientFillSymbol.Outline;
                    this.btnOutline.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        private void cboGradientFillStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_GradientFillSymbol.Style = (esriGradientFillStyle) this.cboGradientFillStyle.SelectedIndex;
                this.refresh(e);
            }
        }

        private void colorRampComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_GradientFillSymbol.ColorRamp = this.colorRampComboBox.GetSelectColorRamp();
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

        private void GradientFillControl1_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownAngle.Value = (decimal) this.m_GradientFillSymbol.GradientAngle;
            this.numericUpDownPrecent.Value = (decimal) (this.m_GradientFillSymbol.GradientPercentage * 100.0);
            this.numericUpDownIntervalCount.Value = this.m_GradientFillSymbol.IntervalCount;
            this.cboGradientFillStyle.SelectedIndex = (int) this.m_GradientFillSymbol.Style;
            this.btnOutline.Style = this.m_GradientFillSymbol.Outline;
            IStyleGalleryItem item = new ServerStyleGalleryItemClass {
                Item = this.m_GradientFillSymbol.ColorRamp
            };
            this.colorRampComboBox.Add(item);
            if (this.m_pSG != null)
            {
                IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Color Ramps", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.colorRampComboBox.Add(item);
                }
                item2 = null;
                GC.Collect();
            }
            if (this.colorRampComboBox.Items.Count > 0)
            {
                this.colorRampComboBox.SelectedIndex = 0;
            }
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.colorRampComboBox = new ColorRampComboBox();
            this.numericUpDownIntervalCount = new SpinEdit();
            this.numericUpDownAngle = new SpinEdit();
            this.numericUpDownPrecent = new SpinEdit();
            this.cboGradientFillStyle = new ComboBoxEdit();
            this.label6 = new Label();
            this.btnOutline = new NewSymbolButton();
            this.numericUpDownIntervalCount.Properties.BeginInit();
            this.numericUpDownAngle.Properties.BeginInit();
            this.numericUpDownPrecent.Properties.BeginInit();
            this.cboGradientFillStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x2c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "间隔";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x4b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2a, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "百分比";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x6a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "角度";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xb8, 0x2c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 0x11);
            this.label4.TabIndex = 3;
            this.label4.Text = "样式";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xb8, 0x6a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x36, 0x11);
            this.label5.TabIndex = 4;
            this.label5.Text = "颜色梯度";
            this.colorRampComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            this.colorRampComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.colorRampComboBox.Location = new Point(0xf8, 0x68);
            this.colorRampComboBox.Name = "colorRampComboBox";
            this.colorRampComboBox.Size = new Size(120, 0x16);
            this.colorRampComboBox.TabIndex = 9;
            this.colorRampComboBox.SelectedIndexChanged += new EventHandler(this.colorRampComboBox_SelectedIndexChanged);
            int[] bits = new int[4];
            bits[0] = 2;
            this.numericUpDownIntervalCount.EditValue = new decimal(bits);
            this.numericUpDownIntervalCount.Location = new Point(0x40, 40);
            this.numericUpDownIntervalCount.Name = "numericUpDownIntervalCount";
            this.numericUpDownIntervalCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 500;
            this.numericUpDownIntervalCount.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 2;
            this.numericUpDownIntervalCount.Properties.MinValue = new decimal(bits);
            this.numericUpDownIntervalCount.Properties.UseCtrlIncrement = false;
            this.numericUpDownIntervalCount.Size = new Size(0x40, 0x17);
            this.numericUpDownIntervalCount.TabIndex = 0x48;
            this.numericUpDownIntervalCount.TextChanged += new EventHandler(this.numericUpDownIntervalCount_ValueChanged);
            bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(0x40, 0x68);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Properties.UseCtrlIncrement = false;
            this.numericUpDownAngle.Size = new Size(0x40, 0x17);
            this.numericUpDownAngle.TabIndex = 0x47;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            bits = new int[4];
            this.numericUpDownPrecent.EditValue = new decimal(bits);
            this.numericUpDownPrecent.Location = new Point(0x40, 0x48);
            this.numericUpDownPrecent.Name = "numericUpDownPrecent";
            this.numericUpDownPrecent.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownPrecent.Properties.MaxValue = new decimal(bits);
            this.numericUpDownPrecent.Properties.UseCtrlIncrement = false;
            this.numericUpDownPrecent.Size = new Size(0x40, 0x17);
            this.numericUpDownPrecent.TabIndex = 70;
            this.numericUpDownPrecent.TextChanged += new EventHandler(this.numericUpDownPrecent_ValueChanged);
            this.cboGradientFillStyle.EditValue = "线性";
            this.cboGradientFillStyle.Location = new Point(0xf8, 40);
            this.cboGradientFillStyle.Name = "cboGradientFillStyle";
            this.cboGradientFillStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboGradientFillStyle.Properties.Items.AddRange(new object[] { "线性", "方形", "圆形", "缓冲区" });
            this.cboGradientFillStyle.Size = new Size(120, 0x17);
            this.cboGradientFillStyle.TabIndex = 0x49;
            this.cboGradientFillStyle.SelectedIndexChanged += new EventHandler(this.cboGradientFillStyle_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xb8, 0x90);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x48, 0x11);
            this.label6.TabIndex = 80;
            this.label6.Text = "轮廓线符号:";
            this.btnOutline.Location = new Point(0x100, 0x88);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(0x70, 0x20);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 0x4f;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.cboGradientFillStyle);
            base.Controls.Add(this.numericUpDownIntervalCount);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.numericUpDownPrecent);
            base.Controls.Add(this.colorRampComboBox);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "GradientFillControl";
            base.Size = new Size(400, 0x120);
            base.Load += new EventHandler(this.GradientFillControl1_Load);
            this.numericUpDownIntervalCount.Properties.EndInit();
            this.numericUpDownAngle.Properties.EndInit();
            this.numericUpDownPrecent.Properties.EndInit();
            this.cboGradientFillStyle.Properties.EndInit();
            base.ResumeLayout(false);
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

        private void numericUpDownAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownAngle.Value == 0M) && !IsNmuber(this.numericUpDownAngle.Text))
                {
                    this.numericUpDownAngle.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownAngle.ForeColor = SystemColors.WindowText;
                    this.m_GradientFillSymbol.GradientAngle = (double) this.numericUpDownAngle.Value;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownIntervalCount_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownIntervalCount.Value < 2M)
                {
                    this.numericUpDownIntervalCount.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownIntervalCount.ForeColor = SystemColors.WindowText;
                    this.m_GradientFillSymbol.IntervalCount = (int) this.numericUpDownIntervalCount.Value;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownPrecent_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownPrecent.Value < 0M) || (this.numericUpDownPrecent.Value > 100M))
                {
                    this.numericUpDownPrecent.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownPrecent.ForeColor = SystemColors.WindowText;
                    this.m_GradientFillSymbol.GradientPercentage = ((double) this.numericUpDownPrecent.Value) / 100.0;
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
    }
}

