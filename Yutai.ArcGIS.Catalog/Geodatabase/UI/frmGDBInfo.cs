using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmGDBInfo : Form
    {
        private Container container_0 = null;
        private DomainControl domainControl_0 = new DomainControl();
        private GDBGeneralCtrl gdbgeneralCtrl_0 = new GDBGeneralCtrl();
        private IWorkspace iworkspace_0 = null;

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

 private void frmGDBInfo_Load(object sender, EventArgs e)
        {
            this.gdbgeneralCtrl_0.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(this.gdbgeneralCtrl_0);
            this.domainControl_0.Dock = DockStyle.Fill;
            this.tabPage2.Controls.Add(this.domainControl_0);
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

