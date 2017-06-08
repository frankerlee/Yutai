namespace JLK.Catalog.UI
{
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmHost : Form
    {
        private SimpleButton btnOK;
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private SimpleButton simpleButton2;
        private string string_0 = "";
        private string string_1 = "";
        private MemoEdit txtDescription;
        private TextEdit txtHost;

        public frmHost()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtHost.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入主机名!");
            }
            else
            {
                this.string_0 = this.txtHost.Text.Trim();
                this.string_1 = this.txtDescription.Text;
                base.DialogResult = DialogResult.OK;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmHost_Load(object sender, EventArgs e)
        {
            if (!(this.string_0 == ""))
            {
                this.txtHost.Text = this.string_0;
                this.txtHost.Enabled = false;
            }
            this.txtDescription.Text = this.string_1;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmHost));
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtHost = new TextEdit();
            this.txtDescription = new MemoEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtHost.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器名:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "描述";
            this.txtHost.EditValue = "";
            this.txtHost.Location = new Point(0x48, 0x10);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new Size(0x90, 0x15);
            this.txtHost.TabIndex = 2;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x10, 80);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0xd0, 0x70);
            this.txtDescription.TabIndex = 3;
            this.btnOK.Location = new Point(0x58, 0xd8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(160, 0xd8);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xea, 0xfc);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtHost);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHost";
            this.Text = "添加机器";
            base.Load += new EventHandler(this.frmHost_Load);
            this.txtHost.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public string Description
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string HostName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

