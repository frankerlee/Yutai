namespace Yutai.PipeConfig
{
	public interface IMetaPointTable
	{
		string Adjunct
		{
			get;
			set;
		}

		string AdjunctPertain
		{
			get;
			set;
		}

		string Height
		{
			get;
			set;
		}

		string PointKind
		{
			get;
			set;
		}

		string GetPointTableFieldName(string sKey);

		bool ReadMetaPointTableFile(string szFile);
	}
}