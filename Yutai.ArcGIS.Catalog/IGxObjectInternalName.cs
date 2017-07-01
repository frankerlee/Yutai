using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxObjectInternalName
    {
        IName InternalObjectName { get; set; }
    }
}