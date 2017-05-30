using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	internal interface IGxFilterDataElements
	{
		IGPDomain ChooseDomain
		{
			get;
			set;
		}

		string Description
		{
			set;
		}

		IGPDomain DisplayDomain
		{
			get;
			set;
		}

		string Name
		{
			set;
		}
	}
}