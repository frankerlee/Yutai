// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  MenuGenerator.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/05/31  18:05
// 更新时间 :  2017/05/31  18:05

using System;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.Plugins.Bookmark.Menu
{
    internal class MenuGenerator
    {
        private readonly YutaiCommands _commands;
        private readonly IAppContext _context;
        private readonly object _menuManager;
        private readonly BookmarkPlugin _plugin;

        public MenuGenerator(IAppContext context, BookmarkPlugin plugin)
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
            doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Bookmark.Menu.MenuLayout.xml"));
            RibbonFactory.CreateMenus(_commands.GetCommands(),  (RibbonControl) _menuManager, _context.MainView.RibbonStatusBar as RibbonStatusBar, doc);
        }
    }
}