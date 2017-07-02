using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Printing.Forms;
using Yutai.Plugins.Printing.Menu;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Plugins.Printing
{
    [YutaiPlugin()]
    public class PrintingPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private IPrintingConfig _printConfig;
        private MapTemplateGallery _templateGallery;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
            _printConfig = new PrintingConfig() as IPrintingConfig;
            FileInfo newFileInfo = new FileInfo(System.IO.Path.Combine(Application.StartupPath,
                "plugins\\configs\\MapTemplate.mdb"));
            _printConfig.TemplateConnectionString = newFileInfo.Exists ? newFileInfo.FullName : "";
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            ISecureContext secureContext=context as ISecureContext;
            if (secureContext.YutaiProject != null)
            {
                XmlPlugin xmlPlugin = secureContext.YutaiProject.FindPlugin("5e933989-b5a4-4a45-a5b7-2d9ded61df0f");
                if (xmlPlugin != null)
                {
                    string fileName = xmlPlugin.ConfigXML;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        _printConfig.TemplateConnectionString = BuildDefaultConnectionString();
                    }
                    else
                    {
                        fileName = FileHelper.GetFullPath(fileName);
                        _printConfig.LoadFromXml(fileName);

                    }
                }
            }
            if (string.IsNullOrEmpty(_printConfig.TemplateConnectionString))
            {
                _printConfig.TemplateConnectionString = BuildDefaultConnectionString();
            }
            _templateGallery =new MapTemplateGallery();
            _templateGallery.SetWorkspace(_printConfig.TemplateConnectionString);
            
            //_menuListener = context.Container.GetInstance<MenuListener>();
            //_mapListener = context.Container.GetInstance<MapListener>();
            //_dockPanelService = context.Container.GetInstance<TemplateDockPanelService>();

            //获取配置对象
            // _pipelineConfig = context.Container.GetSingleton<PipelineConfig>();
            //if (string.IsNullOrEmpty(_pipelineConfig.XmlFile))
            //{
            //    string fileName = ((ISecureContext) _context).YutaiProject.FindPlugin("4a3bcaab-9d3e-4ca7-a19d-7ee08fb0629e").ConfigXML;
            //    if (string.IsNullOrEmpty(fileName)) return;
            //    //fileName = FileHelper.GetFullPath(fileName);
            //    //_pipelineConfig.LoadFromXml(fileName);
            //}
        }

        private string BuildDefaultConnectionString()
        {
            FileInfo fileInfo = new FileInfo(System.IO.Path.Combine(Application.StartupPath + "\\plugins\\configs\\MapTemplate.mdb"));
            if (fileInfo.Exists)
            {
                string ext = fileInfo.Extension.Substring(1);
                return string.Format("dbclient={0};gdbname={1}", ext, fileInfo.FullName);
            }
            return "";
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
            get {
               yield return _context.Container.GetInstance<TemplateConfigPage>(); }
        }

        public  IPrintingConfig PrintingConfig { get { return _printConfig; } set { _printConfig = value; } }

        public MapTemplateGallery TemplateGallery { get { return _templateGallery; } set { _templateGallery = value; } }
    }

    public class PrintLayoutSetting
    {
        public string DefaultTemplateDatabase { get; set; }
    }
}