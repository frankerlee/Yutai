using System;
using System.Linq;
using System.Windows.Forms;
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
        public IRibbonMenuItem FindItem(string key)
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
            _menuIndex.AddItem(command);
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