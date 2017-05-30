using System;

namespace Yutai.Catalog
{
	public interface IGxFileSetup
	{
		string Category
		{
			set;
		}

		void SetImages(int int_0, int int_1, int int_2, int int_3);
	}
}