using System.IO;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterFeatureDatasetsAndFeatureClasses : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                if (((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset) || ((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureClass))
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                    return true;
                }
            }
            else
            {
                if (igxObject_0 is IGxObjectContainer)
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                    return true;
                }
                if (igxObject_0 is IGxBasicObject)
                {
                    myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
                    return true;
                }
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                if (((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset) || ((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureClass))
                {
                    return true;
                }
            }
            else
            {
                if (igxObject_0 is IGxObjectContainer)
                {
                    return true;
                }
                if (igxObject_0 is IGxBasicObject)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
            {
                string path = (igxObject_0 as IGxFile).Path + @"\" + string_0;
                bool_0 = File.Exists(path);
                return true;
            }
            if (igxObject_0 is IGxDatabase)
            {
                bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureDataset, string_0);
                return true;
            }
            if ((igxObject_0 is IGxDataset) && ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset))
            {
                bool_0 = ((igxObject_0 as IGxDataset).Dataset.Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, string_0);
                return true;
            }
            return false;
        }

        public string Description
        {
            get
            {
                return "要素集和要素类";
            }
        }

        public string Name
        {
            get
            {
                return "GxFilterFeatureDatasetsAndFeatureClasses";
            }
        }
    }
}

