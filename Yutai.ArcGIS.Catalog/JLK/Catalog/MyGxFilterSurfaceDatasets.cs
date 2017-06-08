namespace JLK.Catalog
{
    using ESRI.ArcGIS.DataSourcesRaster;
    using ESRI.ArcGIS.Geodatabase;
    using System;

    public class MyGxFilterSurfaceDatasets : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (!(igxObject_0 is IGxDataset))
            {
                if (igxObject_0 is IGxObjectContainer)
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                    return true;
                }
                return false;
            }
            switch ((igxObject_0 as IGxDataset).DatasetName.Type)
            {
                case esriDatasetType.esriDTRasterDataset:
                {
                    IRasterBandCollection dataset = (igxObject_0 as IGxDataset).Dataset as IRasterBandCollection;
                    if (dataset.Count != 1)
                    {
                        myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                    }
                    else
                    {
                        myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                    }
                    break;
                }
                case esriDatasetType.esriDTTin:
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                    break;

                case esriDatasetType.esriDTContainer:
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                    break;

                default:
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                    break;
            }
            return true;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                if (((((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterBand) || ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterDataset)) || ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterBand)) || ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTTin))
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
            return (igxObject_0 is IGxObjectContainer);
        }

        public string Description
        {
            get
            {
                return "表面数据集";
            }
        }

        public string Name
        {
            get
            {
                return "GxFilterSurfaceDatasets";
            }
        }
    }
}

