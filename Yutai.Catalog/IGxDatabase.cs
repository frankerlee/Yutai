using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public interface IGxDatabase
	{
		bool IsConnected
		{
			get;
		}

		bool IsEnterpriseGeodatabase
		{
			get;
		}

		bool IsRemoteDatabase
		{
			get;
		}

		IWorkspace Workspace
		{
			get;
		}

		IWorkspaceName WorkspaceName
		{
			get;
			set;
		}

		void Connect();

		void Disconnect();
	}
}