using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterTables : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "表";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterTables";
			}
		}

		public MyGxFilterTables()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxDataset)
			{
				if ((igxObject_0 as IGxDataset).DatasetName.Type == esriDatasetType.esriDTTable)
				{
					myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				}
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
				if ((igxObject_0 as IGxDataset).DatasetName.Type != esriDatasetType.esriDTTable)
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
			if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				string str = string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0);
				bool_0 = File.Exists(str);
				flag = true;
			}
			else if (!(igxObject_0 is IGxDatabase))
			{
				flag = false;
			}
			else
			{
				bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
				flag = true;
			}
			return flag;
		}
	}
}