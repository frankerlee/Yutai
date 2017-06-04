using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Yutai.ArcGIS.Common.Data
{
	public class OracleFuctions
	{
		public OracleFuctions()
		{
		}

		public static string addUserList(string string_0, string string_1)
		{
			string str = string_0.Replace(',', '-');
			if (str[str.Length - 1] != '-')
			{
				str = string.Concat(str, '-');
			}
			int num = string_1.LastIndexOf('\\');
			str = string_1.Insert(num + 1, str);
			return str;
		}

		public static void CallCreateUserParFileConntion(ref ArrayList arrayList_0, string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
		{
			arrayList_0.Clear();
			arrayList_0.Add("disconnect;");
			string[] string0 = new string[] { "connect ", string_0, "/", string_1, "@", string_2, " as sysdba;" };
			arrayList_0.Add(string.Concat(string0));
			arrayList_0.Add(string.Concat("CREATE USER \"", string_3, "\"  PROFILE \"DEFAULT\" "));
			string0 = new string[] { "    IDENTIFIED BY \"", string_4, "\" DEFAULT TABLESPACE \"", string_5, "\" " };
			arrayList_0.Add(string.Concat(string0));
			arrayList_0.Add("    ACCOUNT UNLOCK;");
			arrayList_0.Add(string.Concat("GRANT ALTER ANY CLUSTER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY DIMENSION TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY INDEX TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY INDEXTYPE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY LIBRARY TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY OUTLINE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY PROCEDURE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY ROLE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY SEQUENCE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY SNAPSHOT TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY TABLE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY TRIGGER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER ANY TYPE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT ALTER SESSION TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY CLUSTER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY CONTEXT TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY DIMENSION TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY DIRECTORY TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY INDEX TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY INDEXTYPE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY LIBRARY TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY OPERATOR TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY OUTLINE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY PROCEDURE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY SEQUENCE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY SNAPSHOT TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY SYNONYM TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY TABLE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY TRIGGER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY TYPE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT CREATE ANY VIEW TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY CLUSTER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY CONTEXT TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY DIMENSION TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY DIRECTORY TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY INDEX TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY INDEXTYPE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY LIBRARY TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY OPERATOR TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY OUTLINE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY PROCEDURE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY ROLE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY SEQUENCE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY SNAPSHOT TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY SYNONYM TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY TABLE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY TRIGGER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY TYPE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT DROP ANY VIEW TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT SYSDBA TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT SYSOPER TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT UNLIMITED TABLESPACE TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT \"CONNECT\" TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT \"DBA\" TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add(string.Concat("GRANT \"OLAP_DBA\" TO \"", string_3, "\" WITH ADMIN OPTION;"));
			arrayList_0.Add("exit;");
		}

		public static void ConnectTest(string string_0, string string_1, string string_2)
		{
			if (!(string_1 == null ? false : !(string_1.Trim() == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(string_2 == null ? false : !(string_2.Trim() == "")))
			{
				MessageBox.Show("密码不能为空");
			}
			else if ((string_0 == null ? false : !(string_0.Trim() == "")))
			{
				OleDbConnection oleDbConnection = new OleDbConnection();
				string[] string1 = new string[] { "Provider=\"OraOLEDB.Oracle\";User ID=", string_1, ";Data Source=", string_0, ";Password=", string_2 };
				oleDbConnection.ConnectionString = string.Concat(string1);
				try
				{
					try
					{
						oleDbConnection.Open();
						if (oleDbConnection.State != ConnectionState.Open)
						{
							MessageBox.Show("连接数据库失败");
						}
						else
						{
							MessageBox.Show("连接数据库成功");
						}
					}
					catch (Exception exception)
					{
						exception.Message.ToString();
						MessageBox.Show("连接数据库失败！");
					}
				}
				finally
				{
					oleDbConnection.Close();
					oleDbConnection.Dispose();
					oleDbConnection = null;
				}
			}
			else
			{
				MessageBox.Show("Oracle服务器名不能为空");
			}
		}

		public static void CreateOracleUsers(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
		{
			int i;
			if (!(string_0 == null ? false : !(string_0.Trim() == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(string_1 == null ? false : !(string_1.Trim() == "")))
			{
				MessageBox.Show("用户名密码不能为空");
			}
			else if (!(string_2 == null ? false : !(string_2.Trim() == "")))
			{
				MessageBox.Show("Oracle服务名不能为空");
			}
			else if (!(string_3 == null ? false : !(string_3.Trim() == "")))
			{
				MessageBox.Show("创建用户名不能为空");
			}
			else if ((string_4 == null ? false : !(string_4.Trim() == "")))
			{
				ArrayList arrayLists = new ArrayList();
				string startupPath = "";
				string str = "";
				startupPath = Application.StartupPath;
				startupPath = (startupPath.Substring(startupPath.Length - 1, 1) != "\\" ? string.Concat(startupPath, "\\create_kdgis_user.par") : string.Concat(startupPath, "create_kdgis_user.par"));
				ClsReadWriteTxt clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(startupPath);
				clsReadWriteTxt.CreateTextFile(startupPath);
				arrayLists.Clear();
				if ((string_5 == null ? true : string_5 == ""))
				{
					string_5 = "SPWORKFLOW";
				}
				OracleFuctions.CallCreateUserParFileConntion(ref arrayLists, string_0, string_1, string_2, string_3, string_4, string_5);
				for (i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(startupPath, arrayLists[i].ToString());
				}
				str = Application.StartupPath;
				str = (str.Substring(str.Length - 1, 1) != "\\" ? string.Concat(str, "\\createUsers.bat") : string.Concat(str, "createUsers.bat"));
				clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(str);
				clsReadWriteTxt.CreateTextFile(str);
				arrayLists.Clear();
				arrayLists.Add("@echo on");
				string[] string0 = new string[] { "\"sqlplus\" ", string_0, "/", string_1, "@", string_2, " @", startupPath };
				arrayLists.Add(string.Concat(string0));
				for (i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(str, arrayLists[i].ToString());
				}
				OracleFuctions.ExecuteWindowExe(str, "创建用户成功");
			}
			else
			{
				MessageBox.Show("创建用户名密码不能为空");
			}
		}

		public static void CreateTableSpace(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
		{
			int i;
			string startupPath = "";
			string str = "";
			ArrayList arrayLists = new ArrayList();
			startupPath = Application.StartupPath;
			startupPath = (startupPath.Substring(startupPath.Length - 1, 1) != "\\" ? string.Concat(startupPath, "\\CreateTableSpace.par") : string.Concat(startupPath, "CreateTableSpace.par"));
			ClsReadWriteTxt clsReadWriteTxt = new ClsReadWriteTxt();
			clsReadWriteTxt.DeleteTextFile(startupPath);
			clsReadWriteTxt.CreateTextFile(startupPath);
			arrayLists.Clear();
			arrayLists.Add("disconnect;");
			string[] string0 = new string[] { "connect ", string_0, "/", string_1, "@", string_2, " as sysdba;" };
			arrayLists.Add(string.Concat(string0));
			string0 = new string[] { "CREATE TABLESPACE \"", string_3, "\"  DATAFILE '", string_4, "' SIZE ", string_5, "M reuse autoextend off;" };
			arrayLists.Add(string.Concat(string0));
			arrayLists.Add("disconnect;");
			arrayLists.Add("Exit;");
			for (i = 0; i < arrayLists.Count; i++)
			{
				clsReadWriteTxt.AppendTextToFile(startupPath, arrayLists[i].ToString());
			}
			str = Application.StartupPath;
			str = (str.Substring(str.Length - 1, 1) != "\\" ? string.Concat(str, "\\CreateTableSpace.bat") : string.Concat(str, "CreateTableSpace.bat"));
			clsReadWriteTxt = new ClsReadWriteTxt();
			clsReadWriteTxt.DeleteTextFile(str);
			clsReadWriteTxt.CreateTextFile(str);
			arrayLists.Clear();
			arrayLists.Add("@echo on");
			string0 = new string[] { "\"sqlplus\" ", string_0, "/", string_1, "@", string_2, " @", startupPath };
			arrayLists.Add(string.Concat(string0));
			for (i = 0; i < arrayLists.Count; i++)
			{
				clsReadWriteTxt.AppendTextToFile(str, arrayLists[i].ToString());
			}
			OracleFuctions.ExecuteWindowExe(str, "创建Oracle表空间成功");
		}

		public static void DeleteTableSpaceFile(string string_0, string string_1, string string_2, string string_3, string string_4)
		{
			int i;
			if (!(string_0 == null ? false : !(string_0.Trim() == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(string_1 == null ? false : !(string_1.Trim() == "")))
			{
				MessageBox.Show("用户名密码不能为空");
			}
			else if (!(string_2 == null ? false : !(string_2.Trim() == "")))
			{
				MessageBox.Show("服务名不能为空");
			}
			else if (!(string_3 == null ? false : !(string_3.Trim() == "")))
			{
				MessageBox.Show("表空间名称不能为空");
			}
			else if ((string_4 == null ? false : !(string_4.Trim() == "")))
			{
				string startupPath = "";
				string str = "";
				ArrayList arrayLists = new ArrayList();
				startupPath = Application.StartupPath;
				startupPath = (startupPath.Substring(startupPath.Length - 1, 1) != "\\" ? string.Concat(startupPath, "\\dropTableSpace.par") : string.Concat(startupPath, "dropTableSpace.par"));
				ClsReadWriteTxt clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(startupPath);
				clsReadWriteTxt.CreateTextFile(startupPath);
				arrayLists.Clear();
				arrayLists.Add("disconnect;");
				string[] string0 = new string[] { "connect ", string_0, "/", string_1, "@", string_2, " as sysdba;" };
				arrayLists.Add(string.Concat(string0));
				if ((string_3 == null ? false : string_3 != ""))
				{
					arrayLists.Add(string.Concat("drop TABLESPACE ", string_3, ";"));
				}
				arrayLists.Add("disconnect;");
				arrayLists.Add("exit;");
				for (i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(startupPath, arrayLists[i].ToString());
				}
				str = Application.StartupPath;
				str = (str.Substring(str.Length - 1, 1) != "\\" ? string.Concat(str, "\\dropTableSpace.bat") : string.Concat(str, "dropTableSpace.bat"));
				clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(str);
				clsReadWriteTxt.CreateTextFile(str);
				arrayLists.Clear();
				arrayLists.Add("@echo on");
				string0 = new string[] { "\"sqlplus\" ", string_0, "/", string_1, "@", string_2, " @", startupPath };
				arrayLists.Add(string.Concat(string0));
				for (i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(str, arrayLists[i].ToString());
				}
				OracleFuctions.ExecuteWindowExe(str, "删除Oracle表空间成功");
			}
			else
			{
				MessageBox.Show("表空间路径不能为空");
			}
		}

		public static void DeleteUsers(string string_0, string string_1, string string_2, string string_3)
		{
			int i;
			if (!(string_0 == null ? false : !(string_0.Trim() == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(string_1 == null ? false : !(string_1.Trim() == "")))
			{
				MessageBox.Show("用户名密码不能为空");
			}
			else if (!(string_2 == null ? false : !(string_2.Trim() == "")))
			{
				MessageBox.Show("服务名不能为空");
			}
			else if ((string_3 == null ? false : !(string_3.Trim() == "")))
			{
				string startupPath = "";
				string str = "";
				ArrayList arrayLists = new ArrayList();
				startupPath = Application.StartupPath;
				startupPath = (startupPath.Substring(startupPath.Length - 1, 1) != "\\" ? string.Concat(startupPath, "\\dropUsers.par") : string.Concat(startupPath, "dropUsers.par"));
				ClsReadWriteTxt clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(startupPath);
				clsReadWriteTxt.CreateTextFile(startupPath);
				arrayLists.Clear();
				arrayLists.Add("disconnect;");
				string[] string0 = new string[] { "connect ", string_0, "/", string_1, "@", string_2, " as sysdba;" };
				arrayLists.Add(string.Concat(string0));
				if ((string_3 == null ? false : string_3 != ""))
				{
					arrayLists.Add(string.Concat("drop user ", string_3, " cascade;"));
				}
				arrayLists.Add("disconnect;");
				arrayLists.Add("exit;");
				for (i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(startupPath, arrayLists[i].ToString());
				}
				str = Application.StartupPath;
				str = (str.Substring(str.Length - 1, 1) != "\\" ? string.Concat(str, "\\dropUsers.bat") : string.Concat(str, "dropUsers.bat"));
				clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(str);
				clsReadWriteTxt.CreateTextFile(str);
				arrayLists.Clear();
				arrayLists.Add("@echo off");
				string0 = new string[] { "\"sqlplus\" ", string_0, "/", string_1, "@", string_2, " @", startupPath };
				arrayLists.Add(string.Concat(string0));
				for (i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(str, arrayLists[i].ToString());
				}
				OracleFuctions.ExecuteWindowExe(str, string.Concat("删除用户 ", string_0, " "));
			}
			else
			{
				MessageBox.Show("待删除的用户名不能为空");
			}
		}

		public static void ExecuteWindowExe(string string_0, string string_1)
		{
			string string0 = string_0;
			try
			{
				Process.Start(string0).Close();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public static void ExportUsers(string string_0, string string_1, string string_2, string string_3, string string_4)
		{
			if (!(string_0 == null ? false : !(string_0.Trim() == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(string_4 == null ? false : !(string_4.Trim() == "")))
			{
				MessageBox.Show("Oracle服务名不能为空");
			}
			else if (!(string_1 == null ? false : !(string_1.Trim() == "")))
			{
				MessageBox.Show("密码不能为空");
			}
			else if (!(string_2 == null ? false : !(string_2.Trim() == "")))
			{
				MessageBox.Show("导出dmp文件路径不能为空");
			}
			else if ((string_3 == null ? false : !(string_3.Trim() == "")))
			{
				string startupPath = "";
				ArrayList arrayLists = new ArrayList();
				startupPath = Application.StartupPath;
				startupPath = (startupPath.Substring(startupPath.Length - 1, 1) != "\\" ? string.Concat(startupPath, "\\Exp_User", string_0, ".bat") : string.Concat(startupPath, "Exp_User", string_0, ".bat"));
				ClsReadWriteTxt clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(startupPath);
				clsReadWriteTxt.CreateTextFile(startupPath);
				arrayLists.Clear();
				string[] string0 = new string[] { "Exp ", string_0, "/", string_1, "@", string_4, "  file=", string_2, " Full=N  owner=", string_3, " Rows=Y;" };
				arrayLists.Add(string.Concat(string0));
				for (int i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(startupPath, arrayLists[i].ToString());
				}
				OracleFuctions.ExecuteWindowExe(startupPath, string.Concat("导出用户列表", string_3, "规划数据库备份成功"));
			}
			else
			{
				MessageBox.Show("导出用户列表不能为空");
			}
		}

		public static string getOraHome()
		{
			string value = "";
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\ORACLE");
				value = (string)registryKey.GetValue("ORACLE_HOME");
			}
			catch (Exception exception)
			{
				exception.Message.ToString();
			}
			int num = value.LastIndexOf('\\');
			string str = string.Concat(value.Substring(0, num + 1), "oradata\\");
			return str;
		}

		public static void ImportUsers(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
		{
			if (!(string_0 == null ? false : !(string_0.Trim() == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(string_1 == null ? false : !(string_1.Trim() == "")))
			{
				MessageBox.Show("用户名密码不能为空");
			}
			else if (!(string_2 == null ? false : !(string_2.Trim() == "")))
			{
				MessageBox.Show("Oracle服务名不能为空");
			}
			else if (!(string_3 == null ? false : !(string_3.Trim() == "")))
			{
				MessageBox.Show("备份文件名不能为空");
			}
			else if (!(string_4 == null ? false : !(string_4.Trim() == "")))
			{
				MessageBox.Show("源用户名不能为空");
			}
			else if ((string_5 == null ? false : !(string_5.Trim() == "")))
			{
				string startupPath = "";
				ArrayList arrayLists = new ArrayList();
				startupPath = Application.StartupPath;
				startupPath = (startupPath.Substring(startupPath.Length - 1, 1) != "\\" ? string.Concat(startupPath, "\\Imp_User", string_0, ".bat") : string.Concat(startupPath, "Imp_User", string_0, ".bat"));
				ClsReadWriteTxt clsReadWriteTxt = new ClsReadWriteTxt();
				clsReadWriteTxt.DeleteTextFile(startupPath);
				clsReadWriteTxt.CreateTextFile(startupPath);
				arrayLists.Clear();
				string[] string0 = new string[] { "imp ", string_0, "/", string_1, "@", string_2, "  file=", string_3, " Full=N  FromUser=", string_4, " ToUser=", string_5, " Rows=Y Compile=Y;" };
				arrayLists.Add(string.Concat(string0));
				for (int i = 0; i < arrayLists.Count; i++)
				{
					clsReadWriteTxt.AppendTextToFile(startupPath, arrayLists[i].ToString());
				}
				OracleFuctions.ExecuteWindowExe(startupPath, string.Concat("导入用户", string_0, "成功"));
			}
			else
			{
				MessageBox.Show("目标用户名不能为空");
			}
		}

		public static string isFileOrFolder(string string_0)
		{
			int num;
			string string0;
			string str = DateTime.Now.ToString("yyyy-MM-dd").Trim();
			if (File.Exists(string_0))
			{
				if (string_0.Substring(string_0.Length - 4, 4) == ".dmp")
				{
					string0 = string_0;
				}
				else
				{
					num = string_0.LastIndexOf('\\');
					string_0 = string_0.Substring(0, num + 1);
					str = string.Concat(string_0, str, ".dmp");
					string0 = str;
				}
			}
			else if (!Directory.Exists(string_0))
			{
				string0 = string_0;
			}
			else
			{
				if (string_0.Substring(string_0.Length - 1, 1) == "\\")
				{
					string_0 = string.Concat(string_0, str, ".dmp");
				}
				else
				{
					num = string_0.LastIndexOf('\\');
					string_0 = string.Concat(string_0, "\\");
					string_0 = string.Concat(string_0, str);
					string_0 = string.Concat(string_0, ".dmp");
					str = string_0;
				}
				string0 = string_0;
			}
			return string0;
		}
	}
}