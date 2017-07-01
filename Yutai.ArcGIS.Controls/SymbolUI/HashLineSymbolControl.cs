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
    internal partial class HashLineSymbolControl : UserControl
    {
        private bool m_CanDo = true;
        public IHashLineSymbol m_pHashLineSymbol;
        public IStyleGallery m_pSG;

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