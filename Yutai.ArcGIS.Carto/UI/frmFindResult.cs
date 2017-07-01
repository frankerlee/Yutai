using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmFindResult : Form
    {
        private IContainer icontainer_0 = null;

        public frmFindResult()
        {
            this.InitializeComponent();
        }

        private void frmFindResult_Closing(object sender, CancelEventArgs e)
        {
        }

        public IActiveView ActiveView
        {
            set { this.findResultControl1.ActiveView = value; }
        }

        public IArray FindResults
        {
            set { this.findResultControl1.FindResults = value; }
        }
    }
}