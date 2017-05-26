using System.Collections.Generic;
using System.Windows.Forms;

namespace Yutai.Plugins.Mvp
{
    public interface IMenuProvider
    {
        IEnumerable<ToolStripItemCollection> ToolStrips { get; }
        IEnumerable<Control> Buttons { get; }
    }
}