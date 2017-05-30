using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Catalog
{
    public class MenuItemDef
    {
        public string BeginGroup
        {
            get;
            set;
        }

        public string BitmapPath
        {
            get;
            set;
        }

        public string Caption
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        public bool HasSubMenu
        {
            get;
            set;
        }

        public string MainMenuItem
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string SubType
        {
            get;
            set;
        }

        public MenuItemDef()
        {
        }
    }
    public interface IPopuMenuWrap
    {
        bool Visible
        {
            get;
            set;
        }

        void AddItem(string string_0, bool bool_0);

        void AddItem(MenuItemDef menuItemDef_0);

        void AddItem(string string_0, string string_1, bool bool_0);

        void AddItemEx(string string_0, string string_1, bool bool_0);

        void AddSubmenuItem(string string_0, string string_1, string string_2, bool bool_0);

        void Clear();

        void ClearSubItem(string string_0);

        void Show(Control control_0, Point point_0);

        void UpdateUI();

        event OnItemClickEventHandler OnItemClickEvent;
    }
    public delegate void OnItemClickEventHandler(object object_0);
}