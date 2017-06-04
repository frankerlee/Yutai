using System.Collections;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class MyEnumNameMapping : IMyEnumNameMapping, IEnumNameMapping
	{
		private int int_0 = 0;

		private IList ilist_0 = new ArrayList();

		public MyEnumNameMapping()
		{
		}

		public void Add(INameMapping inameMapping_0)
		{
			this.ilist_0.Add(inameMapping_0);
		}

		public INameMapping FindDatasetName(string string_0, esriDatasetType esriDatasetType_0)
		{
			return null;
		}

		public INameMapping FindDomain(string string_0)
		{
			return null;
		}

		public INameMapping Next()
		{
			INameMapping item;
			if (this.ilist_0.Count != this.int_0)
			{
				MyEnumNameMapping int0 = this;
				int0.int_0 = int0.int_0 + 1;
				item = this.ilist_0[this.int_0 - 1] as INameMapping;
			}
			else
			{
				item = null;
			}
			return item;
		}

		public void Reset()
		{
			this.int_0 = 0;
		}
	}
}