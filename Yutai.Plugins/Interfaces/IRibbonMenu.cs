using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonMenu
    {
        BarItem FindItem(string key);
        //IRibbonItem FindItem(string key, PluginIdentity identity);
        void RemoveItemsForPlugin(PluginIdentity identity);
        IRibbonMenuIndex SubItems { get; }
        void AddCommand(YutaiCommand command);

        void Remove(IRibbonItem item);
        void Clear();
        void ChangeCurrentTool(string oldToolName, string nowToolName);

        void UpdateMenu();
        void ReorderTabs();
        void AddCommands(XmlDocument xmlDoc, IEnumerable<YutaiCommand> commands);

        void SetStatusValue(string statusKey, object objValue);

        void SetCurrentTool(object control, YutaiTool tool);
        void SetCurrentTool(object control, string toolName);


        void SetContextMenu(Control mainViewMapControlContainer);

        bool GetContextMenuVisible();
        void RefreshContextMenu();
    }
}