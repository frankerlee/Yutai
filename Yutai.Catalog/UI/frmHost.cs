namespace Yutai.Catalog.UI
{
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmHost : Form
    {
        private Button btnOK;
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private Button Button2;
        private string string_0 = "";
        private string string_1 = "";
        private TextBox txtDescription;
        private Button txtHost;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "描述";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(72, 16);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(144, 21);
            this.txtHost.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(16, 80);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(208, 112);
            this.txtDescription.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(88, 216);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Button2
            // 
            this.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button2.Location = new System.Drawing.Point(160, 216);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(56, 24);
            this.Button2.TabIndex = 5;
            this.Button2.Text = "取消";
            // 
            // frmHost
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(234, 252);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHost";
            this.Text = "添加机器";
            this.Load += new System.EventHandler(this.frmHost_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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

