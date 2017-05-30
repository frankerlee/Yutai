using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterShapefiles : IGxObjectFilter
	{
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

		public MyGxFilterShapefiles()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0.Category == "Shapefile")
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
			}
			else if (!(igxObject_0 is IGxObjectContainer))
			{
				flag = false;
			}
			else
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
			}
			return flag;
		}

		public bool CanDisplayObject(IGxObject igxObject_0)
		{
			bool flag;
			if (!(igxObject_0 is IGxDataset))
			{
				if (!(igxObject_0 is IGxObjectContainer))
				{
					flag = false;
					return flag;
				}
				flag = true;
				return flag;
			}
			else
			{
				if (igxObject_0.Category != "Shapefile")
				{
					flag = false;
					return flag;
				}
				flag = true;
				return flag;
			}
			flag = false;
			return flag;
		}

		public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
		{
			bool flag;
			if ((igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				flag = false;
			}
			else
			{
				string str = string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0);
				if (Path.GetExtension(str).ToLower() != ".shp")
				{
					bool_0 = File.Exists(string.Concat(str, ".shp"));
				}
				else
				{
					bool_0 = File.Exists(str);
				}
				flag = true;
			}
			return flag;
		}
	}
}