using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Ribbon
{
   

    internal class RibbonMenuItem : IRibbonMenuItem
    {
        private string _key;
        private IRibbonItem _item;
        private ToolStripItem _stripItem;
        private ToolStrip _strip;
        private string _parentKey;

        public RibbonMenuItem(string key, IRibbonItem item, ToolStripItem stripItem)
        {
            _key = key;
            _item = item;
            _stripItem = stripItem;
        }

        public RibbonMenuItem(string key, IRibbonItem item, ToolStrip stripItem)
        {
            _key = key;
            _item = item;
            _strip = stripItem;
        }

        public string ParentKey
        {
            get { return _parentKey; }
            set { _parentKey = value; }
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public IRibbonItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public ToolStripItem ToolStripItem
        {
            get { return _stripItem; }
            set { _stripItem = value; }
        }

        public ToolStrip ToolStrip
        {
            get { return _strip; }
            set { _strip = value; }
        }


    }
}
