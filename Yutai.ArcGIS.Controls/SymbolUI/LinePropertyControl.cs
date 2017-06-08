using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class LinePropertyControl : UserControl, CommonInterface
    {
        private Container components = null;
        private Label label1;
        private bool m_CanDo = true;
        public ILineProperties m_LineProperties;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownOffset;

        public event ValueChangedHandler ValueChanged;

        public LinePropertyControl()
        {
            this.InitializeComponent();
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownOffset.Value = (decimal) ((((double) this.numericUpDownOffset.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownOffset.Value = (decimal) (this.m_LineProperties.Offset * this.m_unit);
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.numericUpDownOffset = new SpinEdit();
            this.numericUpDownOffset.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "偏移";
            int[] bits = new int[4];
            this.numericUpDownOffset.EditValue = new decimal(bits);
            this.numericUpDownOffset.Location = new Point(0x40, 0x18);
            this.numericUpDownOffset.Name = "numericUpDownOffset";
            this.numericUpDownOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownOffset.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.numericUpDownOffset.Properties.MinValue = new decimal(bits);
            this.numericUpDownOffset.Size = new Size(0x60, 0x17);
            this.numericUpDownOffset.TabIndex = 0x4a;
            this.numericUpDownOffset.TextChanged += new EventHandler(this.numericUpDownOffset_ValueChanged);
            base.Controls.Add(this.numericUpDownOffset);
            base.Controls.Add(this.label1);
            base.Name = "LinePropertyControl";
            base.Size = new Size(0x178, 0x138);
            base.Load += new EventHandler(this.LinePropertyControl_Load);
            this.numericUpDownOffset.Properties.EndInit();
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

        private void LinePropertyControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void numericUpDownOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownOffset.Value == 0M) && !IsNmuber(this.numericUpDownOffset.Text))
                {
                    this.numericUpDownOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownOffset.ForeColor = SystemColors.WindowText;
                    this.m_LineProperties.Offset = ((double) this.numericUpDownOffset.Value) / this.m_unit;
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

