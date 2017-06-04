namespace Yutai.ArcGIS.Common.Geodatabase
{
	public interface ICADConvert : IDataConvert
	{
		string AutoCADVersion
		{
			get;
			set;
		}
	}
}