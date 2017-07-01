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
    internal partial class LinePropertyControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public ILineProperties m_LineProperties;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public LinePropertyControl()
        {
            this.InitializeComponent();
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownOffset.Value = (decimal) ((((double) this.numericUpDownOffset.Value)/this.m_unit)*newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownOffset.Value = (decimal) (this.m_LineProperties.Offset*this.m_unit);
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
                    this.m_LineProperties.Offset = ((double) this.numericUpDownOffset.Value)/this.m_unit;
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