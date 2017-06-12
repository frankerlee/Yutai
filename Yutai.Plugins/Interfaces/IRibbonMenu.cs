using System.Collections.Generic;
using System.Xml;
using DevExpress.XtraBars;
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
        
    }
}