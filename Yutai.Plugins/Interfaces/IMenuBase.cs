using Yutai.Plugins.Concrete;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IMenuBase : IToolbar
    {
        IMenuItem FindItem(string key, PluginIdentity identity);

        void RemoveItemsForPlugin(PluginIdentity identity);
    }
}