using ESRI.ArcGIS.esriSystem;
using System;

namespace Yutai.Catalog
{
	public interface IGxObject
	{
		string BaseName
		{
			get;
		}

		string Category
		{
			get;
		}

		UID ClassID
		{
			get;
		}

		string FullName
		{
			get;
		}

		IName InternalObjectName
		{
			get;
		}

		bool IsValid
		{
			get;
		}

		string Name
		{
			get;
		}

		IGxObject Parent
		{
			get;
		}

		void Attach(IGxObject igxObject_0, IGxCatalog igxCatalog_0);

		void Detach();

		void Refresh();
	}
}