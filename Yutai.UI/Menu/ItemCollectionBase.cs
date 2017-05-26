﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yutai.Plugins;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Menu.Classic;

namespace Yutai.UI.Menu
{
    internal abstract class ItemCollectionBase : IMenuItemCollection
    {
        protected readonly IList _items;
        protected readonly IMenuIndex MenuIndex;

        internal ItemCollectionBase(IList items, IMenuIndex menuIndex)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (menuIndex == null) throw new ArgumentNullException("menuIndex");

            _items = items;
            MenuIndex = menuIndex;
        }

        public static void RemoveItems(IMenuItemCollection items, PluginIdentity identity)
        {
            for (int j = items.Count() - 1; j >= 0; j--)
            {
                var dropDownMenuItem = items[j] as IDropDownMenuItem;
                if (dropDownMenuItem != null)
                {
                    RemoveItems(dropDownMenuItem.SubItems, identity);
                    dropDownMenuItem.Update();
                }

                if (!items[j].Skip && items[j].PluginIdentity == identity)
                {
                    items.Remove(j);
                }
            }
        }

        public abstract IComboBoxMenuItem AddComboBox(string text, string key, PluginIdentity identity);

        public abstract IMenuItem this[int menuItemIndex] { get; }

        protected abstract IDropDownMenuItem AddDropDown(string text, string key, Bitmap icon, PluginIdentity identity,bool isFirst=false);
        

        public abstract IMenuItem AddLabel(string text, string key, PluginIdentity identity);

        public abstract IMenuItem AddButton(string text, string key, Bitmap icon, PluginIdentity identity);

        public IMenuItem AddButton(MenuCommand command, bool beginGroup = false)
        {
            if (command == null) throw new ArgumentNullException("command");

            var item = AddButton(command.Text, command.Key, command.Icon, command.PluginIdentity);

            if (MenuIndex.NeedsToolTip)
            {
                item.Description = command.Description;
            }

            item.ShortcutKeys = command.ShortcutKeys;

            if (beginGroup)
            {
                item.BeginGroup = true;
            }

            return item;
        }

        public IEnumerator<IMenuItem> GetEnumerator()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IMenuItem AddButton(string text, PluginIdentity identity)
        {
            return AddButton(text, string.Empty, identity);
        }

        public IMenuItem AddButton(string text, string key, PluginIdentity identity)
        {
            return AddButton(text, key, null, identity);
        }

        public IDropDownMenuItem AddDropDown(string text, string key, PluginIdentity identity)
        {
            return AddDropDown(text, key, null, identity);
        }

        public IDropDownMenuItem AddDropDown(string text, string key, PluginIdentity identity, bool isFirst)
        {
            return AddDropDown(text, key, null, identity,true);
        }

        public IDropDownMenuItem AddDropDown(string text, Bitmap icon, PluginIdentity identity)
        {
            return AddDropDown(text, string.Empty, icon, identity);
        }

        public void Insert(IMenuItem item, int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new IndexOutOfRangeException("Menu items index is out of range.");
            }
            _items.Insert(index, item);
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new IndexOutOfRangeException("Menu items index is out of range.");
            }

            var menuItem = this[index] as MenuItem;
            if (menuItem != null)
            {
                menuItem.DetachItemListeners();
                MenuIndex.Remove(menuItem.UniqueKey);
            }

            var menuStripItem = this[index] as MenuStripItem;
            if (menuStripItem != null)
            {
                menuStripItem.DetachItemListeners();
                MenuIndex.Remove(menuStripItem.UniqueKey);
            }

            _items.RemoveAt(index);
        }

        public void Remove(IMenuItem item)
        {
            int index = IndexOf(item);
            if (index >= 0 && index < _items.Count)
            {
                Remove(index);
            }
        }

        public void Clear()
        {
            // clear the nested menus recursively
            var subMenus = this.OfType<IDropDownMenuItem>().Select(item => item.SubItems);
            {
                foreach (var menu in subMenus)
                {
                    menu.Clear();
                }
            }

            // clear the index
            var keys = this.Where(item => item.HasKey).Select(item => item.UniqueKey).ToList();
            foreach (var uniqueKey in keys)
            {
                MenuIndex.Remove(uniqueKey);
            }

            _items.Clear();
        }

        public int IndexOf(IMenuItem item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var it = this[i];
                if (!it.Skip && it.UniqueKey == item.UniqueKey)
                {
                    return i;
                }
            }
            return -1;
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public IMenuItem InsertBefore
        {
            get
            {
                var data = MenuIndex.LoadMetadata(_items);
                if (data != null)
                {
                    return data.InsertBefore;
                }
                return null;
            }
            set
            {
                var data = MenuIndex.LoadMetadata(_items) ?? new MenuItemCollectionMetadata();
                data.InsertBefore = value;
                MenuIndex.SaveMetadata(_items, data);
            }
        }

        protected IMenuItem AddItemCore(object item, string key, bool label, bool statusBar)
        {
            int index = -1;
            if (InsertBefore != null)
            {
                index = IndexOf(InsertBefore);
            }

            if (index != -1)
            {
                _items.Insert(index, item);
            }
            else
            {
                index = _items.Add(item);
            }

            var menuItem = this[index];

            if (!string.IsNullOrWhiteSpace(key))
            {
                //if (!label)
                {
                    // if it's main application menu, we want to dispatch all to plugins;
                    // for menus in other forms, just to the control itself
                    if (MenuIndex.IsMainMenu)
                    {
                        if (statusBar)
                        {
                            menuItem.ItemClicked += PluginBroadcaster.Instance.FireStatusItemClicked;
                        }
                        else
                        {
                            menuItem.ItemClicked += PluginBroadcaster.Instance.FireItemClicked;
                        }
                    }
                    else
                    {
                        menuItem.ItemClicked += MenuIndex.FireItemClicked;
                    }
                }

                MenuIndex.AddItem(menuItem.UniqueKey, menuItem);
            }

            return menuItem;
        }
    }
}