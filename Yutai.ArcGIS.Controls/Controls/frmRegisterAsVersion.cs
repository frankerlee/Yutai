using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class frmRegisterAsVersion : Form
    {
        private SimpleButton btnOK;
        private CheckEdit checkEdit1;
        private IContainer components = null;
        private bool edit_to_base = false;
        private SimpleButton simpleButton2;

        public frmRegisterAsVersion()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.edit_to_base = this.checkEdit1.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmRegisterAsVersion_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegisterAsVersion));
            this.checkEdit1 = new CheckEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.checkEdit1.Location = new Point(12, 12);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "是否注册为移动编辑的基版本";
            this.checkEdit1.Size = new Size(0xe2, 0x13);
            this.checkEdit1.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x29, 0x25);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0x91, 0x25);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x4b, 0x17);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x4a);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.checkEdit1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRegisterAsVersion";
            this.Text = "注册为版本";
            base.Load += new EventHandler(this.frmRegisterAsVersion_Load);
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public bool EditToBase
        {
            get
            {
                return this.edit_to_base;
            }
        }
    }
}

