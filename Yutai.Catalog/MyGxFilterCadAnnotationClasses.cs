using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public class MyGxFilterCadAnnotationClasses : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "CAD注记类";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterCadAnnotationClasses";
			}
		}

		public MyGxFilterCadAnnotationClasses()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (!(igxObject_0 is IGxObjectContainer))
			{
				IName internalObjectName = igxObject_0.InternalObjectName;
				if (!(internalObjectName is IFeatureClassName) || (internalObjectName as IFeatureClassName).FeatureType != esriFeatureType.esriFTCoverageAnnotation)
				{
					flag = false;
				}
				else
				{
					myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
					flag = true;
				}
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
			else if ((igxObject_0 as IGxDataset).DatasetName.Type != esriDatasetType.esriDTCadDrawing)
			{
				if (igxObject_0.Category != "CAD注记要素类")
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
			return false;
		}
	}
}