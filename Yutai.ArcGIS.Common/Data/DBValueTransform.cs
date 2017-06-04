using System;

namespace Yutai.ArcGIS.Common.Data
{
	public static class DBValueTransform
	{
		public static double ParseToDouble(object object_0)
		{
			double num;
			num = ((object_0 == null ? false : !(object_0 is DBNull)) ? double.Parse(object_0.ToString()) : 0);
			return num;
		}

		public static int ParseToInt(object object_0)
		{
			int num;
			num = ((object_0 == null ? false : !(object_0 is DBNull)) ? int.Parse(object_0.ToString()) : 0);
			return num;
		}

		public static string ParseToString(object object_0)
		{
			return ((object_0 == null ? false : !(object_0 is DBNull)) ? (string)object_0 : string.Empty);
		}
	}
}