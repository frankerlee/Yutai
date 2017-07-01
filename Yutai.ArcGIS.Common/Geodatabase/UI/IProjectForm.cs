using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase.UI
{
    internal interface IProjectForm
    {
        IFeatureDataConverter FeatureProgress { set; }
    }
}