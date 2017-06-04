using System.Collections;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public interface IDataConvert
	{
		IList InputFeatureClasses
		{
			set;
		}

		string OutPath
		{
			set;
		}

		bool Export();
	}
}