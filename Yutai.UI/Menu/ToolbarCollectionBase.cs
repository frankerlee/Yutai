﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu
{
    internal class ToolbarCollectionBase : IToolbarCollectionBase
    {
        protected readonly IMenuIndex _menuIndex;
        private readonly MainFrameBarManager _manager;
        internal const string FILE_TOOLBAR_KEY = "FileToolbar";
        internal const string MAP_TOOLBAR_KEY = "MapToolbar";

        internal ToolbarCollectionBase(object menuManager, IMenuIndex menuIndex)
        {
            _menuIndex = menuIndex;
            _manager = menuManager as MainFrameBarManager;
            if (menuIndex == null) throw new ArgumentNullException("menuIndex");
            if (_manager == null)
            {
                throw new NullReferenceException("ToolbarCollectionBase: instance of menu manager is not provided.");
            }

#if STYLE2010
            _manager.Style = VisualStyle.Office2010;
#else
            _manager.Style = VisualStyle.OfficeXP;
#endif
        }

        public IEnumerator<IToolbar> GetEnumerator()
        {
            for (int i = 0; i < _manager.Bars.Count; i++)
            {
                yield return this[i];
            }
        }

        public IToolbar this[int index]
        {
            get
            {
                if (index < 0 || index >= _manager.Bars.Count)
                {
                    throw new IndexOutOfRangeException("Invalid toolbar index.");
                }
                return new Toolbar(_manager, _manager.Bars[index], _menuIndex);
            }
        }

        public IMenuItem FindItem(string key, PluginIdentity identity)
        {
            return _menuIndex.GetItem(identity.GetUniqueKey(key));
        }

        public void RemoveItemsForPlugin(PluginIdentity identity)
        {
            _menuIndex.RemoveItemsForPlugin(identity);

            for (int i = this.Count() - 1; i >= 0; i--)
            {
                var bar = this[i];
                if (bar.PluginIdentity == identity)
                {
                    Remove(i);
                }
                else
                {
                    ItemCollectionBase.RemoveItems(bar.Items, identity);
                }
            }
        }

        public IEnumerable<IMenuItem> ItemsForPlugin(PluginIdentity identity)
        {
            return _menuIndex.ItemsForPlugin(identity);
        }



        public IToolbar Add(string name, PluginIdentity identity)
        {
            return Add(name, string.Empty, identity);
        }

        public IToolbar Add(string name, string key, PluginIdentity identity)
        {
            var bar = new Bar(_manager, name);
            int index = _manager.Bars.Add(bar);
            var cbr = _manager.GetBarControl(bar);
            cbr.Tag = new MenuItemMetadata(identity, key);
            return this[index];
        }

        public void Remove(int toolbarIndex)
        {
            if (toolbarIndex < 0 || toolbarIndex >= _manager.Bars.Count)
            {
                throw new IndexOutOfRangeException("Invalid toolbar index.");
            }

            var bar = _manager.Bars[toolbarIndex];
            if (bar.BarStyle == BarStyle.IsMainMenu)
            {
                throw new Exception("Main menu can't be removed.");
            }

            var toolbar = this[toolbarIndex];
            toolbar.Items.Clear();    // make sure to clear the index

            _manager.Bars.RemoveAt(toolbarIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}