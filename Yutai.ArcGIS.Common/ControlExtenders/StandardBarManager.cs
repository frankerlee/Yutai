using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    public class StandardBarManager : ToolStripContainer
    {
        private ContextMenuStrip contextMenuStrip_0 = new ContextMenuStrip();
        private DockExtender dockExtender_0;
        private IContainer icontainer_0 = null;
        private IList<ToolStrip> ilist_0 = new List<ToolStrip>();
        private ToolStrip toolStrip_0 = null;

        public event ToolStripItemClickedEventHandler ItemClicked;

        public StandardBarManager()
        {
            this.method_7();
            this.dockExtender_0 = new DockExtender(this);
            this.contextMenuStrip_0.Items.Add("自定义");
            this.contextMenuStrip_0.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip_0_ItemClicked);
            base.TopToolStripPanel.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            base.RightToolStripPanel.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            base.LeftToolStripPanel.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            base.BottomToolStripPanel.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            if (base.ContentPanel != null)
            {
                base.ContentPanel.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            }
        }

        public void Add(ToolStrip toolStrip_1)
        {
            toolStrip_1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            this.ilist_0.Add(toolStrip_1);
            base.TopToolStripPanel.Controls.Add(toolStrip_1);
        }

        public void Add(ToolStrip toolStrip_1, BarDockStyle barDockStyle_0)
        {
            toolStrip_1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            this.ilist_0.Add(toolStrip_1);
            if (toolStrip_1 != this.toolStrip_0)
            {
                IFloaty floaty = this.dockExtender_0.Attach(toolStrip_1, toolStrip_1, null);
                floaty.Docking += new EventHandler(this.method_2);
                ToolStripMenuItem item = this.method_1(toolStrip_1);
                item.Tag = floaty;
                this.contextMenuStrip_0.Items.Insert(this.contextMenuStrip_0.Items.Count - 1, item);
            }
            switch (barDockStyle_0)
            {
                case BarDockStyle.Top:
                    base.TopToolStripPanel.Controls.Add(toolStrip_1);
                    break;

                case BarDockStyle.Bottom:
                    base.BottomToolStripPanel.Controls.Add(toolStrip_1);
                    break;

                case BarDockStyle.Right:
                    base.RightToolStripPanel.Controls.Add(toolStrip_1);
                    break;

                case BarDockStyle.Left:
                    base.LeftToolStripPanel.Controls.Add(toolStrip_1);
                    break;
            }
        }

        private void contextMenuStrip_0_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag is Floaty)
            {
                Floaty tag = e.ClickedItem.Tag as Floaty;
                bool flag = false;
                if (tag.IsFloating)
                {
                    if (flag = tag.Visible)
                    {
                        if (!this.method_0())
                        {
                            return;
                        }
                        (e.ClickedItem.Tag as Floaty).Hide();
                    }
                    else
                    {
                        (e.ClickedItem.Tag as Floaty).Show();
                    }
                }
                else
                {
                    if ((flag = tag.DockState.Handle.Visible) && !this.method_0())
                    {
                        return;
                    }
                    tag.DockState.Handle.Visible = !flag;
                }
                (e.ClickedItem as ToolStripMenuItem).Checked = !flag;
            }
            else if (e.ClickedItem.Text == "自定义")
            {
                new CustomizationForm { StandardBarManager = this }.ShowDialog();
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public ToolStrip GetBar(int int_0)
        {
            return this.ilist_0[int_0];
        }

        private bool method_0()
        {
            int num = 0;
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                if (this.ilist_0[i].Visible && (++num > 1))
                {
                    return true;
                }
            }
            return false;
        }

        private ToolStripMenuItem method_1(ToolStrip toolStrip_1)
        {
            return new ToolStripMenuItem { Name = toolStrip_1.Name, Text = toolStrip_1.Text, Checked = toolStrip_1.Visible };
        }

        private void method_2(object sender, EventArgs e)
        {
        }

        private void method_3(object sender, ToolStripItemClickedEventArgs e)
        {
            if (this.ItemClicked != null)
            {
                this.ItemClicked(this, e);
            }
        }

        private void method_4()
        {
        }

        private void method_5()
        {
            for (int i = 0; i < this.contextMenuStrip_0.Items.Count; i++)
            {
                Floaty tag = this.contextMenuStrip_0.Items[i].Tag as Floaty;
                if (tag != null)
                {
                    bool visible;
                    if (tag.IsFloating)
                    {
                        visible = tag.Visible;
                    }
                    else
                    {
                        visible = tag.DockState.Handle.Visible;
                    }
                    (this.contextMenuStrip_0.Items[i] as ToolStripMenuItem).Checked = visible;
                }
            }
        }

        private void method_6(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.method_5();
                this.contextMenuStrip_0.Show(this, e.X, e.Y);
            }
        }

        private void method_7()
        {
            base.SuspendLayout();
            base.BottomToolStripPanel.Dock = DockStyle.Bottom;
            base.BottomToolStripPanel.Location = new Point(0, 0xaf);
            base.BottomToolStripPanel.Name = "";
            base.BottomToolStripPanel.Orientation = Orientation.Horizontal;
            base.BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            base.BottomToolStripPanel.Size = new Size(150, 0);
            base.ContentPanel.Size = new Size(150, 0xaf);
            base.LeftToolStripPanel.Dock = DockStyle.Left;
            base.LeftToolStripPanel.Location = new Point(0, 0);
            base.LeftToolStripPanel.Name = "";
            base.LeftToolStripPanel.Orientation = Orientation.Vertical;
            base.LeftToolStripPanel.RowMargin = new Padding(0, 3, 0, 0);
            base.LeftToolStripPanel.Size = new Size(0, 0xaf);
            base.RightToolStripPanel.Dock = DockStyle.Right;
            base.RightToolStripPanel.Location = new Point(150, 0);
            base.RightToolStripPanel.Name = "";
            base.RightToolStripPanel.Orientation = Orientation.Vertical;
            base.RightToolStripPanel.RowMargin = new Padding(0, 3, 0, 0);
            base.RightToolStripPanel.Size = new Size(0, 0xaf);
            base.TopToolStripPanel.Dock = DockStyle.Top;
            base.TopToolStripPanel.Location = new Point(0, 0);
            base.TopToolStripPanel.Name = "";
            base.TopToolStripPanel.Orientation = Orientation.Horizontal;
            base.TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            base.TopToolStripPanel.Size = new Size(150, 0);
            base.TopToolStripPanel.MouseUp += new MouseEventHandler(this.method_6);
            base.MouseUp += new MouseEventHandler(this.StandardBarManager_MouseUp);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void Remove(ToolStrip toolStrip_1)
        {
            this.ilist_0.Remove(toolStrip_1);
        }

        private void StandardBarManager_MouseUp(object sender, MouseEventArgs e)
        {
        }

        public int BarCount
        {
            get
            {
                return this.ilist_0.Count;
            }
        }

        public ToolStrip MainMenu
        {
            get
            {
                if (this.toolStrip_0 != null)
                {
                    return this.toolStrip_0;
                }
                return null;
            }
            set
            {
                this.toolStrip_0 = value;
            }
        }

        public enum BarDockStyle
        {
            Top,
            Bottom,
            Right,
            Left,
            Folat
        }
    }
}

