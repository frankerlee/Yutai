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
    internal partial class FillPropertyControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public IFillProperties m_FillProperties;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public FillPropertyControl()
        {
            this.InitializeComponent();
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownXOffset.Value =
                (decimal) ((((double) this.numericUpDownXOffset.Value)/this.m_unit)*newunit);
            this.numericUpDownYOffset.Value =
                (decimal) ((((double) this.numericUpDownYOffset.Value)/this.m_unit)*newunit);
            this.numericUpDownXSpace.Value = (decimal) ((((double) this.numericUpDownXSpace.Value)/this.m_unit)*newunit);
            this.numericUpDownYSpace.Value = (decimal) ((((double) this.numericUpDownYSpace.Value)/this.m_unit)*newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void FillPropertyControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownXOffset.Value = (decimal) (this.m_FillProperties.XOffset*this.m_unit);
            this.numericUpDownYOffset.Value = (decimal) (this.m_FillProperties.YOffset*this.m_unit);
            this.numericUpDownXSpace.Value = (decimal) (this.m_FillProperties.XSeparation*this.m_unit);
            this.numericUpDownYSpace.Value = (decimal) (this.m_FillProperties.YSeparation*this.m_unit);
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
                    this.m_FillProperties.XOffset = ((double) this.numericUpDownXOffset.Value)/this.m_unit;
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
                    this.m_FillProperties.XSeparation = ((double) this.numericUpDownXSpace.Value)/this.m_unit;
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
                    this.m_FillProperties.YOffset = ((double) this.numericUpDownYOffset.Value)/this.m_unit;
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
                    this.m_FillProperties.YSeparation = ((double) this.numericUpDownYSpace.Value)/this.m_unit;
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