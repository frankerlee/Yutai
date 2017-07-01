using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    internal sealed class DockExtender
    {
        private Control control_0;
        private Floaties floaties_0;
        internal Overlay Overlay = new Overlay();

        public DockExtender(Control control_1)
        {
            this.control_0 = control_1;
            this.floaties_0 = new Floaties();
        }

        public IFloaty Attach(ScrollableControl scrollableControl_0)
        {
            return this.Attach(scrollableControl_0, scrollableControl_0, null);
        }

        public IFloaty Attach(ScrollableControl scrollableControl_0, Control control_1)
        {
            return this.Attach(scrollableControl_0, control_1, null);
        }

        public IFloaty Attach(ScrollableControl scrollableControl_0, Control control_1, Splitter splitter_0)
        {
            if (scrollableControl_0 == null)
            {
                throw new ArgumentException("container cannot be null");
            }
            if (control_1 == null)
            {
                throw new ArgumentException("handle cannot be null");
            }
            DockState state = new DockState
            {
                Container = scrollableControl_0,
                Handle = control_1,
                OrgDockHost = this.control_0,
                Splitter = splitter_0
            };
            Floaty item = new Floaty(this);
            item.Attach(state);
            this.floaties_0.Add(item);
            return item;
        }

        internal Control FindDockHost(Floaty floaty_0, Point point_0)
        {
            Control orgDockHost = null;
            if (this.FormIsHit(floaty_0.DockState.OrgDockHost, point_0))
            {
                orgDockHost = floaty_0.DockState.OrgDockHost;
            }
            if (!floaty_0.DockOnHostOnly)
            {
                using (List<IFloaty>.Enumerator enumerator = this.Floaties.GetEnumerator())
                {
                    Floaty current;
                    while (enumerator.MoveNext())
                    {
                        current = (Floaty) enumerator.Current;
                        if (current.DockState.Container.Visible && this.FormIsHit(current.DockState.Container, point_0))
                        {
                            goto Label_0087;
                        }
                    }
                    return orgDockHost;
                    Label_0087:
                    orgDockHost = current.DockState.Container;
                }
            }
            return orgDockHost;
        }

        internal bool FormIsHit(Control control_1, Point point_0)
        {
            if (control_1 == null)
            {
                return false;
            }
            Point location = control_1.PointToClient(point_0);
            return control_1.ClientRectangle.IntersectsWith(new Rectangle(location, new Size(1, 1)));
        }

        public void Hide(Control control_1)
        {
            IFloaty floaty = this.floaties_0.Find(control_1);
            if (floaty != null)
            {
                floaty.Hide();
            }
        }

        public void Show(Control control_1)
        {
            IFloaty floaty = this.floaties_0.Find(control_1);
            if (floaty != null)
            {
                floaty.Show();
            }
        }

        public Floaties Floaties
        {
            get { return this.floaties_0; }
        }
    }
}