using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public interface IGxNewDatabase
	{
		IWorkspaceFactory WorkspaceFactory
		{
			set;
		}
	}
}