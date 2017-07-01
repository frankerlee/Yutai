using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IToolbarCollectionBase : IEnumerable<IToolbar>
    {
        IToolbar Add(string name, PluginIdentity identity);

        IToolbar Add(string name, string key, PluginIdentity identity);

        void Remove(int toolbarIndex);

        IToolbar this[int toolbarIndex] { get; }

        IMenuItem FindItem(string key, PluginIdentity identity);

        void RemoveItemsForPlugin(PluginIdentity identity);

        IEnumerable<IMenuItem> ItemsForPlugin(PluginIdentity identity);
    }
}