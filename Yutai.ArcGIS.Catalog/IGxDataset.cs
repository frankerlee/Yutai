using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxDataset
    {
        IDataset Dataset { get; }

        IDatasetName DatasetName { get; set; }

        esriDatasetType Type { get; }
    }
}