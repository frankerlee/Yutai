using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmConflictInfo : Form
    {
        private ConflictInfoControl conflictInfoControl_0 = new ConflictInfoControl();
        private Container container_0 = null;

        public frmConflictInfo()
        {
            this.InitializeComponent();
            this.conflictInfoControl_0.Dock = DockStyle.Fill;
            base.Controls.Add(this.conflictInfoControl_0);
        }

        public IWorkspace EditWorkspace
        {
            set { this.conflictInfoControl_0.EditWorkspace = value; }
        }

        public IMap FocusMap
        {
            set { this.conflictInfoControl_0.FocusMap = value; }
        }
    }
}