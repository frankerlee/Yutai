using System.Drawing;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal interface ISplitterDragSource : IDragSource
    {
        void BeginDrag(Rectangle rectSplitter);
        void EndDrag();
        void MoveSplitter(int offset);

        Rectangle DragLimitBounds { get; }

        bool IsVertical { get; }
    }
}

