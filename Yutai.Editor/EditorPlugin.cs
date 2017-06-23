using System;
using System.Collections.Generic;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Controls;
using Yutai.Plugins.Editor.Menu;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Services.Serialization;
using Yutai.Shared;

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
        private IPipelineConfig _pipelineConfig;

        public EditorSettings EditorSettings
        {
            get
            {
                if (_querySettings == null)
                {
                    _querySettings = new EditorSettings();
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
            _dockPanelService = context.Container.GetInstance<TemplateDockPanelService>();

            //获取配置对象
            _pipelineConfig = context.Container.GetSingleton<PipelineConfig>();
            if (string.IsNullOrEmpty(_pipelineConfig.XmlFile))
            {
                string fileName = ((ISecureContext) _context).YutaiProject.FindPlugin("4a3bcaab-9d3e-4ca7-a19d-7ee08fb0629e").ConfigXML;
                fileName = FileHelper.GetFullPath(fileName);
                _pipelineConfig.LoadFromXml(fileName);
            }
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