using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmAttributeEdit : Form
    {
        private Container components = null;
        private AttributeControl m_pAttributeCtrl = new AttributeControl();

        public frmAttributeEdit()
        {
            this.InitializeComponent();
            this.m_pAttributeCtrl.Dock = DockStyle.Fill;
            base.Controls.Add(this.m_pAttributeCtrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeEdit));
            base.SuspendLayout();
            base.ClientSize = new Size(0x124, 0x111);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmAttributeEdit";
            base.ResumeLayout(false);
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

