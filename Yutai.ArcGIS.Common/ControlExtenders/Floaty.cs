using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    internal sealed class Floaty : Form, IFloaty
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private DockExtender dockExtender_0;
        private DockState dockState_0;
        private const int SC_MOVE = 0xf010;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_NCLBUTTONDBLCLK = 0xa3;
        private const int WM_SYSCOMMAND = 0x112;

        public event EventHandler Docking;

        public Floaty(DockExtender dockExtender_1)
        {
            this.dockExtender_0 = dockExtender_1;
            this.InitializeComponent();
        }

        internal void Attach(DockState dockState_1)
        {
            this.dockState_0 = dockState_1;
            this.Text = this.dockState_0.Handle.Text;
            (this.dockState_0.Handle as ToolStrip).EndDrag += new EventHandler(this.method_4);
        }

        public void Dock()
        {
            if (this.bool_1)
            {
                this.method_7();
            }
        }

        public void Float()
        {
            if (!this.bool_1)
            {
                this.Text = this.dockState_0.Handle.Text;
                Point location = this.dockState_0.Container.PointToScreen(new Point(0, 0));
                Size size = this.dockState_0.Container.Size;
                if (this.dockState_0.Container.Equals(this.dockState_0.Handle))
                {
                    size.Width += 0x12;
                    size.Height += 0x1c;
                }
                if (size.Width > 600)
                {
                    size.Width = 600;
                }
                if (size.Height > 600)
                {
                    size.Height = 600;
                }
                this.dockState_0.OrgDockingParent = this.dockState_0.Container.Parent;
                this.dockState_0.OrgBounds = this.dockState_0.Container.Bounds;
                this.dockState_0.OrgDockStyle = this.dockState_0.Container.Dock;
                this.dockState_0.Handle.Hide();
                this.dockState_0.Container.Parent = this;
                this.dockState_0.Container.Dock = DockStyle.Fill;
                if (this.dockState_0.Splitter != null)
                {
                    this.dockState_0.Splitter.Visible = false;
                    this.dockState_0.Splitter.Parent = this;
                }
                base.Bounds = new Rectangle(location, size);
                this.bool_1 = true;
                this.Show();
            }
        }

        public void Hide()
        {
            if (base.Visible)
            {
                base.Hide();
            }
            this.dockState_0.Container.Hide();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.ClientSize = new Size(0xb2, 0x7a);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.MaximizeBox = false;
            base.Name = "Floaty";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.ResumeLayout(false);
            this.bool_3 = true;
            this.bool_2 = true;
        }

        private Rectangle method_0(Control control_0)
        {
            Rectangle bounds = control_0.Bounds;
            if (control_0.Parent != null)
            {
                bounds = control_0.Parent.RectangleToScreen(bounds);
            }
            Rectangle clientRectangle = control_0.ClientRectangle;
            int num = (bounds.Width - clientRectangle.Width) / 2;
            bounds.X += num;
            bounds.Y += (bounds.Height - clientRectangle.Height) - num;
            if (!this.bool_3)
            {
                clientRectangle.X += bounds.X;
                clientRectangle.Y += bounds.Y;
                return clientRectangle;
            }
            foreach (Control control in control_0.Controls)
            {
                if (control.Visible)
                {
                    switch (control.Dock)
                    {
                        case DockStyle.Top:
                            clientRectangle.Y += control.Height;
                            clientRectangle.Height -= control.Height;
                            break;

                        case DockStyle.Bottom:
                            clientRectangle.Height -= control.Height;
                            break;

                        case DockStyle.Left:
                            clientRectangle.X += control.Width;
                            clientRectangle.Width -= control.Width;
                            break;

                        case DockStyle.Right:
                            clientRectangle.Width -= control.Width;
                            break;
                    }
                }
            }
            clientRectangle.X += bounds.X;
            clientRectangle.Y += bounds.Y;
            return clientRectangle;
        }

        private void method_1(Control control_0, Point point_0)
        {
            Rectangle rectangle = this.method_0(control_0);
            Rectangle rectangle2 = rectangle;
            float num = ((float) (point_0.X - rectangle.Left)) / ((float) rectangle.Width);
            float num2 = ((float) (point_0.Y - rectangle.Top)) / ((float) rectangle.Height);
            this.dockExtender_0.Overlay.Dock = DockStyle.None;
            if ((((num > 0f) && (num < num2)) && ((num < 0.25) && (num2 < 0.75))) && (num2 > 0.25))
            {
                rectangle.Width /= 2;
                if (rectangle.Width > base.Width)
                {
                    rectangle.Width = base.Width;
                }
                this.dockExtender_0.Overlay.Dock = DockStyle.Left;
            }
            if ((((num < 1f) && (num > num2)) && ((num > 0.75) && (num2 < 0.75))) && (num2 > 0.25))
            {
                rectangle.Width /= 2;
                if (rectangle.Width > base.Width)
                {
                    rectangle.Width = base.Width;
                }
                rectangle.X = (rectangle2.X + rectangle2.Width) - rectangle.Width;
                this.dockExtender_0.Overlay.Dock = DockStyle.Right;
            }
            if ((((num2 > 0f) && (num2 < num)) && ((num2 < 0.25) && (num < 0.75))) && (num > 0.25))
            {
                rectangle.Height /= 2;
                if (rectangle.Height > base.Height)
                {
                    rectangle.Height = base.Height;
                }
                this.dockExtender_0.Overlay.Dock = DockStyle.Top;
            }
            if ((((num2 < 1f) && (num2 > num)) && ((num2 > 0.75) && (num < 0.75))) && (num > 0.25))
            {
                rectangle.Height /= 2;
                if (rectangle.Height > base.Height)
                {
                    rectangle.Height = base.Height;
                }
                rectangle.Y = (rectangle2.Y + rectangle2.Height) - rectangle.Height;
                this.dockExtender_0.Overlay.Dock = DockStyle.Bottom;
            }
            if (this.dockExtender_0.Overlay.Dock != DockStyle.None)
            {
                this.dockExtender_0.Overlay.Bounds = rectangle;
            }
            else
            {
                this.dockExtender_0.Overlay.Hide();
            }
            if (!(this.dockExtender_0.Overlay.Visible || (this.dockExtender_0.Overlay.Dock == DockStyle.None)))
            {
                this.dockExtender_0.Overlay.DockHostControl = control_0;
                this.dockExtender_0.Overlay.Show(this.dockState_0.OrgDockHost);
                base.BringToFront();
            }
        }

        private void method_10(object sender, EventArgs e)
        {
            this.bool_0 = false;
        }

        private void method_11(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && this.bool_0)
            {
                this.dockState_0.Handle.PointToScreen(new Point(e.X, e.Y));
                this.method_6(this.dockState_0, e.X, e.Y);
            }
        }

        private Rectangle method_2(Point point_0, ToolStripPanel toolStripPanel_0, DockStyle dockStyle_0)
        {
            Rectangle bounds = toolStripPanel_0.Bounds;
            int num = 0x19;
            if (toolStripPanel_0.Controls.Count == 0)
            {
                if ((dockStyle_0 == DockStyle.Top) || (dockStyle_0 == DockStyle.Bottom))
                {
                    bounds.Height = num;
                    return bounds;
                }
                if ((dockStyle_0 == DockStyle.Right) || (dockStyle_0 == DockStyle.Left))
                {
                    bounds.Width = num;
                }
                return bounds;
            }
            for (int i = 0; i < toolStripPanel_0.Controls.Count; i++)
            {
                Rectangle r = toolStripPanel_0.Controls[i].Bounds;
                r = toolStripPanel_0.Parent.RectangleToScreen(r);
                if (r.Contains(point_0))
                {
                    if (dockStyle_0 == DockStyle.Top)
                    {
                        r.Y += num;
                    }
                    else if (dockStyle_0 == DockStyle.Bottom)
                    {
                        r.Y += num;
                    }
                    else if ((dockStyle_0 == DockStyle.Right) || (dockStyle_0 == DockStyle.Left))
                    {
                        r.X += num;
                    }
                    return toolStripPanel_0.Parent.RectangleToClient(r);
                }
            }
            return bounds;
        }

        private void method_3(Control control_0, Point point_0)
        {
            Rectangle bounds = (control_0 as ToolStripContainer).TopToolStripPanel.Bounds;
            Rectangle r = (control_0 as ToolStripContainer).BottomToolStripPanel.Bounds;
            Rectangle rectangle3 = (control_0 as ToolStripContainer).RightToolStripPanel.Bounds;
            Rectangle rectangle4 = (control_0 as ToolStripContainer).LeftToolStripPanel.Bounds;
            bounds = control_0.RectangleToScreen(bounds);
            r = control_0.RectangleToScreen(r);
            rectangle3 = control_0.RectangleToScreen(rectangle3);
            rectangle4 = control_0.RectangleToScreen(rectangle4);
            if (bounds.Height == 0)
            {
                bounds.Y -= 5;
                bounds.Height = 10;
            }
            if (r.Height == 0)
            {
                r.Y -= 5;
                r.Height = 10;
            }
            if (rectangle3.Width == 0)
            {
                rectangle3.X -= 5;
                rectangle3.Width = 10;
            }
            if (rectangle4.Width == 0)
            {
                rectangle4.X -= 5;
                rectangle4.Width = 10;
            }
            Rectangle rectangle5 = this.method_0((control_0 as ToolStripContainer).ContentPanel);
            this.dockExtender_0.Overlay.Dock = DockStyle.None;
            if (bounds.Contains(point_0))
            {
                this.dockState_0.OrgDockStyle = DockStyle.Top;
                this.dockExtender_0.Overlay.Dock = DockStyle.Top;
                rectangle5 = this.method_2(point_0, (control_0 as ToolStripContainer).TopToolStripPanel, DockStyle.Top);
            }
            else if (r.Contains(point_0))
            {
                this.dockState_0.OrgDockStyle = DockStyle.Bottom;
                this.dockExtender_0.Overlay.Dock = DockStyle.Bottom;
                rectangle5 = this.method_2(point_0, (control_0 as ToolStripContainer).BottomToolStripPanel, DockStyle.Bottom);
            }
            else if (rectangle3.Contains(point_0))
            {
                this.dockState_0.OrgDockStyle = DockStyle.Right;
                this.dockExtender_0.Overlay.Dock = DockStyle.Right;
                rectangle5 = this.method_2(point_0, (control_0 as ToolStripContainer).RightToolStripPanel, DockStyle.Right);
            }
            else if (rectangle4.Contains(point_0))
            {
                this.dockState_0.OrgDockStyle = DockStyle.Left;
                this.dockExtender_0.Overlay.Dock = DockStyle.Left;
                rectangle5 = this.method_2(point_0, (control_0 as ToolStripContainer).LeftToolStripPanel, DockStyle.Left);
            }
            if (this.dockExtender_0.Overlay.Dock != DockStyle.None)
            {
                this.dockState_0.Handle.Bounds = rectangle5;
                this.dockExtender_0.Overlay.Bounds = rectangle5;
            }
            else
            {
                this.dockExtender_0.Overlay.Hide();
            }
            if (!(this.dockExtender_0.Overlay.Visible || (this.dockExtender_0.Overlay.Dock == DockStyle.None)))
            {
                this.dockExtender_0.Overlay.DockHostControl = control_0;
                this.dockExtender_0.Overlay.Show(this.dockState_0.OrgDockHost);
                if (this.dockState_0.Handle is ToolStrip)
                {
                    (this.dockState_0.Handle as ToolStrip).GripStyle = ToolStripGripStyle.Visible;
                }
            }
        }

        private void method_4(object sender, EventArgs e)
        {
            this.method_6(this.dockState_0, 0, 0);
        }

        private bool method_5(ToolStripContainer toolStripContainer_0, Point point_0)
        {
            Rectangle bounds = toolStripContainer_0.TopToolStripPanel.Bounds;
            if (toolStripContainer_0.RectangleToScreen(bounds).Contains(point_0))
            {
                return true;
            }
            bounds = toolStripContainer_0.BottomToolStripPanel.Bounds;
            if (toolStripContainer_0.RectangleToScreen(bounds).Contains(point_0))
            {
                return true;
            }
            bounds = toolStripContainer_0.RightToolStripPanel.Bounds;
            if (toolStripContainer_0.RectangleToScreen(bounds).Contains(point_0))
            {
                return true;
            }
            bounds = toolStripContainer_0.LeftToolStripPanel.Bounds;
            return toolStripContainer_0.RectangleToScreen(bounds).Contains(point_0);
        }

        private void method_6(DockState dockState_1, int int_0, int int_1)
        {
            Point position = Cursor.Position;
            Control control = this.dockExtender_0.FindDockHost(this, position);
            if ((control == null) || !this.method_5(control as ToolStripContainer, position))
            {
                this.dockState_0 = dockState_1;
                this.Text = this.dockState_0.Handle.Text;
                if (this.dockState_0.Handle is ToolStrip)
                {
                    (this.dockState_0.Handle as ToolStrip).GripStyle = ToolStripGripStyle.Hidden;
                }
                Size size = this.dockState_0.Container.Size;
                if (size.Width < size.Height)
                {
                    size = new Size(size.Height, size.Width);
                }
                if (this.dockState_0.Container.Equals(this.dockState_0.Handle))
                {
                    size.Width += 0x12;
                    size.Height += 0x1c;
                }
                if (size.Width > 600)
                {
                    size.Width = 600;
                }
                if (size.Height > 600)
                {
                    size.Height = 600;
                }
                this.dockState_0.OrgDockingParent = this.dockState_0.Container.Parent;
                this.dockState_0.OrgBounds = this.dockState_0.Container.Bounds;
                this.dockState_0.OrgDockStyle = this.dockState_0.Container.Dock;
                this.dockState_0.Handle.Hide();
                this.dockState_0.Container.Parent = this;
                this.dockState_0.Container.Dock = DockStyle.Fill;
                if (this.dockState_0.Splitter != null)
                {
                    this.dockState_0.Splitter.Visible = false;
                    this.dockState_0.Splitter.Parent = this;
                }
                SendMessage(this.dockState_0.Handle.Handle.ToInt32(), 0x202, 0, 0);
                position.X -= int_0;
                position.Y -= int_1;
                base.Bounds = new Rectangle(position, size);
                this.bool_1 = true;
                this.Show();
                SendMessage(base.Handle.ToInt32(), 0x112, 0xf012, 0);
            }
        }

        private void method_7()
        {
            this.dockState_0.OrgDockHost.TopLevelControl.BringToFront();
            this.Hide();
            this.dockState_0.Container.Visible = false;
            if (this.dockState_0.OrgDockingParent is ToolStripContainer)
            {
                ToolStripContainer orgDockingParent = this.dockState_0.OrgDockingParent as ToolStripContainer;
                if (this.dockState_0.OrgDockStyle == DockStyle.Bottom)
                {
                    this.dockState_0.Container.Parent = orgDockingParent.BottomToolStripPanel;
                }
                else if (this.dockState_0.OrgDockStyle == DockStyle.Top)
                {
                    this.dockState_0.Container.Parent = orgDockingParent.TopToolStripPanel;
                }
                else if (this.dockState_0.OrgDockStyle == DockStyle.Right)
                {
                    this.dockState_0.Container.Parent = orgDockingParent.RightToolStripPanel;
                }
                else if (this.dockState_0.OrgDockStyle == DockStyle.Left)
                {
                    this.dockState_0.Container.Parent = orgDockingParent.LeftToolStripPanel;
                }
                this.dockState_0.Container.Dock = this.dockState_0.OrgDockStyle;
            }
            else
            {
                this.dockState_0.Container.Parent = this.dockState_0.OrgDockingParent;
                this.dockState_0.Container.Dock = this.dockState_0.OrgDockStyle;
                this.dockState_0.Container.Bounds = this.dockState_0.OrgBounds;
            }
            this.dockState_0.Handle.Visible = true;
            this.dockState_0.Container.Visible = true;
            if (this.bool_3)
            {
                this.dockState_0.Container.BringToFront();
            }
            if (((this.dockState_0.Splitter != null) && (this.dockState_0.OrgDockStyle != DockStyle.Fill)) && (this.dockState_0.OrgDockStyle != DockStyle.None))
            {
                this.dockState_0.Splitter.Parent = this.dockState_0.OrgDockingParent;
                this.dockState_0.Splitter.Dock = this.dockState_0.OrgDockStyle;
                this.dockState_0.Splitter.Visible = true;
                if (this.bool_3)
                {
                    this.dockState_0.Splitter.BringToFront();
                }
                else
                {
                    this.dockState_0.Splitter.SendToBack();
                }
            }
            if (!this.bool_3)
            {
                this.dockState_0.Container.SendToBack();
            }
            this.bool_1 = false;
            if (this.Docking != null)
            {
                this.Docking(this, new EventArgs());
            }
            if (this.dockState_0.Handle is ToolStrip)
            {
                (this.dockState_0.Handle as ToolStrip).GripStyle = ToolStripGripStyle.Visible;
            }
        }

        private void method_8()
        {
            (this.dockState_0.Handle as ToolStrip).EndDrag -= new EventHandler(this.method_4);
            this.dockState_0.Container = null;
            this.dockState_0.Handle = null;
        }

        private void method_9(object sender, EventArgs e)
        {
            this.bool_0 = true;
        }

        protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
        {
            cancelEventArgs_0.Cancel = true;
            this.Hide();
            base.OnClosing(cancelEventArgs_0);
        }

        protected override void OnMouseMove(MouseEventArgs mouseEventArgs_0)
        {
            base.OnMouseMove(mouseEventArgs_0);
        }

        protected override void OnMove(EventArgs eventArgs_0)
        {
            if (!base.IsDisposed)
            {
                Point position = Cursor.Position;
                Point point2 = base.PointToClient(position);
                if (((point2.Y >= -21) && (point2.Y <= 0)) && ((point2.X >= -1) && (point2.X <= base.Width)))
                {
                    Control control = this.dockExtender_0.FindDockHost(this, position);
                    if (control == null)
                    {
                        this.dockExtender_0.Overlay.Hide();
                    }
                    else
                    {
                        this.method_3(control, position);
                    }
                    base.OnMove(eventArgs_0);
                }
            }
        }

        protected override void OnResize(EventArgs eventArgs_0)
        {
            base.OnResize(eventArgs_0);
        }

        protected override void OnResizeBegin(EventArgs eventArgs_0)
        {
            base.OnResizeBegin(eventArgs_0);
        }

        protected override void OnResizeEnd(EventArgs eventArgs_0)
        {
            if (this.dockExtender_0.Overlay.Visible && (this.dockExtender_0.Overlay.DockHostControl != null))
            {
                this.dockState_0.OrgDockingParent = this.dockExtender_0.Overlay.DockHostControl;
                this.dockState_0.OrgBounds = this.dockState_0.Container.RectangleToClient(this.dockExtender_0.Overlay.Bounds);
                this.dockState_0.OrgDockStyle = this.dockExtender_0.Overlay.Dock;
                this.dockExtender_0.Overlay.Hide();
                this.method_7();
            }
            this.dockExtender_0.Overlay.DockHostControl = null;
            this.dockExtender_0.Overlay.Hide();
            base.OnResizeEnd(eventArgs_0);
        }

        [DllImport("User32.dll")]
        private static extern int SendMessage(int int_0, int int_1, int int_2, int int_3);
        public void Show()
        {
            if (!(base.Visible || !this.bool_1))
            {
                base.Show(this.dockState_0.OrgDockHost);
            }
            this.dockState_0.Container.Show();
        }

        public void Show(IWin32Window iwin32Window_0)
        {
            this.Show();
        }

        protected override void WndProc(ref Message message_0)
        {
            if (message_0.Msg == 0xa3)
            {
                this.method_7();
            }
            base.WndProc(ref message_0);
        }

        public bool DockOnHostOnly
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool DockOnInside
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        internal DockState DockState
        {
            get
            {
                return this.dockState_0;
            }
        }

        public bool IsFloating
        {
            get
            {
                return this.bool_1;
            }
        }
    }
}

