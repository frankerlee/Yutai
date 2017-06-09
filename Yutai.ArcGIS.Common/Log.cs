using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Yutai.ArcGIS.Common
{
    public class Log
    {
        public static void backupLog(string string_0)
        {
            try
            {
                if (File.Exists(string_0))
                {
                    string destFileName = string_0 + "_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".bak";
                    File.Copy(string_0, destFileName, true);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);
            }
        }

        public static void clearLog(string string_0)
        {
            StreamWriter writer = null;
            Exception exception;
            try
            {
                if (File.Exists(string_0))
                {
                    writer = File.CreateText(string_0);
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

        public static void deleteLog(string string_0, bool bool_0)
        {
            try
            {
                if (File.Exists(string_0))
                {
                    if (bool_0)
                    {
                        backupLog(string_0);
                    }
                    File.Delete(string_0);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);
            }
        }

        public static void writeLog(string string_0, string[] string_1)
        {
            StreamWriter writer = null;
            Exception exception;
            try
            {
                writer = File.AppendText(string_0);
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
                for (int i = 0; i < string_1.Length; i++)
                {
                    writer.WriteLine(string_1[i]);
                }
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

        public static void writeLog(string string_0, IList ilist_0)
        {
            StreamWriter writer = null;
            Exception exception;
            try
            {
                writer = File.AppendText(string_0);
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
                for (int i = 0; i < ilist_0.Count; i++)
                {
                    writer.WriteLine(ilist_0[i].ToString());
                }
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

        public static void writeLog(string string_0, string string_1)
        {
            StreamWriter writer = null;
            Exception exception;
            try
            {
                writer = File.AppendText(string_0);
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
                writer.WriteLine(string_1);
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
    }
}

