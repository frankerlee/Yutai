using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using Yutai.Check.Views;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Services
{
    public class CheckResultDockPanelService
    {
        private IAppContext _context;
        private CheckResultPresenter _presenter;
        private CheckPlugin _plugin;

        public CheckResultDockPanelService(IAppContext context, CheckResultPresenter presenter, CheckPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            _context = context;
            _presenter = presenter;
            _plugin = plugin;
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
                panel = AddPanel();
            _context.DockPanels.ShowDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName, true, true);
        }

        public bool Visible
            => _context.DockPanels.GetDockVisible(((IDockPanelView)_presenter.GetInternalObject()).DockName);

        public void Hide()
        {
            DockPanel panel =
                _context.DockPanels.GetDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName);
            if (panel == null)
                return;
            _context.DockPanels.ShowDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName, false, false);
        }

        public ICheckResultView View => _presenter.View;
    }
}
