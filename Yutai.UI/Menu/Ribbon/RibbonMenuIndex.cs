using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;

namespace Yutai.UI.Menu.Ribbon
{
    

    internal class RibbonMenuIndex : IRibbonMenuIndex
    {
        private List<IRibbonMenuItem> _items;
        private bool _needsToolTip;
        private RibbonControlAdv _ribbonManager;

        private IEnumerable<IRibbonMenuItem> _cmdItems;
       

        //暂时性的事件触发没有在这儿进行
        public event EventHandler<MenuItemEventArgs> ItemClicked;

        public RibbonMenuIndex(RibbonControlAdv ribbonManager)
        {
            _ribbonManager = ribbonManager;
            _items=new List<IRibbonMenuItem>();
        }

        public string GetParentName(string pName)
        {
            int findSplit = pName.IndexOf('.');
            if (findSplit<0) return string.Empty;
            string pNewName = pName.Substring(0, pName.LastIndexOf('.'));
            return pNewName;
        }
        public void AddItem(YutaiCommand command)
        {
            string parentKey = string.Empty;
            if (string.IsNullOrEmpty(command.ParentName))
            {
                parentKey = GetParentName(command.Name);
            }
            else
            {
                parentKey = command.ParentName;
            }
            IRibbonMenuItem parentMenu = null;
            if (!string.IsNullOrEmpty(parentKey))
            {
                parentMenu = FindItem( parentKey);
            }
            
            if (command.ItemType == RibbonItemType.TabItem)
            {
                AddTabItem((IRibbonItem)command,parentMenu);
            }
            if (command.ItemType == RibbonItemType.ToolStrip)
            {
                AddToolStrip((IRibbonItem)command,parentMenu);
            }
            if (command.ItemType == RibbonItemType.Panel)
            {
                AddPanel((IRibbonItem)command,parentMenu);
            }
            if (command.ItemType == RibbonItemType.Button)
            {
                AddButton((IRibbonItem)command,parentMenu);
            }
            if (command.ItemType == RibbonItemType.Tool)
            {
                AddButton((IRibbonItem)command,parentMenu);
            }
            if (command.ItemType == RibbonItemType.ComboBox)
            {
                AddComboBox((IRibbonItem)command,parentMenu);
            }

            if (command.ItemType == RibbonItemType.DropDown)
            {
                AddDropDown((IRibbonItem)command,parentMenu);
               
            }
        }

        private void AddDropDown(IRibbonItem item, IRibbonMenuItem parentMenu)
        {
            if (ItemExists(item.Name)) return;
            ICommandSubType comboSetting = item as ICommandSubType;
            List<RibbonMenuItem> menus = new List<RibbonMenuItem>();
            ToolStripDropDownButton dropDownButton = new ToolStripDropDownButton()
            {
                Name = item.Name,
                Image = item.Image,
                Text = item.Caption,
                ToolTipText = item.Tooltip,
                DisplayStyle = (ToolStripItemDisplayStyle)item.DisplayStyleYT,
                ImageScaling = (ToolStripItemImageScaling)item.ToolStripItemImageScalingYT
            };
            RibbonMenuItem dropMenuItem = new RibbonMenuItem(item.Name, item, dropDownButton)
            {
                ParentKey = parentMenu.Key
            };
            menus.Add(dropMenuItem);
            for (int i = 0; i < comboSetting.GetCount(); i++)
            {
                comboSetting.SetSubType(i);
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Caption)
                {
                    Name = item.Name + "_menu_" + i.ToString(),
                    Tag = comboSetting,
                    Image = item.Image,
                    ToolTipText = item.Tooltip,
                    DisplayStyle = (ToolStripItemDisplayStyle)item.DisplayStyleYT,
                    TextImageRelation = (TextImageRelation)item.TextImageRelationYT
                };
                menuItem.Click += ((YutaiCommand)item).OnClick;
                dropDownButton.DropDownItems.Add(menuItem);

                RibbonItem subItem=new RibbonItem(item, menuItem.Name) {ParentName = item.Name};
                RibbonMenuItem dropSubItem = new RibbonMenuItem(menuItem.Name, subItem, menuItem)
                {
                    ParentKey = item.Name
                };
                menus.Add(dropSubItem);
            }
            if (parentMenu.Item.ItemType == RibbonItemType.ToolStrip)
            {
                ToolStripEx ex = (ToolStripEx)parentMenu.ToolStrip;
                ex.Items.Add(dropDownButton);
                ex.Update();
            }
            else if (parentMenu.Item.ItemType == RibbonItemType.Panel)
            {
                ToolStripPanelItem ex = (ToolStripPanelItem)parentMenu.ToolStripItem;
                ex.Items.Add(dropDownButton);
            }
            _items.AddRange(menus);
        }
        private void AddComboBox(IRibbonItem item, IRibbonMenuItem parentMenu)
        {
            if (ItemExists(item.Name)) return;
            ICommandComboBox comboSetting = item as ICommandComboBox;
            string newName = comboSetting.ShowCaption == true ? item.Name + "_panel" : item.Name;
            if (ItemExists(newName)) return;

            ToolStripComboBoxEx comboBox = new ToolStripComboBoxEx()
            {
                Name = item.Name,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Style = ToolStripExStyle.Metro,
                Tag=item
            };
            object[] objectItems = comboSetting.Items;
            for (int i = 0; i < objectItems.Length; i++)
            {
                comboBox.Items.Add(objectItems[i]);
            }
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!string.IsNullOrEmpty(comboSetting.SelectedText)) comboBox.Text = comboSetting.SelectedText;
            if (comboSetting.ShowCaption == true)
            {
                //创建面板
                ToolStripPanelItem comboPanel = new ToolStripPanelItem()
                {
                    Name = item.Name + "_panel"
                };
                comboPanel.RowCount = comboSetting.LayoutType == 0 ? 1 : 2;
                ToolStripLabel label = new ToolStripLabel(item.Caption) { Name = item.Name + "_label" };
                comboPanel.Items.Add(label);
                comboPanel.Items.Add(comboBox);
                comboBox.SelectedIndexChanged += comboSetting.SelectedIndexChanged;
                comboSetting.LinkComboBox = comboBox;
                if (parentMenu.Item.ItemType== RibbonItemType.ToolStrip)
                {
                    ToolStripEx ex = (ToolStripEx)parentMenu.ToolStrip;
                    ex.Items.Add(comboPanel);
                    ex.Update();
                }
                else if (parentMenu.Item.ItemType == RibbonItemType.Panel)
                {
                    ToolStripPanelItem ex = (ToolStripPanelItem)parentMenu.ToolStripItem;
                    ex.Items.Add(comboPanel);
                }
                IRibbonItem panelRibbonItem=new RibbonItem(item, item.Name + "_panel") { ItemType =  RibbonItemType.Panel,ParentName = parentMenu.Key};
                RibbonMenuItem panelMenuItem=new RibbonMenuItem(item.Name + "_panel",panelRibbonItem,comboPanel);
                _items.Add(panelMenuItem);
                item.ParentName = item.Name + "_panel";
                RibbonMenuItem menuItem = new RibbonMenuItem(item.Key, item, comboBox);
                menuItem.ParentKey = parentMenu.Key;
                _items.Add(menuItem);
            }
            else
            {
                comboBox.SelectedIndexChanged += comboSetting.SelectedIndexChanged;
                if (parentMenu.Item.ItemType == RibbonItemType.ToolStrip)
                {
                    ToolStripEx ex = (ToolStripEx)parentMenu.ToolStrip;
                    ex.Items.Add(comboBox);
                    ex.Update();
                }
                else if (parentMenu.Item.ItemType == RibbonItemType.Panel)
                {
                    ToolStripPanelItem ex = (ToolStripPanelItem)parentMenu.ToolStripItem;
                    ex.Items.Add(comboBox);
                }

                RibbonMenuItem menuItem = new RibbonMenuItem(item.Key, item, comboBox);
                menuItem.ParentKey = parentMenu.Key;
                _items.Add(menuItem);
            }
        }
        private void AddButton(IRibbonItem item, IRibbonMenuItem parentMenu)
        {
            if (ItemExists(item.Name)) return;
            ToolStripButton button = new ToolStripButton(item.Caption, item.Image)
            {
                ToolTipText = item.Tooltip,
                AutoToolTip = true,
                Name = item.Name,
                Tag = item,
                Checked = item.Checked
            };
            button.DisplayStyle = (ToolStripItemDisplayStyle)((int)item.DisplayStyleYT);
            button.TextImageRelation = (TextImageRelation)item.TextImageRelationYT;
            button.ImageScaling = (ToolStripItemImageScaling)item.ToolStripItemImageScalingYT;
            button.Enabled = item.Enabled;

            if (item is YutaiCommand)
            {
                button.Click += ((YutaiCommand)item).OnClick;
                if (item.NeedUpdateEvent)
                {
                    button.Click += ToolStripButton_Click; ;
                }
            }
            if (!string.IsNullOrEmpty(item.Tooltip))
            {
                ToolTipHelper.UpdateTooltip(button, item);
            }
            try
            {
                item.ParentName = parentMenu.Key;
                RibbonMenuItem menuItem = new RibbonMenuItem(item.Key, item, button);
                menuItem.ParentKey = parentMenu.Key;
                if (parentMenu.Item.ItemType == RibbonItemType.ToolStrip)
                {
                    ((ToolStripEx)parentMenu.ToolStrip).Items.Add(button);
                }
                else if (parentMenu.Item.ItemType == RibbonItemType.Panel)
                {
                    ((ToolStripPanelItem)parentMenu.ToolStripItem).Items.Add(button);
                }
                _items.Add(menuItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
           UpdateMenu();
        }

        private void AddPanel(IRibbonItem item, IRibbonMenuItem parentMenu)
        {
            if (ItemExists(item.Name)) return;
            ToolStripPanelItem stripItem = new ToolStripPanelItem()
            {
                Text = item.Caption,
                Name = item.Name,
                Tag = item,
                RowCount = item.PanelRowCount,
                LayoutStyle = (ToolStripLayoutStyle)item.ToolStripLayoutStyleYT
            };
            try
            {
                item.ParentName = parentMenu.Key;
                RibbonMenuItem menuItem = new RibbonMenuItem(item.Key, item, stripItem);
                menuItem.ParentKey = parentMenu.Key;
                if (parentMenu.Item.ItemType == RibbonItemType.ToolStrip)
                {
                    ((ToolStripEx) parentMenu.ToolStrip).Items.Add(stripItem);
                }
                else if (parentMenu.Item.ItemType == RibbonItemType.Panel)
                {
                    ((ToolStripPanelItem) parentMenu.ToolStripItem).Items.Add(stripItem);
                }
                _items.Add(menuItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddToolStrip(IRibbonItem item, IRibbonMenuItem parentMenu)
        {
            if (ItemExists(item.Name)) return;
            ToolStripEx  stripItem = new ToolStripEx()
            {
                Text = item.Caption,
                Name = item.Name,
                Tag = item,
                GroupedButtons = item.IsGroup
            };
            if (item.Position >= 0) stripItem.TabIndex = item.Position;
            try
            {
                item.ParentName = parentMenu.Key;
                RibbonMenuItem menuItem = new RibbonMenuItem(item.Key, item, stripItem);
                menuItem.ParentKey = parentMenu.Key;
                ((ToolStripTabItem)parentMenu.ToolStripItem).Panel.Controls.Add(stripItem);
                stripItem.GroupedButtons = item.IsGroup;
                _items.Add(menuItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddTabItem(IRibbonItem item, IRibbonMenuItem parentMenu)
        {
            if (ItemExists(item.Name)) return;
            ToolStripTabItem tabItem = new ToolStripTabItem()
            {
                Text = item.Caption,
                Name = item.Name,
                Tag = item,
                Position = item.Position>=0?item.Position:10
                
            };

            //if (item.Position >= 0) tabItem.Position = item.Position;
            try
            {
                RibbonMenuItem menuItem=new RibbonMenuItem(item.Key,item,tabItem);
                _ribbonManager.Header.AddMainItem(tabItem);
                _items.Add(menuItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ItemExists(string itemName)
        {
            IRibbonMenuItem findItem = FindItem(itemName);
            return findItem != null;
        }
        public IRibbonMenuItem FindItem( string key)
        {
            return _items.FirstOrDefault(c => c.Key == key);
        }
        
        public void Remove(string key)
        {
            IRibbonMenuItem item = FindItem( key);
            if(item != null)
                _items.Remove(item);

        }

        
        public void RemoveItemsForPlugin(PluginIdentity pluginIdentity)
        {
            List<IRibbonMenuItem> _removes=new List<IRibbonMenuItem>();
            foreach (var item in _items)
            {
                if (item.Item.PluginIdentity == pluginIdentity)
                {
                    _removes.Add(item);
                }
            }
            foreach (var key in _removes)
            {
                _items.Remove(key);
            }
        }
        

        public IEnumerable<IRibbonMenuItem> ItemsForPlugin(PluginIdentity pluginIdentity)
        {
            return from item in _items where item.Item.PluginIdentity == pluginIdentity select item;
        }
        
        public bool NeedsToolTip
        {
            get { return  AppConfig.Instance.ShowMenuToolTips; }
        }

        
        public void FireItemClicked(object sender, MenuItemEventArgs e)
        {
            var handler = ItemClicked;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public void Clear()
        {
            _items.Clear();
            //后面需要增加删除真实界面的代码
        }

        public void ReorderTabs()
        {
            //按照顺序进行排序，然后加入
            var tabItems = from item in _items
                where item.Item.ItemType == RibbonItemType.TabItem
                orderby item.Item.Position descending select item;
            if (tabItems == null)
            {
                StartToolStripGroups();
                return;
            }
            int max = tabItems.Count() - 1;
            if (max < 0)
            { StartToolStripGroups();
                return;}
            int i = 0;
            foreach (var tabItem in tabItems)
            {
                ((ToolStripTabItem) tabItem.ToolStripItem).Position = max;
                max--;
            }
            StartToolStripGroups();
            
        }

        public void UpdateMenu()
        {
            if (_cmdItems == null)
            {
                 _cmdItems = from item in _items
                    where
                    item.Item.ItemType != RibbonItemType.TabItem && item.Item.ItemType != RibbonItemType.Group &&
                    item.Item.ItemType != RibbonItemType.Panel && item.Item.ItemType != RibbonItemType.ToolStrip
                    select item;

            }
            if (_cmdItems == null) return;
            foreach (IRibbonMenuItem cmdItem in _cmdItems)
            {
                if (cmdItem.Item is YutaiCommand)
                {
                    cmdItem.ToolStripItem.Enabled = ((YutaiCommand)cmdItem.Item).Enabled;
                }
                switch (cmdItem.Item.ItemType)
                {
                    case RibbonItemType.TabItem:
                    case RibbonItemType.ToolStrip:
                    case RibbonItemType.Panel:
                    case RibbonItemType.Group:
                    case RibbonItemType.ComboBox:
                        return;
                        break;
                    case RibbonItemType.Button:
                        ((ToolStripButton)cmdItem.ToolStripItem).Checked = ((YutaiCommand)cmdItem.Item).Checked;
                        break;
                    case RibbonItemType.Tool:
                        ((ToolStripButton)cmdItem.ToolStripItem).Checked = ((YutaiCommand)cmdItem.Item).Checked;
                        break;
                    case RibbonItemType.DropDown:
                        break;
                    case RibbonItemType.CheckBox:
                        ((ToolStripCheckBox)cmdItem.ToolStripItem).Checked = ((YutaiCommand)cmdItem.Item).Checked;
                        break;
                    default:
                        break;
                }
              
            }
        }
        public IEnumerable<IRibbonMenuItem> RibbonMenuItems
        {
            get { return _items; }
        }

        public void StartToolStripGroups()
        {
            var stripItems = from item in _items
                           where item.Item.ItemType == RibbonItemType.ToolStrip && item.Item.IsGroup==true
                           select item;
            foreach (var strip in stripItems)
            {
                ToolStripEx stripEx = ((ToolStripEx) strip.ToolStrip);
                if (stripEx.Image==null)
                 ((ToolStripEx) strip.ToolStrip).Image = ((ToolStripEx) strip.ToolStrip).Items[0].Image;
                stripEx.CollapsedDropDownButtonText = stripEx.Text;
                stripEx.GroupedButtons = true;

            }

            _ribbonManager.Update();
        }
    }
}
