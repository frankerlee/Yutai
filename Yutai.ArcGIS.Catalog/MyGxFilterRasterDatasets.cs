using System.IO;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterRasterDatasets : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                esriDatasetType type = (igxObject_0 as IGxDataset).DatasetName.Type;
                if (type != esriDatasetType.esriDTContainer)
                {
                    if (type != esriDatasetType.esriDTRasterDataset)
                    {
                        myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                    }
                    else
                    {
                        IDataset dataset = (igxObject_0 as IGxDataset).Dataset;
                        if (dataset != null)
                        {
                            IRasterBandCollection bands = dataset as IRasterBandCollection;
                            if (bands.Count == 1)
                            {
                                myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                            }
                            else
                            {
                                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                            }
                        }
                    }
                }
                else
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                }
                return true;
            }
            if (igxObject_0 is IGxObjectContainer)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                if ((((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterBand) || ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterDataset)) || ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterBand))
                {
                    return true;
                }
            }
            else if (igxObject_0 is IGxObjectContainer)
            {
                return true;
            }
            return false;
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            if ((igxObject_0 is IGxObjectContainer) || (igxObject_0 is IGxFolder))
            {
                if (Path.GetExtension(string_0).Length == 0)
                {
                    bool_0 = Directory.Exists(string_0);
                }
                else
                {
                    bool_0 = File.Exists(string_0);
                }
                return true;
            }
            return false;
        }

        public string Description
        {
            get
            {
                return "栅格数据集";
            }
        }

        public string Name
        {
            get
            {
                return "GxFilterRasterDatasets";
            }
        }
    }
}

