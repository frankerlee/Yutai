using ESRI.ArcGIS.GISClient;
using System;

namespace Yutai.Catalog
{
	public interface IGxServersFolder
	{
		IAGSServerConnection2 AGSServerConnection
		{
			get;
			set;
		}

		string FolderName
		{
			get;
			set;
		}
	}
}