using System;
using System.IO;

namespace Yutai.Catalog
{
	public class MyGxFilterWorkspaces : IGxObjectFilter
	{
		public string Description
		{
			get
			{
				return "文件夹和Geodatabases";
			}
		}

		public string Name
		{
			get
			{
				return "GxFilterWorkspaces";
			}
		}

		public MyGxFilterWorkspaces()
		{
		}

		public bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0)
		{
			bool flag;
			if (!(igxObject_0 is IGxDiskConnection || igxObject_0 is IGxFolder ? false : !(igxObject_0 is IGxRemoteDatabaseFolder)))
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRChooseAndDismiss;
				flag = true;
			}
			else if (igxObject_0 is IGxBasicObject)
			{
				myDoubleClickResult_0 = MyDoubleClickResult.myDCRDefault;
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
			if ((igxObject_0 is IGxDiskConnection || igxObject_0 is IGxFolder || igxObject_0 is IGxRemoteDatabaseFolder ? false : !(igxObject_0 is IGxBasicObject)))
			{
				flag = (!(igxObject_0 is IGxDatabase) ? false : true);
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
				flag = (!(igxObject_0 is IGxDatabase) ? false : true);
			}
			else
			{
				string str = string.Concat((igxObject_0 as IGxFile).Path, "\\", string_0);
				if (Path.GetExtension(str).ToLower() != ".mdb")
				{
					bool_0 = Directory.Exists(str);
				}
				else
				{
					bool_0 = File.Exists(str);
				}
				flag = true;
			}
			return flag;
		}
	}
}