using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Shared
{
    public class FileHelper
    {
        public static string GetRelativePath(string relativeFile, string fileName)
        {
            FileInfo fileInfo=new FileInfo(relativeFile);
            return fileInfo.DirectoryName + "\\" + fileName;
        }
    }
}
