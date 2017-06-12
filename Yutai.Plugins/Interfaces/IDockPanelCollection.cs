using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Interfaces
{
    public interface IDockPanelCollection : IEnumerable<IDockPanelView>
    {
        void Lock();
        void Unlock();
        bool Locked { get; }
        DockPanel Add(IDockPanelView view,  PluginIdentity identity);
        void Remove(string panelName, PluginIdentity identity);
        void RemoveItemsForPlugin(PluginIdentity identity);
        DockPanel GetDockPanel(string key);
        void ShowDockPanel(string dockName, bool isVisible, bool isActive);
        void SetActivePanel(string dockName);
        bool GetDockVisible(string dockName);
    }
}
