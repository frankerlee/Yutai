using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    internal interface IGxFilterDataElements
    {
        IGPDomain ChooseDomain { get; set; }

        string Description { set; }

        IGPDomain DisplayDomain { get; set; }

        string Name { set; }
    }
}

