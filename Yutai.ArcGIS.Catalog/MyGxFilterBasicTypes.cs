namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterBasicTypes : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0 is IGxCatalog)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            if (igxObject_0 is IGxBasicObject)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
                return true;
            }
            if (igxObject_0 is IGxDiskConnection)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            if (igxObject_0 is IGxFolder)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            return ((((igxObject_0 is IGxCatalog) || (igxObject_0 is IGxBasicObject)) ||
                     (igxObject_0 is IGxDiskConnection)) || (igxObject_0 is IGxFolder));
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            return false;
        }

        public string Description
        {
            get { return "基本类型"; }
        }

        public string Name
        {
            get { return "GxFilterBasicTypes"; }
        }
    }
}