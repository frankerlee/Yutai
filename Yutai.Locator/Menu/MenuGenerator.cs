using System;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Interfaces;
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
            _commands = new YutaiCommands(_context,plugin.Identity);
            _commands.Plugin = plugin;
            InitMenus();
        }

        private void InitMenus()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Locator.Menu.MenuLayout.xml"));
            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControl) _menuManager,doc);

        }

        

     

      
    }
}
