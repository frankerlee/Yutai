using System;
using System.Collections.Generic;
using System.Linq;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;

namespace Yutai.UI.Menu
{
    internal class MenuIndex : IMenuIndex
    {
        private readonly Dictionary<string, IMenuItem> _items = new Dictionary<string, IMenuItem>();
        private readonly Dictionary<object, MenuItemCollectionMetadata> _collectionMetadata = new Dictionary<object, MenuItemCollectionMetadata>();
        private readonly MenuIndexType _indexType;
        private readonly bool _mainMenu;
        public event EventHandler<MenuItemEventArgs> ItemClicked;

        public MenuIndex(MenuIndexType indexType, bool mainMenu = true)
        {
            _indexType = indexType;
            _mainMenu = mainMenu;
        }

        public void AddItem(string key, IMenuItem item)
        {
            if (NeedsToolTip)
            {
                ToolTipHelper.UpdateTooltip(item);
            }

            item.ItemChanged += (s, e) => ToolTipHelper.UpdateTooltip(item);
            _items.Add(key, item);
        }

        public bool IsMainMenu
        {
            get { return _mainMenu; }
        }

        public void Remove(string key)
        {
            _items.Remove(key);
        }

        public IMenuItem GetItem(string key)
        {
            IMenuItem item;
            _items.TryGetValue(key, out item);
            return item;
        }

        public IEnumerable<IMenuItem> ItemsForPlugin(PluginIdentity identity)
        {
            return from item in _items where item.Value.PluginIdentity == identity select item.Value;
        }

        public void RemoveItemsForPlugin(PluginIdentity pluginIdentity)
        {
            HashSet<string> keys = new HashSet<string>();
            foreach (var item in _items)
            {
                if (item.Value.PluginIdentity == pluginIdentity)
                {
                    keys.Add(item.Key);
                }
            }
            foreach (var key in keys)
            {
                _items.Remove(key);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void SaveMetadata(object key, MenuItemCollectionMetadata metadata)
        {
            _collectionMetadata[key] = metadata;
        }

        public MenuItemCollectionMetadata LoadMetadata(object key)
        {
            MenuItemCollectionMetadata data;
            if (_collectionMetadata.TryGetValue(key, out data))
            {
                return data;
            }
            return null;
        }

        public bool NeedsToolTip
        {
            get
            {
                switch (_indexType)
                {
                    case MenuIndexType.MainMenu:
                        return AppConfig.Instance.ShowMenuToolTips;
                    case MenuIndexType.Toolbar:
                        return true;
                    case MenuIndexType.StatusBar:
                        return false;
                }

                return false;
            }
        }

        public MenuIndexType ToolbarType
        {
            get { return _indexType; }
        }


        public void FireItemClicked(object sender, MenuItemEventArgs e)
        {
            var handler = ItemClicked;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}