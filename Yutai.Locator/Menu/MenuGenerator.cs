using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;
using Yutai.Shared;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.Plugins.Locator.Menu
{
    internal class MenuGenerator
    {
        private readonly IAppContext _context;
        private readonly YutaiCommands _commands;
        private readonly object _menuManager;
        private readonly LocatorPlugin _plugin;


        public MenuGenerator(IAppContext context, LocatorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            // if (pluginManager == null) throw new ArgumentNullException("pluginManager");

            _plugin = plugin;
            _context = context;
            _menuManager = _context.MainView.RibbonManager;
            _commands = new YutaiCommands(_context, plugin.Identity);
            _commands.Plugin = plugin;
            InitMenus();
        }

        private void InitMenus()
        {
            XmlDocument doc = new XmlDocument();
            //检测项目文档里面是否有插件的界面配置，如果没有，则使用默认配置，如果有，则使用配置文件里面的配置
            Guid dllGuid = new Guid("2b81c89a-ee45-4276-9dc1-72bbbf07f53f");
            XmlPlugin plugin =
                ((ISecureContext) _context).YutaiProject.Plugins.FirstOrDefault(
                    c => c.Guid == dllGuid);
            if (plugin != null)
            {
                if (string.IsNullOrEmpty(plugin.MenuXML))
                {
                    doc.Load(
                        base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Locator.Menu.MenuLayout.xml"));
                }
                else
                {
                    FileInfo info = new FileInfo(FileHelper.GetFullPath(plugin.MenuXML));
                    if (info.Exists)
                        doc.Load(FileHelper.GetFullPath(plugin.MenuXML));
                    else
                        doc.Load(
                            base.GetType()
                                .Assembly.GetManifestResourceStream("Yutai.Plugins.Locator.Menu.MenuLayout.xml"));
                }
            }
            else
            {
                doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Locator.Menu.MenuLayout.xml"));
            }

            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControl) _menuManager,
                _context.MainView.RibbonStatusBar as RibbonStatusBar, doc);
        }
    }
}