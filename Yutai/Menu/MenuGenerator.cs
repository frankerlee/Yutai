using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Shared;
using Yutai.UI.Menu;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.Menu
{
    internal class MenuGenerator
    {
        private const string FileToolbarNane = "File";
        private const string MapToolbarName = "Map";
        private const string DefaultXMLLayout = "MainMenu.xml";

        private readonly IAppContext _context;
        private readonly IPluginManager _pluginManager;
        private readonly YutaiCommands _commands;
        private readonly object _menuManager;
        private readonly object _dockingManager;
        private readonly object _statusManager;

        public MenuGenerator(IAppContext context, IPluginManager pluginManager, IMainView mainView)
        {
            if (context == null) throw new ArgumentNullException("context");
           // if (pluginManager == null) throw new ArgumentNullException("pluginManager");
            if (mainView == null) throw new ArgumentNullException("mainView");

            _context = context;
            _pluginManager = pluginManager;
            _menuManager = mainView.RibbonManager;
            _dockingManager = mainView.DockingManager;
            _commands = new YutaiCommands(_context,PluginIdentity.Default);
            _statusManager = mainView.RibbonStatusBar;
            InitMenus();
        }

        public List<string> GetMenuKeys()
        {
            return _commands.GetKeys();
        }
        private void InitMenus()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Menu.MenuLayout.xml"));
            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControl) _menuManager, (RibbonStatusBar)_statusManager,doc);

        }
        
    }
}
