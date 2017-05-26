using System.Windows.Forms;

namespace Yutai.UI.Style
{
    public interface IStyleService
    {
        void ApplyStyle(System.Windows.Forms.Form form);
        void ApplyStyle(Control control);
    }
}