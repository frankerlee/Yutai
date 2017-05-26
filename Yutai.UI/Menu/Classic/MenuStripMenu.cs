using System;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Classic
{
    internal class MenuStripMenu : MenuStripMenuMute, IMenuEx
    {
        internal MenuStripMenu(object menuManager, MenuIndex menuIndex)
            : base(menuManager, menuIndex)
        {
        }

        public event EventHandler<MenuItemEventArgs> ItemClicked
        {
            add { _menuIndex.ItemClicked += value; }
            remove { _menuIndex.ItemClicked -= value; }
        }
    }
}