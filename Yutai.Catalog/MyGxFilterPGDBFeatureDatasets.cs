using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public class MyGxFilterPGDBFeatureDatasets : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "个人数据库要素集";
			}
		}

		public string Name
		{
			get
			{
				return "个人数据库要素集";
			}
		}

		public MyGxFilterPGDBFeatureDatasets()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
			}
			else if (igxObject_0 is IGxDatabase)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
			}
			else if (!(igxObject_0 is IGxDataset) || (igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTFeatureDataset)
			{
				flag = false;
			}
			else
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
			}
			return flag;
		}

		public bool CanDisplayObject(IGxObject igxObject_0)
		{
			bool flag;
			if ((igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				if (!(igxObject_0 is IGxDatabase))
				{
					if (!(igxObject_0 is IGxDataset) || (igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTFeatureDataset)
					{
						goto Label1;
					}
					flag = true;
					return flag;
				}
				else
				{
					if ((igxObject_0 as IGxDatabase).IsRemoteDatabase)
					{
						goto Label1;
					}
					flag = true;
					return flag;
				}
			Label1:
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
		{
			bool flag;
			if (!(igxObject_0 is IGxDatabase))
			{
				flag = false;
			}
			else
			{
				bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureDataset, string_0];
				flag = true;
			}
			return flag;
		}
	}
}