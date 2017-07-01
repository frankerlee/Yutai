﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.Events
{
    public class MenuItemEventArgs : EventArgs
    {
        private string _itemKey;

        public MenuItemEventArgs(string itemKey, bool statusBar = false)
        {
            _itemKey = itemKey ?? string.Empty;
            StatusBar = statusBar;
        }

        public string ItemKey
        {
            get { return _itemKey; }
        }

        public bool StatusBar { get; private set; }
    }
}