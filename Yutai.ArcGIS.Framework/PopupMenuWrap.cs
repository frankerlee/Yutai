using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Framework;
using LoadComponent = Yutai.ArcGIS.Common.LoadComponent;

namespace Yutai.ArcGIS.Framework
{
    public class PopupMenuWrap : IPopuMenuWrap
    {
        private Hashtable hashtable_0 = new Hashtable();
        private Hashtable hashtable_1 = new Hashtable();
        private PopupMenu popupMenu_0;

        public event OnItemClickEventHandler OnItemClickEvent;

        public void AddItem(MenuItemDef menuItemDef)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(menuItemDef.BeginGroup))
            {
                flag = bool.Parse(menuItemDef.BeginGroup);
            }
            if (menuItemDef.HasSubMenu)
            {
                this.ClearSubItem(menuItemDef.Name);
                this.AddSubmenuItem(menuItemDef.Name, menuItemDef.Caption,
                    string.IsNullOrEmpty(menuItemDef.MainMenuItem) ? "" : menuItemDef.MainMenuItem, flag);
            }
            else
            {
                BarSubItem item = null;
                if (!string.IsNullOrEmpty(menuItemDef.MainMenuItem))
                {
                    item = this.popupMenu_0.Manager.Items[menuItemDef.MainMenuItem] as BarSubItem;
                    if (item == null)
                    {
                        item = new BarSubItem
                        {
                            Name = menuItemDef.MainMenuItem
                        };
                        this.popupMenu_0.Manager.Items.Add(item);
                        if (flag)
                        {
                            this.popupMenu_0.AddItem(item).BeginGroup = true;
                        }
                        else
                        {
                            this.popupMenu_0.AddItem(item);
                        }
                    }
                }
                BarItem item2 = null;
                item2 = this.popupMenu_0.Manager.Items[menuItemDef.Name];
                if (item2 == null)
                {
                    item2 = this.method_1(menuItemDef);
                }
                if (item2 != null)
                {
                    if (item != null)
                    {
                        item.ItemLinks.Add(item2).BeginGroup = flag;
                    }
                    else if (flag)
                    {
                        this.popupMenu_0.AddItem(item2).BeginGroup = true;
                    }
                    else
                    {
                        this.popupMenu_0.AddItem(item2);
                    }
                }
            }
        }

        public void AddItem(string string_0, bool bool_0)
        {
            BarItem item = null;
            item = this.popupMenu_0.Manager.Items[string_0];
            if (item != null)
            {
                if (item.Tag is ICommand)
                {
                    item.Enabled = (item.Tag as ICommand).Enabled;
                    item.Caption = (item.Tag as ICommand).Caption;
                }
                if (bool_0)
                {
                    this.popupMenu_0.AddItem(item).BeginGroup = true;
                }
                else
                {
                    this.popupMenu_0.AddItem(item);
                }
            }
        }

        public void AddItem(string string_0, string string_1, bool bool_0)
        {
            BarSubItem item = null;
            if (string_1.Length > 0)
            {
                item = this.popupMenu_0.Manager.Items[string_1] as BarSubItem;
                if (item == null)
                {
                    item = new BarSubItem
                    {
                        Name = string_1
                    };
                    this.popupMenu_0.Manager.Items.Add(item);
                    if (bool_0)
                    {
                        this.popupMenu_0.AddItem(item).BeginGroup = true;
                    }
                    else
                    {
                        this.popupMenu_0.AddItem(item);
                    }
                }
            }
            BarItem item2 = null;
            item2 = this.popupMenu_0.Manager.Items[string_0];
            if (item2 != null)
            {
                if (item2.Tag is ICommand)
                {
                    item2.Enabled = (item2.Tag as ICommand).Enabled;
                    item2.Caption = (item2.Tag as ICommand).Caption;
                }
                if (item != null)
                {
                    item.ItemLinks.Add(item2).BeginGroup = bool_0;
                }
                else if (bool_0)
                {
                    this.popupMenu_0.AddItem(item2).BeginGroup = true;
                }
                else
                {
                    this.popupMenu_0.AddItem(item2);
                }
            }
        }

        public void AddItemEx(string string_0, string string_1, bool bool_0)
        {
            BarItem item = null;
            item = this.popupMenu_0.Manager.Items[string_0];
            if (item != null)
            {
                if (item.Tag is ICommand)
                {
                    item.Enabled = (item.Tag as ICommand).Enabled;
                }
                item.Caption = string_1;
                if (bool_0)
                {
                    this.popupMenu_0.AddItem(item).BeginGroup = true;
                }
                else
                {
                    this.popupMenu_0.AddItem(item);
                }
            }
        }

        public void AddSubmenuItem(string string_0, string string_1, string string_2, bool bool_0)
        {
            BarSubItem item = null;
            if (string_2.Length > 0)
            {
                item = this.popupMenu_0.Manager.Items[string_2] as BarSubItem;
                if (item == null)
                {
                    item = new BarSubItem
                    {
                        Name = string_2
                    };
                    this.popupMenu_0.Manager.Items.Add(item);
                    if (bool_0)
                    {
                        this.popupMenu_0.AddItem(item).BeginGroup = true;
                    }
                    else
                    {
                        this.popupMenu_0.AddItem(item);
                    }
                }
            }
            BarSubItem item2 = null;
            item2 = this.popupMenu_0.Manager.Items[string_0] as BarSubItem;
            if (item2 == null)
            {
                item2 = new BarSubItem
                {
                    Name = string_0
                };
                if ((string_1 != null) && (string_1.Length > 0))
                {
                    item2.Caption = string_1;
                }
                else
                {
                    item2.Caption = string_0;
                }
                this.popupMenu_0.Manager.Items.Add(item2);
            }
            if (item != null)
            {
                item.ItemLinks.Add(item2).BeginGroup = bool_0;
            }
            else if (bool_0)
            {
                this.popupMenu_0.AddItem(item2).BeginGroup = true;
            }
            else
            {
                this.popupMenu_0.AddItem(item2);
            }
        }

        public void Clear()
        {
            this.popupMenu_0.ItemLinks.Clear();
        }

        public void ClearSubItem(string string_0)
        {
            BarSubItem item = null;
            item = this.popupMenu_0.Manager.Items[string_0] as BarSubItem;
            if (item != null)
            {
                item.ItemLinks.Clear();
            }
        }

        private LoadComponent method_0(string string_0)
        {
            LoadComponent component = this.hashtable_0[string_0] as LoadComponent;
            if (component == null)
            {
                component = new LoadComponent();
                component.LoadComponentLibrary(string_0);
                this.hashtable_1[string_0] = component;
            }
            return component;
        }

        private BarItem method_1(MenuItemDef menuItemDef_0)
        {
            ICommand command = null;
            BarManagerCategory category;
            if ((menuItemDef_0.Path != null) && (menuItemDef_0.ClassName != null))
            {
                try
                {
                    if (menuItemDef_0.Path[1] != ':')
                    {
                        menuItemDef_0.Path = Application.StartupPath + @"\" + menuItemDef_0.Path;
                    }
                    if (File.Exists(menuItemDef_0.Path))
                    {
                        command = this.method_0(menuItemDef_0.Path).LoadClass(menuItemDef_0.ClassName) as ICommand;
                        if (command != null)
                        {
                            // command.OnCreate(null);
                            if (menuItemDef_0.SubType != null)
                            {
                                try
                                {
                                    int subType = int.Parse(menuItemDef_0.SubType);
                                    (command as ICommandSubType).SetSubType(subType);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            if (command == null)
            {
                return null;
            }
            BarItem item = null;
            item = this.popupMenu_0.Manager.Items[menuItemDef_0.Name];
            if (item != null)
            {
                return item;
            }
            item = new BarButtonItem();
            this.popupMenu_0.Manager.Items.Add(item);
            item.Id = this.popupMenu_0.Manager.GetNewItemId();
            item.Name = menuItemDef_0.Name;
            item.Tag = command;
            item.Caption = command.Caption;
            if (menuItemDef_0.Caption != null)
            {
                item.Caption = menuItemDef_0.Caption;
            }
            item.Enabled = command.Enabled;
            if (command.Tooltip != null)
            {
                item.Hint = command.Tooltip;
            }
            if ((menuItemDef_0.BitmapPath == null) && (command.Bitmap != 0))
            {
                try
                {
                    IntPtr hbitmap = new IntPtr(command.Bitmap);
                    Bitmap bitmap = Image.FromHbitmap(hbitmap);
                    bitmap.MakeTransparent();
                    item.Glyph = bitmap;
                    goto Label_01E8;
                }
                catch
                {
                    goto Label_01E8;
                }
            }
            if (menuItemDef_0.BitmapPath != null)
            {
                item.Glyph = new Bitmap(menuItemDef_0.BitmapPath);
            }
            Label_01E8:
            if (command.Category != null)
            {
                if (command.Category.Length > 0)
                {
                    category = this.popupMenu_0.Manager.Categories[command.Category];
                    if (category == null)
                    {
                        category = new BarManagerCategory(command.Category, Guid.NewGuid());
                        this.popupMenu_0.Manager.Categories.Add(category);
                    }
                    item.Category = category;
                    return item;
                }
                category = this.popupMenu_0.Manager.Categories["其他"];
                if (category == null)
                {
                    category = new BarManagerCategory("其他", Guid.NewGuid());
                    this.popupMenu_0.Manager.Categories.Add(category);
                }
                item.Category = category;
                return item;
            }
            category = this.popupMenu_0.Manager.Categories["其他"];
            if (category == null)
            {
                category = new BarManagerCategory("其他", Guid.NewGuid());
                this.popupMenu_0.Manager.Categories.Add(category);
            }
            item.Category = category;
            return item;
        }

        private void method_2(BarSubItem barSubItem_0)
        {
            for (int i = 0; i < barSubItem_0.ItemLinks.Count; i++)
            {
                BarItem ownerItem = barSubItem_0.ItemLinks[i].Item;
                if (ownerItem == null)
                {
                    try
                    {
                        ownerItem = barSubItem_0.ItemLinks[i].OwnerItem;
                    }
                    catch
                    {
                    }
                }
                if (ownerItem != null)
                {
                    if (ownerItem is BarSubItem)
                    {
                        this.method_2(ownerItem as BarSubItem);
                    }
                    else if (ownerItem.Tag is ICommand)
                    {
                        ownerItem.Enabled = (ownerItem.Tag as ICommand).Enabled;
                    }
                }
            }
        }

        private void popupMenu_0_Popup(object sender, EventArgs e)
        {
        }

        public void Show(Control control_0, Point point_0)
        {
            this.popupMenu_0.ShowPopup(point_0);
        }

        public void UpdateUI()
        {
            for (int i = 0; i < this.popupMenu_0.ItemLinks.Count; i++)
            {
                BarItem ownerItem = this.popupMenu_0.ItemLinks[i].Item;
                if (ownerItem == null)
                {
                    ownerItem = this.popupMenu_0.ItemLinks[i].OwnerItem;
                }
                if (ownerItem != null)
                {
                    if (ownerItem is BarSubItem)
                    {
                        this.method_2(ownerItem as BarSubItem);
                    }
                    else if (ownerItem.Tag is ICommand)
                    {
                        ownerItem.Enabled = (ownerItem.Tag as ICommand).Enabled;
                    }
                }
            }
        }

        public object PopupMenu
        {
            set
            {
                this.popupMenu_0 = value as PopupMenu;
                this.popupMenu_0.Popup += new EventHandler(this.popupMenu_0_Popup);
            }
        }

        public bool Visible
        {
            get { return this.popupMenu_0.Visible; }
            set
            {
                if (!value)
                {
                    this.popupMenu_0.HidePopup();
                }
            }
        }
    }
}