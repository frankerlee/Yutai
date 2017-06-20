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

		void AddItem(string cmdName, bool isGroup);

		void AddItem(MenuItemDef menuItemDef);

		void AddItemEx(string cmdName, string groupCmdName, bool isGroup);

		void AddItem(string cmdName, string parentName, bool isGroup);

		void AddSubmenuItem(string cmdName, string caption, string parentName, bool isGroup);

		void ClearSubItem(string cmdName);

		void Clear();

		void UpdateUI();

		void Show(System.Windows.Forms.Control control, Point point);
	}
}
