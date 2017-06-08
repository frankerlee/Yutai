using System;
using System.Collections.Generic;
using System.Xml;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonMenuIndex
    {
        event EventHandler<MenuItemEventArgs> ItemClicked;
        bool NeedsToolTip { get; }
   
        IRibbonMenuItem FindItem(string key);
        void Remove(string key);
        void RemoveItemsForPlugin(PluginIdentity pluginIdentity);
        IEnumerable<IRibbonMenuItem> ItemsForPlugin(PluginIdentity pluginIdentity);
        void FireItemClicked(object sender, MenuItemEventArgs e);
        void Clear();
    
        void UpdateMenu();
        IEnumerable<IRibbonMenuItem> RibbonMenuItems { get; }
        void AddItems(XmlDocument xmlDoc, IEnumerable<YutaiCommand> commands);
        void SetCurrentTool(string oldToolName, string nowToolName);
    }
}