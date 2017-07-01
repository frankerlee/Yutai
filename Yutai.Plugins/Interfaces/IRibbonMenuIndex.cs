using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonMenuIndex
    {
        event EventHandler<MenuItemEventArgs> ItemClicked;
        bool NeedsToolTip { get; }

        BarItem FindItem(string key);
        void Remove(string key);
        void RemoveItemsForPlugin(PluginIdentity pluginIdentity);
        IEnumerable<BarItem> ItemsForPlugin(PluginIdentity pluginIdentity);
        void FireItemClicked(object sender, MenuItemEventArgs e);
        void Clear();

        void UpdateMenu();
        List<BarItem> RibbonMenuItems { get; }
        void AddItems(XmlDocument xmlDoc, IEnumerable<YutaiCommand> commands);
        void SetCurrentTool(string oldToolName, string nowToolName);
        void SetStatusValue(string statusKey, object objValue);

        List<YutaiCommand> GetShapeCommands(esriGeometryType geometryType);

        void SetContextMenu(Control mainViewMapControlContainer);

        bool GetContextMenuVisible();
        void RefreshContextMenu();
    }
}