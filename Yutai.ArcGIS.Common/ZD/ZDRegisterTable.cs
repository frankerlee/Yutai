namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDRegisterTable
	{
		public string TableName
		{
			get
			{
				return "ZDInfos";
			}
		}

		public string FeatureClassNameField
		{
			get
			{
				return "FeatureClassName";
			}
		}

		public string RegisterDateFieldName
		{
			get
			{
				return "RegisterDate";
			}
		}

		public string ZDBHFieldName
		{
			get
			{
				return "ZDBHFieldName";
			}
		}

		public string GDBConnectInfoName
		{
			get
			{
				return "GDBConnectInfo";
			}
		}

		public string GuidName
		{
			get
			{
				return "Guid";
			}
		}

		public string HistoryFeatureClassName
		{
			get
			{
				return "HistoryFeatureClass";
			}
		}

		public void CreateTable()
		{
		}
	}
}
