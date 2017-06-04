using System.Windows.Forms;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonMenuItem
    {
        string ParentKey { get; set; }
        string Key { get; set; }
        IRibbonItem Item { get; set; }
        ToolStripItem ToolStripItem { get; set; }
        ToolStrip ToolStrip { get; set; }
    }
}