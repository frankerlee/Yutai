using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public class MyGxFilterRasterCatalogDatasets : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "栅格目录";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterRasterCatalogDatasets";
			}
		}

		public MyGxFilterRasterCatalogDatasets()
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
			else if (!(igxObject_0 is IGxDataset) || (igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTRasterCatalog)
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
			if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				flag = true;
			}
			else if (!(igxObject_0 is IGxDatabase))
			{
				flag = (!(igxObject_0 is IGxDataset) || (igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTRasterCatalog ? false : true);
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
			if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				flag = false;
			}
			else if (!(igxObject_0 is IGxDatabase))
			{
				flag = false;
			}
			else
			{
				bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTRasterCatalog, string_0];
				flag = true;
			}
			return flag;
		}
	}
}