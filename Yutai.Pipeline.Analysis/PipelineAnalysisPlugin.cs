using System;
using Yutai.Pipeline.Analysis.Menu;
using Yutai.Pipeline.Analysis.Services;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Pipeline.Analysis
{
    [YutaiPlugin()]
    public class PipelineAnalysisPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private IPipelineConfig _config;
        private DockPanelService _dockPanelService;

        public event EventHandler<QueryResultArgs> QueryResultChanged;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            _config = context.Container.GetSingleton<PipelineConfig>();
            _dockPanelService = context.Container.GetInstance<DockPanelService>();
            if (string.IsNullOrEmpty(_config.XmlFile))
            {
                string fileName = ((ISecureContext)_context).YutaiProject.FindPlugin("f804e812-481e-45c3-be08-749da82075d1").ConfigXML;
                if (string.IsNullOrEmpty(fileName)) return;
                fileName = FileHelper.GetFullPath(fileName);
                _config.LoadFromXml(fileName);
            }
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

        public IPipelineConfig PipeConfig
        {
            get { return _config; }
        }
        public void FireQueryResultChanged(QueryResultArgs e)
        {
            FireEvent(QueryResultChanged, e);
        }
    }
}