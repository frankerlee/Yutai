using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using SQLDMO;
using Application = SQLDMO.Application;
using Registry = Microsoft.Win32.Registry;

namespace Yutai.ArcGIS.Common.Data
{
	public class SQlDBManage
	{
		private ProgressBar progressBar_0;

		private string string_0 = "";

		private string string_1 = "";

		private string string_2 = "";

		private OleDbConnection oleDbConnection_0 = new OleDbConnection();

		private OleDbCommand oleDbCommand_0 = new OleDbCommand();

		private OleDbDataAdapter oleDbDataAdapter_0 = new OleDbDataAdapter();

		private DataSet dataSet_0 = new DataSet();

		public string Password
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

		public string ServerName
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

		public string UserName
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

		public SQlDBManage()
		{
		}

		public bool attachDB(string string_3, string string_4, string string_5, string string_6)
		{
			bool flag = true;
			try
			{
				try
				{
					this.oleDbConnection_0.ConnectionString = string_3;
					if (this.oleDbConnection_0.State != ConnectionState.Open)
					{
						this.oleDbConnection_0.Open();
					}
					this.oleDbCommand_0.Connection = this.oleDbConnection_0;
					this.oleDbCommand_0.CommandText = "sp_attach_db";
					this.oleDbCommand_0.Parameters.Add(new OleDbParameter("@dbname", OleDbType.VarChar));
					this.oleDbCommand_0.Parameters["@dbname"].Value = string_4;
					this.oleDbCommand_0.Parameters.Add(new OleDbParameter("@filename1", OleDbType.VarChar));
					this.oleDbCommand_0.Parameters["@filename1"].Value = string_5;
					this.oleDbCommand_0.Parameters.Add(new OleDbParameter("@filename2", OleDbType.VarChar));
					this.oleDbCommand_0.Parameters["@filename2"].Value = string_6;
					this.oleDbCommand_0.CommandType = CommandType.StoredProcedure;
					this.oleDbCommand_0.ExecuteNonQuery();
				}
				catch
				{
					flag = false;
				}
			}
			finally
			{
				this.oleDbConnection_0.Close();
			}
			return flag;
		}

		public bool BackUpDB(string string_3, string string_4, string string_5, string string_6)
		{
			bool flag = true;
			try
			{
				try
				{
					this.oleDbConnection_0.ConnectionString = string_3;
					if (this.oleDbConnection_0.State != ConnectionState.Open)
					{
						this.oleDbConnection_0.Open();
					}
					this.oleDbCommand_0.Connection = this.oleDbConnection_0;
					OleDbCommand oleDbCommand0 = this.oleDbCommand_0;
					string[] string4 = new string[] { "BACKUP DATABASE ", string_4, " to disk='", string_6, "'WITH  NOINIT ,  NOUNLOAD ,  NAME = N'", string_5, "',  NOSKIP ,  STATS = 10,  NOFORMAT " };
					oleDbCommand0.CommandText = string.Concat(string4);
					this.oleDbCommand_0.CommandType = CommandType.Text;
					this.oleDbCommand_0.ExecuteNonQuery();
				}
				catch
				{
					flag = false;
				}
			}
			finally
			{
				this.oleDbConnection_0.Close();
			}
			return flag;
		}

        public bool BackUPDB(string string_3, string string_4, System.Windows.Forms.ProgressBar progressBar_1)
        {
            this.progressBar_0 = progressBar_1;
            SQLServer sQLServer = new SQLServer();
            Database database = new Database();

            bool result;
            try
            {
                sQLServer.Connect(this.ServerName, this.UserName, this.Password);
                int i = 1;
                while (i <= sQLServer.Databases.Count)
                {
                    if (!(sQLServer.Databases.ItemByID(i).Name.Trim() == string_3))
                    {
                        i++;
                    }
                    else
                    {
                        database = (Database)sQLServer.Databases.ItemByID(i);
                        IL_7B:
                        if (!database.Isdb_owner)
                        {
                            System.Windows.Forms.MessageBox.Show("没有足够的操作权限");
                            result = false;
                            return result;
                        }
                        Backup backup = new Backup();
                        backup.Action = 0;
                        backup.Initialize=true;
                        BackupSink_PercentCompleteEventHandler backupSink_PercentCompleteEventHandler = new BackupSink_PercentCompleteEventHandler(this.Step);
                        backup.PercentComplete+=(backupSink_PercentCompleteEventHandler);
                        backup.Files=(string_4);
                        backup.Database=(string_3);
                        backup.SQLBackup(sQLServer);
                        result = true;
                        return result;
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("备份数据库失败" + ex.Message);
            }
            finally
            {
                sQLServer.DisConnect();
                
            }
            return false;

        }


        public bool detachDB(string string_3, string string_4, bool bool_0, bool bool_1, string string_5)
		{
			bool flag;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			string str4 = "";
			string str5 = "";
			bool flag1 = true;
			if (bool_0)
			{
				try
				{
					try
					{
						if (this.oleDbConnection_0.State == ConnectionState.Open)
						{
							this.oleDbConnection_0.Close();
						}
						this.oleDbConnection_0.ConnectionString = string_3;
						string str6 = string.Concat("exec sp_helpdb ", string_4);
						this.oleDbDataAdapter_0.SelectCommand = new OleDbCommand(str6, this.oleDbConnection_0);
						this.oleDbDataAdapter_0.Fill(this.dataSet_0);
						str = this.dataSet_0.Tables[1].Rows[0].ItemArray[0].ToString().Trim();
						str1 = this.dataSet_0.Tables[1].Rows[1].ItemArray[0].ToString().Trim();
						str2 = this.dataSet_0.Tables[1].Rows[0].ItemArray[2].ToString().Trim();
						str3 = this.dataSet_0.Tables[1].Rows[1].ItemArray[2].ToString().Trim();
						str4 = string.Concat(string_5, str, ".mdf");
						str5 = string.Concat(string_5, str1, ".ldf");
						if (this.oleDbConnection_0.State != ConnectionState.Open)
						{
							this.oleDbConnection_0.Open();
						}
						this.oleDbCommand_0.Connection = this.oleDbConnection_0;
						this.oleDbCommand_0.CommandText = string.Concat("sp_detach_db '", string_4, "','true'");
						this.oleDbCommand_0.CommandType = CommandType.Text;
						this.oleDbCommand_0.ExecuteNonQuery();
						if (bool_1)
						{
							File.Move(str2, str4);
							File.Move(str3, str5);
						}
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message.ToString());
						flag1 = false;
					}
				}
				finally
				{
					this.oleDbConnection_0.Close();
					this.dataSet_0.Dispose();
				}
				flag = flag1;
			}
			else
			{
				try
				{
					this.oleDbConnection_0.ConnectionString = string_3;
					if (this.oleDbConnection_0.State != ConnectionState.Open)
					{
						this.oleDbConnection_0.Open();
					}
					this.oleDbCommand_0.Connection = this.oleDbConnection_0;
					this.oleDbCommand_0.CommandText = string.Concat("DROP DATABASE ", string_4);
					this.oleDbCommand_0.CommandType = CommandType.Text;
					this.oleDbCommand_0.ExecuteNonQuery();
					flag = flag1;
				}
				catch (Exception exception1)
				{
					MessageBox.Show(exception1.Message.ToString());
					flag = false;
				}
			}
			return flag;
		}

		public ArrayList GetDbList(string string_3, string string_4, string string_5)
		{
			this.ServerName = string_3;
			this.UserName = string_4;
			this.Password = string_5;
			ArrayList arrayLists = new ArrayList();
			SQLDMO.Application applicationClass = new Application();
			SQLServer sQLServerClass = new SQLServer();
			try
			{
				try
				{
					sQLServerClass.Connect(this.ServerName, this.UserName, this.Password);
					foreach (Database databasis in sQLServerClass.Databases)
					{
						if (databasis.Name == null)
						{
							continue;
						}
						arrayLists.Add(databasis.Name);
					}
				}
				catch (Exception exception)
				{
					throw new Exception(string.Concat("连接数据库出错：", exception.Message));
				}
			}
			finally
			{
				sQLServerClass.DisConnect();
				applicationClass.Quit();
			}
			return arrayLists;
		}

		public ArrayList GetServerList()
		{
			ArrayList arrayLists = new ArrayList();
			SQLDMO.Application applicationClass = new Application();
			try
			{
				try
				{
					NameList nameList = applicationClass.ListAvailableSQLServers();
					for (int i = 1; i <= nameList.Count; i++)
					{
						arrayLists.Add(nameList.Item(i));
					}
				}
				catch (Exception exception)
				{
					throw new Exception(string.Concat("取数据库服务器列表出错：", exception.Message));
				}
			}
			finally
			{
				applicationClass.Quit();
			}
			return arrayLists;
		}

		public static string getSQLHome()
		{
			string value = "";
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\MSSQLServer\\Setup");
				value = (string)registryKey.GetValue("SQLPath");
			}
			catch (Exception exception)
			{
				exception.Message.ToString();
			}
			return string.Concat(value, "\\Data\\");
		}

		public bool hasBlack(string string_3)
		{
			bool flag = false;
			int num = 0;
			while (true)
			{
				if (num >= string_3.Length)
				{
					break;
				}
				else if (string_3[num] == ' ')
				{
					flag = true;
					break;
				}
				else
				{
					num++;
				}
			}
			return flag;
		}

		private void Step(string string_3, int int_0)
		{
			this.progressBar_0.Value = int_0;
		}

		public bool restoreDB(string string_3, string string_4, string string_5)
		{
			bool flag = true;
			try
			{
				try
				{
					this.oleDbConnection_0.ConnectionString = string_3;
					if (this.oleDbConnection_0.State != ConnectionState.Open)
					{
						this.oleDbConnection_0.Open();
					}
					this.oleDbCommand_0.Connection = this.oleDbConnection_0;
					this.oleDbCommand_0.CommandText = string.Concat("RESTORE FILELISTONLY from disk='", string_5, "'");
					this.oleDbCommand_0.CommandType = CommandType.Text;
					this.oleDbCommand_0.ExecuteNonQuery();
				}
				catch
				{
					flag = false;
				}
			}
			finally
			{
				this.oleDbConnection_0.Close();
			}
			return flag;
		}

        public bool RestoreDB(string string_3, string string_4, string string_5, ProgressBar progressBar_1)
        {
            int i;
            bool flag;
            this.progressBar_0 = progressBar_1;
            string columnString = "";
            string str = "";
            string str1 = "";
            string str2 = "";
            SQLServer sQLServerClass = new SQLServer();
            try
            {
                try
                {
                    sQLServerClass.Connect(this.ServerName, this.UserName, this.Password);
                    QueryResults queryResult = sQLServerClass.EnumProcesses(-1);
                    int num = -1;
                    int num1 = -1;
                    for (i = 1; i <= queryResult.Columns; i++)
                    {
                        string columnName = queryResult.ColumnName[i];
                        if (columnName.ToUpper().Trim() == "SPID")
                        {
                            num = i;
                        }
                        else if (columnName.ToUpper().Trim() == "DBNAME")
                        {
                            num1 = i;
                        }
                        if ((num == -1 ? false : num1 != -1))
                        {
                            break;
                        }
                    }
                    for (i = 1; i <= queryResult.Rows; i++)
                    {
                        int columnLong = queryResult.GetColumnLong(i, num);
                        if (queryResult.GetColumnString(i, num1).ToUpper() == string_3.ToUpper())
                        {
                            sQLServerClass.KillProcess(columnLong);
                        }
                    }
                    Restore restoreClass = new Restore()
                    {
                        Action = SQLDMO_RESTORE_TYPE.SQLDMORestore_Database
                    };
                    restoreClass.PercentComplete += new RestoreSink_PercentCompleteEventHandler(this.Step);
                    restoreClass.Files = string_4;
                    restoreClass.Database = string_3;
                    restoreClass.RelocateFiles = "[SuperOA],[D:\\aaaa.mdf]";
                    columnString = restoreClass.ReadFileList(sQLServerClass).GetColumnString(1, 1);
                    str = restoreClass.ReadFileList(sQLServerClass).GetColumnString(2, 1);
                    str1 = string.Concat(string_5, columnString, ".mdf");
                    str2 = string.Concat(string_5, str, ".ldf");
                    string[] strArrays = new string[] { "[", columnString, "],[", str1, "],[", str, "],[", str2, "]" };
                    restoreClass.RelocateFiles = string.Concat(strArrays);
                    restoreClass.ReplaceDatabase = true;
                    restoreClass.SQLRestore(sQLServerClass);
                    flag = true;
                }
                catch (Exception exception)
                {
                    exception.Message.ToString();
                    MessageBox.Show("恢复数据库失败,请关闭所有和该数据库连接的程序！");
                    flag = false;
                }
            }
            finally
            {
                sQLServerClass.DisConnect();
            }
            return flag;
        }

        public bool restoreDB2(string string_3, string string_4)
		{
			bool flag = true;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			string value = "";
			string[] serverName = new string[] { "provider=sqloledb.1;Data Source=", this.ServerName, ";user id=", this.UserName, ";pwd=", this.Password };
			string str4 = string.Concat(serverName);
			try
			{
				try
				{
					this.oleDbConnection_0.ConnectionString = str4;
					string str5 = string.Concat("RESTORE FILELISTONLY from disk='", string_4, "'");
					this.oleDbDataAdapter_0.SelectCommand = new OleDbCommand(str5, this.oleDbConnection_0);
					this.oleDbDataAdapter_0.Fill(this.dataSet_0);
					str = this.dataSet_0.Tables[0].Rows[0].ItemArray[0].ToString();
					str1 = this.dataSet_0.Tables[0].Rows[1].ItemArray[0].ToString();
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\MSSQLServer\\Setup");
					value = (string)registryKey.GetValue("SQLPath");
					str2 = string.Concat(value, "\\Data\\", str, ".mdf");
					str3 = string.Concat(value, "\\Data\\", str1, ".ldf");
					if (this.oleDbConnection_0.State != ConnectionState.Open)
					{
						this.oleDbConnection_0.Open();
					}
					this.oleDbCommand_0.Connection = this.oleDbConnection_0;
					OleDbCommand oleDbCommand0 = this.oleDbCommand_0;
					serverName = new string[] { "RESTORE DATABASE ", string_3, " from disk='", string_4, "' with RECOVERY, MOVE '", str, "' TO '", str2, "', MOVE '", str1, "' TO '", str3, "'" };
					oleDbCommand0.CommandText = string.Concat(serverName);
					this.oleDbCommand_0.CommandType = CommandType.Text;
					this.oleDbCommand_0.ExecuteNonQuery();
				}
				catch (Exception exception)
				{
					exception.Message.ToString();
					flag = false;
				}
			}
			finally
			{
				this.oleDbConnection_0.Close();
				this.dataSet_0.Dispose();
			}
			return flag;
		}
	}
}