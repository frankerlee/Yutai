using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Ribbon
{
    internal class RibbonMenuIndex:IRibbonMenuIndex
    {
        private readonly  Dictionary<string ,IRibbonItem> _items=new Dictionary<string,IRibbonItem>();
        private readonly Dictionary<object, RibbonMenuItemCollectionMetadata> _collectionMetadata = new Dictionary<object, RibbonMenuItemCollectionMetadata>();
       
        private bool _needsToolTip;

        public event EventHandler<MenuItemEventArgs> ItemClicked;

        public RibbonMenuIndex()
        {
            
        }
        

        public void AddItem(string key, IRibbonItem item)
        {
            if (item.ItemType != RibbonItemType.TabItem && item.ItemType != RibbonItemType.Panel &&
                item.ItemType != RibbonItemType.ToolStrip)
            {
                //添加事件机制
            }
            _items.Add(item.Key,item);
        }

        public void Remove(string key)
        {
            _items.Remove(key);
        }

        public IRibbonItem GetItem(string key)
        {
            IRibbonItem item;
            _items.TryGetValue(key, out item);
            return item;
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

        public IEnumerable<IRibbonItem> ItemsForPlugin(PluginIdentity pluginIdentity)
        {
            return from item in _items where item.Value.PluginIdentity == pluginIdentity select item.Value;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void SaveMetadata(object key, RibbonMenuItemCollectionMetadata metadata)
        {
            _collectionMetadata[key] = metadata;
        }

        public RibbonMenuItemCollectionMetadata LoadMetadata(object key)
        {
            RibbonMenuItemCollectionMetadata data;
            if (_collectionMetadata.TryGetValue(key, out data))
            {
                return data;
            }
            return null;
        }

        public bool NeedsToolTip
        {
            get { return  AppConfig.Instance.ShowMenuToolTips; }
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
