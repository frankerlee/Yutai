using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class MyRasterFormatGridFilter : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                IRasterDataset dataset = (igxObject_0 as IGxDataset).Dataset as IRasterDataset;
                if (dataset.Format == "GRID")
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
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
                if ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterDataset)
                {
                    IName internalObjectName = igxObject_0.InternalObjectName;
                    if (internalObjectName is IRasterDatasetName)
                    {
                        IRasterDataset dataset = internalObjectName.Open() as IRasterDataset;
                        if ((dataset != null) && (dataset.Format == "GRID"))
                        {
                            return true;
                        }
                    }
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
            if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
            {
                bool_0 = Directory.Exists((igxObject_0 as IGxFile).Path + @"\" + string_0);
                return true;
            }
            return false;
        }

        public string Description
        {
            get { return "GRID"; }
        }

        public string Name
        {
            get { return "RasterFormatGridFilter"; }
        }
    }
}