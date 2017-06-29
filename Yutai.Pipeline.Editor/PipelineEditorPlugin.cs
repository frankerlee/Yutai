using System;

using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Menu;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Pipeline.Editor
{
    [YutaiPlugin()]
    public class PipelineEditorPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private IPipelineConfig _config;
        //private DockPanelService _dockPanelService;

       // public event EventHandler<QueryResultArgs> QueryResultChanged;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            _config = context.Container.GetSingleton<PipelineConfig>();
            //_dockPanelService = context.Container.GetInstance<DockPanelService>();
            if (string.IsNullOrEmpty(_config.XmlFile))
            {
                string fileName = ((ISecureContext)_context).YutaiProject.FindPlugin("95ab64f9-65ca-400f-8f68-c18cadff1421").ConfigXML;
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
        //public void FireQueryResultChanged(QueryResultArgs e)
        //{
        //    FireEvent(QueryResultChanged, e);
        //}
    }
}