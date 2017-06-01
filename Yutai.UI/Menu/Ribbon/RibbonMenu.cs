using System;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using RibbonTabItem = Syncfusion.Windows.Forms.Tools.RibbonTabItem;

namespace Yutai.UI.Menu.Ribbon
{
    public class RibbonMenu : IRibbonMenu
    {
        private IRibbonMenuIndex _menuIndex;
        private int _count;
        private IRibbonItem _insertBefore;
        private RibbonControlAdv _ribbonManager;
        

        public RibbonMenu(IRibbonMenuIndex menuIndex, RibbonControlAdv ribbonManager)
        {
            _menuIndex = menuIndex;
            _ribbonManager = ribbonManager;
        }
        public IRibbonItem FindItem(string key, PluginIdentity identity)
        {
            return _menuIndex.GetItem(key);
        }

        public void RemoveItemsForPlugin(PluginIdentity identity)
        {
            _menuIndex.RemoveItemsForPlugin(identity);
        }

        public IRibbonMenuIndex SubItems
        {
            get { return _menuIndex; }
        }

        public IRibbonItem AddCommand(YutaiCommand command)
        {
            if (command.ItemType == RibbonItemType.TabItem)
            {
                AddTabItem((IRibbonItem)command);
            }
            if (command.ItemType == RibbonItemType.ToolStrip)
            {
                AddToolStrip((IRibbonItem)command);
            }
            if (command.ItemType == RibbonItemType.Panel)
            {
                AddPanel((IRibbonItem)command);
            }
            if (command.ItemType == RibbonItemType.NormalItem)
            {
                AddButton((IRibbonItem) command);
            }
            if (command.ItemType == RibbonItemType.Tool)
            {
                AddButton((IRibbonItem)command);
            }
            
            _menuIndex.AddItem(command.Key,(IRibbonItem)command);
            return (IRibbonItem) command;
        }

        private void AddButton(IRibbonItem item)
        {
            RibbonItemType parentType;
            object control = FindParentObject(item.Key, out parentType);
            if (control == null) return;

            ToolStripButton button=new ToolStripButton(item.Caption,item.Image)
            {
                Name=item.Name,
                Tag=item.Key,
                ToolTipText = item.Tooltip
               
            };
           
            button.DisplayStyle =(ToolStripItemDisplayStyle)((int) item.DisplayStyleYT);
            button.TextImageRelation=(TextImageRelation) item.TextImageRelationYT;
            button.ImageScaling =(ToolStripItemImageScaling) item.ToolStripItemImageScalingYT;
            
            if (item is YutaiCommand)
            {
                button.Click += ((YutaiCommand) item).OnClick;
            }
            
            if (control is ToolStripEx)
            {
                ToolStripEx ex = (ToolStripEx) control;
                ex.Items.Add(button);
                ex.Update();
            }
            else if (control is ToolStripPanelItem)
            {
                ToolStripPanelItem ex = (ToolStripPanelItem)control;
                
                ex.Items.Add(button);
            }
            
        }
        private void AddToolStrip(IRibbonItem item)
        {
            string[] names = item.Name.Split('.');

            foreach (ToolStripTabItem tabItem in _ribbonManager.Header.MainItems)
            {
                if (tabItem.Name == names[0])
                {
                    ToolStripEx ex=new ToolStripEx()
                        {Name=item.Name,Text = item.Caption,Tag = item.Key};
                    tabItem.Panel.Controls.Add(ex);
                }
            }
          
        }

        private void AddPanel(IRibbonItem item)
        {
            string[] names = item.Name.Split('.');
            

            foreach (ToolStripTabItem tabItem in _ribbonManager.Header.MainItems)
            {
                if (tabItem.Name == names[0])
                {
                    foreach (Control panelControl in tabItem.Panel.Controls)
                    {
                        if (panelControl.Name == names[0] + "." + names[1])
                        {
                            ToolStripEx toolEx = (ToolStripEx) panelControl;
                            ToolStripPanelItem panel=new ToolStripPanelItem()
                            {
                                Name = names[0]+"."+ names[1] +"."+names[2],
                                RowCount = item.PanelRowCount,
                                LayoutStyle = (ToolStripLayoutStyle)item.ToolStripLayoutStyleYT
                            };
                            toolEx.Items.Add(panel);
                            return;
                        }
                    }
                    
                }
            }

        }

        private void AddTabItem(IRibbonItem item)
        {
            ToolStripTabItem tabItem = new ToolStripTabItem()
            {
                Text = item.Caption,
                Name=item.Name,
                Tag=item.Key
            };
            try
            {
                _ribbonManager.Header.AddMainItem(tabItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
        private object FindParentObject(string nameStr,out RibbonItemType itemType)
        {
            string[] names = nameStr.Split('.');
            itemType=   RibbonItemType.NormalItem;
            if (names.Length <= 1) return null;
            
                foreach (ToolStripTabItem headerMainItem in _ribbonManager.Header.MainItems)
                {
                    if (headerMainItem.Name == names[0])
                    {
                        if (names.Length == 2)
                        {
                            itemType= RibbonItemType.TabItem;
                            return headerMainItem;
                        }
                        foreach (ToolStripEx oneControl in headerMainItem.Panel.Controls)
                        {
                            if (oneControl.Name == names[0]+"."+names[1])
                            {
                                if (names.Length == 3)
                                {
                                    itemType= RibbonItemType.ToolStrip;
                                    return oneControl;
                                }
                                foreach (ToolStripItem oneControlControl in oneControl.Items)
                                {
                                    if (oneControlControl.Name == names[0] + "." + names[1]+"."+names[2])
                                    {
                                    itemType= RibbonItemType.Panel;
                                        return oneControlControl;
                                    }
                                }
                            }
                        }
                    }
                }
            itemType= RibbonItemType.NormalItem;
            return null;
        }

       

        public IRibbonItem AddCommand(YutaiMenuCommand command)
        {
            if (command.ItemType == RibbonItemType.TabItem)
            {
                AddTabItem((IRibbonItem)command);
            }
            if (command.ItemType == RibbonItemType.ToolStrip)
            {
                AddToolStrip((IRibbonItem)command);
            }
            if (command.ItemType == RibbonItemType.NormalItem)
            {
                AddButton((IRibbonItem)command);
            }

            _menuIndex.AddItem(command.Key, (IRibbonItem)command);
            return (IRibbonItem)command;
        }

        public void Insert(IRibbonItem item, int index)
        {
            throw new NotImplementedException();
        }

        public void Remove(int index)
        {
            throw new NotImplementedException();
        }

        public void Remove(IRibbonItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IRibbonItem item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _count; }
        }

        public IRibbonItem InsertBefore
        {
            get { return _insertBefore; }
            set { _insertBefore = value; }
        }
    }
}