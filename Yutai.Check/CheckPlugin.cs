using Yutai.Check.Menu;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Check
{
    [YutaiPlugin()]
    public class CheckPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private IPipelineConfig _config;
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
                string fileName =
                    ((ISecureContext)_context).YutaiProject.FindPlugin("c58d568b-9dee-4a35-b29b-dad2c92f0188")
                        .ConfigXML;
                if (string.IsNullOrEmpty(fileName)) return;
                fileName = FileHelper.GetFullPath(fileName);
                _config.LoadFromXml(fileName);
            }
            //_menuListener = context.Container.GetInstance<MenuListener>();
            //_mapListener = context.Container.GetInstance<MapListener>();
            // _dockPanelService = context.Container.GetInstance<TemplateDockPanelService>();
        }
    }
}
