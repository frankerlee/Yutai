using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterPointFeatureClasses : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "点要素类";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterPointFeatureClasses";
			}
		}

		public MyGxFilterPointFeatureClasses()
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
				esriDatasetType type = (igxObject_0 as IGxDataset).Type;
				if (type != esriDatasetType.esriDTFeatureDataset)
				{
					if (type != esriDatasetType.esriDTFeatureClass)
					{
						flag = false;
						return flag;
					}
					myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
					flag = true;
					return flag;
				}
				else
				{
					myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
					flag = true;
					return flag;
				}
			}
			flag = false;
			return flag;
		}

		public bool CanDisplayObject(IGxObject igxObject_0)
		{
			bool flag;
			try
			{
				if (igxObject_0 is IGxDataset)
				{
					esriDatasetType type = (igxObject_0 as IGxDataset).Type;
					if (type == esriDatasetType.esriDTFeatureDataset)
					{
						flag = true;
						return flag;
					}
					else if (type == esriDatasetType.esriDTFeatureClass)
					{
						IFeatureClassName datasetName = (igxObject_0 as IGxDataset).DatasetName as IFeatureClassName;
						if (datasetName.FeatureType == esriFeatureType.esriFTSimple)
						{
							if (datasetName.ShapeType == esriGeometryType.esriGeometryNull)
							{
								IFeatureClass dataset = (igxObject_0 as IGxDataset).Dataset as IFeatureClass;
								if ((dataset.ShapeType == esriGeometryType.esriGeometryMultipoint ? true : dataset.ShapeType == esriGeometryType.esriGeometryPoint))
								{
									flag = true;
									return flag;
								}
							}
							else if ((datasetName.ShapeType == esriGeometryType.esriGeometryMultipoint ? true : datasetName.ShapeType == esriGeometryType.esriGeometryPoint))
							{
								flag = true;
								return flag;
							}
						}
					}
				}
				else if (igxObject_0 is IGxObjectContainer)
				{
					flag = true;
					return flag;
				}
				flag = false;
				return flag;
			}
			catch
			{
				flag = false;
				return flag;
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