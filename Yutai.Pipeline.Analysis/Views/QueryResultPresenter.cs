using System;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Analysis.Views
{
    public class QueryResultPresenter : CommandDispatcher<IQueryResultView, QueryResultViewCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private PipelineAnalysisPlugin _plugin;

        #region Constructors

        public QueryResultPresenter(IAppContext context, IQueryResultView view, PipelineAnalysisPlugin plugin)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _plugin = plugin;
            View.Initialize(context);
            _plugin.QueryResultChanged+= PluginOnQueryResultChanged;
           
        }
        private void ActivatePanel()
        {
            var panel = _context.DockPanels.GetDockPanel(((IDockPanelView)View).DockName);
            if (panel == null)
            {
                panel = _context.DockPanels.Add(GetInternalObject() as IDockPanelView, _plugin.Identity);
            }
            panel.Visible = true;
            _context.DockPanels.SetActivePanel(((IDockPanelView) View).DockName);
            
        }
        private void PluginOnQueryResultChanged(object sender, QueryResultArgs queryResultArgs)
        {
            if (queryResultArgs == null)
                View.SetResult(null, null);
            else
            {
                ActivatePanel();
                View.SetResult(queryResultArgs.Cursor, queryResultArgs.Selection);
            }
        }

        #endregion

        #region Properties

        #endregion

        public Control GetInternalObject()
        {
            return View as Control;
        }

        public override void RunCommand(QueryResultViewCommand command)
        {
            return;
        }
    }
}