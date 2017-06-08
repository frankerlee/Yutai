namespace JLK.Catalog
{
    using ESRI.ArcGIS.Geodatabase;
    using System;

    public interface IGxDataset
    {
        IDataset Dataset { get; }

        IDatasetName DatasetName { get; set; }

        esriDatasetType Type { get; }
    }
}

