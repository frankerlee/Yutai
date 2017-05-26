﻿using System;
using Yutai.Plugins;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.XPMenus;
namespace Yutai.UI.Menu
{
    internal class SyncfusionMenu : MenuBase
    {
        private readonly MainFrameBarManager _menuManager;

        internal SyncfusionMenu(object menuManager, MenuIndex menuIndex)
        {
            _menuIndex = menuIndex;
            _menuManager = menuManager as MainFrameBarManager;

            if (menuIndex == null) throw new ArgumentNullException("menuIndex");
            if (_menuManager == null) throw new ApplicationException("Invalid type of menu manager");

            CreateMenuBar();
        }

        internal void CreateMenuBar()
        {
            var bar = new Bar(_menuManager, MainMenuName)
            {
                BarStyle = BarStyle.IsMainMenu | BarStyle.UseWholeRow | BarStyle.Visible
            };

            _menuManager.Bars.Add(bar);

            var cbr = _menuManager.GetBarControl(bar);
            cbr.Tag = new MenuItemMetadata(PluginIdentity.Default, MainMenuName);
            cbr.AlwaysLeadingEdge = true;
        }

        private Bar MenuBar
        {
            get
            {
                var menu = _menuManager.MainMenuBar;
                if (menu == null)
                {
                    throw new ApplicationException("Failed to find main menu if the application.");
                }
                return menu;
            }
        }

        public override string Name
        {
            get { return _menuManager.MainMenuBar.BarName; }
            set { _menuManager.MainMenuBar.BarName = value; }
        }

        public override IMenuItemCollection Items
        {
            get { return new MenuItemCollection(MenuBar.Items, _menuIndex); }
        }

        private CommandBar CommandBar
        {
            get { return _menuManager.GetBarControl(_menuManager.MainMenuBar); }
        }

        public override bool Visible
        {
            get { return CommandBar.Visible; }
            set { CommandBar.Visible = true; }
        }

        public override object Tag
        {
            get { return CommandBar.Tag; }
            set { CommandBar.Tag = value; }
        }

        // don't do anything
        public override bool Enabled { get; set; }

        public override void Refresh()
        {
            // do nothing
        }
    }
}