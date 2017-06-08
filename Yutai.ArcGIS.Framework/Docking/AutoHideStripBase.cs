using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    public abstract class AutoHideStripBase : Control
    {
        private GraphicsPath m_displayingArea = null;
        private DockPanel m_dockPanel;
        private PaneCollection m_panesBottom;
        private PaneCollection m_panesLeft;
        private PaneCollection m_panesRight;
        private PaneCollection m_panesTop;

        protected AutoHideStripBase(DockPanel panel)
        {
            this.m_dockPanel = panel;
            this.m_panesTop = new PaneCollection(panel, DockState.DockTopAutoHide);
            this.m_panesBottom = new PaneCollection(panel, DockState.DockBottomAutoHide);
            this.m_panesLeft = new PaneCollection(panel, DockState.DockLeftAutoHide);
            this.m_panesRight = new PaneCollection(panel, DockState.DockRightAutoHide);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Selectable, false);
        }

        protected virtual Pane CreatePane(DockPane dockPane)
        {
            return new Pane(dockPane);
        }

        protected virtual Tab CreateTab(IDockContent content)
        {
            return new Tab(content);
        }

        internal int GetNumberOfPanes(DockState dockState)
        {
            return this.GetPanes(dockState).Count;
        }

        protected PaneCollection GetPanes(DockState dockState)
        {
            if (dockState == DockState.DockTopAutoHide)
            {
                return this.PanesTop;
            }
            if (dockState == DockState.DockBottomAutoHide)
            {
                return this.PanesBottom;
            }
            if (dockState == DockState.DockLeftAutoHide)
            {
                return this.PanesLeft;
            }
            if (dockState != DockState.DockRightAutoHide)
            {
                throw new ArgumentOutOfRangeException("dockState");
            }
            return this.PanesRight;
        }

        protected internal Rectangle GetTabStripRectangle(DockState dockState)
        {
            int height = this.MeasureHeight();
            if ((dockState == DockState.DockTopAutoHide) && (this.PanesTop.Count > 0))
            {
                return new Rectangle(this.RectangleTopLeft.Width, 0, (base.Width - this.RectangleTopLeft.Width) - this.RectangleTopRight.Width, height);
            }
            if ((dockState == DockState.DockBottomAutoHide) && (this.PanesBottom.Count > 0))
            {
                return new Rectangle(this.RectangleBottomLeft.Width, base.Height - height, (base.Width - this.RectangleBottomLeft.Width) - this.RectangleBottomRight.Width, height);
            }
            if ((dockState == DockState.DockLeftAutoHide) && (this.PanesLeft.Count > 0))
            {
                return new Rectangle(0, this.RectangleTopLeft.Width, height, (base.Height - this.RectangleTopLeft.Height) - this.RectangleBottomLeft.Height);
            }
            if ((dockState == DockState.DockRightAutoHide) && (this.PanesRight.Count > 0))
            {
                return new Rectangle(base.Width - height, this.RectangleTopRight.Width, height, (base.Height - this.RectangleTopRight.Height) - this.RectangleBottomRight.Height);
            }
            return Rectangle.Empty;
        }

        private IDockContent HitTest()
        {
            Point point = base.PointToClient(Control.MousePosition);
            return this.HitTest(point);
        }

        protected abstract IDockContent HitTest(Point point);
        protected internal abstract int MeasureHeight();
        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.RefreshChanges();
            base.OnLayout(levent);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                IDockContent content = this.HitTest();
                if (content != null)
                {
                    content.DockHandler.Activate();
                }
            }
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            IDockContent content = this.HitTest();
            if ((content != null) && (this.DockPanel.ActiveAutoHideContent != content))
            {
                this.DockPanel.ActiveAutoHideContent = content;
            }
            base.ResetMouseEventArgs();
        }

        protected virtual void OnRefreshChanges()
        {
        }

        internal void RefreshChanges()
        {
            if (!base.IsDisposed)
            {
                this.SetRegion();
                this.OnRefreshChanges();
            }
        }

        private void SetRegion()
        {
            this.DisplayingArea.Reset();
            this.DisplayingArea.AddRectangle(this.RectangleTopLeft);
            this.DisplayingArea.AddRectangle(this.RectangleTopRight);
            this.DisplayingArea.AddRectangle(this.RectangleBottomLeft);
            this.DisplayingArea.AddRectangle(this.RectangleBottomRight);
            this.DisplayingArea.AddRectangle(this.GetTabStripRectangle(DockState.DockTopAutoHide));
            this.DisplayingArea.AddRectangle(this.GetTabStripRectangle(DockState.DockBottomAutoHide));
            this.DisplayingArea.AddRectangle(this.GetTabStripRectangle(DockState.DockLeftAutoHide));
            this.DisplayingArea.AddRectangle(this.GetTabStripRectangle(DockState.DockRightAutoHide));
            base.Region = new Region(this.DisplayingArea);
        }

        private GraphicsPath DisplayingArea
        {
            get
            {
                if (this.m_displayingArea == null)
                {
                    this.m_displayingArea = new GraphicsPath();
                }
                return this.m_displayingArea;
            }
        }

        protected DockPanel DockPanel
        {
            get
            {
                return this.m_dockPanel;
            }
        }

        protected PaneCollection PanesBottom
        {
            get
            {
                return this.m_panesBottom;
            }
        }

        protected PaneCollection PanesLeft
        {
            get
            {
                return this.m_panesLeft;
            }
        }

        protected PaneCollection PanesRight
        {
            get
            {
                return this.m_panesRight;
            }
        }

        protected PaneCollection PanesTop
        {
            get
            {
                return this.m_panesTop;
            }
        }

        protected Rectangle RectangleBottomLeft
        {
            get
            {
                int width = this.MeasureHeight();
                return (((this.PanesBottom.Count > 0) && (this.PanesLeft.Count > 0)) ? new Rectangle(0, base.Height - width, width, width) : Rectangle.Empty);
            }
        }

        protected Rectangle RectangleBottomRight
        {
            get
            {
                int width = this.MeasureHeight();
                return (((this.PanesBottom.Count > 0) && (this.PanesRight.Count > 0)) ? new Rectangle(base.Width - width, base.Height - width, width, width) : Rectangle.Empty);
            }
        }

        protected Rectangle RectangleTopLeft
        {
            get
            {
                int width = this.MeasureHeight();
                return (((this.PanesTop.Count > 0) && (this.PanesLeft.Count > 0)) ? new Rectangle(0, 0, width, width) : Rectangle.Empty);
            }
        }

        protected Rectangle RectangleTopRight
        {
            get
            {
                int width = this.MeasureHeight();
                return (((this.PanesTop.Count > 0) && (this.PanesRight.Count > 0)) ? new Rectangle(base.Width - width, 0, width, width) : Rectangle.Empty);
            }
        }

        protected class Pane : IDisposable
        {
            private DockPane m_dockPane;

            protected internal Pane(DockPane dockPane)
            {
                this.m_dockPane = dockPane;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
            }

            ~Pane()
            {
                this.Dispose(false);
            }

            public AutoHideStripBase.TabCollection AutoHideTabs
            {
                get
                {
                    if (this.DockPane.AutoHideTabs == null)
                    {
                        this.DockPane.AutoHideTabs = new AutoHideStripBase.TabCollection(this.DockPane);
                    }
                    return (this.DockPane.AutoHideTabs as AutoHideStripBase.TabCollection);
                }
            }

            public DockPane DockPane
            {
                get
                {
                    return this.m_dockPane;
                }
            }
        }

        protected sealed class PaneCollection : IEnumerable<AutoHideStripBase.Pane>, IEnumerable
        {
            private DockPanel m_dockPanel;
            private AutoHideStateCollection m_states;

            internal PaneCollection(DockPanel panel, DockState dockState)
            {
                this.m_dockPanel = panel;
                this.m_states = new AutoHideStateCollection();
                this.States[DockState.DockTopAutoHide].Selected = dockState == DockState.DockTopAutoHide;
                this.States[DockState.DockBottomAutoHide].Selected = dockState == DockState.DockBottomAutoHide;
                this.States[DockState.DockLeftAutoHide].Selected = dockState == DockState.DockLeftAutoHide;
                this.States[DockState.DockRightAutoHide].Selected = dockState == DockState.DockRightAutoHide;
            }

            public bool Contains(AutoHideStripBase.Pane pane)
            {
                return (this.IndexOf(pane) != -1);
            }

            public int IndexOf(AutoHideStripBase.Pane pane)
            {
                if (pane != null)
                {
                    int num = 0;
                    foreach (DockPane pane2 in this.DockPanel.Panes)
                    {
                        if (this.States.ContainsPane(pane.DockPane))
                        {
                            if (pane == pane2.AutoHidePane)
                            {
                                return num;
                            }
                            num++;
                        }
                    }
                }
                return -1;
            }

            IEnumerator<AutoHideStripBase.Pane> IEnumerable<AutoHideStripBase.Pane>.GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    yield return this[i];
                }
            }

            public int Count
            {
                get
                {
                    int num = 0;
                    foreach (DockPane pane in this.DockPanel.Panes)
                    {
                        if (this.States.ContainsPane(pane))
                        {
                            num++;
                        }
                    }
                    return num;
                }
            }

            public DockPanel DockPanel
            {
                get
                {
                    return this.m_dockPanel;
                }
            }

            public AutoHideStripBase.Pane this[int index]
            {
                get
                {
                    int num = 0;
                    foreach (DockPane pane in this.DockPanel.Panes)
                    {
                        if (this.States.ContainsPane(pane))
                        {
                            if (num == index)
                            {
                                if (pane.AutoHidePane == null)
                                {
                                    pane.AutoHidePane = this.DockPanel.AutoHideStripControl.CreatePane(pane);
                                }
                                return (pane.AutoHidePane as AutoHideStripBase.Pane);
                            }
                            num++;
                        }
                    }
                    throw new ArgumentOutOfRangeException("index");
                }
            }

            private AutoHideStateCollection States
            {
                get
                {
                    return this.m_states;
                }
            }

            private class AutoHideState
            {
                public DockState m_dockState;
                public bool m_selected = false;

                public AutoHideState(DockState dockState)
                {
                    this.m_dockState = dockState;
                }

                public DockState DockState
                {
                    get
                    {
                        return this.m_dockState;
                    }
                }

                public bool Selected
                {
                    get
                    {
                        return this.m_selected;
                    }
                    set
                    {
                        this.m_selected = value;
                    }
                }
            }

            private class AutoHideStateCollection
            {
                private AutoHideStripBase.PaneCollection.AutoHideState[] m_states = new AutoHideStripBase.PaneCollection.AutoHideState[] { new AutoHideStripBase.PaneCollection.AutoHideState(DockState.DockTopAutoHide), new AutoHideStripBase.PaneCollection.AutoHideState(DockState.DockBottomAutoHide), new AutoHideStripBase.PaneCollection.AutoHideState(DockState.DockLeftAutoHide), new AutoHideStripBase.PaneCollection.AutoHideState(DockState.DockRightAutoHide) };

                public bool ContainsPane(DockPane pane)
                {
                    if (!pane.IsHidden)
                    {
                        for (int i = 0; i < this.m_states.Length; i++)
                        {
                            if ((this.m_states[i].DockState == pane.DockState) && this.m_states[i].Selected)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }

                public AutoHideStripBase.PaneCollection.AutoHideState this[DockState dockState]
                {
                    get
                    {
                        for (int i = 0; i < this.m_states.Length; i++)
                        {
                            if (this.m_states[i].DockState == dockState)
                            {
                                return this.m_states[i];
                            }
                        }
                        throw new ArgumentOutOfRangeException("dockState");
                    }
                }
            }


        }

        protected class Tab : IDisposable
        {
            private IDockContent m_content;

            protected internal Tab(IDockContent content)
            {
                this.m_content = content;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
            }

            ~Tab()
            {
                this.Dispose(false);
            }

            public IDockContent Content
            {
                get
                {
                    return this.m_content;
                }
            }
        }

        protected sealed class TabCollection : IEnumerable<AutoHideStripBase.Tab>, IEnumerable
        {
            private DockPane m_dockPane = null;

            internal TabCollection(DockPane pane)
            {
                this.m_dockPane = pane;
            }

            public bool Contains(AutoHideStripBase.Tab tab)
            {
                return (this.IndexOf(tab) != -1);
            }

            public bool Contains(IDockContent content)
            {
                return (this.IndexOf(content) != -1);
            }

            public int IndexOf(AutoHideStripBase.Tab tab)
            {
                if (tab == null)
                {
                    return -1;
                }
                return this.IndexOf(tab.Content);
            }

            public int IndexOf(IDockContent content)
            {
                return this.DockPane.DisplayingContents.IndexOf(content);
            }

            IEnumerator<AutoHideStripBase.Tab> IEnumerable<AutoHideStripBase.Tab>.GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                {
                    yield return this[i];
                }
            }

            public int Count
            {
                get
                {
                    return this.DockPane.DisplayingContents.Count;
                }
            }

            public DockPane DockPane
            {
                get
                {
                    return this.m_dockPane;
                }
            }

            public DockPanel DockPanel
            {
                get
                {
                    return this.DockPane.DockPanel;
                }
            }

            public AutoHideStripBase.Tab this[int index]
            {
                get
                {
                    IDockContent content = this.DockPane.DisplayingContents[index];
                    if (content == null)
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }
                    if (content.DockHandler.AutoHideTab == null)
                    {
                        content.DockHandler.AutoHideTab = this.DockPanel.AutoHideStripControl.CreateTab(content);
                    }
                    return (content.DockHandler.AutoHideTab as AutoHideStripBase.Tab);
                }
            }


        }
    }
}

