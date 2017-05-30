using ESRI.ArcGIS.esriSystem;
using System;

namespace Yutai.Catalog
{
	public interface IGxObjectFactory
	{
		IGxCatalog Catalog
		{
			set;
		}

		string Name
		{
			get;
		}

		IEnumGxObject GetChildren(string string_0, IFileNames ifileNames_0);

		bool HasChildren(string string_0, IFileNames ifileNames_0);
	}
}