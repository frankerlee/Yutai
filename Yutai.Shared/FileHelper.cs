using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Shared
{
    public class FileHelper
    {
        public static string GetRelativePath(string relativeFile, string fileName)
        {
            FileInfo fileInfo = new FileInfo(relativeFile);
            return fileInfo.DirectoryName + "\\" + fileName;
        }

        public static string GetFullPath(string pluginMenuXml)
        {
            string appPath = Application.StartupPath;
            return appPath + pluginMenuXml;
        }
    }
}