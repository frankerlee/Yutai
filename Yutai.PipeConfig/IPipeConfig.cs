using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public interface IPipeConfig : IMetaPointTable, IMetaLineTable, ICoding, IMetaTerrainTable, IEMLayerInfo, IEMDataSetInfo
	{
		DataSetConfig DatasetConfigItem(int idx);

		string GetDataSetNameByEName(string strAlias);

		int GetDataSetNum();

		int GetLayerNum();

		int getLineConfig_Color(string sLineTableName);

		string getLineConfig_DM(string sLineTableName);

		string getLineConfig_FDatasetName(string sLineTableName);

		int getLineConfig_Flag(string sLineTableName);

		int getLineConfig_Group(string sLineTableName);

		int getLineConfig_HeightFlag(string sLineTableName);

		string getLineConfig_Kind(string sLineTableName);

		string getLineConfig_NetworkName(string sLineTableName);

		int getLineGclx(string sLineTableName, string value);

		string GetPipeLayerNameByAlias(string strAlias);

		string getPointConfig_Control(string sPointTableName);

		int getPointConfig_Corlor(string sPointTableName);

		int getPointConfig_Flag(string sPointTableName);

		string getPointConfig_LineTableName(string sPointTableName);

		bool IsPipeLine(string sLayername);

		bool IsPipePoint(string sLayername);

		LayerConfig LayerConfigItem(int idx);

		LineConfig LineConfigItem(string sLineTableName);

		PointConfig PointConfigItem(string sPointTableName);

		bool ReadConfigFile(string sPath);

		bool ReadFromDatabase(IFeatureWorkspace pFWks);

		bool ReadLineTableConfigXML(string sFile);

		bool ReadPointTableConfigXML(string sFile);
	}
}