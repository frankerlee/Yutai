using System;

namespace Yutai.Catalog
{
	public interface IGxObjectFilterCollection
	{
		void AddFilter(IGxObjectFilter igxObjectFilter_0, bool bool_0);

		void RemoveAllFilters();
	}
}