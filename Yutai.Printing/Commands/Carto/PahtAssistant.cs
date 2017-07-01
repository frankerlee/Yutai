using System;
using System.IO;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public class PahtAssistant
    {
        public static string GetFinalFileName(string string_0, string string_1)
        {
            int num = 1;
            string text = string_0 + string_1;
            while (File.Exists(text))
            {
                text = string_0 + "_" + num.ToString() + string_1;
                num++;
            }
            return text;
        }
    }
}