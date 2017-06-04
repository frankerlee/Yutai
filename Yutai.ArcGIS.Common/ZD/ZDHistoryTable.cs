namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDHistoryTable
	{
		public string TableName
		{
			get
			{
				return "ZDHistory";
			}
		}

		public string OrigineZDOIDName
		{
			get
			{
				return "OrigineOID";
			}
		}

		public string NewZDOIDName
		{
			get
			{
				return "NewOID";
			}
		}

		public string HisZDOIDName
		{
			get
			{
				return "HisOID";
			}
		}

		public string OrigineZDHFieldName
		{
			get
			{
				return "OrigineZDH";
			}
		}

		public string NewZDHFieldName
		{
			get
			{
				return "NewZDH";
			}
		}

		public string ChangeTypeFieldName
		{
			get
			{
				return "ChangeType";
			}
		}

		public string ChageDateFieldName
		{
			get
			{
				return "ChageDate";
			}
		}

		public string ZDRegisterGuidName
		{
			get
			{
				return "ZDRegisterGuid";
			}
		}

		public string ZDFeatureClassName
		{
			get
			{
				return "ZDFeatureClass";
			}
		}

		public void CreateTable()
		{
		}
	}
}
