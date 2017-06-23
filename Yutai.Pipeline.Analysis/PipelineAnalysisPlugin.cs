using System;
using Yutai.Pipeline.Analysis.Menu;
using Yutai.Plugins.Catalog;
using Yutai.Plugins.Catalog.Menu;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Analysis
{
    [YutaiPlugin()]
    public class PipelineAnalysisPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        
      public event EventHandler<QueryResultArgs>QueryResultChanged;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();

            //_menuListener = context.Container.GetInstance<MenuListener>();
            //_mapListener = context.Container.GetInstance<MapListener>();
            // _dockPanelService = context.Container.GetInstance<TemplateDockPanelService>();
        }

        private void FireEvent<T>(EventHandler<T> handler, T args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }


        public void FireQueryResultChanged(QueryResultArgs e)
        {
            FireEvent(QueryResultChanged, e);
        }
    }
}