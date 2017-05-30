using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyRasterFormatGridFilter : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "GRID";
			}
		}

		public string Name
		{
			get
			{
				return "RasterFormatGridFilter";
			}
		}

		public MyRasterFormatGridFilter()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxDataset)
			{
				if (((igxObject_0 as IGxDataset).Dataset as IRasterDataset).Format == "GRID")
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
			else if ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterDataset)
			{
				IName internalObjectName = igxObject_0.InternalObjectName;
				if (internalObjectName is IRasterDatasetName)
				{
					IRasterDataset rasterDataset = internalObjectName.Open() as IRasterDataset;
					if (rasterDataset == null || !(rasterDataset.Format == "GRID"))
					{
						flag = false;
						return flag;
					}
					flag = true;
					return flag;
				}
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
				bool_0 = Directory.Exists(string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0));
				flag = true;
			}
			return flag;
		}
	}
}