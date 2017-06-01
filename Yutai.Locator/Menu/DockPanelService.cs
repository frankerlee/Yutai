using System;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Locator.Properties;
using Yutai.Plugins.Locator.Views;

namespace Yutai.Plugins.Locator.Menu
{
    public class DockPanelService
    {
        public DockPanelService(IAppContext context, LocatorPresenter presenter, LocatorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            var panels = context.DockPanels;

            panels.Lock();
            var panel = panels.Add(presenter.GetInternalObject(), Yutai.UI.Docking.DockPanelKeys.Locator, plugin.Identity);
            panel.Caption = "定位器";
            panel.TabPosition = 3;
            panel.SetIcon(Resources.Locator);
            

            var mapLegend = panels.MapLegend;
            if (mapLegend != null && mapLegend.Visible)
            {
                panel.DockTo(mapLegend, DockPanelState.Tabbed, 150);
            }

            panels.Unlock();
        }
    }
}