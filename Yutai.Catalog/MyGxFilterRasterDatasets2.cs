using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterRasterDatasets2 : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "栅格数据集";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterRasterDatasets2";
			}
		}

		public MyGxFilterRasterDatasets2()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxDataset)
			{
				esriDatasetType type = (igxObject_0 as IGxDataset).DatasetName.Type;
				if (type == esriDatasetType.esriDTContainer)
				{
					myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				}
				else if (type != esriDatasetType.esriDTRasterDataset)
				{
					myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				}
				else if ((igxObject_0 as IGxDataset).Dataset != null)
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
				if ((igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTRasterDataset)
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
			if ((igxObject_0 is IGxObjectContainer ? false : !(igxObject_0 is IGxFolder)))
			{
				flag = false;
			}
			else
			{
				if (Path.GetExtension(string_0).Length != 0)
				{
					bool_0 = File.Exists(string_0);
				}
				else
				{
					bool_0 = Directory.Exists(string_0);
				}
				flag = true;
			}
			return flag;
		}
	}
}