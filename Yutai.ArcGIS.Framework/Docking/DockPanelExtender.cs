using System;
using System.Drawing;

namespace Yutai.ArcGIS.Framework.Docking
{
    public sealed class DockPanelExtender
    {
        private IAutoHideStripFactory m_autoHideStripFactory = null;
        private IDockPaneCaptionFactory m_dockPaneCaptionFactory = null;
        private IDockPaneFactory m_dockPaneFactory = null;
        private DockPanel m_dockPanel;
        private IDockPaneStripFactory m_dockPaneStripFactory = null;
        private IFloatWindowFactory m_floatWindowFactory = null;

        internal DockPanelExtender(DockPanel dockPanel)
        {
            this.m_dockPanel = dockPanel;
        }

        public IAutoHideStripFactory AutoHideStripFactory
        {
            get
            {
                if (this.m_autoHideStripFactory == null)
                {
                    this.m_autoHideStripFactory = new DefaultAutoHideStripFactory();
                }
                return this.m_autoHideStripFactory;
            }
            set
            {
                if (this.DockPanel.Contents.Count > 0)
                {
                    throw new InvalidOperationException();
                }
                if (this.m_autoHideStripFactory != value)
                {
                    this.m_autoHideStripFactory = value;
                }
            }
        }

        public IDockPaneCaptionFactory DockPaneCaptionFactory
        {
            get
            {
                if (this.m_dockPaneCaptionFactory == null)
                {
                    this.m_dockPaneCaptionFactory = new DefaultDockPaneCaptionFactory();
                }
                return this.m_dockPaneCaptionFactory;
            }
            set
            {
                if (this.DockPanel.Panes.Count > 0)
                {
                    throw new InvalidOperationException();
                }
                this.m_dockPaneCaptionFactory = value;
            }
        }

        public IDockPaneFactory DockPaneFactory
        {
            get
            {
                if (this.m_dockPaneFactory == null)
                {
                    this.m_dockPaneFactory = new DefaultDockPaneFactory();
                }
                return this.m_dockPaneFactory;
            }
            set
            {
                if (this.DockPanel.Panes.Count > 0)
                {
                    throw new InvalidOperationException();
                }
                this.m_dockPaneFactory = value;
            }
        }

        private DockPanel DockPanel
        {
            get { return this.m_dockPanel; }
        }

        public IDockPaneStripFactory DockPaneStripFactory
        {
            get
            {
                if (this.m_dockPaneStripFactory == null)
                {
                    this.m_dockPaneStripFactory = new DefaultDockPaneStripFactory();
                }
                return this.m_dockPaneStripFactory;
            }
            set
            {
                if (this.DockPanel.Contents.Count > 0)
                {
                    throw new InvalidOperationException();
                }
                this.m_dockPaneStripFactory = value;
            }
        }

        public IFloatWindowFactory FloatWindowFactory
        {
            get
            {
                if (this.m_floatWindowFactory == null)
                {
                    this.m_floatWindowFactory = new DefaultFloatWindowFactory();
                }
                return this.m_floatWindowFactory;
            }
            set
            {
                if (this.DockPanel.FloatWindows.Count > 0)
                {
                    throw new InvalidOperationException();
                }
                this.m_floatWindowFactory = value;
            }
        }

        private class DefaultAutoHideStripFactory : DockPanelExtender.IAutoHideStripFactory
        {
            public AutoHideStripBase CreateAutoHideStrip(DockPanel panel)
            {
                return new VS2005AutoHideStrip(panel);
            }
        }

        private class DefaultDockPaneCaptionFactory : DockPanelExtender.IDockPaneCaptionFactory
        {
            public DockPaneCaptionBase CreateDockPaneCaption(DockPane pane)
            {
                return new VS2005DockPaneCaption(pane);
            }
        }

        private class DefaultDockPaneFactory : DockPanelExtender.IDockPaneFactory
        {
            public DockPane CreateDockPane(IDockContent content, DockState visibleState, bool show)
            {
                return new DockPane(content, visibleState, show);
            }

            public DockPane CreateDockPane(IDockContent content, FloatWindow floatWindow, bool show)
            {
                return new DockPane(content, floatWindow, show);
            }

            public DockPane CreateDockPane(IDockContent content, Rectangle floatWindowBounds, bool show)
            {
                return new DockPane(content, floatWindowBounds, show);
            }

            public DockPane CreateDockPane(IDockContent content, DockPane prevPane, DockAlignment alignment,
                double proportion, bool show)
            {
                return new DockPane(content, prevPane, alignment, proportion, show);
            }
        }

        private class DefaultDockPaneStripFactory : DockPanelExtender.IDockPaneStripFactory
        {
            public DockPaneStripBase CreateDockPaneStrip(DockPane pane)
            {
                return new VS2005DockPaneStrip(pane);
            }
        }

        private class DefaultFloatWindowFactory : DockPanelExtender.IFloatWindowFactory
        {
            public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
            {
                return new FloatWindow(dockPanel, pane);
            }

            public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
            {
                return new FloatWindow(dockPanel, pane, bounds);
            }
        }

        public interface IAutoHideStripFactory
        {
            AutoHideStripBase CreateAutoHideStrip(DockPanel panel);
        }

        public interface IDockPaneCaptionFactory
        {
            DockPaneCaptionBase CreateDockPaneCaption(DockPane pane);
        }

        public interface IDockPaneFactory
        {
            DockPane CreateDockPane(IDockContent content, DockState visibleState, bool show);
            DockPane CreateDockPane(IDockContent content, FloatWindow floatWindow, bool show);
            DockPane CreateDockPane(IDockContent content, Rectangle floatWindowBounds, bool show);

            DockPane CreateDockPane(IDockContent content, DockPane previousPane, DockAlignment alignment,
                double proportion, bool show);
        }

        public interface IDockPaneStripFactory
        {
            DockPaneStripBase CreateDockPaneStrip(DockPane pane);
        }

        public interface IFloatWindowFactory
        {
            FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane);
            FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds);
        }
    }
}