using System.Runtime.CompilerServices;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal class AppConfig
    {
        internal static string database = "";
        internal static string instance = "";
        internal static string layerconfigdb = "";
        internal static XmlDocument m_AppConfig = new XmlDocument();
        internal static IWorkspace m_pWorkspace = null;
        internal static string m_strConfigfile = null;
        internal static string oledb = "";
        internal static string oledbdatabase = "";
        internal static string oledbpassword = "";
        internal static string oledbserver = "";
        internal static string oledbuser = "";
        internal static string password = "";
        internal static string server = "";
        internal static string user = "";
        internal static string version = "";

        internal static string authentication_mode { get; set; }

        internal static string dbclient { get; set; }
    }
}