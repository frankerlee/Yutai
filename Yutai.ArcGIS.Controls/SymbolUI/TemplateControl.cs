using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class TemplateControl : UserControl, CommonInterface
    {
        private bool m_CanDo = true;
        public ILineProperties m_LineProperties;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public TemplateControl()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.m_LineProperties.Template == null)
            {
                this.m_LineProperties.Template = new TemplateClass();
            }
            ITemplate pTemplate = this.m_LineProperties.Template;
            pTemplate.AddPatternElement(5.0, 5.0);
            this.refresh(e);
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
            this.ChangeListView(pTemplate);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.TemplatelistView.Items.Clear();
            this.m_LineProperties.Template.ClearPatternElements();
            this.refresh(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.TemplatelistView.SelectedItems.Count != 0)
            {
                int tag = (int) this.TemplatelistView.SelectedItems[0].Tag;
                tag *= 2;
                this.TemplatelistView.Items.RemoveAt(tag + 1);
                this.TemplatelistView.Items.RemoveAt(tag);
                this.TemplatelistView.SelectedItems.Clear();
                this.ReadTemplate(e);
            }
        }

        private void ChangeListView(ITemplate pTemplate)
        {
            string[] items = new string[2];
            this.TemplatelistView.Items.Clear();
            for (int i = 0; i < pTemplate.PatternElementCount; i++)
            {
                double num;
                double num2;
                pTemplate.GetPatternElement(i, out num, out num2);
                items[0] = "实部长";
                items[1] = (num * this.m_unit).ToString("0.####");
                ListViewItem item = new ListViewItem(items) {
                    Tag = i
                };
                this.TemplatelistView.Items.Add(item);
                items[0] = "虚部长";
                items[1] = (num2 * this.m_unit).ToString("0.####");
                item = new ListViewItem(items) {
                    Tag = i
                };
                this.TemplatelistView.Items.Add(item);
            }
        }

        public void ChangeUnit(double newunit)
        {
            try
            {
                double num = 0.0;
                double num2 = 0.0;
                for (int i = 0; i < this.TemplatelistView.Items.Count; i += 2)
                {
                    num = Convert.ToDouble(this.TemplatelistView.Items[i].SubItems[1].Text);
                    num2 = Convert.ToDouble(this.TemplatelistView.Items[i + 1].SubItems[1].Text);
                    num = (num / this.m_unit) * newunit;
                    num2 = (num2 / this.m_unit) * newunit;
                    this.TemplatelistView.Items[i].SubItems[1].Text = num.ToString("0.####");
                    this.TemplatelistView.Items[i + 1].SubItems[1].Text = num2.ToString("0.####");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据错误！");
            }
            this.m_unit = newunit;
        }

 private void InitControl()
        {
            this.m_CanDo = false;
            string[] items = new string[2];
            if (this.m_LineProperties.Template != null)
            {
                for (int i = 0; i < this.m_LineProperties.Template.PatternElementCount; i++)
                {
                    double num;
                    double num2;
                    this.m_LineProperties.Template.GetPatternElement(i, out num, out num2);
                    items[0] = "实部长";
                    items[1] = (num * this.m_unit).ToString("0.####");
                    ListViewItem item = new ListViewItem(items) {
                        Tag = i
                    };
                    this.TemplatelistView.Items.Add(item);
                    items[0] = "虚部长";
                    items[1] = (num2 * this.m_unit).ToString("0.####");
                    item = new ListViewItem(items) {
                        Tag = i
                    };
                    this.TemplatelistView.Items.Add(item);
                }
                this.numericUpDownInterval.Value = (decimal) this.m_LineProperties.Template.Interval;
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

        private void numericUpDownInterval_KeyUp(object sender, KeyEventArgs e)
        {
            this.numericUpDownInterval_ValueChanged(sender, e);
        }

        private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownInterval.Value <= 0M)
                {
                    this.numericUpDownInterval.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownInterval.ForeColor = SystemColors.WindowText;
                    if (this.m_LineProperties.Template != null)
                    {
                        this.m_LineProperties.Template.Interval = (double) this.numericUpDownInterval.Value;
                        this.refresh(e);
                    }
                }
            }
        }

        private void ReadTemplate(EventArgs e)
        {
            this.TemplatelistView.ForeColor = SystemColors.WindowText;
            ITemplate template = new TemplateClass {
                Interval = this.m_LineProperties.Template.Interval
            };
            double mark = 0.0;
            double gap = 0.0;
            try
            {
                double num3 = 0.0;
                for (int i = 0; i < this.TemplatelistView.Items.Count; i += 2)
                {
                    mark = Convert.ToDouble(this.TemplatelistView.Items[i].SubItems[1].Text);
                    gap = Convert.ToDouble(this.TemplatelistView.Items[i + 1].SubItems[1].Text);
                    if ((mark < 0.0) || (gap < 0.0))
                    {
                        MessageBox.Show("实步长或虚步长不能为负数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.TemplatelistView.ForeColor = Color.Red;
                        return;
                    }
                    mark /= this.m_unit;
                    gap /= this.m_unit;
                    num3 = (num3 + mark) + gap;
                    template.AddPatternElement(mark, gap);
                }
                if (num3 == 0.0)
                {
                    MessageBox.Show("模板总长不能为0！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.TemplatelistView.ForeColor = Color.Red;
                }
                else
                {
                    this.m_LineProperties.Template = template;
                    this.refresh(e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据类型错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.TemplatelistView.ForeColor = Color.Red;
            }
        }

        private void refresh(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        private void TemplateControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void TemplatelistView_ValueChanged(object sender, EventArgs e)
        {
            this.ReadTemplate(e);
        }
    }
}

