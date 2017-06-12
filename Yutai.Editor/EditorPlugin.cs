using System;
using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Controls;
using Yutai.Plugins.Editor.Menu;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor
{
    [YutaiPlugin()]
    public class EditorPlugin : BasePlugin
    {
        private IAppContext _context;
        private TemplateDockPanelService _dockPanelService;
       
        private MenuGenerator _menuGenerator;
        //private MapListener _mapListener;
        private EditorSettings _querySettings;

        public EditorSettings EditorSettings
        {
            get { if (_querySettings == null) { _querySettings=new EditorSettings();}
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
            _dockPanelService = context.Container.GetInstance<TemplateDockPanelService>();
        }

        private void FireEvent<T>(EventHandler<T> handler, T args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }
        public override IEnumerable<IConfigPage> ConfigPages
        {
            get { yield return _context.Container.GetInstance<SnapConfigPage>(); }
        }
    }
}
