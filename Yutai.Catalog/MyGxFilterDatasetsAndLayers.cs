using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterDatasetsAndLayers : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "数据集和图层文件";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterDatasetsAndLayers";
			}
		}

		public MyGxFilterDatasetsAndLayers()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxDataset)
			{
				esriDatasetType type = (igxObject_0 as IGxDataset).DatasetName.Type;
				switch (type)
				{
					case esriDatasetType.esriDTContainer:
					{
						myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
						break;
					}
					case esriDatasetType.esriDTGeo:
					{
						myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
						break;
					}
					case esriDatasetType.esriDTFeatureDataset:
					{
						myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
						break;
					}
					default:
					{
						if (type == esriDatasetType.esriDTCadDrawing)
						{
							goto case esriDatasetType.esriDTFeatureDataset;
						}
						goto case esriDatasetType.esriDTGeo;
					}
				}
				flag = true;
			}
			else if (igxObject_0 is IGxLayer)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
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
			return ((igxObject_0 is IGxDataset || igxObject_0 is IGxObjectContainer ? false : !(igxObject_0 is IGxLayer)) ? false : true);
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
			else if (igxObject_0 is IGxDatabase)
			{
				bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
				flag = true;
			}
			else if (!(igxObject_0 is IGxDataset) || (igxObject_0 as IGxDataset).Type != esriDatasetType.esriDTFeatureDataset)
			{
				flag = false;
			}
			else
			{
				bool_0 = ((igxObject_0 as IGxDataset).Dataset.Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
				flag = true;
			}
			return flag;
		}
	}
}