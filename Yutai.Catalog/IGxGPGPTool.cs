using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.GISClient;
using System;

namespace Yutai.Catalog
{
	public interface IGxGPGPTool
	{
		IAGSServerConnection AGSServerConnection
		{
			get;
			set;
		}

		IAGSServerObjectName AGSServerObjectName
		{
			get;
			set;
		}

		IGPToolInfo GPToolInfo
		{
			get;
			set;
		}
	}
}