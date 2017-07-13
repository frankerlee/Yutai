using System;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Scene.Views
{
    public class SceneViewPresenter : CommandDispatcher<ISceneView, SceneViewCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private ScenePlugin _plugin;
    

        public SceneViewPresenter(IAppContext context, ISceneView view, ScenePlugin plugin)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _plugin = plugin;
            view.Initialize(_context, _plugin);
        }
        
        public Control GetInternalObject()
        {
            return View as Control;
        }
        
        public override void RunCommand(SceneViewCommand command)
        {
            switch (command)
            {
                case SceneViewCommand.Close:
                    // View.Clear();
                    break;
            }
        }

        private void ActivatePanel()
        {
            var panel = _context.DockPanels.GetDockPanel(((IDockPanelView)View).DockName);
            if (panel != null)
            {
                panel.Visible = true;
                _context.DockPanels.SetActivePanel(((IDockPanelView)View).DockName);
            }
        }

    }
}