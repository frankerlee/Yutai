using System.Drawing;

namespace Yutai.ArcGIS.Framework.Docking
{
    public interface INestedPanesContainer
    {
        Rectangle DisplayingRectangle { get; }

        DockState DockState { get; }

        bool IsFloat { get; }

        NestedPaneCollection NestedPanes { get; }

        VisibleNestedPaneCollection VisibleNestedPanes { get; }
    }
}

