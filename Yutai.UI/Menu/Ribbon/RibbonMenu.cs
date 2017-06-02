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
            if (command.ItemType == RibbonItemType.ComboBox)
            {
                AddComboBox((IRibbonItem)command);
            }

            if (command.ItemType == RibbonItemType.DropDown)
            {
                AddDropDown((IRibbonItem)command);
                ((ICommandSubType)command).SetSubType(-1);
            }

            _menuIndex.AddItem(command.Key,(IRibbonItem)command);
            return (IRibbonItem) command;
        }

        private void AddDropDown(IRibbonItem item)
        {
            RibbonItemType parentType;
            object control = FindParentObject(item.Key, out parentType);
            if (control == null) return;

            ICommandSubType comboSetting = item as ICommandSubType;
            ToolStripDropDownButton dropDownButton = new ToolStripDropDownButton()
            {
                Name = item.Name + "_drop",Image=item.Image,Text = item.Caption,ToolTipText = item.Tooltip,DisplayStyle =  (ToolStripItemDisplayStyle) item.DisplayStyleYT,
                ImageScaling =(ToolStripItemImageScaling) item.ToolStripItemImageScalingYT
            };
            for (int i = 0; i < comboSetting.GetCount(); i++)
            {
                comboSetting.SetSubType(i);
                ToolStripMenuItem menuItem=new ToolStripMenuItem(item.Caption)
                {
                    Name=item.Name+"_menu_"+i.ToString(),Tag = i,Image = item.Image,ToolTipText = item.Tooltip,
                    DisplayStyle = (ToolStripItemDisplayStyle) item.DisplayStyleYT,TextImageRelation = (TextImageRelation)item.TextImageRelationYT
                };
                menuItem.Click +=((YutaiCommand)item).OnClick;
                dropDownButton.DropDownItems.Add(menuItem);
            }

            if (control is ToolStripEx)
            {
                ToolStripEx ex = (ToolStripEx)control;
                ex.Items.Add(dropDownButton);
                ex.Update();
            }
            else if (control is ToolStripPanelItem)
            {
                ToolStripPanelItem ex = (ToolStripPanelItem)control;

                ex.Items.Add(dropDownButton);
            }
        }

       

        private void AddComboBox(IRibbonItem item)
        {
            RibbonItemType parentType;
            object control = FindParentObject(item.Key, out parentType);
            if (control == null) return;

            ICommandComboBox comboSetting=item as ICommandComboBox;
            ToolStripComboBoxEx comboBox = new ToolStripComboBoxEx() { Name = item.Name + "_combo" ,DropDownStyle =  ComboBoxStyle.DropDownList,Style = ToolStripExStyle.Metro};
            object[] objectItems = comboSetting.Items;
            for (int i = 0; i < objectItems.Length; i++)
            {
                comboBox.Items.Add(objectItems[i]);
            }

            if (!string.IsNullOrEmpty(comboSetting.SelectedText)) comboBox.Text = comboSetting.SelectedText;
            if (comboSetting.ShowCaption == true)
            {
                //创建面板
                ToolStripPanelItem comboPanel=new ToolStripPanelItem()
                {
                    Name=item.Name+"_panel"
                };
                comboPanel.RowCount = comboSetting.LayoutType == 0 ? 1 : 2;
                ToolStripLabel label=new ToolStripLabel(item.Caption) {Name = item.Name+"_label"};
                comboPanel.Items.Add(label);
                
                comboPanel.Items.Add(comboBox);
                comboBox.SelectedIndexChanged += comboSetting.SelectedIndexChanged;
                comboSetting.LinkComboBox = comboBox;
                if (control is ToolStripEx)
                {
                    ToolStripEx ex = (ToolStripEx)control;
                    ex.Items.Add(comboPanel);
                    ex.Update();
                }
                else if (control is ToolStripPanelItem)
                {
                    ToolStripPanelItem ex = (ToolStripPanelItem)control;

                    ex.Items.Add(comboPanel);
                }
            }
            else
            {
                
                comboBox.SelectedIndexChanged += comboSetting.SelectedIndexChanged;
                if (control is ToolStripEx)
                {
                    ToolStripEx ex = (ToolStripEx)control;
                    ex.Items.Add(comboBox);
                    ex.Update();
                }
                else if (control is ToolStripPanelItem)
                {
                    ToolStripPanelItem ex = (ToolStripPanelItem)control;

                    ex.Items.Add(comboBox);
                }
            }
            
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
                ToolTipText = item.Tooltip,AutoToolTip = true
            };
           
            button.DisplayStyle =(ToolStripItemDisplayStyle)((int) item.DisplayStyleYT);
            button.TextImageRelation=(TextImageRelation) item.TextImageRelationYT;
            button.ImageScaling =(ToolStripItemImageScaling) item.ToolStripItemImageScalingYT;
            
            if (item is YutaiCommand)
            {
                button.Click += ((YutaiCommand) item).OnClick;
            }
            if (!string.IsNullOrEmpty(item.Tooltip))
            {
                ToolTipHelper.UpdateTooltip(button, item);
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
                        {Name=item.Name,Text = item.Caption,Tag = item.Key,ShowItemToolTips = true};
                    tabItem.Panel.Controls.Add(ex);
                    if (!string.IsNullOrEmpty(item.Tooltip))
                    {
                        ToolTipHelper.UpdateTooltip(ex,item);
                    }
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