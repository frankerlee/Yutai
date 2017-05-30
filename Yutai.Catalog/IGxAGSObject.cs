using ESRI.ArcGIS.GISClient;
using System;

namespace Yutai.Catalog
{
	public interface IGxAGSObject
	{
		IAGSServerObjectName AGSServerObjectName
		{
			get;
			set;
		}

		string DefaultMapName
		{
			get;
		}

		int NumInstancesInUse
		{
			get;
		}

		int NumInstancesRunning
		{
			get;
		}

		string Status
		{
			get;
		}

		void EditServerObjectProperties(int int_0);
	}
}