using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using Yutai.ArcGIS.Common.BaseClasses;
using DockingStyle = DevExpress.XtraBars.Docking.DockingStyle;


namespace Yutai.ArcGIS.Framework
{
    public class DockManagerWrapImp : IDockManagerWrap
    {
        private DockManager dockManager_0 = null;

        private void dockManager_0_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (e.Visibility == DockVisibility.Hidden)
            {
                try
                {
                    Control control = e.Panel.Controls[0];
                    control = control.Controls[0];
                    if (control is IDockContentEvents)
                    {
                        (control as IDockContentEvents).DockContentVisibleChanges();
                    }
                }
                catch
                {
                }
            }
        }

        public void DockWindows(object object_0, Form form_0, Bitmap bitmap_0)
        {
            if (form_0 != null)
            {
                form_0.SuspendLayout();
            }
            if (object_0 is IDockContent)
            {
                DockPanel panel = this.dockManager_0.Panels[(object_0 as IDockContent).Name];
                DockPanel panel2 = null;
                if (panel != null)
                {
                    if (panel.ParentPanel != null)
                    {
                        if (!panel.ParentPanel.Visible)
                        {
                            panel.Show();
                        }
                    }
                    else if (!panel.Visible)
                    {
                        if ((panel.Dock == DockingStyle.Float) || (panel.Dock == DockingStyle.Fill))
                        {
                            if (panel.SavedParent != null)
                            {
                                if (!panel.SavedParent.Visible)
                                {
                                    panel.Show();
                                }
                                else
                                {
                                    panel.Show();
                                }
                            }
                            else
                            {
                                panel.Show();
                            }
                        }
                        else if (panel.SavedParent != null)
                        {
                            panel.Show();
                        }
                        else if (!this.method_0(panel))
                        {
                            panel.Show();
                        }
                    }
                }
                else
                {
                    DockingStyle @float;
                    DevExpress.XtraBars.Docking.DockingStyle pStyle =
                        (DevExpress.XtraBars.Docking.DockingStyle)
                        ((int) (object_0 as IDockContent).DefaultDockingStyle);
                    switch (pStyle)
                    {
                        case DockingStyle.Float:
                            @float = DockingStyle.Float;
                            break;

                        case DockingStyle.Bottom:
                            @float = DockingStyle.Bottom;
                            break;

                        case DockingStyle.Left:
                            @float = DockingStyle.Left;
                            break;

                        case  DockingStyle.Right:
                            @float = DockingStyle.Right;
                            break;

                        case  DockingStyle.Fill:
                            @float = DockingStyle.Fill;
                            break;

                        default:
                            @float = DockingStyle.Top;
                            break;
                    }
                    panel = this.dockManager_0.AddPanel(@float);
                    panel.Name = (object_0 as IDockContent).Name;
                    panel.Text = (object_0 as IDockContent).Text;
                    panel.Width = (object_0 as IDockContent).Width;
                    try
                    {
                        (object_0 as Control).Dock = DockStyle.Fill;
                        panel.Controls.Add(object_0 as Control);
                    }
                    catch (Exception exception)
                    {
                        exception.ToString();
                    }
                    if ((DevExpress.XtraBars.Docking.DockingStyle)
                        ((int)(object_0 as IDockContent).DefaultDockingStyle) == DockingStyle.Float)
                    {
                        panel.FloatSize = (object_0 as Control).Size;
                        panel.Dock = @float;
                        int x = Screen.PrimaryScreen.WorkingArea.X + (Screen.PrimaryScreen.WorkingArea.Width / 2);
                        int y = (Screen.PrimaryScreen.WorkingArea.Top + Screen.PrimaryScreen.WorkingArea.Bottom) / 2;
                        Point point = new Point(x, y);
                        panel.Show();
                    }
                    else
                    {
                        for (int i = 0; i < this.dockManager_0.Panels.Count; i++)
                        {
                            if ((this.dockManager_0.Panels[i] != panel) && (this.dockManager_0.Panels[i].Dock == panel.Dock))
                            {
                                panel2 = this.dockManager_0.Panels[i];
                                if (panel2.Visibility != DockVisibility.Hidden)
                                {
                                    panel.DockAsTab(panel2);
                                    break;
                                }
                            }
                        }
                    }
                }
                this.dockManager_0.ActivePanel = panel;
            }
            else if (object_0 is Form)
            {
                (object_0 as Form).MdiParent = form_0;
                (object_0 as Form).Show();
            }
            if (form_0 != null)
            {
                form_0.ResumeLayout();
            }
        }

        public void HideDockWindow(IDockContent idockContent_0)
        {
            DockPanel panel = this.dockManager_0.Panels[idockContent_0.Name];
            if (panel != null)
            {
                panel.Hide();
            }
        }

        private bool method_0(DockPanel dockPanel_0)
        {
            for (int i = 0; i < this.dockManager_0.Panels.Count; i++)
            {
                if ((this.dockManager_0.Panels[i] != dockPanel_0) && (this.dockManager_0.Panels[i].Dock == dockPanel_0.Dock))
                {
                    DockPanel panel = this.dockManager_0.Panels[i];
                    if (panel.Visibility != DockVisibility.Hidden)
                    {
                        dockPanel_0.Visibility = DockVisibility.Visible;
                        dockPanel_0.DockAsTab(panel);
                        return true;
                    }
                }
            }
            return false;
        }

        public object DockManager
        {
            set
            {
                this.dockManager_0 = value as DockManager;
                this.dockManager_0.VisibilityChanged += new VisibilityChangedEventHandler(this.dockManager_0_VisibilityChanged);
            }
        }
    }
}

