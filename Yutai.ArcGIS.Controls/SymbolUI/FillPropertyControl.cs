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
    internal class FillPropertyControl : UserControl, CommonInterface
    {
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool m_CanDo = true;
        public IFillProperties m_FillProperties;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownXOffset;
        private SpinEdit numericUpDownXSpace;
        private SpinEdit numericUpDownYOffset;
        private SpinEdit numericUpDownYSpace;

        public event ValueChangedHandler ValueChanged;

        public FillPropertyControl()
        {
            this.InitializeComponent();
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownXOffset.Value = (decimal) ((((double) this.numericUpDownXOffset.Value) / this.m_unit) * newunit);
            this.numericUpDownYOffset.Value = (decimal) ((((double) this.numericUpDownYOffset.Value) / this.m_unit) * newunit);
            this.numericUpDownXSpace.Value = (decimal) ((((double) this.numericUpDownXSpace.Value) / this.m_unit) * newunit);
            this.numericUpDownYSpace.Value = (decimal) ((((double) this.numericUpDownYSpace.Value) / this.m_unit) * newunit);
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

        private void FillPropertyControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownXOffset.Value = (decimal) (this.m_FillProperties.XOffset * this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_FillProperties.YOffset * this.m_unit);
            this.numericUpDownXSpace.Value = (decimal) (this.m_FillProperties.XSeparation * this.m_unit);
            this.numericUpDownYSpace.Value = (decimal) (this.m_FillProperties.YSeparation * this.m_unit);
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.numericUpDownYOffset = new SpinEdit();
            this.numericUpDownXOffset = new SpinEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.numericUpDownYSpace = new SpinEdit();
            this.numericUpDownXSpace = new SpinEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            this.numericUpDownYOffset.Properties.BeginInit();
            this.numericUpDownXOffset.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.numericUpDownYSpace.Properties.BeginInit();
            this.numericUpDownXSpace.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.numericUpDownYOffset);
            this.groupBox1.Controls.Add(this.numericUpDownXOffset);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x20, 0x20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x80, 0x58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "偏移量";
         
            this.numericUpDownYOffset.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownYOffset.Location = new Point(0x30, 50);
            this.numericUpDownYOffset.Name = "numericUpDownYOffset";
            this.numericUpDownYOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownYOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownYOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownYOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownYOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
           
            this.numericUpDownYOffset.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            
            this.numericUpDownYOffset.Properties.MinValue = new decimal(new int[] {
            100,
            0,
            0,
             -2147483648});
            this.numericUpDownYOffset.Properties.UseCtrlIncrement = false;
            this.numericUpDownYOffset.Size = new Size(0x40, 0x17);
            this.numericUpDownYOffset.TabIndex = 70;
            this.numericUpDownYOffset.TextChanged += new EventHandler(this.numericUpDownYOffset_ValueChanged);
            
            this.numericUpDownXOffset.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownXOffset.Location = new Point(0x30, 0x12);
            this.numericUpDownXOffset.Name = "numericUpDownXOffset";
            this.numericUpDownXOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownXOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownXOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownXOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownXOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            this.numericUpDownXOffset.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownXOffset.Properties.MinValue = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownXOffset.Properties.UseCtrlIncrement = false;
            this.numericUpDownXOffset.Size = new Size(0x40, 0x17);
            this.numericUpDownXOffset.TabIndex = 0x45;
            this.numericUpDownXOffset.TextChanged += new EventHandler(this.numericUpDownXOffset_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            this.groupBox2.Controls.Add(this.numericUpDownYSpace);
            this.groupBox2.Controls.Add(this.numericUpDownXSpace);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new Point(0x20, 0x88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x80, 0x58);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "间隔";
           
            this.numericUpDownYSpace.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownYSpace.Location = new Point(0x30, 50);
            this.numericUpDownYSpace.Name = "numericUpDownYSpace";
            this.numericUpDownYSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownYSpace.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownYSpace.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownYSpace.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownYSpace.Properties.EditFormat.FormatType = FormatType.Numeric;
            this.numericUpDownYSpace.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownYSpace.Properties.MinValue = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownYSpace.Properties.UseCtrlIncrement = false;
            this.numericUpDownYSpace.Size = new Size(0x40, 0x17);
            this.numericUpDownYSpace.TabIndex = 0x48;
            this.numericUpDownYSpace.TextChanged += new EventHandler(this.numericUpDownYSpace_ValueChanged);
            this.numericUpDownXSpace.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownXSpace.Location = new Point(0x30, 0x12);
            this.numericUpDownXSpace.Name = "numericUpDownXSpace";
            this.numericUpDownXSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownXSpace.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownXSpace.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownXSpace.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownXSpace.Properties.EditFormat.FormatType = FormatType.Numeric;
           
            this.numericUpDownXSpace.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownXSpace.Properties.MinValue = new  decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownXSpace.Properties.UseCtrlIncrement = false;
            this.numericUpDownXSpace.Size = new Size(0x40, 0x17);
            this.numericUpDownXSpace.TabIndex = 0x47;
            this.numericUpDownXSpace.TextChanged += new EventHandler(this.numericUpDownXSpace_ValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "Y:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 0x11);
            this.label4.TabIndex = 2;
            this.label4.Text = "X:";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "FillPropertyControl";
            base.Size = new Size(0x180, 0x108);
            base.Load += new EventHandler(this.FillPropertyControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.numericUpDownYOffset.Properties.EndInit();
            this.numericUpDownXOffset.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.numericUpDownYSpace.Properties.EndInit();
            this.numericUpDownXSpace.Properties.EndInit();
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

        private void numericUpDownXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownXOffset.Value == 0M) && !IsNmuber(this.numericUpDownXOffset.Text))
                {
                    this.numericUpDownXOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownXOffset.ForeColor = SystemColors.WindowText;
                    this.m_FillProperties.XOffset = ((double) this.numericUpDownXOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownXSpace_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownXSpace.Value <= 0M)
                {
                    this.numericUpDownXSpace.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownXSpace.ForeColor = SystemColors.WindowText;
                    this.m_FillProperties.XSeparation = ((double) this.numericUpDownXSpace.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownYOffset_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownYOffset.Value == 0M) && !IsNmuber(this.numericUpDownYOffset.Text))
                {
                    this.numericUpDownYOffset.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownYOffset.ForeColor = SystemColors.WindowText;
                    this.m_FillProperties.YOffset = ((double) this.numericUpDownYOffset.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void numericUpDownYSpace_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownYSpace.Value <= 0M)
                {
                    this.numericUpDownYSpace.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownYSpace.ForeColor = SystemColors.WindowText;
                    this.m_FillProperties.YSeparation = ((double) this.numericUpDownYSpace.Value) / this.m_unit;
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

