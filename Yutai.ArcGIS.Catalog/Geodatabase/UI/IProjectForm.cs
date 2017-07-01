using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal interface IProjectForm
    {
        IFeatureDataConverter FeatureProgress { set; }
    }
}