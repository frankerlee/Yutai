using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Enums;

namespace Yutai.UI.Docking
{
    internal static class DockHelper
    {
        internal static DockPanelState DevExpressToMapWindow(DevExpress.XtraBars.Docking.DockingStyle style)
        {
            switch (style)
            {
                case DevExpress.XtraBars.Docking.DockingStyle.Float:
                    return DockPanelState.None;
                    break;
                case DevExpress.XtraBars.Docking.DockingStyle.Top:
                    return DockPanelState.Top;
                    break;
                case DevExpress.XtraBars.Docking.DockingStyle.Bottom:
                    return DockPanelState.Bottom;
                    break;
                case DevExpress.XtraBars.Docking.DockingStyle.Left:
                    return DockPanelState.Left;
                    break;
                case DevExpress.XtraBars.Docking.DockingStyle.Right:
                    return DockPanelState.Right;
                    break;
                case DevExpress.XtraBars.Docking.DockingStyle.Fill:
                    return DockPanelState.Fill;
                    break;
                default:
                    return DockPanelState.None;
            }
        }
        internal static DockPanelState SyncfusionToMapWindow(DockingStyle style)
        {
            switch (style)
            {
                case DockingStyle.Top:
                    return DockPanelState.Top;
                case DockingStyle.Bottom:
                    return DockPanelState.Bottom;
                case DockingStyle.Left:
                    return DockPanelState.Left;
                case DockingStyle.Right:
                    return DockPanelState.Right;
                case DockingStyle.Tabbed:
                    return DockPanelState.Tabbed;
                case DockingStyle.Fill:
                    return DockPanelState.Fill;
                case DockingStyle.None:
                default:
                    return DockPanelState.None;
            }
        }

        internal static DevExpress.XtraBars.Docking.DockingStyle  MapWindowToDevExpress(DockPanelState state)
        {
            switch (state)
            {
                case DockPanelState.Left:
                    return DevExpress.XtraBars.Docking.DockingStyle.Left;
                case DockPanelState.Right:
                    return DevExpress.XtraBars.Docking.DockingStyle.Right;
                case DockPanelState.Top:
                    return DevExpress.XtraBars.Docking.DockingStyle.Top;
                case DockPanelState.Bottom:
                    return DevExpress.XtraBars.Docking.DockingStyle.Bottom;
                case DockPanelState.Tabbed:
                    return DevExpress.XtraBars.Docking.DockingStyle.Fill;
                case DockPanelState.None:
                case DockPanelState.Fill:
                    return DevExpress.XtraBars.Docking.DockingStyle.Fill;
                default:
                    return DevExpress.XtraBars.Docking.DockingStyle.Float;
            }
        }

        internal static DockingStyle MapWindowToSyncfusion(DockPanelState state)
        {
            switch (state)
            {
                case DockPanelState.Left:
                    return DockingStyle.Left;
                case DockPanelState.Right:
                    return DockingStyle.Right;
                case DockPanelState.Top:
                    return DockingStyle.Top;
                case DockPanelState.Bottom:
                    return DockingStyle.Bottom;
                case DockPanelState.Tabbed:
                    return DockingStyle.Tabbed;
                case DockPanelState.None:
                case DockPanelState.Fill:
                    return DockingStyle.Fill;
                default:
                    return DockingStyle.None;
            }
        }
    }
}
