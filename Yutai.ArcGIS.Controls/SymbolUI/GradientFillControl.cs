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
    internal partial class GradientFillControl : UserControl
    {
        private bool m_CanDo = true;
        public IGradientFillSymbol m_GradientFillSymbol;
        public IStyleGallery m_pSG;
        public double m_unit = 1.0;

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

        private void GradientFillControl1_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            this.m_CanDo = false;
            this.numericUpDownAngle.Value = (decimal) this.m_GradientFillSymbol.GradientAngle;
            this.numericUpDownPrecent.Value = (decimal) (this.m_GradientFillSymbol.GradientPercentage*100.0);
            this.numericUpDownIntervalCount.Value = this.m_GradientFillSymbol.IntervalCount;
            this.cboGradientFillStyle.SelectedIndex = (int) this.m_GradientFillSymbol.Style;
            this.btnOutline.Style = this.m_GradientFillSymbol.Outline;
            IStyleGalleryItem item = new ServerStyleGalleryItemClass
            {
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
                    this.m_GradientFillSymbol.GradientPercentage = ((double) this.numericUpDownPrecent.Value)/100.0;
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