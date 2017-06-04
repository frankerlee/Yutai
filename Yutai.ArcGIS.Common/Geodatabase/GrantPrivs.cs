using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	internal class GrantPrivs
	{
		public GrantPrivs()
		{
		}

		public void doGrant(IDatasetName idatasetName_0, string string_0, string string_1)
		{
			ISQLPrivilege idatasetName0 = idatasetName_0 as ISQLPrivilege;
			try
			{
				idatasetName0.Revoke(string_1, 1);
				idatasetName0.Revoke(string_1, 2);
				idatasetName0.Revoke(string_1, 4);
				idatasetName0.Revoke(string_1, 8);
			}
			catch
			{
			}
			string string0 = string_0;
			if (string0 != null)
			{
				if (string0 == "R")
				{
					idatasetName0.Grant(string_1, 1, false);
				}
				else if (string0 == "RW")
				{
					idatasetName0.Grant(string_1, 15, false);
				}
				else if (string0 != "NONE")
				{
				}
			}
		}
	}
}