using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Events
{
    public class DockPanelCancelEventArgs : DockPanelEventArgs
    {
        public DockPanelCancelEventArgs(DockPanel panel, string key)
            : base(panel, key)
        {
        }

        public bool Cancel { get; set; }
    }
}