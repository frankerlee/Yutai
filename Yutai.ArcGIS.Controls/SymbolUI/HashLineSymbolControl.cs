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
    internal class HashLineSymbolControl : UserControl
    {
        private SimpleButton btnLineSymbol;
        private Container components = null;
        private Label label1;
        private bool m_CanDo = true;
        public IHashLineSymbol m_pHashLineSymbol;
        public IStyleGallery m_pSG;
        private SpinEdit numericUpDownAngle;
        private NewSymbolButton simpleButton1;

        public event ValueChangedHandler ValueChanged;

        public HashLineSymbolControl()
        {
            this.InitializeComponent();
        }

        private void btnLineSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                ISymbol pSym = null;
                if (this.m_pHashLineSymbol.HashSymbol != null)
                {
                    pSym = (ISymbol) ((IClone) this.m_pHashLineSymbol.HashSymbol).Clone();
                }
                else
                {
                    pSym = new SimpleLineSymbolClass();
                }
                selector.SetSymbol(pSym);
                selector.SetStyleGallery(this.m_pSG);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_pHashLineSymbol.HashSymbol = (ILineSymbol) selector.GetSymbol();
                    this.refresh(e);
                    this.simpleButton1.Invalidate();
                }
            }
            catch
            {
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

        private void HashLineSymbolControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownAngle.Value = (decimal) this.m_pHashLineSymbol.Angle;
            this.simpleButton1.Style = this.m_pHashLineSymbol;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.numericUpDownAngle = new SpinEdit();
            this.btnLineSymbol = new SimpleButton();
            this.simpleButton1 = new NewSymbolButton();
            this.numericUpDownAngle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x23);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "角度";
            int[] bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(0x48, 0x20);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Size = new Size(0x40, 0x17);
            this.numericUpDownAngle.TabIndex = 0x44;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            this.btnLineSymbol.Location = new Point(0x48, 0xa8);
            this.btnLineSymbol.Name = "btnLineSymbol";
            this.btnLineSymbol.Size = new Size(0x60, 0x18);
            this.btnLineSymbol.TabIndex = 0x45;
            this.btnLineSymbol.Text = "细切线符号...";
            this.btnLineSymbol.Visible = false;
            this.btnLineSymbol.Click += new EventHandler(this.btnLineSymbol_Click);
            this.simpleButton1.Location = new Point(0x48, 0x40);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x60, 0x30);
            this.simpleButton1.Style = null;
            this.simpleButton1.TabIndex = 70;
            this.simpleButton1.Click += new EventHandler(this.btnLineSymbol_Click);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnLineSymbol);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.label1);
            base.Name = "HashLineSymbolControl";
            base.Size = new Size(360, 0x128);
            base.Load += new EventHandler(this.HashLineSymbolControl_Load);
            this.numericUpDownAngle.Properties.EndInit();
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
                    this.m_pHashLineSymbol.Angle = (double) this.numericUpDownAngle.Value;
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

