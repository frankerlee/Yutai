using System;

namespace Yutai.Catalog
{
	public interface IGxObjectFilter
	{
		string Description
		{
			get;
		}

		string Name
		{
			get;
		}

		bool CanChooseObject(IGxObject igxObject_0, ref MyDoubleClickResult myDoubleClickResult_0);

		bool CanDisplayObject(IGxObject igxObject_0);

		bool CanSaveObject(IGxObject igxObject_0, string string_0, ref bool bool_0);
	}
}