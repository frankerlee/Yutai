using ESRI.ArcGIS.esriSystem;
using System;

namespace Yutai.Catalog
{
	public interface IGxObjectInternalName
	{
		IName InternalObjectName
		{
			get;
			set;
		}
	}
}