using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Views;

namespace Yutai.Plugins.Scene.Menu
{
    public class SceneViewService
    {
        private IAppContext _context;
        private SceneViewPresenter _presenter;
        private ScenePlugin _plugin;
        private bool _isShow=false;

        public SceneViewService(IAppContext context, SceneViewPresenter presenter, ScenePlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            _context = context;
            _presenter = presenter;
            _plugin = plugin;

            var panels = context.DockPanels;
        }

        public ISceneView SceneView
        {
            get { return _presenter.View; }
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
               // ((ISceneView)_presenter.GetInternalObject()).SetBuddyControl();
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
