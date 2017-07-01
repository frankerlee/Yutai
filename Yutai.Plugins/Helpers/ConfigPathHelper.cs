using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Yutai.Shared;

namespace Yutai.Plugins.Helpers
{
    public static class ConfigPathHelper
    {
        private const string AppName = "YutaiGIS";

        public static string GetConfigPath()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string path = Path.Combine(folder, AppName);

            CreateIfNotExists(path);

            return path;
        }

        private static void CreateIfNotExists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Logger.Current.Warn("Failed to create folder: " + path, ex);
            }
        }

        public static string GetConfigFilePath()
        {
            return GetConfigPath() + @"\ytconfig.xml";
        }

        public static string GetRepositoryConfigPath()
        {
            return GetConfigPath() + @"\repository.xml";
        }

        public static string GetDockingConfigPath()
        {
            return GetConfigPath() + @"\Toolbars\dockstate";
        }

        public static string GetToolbarConfigPath()
        {
            return GetConfigPath() + @"\Toolbars\toolbars";
        }

        public static string GetToolsConfigPath()
        {
            string path = GetConfigPath() + @"\Tools\";
            CreateIfNotExists(path);
            return path;
        }

        public static string GetDriversConfigPath()
        {
            string path = GetConfigPath() + @"\Drivers\";
            CreateIfNotExists(path);
            return path;
        }

        public static string GetWmsCachePath()
        {
            string path = GetConfigPath() + @"\WMS\";
            CreateIfNotExists(path);
            return path;
        }

        public static string GetLayoutPath()
        {
            string path = GetConfigPath() + @"\Layout\";
            CreateIfNotExists(path);
            return path;
        }
    }
}