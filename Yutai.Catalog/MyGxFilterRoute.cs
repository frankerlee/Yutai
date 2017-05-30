using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterRoute : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "线要素包含路径";
			}
		}

		public string Name
		{
			get
			{
				return "MyGxFilterRoute";
			}
		}

		public MyGxFilterRoute()
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
			else if (!(igxObject_0 is IGxDatabase))
			{
				if (igxObject_0 is IGxDataset)
				{
					switch ((igxObject_0 as IGxDataset).Type)
					{
						case esriDatasetType.esriDTFeatureDataset:
						{
							myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
							flag = true;
							return flag;
						}
						case esriDatasetType.esriDTFeatureClass:
						{
							myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
							flag = true;
							return flag;
						}
					}
				}
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
			bool hasM;
			if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				hasM = true;
			}
			else if (!(igxObject_0 is IGxDatabase))
			{
				if (igxObject_0 is IGxDataset)
				{
					switch ((igxObject_0 as IGxDataset).Type)
					{
						case esriDatasetType.esriDTFeatureDataset:
						{
							hasM = true;
							return hasM;
						}
						case esriDatasetType.esriDTFeatureClass:
						{
							IFeatureClass dataset = (igxObject_0 as IGxDataset).Dataset as IFeatureClass;
							if (dataset.ShapeType != esriGeometryType.esriGeometryPolyline)
							{
								dataset = null;
								break;
							}
							else
							{
								int num = dataset.FindField(dataset.ShapeFieldName);
								IField field = dataset.Fields.Field[num];
								dataset = null;
								hasM = field.GeometryDef.HasM;
								return hasM;
							}
						}
					}
				}
				hasM = false;
			}
			else
			{
				hasM = true;
			}
			return hasM;
		}

		public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
		{
			bool flag;
			try
			{
				if (!(igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
				{
					string str = string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0);
					if (System.IO.Path.GetExtension(str).ToLower() != ".shp")
					{
						bool_0 = File.Exists(string.Concat(str, ".shp"));
					}
					else
					{
						bool_0 = File.Exists(str);
					}
					flag = true;
					return flag;
				}
				else if (igxObject_0 is IGxDatabase)
				{
					bool_0 = ((igxObject_0 as IGxDatabase).Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
					flag = true;
					return flag;
				}
				else if (igxObject_0 is IGxDataset)
				{
					bool_0 = ((igxObject_0 as IGxDataset).Dataset.Workspace as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, string_0];
					if ((igxObject_0 as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
					{
						flag = true;
						return flag;
					}
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