using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Yutai.ArcGIS.Common
{
    public class CErrorLog
    {
        private static string m_ErrorLogFileName;
        private static string m_ErrorLogFullPath;

        static CErrorLog()
        {
            old_acctor_mc();
        }

        public static void backupErrorLog()
        {
            string path = m_ErrorLogFullPath + m_ErrorLogFileName + ".log";
            try
            {
                if (File.Exists(path))
                {
                    string destFileName = m_ErrorLogFullPath + m_ErrorLogFileName + "_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".bak";
                    File.Copy(path, destFileName, true);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);
            }
        }

        public static void clearErrorLog()
        {
            Exception exception;
            string path = m_ErrorLogFullPath + m_ErrorLogFileName + ".log";
            StreamWriter writer = null;
            try
            {
                if (File.Exists(path))
                {
                    writer = File.CreateText(path);
                }
                else
                {
                    return;
                }
            }
            catch (Exception exception1)
            {
                exception = exception1;
                Trace.WriteLine(exception);
                return;
            }
            try
            {
                writer.Write("");
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Trace.WriteLine(exception);
            }
            finally
            {
                writer.Flush();
                writer.Close();
            }
        }

        public static void deleteErrorLog(bool bool_0)
        {
            string path = m_ErrorLogFullPath + m_ErrorLogFileName + ".log";
            try
            {
                if (File.Exists(path))
                {
                    if (bool_0)
                    {
                        backupErrorLog();
                    }
                    File.Delete(path);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);
            }
        }

        private static void old_acctor_mc()
        {
            m_ErrorLogFileName = "ErrorLog";
        }

        public static void writeErrorLog(object object_0, Exception exception_0, string string_0)
        {
            Exception exception;
            string path = m_ErrorLogFullPath + m_ErrorLogFileName + ".log";
            StreamWriter writer = null;
            try
            {
                writer = File.AppendText(path);
            }
            catch (Exception exception1)
            {
                exception = exception1;
                Trace.WriteLine(exception);
                return;
            }
            try
            {
                writer.WriteLine();
                writer.WriteLine("----------------------------------------------");
                writer.WriteLine("错误信息：" + string_0);
                string str2 = "";
                if (object_0 != null)
                {
                    str2 = object_0.ToString();
                }
                writer.WriteLine("对象信息：" + str2);
                string str3 = "";
                if (exception_0 != null)
                {
                    str3 = exception_0.ToString();
                }
                writer.WriteLine("异常信息：" + str3);
                writer.WriteLine("时间：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                writer.WriteLine("主机名：" + Dns.GetHostName());
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Trace.WriteLine(exception);
            }
            finally
            {
                writer.Flush();
                writer.Close();
            }
        }

        public static string ErrorLogFileName
        {
            get
            {
                return m_ErrorLogFileName;
            }
            set
            {
                m_ErrorLogFileName = value;
            }
        }

        public static string ErrorLogFullPath
        {
            get
            {
                return m_ErrorLogFullPath;
            }
            set
            {
                string str = value;
                if (!str.Substring(str.Length - 1).Equals(@"\"))
                {
                    m_ErrorLogFullPath = str.Insert(str.Length, @"\");
                }
                else
                {
                    m_ErrorLogFullPath = str;
                }
            }
        }
    }
}

