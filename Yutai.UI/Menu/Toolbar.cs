﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu
{
    internal class Toolbar : IToolbar
    {
        private MainFrameBarManager _manager;
        private readonly CommandBar _commandBar;
        private readonly Bar _bar;
        private readonly IMenuIndex _menuIndex;

        internal Toolbar(MainFrameBarManager manager, Bar bar, IMenuIndex menuIndex)
        {
            _manager = manager;
            _bar = bar;
            _menuIndex = menuIndex;
            _commandBar = _manager.GetBarControl(_bar);

            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            if (menuIndex == null)
            {
                throw new ArgumentNullException("menuIndex");
            }
            if (bar == null || _commandBar == null)
            {
                throw new NullReferenceException("Internal toolbar reference is null.");
            }
        }

        public string Name
        {
            get { return _bar.BarName; }
            set { _bar.BarName = value; }
        }

        public IMenuItemCollection Items
        {
            get { return new MenuItemCollection(_bar.Items, _menuIndex); }
        }

        public bool Visible
        {
            get { return _commandBar.Visible; }
            set { _commandBar.Visible = value; }
        }

        public object Tag
        {
            get { return Metadata.Tag; }
            set { Metadata.Tag = value; }
        }

        public string Key
        {
            get { return Metadata.Key; }
        }

        public ToolbarDockState DockState
        {
            get { return (ToolbarDockState)_manager.GetBarControl(_bar).DockState; }
            set { _manager.GetBarControl(_bar).DockState = (CommandBarDockState)value; }
        }

        public void Update()
        {
            UpdateSeparators();
        }

        public void Refresh()
        {
            _commandBar.Refresh();
        }

        private void UpdateSeparators()
        {
            _bar.SeparatorIndices.Clear();
            for (int i = 0; i < Items.Count(); i++)
            {
                var item = Items[i];
                if (item.BeginGroup)
                {
                    _bar.SeparatorIndices.Add(i);
                }
            }

            // do the same for dropdowns
            foreach (var menu in Items.OfType<IDropDownMenuItem>())
            {
                menu.Update();
            }
        }

        private MenuItemMetadata Metadata
        {
            get
            {
                var metadata = _commandBar.Tag as MenuItemMetadata;
                if (metadata == null)
                {
                    throw new ApplicationException("Tag must have an instance of MenuItemMetadata class.");
                }
                return metadata;
            }
        }

        public PluginIdentity PluginIdentity
        {
            get { return Metadata.PluginIdentity; }
        }

        public bool Enabled
        {
            get
            {
                return Items.Any(item => item.Enabled);
            }
            set
            {
                foreach (var item in Items)
                {
                    item.Enabled = value;
                }
            }
        }
    }
}
