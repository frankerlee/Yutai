using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;
using IRibbonItem = Yutai.Plugins.Interfaces.IRibbonItem;
using SuperToolTip = DevExpress.Utils.SuperToolTip;

namespace Yutai.UI.Menu.Ribbon
{
    

    internal class RibbonMenuIndex : IRibbonMenuIndex
    {
        private List<IRibbonMenuItem> _items;
        private bool _needsToolTip;
        private RibbonControl _ribbonManager;
        private IEnumerable<IRibbonMenuItem> _cmdItems;
        private List<YutaiCommand> _commands;
        private string _oldToolName = "";
        //暂时性的事件触发没有在这儿进行
        public event EventHandler<MenuItemEventArgs> ItemClicked;

        public RibbonMenuIndex(RibbonControl ribbonManager)
        {
            _ribbonManager = ribbonManager;
            _items=new List<IRibbonMenuItem>();
            _commands=new List<YutaiCommand>();
        }

        #region XML处理
        public  string GetXMLAttribute(XmlDocument xmlDoc, string path, string attributeName)
        {
            try
            {
                XmlNode node = xmlDoc.SelectSingleNode(path);
                return GetXMLAttribute(node, attributeName);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }

            return string.Empty;
        }
      
        public  string GetXMLAttribute(XmlNode node, string attributeName)
        {
            try
            {
                if (node != null
                    && node.Attributes != null
                    && node.Attributes.Count > 0
                    && node.Attributes[attributeName] != null)
                {
                    return node.Attributes[attributeName].Value;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return string.Empty;
        }
        #endregion


        #region 创建菜单

        private void AddCommands(IEnumerable<YutaiCommand> commands)
        {
            foreach (YutaiCommand command in commands)
            {
                if (_commands.FirstOrDefault(c => c.Key == command.Key) != null)
                {
                    continue;
                }
                _commands.Add(command);
                CreateCommand(command);
            }
        }


        public void AddItems(XmlDocument xmlDoc, IEnumerable<YutaiCommand> commands)
        {
            //插件的界面分步加载，因此首先将Command加入，然后创建对象
            AddCommands(commands);
            XmlNode node = xmlDoc.SelectSingleNode("/YutaiGIS/MainMenu/Pages");
            if (node == null)
            {
                return;
            }
            XmlNodeList xmlNodeListPageItem = node.SelectNodes("Page");
            if (xmlNodeListPageItem == null)
                return;
            List<RibbonPage> pages=new List<RibbonPage>();
            for (var i = 0; i < xmlNodeListPageItem.Count; i++)
            {
                XmlNode xmlNodePageItem = xmlNodeListPageItem.Item(i);
              
                CreateRibbonPage( xmlNodePageItem);
               
            }
            //开始排序加入
            //_ribbonManager.Pages.AddRange(pages.ToArray());

        }
        public void SetCurrentTool(object sender, ItemClickEventArgs e)
        {
            string nowToolName = e.Item.Name;
            BarItem item;
            if (nowToolName == _oldToolName)
            {
                item = _ribbonManager.Items[nowToolName];
                if (item != null) { ((BarButtonItem)item).Down = true; item.Refresh(); }
                _oldToolName = nowToolName;
                return;
            }
            if (!string.IsNullOrEmpty(_oldToolName))
            {
                item = _ribbonManager.Items[_oldToolName];
                if (item != null) { ((BarButtonItem)item).Down = false; item.Refresh(); }
            }
            if (!string.IsNullOrEmpty(nowToolName))
            {
                item = _ribbonManager.Items[nowToolName];
                if (item != null) { ((BarButtonItem)item).Down = true; item.Refresh(); }
                _oldToolName = nowToolName;
            }
        }
        public void SetCurrentTool(string oldToolName, string nowToolName)
        {
            BarItem item;
            if (!string.IsNullOrEmpty(oldToolName))
            {
               item =_ribbonManager.Items[oldToolName];
                if (item != null) { ((BarButtonItem) item).Down = false; item.Refresh();}
            }
            if (!string.IsNullOrEmpty(nowToolName))
            {
                item = _ribbonManager.Items[nowToolName];
                if (item != null) { ((BarButtonItem) item).Down = true;item.Refresh();}
            }
        }

        private RibbonPage CreateRibbonPage(XmlNode node)
        {
            if (node == null || node.NodeType == XmlNodeType.Comment || node.Attributes == null)
                return null;
            string pName = GetXMLAttribute(node, "Key");
            string pText = GetXMLAttribute(node, "Caption");

            RibbonPage ribbonPage = _ribbonManager.Pages.GetPageByName(pName);
            if (ribbonPage == null)
            {
                ribbonPage=new RibbonPage();
                ribbonPage.Name = GetXMLAttribute(node, "Key");
                ribbonPage.Text = GetXMLAttribute(node, "Caption");
                ribbonPage.Visible = GetXMLAttribute(node, "Visible").ToUpper().IndexOf("F") > -1 ? false : true;
                _ribbonManager.Pages.Add(ribbonPage);
            }

          

            XmlNodeList nodeListSubPageItem = node.ChildNodes;
            foreach (XmlNode nodeSubPageItem in nodeListSubPageItem)
            {
                RibbonPageGroup group = CreateRibbonPageGroup(ribbonPage,nodeSubPageItem) as RibbonPageGroup;
                if (group != null)
                {
                    ribbonPage.Groups.Add(group);
                }
            }
            return ribbonPage;
        }

        private object CreateRibbonPageGroup(RibbonPage pPage,XmlNode nodePageGroup)
        {
            if (nodePageGroup == null || nodePageGroup.NodeType == XmlNodeType.Comment || nodePageGroup.Attributes == null)
                return null;
            string grpName = GetXMLAttribute(nodePageGroup, "Key");
            RibbonPageGroup ribbonPageGroup = pPage.Groups.GetGroupByName(grpName);
            if (ribbonPageGroup == null)
            {
                 ribbonPageGroup = new RibbonPageGroup()
                {
                    Name = GetXMLAttribute(nodePageGroup, "Key"),
                    Text = GetXMLAttribute(nodePageGroup, "Caption"),
                    Visible = GetXMLAttribute(nodePageGroup, "Visible").ToUpper().IndexOf("F") > -1 ? false : true,
                    ShowCaptionButton =
                        GetXMLAttribute(nodePageGroup, "ShowCaption").ToUpper().IndexOf("F") > -1 ? false : true
                };
                pPage.Groups.Add(ribbonPageGroup);
            }
            XmlNodeList nodeListSubItem = nodePageGroup.SelectNodes("BarItem");
            foreach (XmlNode nodeSubItem in nodeListSubItem)
            {
                CreateBarItem(ribbonPageGroup,nodeSubItem);
                //if (groupItem != null)
                //{
                //    ribbonPageGroup.ItemLinks.Add(groupItem);
                //}
            }
            return ribbonPageGroup;
        }

        private BarItem CreateRibbonButtonGroup(RibbonPageGroup pageGroup,XmlNode xmlNode)
        {
            BarButtonGroup group=new BarButtonGroup()
            {
                Name=GetXMLAttribute(xmlNode,"Key")
            };
            pageGroup.ItemLinks.Add(group);
            XmlNodeList nodeListSubItem = xmlNode.ChildNodes;
            foreach (XmlNode nodeSubItem in nodeListSubItem)
            {
                 CreateBarItem(group,nodeSubItem);
              
            }
            return group;
        }
        private void CreateBarItem(object pGroup,XmlNode xmlNode)
        {
            if (xmlNode != null)
            {
                string itemTye = GetXMLAttribute(xmlNode, "ItemType");
                string nodeKey= GetXMLAttribute(xmlNode, "Key");

                if (itemTye.ToLower().Equals("buttongroup"))
                {
                    //表示下面还是组的形式，有子对象;
                    BarItem groupButton = CreateRibbonButtonGroup(pGroup as RibbonPageGroup, xmlNode);
                    return ;
                }
                else
                {
                    //首先检查已有的对象里面是否存在该菜单
                    BarItem barItem = _ribbonManager.Items[nodeKey];
                    if (barItem != null)
                    {
                        if (pGroup is RibbonPageGroup)
                        {
                            ((pGroup) as RibbonPageGroup).ItemLinks.Add(barItem);
                        }
                        else if (pGroup is BarButtonGroup)
                        {
                            ((pGroup) as BarButtonGroup).ItemLinks.Add(barItem);
                        }
                    }
                }
                
            }
            return ;
        }

        private BarItem CreateCommand(YutaiCommand command)
        {
            BarItem item = null;
            if (command.ItemType == RibbonItemType.Button || command.ItemType == RibbonItemType.Tool)
            {
                item= CreateButton(command);
            }
            else if (command.ItemType == RibbonItemType.ComboBox)
            {
                item = CreateComboBox(command);
            }
            else if (command.ItemType == RibbonItemType.CheckBox)
            {
                item = CreateCheckBox(command);
            }
            else if (command.ItemType == RibbonItemType.DropDown)
            {
                item = CreateDropDown(command);
            }
           item.SuperTip=new SuperToolTip();
            item.SuperTip.Items.AddTitle(item.Caption);
            item.SuperTip.Items.Add((string) command.Tooltip);
            return item;
        }

        private BarItem CreateCheckBox(IRibbonItem item)
        {


            BarCheckItem button = new BarCheckItem()
            {
                Name = item.Key,
                Caption = item.Caption
            };
            if (item.Image.Width > 24 || item.Image.Height > 24)
            {
                button.LargeGlyph = item.Image;
            }
            else
            {
                button.Glyph = item.Image;
            }
            button.Checked = item.Checked;
            button.Tag = item;
            button.ItemClick += ((YutaiCommand)item).OnClick;
            //开始检查Category是否存在
            BarManagerCategory category = CheckCategoryExists(item.Category);
            button.Category = category;
            _ribbonManager.Items.Add(button);
            return button;
        }
        private BarItem CreateButton(IRibbonItem item)
        {

            
            BarButtonItem button = new BarButtonItem()
            {
                Name = item.Key,
                Caption = item.Caption
            };
            if (item.Image.Width > 24 || item.Image.Height > 24)
            {
                button.LargeGlyph = item.Image;
            }
            else
            {
                button.Glyph = item.Image;
            }
            button.Tag = item;
            button.ItemClick += ((YutaiCommand)item).OnClick;
            if (item.ItemType == RibbonItemType.Tool)
            {
                button.ButtonStyle = BarButtonStyle.Check;
                button.ItemClick += SetCurrentTool;
            }
            else
            {
                button.ButtonStyle= BarButtonStyle.Default;
            }

           
            //开始检查Category是否存在
            BarManagerCategory category = CheckCategoryExists(item.Category);
            button.Category = category;
            _ribbonManager.Items.Add(button);
            return button;
        }

        private BarItem CreateDropDown(IRibbonItem item)
        {

            ICommandSubType comboSetting = item as ICommandSubType;

            BarLinkContainerItem dropItem = new BarLinkContainerItem()
            {
                Name = item.Key,
                Caption = item.Caption
            };

            
            for (int i = 0; i < comboSetting.GetCount(); i++)
            {
                ((ICommandSubType)item).SetSubType(i);
                BarButtonItem button=new BarButtonItem()
                {
                    Name=item.Key+"_menu_"+i.ToString(),
                    Caption = item.Caption
                };
                if (item.Image != null)
                {
                    if (item.Image.Width > 24 || item.Image.Height > 24)
                    {
                        button.LargeGlyph = item.Image;
                    }
                    else
                    {
                        button.Glyph = item.Image;
                    }
                }
                button.Tag = item;
                BarManagerCategory category = CheckCategoryExists(item.Category);
                button.Category = category;
                dropItem.LinksPersistInfo.Add(new LinkPersistInfo() {Item = button});
            }
            
            return dropItem;
        }

        private BarItem CreateComboBox(IRibbonItem item)
        {
           
            ICommandComboBox comboSetting = item as ICommandComboBox;
            BarEditItem comboBox = new BarEditItem()
            {
                Name = item.Key,
                Caption = item.Caption
            };

            RepositoryItemComboBox cmbEdit=new RepositoryItemComboBox()
            {Name = item.Key+"_combo",BestFitWidth = 200};
            cmbEdit.Buttons.AddRange(new EditorButton[] {new EditorButton(ButtonPredefines.Combo)  });
            comboBox.Edit = cmbEdit;

            object[] objectItems = comboSetting.Items;
            for (int i = 0; i < objectItems.Length; i++)
            {
                cmbEdit.Items.Add(objectItems[i]);
            }
            comboBox.Tag = item;
            comboBox.EditValueChanged += ((YutaiCommand) item).OnClick;
            //开始检查Category是否存在
            BarManagerCategory category=CheckCategoryExists(item.Category);
            comboBox.Category = category;
            _ribbonManager.Items.Add(comboBox);
            return comboBox;
        }

        private BarManagerCategory CheckCategoryExists(string category)
        {
            int catIndex = _ribbonManager.Categories.IndexOf(category);
            if (catIndex >= 0)
                return _ribbonManager.Categories[catIndex];

            BarManagerCategory newCategory = _ribbonManager.Categories.Add(category);
            return newCategory;
        }
        #endregion


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

      

        public void UpdateMenu()
        {
           
        }
        public IEnumerable<IRibbonMenuItem> RibbonMenuItems
        {
            get { return _items; }
        }

       

     
    }
}
