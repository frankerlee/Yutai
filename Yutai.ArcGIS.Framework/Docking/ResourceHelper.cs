namespace Yutai.ArcGIS.Framework.Docking
{
    internal static class ResourceHelper
    {
        private static System.Resources.ResourceManager _resourceManager = null;

        public static string GetString(string name)
        {
            return ResourceManager.GetString(name);
        }

        private static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new System.Resources.ResourceManager("JLK.WinFormsUI.Docking.Strings",
                        typeof(ResourceHelper).Assembly);
                }
                return _resourceManager;
            }
        }
    }
}