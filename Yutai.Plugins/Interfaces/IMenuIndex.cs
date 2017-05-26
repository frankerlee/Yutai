using System;
using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonMenuIndex
    {
        void AddItem(string key, IRibbonItem item);
        void Remove(string key);
        IRibbonItem GetItem(string key);
        void RemoveItemsForPlugin(PluginIdentity pluginIdentity);
        IEnumerable<IRibbonItem> ItemsForPlugin(PluginIdentity pluginIdentity);
        void Clear();
        void SaveMetadata(object key, RibbonMenuItemCollectionMetadata metadata);
        RibbonMenuItemCollectionMetadata LoadMetadata(object key);
        //MenuIndexType ToolbarType { get; }
        bool NeedsToolTip { get; }
        //bool IsMainMenu { get; }
        event EventHandler<MenuItemEventArgs> ItemClicked;
        void FireItemClicked(object sender, MenuItemEventArgs e);
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