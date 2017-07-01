using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls.TOCTreeview;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmTOCTreeView2 : DockContent
    {
        internal TOCControl tocTreeView1;

        public frmTOCTreeView2()
        {
            this.InitializeComponent();
        }

        public IApplication Application
        {
            set { this.tocTreeView1.Application = value; }
        }
    }
}