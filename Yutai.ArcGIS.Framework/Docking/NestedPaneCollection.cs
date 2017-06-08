using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Yutai.ArcGIS.Framework.Docking
{
    public sealed class NestedPaneCollection : ReadOnlyCollection<DockPane>
    {
        private INestedPanesContainer m_container;
        private VisibleNestedPaneCollection m_visibleNestedPanes;

        internal NestedPaneCollection(INestedPanesContainer container) : base(new List<DockPane>())
        {
            this.m_container = container;
            this.m_visibleNestedPanes = new VisibleNestedPaneCollection(this);
        }

        internal void Add(DockPane pane)
        {
            if (pane != null)
            {
                NestedPaneCollection panes = (pane.NestedPanesContainer == null) ? null : pane.NestedPanesContainer.NestedPanes;
                if (panes != null)
                {
                    panes.InternalRemove(pane);
                }
                base.Items.Add(pane);
                if (panes != null)
                {
                    panes.CheckFloatWindowDispose();
                }
            }
        }

        private void CheckFloatWindowDispose()
        {
            if ((base.Count == 0) && (this.Container.DockState == DockState.Float))
            {
                FloatWindow container = (FloatWindow) this.Container;
                if (!(container.Disposing || container.IsDisposed))
                {
                    NativeMethods.PostMessage(((FloatWindow) this.Container).Handle, 0x401, 0, 0);
                }
            }
        }

        public DockPane GetDefaultPreviousPane(DockPane pane)
        {
            for (int i = base.Count - 1; i >= 0; i--)
            {
                if (base[i] != pane)
                {
                    return base[i];
                }
            }
            return null;
        }

        private void InternalRemove(DockPane pane)
        {
            if (base.Contains(pane))
            {
                int num;
                NestedDockingStatus nestedDockingStatus = pane.NestedDockingStatus;
                DockPane pane2 = null;
                for (num = base.Count - 1; num > base.IndexOf(pane); num--)
                {
                    if (base[num].NestedDockingStatus.PreviousPane == pane)
                    {
                        pane2 = base[num];
                        break;
                    }
                }
                if (pane2 != null)
                {
                    int index = base.IndexOf(pane2);
                    base.Items.Remove(pane2);
                    base.Items[base.IndexOf(pane)] = pane2;
                    pane2.NestedDockingStatus.SetStatus(this, nestedDockingStatus.PreviousPane, nestedDockingStatus.Alignment, nestedDockingStatus.Proportion);
                    for (num = index - 1; num > base.IndexOf(pane2); num--)
                    {
                        NestedDockingStatus status3 = base[num].NestedDockingStatus;
                        if (status3.PreviousPane == pane)
                        {
                            status3.SetStatus(this, pane2, status3.Alignment, status3.Proportion);
                        }
                    }
                }
                else
                {
                    base.Items.Remove(pane);
                }
                nestedDockingStatus.SetStatus(null, null, DockAlignment.Left, 0.5);
                nestedDockingStatus.SetDisplayingStatus(false, null, DockAlignment.Left, 0.5);
                nestedDockingStatus.SetDisplayingBounds(Rectangle.Empty, Rectangle.Empty, Rectangle.Empty);
            }
        }

        internal void Remove(DockPane pane)
        {
            this.InternalRemove(pane);
            this.CheckFloatWindowDispose();
        }

        public INestedPanesContainer Container
        {
            get
            {
                return this.m_container;
            }
        }

        public DockState DockState
        {
            get
            {
                return this.Container.DockState;
            }
        }

        public bool IsFloat
        {
            get
            {
                return (this.DockState == DockState.Float);
            }
        }

        public VisibleNestedPaneCollection VisibleNestedPanes
        {
            get
            {
                return this.m_visibleNestedPanes;
            }
        }
    }
}

