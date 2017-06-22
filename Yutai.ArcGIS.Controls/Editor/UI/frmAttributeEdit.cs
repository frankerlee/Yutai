using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmAttributeEdit : Form
    {
        private AttributeControl m_pAttributeCtrl = new AttributeControl();

        public frmAttributeEdit()
        {
            this.InitializeComponent();
            this.m_pAttributeCtrl.Dock = DockStyle.Fill;
            base.Controls.Add(this.m_pAttributeCtrl);
        }

 public IWorkspace EditWorkspace
        {
            set
            {
                this.m_pAttributeCtrl.EditWorkspace = value;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pAttributeCtrl.FocusMap = value;
            }
        }
    }
}

