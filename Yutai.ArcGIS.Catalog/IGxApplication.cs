using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxApplication : IApplication
    {
        IGxCatalog GxCatalog { get; set; }

        IGxSelection GxSelection { get; set; }
    }
}