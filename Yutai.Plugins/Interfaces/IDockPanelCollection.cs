using System.Collections.Generic;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Interfaces
{
    public interface IDockPanelCollection : IEnumerable<IDockPanel>
    {
        void Lock();
        void Unlock();
        bool Locked { get; }
        IDockPanel Add(Control control, string key, PluginIdentity identity);
        void Remove(IDockPanel panel, PluginIdentity identity);
        void RemoveItemsForPlugin(PluginIdentity identity);
        IDockPanel Find(string key);
        IDockPanel MapLegend { get; }
        IDockPanel Preview { get; }
        IDockPanel Toolbox { get; }
    }
}
