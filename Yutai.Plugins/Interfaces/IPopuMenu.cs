using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Events;

namespace Yutai.Plugins.Interfaces
{
    public interface IPopuMenu
    {
        bool Visible { get; set; }

        void AddItem(string string_0, bool bool_0);

        void AddItem(string string_0, string string_1, bool bool_0);

        void AddItemEx(string string_0, string string_1, bool bool_0);

        void AddSubmenuItem(string string_0, string string_1, string string_2, bool bool_0);

        void Clear();

        void ClearSubItem(string string_0);

        void Show(Control control_0, Point point_0);

        void UpdateUI();

        event OnItemClickEventHandler OnItemClickEvent;
    }
}