using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterEnteripesGeoDatabases : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "空间数据库";
			}
		}

		public string Name
		{
			get
			{
				return "空间数据库";
			}
		}

		public MyGxFilterEnteripesGeoDatabases()
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
			if ((igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				flag = (!(igxObject_0 is IGxDatabase) || !(igxObject_0 as IGxDatabase).IsRemoteDatabase ? false : true);
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
			if ((igxObject_0 is IGxDiskConnection ? false : !(igxObject_0 is IGxFolder)))
			{
				flag = false;
			}
			else
			{
				string str = string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0);
				bool_0 = File.Exists(str);
				flag = true;
			}
			return flag;
		}
	}
}