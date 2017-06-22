namespace Yutai.PipeConfig
{
	public interface IEMLayerInfo
	{
		int GetLayerNum();

		string GetPipeLayerNameByAlias(string strAlias);

		bool IsPipeLine(string sLayername);

		bool IsPipePoint(string sLayername);

		LayerConfig LayerConfigItem(string sTableName);

		LayerConfig LayerConfigItem(int idx);

		bool ReadLayerInfoFromFile(string szFile);
	}
}