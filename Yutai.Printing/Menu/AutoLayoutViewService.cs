using System;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Views;

namespace Yutai.Plugins.Printing.Menu
{
    public class AutoLayoutViewService
    {
        private IAppContext _context;
        private AutoLayoutPresenter _presenter;
        private PrintingPlugin _plugin;

        public AutoLayoutViewService(IAppContext context,AutoLayoutPresenter presenter, PrintingPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            _context = context;
            _presenter = presenter;
            _plugin = plugin;

            var panels = context.DockPanels;
        }

        public DockPanel AddPanel()
        {
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
                ((IAutoLayoutView)_presenter.GetInternalObject()).SetBuddyControl();
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