using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Events
{
    public class DockPanelCancelEventArgs : DockPanelEventArgs
    {
        public DockPanelCancelEventArgs(IDockPanel panel, string key)
            : base(panel, key)
        {
        }

        public bool Cancel { get; set; }
    }
}