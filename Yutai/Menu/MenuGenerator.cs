using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
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

        private readonly IAppContext _context;
        private readonly IPluginManager _pluginManager;
        private readonly YutaiCommands _commands;
        private readonly object _menuManager;
        private readonly object _dockingManager;

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

            InitMenus();
        }

        private void InitMenus()
        {
            RibbonFactory.CreateMenus(_commands.GetCommands(), (RibbonControlAdv) _menuManager);

        }
        
    }
}
