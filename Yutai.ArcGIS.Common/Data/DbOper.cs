using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SQLDMO;

namespace Yutai.ArcGIS.Common.Data
{
    public class DbOper
    {
        public DbOper()
        {
        }

        public static void DbBackup(string string_0, string string_1, string string_2, string string_3, string string_4,
            string string_5, string string_6)
        {
            Backup backupClass = new Backup();
            SQLServer sQLServerClass = new SQLServer();
            try
            {
                try
                {
                    sQLServerClass.LoginSecure = false;
                    sQLServerClass.Connect(string_0.Trim(), string_1.Trim(), string_2.Trim());
                    backupClass.Action = 0;
                    backupClass.Database = string_3.Trim();
                    backupClass.Files = string_4;
                    backupClass.BackupSetName = string_5;
                    backupClass.BackupSetDescription = string_6;
                    backupClass.Initialize = true;
                    backupClass.SQLBackup(sQLServerClass);
                }
                catch
                {
                    throw;
                }
            }
            finally
            {
                sQLServerClass.DisConnect();
            }
        }

        public static bool DbBackupEx(string string_0, string string_1, string string_2, string string_3,
            string string_4, string string_5, string string_6)
        {
            bool flag;
            bool flag1 = false;
            Backup backupClass = new Backup();
            SQLServer sQLServerClass = new SQLServer();
            try
            {
                if (!File.Exists(string_4))
                {
                    FileStream fileStream = File.Create(string_4);
                    try
                    {
                    }
                    finally
                    {
                        if (fileStream != null)
                        {
                            ((IDisposable) fileStream).Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                flag = false;
                return flag;
            }
            if ((string_5 == null ? true : string_5 == ""))
            {
                string_5 = string.Concat("你现在进行了数据库", string_3.Trim(), "的备份设置");
            }
            if ((string_6 == null ? true : string_6 == ""))
            {
                string_6 = string.Concat("你现在备份的数据库为", string_3.Trim());
            }
            try
            {
                try
                {
                    sQLServerClass.LoginSecure = false;
                    sQLServerClass.Connect(string_0.Trim(), string_1.Trim(), string_2.Trim());
                    backupClass.Action = 0;
                    backupClass.Database = string_3.Trim();
                    backupClass.Files = string_4;
                    backupClass.BackupSetName = string_5;
                    backupClass.BackupSetDescription = string_6;
                    backupClass.Initialize = true;
                    backupClass.SQLBackup(sQLServerClass);
                    flag1 = true;
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message.ToString());
                }
            }
            finally
            {
                sQLServerClass.DisConnect();
            }
            flag = flag1;
            return flag;
        }

        public static void DbRestore(string string_0, string string_1, string string_2, string string_3, string string_4)
        {
            Restore restoreClass = new Restore();
            SQLServer sQLServerClass = new SQLServer();
            try
            {
                try
                {
                    sQLServerClass.LoginSecure = false;
                    sQLServerClass.Connect(string_0.Trim(), string_1.Trim(), string_2.Trim());
                    restoreClass.Action = 0;
                    restoreClass.Database = string_3.Trim();
                    restoreClass.Files = string_4;
                    restoreClass.FileNumber = 1;
                    restoreClass.ReplaceDatabase = true;
                    restoreClass.SQLRestore(sQLServerClass);
                }
                catch
                {
                    throw;
                }
            }
            finally
            {
                sQLServerClass.DisConnect();
            }
        }

        public static bool DbRestoreEx(string string_0, string string_1, string string_2, string string_3,
            string string_4)
        {
            bool flag = false;
            string columnString = "";
            Restore restoreClass = new Restore();
            SQLServer sQLServerClass = new SQLServer();
            try
            {
                try
                {
                    sQLServerClass.LoginSecure = false;
                    sQLServerClass.Connect(string_0.Trim(), string_1.Trim(), string_2.Trim());
                    restoreClass.Action = 0;
                    restoreClass.Database = string_3.Trim();
                    restoreClass.Files = string_4;
                    restoreClass.FileNumber = 1;


                    columnString = restoreClass.ReadFileList(sQLServerClass).GetColumnString(1, 2);
                    columnString = columnString.Substring(0, columnString.LastIndexOf('\\'));
                    if (!Directory.Exists(columnString))
                    {
                        Directory.CreateDirectory(columnString);
                    }
                    restoreClass.ReplaceDatabase = true;
                    restoreClass.SQLRestore(sQLServerClass);
                    flag = true;
                }
                catch (Exception exception)
                {
                    exception.ToString();
                    MessageBox.Show("请删除与要恢复的数据库同名的数据文件");
                }
            }
            finally
            {
                sQLServerClass.DisConnect();
            }
            return flag;
        }

        public static bool GetMachineSQL(string string_0, string string_1, string string_2)
        {
            SQLServer sQLServerClass = new SQLServer();
            bool flag = false;
            sQLServerClass.LoginSecure = false;
            try
            {
                sQLServerClass.Connect(string_0, string_1, string_2);
                flag = true;
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode == -2147221504)
                {
                    MessageBox.Show("服务器没有启动或不存在");
                }
                if (cOMException.ErrorCode == -2147203048)
                {
                    MessageBox.Show(string.Concat("用户名'", string_1, "'登录失败"));
                }
                if (cOMException.ErrorCode == -2147204362)
                {
                    MessageBox.Show("服务器暂停，不允许进行新的连接");
                }
                flag = false;
            }
            return flag;
        }

        public static bool GetMachineSQL(string string_0, string string_1, string string_2, string string_3)
        {
            bool flag;
            SQLServer sQLServerClass = new SQLServer();
            Database databaseClass = new Database();
            bool flag1 = false;
            bool flag2 = false;
            sQLServerClass.LoginSecure = false;
            int count = 0;
            try
            {
                sQLServerClass.Connect(string_0, string_2, string_3);
                count = sQLServerClass.Databases.Count;
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode == -2147221504)
                {
                    MessageBox.Show("服务器没有启动或不存在");
                }
                if (cOMException.ErrorCode == -2147203048)
                {
                    MessageBox.Show(string.Concat("用户名'", string_2, "'登录失败"));
                }
                if (cOMException.ErrorCode == -2147204362)
                {
                    MessageBox.Show("服务器暂停，不允许进行新的连接");
                }
                flag = flag2;
                return flag;
            }
            try
            {
                try
                {
                    int num = 1;
                    while (true)
                    {
                        if (num < count + 1)
                        {
                            string str = "";
                            databaseClass = (Database) sQLServerClass.Databases.ItemByID(num);
                            str = databaseClass.Name.Trim();
                            if (string_1.Trim().ToUpper() == str.ToUpper())
                            {
                                flag1 = true;
                                break;
                            }
                            else
                            {
                                num++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!flag1)
                    {
                        MessageBox.Show("目标服务器上不存在该数据库");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString());
                }
            }
            finally
            {
                sQLServerClass.DisConnect();
            }
            flag = flag2;
            return flag;
        }

        public static string isFileOrFolder(string string_0, string string_1)
        {
            int num;
            string string0;
            string str = DateTime.Now.ToString("yyyy-MM-dd").Trim();
            string string01 = string.Concat(string_1, str);
            if (File.Exists(string_0))
            {
                if (string_0.Substring(string_0.Length - 4, 4) == ".bak")
                {
                    string0 = string_0;
                }
                else
                {
                    num = string_0.LastIndexOf('\\');
                    string_0 = string_0.Substring(0, num + 1);
                    string01 = string.Concat(string_0, string01, ".bak");
                    string0 = string01;
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
                    string_0 = string.Concat(string_0, string01, ".bak");
                }
                else
                {
                    num = string_0.LastIndexOf('\\');
                    string_0 = string.Concat(string_0, "\\");
                    string_0 = string.Concat(string_0, string01);
                    string_0 = string.Concat(string_0, ".bak");
                    string01 = string_0;
                }
                string0 = string_0;
            }
            return string0;
        }
    }
}