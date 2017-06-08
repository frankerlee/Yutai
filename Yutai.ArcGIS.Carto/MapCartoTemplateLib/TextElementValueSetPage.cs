using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class TextElementValueSetPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button btnExpress;
        private CheckBox checkBox1;
        private IContainer icontainer_0 = null;
        private Label label1;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private TextBox textBox1;

        public event OnValueChangeEventHandler OnValueChange;

        public TextElementValueSetPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                (this.mapTemplateElement_0 as MapTemplateTextElement).Text = this.textBox1.Text;
                (this.mapTemplateElement_0 as MapTemplateTextElement).IsVerticalText = this.checkBox1.Checked;
            }
        }

        private void btnExpress_Click(object sender, EventArgs e)
        {
            frmExpressBulider bulider2 = new frmExpressBulider {
                MapTemplate = this.mapTemplateElement_0.MapTemplate,
                Expression = this.textBox1.Text
            };
            if (bulider2.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = bulider2.Expression;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
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
            this.textBox1 = new TextBox();
            this.checkBox1 = new CheckBox();
            this.btnExpress = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本值";
            this.textBox1.Location = new Point(15, 0x19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xed, 60);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x11, 0x5b);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x48, 0x10);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "竖向文本";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.btnExpress.Location = new Point(0xb1, 0x57);
            this.btnExpress.Name = "btnExpress";
            this.btnExpress.Size = new Size(0x4b, 0x17);
            this.btnExpress.TabIndex = 6;
            this.btnExpress.Text = "表达式";
            this.btnExpress.UseVisualStyleBackColor = true;
            this.btnExpress.Click += new EventHandler(this.btnExpress_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnExpress);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "TextElementValueSetPage";
            base.Size = new Size(0x123, 0x8b);
            base.Load += new EventHandler(this.TextElementValueSetPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (this.mapTemplateElement_0 is MapTemplateTextElement)
            {
                this.bool_0 = false;
                this.textBox1.Text = (this.mapTemplateElement_0 as MapTemplateTextElement).Text;
                this.checkBox1.Checked = (this.mapTemplateElement_0 as MapTemplateTextElement).IsVerticalText;
                this.bool_0 = true;
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplateElement = object_0 as MapCartoTemplateLib.MapTemplateElement;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void TextElementValueSetPage_Load(object sender, EventArgs e)
        {
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.method_0();
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_0();
            }
        }

        public string Title
        {
            get
            {
                return "文本";
            }
            set
            {
            }
        }
    }
}

