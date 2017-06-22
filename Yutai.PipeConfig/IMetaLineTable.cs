namespace Yutai.PipeConfig
{
	public interface IMetaLineTable
	{
		string Diameter
		{
			get;
			set;
		}

		string FlowDirection
		{
			get;
			set;
		}

		string Kind
		{
			get;
			set;
		}

		string Material
		{
			get;
			set;
		}

		string Pertain
		{
			get;
			set;
		}

		string Place
		{
			get;
			set;
		}

		string Section_Size
		{
			get;
			set;
		}

		string GetLineTableFieldName(string sKey);

		bool ReadMetaLineTableFile(string szFile);
	}
}