using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Yutai.ArcGIS.Framework.Docking
{
    public sealed class VisibleNestedPaneCollection : ReadOnlyCollection<DockPane>
    {
        private NestedPaneCollection m_nestedPanes;

        internal VisibleNestedPaneCollection(NestedPaneCollection nestedPanes) : base(new List<DockPane>())
        {
            this.m_nestedPanes = nestedPanes;
        }

        private void CalculateBounds()
        {
            if (base.Count != 0)
            {
                base[0].NestedDockingStatus.SetDisplayingBounds(this.Container.DisplayingRectangle,
                    this.Container.DisplayingRectangle, Rectangle.Empty);
                for (int i = 1; i < base.Count; i++)
                {
                    DockPane pane = base[i];
                    NestedDockingStatus nestedDockingStatus = pane.NestedDockingStatus;
                    NestedDockingStatus status2 = nestedDockingStatus.DisplayingPreviousPane.NestedDockingStatus;
                    Rectangle paneBounds = status2.PaneBounds;
                    bool flag = (nestedDockingStatus.DisplayingAlignment == DockAlignment.Left) ||
                                (nestedDockingStatus.DisplayingAlignment == DockAlignment.Right);
                    Rectangle empty = paneBounds;
                    Rectangle rectangle3 = paneBounds;
                    Rectangle splitterBounds = paneBounds;
                    if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Left)
                    {
                        empty.Width = ((int) (paneBounds.Width*nestedDockingStatus.DisplayingProportion)) - 2;
                        splitterBounds.X = empty.X + empty.Width;
                        splitterBounds.Width = 4;
                        rectangle3.X = splitterBounds.X + splitterBounds.Width;
                        rectangle3.Width = (paneBounds.Width - empty.Width) - splitterBounds.Width;
                    }
                    else if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Right)
                    {
                        rectangle3.Width = (paneBounds.Width -
                                            ((int) (paneBounds.Width*nestedDockingStatus.DisplayingProportion))) - 2;
                        splitterBounds.X = rectangle3.X + rectangle3.Width;
                        splitterBounds.Width = 4;
                        empty.X = splitterBounds.X + splitterBounds.Width;
                        empty.Width = (paneBounds.Width - rectangle3.Width) - splitterBounds.Width;
                    }
                    else if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Top)
                    {
                        empty.Height = ((int) (paneBounds.Height*nestedDockingStatus.DisplayingProportion)) - 2;
                        splitterBounds.Y = empty.Y + empty.Height;
                        splitterBounds.Height = 4;
                        rectangle3.Y = splitterBounds.Y + splitterBounds.Height;
                        rectangle3.Height = (paneBounds.Height - empty.Height) - splitterBounds.Height;
                    }
                    else if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Bottom)
                    {
                        rectangle3.Height = (paneBounds.Height -
                                             ((int) (paneBounds.Height*nestedDockingStatus.DisplayingProportion))) - 2;
                        splitterBounds.Y = rectangle3.Y + rectangle3.Height;
                        splitterBounds.Height = 4;
                        empty.Y = splitterBounds.Y + splitterBounds.Height;
                        empty.Height = (paneBounds.Height - rectangle3.Height) - splitterBounds.Height;
                    }
                    else
                    {
                        empty = Rectangle.Empty;
                    }
                    splitterBounds.Intersect(paneBounds);
                    empty.Intersect(paneBounds);
                    rectangle3.Intersect(paneBounds);
                    nestedDockingStatus.SetDisplayingBounds(paneBounds, empty, splitterBounds);
                    status2.SetDisplayingBounds(status2.LogicalBounds, rectangle3, status2.SplitterBounds);
                }
            }
        }

        internal void Refresh()
        {
            NestedDockingStatus nestedDockingStatus;
            base.Items.Clear();
            for (int i = 0; i < this.NestedPanes.Count; i++)
            {
                DockPane item = this.NestedPanes[i];
                nestedDockingStatus = item.NestedDockingStatus;
                nestedDockingStatus.SetDisplayingStatus(true, nestedDockingStatus.PreviousPane,
                    nestedDockingStatus.Alignment, nestedDockingStatus.Proportion);
                base.Items.Add(item);
            }
            foreach (DockPane pane in this.NestedPanes)
            {
                if ((pane.DockState != this.DockState) || pane.IsHidden)
                {
                    pane.Bounds = Rectangle.Empty;
                    pane.SplitterBounds = Rectangle.Empty;
                    this.Remove(pane);
                }
            }
            this.CalculateBounds();
            foreach (DockPane pane in this)
            {
                nestedDockingStatus = pane.NestedDockingStatus;
                pane.Bounds = nestedDockingStatus.PaneBounds;
                pane.SplitterBounds = nestedDockingStatus.SplitterBounds;
                pane.SplitterAlignment = nestedDockingStatus.Alignment;
            }
        }

        private void Remove(DockPane pane)
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
                    pane2.NestedDockingStatus.SetDisplayingStatus(true, nestedDockingStatus.DisplayingPreviousPane,
                        nestedDockingStatus.DisplayingAlignment, nestedDockingStatus.DisplayingProportion);
                    for (num = index - 1; num > base.IndexOf(pane2); num--)
                    {
                        NestedDockingStatus status3 = base[num].NestedDockingStatus;
                        if (status3.PreviousPane == pane)
                        {
                            status3.SetDisplayingStatus(true, pane2, status3.DisplayingAlignment,
                                status3.DisplayingProportion);
                        }
                    }
                }
                else
                {
                    base.Items.Remove(pane);
                }
                nestedDockingStatus.SetDisplayingStatus(false, null, DockAlignment.Left, 0.5);
            }
        }

        public INestedPanesContainer Container
        {
            get { return this.NestedPanes.Container; }
        }

        public DockState DockState
        {
            get { return this.NestedPanes.DockState; }
        }

        public bool IsFloat
        {
            get { return this.NestedPanes.IsFloat; }
        }

        public NestedPaneCollection NestedPanes
        {
            get { return this.m_nestedPanes; }
        }
    }
}