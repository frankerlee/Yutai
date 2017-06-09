using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class TemplatePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button button1;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox txtHeight;
        private TextBox txtScale;
        private TextBox txtWidth;

        public event OnValueChangeEventHandler OnValueChange;

        public TemplatePropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.textBox1.Text.Trim().Length != 0)
                {
                    CartoTemplateClass.Name = this.textBox1.Text;
                    CartoTemplateClass.Description = this.textBox2.Text;
                    if ((this.textBox3.Text.Length > 0) && File.Exists(this.textBox3.Text))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(this.textBox3.Text);
                        CartoTemplateClass.LegendInfo = document.InnerXml;
                    }
                    try
                    {
                        CartoTemplateClass.Scale = double.Parse(this.txtScale.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        CartoTemplateClass.Width = double.Parse(this.txtWidth.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        CartoTemplateClass.Height = double.Parse(this.txtHeight.Text);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = dialog.FileName;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                this.bool_1 = true;
            }
        }

        public bool CanApply()
        {
            if (this.textBox1.Text.Trim().Length == 0)
            {
                return false;
            }
            try
            {
                CartoTemplateClass.Scale = double.Parse(this.txtScale.Text);
            }
            catch
            {
                return false;
            }
            try
            {
                CartoTemplateClass.Width = double.Parse(this.txtWidth.Text);
            }
            catch
            {
                return false;
            }
            try
            {
                CartoTemplateClass.Height = double.Parse(this.txtHeight.Text);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.txtWidth = new TextBox();
            this.label12 = new Label();
            this.txtScale = new TextBox();
            this.label11 = new Label();
            this.txtHeight = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label5 = new Label();
            this.textBox3 = new TextBox();
            this.label6 = new Label();
            this.button1 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(14, 0x29);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "描述";
            this.textBox1.Location = new Point(0x3d, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xb9, 0x15);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.textBox2.Location = new Point(0x3d, 0x29);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0xb9, 0x53);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtWidth.Location = new Point(0x3d, 0x9d);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(140, 0x15);
            this.txtWidth.TabIndex = 0x4c;
            this.txtWidth.Text = "50";
            this.txtWidth.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(14, 160);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 12);
            this.label12.TabIndex = 0x4b;
            this.label12.Text = "宽";
            this.txtScale.Location = new Point(0x3d, 0x85);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(0xb9, 0x15);
            this.txtScale.TabIndex = 0x4a;
            this.txtScale.Text = "0";
            this.txtScale.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(14, 0x88);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x29, 12);
            this.label11.TabIndex = 0x49;
            this.label11.Text = "比例尺";
            this.txtHeight.Location = new Point(0x3d, 0xba);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(140, 0x15);
            this.txtHeight.TabIndex = 0x4e;
            this.txtHeight.Text = "50";
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 0xba);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 0x4d;
            this.label4.Text = "高";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xcf, 160);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x4f;
            this.label3.Text = "厘米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xcf, 0xbd);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 80;
            this.label5.Text = "厘米";
            this.textBox3.Location = new Point(0x3d, 0xd5);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(160, 0x15);
            this.textBox3.TabIndex = 0x52;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(10, 220);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 0x51;
            this.label6.Text = "图例";
            this.button1.Location = new Point(0xe3, 0xd5);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x22, 0x13);
            this.button1.TabIndex = 0x53;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtHeight);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.txtScale);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TemplatePropertyPage";
            base.Size = new Size(0x11c, 0x108);
            base.Load += new EventHandler(this.TemplatePropertyPage_Load);
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
        }

        private void TemplatePropertyPage_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = CartoTemplateClass.Name;
            this.textBox2.Text = CartoTemplateClass.Description;
            this.txtScale.Text = CartoTemplateClass.Scale.ToString();
            this.txtWidth.Text = CartoTemplateClass.Width.ToString();
            this.txtHeight.Text = CartoTemplateClass.Height.ToString();
            this.bool_0 = true;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                this.bool_1 = true;
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
                return "图幅";
            }
            set
            {
            }
        }
    }
}

