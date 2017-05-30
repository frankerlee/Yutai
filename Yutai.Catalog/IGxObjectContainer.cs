using System;

namespace Yutai.Catalog
{
	public interface IGxObjectContainer
	{
		bool AreChildrenViewable
		{
			get;
		}

		IEnumGxObject Children
		{
			get;
		}

		bool HasChildren
		{
			get;
		}

		IGxObject AddChild(IGxObject igxObject_0);

		void DeleteChild(IGxObject igxObject_0);

		void SearchChildren(string string_0, IGxObjectArray igxObjectArray_0);
	}
}