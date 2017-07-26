using System;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Template.Views;

namespace Yutai.Plugins.Template.Menu
{
    public class TemplateViewService
    {
        private IAppContext _context;
        private TemplateViewPresenter _presenter;
        private TemplatePlugin _plugin;

        public TemplateViewService(IAppContext context, TemplateViewPresenter presenter, TemplatePlugin plugin)
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
                // ((ITemplateView)_presenter.GetInternalObject()).SetBuddyControl();
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