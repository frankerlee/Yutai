using System;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Template.Views
{
    public class TemplateViewPresenter : CommandDispatcher<ITemplateView, TemplateViewCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
   
        private TemplatePlugin _plugin;
       


        public TemplateViewPresenter(IAppContext context, ITemplateView view, TemplatePlugin plugin)
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


        public override void RunCommand(TemplateViewCommand command)
        {
            switch (command)
            {
                case TemplateViewCommand.Close:
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

        private void OnIdentifierModeChanged()
        {
            //MessageBox.Show("Mode Changed");
            // View.Identify(null);
        }

        private void OnItemSelected()
        {
        }
    }
}