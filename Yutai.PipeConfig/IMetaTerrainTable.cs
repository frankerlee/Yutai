using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public interface IMetaTerrainTable
	{
		string BuildHigh
		{
			get;
			set;
		}

		string FloorHigh
		{
			get;
			set;
		}

		string FloorNumber
		{
			get;
			set;
		}

		string GetTerrainTableFieldName(string sKey);

		bool ReadMetaTerrainTableFile(ITable pTable);
	}
}