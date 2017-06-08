using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Framework
{
    public interface IDockManagerWrap
    {
        void DockWindows(object object_0, Form form_0, Bitmap bitmap_0);
        void HideDockWindow(IDockContent idockContent_0);

        object DockManager { set; }
    }
}

