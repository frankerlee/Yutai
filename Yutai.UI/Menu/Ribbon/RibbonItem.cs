using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Ribbon
{
    public class RibbonItem:IRibbonItem
    {
        private string _name;
        private string _key;
        private string _caption;
        private Bitmap _image;
        private string _tooltip;
        private string _category;
        private RibbonItemType _itemType;
        private bool _checked;
        private bool _enabled;
        private PluginIdentity _pluginIdentity;

        public RibbonItem(YutaiCommand command)
        {
            IRibbonItem source = (IRibbonItem) command;
            this._name = source.Name;
            this._key = source.Key;
            this._caption = source.Caption;
            this._image = source.Image;
            this._tooltip = source.Tooltip;
            this._category = source.Category;
            this._checked = source.Checked;
            this._enabled = source.Enabled;
            this._itemType = source.ItemType;
            _pluginIdentity = source.PluginIdentity;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Key
        {
            get { return _key; }
        }

        public string Caption
        {
            get { return _caption; }
        }

        public Bitmap Image
        {
            get { return _image; }
        }

        public string Tooltip
        {
            get { return _tooltip; }
        }

        public string Category
        {
            get { return _category; }
        }

        public RibbonItemType ItemType
        {
            get { return _itemType; }
        }

        public bool Checked
        {
            get { return _checked; }
        }

        public bool Enabled
        {
            get { return _enabled; }
        }

        public PluginIdentity PluginIdentity
        {
            get { return _pluginIdentity; }
        }
    }
}
