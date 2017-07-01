using System;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Identifer.Menu;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Identifer
{
    [YutaiPlugin()]
    public class IdentifierPlugin : BasePlugin
    {
        private IAppContext _context;
        public event EventHandler<MapIdentifyArgs> MapIdentifying;
        public event EventHandler<EventArgs> UnMapIdentify;
        public event EventHandler<EventArgs> StartMapIdentify;
        private DockPanelService _dockPanelService;

        private MenuGenerator _menuGenerator;
        //private MapListener _mapListener;
        private QuerySettings _querySettings;

        public QuerySettings QuerySettings
        {
            get
            {
                if (_querySettings == null)
                {
                    _querySettings = new QuerySettings();
                }
                return _querySettings;
            }
        }

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
            _dockPanelService = context.Container.GetInstance<DockPanelService>();
        }

        public void FireMapIdentifying(MapIdentifyArgs e)
        {
            FireEvent(MapIdentifying, e);
        }

        public void FireUnMapIdentify(EventArgs e)
        {
            FireEvent(UnMapIdentify, e);
        }

        public void FireStartMapIdentify(EventArgs e)
        {
            FireEvent(StartMapIdentify, e);
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