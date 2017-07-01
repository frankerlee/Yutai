using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Ribbon
{
    public class RibbonPopupMenu : IPopuMenu
    {
        private OnItemClickEventHandler onItemClickEventHandler_0;

        private PopupMenu pPopupMenu;

        private Hashtable _table1 = new Hashtable();

        private Hashtable _table2 = new Hashtable();

        public object PopupMenu
        {
            set
            {
                this.pPopupMenu = value as PopupMenu;
                this.pPopupMenu.Popup += new EventHandler(this.pPopupMenu_Popup);
            }
        }

        public bool Visible
        {
            get { return this.pPopupMenu.Visible; }
            set
            {
                if (!value)
                {
                    this.pPopupMenu.HidePopup();
                }
            }
        }

        public RibbonPopupMenu()
        {
        }

        public void AddItem(string key, bool isGroup)
        {
            BarItem item = this.FindInPop(key);
            if (item == null)
            {
                item = this.pPopupMenu.Ribbon.Items[key];
                if (item != null)
                {
                    if (item.Tag is YutaiCommand)
                    {
                        item.Enabled = (item.Tag as YutaiCommand).Enabled;
                        item.Caption = (item.Tag as YutaiCommand).Caption;
                    }
                    if (!isGroup)
                    {
                        this.pPopupMenu.AddItem(item);
                    }
                    else
                    {
                        this.pPopupMenu.AddItem(item).BeginGroup = true;
                    }
                }
            }
        }

        public void AddItem(string parent, string child, bool bool_0)
        {
            BarItem item = this.FindInPop(parent);
            if (item == null)
            {
                BarSubItem barSubItem = null;
                if (child.Length > 0)
                {
                    barSubItem = this.FindSubInPop(child, "");
                    if (barSubItem == null)
                    {
                        barSubItem = new BarSubItem()
                        {
                            Name = child
                        };
                        if (!bool_0)
                        {
                            this.pPopupMenu.AddItem(barSubItem);
                        }
                        else
                        {
                            this.pPopupMenu.AddItem(barSubItem).BeginGroup = true;
                        }
                    }
                }
                item = this.pPopupMenu.Ribbon.Items[parent];
                if (item != null)
                {
                    if (item.Tag is YutaiCommand)
                    {
                        item.Enabled = (item.Tag as YutaiCommand).Enabled;
                        item.Caption = (item.Tag as YutaiCommand).Caption;
                    }
                    if (barSubItem != null)
                    {
                        barSubItem.ItemLinks.Add(item).BeginGroup = bool_0;
                    }
                    else if (!bool_0)
                    {
                        this.pPopupMenu.AddItem(item);
                    }
                    else
                    {
                        this.pPopupMenu.AddItem(item).BeginGroup = true;
                    }
                }
            }
        }

        public void AddItemEx(string string_0, string string_1, bool bool_0)
        {
            BarItem item = null;
            item = this.pPopupMenu.Ribbon.Items[string_0];
            if (item != null)
            {
                if (item.Tag is YutaiCommand)
                {
                    item.Enabled = (item.Tag as YutaiCommand).Enabled;
                }
                item.Caption = string_1;
                if (!bool_0)
                {
                    this.pPopupMenu.AddItem(item);
                }
                else
                {
                    this.pPopupMenu.AddItem(item).BeginGroup = true;
                }
            }
        }

        public void AddSubmenuItem(string string_0, string string_1, string string_2, bool bool_0)
        {
            BarSubItem barSubItem = this.FindSubInPop(string_0, string_1);
            if (barSubItem == null)
            {
                BarSubItem barSubItem1 = null;
                if (string_2.Length > 0)
                {
                    barSubItem1 = this.FindSubInPop(string_2, "");
                    if (barSubItem1 == null)
                    {
                        barSubItem1 = new BarSubItem()
                        {
                            Name = string_2
                        };
                        if (!bool_0)
                        {
                            this.pPopupMenu.AddItem(barSubItem1);
                        }
                        else
                        {
                            this.pPopupMenu.AddItem(barSubItem1).BeginGroup = true;
                        }
                    }
                }
                barSubItem = this.FindSubInPop(string_0, string_1);
                if (barSubItem == null)
                {
                    barSubItem = new BarSubItem()
                    {
                        Name = string_0
                    };
                    if ((string_1 == null ? true : string_1.Length <= 0))
                    {
                        barSubItem.Caption = string_0;
                    }
                    else
                    {
                        barSubItem.Caption = string_1;
                    }
                }
                if (barSubItem1 != null)
                {
                    barSubItem1.ItemLinks.Add(barSubItem).BeginGroup = bool_0;
                }
                else if (!bool_0)
                {
                    this.pPopupMenu.AddItem(barSubItem);
                }
                else
                {
                    this.pPopupMenu.AddItem(barSubItem).BeginGroup = true;
                }
            }
        }

        public void Clear()
        {
            this.pPopupMenu.ItemLinks.Clear();
        }

        public void ClearSubItem(string string_0)
        {
            BarSubItem barSubItem = null;
            barSubItem = this.FindSubInPop(string_0, "");
            if (barSubItem != null)
            {
                barSubItem.ItemLinks.Clear();
            }
        }


        public BarItem FindInPop(string string_0, BarSubItem barSubItem_0)
        {
            BarItem barItem;
            int num = 0;
            while (true)
            {
                if (num < barSubItem_0.ItemLinks.Count)
                {
                    BarItem item = barSubItem_0.ItemLinks[num].Item;
                    if (item is BarSubItem)
                    {
                        BarItem barItem1 = this.FindInPop(string_0, item as BarSubItem);
                        if (barItem1 != null)
                        {
                            barItem = barItem1;
                            break;
                        }
                    }
                    else if (item.Name == string_0)
                    {
                        barItem = item;
                        break;
                    }
                    num++;
                }
                else
                {
                    barItem = null;
                    break;
                }
            }
            return barItem;
        }

        public BarItem FindInPop(string string_0)
        {
            BarItem barItem;
            int num = 0;
            while (true)
            {
                if (num < this.pPopupMenu.ItemLinks.Count)
                {
                    BarItem item = this.pPopupMenu.ItemLinks[num].Item;
                    if (item is BarSubItem)
                    {
                        BarItem barItem1 = this.FindInPop(string_0, item as BarSubItem);
                        if (barItem1 != null)
                        {
                            barItem = barItem1;
                            break;
                        }
                    }
                    else if (item.Name == string_0)
                    {
                        barItem = item;
                        break;
                    }
                    num++;
                }
                else
                {
                    barItem = null;
                    break;
                }
            }
            return barItem;
        }

        public BarSubItem FindSubInPop(string string_0, string string_1, BarSubItem barSubItem_0)
        {
            BarSubItem barSubItem;
            int num = 0;
            while (true)
            {
                if (num < barSubItem_0.ItemLinks.Count)
                {
                    BarItem item = barSubItem_0.ItemLinks[num].Item;
                    if (item is BarSubItem)
                    {
                        if (item.Name == string_0)
                        {
                            barSubItem = item as BarSubItem;
                            break;
                        }
                        else
                        {
                            BarSubItem barSubItem1 = this.FindSubInPop(string_0, string_1, item as BarSubItem);
                            if (barSubItem1 != null)
                            {
                                barSubItem = barSubItem1;
                                break;
                            }
                        }
                    }
                    num++;
                }
                else
                {
                    barSubItem = null;
                    break;
                }
            }
            return barSubItem;
        }

        public BarSubItem FindSubInPop(string string_0, string string_1)
        {
            BarSubItem barSubItem;
            int num = 0;
            while (true)
            {
                if (num < this.pPopupMenu.ItemLinks.Count)
                {
                    BarItem item = this.pPopupMenu.ItemLinks[num].Item;
                    if (item is BarSubItem)
                    {
                        if (item.Name == string_0)
                        {
                            barSubItem = item as BarSubItem;
                            break;
                        }
                        else
                        {
                            BarSubItem barSubItem1 = this.FindSubInPop(string_0, string_1, item as BarSubItem);
                            if (barSubItem1 != null)
                            {
                                barSubItem = barSubItem1;
                                break;
                            }
                        }
                    }
                    num++;
                }
                else
                {
                    barSubItem = null;
                    break;
                }
            }
            return barSubItem;
        }

        private LoadComponent method_0(string string_0)
        {
            LoadComponent item = this._table1[string_0] as LoadComponent;
            if (item == null)
            {
                item = new LoadComponent();
                item.LoadComponentLibrary(string_0);
                this._table2[string_0] = item;
            }
            return item;
        }


        private void method_2(BarSubItem barSubItem_0)
        {
            for (int i = 0; i < barSubItem_0.ItemLinks.Count; i++)
            {
                BarItem item = barSubItem_0.ItemLinks[i].Item;
                if (item == null)
                {
                    try
                    {
                        item = barSubItem_0.ItemLinks[i].OwnerItem;
                    }
                    catch
                    {
                    }
                }
                if (item != null)
                {
                    if (item is BarSubItem)
                    {
                        this.method_2(item as BarSubItem);
                    }
                    else if (item.Tag is YutaiCommand)
                    {
                        item.Enabled = (item.Tag as YutaiCommand).Enabled;
                    }
                }
            }
        }

        private void pPopupMenu_Popup(object sender, EventArgs e)
        {
        }

        public void Show(Control control_0, Point point_0)
        {
            point_0 = control_0.PointToScreen(point_0);
            this.pPopupMenu.ShowPopup(point_0);
        }

        public void UpdateUI()
        {
            for (int i = 0; i < this.pPopupMenu.ItemLinks.Count; i++)
            {
                BarItem item = this.pPopupMenu.ItemLinks[i].Item;
                if (item == null)
                {
                    item = this.pPopupMenu.ItemLinks[i].OwnerItem;
                }
                if (item != null)
                {
                    if (item is BarSubItem)
                    {
                        this.method_2(item as BarSubItem);
                    }
                    else if (item.Tag is YutaiCommand)
                    {
                        item.Enabled = (item.Tag as YutaiCommand).Enabled;
                    }
                }
            }
        }

        public event OnItemClickEventHandler OnItemClickEvent
        {
            add
            {
                OnItemClickEventHandler onItemClickEventHandler;
                OnItemClickEventHandler onItemClickEventHandler0 = this.onItemClickEventHandler_0;
                do
                {
                    onItemClickEventHandler = onItemClickEventHandler0;
                    OnItemClickEventHandler onItemClickEventHandler1 =
                        (OnItemClickEventHandler) Delegate.Combine(onItemClickEventHandler, value);
                    onItemClickEventHandler0 =
                        Interlocked.CompareExchange<OnItemClickEventHandler>(ref this.onItemClickEventHandler_0,
                            onItemClickEventHandler1, onItemClickEventHandler);
                } while ((object) onItemClickEventHandler0 != (object) onItemClickEventHandler);
            }
            remove
            {
                OnItemClickEventHandler onItemClickEventHandler;
                OnItemClickEventHandler onItemClickEventHandler0 = this.onItemClickEventHandler_0;
                do
                {
                    onItemClickEventHandler = onItemClickEventHandler0;
                    OnItemClickEventHandler onItemClickEventHandler1 =
                        (OnItemClickEventHandler) Delegate.Remove(onItemClickEventHandler, value);
                    onItemClickEventHandler0 =
                        Interlocked.CompareExchange<OnItemClickEventHandler>(ref this.onItemClickEventHandler_0,
                            onItemClickEventHandler1, onItemClickEventHandler);
                } while ((object) onItemClickEventHandler0 != (object) onItemClickEventHandler);
            }
        }
    }
}