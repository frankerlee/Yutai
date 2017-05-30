using ESRI.ArcGIS.Geodatabase;
using System;

namespace Yutai.Catalog
{
	public interface IGxDataset
	{
		IDataset Dataset
		{
			get;
		}

		IDatasetName DatasetName
		{
			get;
			set;
		}

		esriDatasetType Type
		{
			get;
		}
	}
}