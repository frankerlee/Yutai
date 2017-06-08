using System;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Events
{
    public class DockPanelEventArgs : EventArgs
    {
        public DockPanelEventArgs(DockPanel panel, string key)
        {
            if (panel == null) throw new ArgumentNullException("panel");
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");

            Panel = panel;
            Key = key;
        }

        public DockPanel Panel { get; private set; }
        public string Key { get; private set; }
    }
}