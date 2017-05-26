using System;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Classic
{
    internal class MenuStripItemCollection : ItemCollectionBase
    {
        public MenuStripItemCollection(ToolStripItemCollection items, IMenuIndex menuIndex)
            : base(items, menuIndex)
        {
        }

        private ToolStripItemCollection ToolStripItems
        {
            get { return _items as ToolStripItemCollection; }
        }

        public override IComboBoxMenuItem AddComboBox(string text, string key, PluginIdentity identity)
        {
            throw new NotSupportedException("Combo boxes are not supported in the main menu.");
        }

        public override IMenuItem this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Count)
                {
                    return null;
                }

                var item = ToolStripItems[index] as ToolStripMenuItem;
                if (item == null)
                {
                    return new MenuStripSeparator(ToolStripItems[index]);
                }

                var meta = item.Tag as MenuItemMetadata;
                if (meta != null)
                {
                    if (meta.DropDown)
                    {
                        return new MenuStripDropDownItem(item, MenuIndex);
                    }

                    return new MenuStripItem(item);
                }

                return new MenuStripSeparator(item);
            }
        }

       

        protected override IDropDownMenuItem AddDropDown(string text, string key, Bitmap icon, PluginIdentity identity, bool isFirst = false)
        {
            return AddCore(text, key, icon, identity, true) as IDropDownMenuItem;
        }

        public override IMenuItem AddLabel(string text, string key, PluginIdentity identity)
        {
            return AddCore(text, key, null, identity, false);
        }

        public override IMenuItem AddButton(string text, string key, Bitmap icon, PluginIdentity identity)
        {
            return AddCore(text, key, icon, identity, false);
        }

        private IMenuItem AddCore(string text, string key, Bitmap icon, PluginIdentity identity, bool dropDown)
        {
            var item = new ToolStripMenuItem
            {
                Text = text,
                Image = icon,
                Tag = new MenuItemMetadata(identity, key, dropDown)
            };

            return AddItemCore(item, key, false, false);
        }
    }
}