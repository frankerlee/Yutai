using System.Drawing;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{
    public interface IDockPanelView
    {
        string DockName { get;  }
        Bitmap Image { get; }
        string Caption { get; set; }
        Size DefaultSize { get; }
        DockPanelState DefaultDock { get; }

        string DefaultNestDockName { get; }
    }
}