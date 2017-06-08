using System.IO;

namespace Yutai.ArcGIS.Common
{
    internal class PahtAssistant
    {
        public PahtAssistant()
        {
        }

        public static string GetFinalFileName(string string_0, string string_1)
        {
            int num = 1;
            string str = string.Concat(string_0, string_1);
            while (File.Exists(str))
            {
                str = string.Concat(string_0, "_", num.ToString(), string_1);
                num++;
            }
            return str;
        }
    }
}