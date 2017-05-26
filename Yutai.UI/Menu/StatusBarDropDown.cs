﻿using System;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.UI.Menu
{
    internal class StatusBarDropDown : StatusBarItem, IDropDownMenuItem
    {
        private readonly ToolStripItem _item;
        private readonly IMenuIndex _menuIndex;

        public StatusBarDropDown(ToolStripItem item, IMenuIndex menuIndex)
            : base(item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (menuIndex == null) throw new ArgumentNullException("menuIndex");

            _item = item;
            _menuIndex = menuIndex;
        }

        public IMenuItemCollection SubItems
        {
            get
            {
                var item2 = _item as ToolStripMenuItem;
                if (item2 != null)
                {
                    return new StatusItemCollection(item2.DropDownItems, _menuIndex, false);
                }

                var item = _item as ToolStripDropDownItem;
                if (item != null)
                {
                    return new StatusItemCollection(item.DropDownItems, _menuIndex, false);
                }

                throw new ApplicationException("Invalid menu item: parent menu item expected.");
            }
        }

        private ToolStripDropDownItem AsParent
        {
            get { return _item as ToolStripDropDownItem; }
        }

        public event EventHandler DropDownOpening
        {
            add
            {
                AsParent.DropDownOpening += (s, e) => value.Invoke(this, e);
            }
            remove
            {
                AsParent.DropDownOpening -= value;
            }
        }

        public event EventHandler DropDownClosed
        {
            add
            {
                AsParent.DropDownClosed += (s, e) => value.Invoke(this, e);
            }
            remove
            {
                AsParent.DropDownClosed -= value;
            }
        }

        public void Update()
        {
            var items = AsParent.DropDownItems;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i] is ToolStripSeparator)
                {
                    items.RemoveAt(i);
                }
            }

            for (int i = SubItems.Count - 1; i >= 0; i--)
            {
                var item = SubItems[i];
                if (item.BeginGroup)
                {
                    var sep = new ToolStripSeparator
                    {
                        Tag = new MenuItemMetadata(item.PluginIdentity, Guid.NewGuid().ToString())
                    };
                    items.Insert(i, sep);
                }
            }
        }

        protected override void DetachItemListeners()
        {
            base.DetachItemListeners();

            EventHelper.RemoveEventHandler(_item, "Popup");
            EventHelper.RemoveEventHandler(_item, "DropDownItemClicked");
        }

        public override event EventHandler<MenuItemEventArgs> ItemClicked
        {
            add
            {
                var button = _item as StatusStripSplitButton;
                if (button != null)
                {
                    button.ButtonClick += (s, e) =>
                    {
                        value(this, new MenuItemEventArgs(Key, true));
                    };
                }
                else
                {
                    _item.Click += (sender, args) => value.Invoke(this, new MenuItemEventArgs(Key, true));
                }
            }
            remove
            {

            }
        }
    }
}