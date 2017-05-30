using System;

namespace Yutai.Catalog
{
	public class MyGxFilterBasicTypes : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "基本类型";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterBasicTypes";
			}
		}

		public MyGxFilterBasicTypes()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (igxObject_0 is IGxCatalog)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
			}
			else if (igxObject_0 is IGxBasicObject)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
				flag = true;
			}
			else if (igxObject_0 is IGxDiskConnection)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRShowChildren;
				flag = true;
			}
			else if (!(igxObject_0 is IGxFolder))
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
			return ((igxObject_0 is IGxCatalog || igxObject_0 is IGxBasicObject || igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)) ? false : true);
		}

		public bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0)
		{
			return false;
		}
	}
}