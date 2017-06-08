using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    internal class frmFolderProperty : Form
    {
        private Container components = null;
        private Label label1;
        private string m_FolderName = "";
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;

        public frmFolderProperty()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmFolderProperty_Load(object sender, EventArgs e)
        {
            this.textEdit1.Text = this.m_FolderName;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(0x38, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(0x90, 0x17);
            this.textEdit1.TabIndex = 1;
            this.simpleButton1.DialogResult = DialogResult.OK;
            this.simpleButton1.Location = new Point(0x40, 0x30);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x38, 0x18);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0x90, 0x30);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xe0, 0x4f);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFolderProperty";
            this.Text = "文件夹";
            base.Load += new EventHandler(this.frmFolderProperty_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.m_FolderName = this.textEdit1.Text;
        }

        public string FolderName
        {
            get
            {
                return this.m_FolderName;
            }
            set
            {
                this.m_FolderName = value;
            }
        }
    }
}

