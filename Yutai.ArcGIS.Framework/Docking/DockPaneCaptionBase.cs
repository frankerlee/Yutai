using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    public abstract class DockPaneCaptionBase : Control
    {
        private DockPane m_dockPane;

        protected internal DockPaneCaptionBase(DockPane pane)
        {
            this.m_dockPane = pane;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.Selectable, false);
        }

        protected internal abstract int MeasureHeight();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((((e.Button == MouseButtons.Left) && this.DockPane.DockPanel.AllowEndUserDocking) && (this.DockPane.AllowDockDragAndDrop && !DockHelper.IsDockStateAutoHide(this.DockPane.DockState))) && (this.DockPane.ActiveContent != null))
            {
                this.DockPane.DockPanel.BeginDrag(this.DockPane);
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

        protected virtual void OnRightToLeftLayoutChanged()
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

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 515)
            {
                if (DockHelper.IsDockStateAutoHide(this.DockPane.DockState))
                {
                    this.DockPane.DockPanel.ActiveAutoHideContent = null;
                    return;
                }
                if (this.DockPane.IsFloat)
                {
                    this.DockPane.RestoreToPanel();
                }
                else
                {
                    this.DockPane.Float();
                }
            }
            base.WndProc(ref m);
        }

        protected DockPane.AppearanceStyle Appearance
        {
            get
            {
                return this.DockPane.Appearance;
            }
        }

        protected DockPane DockPane
        {
            get
            {
                return this.m_dockPane;
            }
        }

        protected bool HasTabPageContextMenu
        {
            get
            {
                return this.DockPane.HasTabPageContextMenu;
            }
        }
    }
}

