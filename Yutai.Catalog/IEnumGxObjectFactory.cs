using System;

namespace Yutai.Catalog
{
	public interface IEnumGxObjectFactory
	{
		IGxObjectFactory Next();

		void Reset();
	}
}