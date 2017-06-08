using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmInput : Form
    {
        private Container container_0 = null;
        private Label label1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private string string_0 = "";
        private string string_1 = "";
        private TextEdit textEdit1;

        public frmInput(string string_2, string string_3)
        {
            this.InitializeComponent();
            this.string_1 = string_3;
            this.string_0 = string_2;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            this.textEdit1.Text = this.string_1;
            this.label1.Text = this.string_0;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInput));
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 0;
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(8, 40);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(0xe8, 0x15);
            this.textEdit1.TabIndex = 1;
            this.simpleButton1.DialogResult = DialogResult.OK;
            this.simpleButton1.Location = new Point(0x58, 80);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x40, 0x18);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(160, 80);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x40, 0x18);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x10a, 0x7f);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmInput";
            base.Load += new EventHandler(this.frmInput_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.string_1 = this.textEdit1.Text;
        }

        public string InputValue
        {
            get
            {
                return this.string_1;
            }
        }
    }
}

