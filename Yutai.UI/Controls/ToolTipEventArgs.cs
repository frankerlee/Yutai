using System.ComponentModel;
using Syncfusion.Windows.Forms.Tools;

namespace Yutai.UI.Controls
{
    public class ToolTipEventArgs : CancelEventArgs
    {
        public ToolTipEventArgs(ToolTipInfo info)
        {
            ToolTip = info;
        }

        public ToolTipInfo ToolTip { get; private set; }
    }
}