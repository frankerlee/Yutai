using System.Data;
using System.Data.OleDb;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Common.Data
{
	public class DbConnectionPara
	{
		private string string_0 = string.Empty;

		private string string_1 = string.Empty;

		private string string_2 = string.Empty;

		private string string_3 = string.Empty;

		public string Database
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public DbProviderType DbProviderType
		{
			get;
			set;
		}

		public string Password
		{
			get
			{
				return this.string_3;
			}
			set
			{
				this.string_3 = value;
			}
		}

		public string Server
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public string User
		{
			get
			{
				return this.string_2;
			}
			set
			{
				this.string_2 = value;
			}
		}

		public DbConnectionPara()
		{
			this.string_0 = AppConfigInfo.SDEServer;
			this.string_2 = AppConfigInfo.SDEUser;
			this.string_3 = AppConfigInfo.SDEPassword;
			this.string_1 = AppConfigInfo.SDEDatabase;
			this.DbProviderType = AppConfigInfo.DbProviderType;
		}

		public DbConnectionPara(string string_4, string string_5, string string_6, string string_7)
		{
			this.string_0 = string_4;
			this.string_1 = string_5;
			this.string_2 = string_6;
			this.string_3 = string_7;
		}

		public string GetAdoConnectionString(DbProviderType dbProviderType_1)
		{
			string[] string1;
			string empty = null;
			switch (dbProviderType_1)
			{
				case DbProviderType.Access:
				{
					string1 = new string[] { "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", this.string_1, ";user id=", this.string_2, ";password=", this.string_3, ";" };
					empty = string.Concat(string1);
					break;
				}
				case DbProviderType.SqlServer:
				{
					string1 = new string[] { "Provider=MSDASQL.1;Data Source=", this.Server, ";Initial Catalog=", this.string_1, ";user id=", this.string_2, ";password=", this.string_3, ";Persist Security Info=True;" };
					empty = string.Concat(string1);
					break;
				}
				case DbProviderType.Oracel:
				{
					string1 = new string[] { "Provider=OraOLEDB.Oracle.1;Data Source=", this.string_1, ";user id=", this.string_2, ";password=", this.string_3, ";Connection Lifetime=10" };
					empty = string.Concat(string1);
					break;
				}
				default:
				{
					empty = string.Empty;
					break;
				}
			}
			return empty;
		}

		public string GetOledbConnectionString()
		{
			return this.GetOledbConnectionString(this.DbProviderType);
		}

		public string GetOledbConnectionString(DbProviderType dbProviderType_1)
		{
			string[] string1;
			string empty = null;
			switch (dbProviderType_1)
			{
				case DbProviderType.Access:
				{
					string1 = new string[] { "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=", this.string_1, ";User ID=", this.string_2, ";Password=", this.string_3, ";" };
					empty = string.Concat(string1);
					break;
				}
				case DbProviderType.SqlServer:
				{
					string1 = new string[] { "Provider=SQLOLEDB;Data Source=", this.string_0, ";User ID=", this.string_2, ";Password=", this.string_3, ";Initial Catalog=", this.string_1, ";" };
					empty = string.Concat(string1);
					break;
				}
				case DbProviderType.Oracel:
				{
					string1 = new string[] { "Provider=MSDAORA;Data Source=", this.string_1, ";User ID=", this.string_2, ";Password=", this.string_3, ";Connection Lifetime=10;" };
					empty = string.Concat(string1);
					break;
				}
				default:
				{
					empty = string.Empty;
					break;
				}
			}
			return empty;
		}

		public void TestOlddbConnection(DbProviderType dbProviderType_1)
		{
			IDbConnection oleDbConnection = new OleDbConnection();
			try
			{
				oleDbConnection.ConnectionString = this.GetOledbConnectionString(dbProviderType_1);
				oleDbConnection.Open();
			}
			finally
			{
				if (oleDbConnection.State != ConnectionState.Closed)
				{
					oleDbConnection.Close();
				}
				oleDbConnection.Dispose();
			}
		}
	}
}