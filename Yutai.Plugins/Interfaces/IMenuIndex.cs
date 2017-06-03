using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonMenuItem
    {
        string ParentKey { get; set; }
        string Key { get; set; }
        IRibbonItem Item { get; set; }
        ToolStripItem ToolStripItem { get; set; }
        ToolStrip ToolStrip { get; set; }
    }

    public interface IRibbonMenuIndex
    {
        event EventHandler<MenuItemEventArgs> ItemClicked;
        bool NeedsToolTip { get; }
        string GetParentName(string pName);
        void AddItem(YutaiCommand command);
        bool ItemExists(string itemName);
        IRibbonMenuItem FindItem(string key);
        void Remove(string key);
        void RemoveItemsForPlugin(PluginIdentity pluginIdentity);
        IEnumerable<IRibbonMenuItem> ItemsForPlugin(PluginIdentity pluginIdentity);
        void FireItemClicked(object sender, MenuItemEventArgs e);
        void Clear();
    }

    internal interface IMenuIndex
    {
        void AddItem(string key, IMenuItem item);
        void Remove(string key);
        IMenuItem GetItem(string key);
        void RemoveItemsForPlugin(PluginIdentity pluginIdentity);
        IEnumerable<IMenuItem> ItemsForPlugin(PluginIdentity pluginIdentity);
        void Clear();
        void SaveMetadata(object key, MenuItemCollectionMetadata metadata);
        MenuItemCollectionMetadata LoadMetadata(object key);
        MenuIndexType ToolbarType { get; }
        bool NeedsToolTip { get; }
        bool IsMainMenu { get; }
        event EventHandler<MenuItemEventArgs> ItemClicked;
        void FireItemClicked(object sender, MenuItemEventArgs e);
    }

   
}