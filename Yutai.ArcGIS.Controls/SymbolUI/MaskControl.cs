using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class MaskControl : UserControl, CommonInterface
    {
        private NewSymbolButton btnFillSymbol;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label1;
        private bool m_CanDo = true;
        public IMask m_pMask;
        public IStyleGallery m_pSG;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownSize;
        private RadioGroup radMaskStyle;

        public event ValueChangedHandler ValueChanged;

        public MaskControl()
        {
            this.InitializeComponent();
        }

        private void btnFillSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                ISymbol symbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (this.m_pMask.MaskSymbol != null)
                {
                    symbol = (ISymbol) ((IClone) this.m_pMask.MaskSymbol).Clone();
                }
                else
                {
                    symbol = new SimpleFillSymbolClass();
                }
                selector.SetSymbol(symbol);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_pMask.MaskSymbol = (IFillSymbol) selector.GetSymbol();
                    this.btnFillSymbol.Style = this.m_pMask.MaskSymbol;
                    this.btnFillSymbol.Invalidate();
                    this.refresh(e);
                }
            }
            catch
            {
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownSize.Value = (decimal) ((((double) this.numericUpDownSize.Value) / this.m_unit) * newunit);
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

        public void InitControl()
        {
            this.m_CanDo = false;
            if (this.m_pMask.MaskStyle == esriMaskStyle.esriMSHalo)
            {
                this.radMaskStyle.SelectedIndex = 1;
            }
            else
            {
                this.radMaskStyle.SelectedIndex = 0;
            }
            this.numericUpDownSize.Value = (decimal) (this.m_pMask.MaskSize * this.m_unit);
            this.btnFillSymbol.Style = this.m_pMask.MaskSymbol;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.radMaskStyle = new RadioGroup();
            this.label1 = new Label();
            this.numericUpDownSize = new SpinEdit();
            this.btnFillSymbol = new NewSymbolButton();
            this.groupBox1.SuspendLayout();
            this.radMaskStyle.Properties.BeginInit();
            this.numericUpDownSize.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radMaskStyle);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(80, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            this.radMaskStyle.Location = new Point(8, 0x10);
            this.radMaskStyle.Name = "radMaskStyle";
            this.radMaskStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radMaskStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radMaskStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radMaskStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "无"), new RadioGroupItem(null, "中空") });
            this.radMaskStyle.Size = new Size(0x38, 0x30);
            this.radMaskStyle.TabIndex = 2;
            this.radMaskStyle.SelectedIndexChanged += new EventHandler(this.radMaskStyle_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x7b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "大小";
            int[] bits = new int[4];
            this.numericUpDownSize.EditValue = new decimal(bits);
            this.numericUpDownSize.Location = new Point(0x38, 120);
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownSize.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownSize.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownSize.Properties.MaxValue = new decimal(bits);
            this.numericUpDownSize.Properties.UseCtrlIncrement = false;
            this.numericUpDownSize.Size = new Size(0x58, 0x17);
            this.numericUpDownSize.TabIndex = 4;
            this.numericUpDownSize.TextChanged += new EventHandler(this.numericUpDownSize_ValueChanged);
            this.btnFillSymbol.Location = new Point(0x98, 120);
            this.btnFillSymbol.Name = "btnFillSymbol";
            this.btnFillSymbol.Size = new Size(0x68, 40);
            this.btnFillSymbol.Style = null;
            this.btnFillSymbol.TabIndex = 5;
            this.btnFillSymbol.Click += new EventHandler(this.btnFillSymbol_Click);
            base.Controls.Add(this.btnFillSymbol);
            base.Controls.Add(this.numericUpDownSize);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "MaskControl";
            base.Size = new Size(0x160, 240);
            base.Load += new EventHandler(this.MaskControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.radMaskStyle.Properties.EndInit();
            this.numericUpDownSize.Properties.EndInit();
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

        private void MaskControl_Load(object sender, EventArgs e)
        {
            if (this.m_pMask != null)
            {
                this.InitControl();
            }
        }

        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if ((this.numericUpDownSize.Value == 0M) && !IsNmuber(this.numericUpDownSize.Text))
                {
                    this.numericUpDownSize.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownSize.ForeColor = SystemColors.WindowText;
                    this.m_pMask.MaskSize = ((double) this.numericUpDownSize.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void radMaskStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.radMaskStyle.SelectedIndex == 0)
                {
                    this.m_pMask.MaskStyle = esriMaskStyle.esriMSNone;
                }
                else
                {
                    this.m_pMask.MaskStyle = esriMaskStyle.esriMSHalo;
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
    }
}

