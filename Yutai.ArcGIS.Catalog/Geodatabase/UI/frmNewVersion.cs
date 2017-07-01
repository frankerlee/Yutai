using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewVersion : Form
    {
        private Container container_0 = null;
        private NewVersionControl newVersionControl_0 = new NewVersionControl();

        public frmNewVersion()
        {
            this.InitializeComponent();
            this.panel1.Controls.Add(this.newVersionControl_0);
            this.newVersionControl_0.Dock = DockStyle.Fill;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.newVersionControl_0.CreateVersion();
            base.Close();
        }

        private void frmNewVersion_Load(object sender, EventArgs e)
        {
        }

        public IArray ParentVersions
        {
            set { this.newVersionControl_0.ParentVersions = value; }
        }
    }
}