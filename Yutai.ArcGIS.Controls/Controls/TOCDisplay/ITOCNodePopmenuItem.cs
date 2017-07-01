using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public interface ITOCNodePopmenuItem
    {
        IMapControl2 InMapCtrl { get; set; }

        bool IsShow { get; }

        IPageLayoutControl2 PageLayoutControl { get; set; }

        TocTreeViewBase TreeView { get; set; }
    }
}