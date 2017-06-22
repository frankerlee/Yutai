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
    internal partial class MaskControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public IMask m_pMask;
        public IStyleGallery m_pSG;
        public double m_unit = 1.0;

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

