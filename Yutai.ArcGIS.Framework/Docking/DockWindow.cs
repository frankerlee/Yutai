using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    [ToolboxItem(false)]
    public class DockWindow : Panel, INestedPanesContainer, ISplitterDragSource, IDragSource
    {
        private DockPanel m_dockPanel;
        private DockState m_dockState;
        private NestedPaneCollection m_nestedPanes;
        private SplitterControl m_splitter;

        internal DockWindow(DockPanel dockPanel, DockState dockState)
        {
            this.m_nestedPanes = new NestedPaneCollection(this);
            this.m_dockPanel = dockPanel;
            this.m_dockState = dockState;
            base.Visible = false;
            base.SuspendLayout();
            if ((((this.DockState == DockState.DockLeft) || (this.DockState == DockState.DockRight)) || (this.DockState == DockState.DockTop)) || (this.DockState == DockState.DockBottom))
            {
                this.m_splitter = new SplitterControl();
                base.Controls.Add(this.m_splitter);
            }
            if (this.DockState == DockState.DockLeft)
            {
                this.Dock = DockStyle.Left;
                this.m_splitter.Dock = DockStyle.Right;
            }
            else if (this.DockState == DockState.DockRight)
            {
                this.Dock = DockStyle.Right;
                this.m_splitter.Dock = DockStyle.Left;
            }
            else if (this.DockState == DockState.DockTop)
            {
                this.Dock = DockStyle.Top;
                this.m_splitter.Dock = DockStyle.Bottom;
            }
            else if (this.DockState == DockState.DockBottom)
            {
                this.Dock = DockStyle.Bottom;
                this.m_splitter.Dock = DockStyle.Top;
            }
            else if (this.DockState == DockState.Document)
            {
                this.Dock = DockStyle.Fill;
            }
            base.ResumeLayout();
        }

        void ISplitterDragSource.BeginDrag(Rectangle rectSplitter)
        {
        }

        void ISplitterDragSource.EndDrag()
        {
        }

        void ISplitterDragSource.MoveSplitter(int offset)
        {
            if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
            {
                base.SendToBack();
            }
            Rectangle dockArea = this.DockPanel.DockArea;
            if ((this.DockState == DockState.DockLeft) && (dockArea.Width > 0))
            {
                if (this.DockPanel.DockLeftPortion > 1.0)
                {
                    this.DockPanel.DockLeftPortion = base.Width + offset;
                }
                else
                {
                    DockPanel dockPanel = this.DockPanel;
                    dockPanel.DockLeftPortion += ((double) offset) / ((double) dockArea.Width);
                }
            }
            else if ((this.DockState == DockState.DockRight) && (dockArea.Width > 0))
            {
                if (this.DockPanel.DockRightPortion > 1.0)
                {
                    this.DockPanel.DockRightPortion = base.Width - offset;
                }
                else
                {
                    DockPanel panel2 = this.DockPanel;
                    panel2.DockRightPortion -= ((double) offset) / ((double) dockArea.Width);
                }
            }
            else if ((this.DockState == DockState.DockBottom) && (dockArea.Height > 0))
            {
                if (this.DockPanel.DockBottomPortion > 1.0)
                {
                    this.DockPanel.DockBottomPortion = base.Height - offset;
                }
                else
                {
                    DockPanel panel3 = this.DockPanel;
                    panel3.DockBottomPortion -= ((double) offset) / ((double) dockArea.Height);
                }
            }
            else if ((this.DockState == DockState.DockTop) && (dockArea.Height > 0))
            {
                if (this.DockPanel.DockTopPortion > 1.0)
                {
                    this.DockPanel.DockTopPortion = base.Height + offset;
                }
                else
                {
                    DockPanel panel4 = this.DockPanel;
                    panel4.DockTopPortion += ((double) offset) / ((double) dockArea.Height);
                }
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.VisibleNestedPanes.Refresh();
            if (this.VisibleNestedPanes.Count == 0)
            {
                if (base.Visible)
                {
                    base.Visible = false;
                }
            }
            else if (!base.Visible)
            {
                base.Visible = true;
                this.VisibleNestedPanes.Refresh();
            }
            base.OnLayout(levent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.DockState == DockState.Document)
            {
                e.Graphics.DrawRectangle(SystemPens.ControlDark, base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
            }
            base.OnPaint(e);
        }

        internal DockPane DefaultPane
        {
            get
            {
                return ((this.VisibleNestedPanes.Count == 0) ? null : this.VisibleNestedPanes[0]);
            }
        }

        public virtual Rectangle DisplayingRectangle
        {
            get
            {
                Rectangle clientRectangle = base.ClientRectangle;
                if (this.DockState == DockState.Document)
                {
                    clientRectangle.X++;
                    clientRectangle.Y++;
                    clientRectangle.Width -= 2;
                    clientRectangle.Height -= 2;
                    return clientRectangle;
                }
                if (this.DockState == DockState.DockLeft)
                {
                    clientRectangle.Width -= 4;
                    return clientRectangle;
                }
                if (this.DockState == DockState.DockRight)
                {
                    clientRectangle.X += 4;
                    clientRectangle.Width -= 4;
                    return clientRectangle;
                }
                if (this.DockState == DockState.DockTop)
                {
                    clientRectangle.Height -= 4;
                    return clientRectangle;
                }
                if (this.DockState == DockState.DockBottom)
                {
                    clientRectangle.Y += 4;
                    clientRectangle.Height -= 4;
                }
                return clientRectangle;
            }
        }

        public DockPanel DockPanel
        {
            get
            {
                return this.m_dockPanel;
            }
        }

        public DockState DockState
        {
            get
            {
                return this.m_dockState;
            }
        }

        public bool IsFloat
        {
            get
            {
                return (this.DockState == DockState.Float);
            }
        }

        Control IDragSource.DragControl
        {
            get
            {
                return this;
            }
        }

        Rectangle ISplitterDragSource.DragLimitBounds
        {
            get
            {
                Point location;
                Rectangle dockArea = this.DockPanel.DockArea;
                if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
                {
                    location = base.Location;
                }
                else
                {
                    location = this.DockPanel.DockArea.Location;
                }
                if (((ISplitterDragSource) this).IsVertical)
                {
                    dockArea.X += 24;
                    dockArea.Width -= 48;
                    dockArea.Y = location.Y;
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
                    {
                        dockArea.Height = base.Height;
                    }
                }
                else
                {
                    dockArea.Y += 24;
                    dockArea.Height -= 48;
                    dockArea.X = location.X;
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
                    {
                        dockArea.Width = base.Width;
                    }
                }
                return this.DockPanel.RectangleToScreen(dockArea);
            }
        }

        bool ISplitterDragSource.IsVertical
        {
            get
            {
                return ((this.DockState == DockState.DockLeft) || (this.DockState == DockState.DockRight));
            }
        }

        public NestedPaneCollection NestedPanes
        {
            get
            {
                return this.m_nestedPanes;
            }
        }

        public VisibleNestedPaneCollection VisibleNestedPanes
        {
            get
            {
                return this.NestedPanes.VisibleNestedPanes;
            }
        }

        private class SplitterControl : SplitterBase
        {
            protected override void StartDrag()
            {
                DockWindow parent = base.Parent as DockWindow;
                if (parent != null)
                {
                    parent.DockPanel.BeginDrag(parent, parent.RectangleToScreen(base.Bounds));
                }
            }

            protected override int SplitterSize
            {
                get
                {
                    return 4;
                }
            }
        }
    }
}

