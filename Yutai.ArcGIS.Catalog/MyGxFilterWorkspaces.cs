using System.IO;

namespace Yutai.ArcGIS.Catalog
{
    public class MyGxFilterWorkspaces : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder)) ||
                (igxObject_0 is IGxRemoteDatabaseFolder))
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
                return true;
            }
            if (igxObject_0 is IGxBasicObject)
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
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
            return (((((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder)) ||
                      (igxObject_0 is IGxRemoteDatabaseFolder)) || (igxObject_0 is IGxBasicObject)) ||
                    (igxObject_0 is IGxDatabase));
        }

        public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
        {
            if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
            {
                string path = (igxObject_0 as IGxFile).Path + @"\" + string_0;
                if (Path.GetExtension(path).ToLower() == ".mdb")
                {
                    bool_0 = File.Exists(path);
                }
                else
                {
                    bool_0 = Directory.Exists(path);
                }
                return true;
            }
            return (igxObject_0 is IGxDatabase);
        }

        public string Description
        {
            get { return "文件夹和Geodatabases"; }
        }

        public string Name
        {
            get { return "GxFilterWorkspaces"; }
        }
    }
}