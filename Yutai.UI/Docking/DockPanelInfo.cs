using System;
using Yutai.Plugins.Concrete;

namespace Yutai.UI.Docking
{
    internal class DockPanelInfo
    {
        public DockPanelInfo(PluginIdentity identity, string key)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new NullReferenceException("");
            }
            Identity = identity;
            Key = key;
        }
        public PluginIdentity Identity { get; set; }
        public string Key { get; set; }
    }
}