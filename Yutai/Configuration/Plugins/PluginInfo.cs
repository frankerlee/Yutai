using System;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;

namespace Yutai.Configuration.Plugins
{
    public class PluginInfo
    {
        private BasePlugin _plugin;

        public PluginInfo(BasePlugin plugin, bool selected)
        {
            if (plugin == null) throw new ArgumentNullException("plugin");
            _plugin = plugin;
            Selected = selected;
        }


        public bool Selected { get; set; }

        public string Name
        {
            get { return _plugin.Identity.Name; }
        }

        public string Author
        {
            get { return _plugin.Identity.Author; }
        }

        internal BasePlugin BasePlugin
        {
            get { return _plugin; }
        }
    }
}
