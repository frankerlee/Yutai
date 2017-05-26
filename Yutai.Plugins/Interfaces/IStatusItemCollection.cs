using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Interfaces
{
    public interface IStatusItemCollection : IMenuItemCollection
    {
        IMenuItem AddProgressBar(string key, PluginIdentity identity);
        IDropDownMenuItem AddSplitButton(string text, string key, PluginIdentity identity);
    }
}