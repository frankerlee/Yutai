using System;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Locator.Properties;
using Yutai.Plugins.Locator.Views;

namespace Yutai.Plugins.Locator.Menu
{
    public class DockPanelService
    {
        private IAppContext _context;
        private LocatorPresenter _presenter;
        private LocatorPlugin _plugin;
        public DockPanelService(IAppContext context, LocatorPresenter presenter, LocatorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");
            _context = context;
            _presenter = presenter;
            _plugin = plugin;
            var panels = context.DockPanels;

            //panels.Lock();
            //var panel = panels.Add(presenter.GetInternalObject() as IDockPanelView, plugin.Identity);

            //panels.Unlock();
        }

        public DockPanel AddPanel()
        {
            //  ((IdentifierDockPanel)_presenter.GetInternalObject()).LinkMap();
            return _context.DockPanels.Add(_presenter.GetInternalObject() as IDockPanelView, _plugin.Identity);
        }

        public void Show()
        {
            DockPanel panel =
                _context.DockPanels.GetDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName);
            if (panel == null)
            {
                panel = AddPanel();
            }
            else
            {
                // ((IdentifierDockPanel)_presenter.GetInternalObject()).ValidateWorkspace();
            }
            _context.DockPanels.ShowDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName, true, true);
        }

        public bool Visible
        {
            get
            {
                return _context.DockPanels.GetDockVisible(((IDockPanelView)_presenter.GetInternalObject()).DockName);
            }
        }

        public void Hide()
        {
            DockPanel panel =
                _context.DockPanels.GetDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName);
            if (panel == null)
            {
                return;
            }
            _context.DockPanels.ShowDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName, false, false);
        }
    }
}