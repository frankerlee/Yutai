using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class TableGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        [CompilerGenerated]
        private bool bool_2;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private MapTemplateElement mapTemplateElement_0;
        private TextBox txtCol;
        private TextBox txtHeight;
        private TextBox txtRow;
        private TextBox txtWidth;

        public event OnValueChangeEventHandler OnValueChange;

        public TableGeneralPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
                element.Width = Convert.ToDouble(this.txtWidth.Text);
                element.Height = Convert.ToDouble(this.txtHeight.Text);
                if (!this.IsEdit)
                {
                    element.RowNumber = Convert.ToInt32(this.txtRow.Text);
                    element.ColumnNumber = Convert.ToInt32(this.txtCol.Text);
                }
            }
        }

        public bool CanApply()
        {
            try
            {
                double num = Convert.ToDouble(this.txtWidth.Text);
                double num2 = Convert.ToDouble(this.txtHeight.Text);
                double num3 = Convert.ToDouble(this.txtRow.Text);
                double num4 = Convert.ToDouble(this.txtCol.Text);
                if (num <= 0.0)
                {
                    MessageBox.Show("宽度必须大于0!");
                    return false;
                }
                if (num2 <= 0.0)
                {
                    MessageBox.Show("高度必须大于0!");
                    return false;
                }
                if (num3 <= 0.0)
                {
                    MessageBox.Show("行数必须大于0!");
                    return false;
                }
                if (num4 <= 0.0)
                {
                    MessageBox.Show("列数必须大于0!");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("请检查输入数据是否正确!");
                return false;
            }
            return true;
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtRow = new TextBox();
            this.txtCol = new TextBox();
            this.txtWidth = new TextBox();
            this.txtHeight = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x1a, 0x4d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "行数";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x1a, 0x73);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "列数";
            this.txtRow.Location = new Point(0x49, 0x4a);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new Size(0xc2, 0x15);
            this.txtRow.TabIndex = 2;
            this.txtRow.Text = "2";
            this.txtRow.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtCol.Location = new Point(0x49, 0x70);
            this.txtCol.Name = "txtCol";
            this.txtCol.Size = new Size(0xc2, 0x15);
            this.txtCol.TabIndex = 3;
            this.txtCol.Text = "3";
            this.txtCol.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtWidth.Location = new Point(0x49, 40);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(0xc2, 0x15);
            this.txtWidth.TabIndex = 7;
            this.txtWidth.Text = "4";
            this.txtWidth.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtHeight.Location = new Point(0x49, 13);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(0xc2, 0x15);
            this.txtHeight.TabIndex = 6;
            this.txtHeight.Text = "4";
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1a, 0x2b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "宽";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1a, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "高";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x111, 0x10);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "厘米";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x111, 0x2b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "厘米";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.txtHeight);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtCol);
            base.Controls.Add(this.txtRow);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TableGeneralPage";
            base.Size = new Size(0x149, 0xe3);
            base.Load += new EventHandler(this.TableGeneralPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
        }

        private void TableGeneralPage_Load(object sender, EventArgs e)
        {
            this.txtCol.Visible = !this.IsEdit;
            this.txtRow.Visible = !this.IsEdit;
            this.label1.Visible = !this.IsEdit;
            this.label2.Visible = !this.IsEdit;
            MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
            this.txtWidth.Text = element.Width.ToString();
            this.txtHeight.Text = element.Height.ToString();
            this.txtRow.Text = element.RowNumber.ToString();
            this.txtCol.Text = element.ColumnNumber.ToString();
            this.bool_0 = true;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsEdit
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return "常规";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

