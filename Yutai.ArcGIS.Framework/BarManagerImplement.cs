using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Priviliges;
using Yutai.Plugins.Interfaces;
using ICommand = ESRI.ArcGIS.SystemUI.ICommand;
using ICommandSubType = ESRI.ArcGIS.SystemUI.ICommandSubType;
using IToolContextMenu = Yutai.ArcGIS.Common.Framework.IToolContextMenu;
using LoadComponent = Yutai.ArcGIS.Common.LoadComponent;

namespace Yutai.ArcGIS.Framework
{
    public class BarManagerImplement : IBarManager, IBarManagerEvents
    {
        private AddWindows addWindows_0 = null;
        public BarStaticItem BarCurrentToolInfoItem = null;
        protected BarManager barManager1 = null;
        public BarStaticItem BarMessageItem = null;
        public BarStaticItem BarMousePositionItem = null;
        public BarStaticItem BarPagePositionItem = null;
        private BarSubItem barSubItem_0 = null;
        private bool bool_0 = true;
        private CreateBarsHelper createBarsHelper_0 = null;
        private Hashtable hashtable_0 = new Hashtable();
        private Hashtable hashtable_1 = new Hashtable();
        private IFramework iframework_0 = null;
        private IList<Control> ilist_0 = new List<Control>();
        private IList<string> ilist_1 = new List<string>();
        private IPopuMenuWrap ipopuMenuWrap_0 = new PopupMenuWrap();
        private ITool itool_0 = null;
        private List<ESRI.ArcGIS.SystemUI.ICommand> list_0 = new List<ESRI.ArcGIS.SystemUI.ICommand>();
        protected PopupMenu m_pCurrentPopupMenu = new PopupMenu();
        protected PopupMenu m_pSystemPopupMenu = null;
        private MenuInfoHelper menuInfoHelper_0 = new MenuInfoHelper();
        private string string_0 = "";
        private SysGrants sysGrants_0;

        public event OnBarsCreateCompleteHandler OnBarsCreateComplete;

        public event OnItemClickEventHandler OnItemClickEvent;

        public BarManagerImplement()
        {
            this.m_pSystemPopupMenu = new PopupMenu();
            (this.ipopuMenuWrap_0 as PopupMenuWrap).PopupMenu = this.m_pCurrentPopupMenu;
            this.sysGrants_0 = new SysGrants(AppConfigInfo.UserID);
        }

        public void AddItem(MenuItemDef menuItemDef_0)
        {
            ICommand command = this.method_0(menuItemDef_0);
            if ((command != null) && (this.barManager1.Items[menuItemDef_0.Name] == null))
            {
                BarItem item = this.method_1(menuItemDef_0, command);
                this.barManager1.Items.Add(item);
            }
        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Control tag = e.Item.Tag as Control;
            if (tag != null)
            {
                tag.Show();
            }
            else if (this.OnItemClickEvent != null)
            {
                if (e.Item.Tag != null)
                {
                    this.OnItemClickEvent(e.Item.Tag);
                    if (e.Item.Tag is ITool)
                    {
                        BarButtonItem item = e.Item as BarButtonItem;
                        if (item != null)
                        {
                            item.ButtonStyle = BarButtonStyle.Check;
                            item.Down = true;
                        }
                    }
                }
                else
                {
                    this.OnItemClickEvent(e.Item.Name);
                }
            }
        }

        public void ChangeHook(object object_0)
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                this.list_0[i].OnCreate(object_0);
            }
        }

        public void CreateBars(string string_1)
        {
            this.string_0 = string_1;
            this.createBarsHelper_0 = new CreateBarsHelper(this.barManager1, this.m_pSystemPopupMenu, this.iframework_0, this.list_0, this.ipopuMenuWrap_0);
            this.createBarsHelper_0.OnCreateComplete += new CreateBarsHelper.OnCreateCompleteHandler(this.method_22);
            this.createBarsHelper_0.HasMainMenu = this.bool_0;
            new Thread(new ThreadStart(this.method_21)).Start();
            Thread.Sleep(0);
        }

        public void CreateBars2(string string_1)
        {
            this.string_0 = string_1;
            this.createBarsHelper_0 = new CreateBarsHelper(this.barManager1, this.m_pSystemPopupMenu, this.iframework_0, this.list_0, this.ipopuMenuWrap_0);
            this.createBarsHelper_0.StartCreateBar(string_1);
            this.method_22();
            this.UpdateUI(null);
        }

        public ICommand FindCommand(string string_1)
        {
            if (this.barManager1 != null)
            {
                BarItem item = this.barManager1.Items[string_1];
                if (item != null)
                {
                    return (item.Tag as ICommand);
                }
            }
            return null;
        }

        public void Init()
        {
            if (this.barManager1 != null)
            {
                this.barManager1.ItemClick += new ItemClickEventHandler(this.barManager1_ItemClick);
            }
        }

        public void LoadTools(IAppContext iapplication_0, string string_1)
        {
            //LoadComponent component = new LoadComponent();
            //ComponentList list = new ComponentList(string_1);
            //list.beginRead();
            //string str = "";
            //string str2 = "";
            //int subType = -1;
            //for (int i = 0; i < list.GetComponentCount(); i++)
            //{
            //    str2 = list.getClassName(i);
            //    str = MediaTypeNames.Application.StartupPath + @"\" + list.getfilename(i);
            //    subType = list.getSubType(i);
            //    component.LoadComponentLibrary(str);
            //    try
            //    {
            //        ICommand command = null;
            //        command = component.LoadClass(str2) as ICommand;
            //        if (command == null)
            //        {
            //            CErrorLog.writeErrorLog(this, null, "无法创建:" + str2);
            //        }
            //        else
            //        {
            //            command.OnCreate(iapplication_0);
            //            if (subType != -1)
            //            {
            //                (command as ICommandSubType).SetSubType(subType);
            //            }
            //            if (command is IToolContextMenu)
            //            {
            //                (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
            //            }
            //            BarItem item = this.barManager1.Items[command.Name];
            //            if (item == null)
            //            {
            //                item = this.method_2(command);
            //            }
            //            if ((command is IBarEditItem) && (item is BarEditItem))
            //            {
            //                (command as IBarEditItem).BarEditItem = new JLKComboxBarItem(item);
            //            }
            //            if (item != null)
            //            {
            //                item.Tag = command;
            //            }
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        CErrorLog.writeErrorLog(null, exception, "");
            //    }
            //}
        }

        public void Message(MSGTYPE msgtype_0, object object_0)
        {
            switch (msgtype_0)
            {
                case MSGTYPE.MTToolTip:
                case MSGTYPE.MTOther:
                    if (this.BarMessageItem != null)
                    {
                        this.BarMessageItem.Caption = object_0 as string;
                    }
                    break;

                case MSGTYPE.MTMapPosition:
                    if (this.BarMousePositionItem != null)
                    {
                        this.BarMousePositionItem.Caption = object_0 as string;
                    }
                    break;

                case MSGTYPE.MTPagePosition:
                    if (this.BarPagePositionItem != null)
                    {
                        this.BarPagePositionItem.Caption = object_0 as string;
                    }
                    break;

                case MSGTYPE.MTCurrentTool:
                    if (this.BarCurrentToolInfoItem == null)
                    {
                        if (this.BarMessageItem != null)
                        {
                            this.BarMessageItem.Caption = object_0 as string;
                        }
                        break;
                    }
                    this.BarCurrentToolInfoItem.Caption = object_0 as string;
                    break;
            }
        }

        private ICommand method_0(MenuItemDef menuItemDef_0)
        {
            return null;
            //ICommand command = null;
            //if ((menuItemDef_0.Path != null) && (menuItemDef_0.ClassName != null))
            //{
            //    try
            //    {
            //        if (menuItemDef_0.Path[1] != ':')
            //        {
            //            menuItemDef_0.Path = MediaTypeNames.Application.StartupPath + @"\" + menuItemDef_0.Path;
            //        }
            //        if (File.Exists(menuItemDef_0.Path))
            //        {
            //            command = this.method_3(menuItemDef_0.Path).LoadClass(menuItemDef_0.ClassName) as ICommand;
            //            if (command != null)
            //            {
            //                command.OnCreate(ApplicationRef.Application);
            //                if (menuItemDef_0.SubType != null)
            //                {
            //                    try
            //                    {
            //                        int subType = int.Parse(menuItemDef_0.SubType);
            //                        (command as ICommandSubType).SetSubType(subType);
            //                    }
            //                    catch
            //                    {
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            //if (command != null)
            //{
            //    menuItemDef_0.Name = command.Name;
            //}
            //return command;
        }

        private BarItem method_1(MenuItemDef menuItemDef_0, ICommand icommand_0)
        {
            BarItem item = null;
            BarManagerCategory category;
            item = new BarButtonItem {
                Id = this.barManager1.GetNewItemId(),
                Name = menuItemDef_0.Name,
                Tag = icommand_0,
                Caption = icommand_0.Caption
            };
            if (menuItemDef_0.Caption != null)
            {
                item.Caption = menuItemDef_0.Caption;
            }
            item.Enabled = icommand_0.Enabled;
            if (icommand_0.Tooltip != null)
            {
                item.Hint = icommand_0.Tooltip;
            }
            if ((menuItemDef_0.BitmapPath == null) && (icommand_0.Bitmap != 0))
            {
                try
                {
                    IntPtr hbitmap = new IntPtr(icommand_0.Bitmap);
                    Bitmap bitmap = Image.FromHbitmap(hbitmap);
                    bitmap.MakeTransparent();
                    item.Glyph = bitmap;
                }
                catch
                {
                }
            }
            else if (menuItemDef_0.BitmapPath != null)
            {
                item.Glyph = new Bitmap(menuItemDef_0.BitmapPath);
            }
            if (icommand_0.Category != null)
            {
                if (icommand_0.Category.Length > 0)
                {
                    category = this.barManager1.Categories[icommand_0.Category];
                    if (category == null)
                    {
                        category = new BarManagerCategory(icommand_0.Category, Guid.NewGuid());
                        this.barManager1.Categories.Add(category);
                    }
                    item.Category = category;
                    return item;
                }
                category = this.barManager1.Categories["其他"];
                if (category == null)
                {
                    category = new BarManagerCategory("其他", Guid.NewGuid());
                    this.barManager1.Categories.Add(category);
                }
                item.Category = category;
                return item;
            }
            category = this.barManager1.Categories["其他"];
            if (category == null)
            {
                category = new BarManagerCategory("其他", Guid.NewGuid());
                this.barManager1.Categories.Add(category);
            }
            item.Category = category;
            return item;
        }

        private string[] method_10(XmlNode xmlNode_0, out string string_1)
        {
            string_1 = null;
            return null;
            //string[] strArray = new string[15];
            //string_1 = "0";
            //int num = 0;
            //while (true)
            //{
            //    if (num >= xmlNode_0.Attributes.Count)
            //    {
            //        return strArray;
            //    }
            //    XmlAttribute attribute = xmlNode_0.Attributes[num];
            //    switch (attribute.Name.ToLower())
            //    {
            //        case "hooktype":
            //            string_1 = attribute.Value;
            //            break;

            //        case "name":
            //            strArray[0] = attribute.Value;
            //            break;

            //        case "caption":
            //            strArray[1] = attribute.Value;
            //            break;

            //        case "begingroup":
            //            strArray[2] = attribute.Value;
            //            break;

            //        case "visible":
            //            strArray[3] = attribute.Value;
            //            break;

            //        case "path":
            //            strArray[4] = attribute.Value;
            //            break;

            //        case "showinmaps":
            //            strArray[14] = attribute.Value;
            //            break;

            //        case "classname":
            //            strArray[5] = attribute.Value;
            //            break;

            //        case "subtype":
            //            strArray[6] = attribute.Value;
            //            break;

            //        case "ispopupmenu":
            //            strArray[7] = attribute.Value;
            //            break;

            //        case "progid":
            //            strArray[8] = attribute.Value;
            //            break;

            //        case "shortcut":
            //            strArray[9] = attribute.Value;
            //            break;

            //        case "type":
            //            strArray[10] = attribute.Value;
            //            break;

            //        case "tooltip":
            //            strArray[11] = attribute.Value;
            //            break;

            //        case "image":
            //            strArray[12] = attribute.Value;
            //            break;

            //        case "bitmap":
            //        {
            //            string path = attribute.Value;
            //            if ((path.Length > 3) && (path[1] != ':'))
            //            {
            //                path = MediaTypeNames.Application.StartupPath + @"\" + path;
            //                try
            //                {
            //                    if (File.Exists(path))
            //                    {
            //                        strArray[13] = path;
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //            }
            //            break;
            //        }
            //    }
            //    num++;
            //}
        }

        private string[] method_11(XmlNode xmlNode_0)
        {
            return null;
            //string[] strArray = new string[9];
            //for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            //{
            //    XmlAttribute attribute = xmlNode_0.Attributes[i];
            //    switch (attribute.Name.ToLower())
            //    {
            //        case "name":
            //            strArray[0] = attribute.Value;
            //            break;

            //        case "caption":
            //            strArray[1] = attribute.Value;
            //            break;

            //        case "row":
            //            strArray[2] = attribute.Value;
            //            break;

            //        case "col":
            //            strArray[3] = attribute.Value;
            //            break;

            //        case "visible":
            //            strArray[4] = attribute.Value;
            //            break;

            //        case "ismainmenu":
            //            strArray[5] = attribute.Value;
            //            break;

            //        case "ispopupmenu":
            //            strArray[6] = attribute.Value;
            //            break;

            //        case "path":
            //            strArray[7] = attribute.Value;
            //            if (strArray[7][1] != ':')
            //            {
            //                strArray[7] = string.Format(@"{0}\{1}", MediaTypeNames.Application.StartupPath, strArray[7]);
            //            }
            //            break;

            //        case "classname":
            //            strArray[8] = attribute.Value;
            //            break;
            //    }
            //}
            //return strArray;
        }

        private void method_12(object object_0, XmlNode xmlNode_0, bool bool_1)
        {
            //int num;
            //Exception exception;
            //string str = "0";
            //string[] strArray = this.method_10(xmlNode_0, out str);
            //ICommand command = null;
            //if ((strArray[4] != null) && (strArray[5] != null))
            //{
            //    try
            //    {
            //        if ((AppConfigInfo.UserID != "admin") && !this.sysGrants_0.StaffIsHasMenuPri(this.menuInfoHelper_0, strArray[4], strArray[5]))
            //        {
            //            return;
            //        }
            //        if (strArray[4][1] != ':')
            //        {
            //            strArray[4] = MediaTypeNames.Application.StartupPath + @"\" + strArray[4];
            //        }
            //        if (File.Exists(strArray[4]))
            //        {
            //            command = this.method_3(strArray[4]).LoadClass(strArray[5]) as ICommand;
            //            if (command != null)
            //            {
            //                command.OnCreate(this.iframework_0.Application);
            //                if (command is IToolContextMenu)
            //                {
            //                    (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
            //                }
            //                if (strArray[6] != null)
            //                {
            //                    try
            //                    {
            //                        num = int.Parse(strArray[6]);
            //                        (command as ICommandSubType).SetSubType(num);
            //                    }
            //                    catch
            //                    {
            //                        Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的子命令设置失败", strArray[4], strArray[5]));
            //                    }
            //                }
            //                if ((strArray[14] != null) && bool.Parse(strArray[14]))
            //                {
            //                    this.iframework_0.Application.AddCommands(command);
            //                }
            //            }
            //            else
            //            {
            //                Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", strArray[4], strArray[5]));
            //            }
            //        }
            //        else
            //        {
            //            Logger.Current.Error("", null, string.Format("文件{0}不存在！", strArray[4]));
            //        }
            //    }
            //    catch (Exception exception1)
            //    {
            //        exception = exception1;
            //        Logger.Current.Error("", exception, string.Format("创建{0}中的类{1}的失败", strArray[4], strArray[5]));
            //    }
            //}
            //else if (strArray[8] != null)
            //{
            //    try
            //    {
            //        if ((AppConfigInfo.UserID != "admin") && !this.sysGrants_0.StaffIsHasMenuPri(this.menuInfoHelper_0, strArray[8]))
            //        {
            //            return;
            //        }
            //        command = Activator.CreateInstance(Type.GetTypeFromProgID(strArray[8])) as ICommand;
            //        if (command != null)
            //        {
            //            command.OnCreate(this.iframework_0.Application);
            //            if (command is IToolContextMenu)
            //            {
            //                (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
            //            }
            //            if (strArray[6] != null)
            //            {
            //                try
            //                {
            //                    num = int.Parse(strArray[6]);
            //                    (command as ICommandSubType).SetSubType(num);
            //                }
            //                catch
            //                {
            //                    Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的子命令设置失败", strArray[4], strArray[5]));
            //                }
            //            }
            //            if ((strArray[14] != null) && bool.Parse(strArray[14]))
            //            {
            //                this.iframework_0.Application.AddCommands(command);
            //            }
            //        }
            //        else
            //        {
            //            Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", strArray[4], strArray[5]));
            //        }
            //    }
            //    catch (Exception exception2)
            //    {
            //        exception = exception2;
            //        Logger.Current.Error("", exception, "创建" + strArray[4] + "中的类" + strArray[5] + "失败");
            //    }
            //}
            //else if (strArray[10] != null)
            //{
            //    try
            //    {
            //        int index = strArray[10].IndexOf(',');
            //        string typeName = strArray[10].Substring(0, index);
            //        string str3 = strArray[10].Substring(index + 1).Trim();
            //        command = this.method_9(str3).CreateInstance(typeName) as ICommand;
            //        if (command != null)
            //        {
            //            command.OnCreate(this.iframework_0.Application);
            //            if (command is IToolContextMenu)
            //            {
            //                (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
            //            }
            //            if (strArray[6] != null)
            //            {
            //                try
            //                {
            //                    num = int.Parse(strArray[6]);
            //                    (command as ICommandSubType).SetSubType(num);
            //                }
            //                catch
            //                {
            //                    Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的子命令设置失败", strArray[4], strArray[5]));
            //                }
            //            }
            //            if ((strArray[14] != null) && bool.Parse(strArray[14]))
            //            {
            //                this.iframework_0.Application.AddCommands(command);
            //            }
            //        }
            //        else
            //        {
            //            Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", strArray[4], strArray[5]));
            //        }
            //    }
            //    catch (Exception exception3)
            //    {
            //        exception = exception3;
            //        Logger.Current.Error("", exception, "创建" + strArray[4] + "中的类" + strArray[5] + "失败");
            //    }
            //}
            //if ((strArray[0] != null) || (command != null))
            //{
            //    if (command is ICommandLine)
            //    {
            //        if ((command as ICommandLine).CommandName != null)
            //        {
            //            strArray[0] = (command as ICommandLine).CommandName;
            //        }
            //    }
            //    else if ((command != null) && (command.Name != null))
            //    {
            //        strArray[0] = command.Name;
            //    }
            //    BarItem item = null;
            //    if (strArray[0].Length > 0)
            //    {
            //        item = this.barManager1.Items[strArray[0]];
            //    }
            //    if (item == null)
            //    {
            //        BarManagerCategory category;
            //        string caption;
            //        if ((str != "0") && (command != null))
            //        {
            //            this.list_0.Add(command);
            //        }
            //        if (xmlNode_0.ChildNodes.Count > 0)
            //        {
            //            item = new BarSubItem();
            //            this.barManager1.Items.Add(item);
            //            if (object_0 is Bar)
            //            {
            //                category = this.barManager1.Categories["弹出式菜单"];
            //                if (category == null)
            //                {
            //                    category = new BarManagerCategory("弹出式菜单", Guid.NewGuid());
            //                    this.barManager1.Categories.Add(category);
            //                }
            //                item.Category = category;
            //            }
            //            else if (object_0 is BarSubItem)
            //            {
            //                caption = (object_0 as BarSubItem).Caption;
            //                category = this.barManager1.Categories[caption];
            //                if (category == null)
            //                {
            //                    category = new BarManagerCategory(caption, Guid.NewGuid());
            //                    this.barManager1.Categories.Add(category);
            //                }
            //                item.Category = category;
            //            }
            //        }
            //        else
            //        {
            //            if (command is IBarEditItem)
            //            {
            //                if ((command as IBarEditItem).Style == BarEditStyle.ComboBoxEdit)
            //                {
            //                    item = new BarEditItem();
            //                    RepositoryItemComboBox box = new RepositoryItemComboBox();
            //                    (item as BarEditItem).Edit = box;
            //                    box.AutoHeight = false;
            //                    box.Name = command.Name + "_ItemComboBox";
            //                    (command as IBarEditItem).BarEditItem = new JLKComboxBarItem(item);
            //                    item.Width = (command as IBarEditItem).Width;
            //                }
            //                else
            //                {
            //                    if ((command as IBarEditItem).Style != BarEditStyle.ButtonEdit)
            //                    {
            //                        return;
            //                    }
            //                    item = new BarButtonItem();
            //                    (command as IBarEditItem).BarEditItem = new JLKButtonBarItem(item);
            //                    item.Width = (command as IBarEditItem).Width;
            //                }
            //            }
            //            else if (command is IToolControl)
            //            {
            //                item = new BarButtonItem();
            //            }
            //            else
            //            {
            //                item = new BarButtonItem();
            //            }
            //            this.barManager1.Items.Add(item);
            //        }
            //        item.Id = this.barManager1.GetNewItemId();
            //        item.Name = strArray[0];
            //        item.Tag = command;
            //        if (strArray[1] != null)
            //        {
            //            item.Caption = strArray[1];
            //        }
            //        if (command != null)
            //        {
            //            if (strArray[11] != null)
            //            {
            //                item.Hint = strArray[11];
            //            }
            //            else if (command.Tooltip != null)
            //            {
            //                item.Hint = command.Tooltip;
            //            }
            //            else if (command.Caption != null)
            //            {
            //                item.Hint = command.Caption;
            //            }
            //            item.Enabled = command.Enabled;
            //            if ((command is IShortcut) && (strArray[9] == null))
            //            {
            //                item.ItemShortcut = new BarShortcut((command as IShortcut).Keys);
            //            }
            //            if (((strArray[12] == null) && (strArray[13] == null)) && (command.Bitmap != 0))
            //            {
            //                try
            //                {
            //                    IntPtr hbitmap = new IntPtr(command.Bitmap);
            //                    Bitmap bitmap = Image.FromHbitmap(hbitmap);
            //                    bitmap.MakeTransparent();
            //                    item.Glyph = bitmap;
            //                }
            //                catch
            //                {
            //                }
            //            }
            //            if (strArray[1] == null)
            //            {
            //                item.Caption = command.Caption;
            //            }
            //            if (command.Category != null)
            //            {
            //                if (command.Category.Length > 0)
            //                {
            //                    category = this.barManager1.Categories[command.Category];
            //                    if (category == null)
            //                    {
            //                        category = new BarManagerCategory(command.Category, Guid.NewGuid());
            //                        this.barManager1.Categories.Add(category);
            //                    }
            //                    item.Category = category;
            //                }
            //                else
            //                {
            //                    category = this.barManager1.Categories["其他"];
            //                    if (category == null)
            //                    {
            //                        category = new BarManagerCategory("其他", Guid.NewGuid());
            //                        this.barManager1.Categories.Add(category);
            //                    }
            //                    item.Category = category;
            //                }
            //            }
            //            else
            //            {
            //                category = this.barManager1.Categories["其他"];
            //                if (category == null)
            //                {
            //                    category = new BarManagerCategory("其他", Guid.NewGuid());
            //                    this.barManager1.Categories.Add(category);
            //                }
            //                item.Category = category;
            //            }
            //        }
            //        else if (strArray[11] != null)
            //        {
            //            item.Hint = strArray[11];
            //        }
            //        try
            //        {
            //            if (strArray[9] != null)
            //            {
            //                KeysConverter converter;
            //                if (!(item is BarSubItem))
            //                {
            //                    converter = new KeysConverter();
            //                    converter.ConvertFromString(strArray[9]).ToString();
            //                    item.ItemShortcut = new BarShortcut((Keys) converter.ConvertFromString(strArray[9]));
            //                }
            //                else if (item is BarSubItem)
            //                {
            //                    caption = (item as BarSubItem).Caption;
            //                    string[] strArray3 = strArray[9].Split(new char[] { '+' });
            //                    if ((strArray3.Length == 2) && (strArray3[0].ToLower() == "alt"))
            //                    {
            //                        (item as BarSubItem).Caption = caption + "(&" + strArray3[1] + ")";
            //                    }
            //                    else
            //                    {
            //                        converter = new KeysConverter();
            //                        converter.ConvertFromString(strArray[9]).ToString();
            //                        item.ItemShortcut = new BarShortcut((Keys) converter.ConvertFromString(strArray[9]));
            //                    }
            //                }
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    if (strArray[13] != null)
            //    {
            //        item.Glyph = new Bitmap(strArray[13]);
            //        (item.Glyph as Bitmap).MakeTransparent();
            //    }
            //    else if (strArray[12] != null)
            //    {
            //        MemoryStream stream = new MemoryStream(Convert.FromBase64String(strArray[12]));
            //        try
            //        {
            //            item.Glyph = Image.FromStream(stream) as Bitmap;
            //        }
            //        catch (Exception exception4)
            //        {
            //            exception4.ToString();
            //        }
            //        finally
            //        {
            //            if (stream != null)
            //            {
            //                stream.Dispose();
            //            }
            //        }
            //    }
            //    for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
            //    {
            //        this.method_12(item, xmlNode_0.ChildNodes[i], bool_1);
            //    }
            //    BarItemLink link = null;
            //    if (object_0 is Bar)
            //    {
            //        if (strArray[3] != null)
            //        {
            //            if (bool.Parse(strArray[3]))
            //            {
            //                if (item is BarSubItem)
            //                {
            //                    if ((item as BarSubItem).ItemLinks.Count > 0)
            //                    {
            //                        link = (object_0 as Bar).AddItem(item);
            //                    }
            //                    else
            //                    {
            //                        this.barManager1.Items.Remove(item);
            //                    }
            //                }
            //                else
            //                {
            //                    link = (object_0 as Bar).AddItem(item);
            //                }
            //            }
            //        }
            //        else if (item is BarSubItem)
            //        {
            //            if ((item as BarSubItem).ItemLinks.Count > 0)
            //            {
            //                link = (object_0 as Bar).AddItem(item);
            //            }
            //            else
            //            {
            //                this.barManager1.Items.Remove(item);
            //            }
            //        }
            //        else
            //        {
            //            link = (object_0 as Bar).AddItem(item);
            //        }
            //    }
            //    else if (object_0 is BarSubItem)
            //    {
            //        if (strArray[3] != null)
            //        {
            //            if (bool.Parse(strArray[3]))
            //            {
            //                if (item is BarSubItem)
            //                {
            //                    if ((item as BarSubItem).ItemLinks.Count > 0)
            //                    {
            //                        link = (object_0 as BarSubItem).AddItem(item);
            //                    }
            //                    else
            //                    {
            //                        this.barManager1.Items.Remove(item);
            //                    }
            //                }
            //                else
            //                {
            //                    link = (object_0 as BarSubItem).AddItem(item);
            //                }
            //            }
            //        }
            //        else if (item is BarSubItem)
            //        {
            //            if ((item as BarSubItem).ItemLinks.Count > 0)
            //            {
            //                link = (object_0 as BarSubItem).AddItem(item);
            //            }
            //            else
            //            {
            //                this.barManager1.Items.Remove(item);
            //            }
            //        }
            //        else
            //        {
            //            link = (object_0 as BarSubItem).AddItem(item);
            //        }
            //    }
            //    if ((link != null) && (strArray[2] != null))
            //    {
            //        link.BeginGroup = bool.Parse(strArray[2]);
            //    }
            //    if (((strArray[7] != null) && (this.m_pSystemPopupMenu != null)) && !bool_1)
            //    {
            //        try
            //        {
            //            if (bool.Parse(strArray[7]))
            //            {
            //                if (strArray[2] != null)
            //                {
            //                    this.m_pSystemPopupMenu.AddItem(item).BeginGroup = bool.Parse(strArray[2]);
            //                }
            //                else
            //                {
            //                    this.m_pSystemPopupMenu.AddItem(item);
            //                }
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //}
        }

        private void method_13(Bar bar_0, string[] string_1)
        {
            if (string_1[1] != null)
            {
                bar_0.Text = string_1[1];
            }
            if (string_1[2] != null)
            {
                bar_0.DockRow = short.Parse(string_1[2]);
            }
            if (string_1[3] != null)
            {
                bar_0.DockCol = short.Parse(string_1[3]);
            }
            if (string_1[4] != null)
            {
                bar_0.Visible = bool.Parse(string_1[4]);
            }
            if ((string_1[5] != null) && bool.Parse(string_1[4]))
            {
                this.barManager1.MainMenu = bar_0;
            }
        }

        private void method_14(object sender, EventArgs e)
        {
            try
            {
                this.method_7(sender as BarSubItem, null);
            }
            catch
            {
            }
        }

        private void method_15(Bar bar_0, XmlNode xmlNode_0)
        {
            for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
            {
                XmlNode node = xmlNode_0.ChildNodes[i];
                if (node.Name.ToLower() == "options")
                {
                    this.method_8(bar_0, node);
                }
                else if (node.Name.ToLower() == "baritem")
                {
                    this.method_12(bar_0, node, false);
                }
            }
        }

        private void method_16(Bar bar_0, XmlNode xmlNode_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= xmlNode_0.Attributes.Count)
                {
                    return;
                }
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        bar_0.BarName = attribute.Value;
                        break;

                    case "caption":
                        bar_0.Text = attribute.Value;
                        break;

                    case "col":
                        try
                        {
                            bar_0.DockCol = short.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "row":
                        try
                        {
                            bar_0.DockRow = short.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "ismainmenu":
                        try
                        {
                            if (bool.Parse(attribute.Value))
                            {
                                this.barManager1.MainMenu = bar_0;
                            }
                        }
                        catch
                        {
                        }
                        break;

                    case "visible":
                        try
                        {
                            bar_0.Visible = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
            }
        }

        private void method_17(string string_1)
        {
        }

        private void method_18(Bar bar_0, bool bool_1)
        {
            if (bool_1)
            {
                Delegate5 delegate1 = new Delegate5(this.method_18);
            }
            else
            {
                this.barManager1.Bars.Add(bar_0);
            }
        }

        private void method_19(Bar bar_0, IToolBarDef itoolBarDef_0)
        {
            Assembly assembly = itoolBarDef_0.GetType().Assembly;
            ItemDefClass itemDef = new ItemDefClass();
            for (int i = 0; i < itoolBarDef_0.ItemCount; i++)
            {
                itoolBarDef_0.GetItemInfo(i, itemDef);
                string iD = itemDef.ID;
                ICommand command = null;
                command = assembly.CreateInstance(iD) as ICommand;
                this.method_20(bar_0, command, itemDef.Group, itemDef.SubType);
            }
        }

        private BarItem method_2(ICommand icommand_0)
        {
            BarItem item;
            BarManagerCategory category;
            if (icommand_0 is IBarEditItem)
            {
                item = new BarEditItem();
                RepositoryItemComboBox box = new RepositoryItemComboBox();
                item.Width = (icommand_0 as IBarEditItem).Width;
                (item as BarEditItem).Edit = box;
                box.AutoHeight = false;
                box.Name = icommand_0.Name + "_ItemComboBox";
                (icommand_0 as IBarEditItem).BarEditItem = item;
            }
            else
            {
                item = new BarButtonItem();
            }
            this.barManager1.Items.Add(item);
            item.Id = this.barManager1.GetNewItemId();
            item.Name = icommand_0.Name;
            item.Tag = icommand_0;
            item.Caption = icommand_0.Caption;
            if (icommand_0.Tooltip != null)
            {
                item.Hint = icommand_0.Tooltip;
            }
            else if (icommand_0.Caption != null)
            {
                item.Hint = icommand_0.Caption;
            }
            if (icommand_0 is IShortcut)
            {
                item.ItemShortcut = new BarShortcut((icommand_0 as IShortcut).Keys);
            }
            if (icommand_0.Bitmap != 0)
            {
                try
                {
                    IntPtr hbitmap = new IntPtr(icommand_0.Bitmap);
                    item.Glyph = Image.FromHbitmap(hbitmap);
                }
                catch
                {
                }
            }
            if (icommand_0.Category != null)
            {
                if (icommand_0.Category.Length > 0)
                {
                    category = this.barManager1.Categories[icommand_0.Category];
                    if (category == null)
                    {
                        category = new BarManagerCategory(icommand_0.Category, Guid.NewGuid());
                        this.barManager1.Categories.Add(category);
                    }
                    item.Category = category;
                    return item;
                }
                category = this.barManager1.Categories["其他"];
                if (category == null)
                {
                    category = new BarManagerCategory("其他", Guid.NewGuid());
                    this.barManager1.Categories.Add(category);
                }
                item.Category = category;
                return item;
            }
            category = this.barManager1.Categories["其他"];
            if (category == null)
            {
                category = new BarManagerCategory("其他", Guid.NewGuid());
                this.barManager1.Categories.Add(category);
            }
            item.Category = category;
            return item;
        }

        private void method_20(Bar bar_0, ICommand icommand_0, bool bool_1, int int_0)
        {
            if (icommand_0 is ICommandSubType)
            {
                (icommand_0 as ICommandSubType).SetSubType(int_0);
            }
            icommand_0.OnCreate(this.iframework_0.Application);
            if (icommand_0 is IToolContextMenu)
            {
                (icommand_0 as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
            }
            string name = icommand_0.Name;
            if ((icommand_0 is ICommandLine) && ((icommand_0 as ICommandLine).CommandName != null))
            {
                name = (icommand_0 as ICommandLine).CommandName;
            }
            BarItem item = null;
            item = this.barManager1.Items[name];
            if (item == null)
            {
                if (icommand_0 is IBarEditItem)
                {
                    if ((icommand_0 as IBarEditItem).Style == BarEditStyle.ComboBoxEdit)
                    {
                        item = new BarEditItem();
                        RepositoryItemComboBox box = new RepositoryItemComboBox();
                        (item as BarEditItem).Edit = box;
                        box.AutoHeight = false;
                        box.Name = icommand_0.Name + "_ItemComboBox";
                        (icommand_0 as IBarEditItem).BarEditItem = new JLKComboxBarItem(item);
                        item.Width = (icommand_0 as IBarEditItem).Width;
                    }
                    else
                    {
                        if ((icommand_0 as IBarEditItem).Style != BarEditStyle.ButtonEdit)
                        {
                            return;
                        }
                        item = new BarButtonItem();
                        (icommand_0 as IBarEditItem).BarEditItem = new JLKButtonBarItem(item);
                        item.Width = (icommand_0 as IBarEditItem).Width;
                    }
                }
                else if (icommand_0 is IToolControl)
                {
                    item = new BarButtonItem();
                }
                else
                {
                    item = new BarButtonItem();
                }
                this.barManager1.Items.Add(item);
                item.Id = this.barManager1.GetNewItemId();
                item.Name = name;
                item.Tag = icommand_0;
                item.Caption = icommand_0.Caption;
                if (icommand_0 != null)
                {
                    BarManagerCategory category;
                    if (icommand_0.Tooltip != null)
                    {
                        item.Hint = icommand_0.Tooltip;
                    }
                    else if (icommand_0.Caption != null)
                    {
                        item.Hint = icommand_0.Caption;
                    }
                    if (icommand_0 is IShortcut)
                    {
                        item.ItemShortcut = new BarShortcut((icommand_0 as IShortcut).Keys);
                    }
                    if (icommand_0.Bitmap != 0)
                    {
                        try
                        {
                            IntPtr hbitmap = new IntPtr(icommand_0.Bitmap);
                            Bitmap bitmap = Image.FromHbitmap(hbitmap);
                            bitmap.MakeTransparent();
                            item.Glyph = bitmap;
                        }
                        catch
                        {
                        }
                    }
                    if (icommand_0.Category != null)
                    {
                        if (icommand_0.Category.Length > 0)
                        {
                            category = this.barManager1.Categories[icommand_0.Category];
                            if (category == null)
                            {
                                category = new BarManagerCategory(icommand_0.Category, Guid.NewGuid());
                                this.barManager1.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                        else
                        {
                            category = this.barManager1.Categories["其他"];
                            if (category == null)
                            {
                                category = new BarManagerCategory("其他", Guid.NewGuid());
                                this.barManager1.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                    }
                    else
                    {
                        category = this.barManager1.Categories["其他"];
                        if (category == null)
                        {
                            category = new BarManagerCategory("其他", Guid.NewGuid());
                            this.barManager1.Categories.Add(category);
                        }
                        item.Category = category;
                    }
                }
            }
            BarItemLink link = bar_0.AddItem(item);
            if (link != null)
            {
                link.BeginGroup = bool_1;
            }
        }

        private void method_21()
        {
            this.createBarsHelper_0.InvokeMethod(this.string_0, this.addWindows_0);
        }

        private void method_22()
        {
            this.method_4();
            if (this.OnBarsCreateComplete != null)
            {
                this.OnBarsCreateComplete();
            }
        }

        private void method_23(object sender, ItemClickEventArgs e)
        {
            Control tag = e.Item.Tag as Control;
            if (tag != null)
            {
                tag.Show();
            }
        }

        private void method_24()
        {
            if (this.ilist_0.Count != 0)
            {
                if (this.barSubItem_0 == null)
                {
                    this.barSubItem_0 = new BarSubItem();
                    this.barSubItem_0.Caption = "窗口";
                    this.barSubItem_0.Id = this.barManager1.GetNewItemId();
                    this.barSubItem_0.Name = "WindowMenuItem";
                    if (this.barManager1.MainMenu != null)
                    {
                        this.barManager1.MainMenu.AddItem(this.barSubItem_0);
                    }
                    else if (this.barManager1.Bars.Count > 0)
                    {
                        this.barManager1.Bars[0].AddItem(this.barSubItem_0);
                    }
                }
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    Control control = this.ilist_0[i];
                    string text = this.ilist_1[i];
                    if ((text == null) || (text.Length == 0))
                    {
                        text = control.Text;
                    }
                    BarButtonItem item = new BarButtonItem {
                        Tag = control,
                        Id = this.barManager1.GetNewItemId(),
                        Caption = text,
                        Description = text,
                        Name = control.Name + "_Windows"
                    };
                    this.barSubItem_0.AddItem(item);
                }
            }
        }

        private void method_25(Bar bar_0, XmlNode xmlNode_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= xmlNode_0.ChildNodes.Count)
                {
                    return;
                }
                XmlNode node = xmlNode_0.ChildNodes[num];
                if (node.ChildNodes.Count != 0)
                {
                    switch (node.Attributes["name"].Value)
                    {
                        case "DisableClose":
                            try
                            {
                                bar_0.OptionsBar.DisableClose = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "AllowDelete":
                            try
                            {
                                bar_0.OptionsBar.AllowDelete = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "AllowQuickCustomization":
                            try
                            {
                                bar_0.OptionsBar.AllowQuickCustomization = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "RotateWhenVertical":
                            try
                            {
                                bar_0.OptionsBar.RotateWhenVertical = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "UseWholeRow":
                            try
                            {
                                bar_0.OptionsBar.UseWholeRow = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "DisableCustomization":
                            try
                            {
                                bar_0.OptionsBar.DisableCustomization = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "Hidden":
                            try
                            {
                                bar_0.OptionsBar.Hidden = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "MultiLine":
                            try
                            {
                                bar_0.OptionsBar.MultiLine = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "DrawSizeGrip":
                            try
                            {
                                bar_0.OptionsBar.DrawSizeGrip = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "AllowRename":
                            try
                            {
                                bar_0.OptionsBar.AllowRename = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;

                        case "DrawDragBorder":
                            try
                            {
                                bar_0.OptionsBar.DrawDragBorder = bool.Parse(node.ChildNodes[0].Value);
                            }
                            catch
                            {
                            }
                            break;
                    }
                }
                num++;
            }
        }

        private LoadComponent method_3(string string_1)
        {
            LoadComponent component = this.hashtable_1[string_1] as LoadComponent;
            if (component == null)
            {
                component = new LoadComponent();
                component.LoadComponentLibrary(string_1);
                this.hashtable_0[string_1] = component;
            }
            return component;
        }

        private void method_4()
        {
            int num;
            for (num = 0; num < this.barManager1.Bars.Count; num++)
            {
                Bar bar = this.barManager1.Bars[num];
                bar.VisibleChanged += new EventHandler(this.method_5);
            }
            for (num = 0; num < this.barManager1.Items.Count; num++)
            {
                BarItem item = this.barManager1.Items[num];
                if (item is BarSubItem)
                {
                    (item as BarSubItem).Popup += new EventHandler(this.method_14);
                }
            }
        }

        private void method_5(object sender, EventArgs e)
        {
            try
            {
                Bar bar = sender as Bar;
                if (bar.Visible)
                {
                    this.method_6(sender as Bar, this.itool_0);
                }
            }
            catch
            {
            }
        }

        private void method_6(Bar bar_0, ITool itool_1)
        {
            ICommand tag = null;
            string name = "";
            if ((itool_1 != null) && (itool_1 as ICommand).Enabled)
            {
                name = (itool_1 as ICommand).Name;
            }
            for (int i = 0; i < bar_0.ItemLinks.Count; i++)
            {
                BarItem item = bar_0.ItemLinks[i].Item;
                if (item.Tag != null)
                {
                    tag = item.Tag as ICommand;
                }
                if (tag != null)
                {
                    try
                    {
                        item.Enabled = tag.Enabled;
                    }
                    catch
                    {
                    }
                    BarButtonItem item2 = item as BarButtonItem;
                    if (item2 != null)
                    {
                        if (tag.Name == name)
                        {
                            item2.ButtonStyle = BarButtonStyle.Check;
                            item2.Down = true;
                        }
                        else if (tag.Checked)
                        {
                            item2.ButtonStyle = BarButtonStyle.Check;
                            item2.Down = true;
                        }
                        else
                        {
                            item2.ButtonStyle = BarButtonStyle.Default;
                            item2.Down = false;
                        }
                    }
                }
            }
        }

        private void method_7(BarSubItem barSubItem_1, ITool itool_1)
        {
            string name = "";
            if ((itool_1 != null) && (itool_1 as ICommand).Enabled)
            {
                name = (itool_1 as ICommand).Name;
            }
            ICommand tag = null;
            for (int i = 0; i < barSubItem_1.ItemLinks.Count; i++)
            {
                BarItem item = barSubItem_1.ItemLinks[i].Item;
                if (item.Tag != null)
                {
                    tag = item.Tag as ICommand;
                }
                if (tag != null)
                {
                    try
                    {
                        item.Enabled = tag.Enabled;
                    }
                    catch
                    {
                    }
                    BarButtonItem item2 = item as BarButtonItem;
                    if (item2 != null)
                    {
                        if (tag.Name == name)
                        {
                            item2.ButtonStyle = BarButtonStyle.Check;
                            item2.Down = true;
                        }
                        else if (tag.Checked)
                        {
                            item2.ButtonStyle = BarButtonStyle.Check;
                            item2.Down = true;
                        }
                        else
                        {
                            item2.ButtonStyle = BarButtonStyle.Default;
                            item2.Down = false;
                        }
                    }
                }
            }
        }

        private void method_8(Bar bar_0, XmlNode xmlNode_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= xmlNode_0.Attributes.Count)
                {
                    return;
                }
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "disableclose":
                        try
                        {
                            bar_0.OptionsBar.DisableClose = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "allowdelete":
                        try
                        {
                            bar_0.OptionsBar.AllowDelete = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "multiline":
                        try
                        {
                            bar_0.OptionsBar.MultiLine = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;

                    case "drawdragborder":
                        try
                        {
                            bar_0.OptionsBar.DrawDragBorder = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
            }
        }

        private Assembly method_9(string string_1)
        {
            Assembly assembly = this.hashtable_1[string_1] as Assembly;
            if (assembly == null)
            {
                assembly = Assembly.Load(string_1);
                this.hashtable_1[string_1] = assembly;
            }
            return assembly;
        }

        public void RegisterWindow(Control control_0, string string_1)
        {
            this.ilist_0.Add(control_0);
            this.ilist_1.Add(string_1);
        }

        public void RegisterWindow1(Control control_0, string string_1)
        {
            if (this.barSubItem_0 == null)
            {
                this.barSubItem_0 = new BarSubItem();
                this.barSubItem_0.Caption = "窗口";
                this.barSubItem_0.Id = this.barManager1.GetNewItemId();
                this.barSubItem_0.Name = "WindowMenuItem";
                if (this.barManager1.MainMenu != null)
                {
                    this.barManager1.MainMenu.AddItem(this.barSubItem_0);
                }
                else if (this.barManager1.Bars.Count > 0)
                {
                    this.barManager1.Bars[0].AddItem(this.barSubItem_0);
                }
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                string_1 = control_0.Text;
            }
            BarButtonItem item = new BarButtonItem {
                Tag = control_0,
                Id = this.barManager1.GetNewItemId(),
                Caption = string_1,
                Description = string_1,
                Name = control_0.Name + "_Windows"
            };
            this.barSubItem_0.AddItem(item);
        }

        public void SetContextMenu(Control control_0)
        {
            if (this.m_pCurrentPopupMenu.ItemLinks.Count > 0)
            {
                this.barManager1.SetPopupContextMenu(control_0, this.m_pCurrentPopupMenu);
            }
        }

        public void SetPopupContextMenu(Control control_0)
        {
            if (this.m_pSystemPopupMenu != null)
            {
                this.barManager1.SetPopupContextMenu(control_0, this.m_pSystemPopupMenu);
            }
        }

        public void SetPopupContextMenu(Control control_0, object object_0)
        {
            if (object_0 is Array)
            {
                this.m_pCurrentPopupMenu.Manager = this.barManager1;
                PopupMenu pCurrentPopupMenu = this.m_pCurrentPopupMenu;
                pCurrentPopupMenu.ClearLinks();
                Array array = object_0 as Array;
                BarItem item = null;
                IEnumerator enumerator = array.GetEnumerator();
                enumerator.Reset();
                while (enumerator.MoveNext())
                {
                    item = this.barManager1.Items[enumerator.Current as string];
                    if (item != null)
                    {
                        pCurrentPopupMenu.AddItem(item);
                    }
                }
                if (pCurrentPopupMenu.ItemLinks.Count > 0)
                {
                    this.barManager1.SetPopupContextMenu(control_0, pCurrentPopupMenu);
                }
                else
                {
                    this.barManager1.SetPopupContextMenu(control_0, this.m_pSystemPopupMenu);
                }
            }
            else
            {
                this.barManager1.SetPopupContextMenu(control_0, this.m_pSystemPopupMenu);
            }
        }

        public void StartCreateBar()
        {
            try
            {
                this.list_0.Clear();
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(this.string_0);
                }
                catch
                {
                }
                XmlElement documentElement = document.DocumentElement;
                Bar bar = null;
                int num = 0;
            Label_002E:
                if (num >= documentElement.ChildNodes.Count)
                {
                    goto Label_01DE;
                }
                XmlNode node = documentElement.ChildNodes[num];
                if (!(node.Name.ToLower() == "bar"))
                {
                    goto Label_01C4;
                }
                string[] strArray = this.method_11(node);
                IToolBarDef def = null;
                if ((strArray[7] != null) && (strArray[8] != null))
                {
                    def = this.method_3(strArray[7]).LoadClass(strArray[8]) as IToolBarDef;
                    if (strArray[0] == null)
                    {
                        strArray[0] = def.Name;
                    }
                    if (strArray[1] == null)
                    {
                        strArray[1] = def.Caption;
                    }
                }
                if (strArray[0] == null)
                {
                    goto Label_01CD;
                }
                try
                {
                    bar = this.barManager1.Bars[strArray[0]];
                    goto Label_01CD;
                }
                catch
                {
                    goto Label_01CD;
                }
            Label_0105:
                bar = new Bar();
                bool flag = true;
                bar.BarName = this.barManager1.GetNewBarName();
                bar.DockCol = 0;
                bar.DockRow = this.barManager1.Bars.Count;
                bar.FloatSize = new Size(0x120, 0x18);
                this.method_13(bar, strArray);
                if (bar.DockStyle == BarDockStyle.None)
                {
                    bar.DockStyle = BarDockStyle.Top;
                }
                if (def != null)
                {
                    this.method_19(bar, def);
                }
            Label_017C:
                this.method_15(bar, node);
                if (flag && (bar.ItemLinks.Count == 0))
                {
                    int index = this.barManager1.Bars.IndexOf(bar);
                    this.barManager1.Bars.RemoveAt(index);
                }
            Label_01C4:
                num++;
                goto Label_002E;
            Label_01CD:
                flag = false;
                if (bar != null)
                {
                    goto Label_017C;
                }
                goto Label_0105;
            Label_01DE:
                if ((this.barManager1.MainMenu == null) && (this.barManager1.Bars.Count > 0))
                {
                    bar = this.barManager1.Bars[1];
                    this.barManager1.MainMenu = bar;
                    bar.OptionsBar.DisableClose = false;
                    bar.OptionsBar.AllowQuickCustomization = false;
                    bar.OptionsBar.DrawDragBorder = false;
                    bar.OptionsBar.MultiLine = true;
                    bar.OptionsBar.RotateWhenVertical = false;
                }
                this.method_4();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                MessageBox.Show("无法创建菜单和工具栏，请检查文件" + this.string_0 + "是否正确!");
            }
        }

        public bool UpdateUI(ITool itool_1)
        {
            this.itool_0 = itool_1;
            if (this.barManager1 != null)
            {
                if ((itool_1 != null) && (itool_1 as ICommand).Enabled)
                {
                    string name = (itool_1 as ICommand).Name;
                }
                for (int i = 0; i < this.barManager1.Bars.Count; i++)
                {
                    Bar bar = this.barManager1.Bars[i];
                    if (bar.Visible)
                    {
                        this.method_6(bar, itool_1);
                    }
                }
                return true;
            }
            return false;
        }

        public bool UpdateUI(string string_1, bool bool_1, bool bool_2)
        {
            if (this.barManager1 != null)
            {
                BarItem item = this.barManager1.Items[string_1];
                if (item != null)
                {
                    item.Enabled = bool_1;
                    BarButtonItem item2 = item as BarButtonItem;
                    if (item2 != null)
                    {
                        if (bool_2)
                        {
                            item2.ButtonStyle = BarButtonStyle.Check;
                            item2.Down = true;
                        }
                        else
                        {
                            item2.ButtonStyle = BarButtonStyle.Default;
                            item2.Down = false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public BarManager BarManager
        {
            set
            {
                this.barManager1 = value;
                this.m_pCurrentPopupMenu.Manager = value;
                this.m_pSystemPopupMenu.Manager = value;
            }
        }

        public object Framework
        {
            get
            {
                return this.iframework_0;
            }
            set
            {
                this.iframework_0 = value as IFramework;
            }
        }

        public bool HasMainMenu
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public string PaintStyleName
        {
            get
            {
                if (this.barManager1 == null)
                {
                    return "";
                }
                string paintStyleName = this.barManager1.GetController().PaintStyleName;
                if (paintStyleName == "Skin")
                {
                    paintStyleName = paintStyleName + " " + UserLookAndFeel.Default.ActiveSkinName;
                }
                return paintStyleName;
            }
        }

        public PopupMenu PopupMenu
        {
            set
            {
                this.m_pSystemPopupMenu = value;
            }
        }

        private delegate void Delegate4(string string_0);

        private delegate void Delegate5(Bar bar_0, bool bool_0);
    }
}

