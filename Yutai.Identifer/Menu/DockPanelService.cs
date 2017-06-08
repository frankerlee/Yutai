using System;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Properties;
using Yutai.Plugins.Identifer.Views;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Menu
{
    public class DockPanelService
    {
        public DockPanelService(IAppContext context, IdentifierPresenter presenter, IdentifierPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            var panels = context.DockPanels;

            panels.Lock();
            var panel = panels.Add(presenter.GetInternalObject() as IDockPanelView,  plugin.Identity);
            panels.Unlock();
        }
    }
}