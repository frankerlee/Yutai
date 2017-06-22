using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmVersionProperty : Form
    {
        private Container container_0 = null;
        private VersionPropertyCtrl versionPropertyCtrl_0 = new VersionPropertyCtrl();

        public frmVersionProperty()
        {
            this.InitializeComponent();
            base.StartPosition = FormStartPosition.CenterParent;
            this.panel1.Controls.Add(this.versionPropertyCtrl_0);
            this.versionPropertyCtrl_0.Dock = DockStyle.Fill;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.versionPropertyCtrl_0.Apply();
            base.Close();
        }

 public IVersion Version
        {
            set
            {
                this.versionPropertyCtrl_0.Version = value;
            }
        }
    }
}

