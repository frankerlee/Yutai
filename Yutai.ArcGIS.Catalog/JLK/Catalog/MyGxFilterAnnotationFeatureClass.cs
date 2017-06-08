namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using System;

    public class MyGxFilterAnnotationFeatureClass : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxObjectContainer)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            IName internalObjectName = igxObject_0.InternalObjectName;
            if ((internalObjectName is IFeatureClassName) && ((internalObjectName as IFeatureClassName).FeatureType == esriFeatureType.esriFTAnnotation))
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                return true;
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            if (igxObject_0 is IGxDataset)
            {
                IName internalObjectName = igxObject_0.InternalObjectName;
                if ((internalObjectName is IFeatureClassName) && ((internalObjectName as IFeatureClassName).FeatureType == esriFeatureType.esriFTAnnotation))
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
            if (igxObject_0 is IGxDatabase)
            {
                bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, string_0);
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
                return "注记类";
            }
        }

        public string Name
        {
            get
            {
                return "GxFilterAnnotationFeatureClass";
            }
        }
    }
}

