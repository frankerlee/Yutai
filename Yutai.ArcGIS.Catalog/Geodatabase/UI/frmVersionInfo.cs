using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmVersionInfo : Form
    {
        private Container container_0 = null;
        private VersionInfoControl versionInfoControl_0 = new VersionInfoControl();

        public frmVersionInfo()
        {
            this.InitializeComponent();
            this.versionInfoControl_0.Dock = DockStyle.Fill;
            base.Controls.Add(this.versionInfoControl_0);
        }

 public IVersionedWorkspace VersionWorkspace
        {
            set
            {
                this.versionInfoControl_0.VersionWorkspace = value;
            }
        }
    }
}

