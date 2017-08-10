using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.Check.Menu
{
    internal class MenuGenerator
    {
        private readonly IAppContext _context;
        private readonly YutaiCommands _commands;
        private readonly object _menuManager;
        private readonly CheckPlugin _plugin;


        public MenuGenerator(IAppContext context, CheckPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            // if (pluginManager == null) throw new ArgumentNullException("pluginManager");

            _plugin = plugin;
            _context = context;
            _menuManager = _context.MainView.RibbonManager;
            _commands = new YutaiCommands(_context, plugin);
            _commands.Plugin = plugin;
            InitMenus();
        }

        public List<string> GetMenuKeys()
        {
            return _commands.GetKeys();
        }

        private void InitMenus()
        {
            XmlDocument doc = new XmlDocument();
            Assembly assembly = base.GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream(@"Yutai.Check.Menu.MenuLayout.xml");
            if (stream != null) doc.Load(stream);
            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControl)_menuManager,
                _context.MainView.RibbonStatusBar as RibbonStatusBar, doc);
        }
    }
}
