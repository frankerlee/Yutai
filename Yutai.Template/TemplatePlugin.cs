using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Template.Concretes;
using Yutai.Plugins.Template.Interfaces;
using Yutai.Plugins.Template.Menu;
using Yutai.Services.Serialization;

namespace Yutai.Plugins.Template
{
    [YutaiPlugin()]
    public class TemplatePlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private ITemplateDatabase _templateDatabase;
     

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
            FileInfo newFileInfo = new FileInfo(Application.StartupPath+"\\plugins\\configs\\YutaiTemplate.mdb");
            _templateDatabase=new TemplateDatabase();
            if (newFileInfo.Exists)
            {
                _templateDatabase.DatabaseName = newFileInfo.FullName;
            }
            //_printConfig.TemplateConnectionString = newFileInfo.Exists ? newFileInfo.FullName : "";
        }

        public ITemplateDatabase TemplateDatabase { get { return _templateDatabase; } }
        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            ISecureContext secureContext=context as ISecureContext;
            if (secureContext.YutaiProject != null)
            {
                XmlPlugin xmlPlugin = secureContext.YutaiProject.FindPlugin("2fd39cff-e0d7-44cb-a6f7-b5e90499124c");
                if (xmlPlugin != null)
                {
                    string fileName = xmlPlugin.ConfigXML;
                    //if (string.IsNullOrEmpty(fileName))
                    //{
                    //    _printConfig.TemplateConnectionString = BuildDefaultConnectionString();
                    //}
                    //else
                    //{
                    //    fileName = FileHelper.GetFullPath(fileName);
                    //    _printConfig.LoadFromXml(fileName);

                    //}
                }
            }
            //if (string.IsNullOrEmpty(_printConfig.TemplateConnectionString))
            //{
            //    _printConfig.TemplateConnectionString = BuildDefaultConnectionString();
            //}
            //_templateGallery =new MapTemplateGallery();
            //_templateGallery.SetWorkspace(_printConfig.TemplateConnectionString);
            
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
                yield break;
                /* yield return _context.Container.GetInstance<TemplateConfigPage>();*/
            }
        }
        
    }

   
}