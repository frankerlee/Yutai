using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterFeatureClasses : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "要素类";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterFeatureClasses";
			}
		}

		public MyGxFilterFeatureClasses()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (!(igxObject_0 is IGxDataset))
			{
				if (!(igxObject_0 is IGxObjectContainer))
				{
					flag = false;
					return flag;
				}
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
				return flag;
			}
			else
			{
				if ((igxObject_0 as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureClass)
				{
					flag = false;
					return flag;
				}
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
				return flag;
			}
			flag = false;
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
			}
			else
			{
				esriDatasetType type = (igxObject_0 as IGxDataset).Type;
				if (type == esriDatasetType.esriDTFeatureDataset)
				{
					flag = true;
				}
				else if (!(igxObject_0 is GxCadDrawing))
				{
					if ((type == esriDatasetType.esriDTFeatureClass ? false : type != esriDatasetType.esriDTCadDrawing))
					{
						flag = false;
						return flag;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
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