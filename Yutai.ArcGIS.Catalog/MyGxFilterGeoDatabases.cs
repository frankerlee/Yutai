using System.IO;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterGeoDatabases : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
                return true;
            }
            if (igxObject_0 is IGxDatabase)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                return true;
            }
            return false;
        }

        public bool CanDisplayObject(IGxObject igxObject_0)
        {
            return (((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder)) || (igxObject_0 is IGxDatabase));
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
            {
                string path = (igxObject_0 as IGxFile).Path + @"\" + string_0;
                bool_0 = File.Exists(path);
                return true;
            }
            return false;
        }

        public string Description
        {
            get
            {
                return "空间数据库";
            }
        }

        public string Name
        {
            get
            {
                return "空间数据库";
            }
        }
    }
}

