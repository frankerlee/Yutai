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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.Geometry;
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
       
        private bool _needsToolTip;
        private RibbonControl _ribbonManager;
    
        private List<YutaiCommand> _commands;
        private List<YutaiCommand> _shapeCommands;
        private string _oldToolName = "";
        private RibbonStatusBar _statusManager;
        //暂时性的事件触发没有在这儿进行
        public event EventHandler<MenuItemEventArgs> ItemClicked;

        public RibbonMenuIndex(RibbonControl ribbonManager, RibbonStatusBar statusBar)
        {
            _ribbonManager = ribbonManager;
            _shapeCommands=new List<YutaiCommand>();
              _commands = new List<YutaiCommand>();
            _statusManager = statusBar;
        }

        #region XML处理

        public string GetXMLAttribute(XmlDocument xmlDoc, string path, string attributeName)
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

        public string GetXMLAttribute(XmlNode node, string attributeName)
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
                if (command is IShapeConstructorTool)
                {
                    _shapeCommands.Add(command);
                }
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
                AddStatusItems(xmlDoc);
                return;
            }
            XmlNodeList xmlNodeListPageItem = node.SelectNodes("Page");
            if (xmlNodeListPageItem == null)
            {
                AddStatusItems(xmlDoc);
                return;
            }
            List<RibbonPage> pages = new List<RibbonPage>();
            for (var i = 0; i < xmlNodeListPageItem.Count; i++)
            {
                XmlNode xmlNodePageItem = xmlNodeListPageItem.Item(i);

                CreateRibbonPage(xmlNodePageItem);
            }
            AddStatusItems(xmlDoc);
        }

        private void AddStatusItems(XmlDocument xmlDoc)
        {
            XmlNode node = xmlDoc.SelectSingleNode("/YutaiGIS/StatusBar/Links");
            if (node == null)
            {
                return;
            }
            XmlNodeList nodeList = node.ChildNodes;
            foreach (XmlNode subnode in nodeList)
            {
                string key = GetXMLAttribute(subnode, "Key");
                BarItem item = _ribbonManager.Items[key];
                string widthStr = GetXMLAttribute(subnode, "Width");
                if (!string.IsNullOrEmpty(widthStr))
                    item.Width = Convert.ToInt32(widthStr);
                string rightStr = GetXMLAttribute(subnode, "IsRight");
                if (!string.IsNullOrEmpty(rightStr))
                {
                    item.Alignment = rightStr.ToUpper().IndexOf("F") > -1
                        ? BarItemLinkAlignment.Left
                        : BarItemLinkAlignment.Right;
                }

                _statusManager.ItemLinks.Add(_ribbonManager.Items[key]);
            }
        }

        public void SetCurrentTool(object sender, ItemClickEventArgs e)
        {
            string nowToolName = e.Item.Name;
            BarItem item;
            if (nowToolName == _oldToolName)
            {
                item = _ribbonManager.Items[nowToolName];
                if (item != null)
                {
                    ((BarButtonItem) item).Down = true;
                    item.Refresh();
                }
                _oldToolName = nowToolName;
                return;
            }
            if (!string.IsNullOrEmpty(_oldToolName))
            {
                item = _ribbonManager.Items[_oldToolName];
                if (item != null)
                {
                    ((BarButtonItem) item).Down = false;
                    item.Refresh();
                }
            }
            if (!string.IsNullOrEmpty(nowToolName))
            {
                item = _ribbonManager.Items[nowToolName];
                if (item != null)
                {
                    ((BarButtonItem) item).Down = true;
                    item.Refresh();
                }
                _oldToolName = nowToolName;
            }
        }

        public void SetCurrentTool(string oldToolName, string nowToolName)
        {
            BarItem item;
            if (!string.IsNullOrEmpty(oldToolName))
            {
                item = _ribbonManager.Items[oldToolName];
                if (item != null)
                {
                    ((BarButtonItem) item).Down = false;
                    item.Refresh();
                }
            }
            if (!string.IsNullOrEmpty(nowToolName))
            {
                item = _ribbonManager.Items[nowToolName];
                if (item != null)
                {
                    ((BarButtonItem) item).Down = true;
                    item.Refresh();
                }
            }
        }

        public void SetStatusValue(string statusKey, object objValue)
        {
            if (objValue == null) objValue = "";
            BarItem item = _ribbonManager.Items[statusKey];
            if (item == null) return;
            if (item is BarStaticItem)
            {
                ((BarStaticItem) item).Caption = objValue.ToString();
            }
            else if (item is BarEditItem)
            {
                ((BarEditItem) item).EditValue = objValue;
            }
        }

        public List<YutaiCommand> GetShapeCommands(esriGeometryType geometryType)
        {
            return
            (from command in _shapeCommands
                where ((IShapeConstructorTool) command).GeometryType == geometryType
                select command).ToList();
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
                ribbonPage = new RibbonPage();
                ribbonPage.Name = GetXMLAttribute(node, "Key");
                ribbonPage.Text = GetXMLAttribute(node, "Caption");
                ribbonPage.Visible = GetXMLAttribute(node, "Visible").ToUpper().IndexOf("F") > -1 ? false : true;
                _ribbonManager.Pages.Add(ribbonPage);
            }


            XmlNodeList nodeListSubPageItem = node.ChildNodes;
            foreach (XmlNode nodeSubPageItem in nodeListSubPageItem)
            {
                RibbonPageGroup group = CreateRibbonPageGroup(ribbonPage, nodeSubPageItem) as RibbonPageGroup;
                if (group != null)
                {
                    ribbonPage.Groups.Add(group);
                }
            }
            return ribbonPage;
        }

        private object CreateRibbonPageGroup(RibbonPage pPage, XmlNode nodePageGroup)
        {
            if (nodePageGroup == null || nodePageGroup.NodeType == XmlNodeType.Comment ||
                nodePageGroup.Attributes == null)
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
                CreateBarItem(ribbonPageGroup, nodeSubItem);
                //if (groupItem != null)
                //{
                //    ribbonPageGroup.ItemLinks.Add(groupItem);
                //}
            }
            return ribbonPageGroup;
        }

        private BarItem CreateRibbonButtonGroup(RibbonPageGroup pageGroup, XmlNode xmlNode)
        {
            BarButtonGroup group = new BarButtonGroup()
            {
                Name = GetXMLAttribute(xmlNode, "Key")
            };
            pageGroup.ItemLinks.Add(group);
            XmlNodeList nodeListSubItem = xmlNode.ChildNodes;
            foreach (XmlNode nodeSubItem in nodeListSubItem)
            {
                CreateBarItem(group, nodeSubItem);
            }
            return group;
        }

        private void CreateBarItem(object pGroup, XmlNode xmlNode)
        {
            if (xmlNode != null)
            {
                string itemTye = GetXMLAttribute(xmlNode, "ItemType");
                string nodeKey = GetXMLAttribute(xmlNode, "Key");
                string widthStr = GetXMLAttribute(xmlNode, "Width");
                string subItemStr = GetXMLAttribute(xmlNode, "SubItems");
                if (itemTye.ToLower().Equals("buttongroup"))
                {
                    //表示下面还是组的形式，有子对象;
                    BarItem groupButton = CreateRibbonButtonGroup(pGroup as RibbonPageGroup, xmlNode);
                    if (!string.IsNullOrEmpty(widthStr))
                    {
                        groupButton.Width = Convert.ToInt32(widthStr);
                    }
                    return;
                }
                else if (!string.IsNullOrEmpty(subItemStr))
                {
                    BarSubItem barItem = _ribbonManager.Items[nodeKey] as BarSubItem;

                    if (!string.IsNullOrEmpty(subItemStr))
                    {
                        string[] subs = subItemStr.Split(';');
                        for (int i = 0; i < subs.Length; i++)
                        {
                            BarItem oneItem = _ribbonManager.Items[subs[i]];
                            if (oneItem == null) continue;
                            barItem.LinksPersistInfo.Add(new LinkPersistInfo(oneItem));
                        }
                    }
                    if (pGroup is RibbonPageGroup)
                    {
                        ((pGroup) as RibbonPageGroup).ItemLinks.Add(barItem);
                    }
                    else if (pGroup is BarButtonGroup)
                    {
                        ((pGroup) as BarButtonGroup).ItemLinks.Add(barItem);
                    }
                }
                else
                {
                    //首先检查已有的对象里面是否存在该菜单
                    BarItem barItem = _ribbonManager.Items[nodeKey];
                    if (barItem == null) return;

                    if (!string.IsNullOrEmpty(widthStr))
                    {
                        barItem.Width = Convert.ToInt32(widthStr);
                    }
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
            return;
        }

        private BarItem CreateCommand(YutaiCommand command)
        {
            BarItem item = null;
            if (command.ItemType == RibbonItemType.Button || command.ItemType == RibbonItemType.Tool)
            {
                item = CreateButton(command);
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
            else if (command.ItemType == RibbonItemType.Label)
            {
                item = CreateLabel(command);
            }
            // if (!string.IsNullOrEmpty(command.Message))
            item.ItemClick += FireMessageSetting;
            if (command.NeedUpdateEvent)
            {
                item.ItemClick += UpdateMenuClick;
            }
            item.Enabled = command.Enabled;
            item.SuperTip = new SuperToolTip();
            item.SuperTip.Items.AddTitle(item.Caption);
            item.SuperTip.Items.Add((string) command.Tooltip);
            return item;
        }

        private BarItem CreateLabel(IRibbonItem item)
        {
            BarStaticItem button = new BarStaticItem()
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
            //button.Checked = item.Checked;
            button.Tag = item;
            button.ItemClick += ((YutaiCommand) item).OnClick;

            //开始检查Category是否存在
            BarManagerCategory category = CheckCategoryExists(item.Category);
            button.Category = category;
            _ribbonManager.Items.Add(button);
            return button;
        }

        private void UpdateMenuClick(object sender, ItemClickEventArgs e)
        {
            UpdateMenu();
        }
        private void FireMessageSetting(object sender, ItemClickEventArgs e)
        {
            IRibbonItem command = e.Item.Tag as IRibbonItem;
            if (command == null) return;
            SetStatusValue("Status_Message", command.Message);
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
            button.ItemClick += ((YutaiCommand) item).OnClick;
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
            button.ItemClick += ((YutaiCommand) item).OnClick;
            if (item.ItemType == RibbonItemType.Tool)
            {
                button.ButtonStyle = BarButtonStyle.Check;
                button.ItemClick += SetCurrentTool;
            }
            else
            {
                button.ButtonStyle = BarButtonStyle.Default;
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

            BarSubItem dropItem = new BarSubItem()
            {
                Name = item.Key,
                Caption = item.Caption
            };
            if (item.Image.Width > 24 || item.Image.Height > 24)
            {
                dropItem.LargeGlyph = item.Image;
            }
            else
            {
                dropItem.Glyph = item.Image;
            }

            //for (int i = 0; i < comboSetting.GetCount(); i++)
            //{
            //    ((ICommandSubType)item).SetSubType(i);
            //    BarItem button = _ribbonManager.Items[item.Key];
            //    dropItem.LinksPersistInfo.Add(new LinkPersistInfo() { Item = button });
            //}
            BarManagerCategory category = CheckCategoryExists(item.Category);
            dropItem.Category = category;
            _ribbonManager.Items.Add(dropItem);
            return dropItem;
        }

        private BarItem CreateComboBox(IRibbonItem item)
        {
            ICommandComboBox comboSetting = item as ICommandComboBox;
            //ComboBoxEdit comboBox = new ComboBoxEdit()
            //{
            //    Name = item.Key,
            //    Caption = item.Caption
            //};
            BarEditItem comboBox = new BarEditItem()
            {
                Name = item.Key,
                Caption = item.Caption,
                Width = 230
            };

            RepositoryItemComboBox cmbEdit = new RepositoryItemComboBox()
                {Name = item.Key + "_combo", BestFitWidth = 300};
            //cmbEdit.Buttons.AddRange(new EditorButton[] {new EditorButton(ButtonPredefines.Combo)  });
            comboBox.Edit = cmbEdit;

            if (comboSetting.DropDownList)
            {
                //cmbEdit.AllowDropDownWhenReadOnly = DefaultBoolean.True;
                //cmbEdit.ReadOnly = true;
            }

            object[] objectItems = comboSetting.Items;
            for (int i = 0; i < objectItems.Length; i++)
            {
                cmbEdit.Items.Add(objectItems[i]);
            }
            comboBox.Tag = item;
            comboSetting.LinkComboBox = comboBox;
            comboBox.EditValueChanged += ((ICommandComboBox) item).OnEditValueChanged;
            comboSetting.LinkComboBox = comboBox;
            //开始检查Category是否存在
            BarManagerCategory category = CheckCategoryExists(item.Category);
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

        public BarItem FindItem(string key)
        {
            return _ribbonManager.Items[key];
        }

        public void Remove(string key)
        {
            BarItem item = FindItem(key);
            if (item != null)
                _ribbonManager.Items.Remove(item);
        }


        public void RemoveItemsForPlugin(PluginIdentity pluginIdentity)
        {
            //List<IRibbonMenuItem> _removes = new List<IRibbonMenuItem>();
            //foreach (var item in _items)
            //{
            //    if (item.Item.PluginIdentity == pluginIdentity)
            //    {
            //        _removes.Add(item);
            //    }
            //}
            //foreach (var key in _removes)
            //{
            //    _items.Remove(key);
            //}
        }


        public IEnumerable<BarItem> ItemsForPlugin(PluginIdentity pluginIdentity)
        {
           // return from item in _items where item.Item.PluginIdentity == pluginIdentity select item;
            return null;
        }

        public bool NeedsToolTip
        {
            get { return AppConfig.Instance.ShowMenuToolTips; }
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
            _commands.Clear();
            _shapeCommands.Clear();
            _ribbonManager.Items.Clear();
            //后面需要增加删除真实界面的代码
        }


        public void UpdateMenu()
        {
            foreach (BarItem item in _ribbonManager.Items)
            {
                if (item.Tag != null)
                {
                    YutaiCommand command = item.Tag as YutaiCommand;
                    if (command != null)
                    {
                        item.Enabled = command.Enabled;
                        if (item is BarCheckItem) ((BarCheckItem) item).Checked = command.Checked;
                       continue;
                    }
                }
                YutaiCommand oneCommand = _commands.Find(c => c.Key == item.Name);
                if (oneCommand != null)
                {
                    item.Enabled = oneCommand.Enabled;
                    if (item is BarCheckItem) ((BarCheckItem)item).Checked = oneCommand.Checked;
                    continue;
                }
            }
        }

        public List<BarItem> RibbonMenuItems
        {
            get { return _ribbonManager.Items.GetSortedList(); }
        }
    }
}