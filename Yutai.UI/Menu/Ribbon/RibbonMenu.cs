using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using ESRI.ArcGIS.SystemUI;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;
using ICommandSubType = Yutai.Plugins.Interfaces.ICommandSubType;
using RibbonTabItem = Syncfusion.Windows.Forms.Tools.RibbonTabItem;

namespace Yutai.UI.Menu.Ribbon
{
    public class RibbonMenu : IRibbonMenu
    {
        private IRibbonMenuIndex _menuIndex;

        public RibbonMenu(IRibbonMenuIndex menuIndex)
        {
            _menuIndex = menuIndex;
          
        }

        public void ChangeCurrentTool(string oldToolName, string nowToolName)
        {
            _menuIndex.SetCurrentTool(oldToolName, nowToolName);
        }

        public void UpdateMenu()
        {
            _menuIndex.UpdateMenu();
        }

        public void ReorderTabs()
        {
            //_menuIndex.ReorderTabs();
        }

        public void AddCommands(XmlDocument xmlDoc, IEnumerable<YutaiCommand> commands)
        {
            _menuIndex.AddItems(xmlDoc, commands);
        }

        public void SetStatusValue(string statusKey, object objValue)
        {
            _menuIndex.SetStatusValue(statusKey, objValue);
        }

        public void SetCurrentTool(object control, YutaiTool tool)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentTool(object control, string toolName)
        {
            throw new NotImplementedException();
        }

        public void SetContextMenu(Control mainViewMapControlContainer)
        {
            _menuIndex.SetContextMenu(mainViewMapControlContainer);
        }

        public BarItem FindItem(string key)
        {
            return _menuIndex.FindItem(key);
        }
        
        public void RemoveItemsForPlugin(PluginIdentity identity)
        {
            _menuIndex.RemoveItemsForPlugin(identity);
        }

        public IRibbonMenuIndex SubItems
        {
            get { return _menuIndex; }
        }

        public void AddCommand(YutaiCommand command)
        {
            //_menuIndex.AddItem(command);
        }

        public void Remove(IRibbonItem item)
        {
           _menuIndex.Remove(item.Key);
        }

        public void Clear()
        {
            _menuIndex.Clear();
        }

        #region 重新编写的关于界面的方法

        
        #endregion
    }
}