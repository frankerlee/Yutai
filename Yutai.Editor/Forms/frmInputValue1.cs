using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Editor.Forms
{
    internal class frmInputValue1 : XtraForm
    {
        private SimpleButton simpleButton_0;

        private TextEdit textEdit_0;

        private TextEdit textEdit_1;

        private System.ComponentModel.Container container_0 = null;

        private double double_0 = 0;

        private double double_1 = 0;

        public double InputValue1
        {
            get { return this.double_0; }
            set { this.double_0 = value; }
        }

        public double InputValue2
        {
            get { return this.double_1; }
            set { this.double_1 = value; }
        }

        public frmInputValue1()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && this.container_0 != null)
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmInputValue1_Load(object sender, EventArgs e)
        {
            this.textEdit_0.Text = this.double_0.ToString("0.####");
            this.textEdit_1.Text = this.double_1.ToString("0.####");
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmInputValue1));
            this.simpleButton_0 = new SimpleButton();
            this.textEdit_0 = new TextEdit();
            this.textEdit_1 = new TextEdit();
            ((ISupportInitialize) this.textEdit_0.Properties).BeginInit();
            ((ISupportInitialize) this.textEdit_1.Properties).BeginInit();
            base.SuspendLayout();
            this.simpleButton_0.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_0.Location = new Point(32, 16);
            this.simpleButton_0.Name = "simpleButton1";
            this.simpleButton_0.Size = new System.Drawing.Size(56, 24);
            this.simpleButton_0.TabIndex = 1;
            this.simpleButton_0.Text = "simpleButton1";
            this.simpleButton_0.Click += new EventHandler(this.simpleButton_0_Click);
            this.textEdit_0.EditValue = "0";
            this.textEdit_0.Location = new Point(0, 0);
            this.textEdit_0.Name = "textEdit1";
            this.textEdit_0.Size = new System.Drawing.Size(112, 21);
            this.textEdit_0.TabIndex = 2;
            this.textEdit_1.EditValue = "0";
            this.textEdit_1.Location = new Point(117, 0);
            this.textEdit_1.Name = "textEdit2";
            this.textEdit_1.Size = new System.Drawing.Size(112, 21);
            this.textEdit_1.TabIndex = 3;
            base.AcceptButton = this.simpleButton_0;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            base.ClientSize = new System.Drawing.Size(234, 23);
            base.Controls.Add(this.textEdit_1);
            base.Controls.Add(this.textEdit_0);
            base.Controls.Add(this.simpleButton_0);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmInputValue1";
            base.Load += new EventHandler(this.frmInputValue1_Load);
            ((ISupportInitialize) this.textEdit_0.Properties).EndInit();
            ((ISupportInitialize) this.textEdit_1.Properties).EndInit();
            base.ResumeLayout(false);
        }

        private void simpleButton_0_Click(object sender, EventArgs e)
        {
            if ((this.textEdit_0.Text.Trim().Length != 0 ? true : this.textEdit_1.Text.Trim().Length != 0))
            {
                try
                {
                    this.double_0 = double.Parse(this.textEdit_0.Text);
                    this.double_1 = double.Parse(this.textEdit_1.Text);
                    base.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch
                {
                    base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            else
            {
                base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
    }
}