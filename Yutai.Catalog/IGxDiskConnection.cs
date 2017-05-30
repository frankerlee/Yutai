using System;

namespace Yutai.Catalog
{
	public interface IGxDiskConnection
	{
		bool HasCachedChildren
		{
			get;
		}

		void RefreshStatus();
	}
}