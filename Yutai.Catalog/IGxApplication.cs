using System;
using ESRI.ArcGIS.Framework;

namespace Yutai.Catalog
{
	public interface IGxApplication : IApplication
	{
		IGxCatalog GxCatalog
		{
			get;
			set;
		}

		IGxSelection GxSelection
		{
			get;
			set;
		}
	}
}