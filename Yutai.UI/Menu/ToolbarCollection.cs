using System;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu
{
    internal class ToolbarCollection : ToolbarCollectionBase, IToolbarCollectionEx
    {
        internal ToolbarCollection(object menuManager, IMenuIndex menuIndex)
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