using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    internal sealed partial class Overlay : Form
    {
        public DockStyle Dock;
        public Control DockHostControl;
        private IContainer icontainer_0 = null;

        public Overlay()
        {
            this.InitializeComponent();
        }
    }
}