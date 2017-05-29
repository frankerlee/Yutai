using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Locator.Views;
using Yutai.Shared;
using Yutai.UI.Docking;

namespace Yutai.Plugins.Locator
{
    internal class ProjectListener
    {
        private readonly IAppContext _context;
        LocatorPresenter _presenter;

        public ProjectListener(IAppContext context, LocatorPlugin plugin, LocatorPresenter presenter)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (plugin == null) throw new ArgumentNullException("plugin");
            if (presenter == null) throw new ArgumentNullException("presenter");

            _context = context;
            _presenter = presenter;
            plugin.ProjectClosed += OnProjectClosed;
            plugin.ProjectOpened += Plugin_ProjectOpened;
        }

        private void Plugin_ProjectOpened(object sender, EventArgs e)
        {
            _presenter.View.LoadLocators();
        }


        private void OnProjectClosed(object sender, EventArgs e)
        {
            var panel = _context.DockPanels.Find(DockPanelKeys.Locator);
            if (panel.Visible)
            {
                panel.Visible = false;
            }
        }
    }
}