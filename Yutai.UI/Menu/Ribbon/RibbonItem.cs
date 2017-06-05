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
        private TextImageRelationYT _textImageRelationYt;
        private DisplayStyleYT _displayStyleYt;
        private ToolStripItemImageScalingYT _toolStripItemImageScalingYt;
        private ToolStripLayoutStyleYT _toolStripLayoutStyleYt;
        private int _panelRowCount;
        private int _position;
        private string _parentName;
        private bool _isGroup;
        private bool _needUpdateEvent;

        public RibbonItem(IRibbonItem source,string newName)
        {
            this._name = newName;
            this._key = source.Key;
            this._caption = source.Caption;
            this._image = source.Image;
            this._tooltip = source.Tooltip;
            this._category = source.Category;
            //this._checked = source.Checked;
            //this._enabled = source.Enabled;
            this._itemType = source.ItemType;
            _pluginIdentity = source.PluginIdentity;
            _parentName = source.ParentName;
            _position = source.Position;
        }
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

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; }
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
            set { _itemType = value; }
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

        public TextImageRelationYT TextImageRelationYT
        {
            get { return _textImageRelationYt; }
            set { _textImageRelationYt = value; }
        }

        public DisplayStyleYT DisplayStyleYT
        {
            get { return _displayStyleYt; }
            set { _displayStyleYt = value; }
        }

        public ToolStripItemImageScalingYT ToolStripItemImageScalingYT
        {
            get { return _toolStripItemImageScalingYt; }
            set { _toolStripItemImageScalingYt = value; }
        }

        public ToolStripLayoutStyleYT ToolStripLayoutStyleYT
        {
            get { return _toolStripLayoutStyleYt; }
            set { _toolStripLayoutStyleYt = value; }
        }

        public int PanelRowCount
        {
            get { return _panelRowCount; }
            set { _panelRowCount = value; }
        }

        public bool IsGroup
        {
            get { return _isGroup; }
            set { _isGroup = value; }
        }

        public bool NeedUpdateEvent
        {
            get { return _needUpdateEvent; }
            set { _needUpdateEvent = value; }
        }
    }
}
