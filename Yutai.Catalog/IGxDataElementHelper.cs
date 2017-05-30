using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	internal interface IGxDataElementHelper
	{
		void RetrieveDEBaseProperties(IDataElement idataElement_0);

		void RetrieveDEFullProperties(IDataElement idataElement_0);
	}
}