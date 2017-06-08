using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Priviliges;
using Yutai.Shared;

namespace Yutai.ArcGIS.Framework
{
    internal sealed class CreateBarsHelper : Control
    {
        private BarManager barManager_0;
        private bool bool_0 = true;
        private Delegate delegate_0 = null;
        private Hashtable hashtable_0 = new Hashtable();
        private Hashtable hashtable_1 = new Hashtable();
        private IFramework iframework_0;
        private IPopuMenuWrap ipopuMenuWrap_0;
        private List<ICommand> list_0;
        private MenuInfoHelper menuInfoHelper_0 = new MenuInfoHelper();
        private PopupMenu popupMenu_0;
        private SysGrants sysGrants_0;

        internal event OnCreateCompleteHandler OnCreateComplete;

        public CreateBarsHelper(BarManager barManager_1, PopupMenu popupMenu_1, IFramework iframework_1, List<ICommand> list_1, IPopuMenuWrap ipopuMenuWrap_1)
        {
            this.CreateHandle();
            base.CreateControl();
            this.popupMenu_0 = popupMenu_1;
            this.barManager_0 = barManager_1;
            this.iframework_0 = iframework_1;
            this.list_0 = list_1;
            this.ipopuMenuWrap_0 = ipopuMenuWrap_1;
            this.sysGrants_0 = new SysGrants(AppConfigInfo.UserID);
        }

        public void InvokeMethod(string string_0, Delegate delegate_1)
        {
            this.delegate_0 = delegate_1;
            if (!base.IsDisposed && base.IsHandleCreated)
            {
                try
                {
                    base.Invoke(new MessageHandler(this.StartCreateBar), new object[] { string_0 });
                }
                catch (Exception)
                {
                }
            }
        }

        private void method_0(Bar bar_0, bool bool_1)
        {
            if (bool_1)
            {
                Delegate3 method = new Delegate3(this.method_0);
                base.Invoke(method, new object[] { bar_0, false });
            }
            else
            {
                this.barManager_0.Bars.Add(bar_0);
            }
        }

        private void method_1(Bar bar_0, string[] string_0)
        {
            if (string_0[1] != null)
            {
                bar_0.Text = string_0[1];
            }
            if (string_0[2] != null)
            {
                bar_0.DockRow = short.Parse(string_0[2]);
            }
            if (string_0[3] != null)
            {
                bar_0.DockCol = short.Parse(string_0[3]);
            }
            if (string_0[4] != null)
            {
                bar_0.Visible = bool.Parse(string_0[4]);
            }
            if ((string_0[5] != null) && bool.Parse(string_0[4]))
            {
                this.barManager_0.MainMenu = bar_0;
            }
        }

        private LoadComponent method_10(string string_0)
        {
            LoadComponent component = this.hashtable_1[string_0] as LoadComponent;
            if (component == null)
            {
                component = new LoadComponent();
                component.LoadComponentLibrary(string_0);
                this.hashtable_0[string_0] = component;
            }
            return component;
        }

        private Assembly method_11(string string_0)
        {
            Assembly assembly = this.hashtable_1[string_0] as Assembly;
            if (assembly == null)
            {
                assembly = Assembly.Load(string_0);
                this.hashtable_1[string_0] = assembly;
            }
            return assembly;
        }

        private void method_12()
        {
        }

        private void method_2(Bar bar_0, XmlNode xmlNode_0)
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
                                this.barManager_0.MainMenu = bar_0;
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

        private void method_3(Bar bar_0, XmlNode xmlNode_0)
        {
            for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
            {
                XmlNode node = xmlNode_0.ChildNodes[i];
                if (node.Name.ToLower() == "options")
                {
                    this.method_9(bar_0, node);
                }
                else if (node.Name.ToLower() == "baritem")
                {
                    this.method_6(bar_0, node, false);
                }
            }
        }

        private void method_4(Bar bar_0, IToolBarDef itoolBarDef_0)
        {
            Assembly assembly = itoolBarDef_0.GetType().Assembly;
            ItemDefClass itemDef = new ItemDefClass();
            for (int i = 0; i < itoolBarDef_0.ItemCount; i++)
            {
                itoolBarDef_0.GetItemInfo(i, itemDef);
                string iD = itemDef.ID;
                ICommand command = null;
                command = assembly.CreateInstance(iD) as ICommand;
                this.method_5(bar_0, command, itemDef.Group, itemDef.SubType);
            }
        }

        private void method_5(Bar bar_0, ICommand icommand_0, bool bool_1, int int_0)
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
            item = this.barManager_0.Items[name];
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
                this.barManager_0.Items.Add(item);
                item.Id = this.barManager_0.GetNewItemId();
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
                            category = this.barManager_0.Categories[icommand_0.Category];
                            if (category == null)
                            {
                                category = new BarManagerCategory(icommand_0.Category, Guid.NewGuid());
                                this.barManager_0.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                        else
                        {
                            category = this.barManager_0.Categories["其他"];
                            if (category == null)
                            {
                                category = new BarManagerCategory("其他", Guid.NewGuid());
                                this.barManager_0.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                    }
                    else
                    {
                        category = this.barManager_0.Categories["其他"];
                        if (category == null)
                        {
                            category = new BarManagerCategory("其他", Guid.NewGuid());
                            this.barManager_0.Categories.Add(category);
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

        private void method_6(object object_0, XmlNode xmlNode_0, bool bool_1)
        {
            int num;
            Exception exception;
            string str = "0";
            string[] strArray = this.method_7(xmlNode_0, out str);
            ICommand command = null;
            if ((strArray[4] != null) && (strArray[5] != null))
            {
                try
                {
                    if ((AppConfigInfo.UserID != "admin") && !this.sysGrants_0.StaffIsHasMenuPri(this.menuInfoHelper_0, strArray[4], strArray[5]))
                    {
                        return;
                    }
                    if (strArray[4][1] != ':')
                    {
                        strArray[4] = Application.StartupPath + @"\" + strArray[4];
                    }
                    if (File.Exists(strArray[4]))
                    {
                        command = this.method_10(strArray[4]).LoadClass(strArray[5]) as ICommand;
                        if (command != null)
                        {
                            if (command is IToolContextMenu)
                            {
                                (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
                            }
                            if (strArray[6] != null)
                            {
                                try
                                {
                                    num = int.Parse(strArray[6]);
                                    (command as ICommandSubType).SetSubType(num);
                                }
                                catch
                                {
                                    Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的子命令设置失败", strArray[4], strArray[5]));
                                }
                            }
                            if ((strArray[14] != null) && bool.Parse(strArray[14]))
                            {
                               // this.iframework_0.Application.AddCommands(command);
                            }
                        }
                        else
                        {
                            Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", strArray[4], strArray[5]));
                        }
                    }
                    else
                    {
                        Logger.Current.Error("", null, string.Format("文件{0}不存在！", strArray[4]));
                    }
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    Logger.Current.Error("", exception, string.Format("创建{0}中的类{1}的失败", strArray[4], strArray[5]));
                }
            }
            else if (strArray[8] != null)
            {
                try
                {
                    if ((AppConfigInfo.UserID != "admin") && !this.sysGrants_0.StaffIsHasMenuPri(this.menuInfoHelper_0, strArray[8]))
                    {
                        return;
                    }
                    command = Activator.CreateInstance(Type.GetTypeFromProgID(strArray[8])) as ICommand;
                    if (command != null)
                    {
                        if (command is IToolContextMenu)
                        {
                            (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
                        }
                        if (strArray[6] != null)
                        {
                            try
                            {
                                num = int.Parse(strArray[6]);
                                (command as ICommandSubType).SetSubType(num);
                            }
                            catch
                            {
                                Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的子命令设置失败", strArray[4], strArray[5]));
                            }
                        }
                        if ((strArray[14] != null) && bool.Parse(strArray[14]))
                        {
                            //this.iframework_0.Application.AddCommands(command);
                        }
                    }
                    else
                    {
                        Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", strArray[4], strArray[5]));
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    Logger.Current.Error("", exception, "创建" + strArray[4] + "中的类" + strArray[5] + "失败");
                }
            }
            else if (strArray[10] != null)
            {
                try
                {
                    int index = strArray[10].IndexOf(',');
                    string typeName = strArray[10].Substring(0, index);
                    string str3 = strArray[10].Substring(index + 1).Trim();
                    command = this.method_11(str3).CreateInstance(typeName) as ICommand;
                    if (command != null)
                    {
                        if (command is IToolContextMenu)
                        {
                            (command as IToolContextMenu).PopuMenu = this.ipopuMenuWrap_0;
                        }
                        if (strArray[6] != null)
                        {
                            try
                            {
                                num = int.Parse(strArray[6]);
                                (command as ICommandSubType).SetSubType(num);
                            }
                            catch
                            {
                                Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的子命令设置失败", strArray[4], strArray[5]));
                            }
                        }
                        if ((strArray[14] != null) && bool.Parse(strArray[14]))
                        {
                         //   this.iframework_0.Application.AddCommands(command);
                        }
                    }
                    else
                    {
                        Logger.Current.Error("", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", strArray[4], strArray[5]));
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                    Logger.Current.Error("", exception, "创建" + strArray[4] + "中的类" + strArray[5] + "失败");
                }
            }
            if ((strArray[0] != null) || (command != null))
            {
                if (command is ICommandLine)
                {
                    if ((command as ICommandLine).CommandName != null)
                    {
                        strArray[0] = (command as ICommandLine).CommandName;
                    }
                }
                else if ((command != null) && (command.Name != null))
                {
                    strArray[0] = command.Name;
                }
                BarItem item = null;
                if (strArray[0].Length > 0)
                {
                    item = this.barManager_0.Items[strArray[0]];
                }
                if (item == null)
                {
                    BarManagerCategory category;
                    string caption;
                    if (command != null)
                    {
                        command.OnCreate((object)this.iframework_0.Application);
                    }
                    if ((str != "0") && (command != null))
                    {
                        this.list_0.Add(command);
                    }
                    if (xmlNode_0.ChildNodes.Count > 0)
                    {
                        item = new BarSubItem();
                        this.barManager_0.Items.Add(item);
                        if (object_0 is Bar)
                        {
                            category = this.barManager_0.Categories["弹出式菜单"];
                            if (category == null)
                            {
                                category = new BarManagerCategory("弹出式菜单", Guid.NewGuid());
                                this.barManager_0.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                        else if (object_0 is BarSubItem)
                        {
                            caption = (object_0 as BarSubItem).Caption;
                            category = this.barManager_0.Categories[caption];
                            if (category == null)
                            {
                                category = new BarManagerCategory(caption, Guid.NewGuid());
                                this.barManager_0.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                    }
                    else
                    {
                        if (command is IBarEditItem)
                        {
                            if ((command as IBarEditItem).Style == BarEditStyle.ComboBoxEdit)
                            {
                                item = new BarEditItem();
                                RepositoryItemComboBox box = new RepositoryItemComboBox();
                                (item as BarEditItem).Edit = box;
                                box.AutoHeight = false;
                                box.Name = command.Name + "_ItemComboBox";
                                (command as IBarEditItem).BarEditItem = new JLKComboxBarItem(item);
                                item.Width = (command as IBarEditItem).Width;
                            }
                            else
                            {
                                if ((command as IBarEditItem).Style != BarEditStyle.ButtonEdit)
                                {
                                    return;
                                }
                                item = new BarButtonItem();
                                (command as IBarEditItem).BarEditItem = new JLKButtonBarItem(item);
                                item.Width = (command as IBarEditItem).Width;
                            }
                        }
                        else if (command is IToolControl)
                        {
                            item = new BarButtonItem();
                        }
                        else
                        {
                            item = new BarButtonItem();
                        }
                        this.barManager_0.Items.Add(item);
                    }
                    item.Id = this.barManager_0.GetNewItemId();
                    item.Name = strArray[0];
                    item.Tag = command;
                    if (strArray[1] != null)
                    {
                        item.Caption = strArray[1];
                    }
                    if (command != null)
                    {
                        if (strArray[11] != null)
                        {
                            item.Hint = strArray[11];
                        }
                        else if (command.Tooltip != null)
                        {
                            item.Hint = command.Tooltip;
                        }
                        else if (command.Caption != null)
                        {
                            item.Hint = command.Caption;
                        }
                        if ((command is IShortcut) && (strArray[9] == null))
                        {
                            item.ItemShortcut = new BarShortcut((command as IShortcut).Keys);
                        }
                        if (((strArray[12] == null) && (strArray[13] == null)) && (command.Bitmap != 0))
                        {
                            try
                            {
                                IntPtr hbitmap = new IntPtr(command.Bitmap);
                                Bitmap bitmap = Image.FromHbitmap(hbitmap);
                                bitmap.MakeTransparent();
                                item.Glyph = bitmap;
                            }
                            catch
                            {
                            }
                        }
                        if (strArray[1] == null)
                        {
                            item.Caption = command.Caption;
                        }
                        if (command.Category != null)
                        {
                            if (command.Category.Length > 0)
                            {
                                category = this.barManager_0.Categories[command.Category];
                                if (category == null)
                                {
                                    category = new BarManagerCategory(command.Category, Guid.NewGuid());
                                    this.barManager_0.Categories.Add(category);
                                }
                                item.Category = category;
                            }
                            else
                            {
                                category = this.barManager_0.Categories["其他"];
                                if (category == null)
                                {
                                    category = new BarManagerCategory("其他", Guid.NewGuid());
                                    this.barManager_0.Categories.Add(category);
                                }
                                item.Category = category;
                            }
                        }
                        else
                        {
                            category = this.barManager_0.Categories["其他"];
                            if (category == null)
                            {
                                category = new BarManagerCategory("其他", Guid.NewGuid());
                                this.barManager_0.Categories.Add(category);
                            }
                            item.Category = category;
                        }
                    }
                    else if (strArray[11] != null)
                    {
                        item.Hint = strArray[11];
                    }
                    try
                    {
                        if (strArray[9] != null)
                        {
                            KeysConverter converter;
                            if (!(item is BarSubItem))
                            {
                                converter = new KeysConverter();
                                converter.ConvertFromString(strArray[9]).ToString();
                                item.ItemShortcut = new BarShortcut((Keys) converter.ConvertFromString(strArray[9]));
                            }
                            else if (item is BarSubItem)
                            {
                                caption = (item as BarSubItem).Caption;
                                string[] strArray3 = strArray[9].Split(new char[] { '+' });
                                if ((strArray3.Length == 2) && (strArray3[0].ToLower() == "alt"))
                                {
                                    (item as BarSubItem).Caption = caption + "(&" + strArray3[1] + ")";
                                }
                                else
                                {
                                    converter = new KeysConverter();
                                    converter.ConvertFromString(strArray[9]).ToString();
                                    item.ItemShortcut = new BarShortcut((Keys) converter.ConvertFromString(strArray[9]));
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (strArray[13] != null)
                    {
                        item.Glyph = new Bitmap(strArray[13]);
                        (item.Glyph as Bitmap).MakeTransparent();
                    }
                    else if (strArray[12] != null)
                    {
                        MemoryStream stream = new MemoryStream(Convert.FromBase64String(strArray[12]));
                        try
                        {
                            item.Glyph = Image.FromStream(stream) as Bitmap;
                        }
                        catch (Exception exception4)
                        {
                            exception4.ToString();
                        }
                        finally
                        {
                            if (stream != null)
                            {
                                stream.Dispose();
                            }
                        }
                    }
                }
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    this.method_6(item, xmlNode_0.ChildNodes[i], bool_1);
                }
                BarItemLink link = null;
                if (object_0 is Bar)
                {
                    if (strArray[3] != null)
                    {
                        if (bool.Parse(strArray[3]))
                        {
                            if (item is BarSubItem)
                            {
                                if ((item as BarSubItem).ItemLinks.Count > 0)
                                {
                                    link = (object_0 as Bar).AddItem(item);
                                }
                                else
                                {
                                    this.barManager_0.Items.Remove(item);
                                }
                            }
                            else
                            {
                                link = (object_0 as Bar).AddItem(item);
                            }
                        }
                    }
                    else if (item is BarSubItem)
                    {
                        if ((item as BarSubItem).ItemLinks.Count > 0)
                        {
                            link = (object_0 as Bar).AddItem(item);
                        }
                        else
                        {
                            this.barManager_0.Items.Remove(item);
                        }
                    }
                    else
                    {
                        link = (object_0 as Bar).AddItem(item);
                    }
                }
                else if (object_0 is BarSubItem)
                {
                    if (strArray[3] != null)
                    {
                        if (bool.Parse(strArray[3]))
                        {
                            if (item is BarSubItem)
                            {
                                if ((item as BarSubItem).ItemLinks.Count > 0)
                                {
                                    link = (object_0 as BarSubItem).AddItem(item);
                                }
                                else
                                {
                                    this.barManager_0.Items.Remove(item);
                                }
                            }
                            else
                            {
                                link = (object_0 as BarSubItem).AddItem(item);
                            }
                        }
                    }
                    else if (item is BarSubItem)
                    {
                        if ((item as BarSubItem).ItemLinks.Count > 0)
                        {
                            link = (object_0 as BarSubItem).AddItem(item);
                        }
                        else
                        {
                            this.barManager_0.Items.Remove(item);
                        }
                    }
                    else
                    {
                        link = (object_0 as BarSubItem).AddItem(item);
                    }
                }
                if ((link != null) && (strArray[2] != null))
                {
                    link.BeginGroup = bool.Parse(strArray[2]);
                }
                if (((strArray[7] != null) && (this.popupMenu_0 != null)) && !bool_1)
                {
                    try
                    {
                        if (bool.Parse(strArray[7]))
                        {
                            if (strArray[2] != null)
                            {
                                this.popupMenu_0.AddItem(item).BeginGroup = bool.Parse(strArray[2]);
                            }
                            else
                            {
                                this.popupMenu_0.AddItem(item);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private string[] method_7(XmlNode xmlNode_0, out string string_0)
        {
            string[] strArray = new string[15];
            string_0 = "0";
            int num = 0;
            while (true)
            {
                if (num >= xmlNode_0.Attributes.Count)
                {
                    return strArray;
                }
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "hooktype":
                        string_0 = attribute.Value;
                        break;

                    case "name":
                        strArray[0] = attribute.Value;
                        break;

                    case "caption":
                        strArray[1] = attribute.Value;
                        break;

                    case "showinmaps":
                        strArray[14] = attribute.Value;
                        break;

                    case "begingroup":
                        strArray[2] = attribute.Value;
                        break;

                    case "visible":
                        strArray[3] = attribute.Value;
                        break;

                    case "path":
                        strArray[4] = attribute.Value;
                        break;

                    case "classname":
                        strArray[5] = attribute.Value;
                        break;

                    case "subtype":
                        strArray[6] = attribute.Value;
                        break;

                    case "ispopupmenu":
                        strArray[7] = attribute.Value;
                        break;

                    case "progid":
                        strArray[8] = attribute.Value;
                        break;

                    case "shortcut":
                        strArray[9] = attribute.Value;
                        break;

                    case "type":
                        strArray[10] = attribute.Value;
                        break;

                    case "tooltip":
                        strArray[11] = attribute.Value;
                        break;

                    case "image":
                        strArray[12] = attribute.Value;
                        break;

                    case "bitmap":
                    {
                        string path = attribute.Value;
                        if ((path.Length > 3) && (path[1] != ':'))
                        {
                            path = Application.StartupPath + @"\" + path;
                            try
                            {
                                if (File.Exists(path))
                                {
                                    strArray[13] = path;
                                }
                            }
                            catch
                            {
                            }
                        }
                        break;
                    }
                }
                num++;
            }
        }

        private string[] method_8(XmlNode xmlNode_0)
        {
            string[] strArray = new string[9];
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        strArray[0] = attribute.Value;
                        break;

                    case "caption":
                        strArray[1] = attribute.Value;
                        break;

                    case "row":
                        strArray[2] = attribute.Value;
                        break;

                    case "col":
                        strArray[3] = attribute.Value;
                        break;

                    case "visible":
                        strArray[4] = attribute.Value;
                        break;

                    case "ismainmenu":
                        strArray[5] = attribute.Value;
                        break;

                    case "ispopupmenu":
                        strArray[6] = attribute.Value;
                        break;

                    case "path":
                        strArray[7] = attribute.Value;
                        if (strArray[7][1] != ':')
                        {
                            strArray[7] = string.Format(@"{0}\{1}", Application.StartupPath, strArray[7]);
                        }
                        break;

                    case "classname":
                        strArray[8] = attribute.Value;
                        break;
                }
            }
            return strArray;
        }

        private void method_9(Bar bar_0, XmlNode xmlNode_0)
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

        internal void StartCreateBar(string string_0)
        {
            try
            {
                this.list_0.Clear();
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(string_0);
                }
                catch
                {
                }
                XmlElement documentElement = document.DocumentElement;
                Bar bar = null;
                int num = 0;
            Label_0029:
                if (num >= documentElement.ChildNodes.Count)
                {
                    goto Label_01EB;
                }
                XmlNode node = documentElement.ChildNodes[num];
                if (!(node.Name.ToLower() == "bar"))
                {
                    goto Label_01D1;
                }
                string[] strArray = this.method_8(node);
                IToolBarDef def = null;
                if ((strArray[7] != null) && (strArray[8] != null))
                {
                    def = this.method_10(strArray[7]).LoadClass(strArray[8]) as IToolBarDef;
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
                    goto Label_01DA;
                }
                try
                {
                    bar = this.barManager_0.Bars[strArray[0]];
                    goto Label_01DA;
                }
                catch
                {
                    goto Label_01DA;
                }
            Label_0100:
                bar = new Bar();
                bool flag = true;
                bar.BarName = this.barManager_0.GetNewBarName();
                bar.DockCol = 0;
                bar.DockRow = this.barManager_0.Bars.Count;
                bar.FloatSize = new Size(0x120, 0x18);
                this.barManager_0.Bars.Add(bar);
                this.method_1(bar, strArray);
                if (bar.DockStyle == BarDockStyle.None)
                {
                    bar.DockStyle = BarDockStyle.Top;
                }
                if (def != null)
                {
                    this.method_4(bar, def);
                }
            Label_0189:
                this.method_3(bar, node);
                if (flag && (bar.ItemLinks.Count == 0))
                {
                    int index = this.barManager_0.Bars.IndexOf(bar);
                    this.barManager_0.Bars.RemoveAt(index);
                }
            Label_01D1:
                num++;
                goto Label_0029;
            Label_01DA:
                flag = false;
                if (bar != null)
                {
                    goto Label_0189;
                }
                goto Label_0100;
            Label_01EB:
                if ((this.bool_0 && (this.barManager_0.MainMenu == null)) && (this.barManager_0.Bars.Count > 1))
                {
                    bar = this.barManager_0.Bars[1];
                    this.barManager_0.MainMenu = bar;
                    bar.OptionsBar.DisableClose = false;
                    bar.OptionsBar.AllowQuickCustomization = false;
                    bar.OptionsBar.DrawDragBorder = false;
                    bar.OptionsBar.MultiLine = true;
                    bar.OptionsBar.RotateWhenVertical = false;
                }
                if (this.OnCreateComplete != null)
                {
                    this.OnCreateComplete();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                MessageBox.Show("无法创建菜单和工具栏，请检查文件" + string_0 + "是否正确!");
            }
        }

        public bool HasMainMenu
        {
            set
            {
                this.bool_0 = value;
            }
        }

        private delegate void Delegate3(Bar bar_0, bool bool_0);

        internal delegate void MessageHandler(string string_0);

        internal delegate void OnCreateCompleteHandler();
    }
}

