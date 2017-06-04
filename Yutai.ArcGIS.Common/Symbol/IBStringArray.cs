namespace Yutai.ArcGIS.Common.Symbol
{
	public interface IBStringArray
	{
		int Count
		{
			get;
		}

	    string String(int int_0);
		

		void AddString(string string_0);

		void RemoveAll();

		void RemoveString(int int_0);
	}
}