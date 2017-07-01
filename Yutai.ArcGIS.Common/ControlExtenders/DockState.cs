using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DockState
    {
        public ScrollableControl Container;
        public Control Handle;
        public System.Windows.Forms.Splitter Splitter;
        public Control OrgDockingParent;
        public Control OrgDockHost;
        public DockStyle OrgDockStyle;
        public Rectangle OrgBounds;
    }
}