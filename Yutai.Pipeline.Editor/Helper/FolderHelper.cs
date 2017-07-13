using System.Collections.Generic;
using System.IO;

namespace Yutai.Pipeline.Editor.Helper
{
    public class FolderHelper
    {
        public static List<FileInfo> GetFileInfos(string folderPath, List<string> extension = null, string filter = null)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            return GetFileInfos(directoryInfo, extension, filter);
        }

        public static List<FileInfo> GetFileInfos(DirectoryInfo directoryInfo, List<string> extension = null, string filter = null)
        {
            List<FileInfo> list = new List<FileInfo>();

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (extension != null && extension.Count > 0 && !extension.Contains(fileInfo.Extension.ToLower()))
                    continue;
                if (!string.IsNullOrEmpty(filter) && !fileInfo.Name.ToLower().Contains(filter.ToLower()))
                    continue;
                list.Add(fileInfo);
            }

            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                list.AddRange(GetFileInfos(directory, extension, filter));
            }

            return list;
        }
    }
}
