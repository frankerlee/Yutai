using System.Windows.Forms;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{

    public interface IRibbonEditItem
    {
        object RibbonEditItem { set; }
        RibbonEditStyle Style { get; }

        int Width { get; }
    }

    
    public interface IRibbonMenuItem
    {
        string ParentKey { get; set; }
        string Key { get; set; }
        IRibbonItem Item { get; set; }
        ToolStripItem ToolStripItem { get; set; }
        object RibbonObject { get; set; }
    }
}