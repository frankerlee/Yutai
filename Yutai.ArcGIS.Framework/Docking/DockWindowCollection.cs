using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Yutai.ArcGIS.Framework.Docking
{
    public class DockWindowCollection : ReadOnlyCollection<DockWindow>
    {
        internal DockWindowCollection(DockPanel dockPanel) : base(new List<DockWindow>())
        {
            base.Items.Add(new DockWindow(dockPanel, DockState.Document));
            base.Items.Add(new DockWindow(dockPanel, DockState.DockLeft));
            base.Items.Add(new DockWindow(dockPanel, DockState.DockRight));
            base.Items.Add(new DockWindow(dockPanel, DockState.DockTop));
            base.Items.Add(new DockWindow(dockPanel, DockState.DockBottom));
        }

        public DockWindow this[DockState dockState]
        {
            get
            {
                if (dockState == DockState.Document)
                {
                    return base.Items[0];
                }
                if ((dockState == DockState.DockLeft) || (dockState == DockState.DockLeftAutoHide))
                {
                    return base.Items[1];
                }
                if ((dockState == DockState.DockRight) || (dockState == DockState.DockRightAutoHide))
                {
                    return base.Items[2];
                }
                if ((dockState == DockState.DockTop) || (dockState == DockState.DockTopAutoHide))
                {
                    return base.Items[3];
                }
                if ((dockState != DockState.DockBottom) && (dockState != DockState.DockBottomAutoHide))
                {
                    throw new ArgumentOutOfRangeException();
                }
                return base.Items[4];
            }
        }
    }
}

