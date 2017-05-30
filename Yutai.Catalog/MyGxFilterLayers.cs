using System;

namespace Yutai.Catalog
{
	public class MyGxFilterLayers : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "图层文件";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterLayers";
			}
		}

		public MyGxFilterLayers()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxLayer)
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
			return ((igxObject_0 is IGxObjectContainer ? false : !(igxObject_0 is IGxLayer)) ? false : true);
		}

		public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
		{
			return false;
		}
	}
}