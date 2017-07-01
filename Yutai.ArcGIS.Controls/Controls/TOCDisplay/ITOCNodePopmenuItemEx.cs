using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public interface ITOCNodePopmenuItemEx
    {
        IMapControl2 InMapCtrl { get; set; }

        bool IsShow { get; }

        IPageLayoutControl2 PageLayoutControl { get; set; }

        TreeViewWrapBase TreeBase { get; set; }

        TOCTreeView TreeView { get; set; }
    }
}