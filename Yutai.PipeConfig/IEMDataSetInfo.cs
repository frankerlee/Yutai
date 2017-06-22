namespace Yutai.PipeConfig
{
	public interface IEMDataSetInfo
	{
		DataSetConfig DatasetConfigItem(int idx);

		DataSetConfig DataSetConfigItem(string sTableName);

		string GetDataSetNameByEName(string strAlias);

		int GetDataSetNum();

		bool ReadDataSetInfoFromFile(string szFile);
	}
}