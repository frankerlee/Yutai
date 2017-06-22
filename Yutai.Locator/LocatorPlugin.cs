using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Locator.Menu;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Locator
{
    [YutaiPlugin()]
    public class LocatorPlugin : BasePlugin
    {
        private IAppContext _context;
        public event EventHandler<EventArgs> StartLocator;
        private DockPanelService _dockPanelService;
        private MenuGenerator _menuGenerator;
        private ProjectListener _projectListener;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            _dockPanelService = context.Container.GetInstance<DockPanelService>();
            _projectListener = context.Container.GetInstance<ProjectListener>();
           // IPluginManager plugin = _context.Container.GetSingleton<IPluginManager>();

        }

       
      
        public void FireStartLocator(EventArgs e)
        {
            FireEvent(StartLocator, e);
        }

        private void FireEvent<T>(EventHandler<T> handler, T args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}