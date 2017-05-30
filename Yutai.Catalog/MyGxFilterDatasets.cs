using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterDatasets : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "数据集";
			}
		}

		public string Name
		{
			get
			{
				return "Dataset";
			}
		}

		public MyGxFilterDatasets()
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
			else if (igxObject_0 is IGxVCTObject)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
			}
			else if (igxObject_0 is IGxLayer)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
			}
			else if (igxObject_0 is IGxObjectContainer)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
			}
			else if (!(igxObject_0 is IGxAGSMap))
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
			if (!(igxObject_0 is IGxDataset ? false : !(igxObject_0 is IGxLayer)))
			{
				flag = true;
			}
			else if (!(igxObject_0 is IGxAGSMap))
			{
				flag = (!(igxObject_0 is IGxObjectContainer) ? false : true);
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
			try
			{
				if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
				{
					string str = string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0);
					bool_0 = File.Exists(str);
					flag = true;
					return flag;
				}
				else if (igxObject_0 is IGxDatabase)
				{
					bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
					flag = true;
					return flag;
				}
				else if (igxObject_0 is IGxDataset && (igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
				{
					bool_0 = ((igxObject_0 as IGxDataset).Dataset.Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
					flag = true;
					return flag;
				}
			}
			catch
			{
			}
			flag = false;
			return flag;
		}
	}
}