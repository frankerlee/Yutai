using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{
    public interface IDockPanel
    {
        string Name { get; }
        void Activate();
        Control Control { get; }
        DockPanelState DockState { get; }
        bool Visible { get; set; }

        void DockTo(string parentName, DockPanelState state, int size);
        void DockTo(IDockPanel parent, DockPanelState state, int size);
        void DockTo(DockPanelState state, int size);
        string Caption { get; set; }
        Size Size { get; set; }
        bool FloatOnly { get; set; }
        bool AllowFloating { get; set; }
        void SetIcon(Icon icon);
        Image GetIcon();
        int TabPosition { get; set; }
        bool IsFloating { get; }
        void Float(Rectangle rect, bool tabFloating);
        bool AutoHidden { get; set; }
        void Focus();
    }
}