using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.Framework;

namespace Yutai.ArcGIS.Framework
{
    public interface IBarManager
    {
        void AddItem(MenuItemDef menuItemDef_0);
        void ChangeHook(object object_0);
        void CreateBars(string string_0);
        ICommand FindCommand(string string_0);
        void Init();
        void Message(MSGTYPE msgtype_0, object object_0);
        void RegisterWindow(Control control_0, string string_0);
        void SetContextMenu(Control control_0);
        void SetPopupContextMenu(Control control_0);
        void SetPopupContextMenu(Control control_0, object object_0);
        bool UpdateUI(ITool itool_0);
        bool UpdateUI(string string_0, bool bool_0, bool bool_1);

        object Framework { get; set; }

        string PaintStyleName { get; }
    }
}

