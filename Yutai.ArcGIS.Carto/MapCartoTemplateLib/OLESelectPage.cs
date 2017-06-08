using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class OLESelectPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private Button btnSelectPicture;
        private IContainer icontainer_0 = null;
        private Label label1;
        private TextBox textBox1;

        public event OnValueChangeEventHandler OnValueChange;

        public OLESelectPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
        }

        private void btnSelectPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "所有支持格式|*.bmp;*.jpg;*.gif;*.emf;*.tif;*.png|位图文件|*.bmp|JPEG|*.jpg|TIFF|*.tif|EMF|*.emf|PNG|*.png|GIF|*.gif"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.FileName;
            }
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.btnSelectPicture = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图片文件";
            this.textBox1.Location = new Point(0x3f, 0x12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x121, 0x15);
            this.textBox1.TabIndex = 1;
            this.btnSelectPicture.Location = new Point(0x115, 0x2d);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(0x4b, 0x17);
            this.btnSelectPicture.TabIndex = 2;
            this.btnSelectPicture.Text = "选择图片";
            this.btnSelectPicture.UseVisualStyleBackColor = true;
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Name = "PictureSelectPage";
            base.Size = new Size(0x177, 0xc0);
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

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
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
                return "图片";
            }
            set
            {
            }
        }
    }
}

