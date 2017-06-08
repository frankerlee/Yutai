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
    internal class TemplateControl : UserControl, CommonInterface
    {
        private SimpleButton btnAdd;
        private SimpleButton btnClear;
        private SimpleButton btnDelete;
        private Container components = null;
        private KDColumnHeader header1;
        private KDColumnHeader header2;
        private Label label1;
        private Label label2;
        private bool m_CanDo = true;
        public ILineProperties m_LineProperties;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownInterval;
        private KDEditListView TemplatelistView;

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

        private void InitializeComponent()
        {
            this.TemplatelistView = new KDEditListView();
            this.header1 = new KDColumnHeader();
            this.header2 = new KDColumnHeader();
            this.label1 = new Label();
            this.numericUpDownInterval = new SpinEdit();
            this.label2 = new Label();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnClear = new SimpleButton();
            this.numericUpDownInterval.Properties.BeginInit();
            base.SuspendLayout();
            this.TemplatelistView.Columns.AddRange(new ColumnHeader[] { this.header1, this.header2 });
            this.TemplatelistView.ComboBoxBgColor = Color.LightBlue;
            this.TemplatelistView.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.TemplatelistView.EditBgColor = Color.LightBlue;
            this.TemplatelistView.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.TemplatelistView.FullRowSelect = true;
            this.TemplatelistView.GridLines = true;
            this.TemplatelistView.Location = new Point(0x18, 0x20);
            this.TemplatelistView.Name = "TemplatelistView";
            this.TemplatelistView.Size = new Size(240, 0x80);
            this.TemplatelistView.TabIndex = 3;
            this.TemplatelistView.View = View.Details;
            this.TemplatelistView.ValueChanged += new ValueChangedHandler(this.TemplatelistView_ValueChanged);
            this.header1.ColumnStyle = KDListViewColumnStyle.ReadOnly;
            this.header1.Text = "步长类型";
            this.header1.Width = 90;
            this.header2.ColumnStyle = KDListViewColumnStyle.EditBox;
            this.header2.Text = "步长值";
            this.header2.Width = 0x8a;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0xb8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "间隔";
            int[] bits = new int[4];
            this.numericUpDownInterval.EditValue = new decimal(bits);
            this.numericUpDownInterval.Location = new Point(0x40, 0xb0);
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 500;
            this.numericUpDownInterval.Properties.MaxValue = new decimal(bits);
            this.numericUpDownInterval.Properties.UseCtrlIncrement = false;
            this.numericUpDownInterval.Size = new Size(0x60, 0x17);
            this.numericUpDownInterval.TabIndex = 0x52;
            this.numericUpDownInterval.TextChanged += new EventHandler(this.numericUpDownInterval_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x18, 0xd0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x152, 0x11);
            this.label2.TabIndex = 0x53;
            this.label2.Text = "步长值的单位是单位组合框所选择项。间隔是步长值的倍数。";
            this.btnAdd.Location = new Point(0x110, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 0x54;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(0x110, 0x4c);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x38, 0x18);
            this.btnDelete.TabIndex = 0x55;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnClear.Location = new Point(0x110, 0x70);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x38, 0x18);
            this.btnClear.TabIndex = 0x56;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.numericUpDownInterval);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TemplatelistView);
            base.Name = "TemplateControl";
            base.Size = new Size(0x170, 0x100);
            base.Load += new EventHandler(this.TemplateControl_Load);
            this.numericUpDownInterval.Properties.EndInit();
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

