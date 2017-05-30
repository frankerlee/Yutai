using ESRI.ArcGIS.Carto;
using System;

namespace Yutai.Catalog
{
	public interface IGxLayer
	{
		ILayer Layer
		{
			get;
			set;
		}

		Yutai.Catalog.LayerType LayerType
		{
			get;
		}
	}
}