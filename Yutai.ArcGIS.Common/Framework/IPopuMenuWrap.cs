using System.Drawing;

namespace Yutai.ArcGIS.Common.Framework
{
	public interface IPopuMenuWrap
	{
		event OnItemClickEventHandler OnItemClickEvent;

		bool Visible
		{
			get;
			set;
		}

		void AddItem(string string_0, bool bool_0);

		void AddItem(MenuItemDef menuItemDef_0);

		void AddItemEx(string string_0, string string_1, bool bool_0);

		void AddItem(string string_0, string string_1, bool bool_0);

		void AddSubmenuItem(string string_0, string string_1, string string_2, bool bool_0);

		void ClearSubItem(string string_0);

		void Clear();

		void UpdateUI();

		void Show(System.Windows.Forms.Control control_0, Point point_0);
	}
}
