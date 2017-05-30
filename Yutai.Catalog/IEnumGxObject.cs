using System;

namespace Yutai.Catalog
{
	public interface IEnumGxObject
	{
		IGxObject Next();

		void Reset();
	}
}