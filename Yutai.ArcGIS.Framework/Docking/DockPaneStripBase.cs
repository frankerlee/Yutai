using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    public abstract class DockPaneStripBase : Control
    {
        private DockPane m_dockPane;
        private TabCollection m_tabs = null;

        protected DockPaneStripBase(DockPane pane)
        {
            this.m_dockPane = pane;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Selectable, false);
            this.AllowDrop = true;
        }

        protected internal virtual Tab CreateTab(IDockContent content)
        {
            return new Tab(content);
        }

        protected internal abstract void EnsureTabVisible(IDockContent content);
        protected internal abstract GraphicsPath GetOutline(int index);

        protected int HitTest()
        {
            return this.HitTest(base.PointToClient(Control.MousePosition));
        }

        protected internal abstract int HitTest(Point point);
        protected internal abstract int MeasureHeight();

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            int num = this.HitTest();
            if (num != -1)
            {
                IDockContent content = this.Tabs[num].Content;
                if (this.DockPane.ActiveContent != content)
                {
                    this.DockPane.ActiveContent = content;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int num = this.HitTest();
            if (num != -1)
            {
                IDockContent content = this.Tabs[num].Content;
                if (this.DockPane.ActiveContent != content)
                {
                    this.DockPane.ActiveContent = content;
                }
            }
            if ((e.Button == MouseButtons.Left) &&
                ((this.DockPane.DockPanel.AllowEndUserDocking && this.DockPane.AllowDockDragAndDrop) &&
                 this.DockPane.ActiveContent.DockHandler.AllowEndUserDocking))
            {
                this.DockPane.DockPanel.BeginDrag(this.DockPane.ActiveContent.DockHandler);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                this.ShowTabPageContextMenu(new Point(e.X, e.Y));
            }
        }

        protected virtual void OnRefreshChanges()
        {
        }

        internal void RefreshChanges()
        {
            if (!base.IsDisposed)
            {
                this.OnRefreshChanges();
            }
        }

        protected void ShowTabPageContextMenu(Point position)
        {
            this.DockPane.ShowTabPageContextMenu(this, position);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 515)
            {
                base.WndProc(ref m);
                int num = this.HitTest();
                if (this.DockPane.DockPanel.AllowEndUserDocking && (num != -1))
                {
                    IDockContent content = this.Tabs[num].Content;
                    if (content.DockHandler.CheckDockState(!content.DockHandler.IsFloat) != DockState.Unknown)
                    {
                        content.DockHandler.IsFloat = !content.DockHandler.IsFloat;
                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected DockPane.AppearanceStyle Appearance
        {
            get { return this.DockPane.Appearance; }
        }

        protected DockPane DockPane
        {
            get { return this.m_dockPane; }
        }

        protected bool HasTabPageContextMenu
        {
            get { return this.DockPane.HasTabPageContextMenu; }
        }

        protected TabCollection Tabs
        {
            get
            {
                if (this.m_tabs == null)
                {
                    this.m_tabs = new TabCollection(this.DockPane);
                }
                return this.m_tabs;
            }
        }

        internal protected class Tab : IDisposable
        {
            private IDockContent m_content;

            public Tab(IDockContent content)
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
                get { return this.m_content; }
            }

            public Form ContentForm
            {
                get { return (this.m_content as Form); }
            }
        }

        protected sealed class TabCollection : IEnumerable<DockPaneStripBase.Tab>, IEnumerable
        {
            private DockPane m_dockPane;

            internal TabCollection(DockPane pane)
            {
                this.m_dockPane = pane;
            }

            public bool Contains(DockPaneStripBase.Tab tab)
            {
                return (this.IndexOf(tab) != -1);
            }

            public bool Contains(IDockContent content)
            {
                return (this.IndexOf(content) != -1);
            }

            public int IndexOf(DockPaneStripBase.Tab tab)
            {
                if (tab == null)
                {
                    return -1;
                }
                return this.DockPane.DisplayingContents.IndexOf(tab.Content);
            }

            public int IndexOf(IDockContent content)
            {
                return this.DockPane.DisplayingContents.IndexOf(content);
            }

            IEnumerator<DockPaneStripBase.Tab> IEnumerable<DockPaneStripBase.Tab>.GetEnumerator()
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
                get { return this.DockPane.DisplayingContents.Count; }
            }

            public DockPane DockPane
            {
                get { return this.m_dockPane; }
            }

            public DockPaneStripBase.Tab this[int index]
            {
                get
                {
                    IDockContent content = this.DockPane.DisplayingContents[index];
                    if (content == null)
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }
                    return content.DockHandler.GetTab(this.DockPane.TabStripControl);
                }
            }
        }
    }
}