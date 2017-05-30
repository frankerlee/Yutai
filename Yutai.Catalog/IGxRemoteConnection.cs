using System;

namespace Yutai.Catalog
{
	public interface IGxRemoteConnection
	{
		void Connect();

		void Disconnect();
	}
}