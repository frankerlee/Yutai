using ESRI.ArcGIS.esriSystem;
using System;

namespace Yutai.Catalog
{
	public interface IGxObjectFactories
	{
		int Count
		{
			get;
		}

		IEnumGxObjectFactory EnabledGxObjectFactories
		{
			get;
		}

		IGxObjectFactory GxObjectFactory
		{
			get;
		}

		UID GxObjectFactoryCLSID
		{
			get;
		}

		bool IsEnabled
		{
			get;
		}

		void EnableGxObjectFactory(int int_0, bool bool_0);
	}
}