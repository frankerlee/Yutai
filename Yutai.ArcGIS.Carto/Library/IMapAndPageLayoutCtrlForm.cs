using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Carto.Library
{
    public interface IMapAndPageLayoutCtrlForm
    {
        IActiveView ActiveView { get; set; }

        IMap FocusMap { get; set; }

        IMapControl3 MapControl { get; }

        IPageLayout PageLayout { get; set; }

        IPageLayoutControl2 PageLayoutControl { get; }
    }
}

