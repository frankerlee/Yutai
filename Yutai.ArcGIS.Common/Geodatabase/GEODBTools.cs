using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class GEODBTools
	{
		public GEODBTools()
		{
		}

		public static bool CheckRecordIsExist(ITable itable_0, string string_0)
		{
			bool flag = false;
			IQueryFilter queryFilterClass = new QueryFilter()
			{
				WhereClause = string_0
			};
			ICursor cursor = itable_0.Search(queryFilterClass, false);
			IRow row = cursor.NextRow();
			flag = row != null;
			ComReleaser.ReleaseCOMObject(cursor);
			ComReleaser.ReleaseCOMObject(row);
			return flag;
		}
	}
}