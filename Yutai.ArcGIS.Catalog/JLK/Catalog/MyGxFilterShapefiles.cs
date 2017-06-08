namespace JLK.Catalog
{
    using System;
    using System.IO;

    public class MyGxFilterShapefiles : IGxObjectFilter
    {
        public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
        {
            if (igxObject_0.Category == "Shapefile")
            {
                myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
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
                if (igxObject_0.Category == "Shapefile")
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
            if ((igxObject_0 is IGxDiskConnection) || (igxObject_0 is IGxFolder))
            {
                string path = (igxObject_0 as IGxFile).Path + @"\" + string_0;
                if (Path.GetExtension(path).ToLower() == ".shp")
                {
                    bool_0 = File.Exists(path);
                }
                else
                {
                    bool_0 = File.Exists(path + ".shp");
                }
                return true;
            }
            return false;
        }

        public string Description
        {
            get
            {
                return "Shapefile";
            }
        }

        public string Name
        {
            get
            {
                return "Shapefiles";
            }
        }
    }
}

