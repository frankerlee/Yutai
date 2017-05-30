using System;

namespace Yutai.Catalog
{
	public interface IGxFile
	{
		string Path
		{
			get;
			set;
		}

		void Close(bool bool_0);

		void Edit();

		void New();

		void Open();

		void Save();
	}
}