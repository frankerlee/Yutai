using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public class MyGxFilterSurfaceDatasets : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "表面数据集";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterSurfaceDatasets";
			}
		}

		public MyGxFilterSurfaceDatasets()
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
				else
				{
					switch (type)
					{
						case esriDatasetType.esriDTRasterDataset:
						{
							if (((igxObject_0 as IGxDataset).Dataset as IRasterBandCollection).Count != 1)
							{
								myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
								break;
							}
							else
							{
								myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
								break;
							}
						}
						case esriDatasetType.esriDTRasterBand:
						{
							myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
							break;
						}
						case esriDatasetType.esriDTTin:
						{
							myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
							break;
						}
						default:
						{
							goto case esriDatasetType.esriDTRasterBand;
						}
					}
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
				if (((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterBand || (igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterDataset || (igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTRasterBand ? false : (igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTTin))
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
			return (!(igxObject_0 is IGxObjectContainer) ? false : true);
		}
	}
}