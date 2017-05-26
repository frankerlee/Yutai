using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Shared;
using Yutai.UI.Menu.Classic;
using Yutai.UI.Menu.Ribbon;

namespace Yutai.UI.Menu
{
   
    public class MenuFactory
    {
        private static IMenu _instance;
        private static bool _usingRibbon;

        internal static IMenu CreateMainMenu(object menuManager,bool usingRibbon)
        {
            _usingRibbon = usingRibbon;
            if (_instance == null)
            {
                var menuIndex = new MenuIndex(MenuIndexType.MainMenu);
                
               
                if (DebugHelper.SyncfusionMenu)
                {
                    _instance = new MainSyncfusionMenu(menuManager, menuIndex);
                }
                else
                {
                    _instance = new MainMenuStripMenu(menuManager, menuIndex);
                }
                }
                CreateDefaultMenuItems(_instance);
         

            return _instance;
        }

        internal static IMenuEx CreateMenu(object menuManager)
        {
            var menuIndex = new MenuIndex(MenuIndexType.MainMenu, false);

            return new MenuStripMenu(menuManager, menuIndex);
        }

        internal static IStatusBar CreateStatusBar(object bar, PluginIdentity identity)
        {
            var menuIndex = new MenuIndex(MenuIndexType.StatusBar);
            var statusBar = new StatusBar(bar, menuIndex, identity);
            return statusBar;
        }

        internal static IToolbarCollection CreateMainToolbars(object menuManager)
        {
            var menuIndex = new MenuIndex(MenuIndexType.Toolbar);
            return new ToolbarCollectionMain(menuManager, menuIndex);
        }

        internal static IToolbarCollectionEx CreateToolbars(object menuManager)
        {
            var menuIndex = new MenuIndex(MenuIndexType.Toolbar, false);
            return new ToolbarCollection(menuManager, menuIndex);
        }

        private static void CreateDefaultMenuItems(IMenuBase menu)
        {
            var items = menu.Items;

            if (!_usingRibbon)
            {
                items.AddDropDown("File", MainMenuKeys.File, PluginIdentity.Default);
                items.AddDropDown("View", MainMenuKeys.View, PluginIdentity.Default);
                items.AddDropDown("Map", MainMenuKeys.Map, PluginIdentity.Default);
                items.AddDropDown("Layer", MainMenuKeys.Layer, PluginIdentity.Default);
                items.AddDropDown("Plugins", MainMenuKeys.Plugins, PluginIdentity.Default);
                items.AddDropDown("Tiles", MainMenuKeys.Tiles, PluginIdentity.Default);
                items.AddDropDown("Tools", MainMenuKeys.Tools, PluginIdentity.Default);
                items.AddDropDown("Help", MainMenuKeys.Help, PluginIdentity.Default);
            }
            else
            {
                items.AddDropDown("File", MainMenuKeys.File, PluginIdentity.Default, true);
                items.AddDropDown("View", MainMenuKeys.View, PluginIdentity.Default, true);
                items.AddDropDown("Map", MainMenuKeys.Map, PluginIdentity.Default, true);
                items.AddDropDown("Layer", MainMenuKeys.Layer, PluginIdentity.Default, true);
                items.AddDropDown("Plugins", MainMenuKeys.Plugins, PluginIdentity.Default, true);
                items.AddDropDown("Tiles", MainMenuKeys.Tiles, PluginIdentity.Default, true);
                items.AddDropDown("Tools", MainMenuKeys.Tools, PluginIdentity.Default, true);
                items.AddDropDown("Help", MainMenuKeys.Help, PluginIdentity.Default, true);
            }

            menu.Update();
        }
    }
}