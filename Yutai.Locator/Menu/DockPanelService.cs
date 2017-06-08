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
            var panel = panels.Add(presenter.GetInternalObject() as IDockPanelView, plugin.Identity);

            panels.Unlock();
        }
    }
}