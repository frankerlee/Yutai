using System;

namespace Yutai.Catalog
{
	public class MyGxFilterSDEFeatureDatasets : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return null;
			}
		}

		public string Name
		{
			get
			{
				return null;
			}
		}

		public MyGxFilterSDEFeatureDatasets()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			return false;
		}

		public bool CanDisplayObject(IGxObject igxObject_0)
		{
			return false;
		}

		public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
		{
			return false;
		}
	}
}