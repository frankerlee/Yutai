using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.Library
{
    public interface IMxDocument
    {
        IActiveView ActiveView { get; set; }

        IMap FocusMap { get; set; }

        IMapAndPageLayoutCtrlForm MapAndPageLayoutCtrlForm { get; set; }

        IPageLayout PageLayout { get; set; }
    }
}

