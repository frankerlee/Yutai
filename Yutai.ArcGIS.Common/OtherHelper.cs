using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;

namespace Yutai.ArcGIS.Common
{
    public class OtherHelper
    {
        public OtherHelper()
        {
        }

        public static void ReleaseObject(object o)
        {
            ComReleaser.ReleaseCOMObject(o);
            Marshal.ReleaseComObject(o);
            GC.Collect();
        }
        public static string GetLeftName(string sName)
        {
            string str;
            str = (!(sName == "") ? sName.Substring(0, sName.IndexOf("|")).Trim() : "");
            return str;
        }

        public static string GetLeftName(string sName, string sFlag)
        {
            string str;
            if (!(sName == ""))
            {
                str = (sName.IndexOf(sFlag) != -1 ? sName.Substring(0, sName.IndexOf(sFlag)).Trim() : sName);
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static string GetRightName(string sName)
        {
            string str;
            str = (!(sName == "") ? sName.Substring(sName.IndexOf("|") + 1, sName.Length - sName.IndexOf("|") - 1).Trim() : "");
            return str;
        }

        public static string GetRightName(string sName, string sFlag)
        {
            string str;
            if (!(sName == ""))
            {
                str = (sName.IndexOf(sFlag) != -1 ? sName.Substring(sName.IndexOf(sFlag) + 1, sName.Length - sName.IndexOf(sFlag) - 1).Trim() : sName);
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static bool OnlyRunOncetime(Form frm, string sProductName)
        {
            bool flag;
            bool flag1 = false;
            Mutex mutex = new Mutex(true, "OnlyRunOncetime", out flag1);
            if (!flag1)
            {
                MessageBoxHelper.ShowMessageBox(string.Concat(sProductName, "已经启动"));
                flag = false;
            }
            else
            {
                Application.Run(frm);
                mutex.ReleaseMutex();
                flag = true;
            }
            return flag;
        }
    }
}