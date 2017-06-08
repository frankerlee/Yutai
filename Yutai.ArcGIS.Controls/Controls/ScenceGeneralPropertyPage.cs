using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class ScenceGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private Button button1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IScene m_Scence = null;
        private string m_Title = "常规";
        private TextBox textBox1;

        public event OnValueChangeEventHandler OnValueChange;

        public ScenceGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_Scence.Description = this.textBox1.Text;
                try
                {
                    this.m_Scence.ExaggerationFactor = double.Parse(this.comboBox1.Text);
                }
                catch
                {
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double num2;
            double aspectRatio = 0.12;
            try
            {
                aspectRatio = this.m_Scence.Extent.Width / this.m_Scence.Extent.Height;
            }
            catch
            {
            }
            this.m_Scence.SuggestExaggerationFactor(aspectRatio, out num2);
            this.comboBox1.Text = num2.ToString("0.##");
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.m_IsPageDirty = true;
        }

        public void Cancel()
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.comboBox1 = new ComboBox();
            this.button1 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "描述";
            this.textBox1.Location = new Point(5, 0x13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x112, 0x75);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 0x93);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "竖直方向夸大因子";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "0", "0.1", "0.5", "1", "1.5", "2", "5", "10" });
            this.comboBox1.Location = new Point(110, 0x90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x41, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "1";
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.button1.Location = new Point(0xb5, 0x8e);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x62, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "根据场景计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "ScenceGeneralPropertyPage";
            base.Size = new Size(0x123, 0xe5);
            base.Load += new EventHandler(this.ScenceGeneralPropertyPage_Load);
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

        private void ScenceGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.m_Scence.Description;
            this.comboBox1.Text = this.m_Scence.ExaggerationFactor.ToString();
            this.m_CanDo = true;
        }

        public void SetObjects(object @object)
        {
            this.m_Scence = @object as IScene;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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

        public IScene Scence
        {
            set
            {
                this.m_Scence = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

