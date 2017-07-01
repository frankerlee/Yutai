using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public interface ILayerAndStandaloneTablePropertyPage
    {
        bool Apply();

        IBasicMap FocusMap { set; }

        bool IsPageDirty { get; }

        object SelectItem { set; }
    }
}