namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmGDBInfo : Form
    {
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private DomainControl domainControl_0 = new DomainControl();
        private GDBGeneralCtrl gdbgeneralCtrl_0 = new GDBGeneralCtrl();
        private IWorkspace iworkspace_0 = null;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;

        public frmGDBInfo()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.domainControl_0.Apply();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.domainControl_0.Apply();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.domainControl_0.Dispose();
                this.gdbgeneralCtrl_0.Dispose();
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmGDBInfo_Load(object sender, EventArgs e)
        {
            this.gdbgeneralCtrl_0.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(this.gdbgeneralCtrl_0);
            this.domainControl_0.Dock = DockStyle.Fill;
            this.tabPage2.Controls.Add(this.domainControl_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmGDBInfo));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x178, 480);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(0x170, 0x1c7);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new Size(0x170, 0x1c7);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "域";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x90, 0x1f8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x48, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe8, 0x1f8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnApply.Location = new Point(0x138, 0x1f8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x40, 0x18);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x188, 530);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmGDBInfo";
            this.Text = "数据库属性";
            base.Load += new EventHandler(this.frmGDBInfo_Load);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
                this.gdbgeneralCtrl_0.Workspace = value;
                this.domainControl_0.WorkspaceDomains = value as IWorkspaceDomains;
            }
        }
    }
}

