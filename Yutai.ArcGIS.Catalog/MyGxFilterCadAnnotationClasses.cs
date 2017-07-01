using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterCadAnnotationClasses : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxObjectContainer)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            IName internalObjectName = igxObject_0.InternalObjectName;
            if ((internalObjectName is IFeatureClassName) &&
                ((internalObjectName as IFeatureClassName).FeatureType == esriFeatureType.esriFTCoverageAnnotation))
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
                if ((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTCadDrawing)
                {
                    return true;
                }
                if (igxObject_0.Category == "CAD注记要素类")
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
            return false;
        }

        public string Description
        {
            get { return "CAD注记类"; }
        }

        public string Name
        {
            get { return "GxFilterCadAnnotationClasses"; }
        }
    }
}