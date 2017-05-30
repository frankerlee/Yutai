using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public class MyGxFilterFeatureDatasets : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "要素集";
			}
		}

		public string Name
		{
			get
			{
				return "要素集";
			}
		}

		public MyGxFilterFeatureDatasets()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxDataset)
			{
				if ((igxObject_0 as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureDataset)
				{
					flag = false;
					return flag;
				}
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
				return flag;
			}
			else if (!(igxObject_0 is IGxObjectContainer))
			{
				if (!(igxObject_0 is IGxBasicObject))
				{
					flag = false;
					return flag;
				}
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
				flag = true;
				return flag;
			}
			else
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
				return flag;
			}
			flag = false;
			return flag;
		}

		public bool CanDisplayObject(IGxObject igxObject_0)
		{
			bool flag;
			if (igxObject_0 is IGxDataset)
			{
				if ((igxObject_0 as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureDataset)
				{
					flag = false;
					return flag;
				}
				flag = true;
				return flag;
			}
			else if (!(igxObject_0 is IGxObjectContainer))
			{
				if (!(igxObject_0 is IGxBasicObject))
				{
					flag = false;
					return flag;
				}
				flag = true;
				return flag;
			}
			else
			{
				flag = true;
				return flag;
			}
			flag = false;
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
				bool_0 = this.method_0((igxObject_0 as IGxDatabase).Workspace, string_0.ToLower());
				flag = true;
			}
			return flag;
		}

		private bool method_0(IWorkspace iworkspace_0, string string_0)
		{
			bool flag;
			IEnumDatasetName datasetNames = iworkspace_0.DatasetNames[esriDatasetType.esriDTFeatureDataset];
			datasetNames.Reset();
			IDatasetName datasetName = datasetNames.Next();
			while (true)
			{
				if (datasetName != null)
				{
					string name = datasetName.Name;
					string[] strArrays = name.Split(new char[] { '.' });
					if (string_0 == strArrays[(int)strArrays.Length - 1].ToLower())
					{
						flag = true;
						break;
					}
					else
					{
						datasetName = datasetNames.Next();
					}
				}
				else
				{
					flag = false;
					break;
				}
			}
			return flag;
		}
	}
}