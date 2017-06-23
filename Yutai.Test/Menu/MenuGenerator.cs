// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  MenuGenerator.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/05/31  18:05
// 更新时间 :  2017/05/31  18:05

using System;
using System.IO;
using System.Linq;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;
using Yutai.Shared;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.Test.Menu
{
    internal class MenuGenerator
    {
        private readonly YutaiCommands _commands;
        private readonly IAppContext _context;
        private readonly object _menuManager;
        private readonly TestPlugin _plugin;

        public MenuGenerator(IAppContext context, TestPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (plugin == null) throw new ArgumentNullException("plugin");

            _context = context;
            _plugin = plugin;
            _menuManager = _context.MainView.RibbonManager;
            _commands = new YutaiCommands(_context, plugin.Identity);
            InitMenu();

        }

        private void InitMenu()
        {
            XmlDocument doc = new XmlDocument();
            //检测项目文档里面是否有插件的界面配置，如果没有，则使用默认配置，如果有，则使用配置文件里面的配置
            Guid dllGuid = new Guid("f9bff4cd-5840-48c1-9a7b-2385c554d95b");
            XmlPlugin plugin =((ISecureContext)_context).YutaiProject.Plugins.FirstOrDefault(c => c.Guid == dllGuid);
            if (plugin != null)
            {
                if (string.IsNullOrEmpty(plugin.MenuXML))
                {
                    doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Test.Menu.MenuLayout.xml"));
                }
                else
                {
                    FileInfo info = new FileInfo(FileHelper.GetFullPath(plugin.MenuXML));
                    if (info.Exists)
                        doc.Load(FileHelper.GetFullPath(plugin.MenuXML));
                    else
                        doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Test.Menu.MenuLayout.xml"));
                }
            }
            else
            {
                doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Test.Menu.MenuLayout.xml"));
            }
            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControl)_menuManager, _context.MainView.RibbonStatusBar as RibbonStatusBar, doc);
        }
    }
}