using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    [ToolboxItem(false)]
    public class DockPane : UserControl, IDockDragSource, IDragSource
    {
        private static readonly object DockStateChangedEvent = new object();
        private static readonly object IsActivatedChangedEvent = new object();
        private static readonly object IsActiveDocumentPaneChangedEvent = new object();
        private IDockContent m_activeContent;
        private bool m_allowDockDragAndDrop;
        private IDisposable m_autoHidePane;
        private object m_autoHideTabs;
        private DockPaneCaptionBase m_captionControl;
        private DockContentCollection m_contents;
        private int m_countRefreshStateChange;
        private DockContentCollection m_displayingContents;
        private DockPanel m_dockPanel;
        private DockState m_dockState;
        private bool m_isActivated;
        private bool m_isActiveDocumentPane;
        private bool m_isFloat;
        private bool m_isHidden;
        private NestedDockingStatus m_nestedDockingStatus;
        private SplitterControl m_splitter;
        private DockPaneStripBase m_tabStripControl;

        public event EventHandler DockStateChanged
        {
            add
            {
                base.Events.AddHandler(DockStateChangedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(DockStateChangedEvent, value);
            }
        }

        public event EventHandler IsActivatedChanged
        {
            add
            {
                base.Events.AddHandler(IsActivatedChangedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(IsActivatedChangedEvent, value);
            }
        }

        public event EventHandler IsActiveDocumentPaneChanged
        {
            add
            {
                base.Events.AddHandler(IsActiveDocumentPaneChangedEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(IsActiveDocumentPaneChangedEvent, value);
            }
        }

        protected internal DockPane(IDockContent content, DockState visibleState, bool show)
        {
            this.m_activeContent = null;
            this.m_allowDockDragAndDrop = true;
            this.m_autoHidePane = null;
            this.m_autoHideTabs = null;
            this.m_isActivated = false;
            this.m_isActiveDocumentPane = false;
            this.m_isHidden = true;
            this.m_dockState = DockState.Unknown;
            this.m_countRefreshStateChange = 0;
            this.InternalConstruct(content, visibleState, false, Rectangle.Empty, null, DockAlignment.Right, 0.5, show);
        }

        protected internal DockPane(IDockContent content, FloatWindow floatWindow, bool show)
        {
            this.m_activeContent = null;
            this.m_allowDockDragAndDrop = true;
            this.m_autoHidePane = null;
            this.m_autoHideTabs = null;
            this.m_isActivated = false;
            this.m_isActiveDocumentPane = false;
            this.m_isHidden = true;
            this.m_dockState = DockState.Unknown;
            this.m_countRefreshStateChange = 0;
            if (floatWindow == null)
            {
                throw new ArgumentNullException("floatWindow");
            }
            this.InternalConstruct(content, DockState.Float, false, Rectangle.Empty, floatWindow.NestedPanes.GetDefaultPreviousPane(this), DockAlignment.Right, 0.5, show);
        }

        protected internal DockPane(IDockContent content, Rectangle floatWindowBounds, bool show)
        {
            this.m_activeContent = null;
            this.m_allowDockDragAndDrop = true;
            this.m_autoHidePane = null;
            this.m_autoHideTabs = null;
            this.m_isActivated = false;
            this.m_isActiveDocumentPane = false;
            this.m_isHidden = true;
            this.m_dockState = DockState.Unknown;
            this.m_countRefreshStateChange = 0;
            this.InternalConstruct(content, DockState.Float, true, floatWindowBounds, null, DockAlignment.Right, 0.5, show);
        }

        protected internal DockPane(IDockContent content, DockPane previousPane, DockAlignment alignment, double proportion, bool show)
        {
            this.m_activeContent = null;
            this.m_allowDockDragAndDrop = true;
            this.m_autoHidePane = null;
            this.m_autoHideTabs = null;
            this.m_isActivated = false;
            this.m_isActiveDocumentPane = false;
            this.m_isHidden = true;
            this.m_dockState = DockState.Unknown;
            this.m_countRefreshStateChange = 0;
            if (previousPane == null)
            {
                throw new ArgumentNullException("previousPane");
            }
            this.InternalConstruct(content, previousPane.DockState, false, Rectangle.Empty, previousPane, alignment, proportion, show);
        }

        public void Activate()
        {
            if (DockHelper.IsDockStateAutoHide(this.DockState) && (this.DockPanel.ActiveAutoHideContent != this.ActiveContent))
            {
                this.DockPanel.ActiveAutoHideContent = this.ActiveContent;
            }
            else if (!(this.IsActivated || (this.ActiveContent == null)))
            {
                this.ActiveContent.DockHandler.Activate();
            }
        }

        internal void AddContent(IDockContent content)
        {
            if (!this.Contents.Contains(content))
            {
                this.Contents.Add(content);
            }
        }

        internal void Close()
        {
            base.Dispose();
        }

        public void CloseActiveContent()
        {
            this.CloseContent(this.ActiveContent);
        }

        internal void CloseContent(IDockContent content)
        {
            DockPanel dockPanel = this.DockPanel;
            dockPanel.SuspendLayout(true);
            if ((content != null) && content.DockHandler.CloseButton)
            {
                if (content.DockHandler.HideOnClose)
                {
                    content.DockHandler.Hide();
                }
                else
                {
                    content.DockHandler.Close();
                }
                dockPanel.ResumeLayout(true, true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_dockState = DockState.Unknown;
                if (this.NestedPanesContainer != null)
                {
                    this.NestedPanesContainer.NestedPanes.Remove(this);
                }
                if (this.DockPanel != null)
                {
                    this.DockPanel.RemovePane(this);
                    this.m_dockPanel = null;
                }
                this.Splitter.Dispose();
                if (this.m_autoHidePane != null)
                {
                    this.m_autoHidePane.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public DockPane DockTo(INestedPanesContainer container)
        {
            DockAlignment bottom;
            if (container == null)
            {
                throw new InvalidOperationException(Strings.DockPane_DockTo_NullContainer);
            }
            if ((container.DockState == DockState.DockLeft) || (container.DockState == DockState.DockRight))
            {
                bottom = DockAlignment.Bottom;
            }
            else
            {
                bottom = DockAlignment.Right;
            }
            return this.DockTo(container, container.NestedPanes.GetDefaultPreviousPane(this), bottom, 0.5);
        }

        public void DockTo(DockPanel panel, DockStyle dockStyle)
        {
            if (panel != this.DockPanel)
            {
                throw new ArgumentException(Strings.IDockDragSource_DockTo_InvalidPanel, "panel");
            }
            if (dockStyle == DockStyle.Top)
            {
                this.DockState = DockState.DockTop;
            }
            else if (dockStyle == DockStyle.Bottom)
            {
                this.DockState = DockState.DockBottom;
            }
            else if (dockStyle == DockStyle.Left)
            {
                this.DockState = DockState.DockLeft;
            }
            else if (dockStyle == DockStyle.Right)
            {
                this.DockState = DockState.DockRight;
            }
            else if (dockStyle == DockStyle.Fill)
            {
                this.DockState = DockState.Document;
            }
        }

        public void DockTo(DockPane pane, DockStyle dockStyle, int contentIndex)
        {
            if (dockStyle == DockStyle.Fill)
            {
                IDockContent activeContent = this.ActiveContent;
                for (int i = this.Contents.Count - 1; i >= 0; i--)
                {
                    IDockContent content = this.Contents[i];
                    content.DockHandler.Pane = pane;
                    if (contentIndex != -1)
                    {
                        pane.SetContentIndex(content, contentIndex);
                    }
                }
                pane.ActiveContent = activeContent;
            }
            else
            {
                if (dockStyle == DockStyle.Left)
                {
                    this.DockTo(pane.NestedPanesContainer, pane, DockAlignment.Left, 0.5);
                }
                else if (dockStyle == DockStyle.Right)
                {
                    this.DockTo(pane.NestedPanesContainer, pane, DockAlignment.Right, 0.5);
                }
                else if (dockStyle == DockStyle.Top)
                {
                    this.DockTo(pane.NestedPanesContainer, pane, DockAlignment.Top, 0.5);
                }
                else if (dockStyle == DockStyle.Bottom)
                {
                    this.DockTo(pane.NestedPanesContainer, pane, DockAlignment.Bottom, 0.5);
                }
                this.DockState = pane.DockState;
            }
        }

        public DockPane DockTo(INestedPanesContainer container, DockPane previousPane, DockAlignment alignment, double proportion)
        {
            DockPane pane;
            if (container == null)
            {
                throw new InvalidOperationException(Strings.DockPane_DockTo_NullContainer);
            }
            if (container.IsFloat == this.IsFloat)
            {
                this.InternalAddToDockList(container, previousPane, alignment, proportion);
                return this;
            }
            if (this.GetFirstContent(container.DockState) == null)
            {
                return null;
            }
            this.DockPanel.DummyContent.DockPanel = this.DockPanel;
            if (container.IsFloat)
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.DockPanel.DummyContent, (FloatWindow) container, true);
            }
            else
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.DockPanel.DummyContent, container.DockState, true);
            }
            pane.DockTo(container, previousPane, alignment, proportion);
            this.SetVisibleContentsToPane(pane);
            this.DockPanel.DummyContent.DockPanel = null;
            return pane;
        }

        public DockPane Float()
        {
            this.DockPanel.SuspendLayout(true);
            IDockContent activeContent = this.ActiveContent;
            DockPane floatPaneFromContents = this.GetFloatPaneFromContents();
            if (floatPaneFromContents == null)
            {
                IDockContent firstContent = this.GetFirstContent(DockState.Float);
                if (firstContent == null)
                {
                    this.DockPanel.ResumeLayout(true, true);
                    return null;
                }
                floatPaneFromContents = this.DockPanel.DockPaneFactory.CreateDockPane(firstContent, DockState.Float, true);
            }
            this.SetVisibleContentsToPane(floatPaneFromContents, activeContent);
            this.DockPanel.ResumeLayout(true, true);
            return floatPaneFromContents;
        }

        public void FloatAt(Rectangle floatWindowBounds)
        {
            if ((this.FloatWindow == null) || (this.FloatWindow.NestedPanes.Count != 1))
            {
                this.FloatWindow = this.DockPanel.FloatWindowFactory.CreateFloatWindow(this.DockPanel, this, floatWindowBounds);
            }
            else
            {
                this.FloatWindow.Bounds = floatWindowBounds;
            }
            this.DockState = DockState.Float;
        }

        private IDockContent GetFirstContent(DockState dockState)
        {
            for (int i = 0; i < this.DisplayingContents.Count; i++)
            {
                IDockContent content = this.DisplayingContents[i];
                if (content.DockHandler.IsDockStateValid(dockState))
                {
                    return content;
                }
            }
            return null;
        }

        private DockPane GetFloatPaneFromContents()
        {
            DockPane floatPane = null;
            for (int i = 0; i < this.DisplayingContents.Count; i++)
            {
                IDockContent content = this.DisplayingContents[i];
                if (content.DockHandler.IsDockStateValid(DockState.Float))
                {
                    if ((floatPane != null) && (content.DockHandler.FloatPane != floatPane))
                    {
                        return null;
                    }
                    floatPane = content.DockHandler.FloatPane;
                }
            }
            return floatPane;
        }

        private IDockContent GetFocusedContent()
        {
            foreach (IDockContent content2 in this.Contents)
            {
                if (content2.DockHandler.Form.ContainsFocus)
                {
                    return content2;
                }
            }
            return null;
        }

        private HitTestResult GetHitTest(Point ptMouse)
        {
            Point pt = base.PointToClient(ptMouse);
            if (this.CaptionRectangle.Contains(pt))
            {
                return new HitTestResult(HitTestArea.Caption, -1);
            }
            if (this.ContentRectangle.Contains(pt))
            {
                return new HitTestResult(HitTestArea.Content, -1);
            }
            if (this.TabStripRectangle.Contains(pt))
            {
                return new HitTestResult(HitTestArea.TabStrip, this.TabStripControl.HitTest(this.TabStripControl.PointToClient(ptMouse)));
            }
            return new HitTestResult(HitTestArea.None, -1);
        }

        private void InternalAddToDockList(INestedPanesContainer container, DockPane prevPane, DockAlignment alignment, double proportion)
        {
            if ((container.DockState == DockState.Float) != this.IsFloat)
            {
                throw new InvalidOperationException(Strings.DockPane_DockTo_InvalidContainer);
            }
            int count = container.NestedPanes.Count;
            if (container.NestedPanes.Contains(this))
            {
                count--;
            }
            if ((prevPane == null) && (count > 0))
            {
                throw new InvalidOperationException(Strings.DockPane_DockTo_NullPrevPane);
            }
            if (!((prevPane == null) || container.NestedPanes.Contains(prevPane)))
            {
                throw new InvalidOperationException(Strings.DockPane_DockTo_NoPrevPane);
            }
            if (prevPane == this)
            {
                throw new InvalidOperationException(Strings.DockPane_DockTo_SelfPrevPane);
            }
            INestedPanesContainer nestedPanesContainer = this.NestedPanesContainer;
            DockState dockState = this.DockState;
            container.NestedPanes.Add(this);
            this.NestedDockingStatus.SetStatus(container.NestedPanes, prevPane, alignment, proportion);
            if (DockHelper.IsDockWindowState(this.DockState))
            {
                this.m_dockState = container.DockState;
            }
            this.RefreshStateChange(nestedPanesContainer, dockState);
        }

        private void InternalConstruct(IDockContent content, DockState dockState, bool flagBounds, Rectangle floatWindowBounds, DockPane prevPane, DockAlignment alignment, double proportion, bool show)
        {
            if ((dockState == DockState.Hidden) || (dockState == DockState.Unknown))
            {
                throw new ArgumentException(Strings.DockPane_SetDockState_InvalidState);
            }
            if (content == null)
            {
                throw new ArgumentNullException(Strings.DockPane_Constructor_NullContent);
            }
            if (content.DockHandler.DockPanel == null)
            {
                throw new ArgumentException(Strings.DockPane_Constructor_NullDockPanel);
            }
            base.SuspendLayout();
            base.SetStyle(ControlStyles.Selectable, false);
            this.m_isFloat = dockState == DockState.Float;
            this.m_contents = new DockContentCollection();
            this.m_displayingContents = new DockContentCollection(this);
            this.m_dockPanel = content.DockHandler.DockPanel;
            this.m_dockPanel.AddPane(this);
            this.m_splitter = new SplitterControl(this);
            this.m_nestedDockingStatus = new NestedDockingStatus(this);
            this.m_captionControl = this.DockPanel.DockPaneCaptionFactory.CreateDockPaneCaption(this);
            this.m_tabStripControl = this.DockPanel.DockPaneStripFactory.CreateDockPaneStrip(this);
            base.Controls.AddRange(new Control[] { this.m_captionControl, this.m_tabStripControl });
            this.DockPanel.SuspendLayout(true);
            if (flagBounds)
            {
                this.FloatWindow = this.DockPanel.FloatWindowFactory.CreateFloatWindow(this.DockPanel, this, floatWindowBounds);
            }
            else if (prevPane != null)
            {
                this.DockTo(prevPane.NestedPanesContainer, prevPane, alignment, proportion);
            }
            this.SetDockState(dockState);
            if (show)
            {
                content.DockHandler.Pane = this;
            }
            else if (this.IsFloat)
            {
                content.DockHandler.FloatPane = this;
            }
            else
            {
                content.DockHandler.PanelPane = this;
            }
            base.ResumeLayout();
            this.DockPanel.ResumeLayout(true, true);
        }

        private void InternalSetDockState(DockState value)
        {
            if (this.m_dockState != value)
            {
                DockState dockState = this.m_dockState;
                INestedPanesContainer nestedPanesContainer = this.NestedPanesContainer;
                this.m_dockState = value;
                this.SuspendRefreshStateChange();
                IDockContent focusedContent = this.GetFocusedContent();
                if (focusedContent != null)
                {
                    this.DockPanel.SaveFocus();
                }
                if (!this.IsFloat)
                {
                    this.DockWindow = this.DockPanel.DockWindows[this.DockState];
                }
                else if (this.FloatWindow == null)
                {
                    this.FloatWindow = this.DockPanel.FloatWindowFactory.CreateFloatWindow(this.DockPanel, this);
                }
                if (focusedContent != null)
                {
                    this.DockPanel.ContentFocusManager.Activate(focusedContent);
                }
                this.ResumeRefreshStateChange(nestedPanesContainer, dockState);
            }
        }

        public bool IsDockStateValid(DockState dockState)
        {
            foreach (IDockContent content in this.Contents)
            {
                if (!content.DockHandler.IsDockStateValid(dockState))
                {
                    return false;
                }
            }
            return true;
        }

        Rectangle IDockDragSource.BeginDrag(Point ptMouse)
        {
            Size defaultFloatWindowSize;
            Point location = base.PointToScreen(new Point(0, 0));
            DockPane floatPane = this.ActiveContent.DockHandler.FloatPane;
            if (((this.DockState == DockState.Float) || (floatPane == null)) || (floatPane.FloatWindow.NestedPanes.Count != 1))
            {
                defaultFloatWindowSize = this.DockPanel.DefaultFloatWindowSize;
            }
            else
            {
                defaultFloatWindowSize = floatPane.FloatWindow.Size;
            }
            if (ptMouse.X > (location.X + defaultFloatWindowSize.Width))
            {
                location.X += (ptMouse.X - (location.X + defaultFloatWindowSize.Width)) + 4;
            }
            return new Rectangle(location, defaultFloatWindowSize);
        }

        bool IDockDragSource.CanDockTo(DockPane pane)
        {
            if (!this.IsDockStateValid(pane.DockState))
            {
                return false;
            }
            if (pane == this)
            {
                return false;
            }
            return true;
        }

        bool IDockDragSource.IsDockStateValid(DockState dockState)
        {
            return this.IsDockStateValid(dockState);
        }

        protected virtual void OnDockStateChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[DockStateChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnIsActivatedChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[IsActivatedChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnIsActiveDocumentPaneChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[IsActiveDocumentPaneChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.SetIsHidden(this.DisplayingContents.Count == 0);
            if (!this.IsHidden)
            {
                this.CaptionControl.Bounds = this.CaptionRectangle;
                this.TabStripControl.Bounds = this.TabStripRectangle;
                this.SetContentBounds();
                foreach (IDockContent content in this.Contents)
                {
                    if (this.DisplayingContents.Contains(content) && (content.DockHandler.FlagClipWindow && content.DockHandler.Form.Visible))
                    {
                        content.DockHandler.FlagClipWindow = false;
                    }
                }
            }
            base.OnLayout(levent);
        }

        internal void RefreshChanges()
        {
            this.RefreshChanges(true);
        }

        private void RefreshChanges(bool performLayout)
        {
            if (!base.IsDisposed)
            {
                this.CaptionControl.RefreshChanges();
                this.TabStripControl.RefreshChanges();
                if (this.DockState == DockState.Float)
                {
                    this.FloatWindow.RefreshChanges();
                }
                if (DockHelper.IsDockStateAutoHide(this.DockState) && (this.DockPanel != null))
                {
                    this.DockPanel.RefreshAutoHideStrip();
                    this.DockPanel.PerformLayout();
                }
                if (performLayout)
                {
                    base.PerformLayout();
                }
            }
        }

        private void RefreshStateChange(INestedPanesContainer oldContainer, DockState oldDockState)
        {
            lock (this)
            {
                if (this.IsRefreshStateChangeSuspended)
                {
                    return;
                }
                this.SuspendRefreshStateChange();
            }
            this.DockPanel.SuspendLayout(true);
            IDockContent focusedContent = this.GetFocusedContent();
            if (focusedContent != null)
            {
                this.DockPanel.SaveFocus();
            }
            this.SetParent();
            if (this.ActiveContent != null)
            {
                this.ActiveContent.DockHandler.SetDockState(this.ActiveContent.DockHandler.IsHidden, this.DockState, this.ActiveContent.DockHandler.Pane);
            }
            foreach (IDockContent content2 in this.Contents)
            {
                if (content2.DockHandler.Pane == this)
                {
                    content2.DockHandler.SetDockState(content2.DockHandler.IsHidden, this.DockState, content2.DockHandler.Pane);
                }
            }
            if (oldContainer != null)
            {
                Control control = (Control) oldContainer;
                if (!((oldContainer.DockState != oldDockState) || control.IsDisposed))
                {
                    control.PerformLayout();
                }
            }
            if (DockHelper.IsDockStateAutoHide(oldDockState))
            {
                this.DockPanel.RefreshActiveAutoHideContent();
            }
            if (this.NestedPanesContainer.DockState == this.DockState)
            {
                ((Control) this.NestedPanesContainer).PerformLayout();
            }
            if (DockHelper.IsDockStateAutoHide(this.DockState))
            {
                this.DockPanel.RefreshActiveAutoHideContent();
            }
            if (DockHelper.IsDockStateAutoHide(oldDockState) || DockHelper.IsDockStateAutoHide(this.DockState))
            {
                this.DockPanel.RefreshAutoHideStrip();
                this.DockPanel.PerformLayout();
            }
            this.ResumeRefreshStateChange();
            if (focusedContent != null)
            {
                focusedContent.DockHandler.Activate();
            }
            this.DockPanel.ResumeLayout(true, true);
            if (oldDockState != this.DockState)
            {
                this.OnDockStateChanged(EventArgs.Empty);
            }
        }

        internal void RemoveContent(IDockContent content)
        {
            if (this.Contents.Contains(content))
            {
                this.Contents.Remove(content);
            }
        }

        public void RestoreToPanel()
        {
            this.DockPanel.SuspendLayout(true);
            IDockContent activeContent = this.DockPanel.ActiveContent;
            for (int i = this.DisplayingContents.Count - 1; i >= 0; i--)
            {
                IDockContent content2 = this.DisplayingContents[i];
                if (content2.DockHandler.CheckDockState(false) != DockState.Unknown)
                {
                    content2.DockHandler.IsFloat = false;
                }
            }
            this.DockPanel.ResumeLayout(true, true);
        }

        private void ResumeRefreshStateChange()
        {
            this.m_countRefreshStateChange--;
            Debug.Assert(this.m_countRefreshStateChange >= 0);
            this.DockPanel.ResumeLayout(true, true);
        }

        private void ResumeRefreshStateChange(INestedPanesContainer oldContainer, DockState oldDockState)
        {
            this.ResumeRefreshStateChange();
            this.RefreshStateChange(oldContainer, oldDockState);
        }

        internal void SetContentBounds()
        {
            Rectangle contentRectangle = this.ContentRectangle;
            if ((this.DockState == DockState.Document) && (this.DockPanel.DocumentStyle == DocumentStyle.DockingMdi))
            {
                contentRectangle = this.DockPanel.RectangleToMdiClient(base.RectangleToScreen(contentRectangle));
            }
            Rectangle rectangle2 = new Rectangle(-contentRectangle.Width, contentRectangle.Y, contentRectangle.Width, contentRectangle.Height);
            foreach (IDockContent content in this.Contents)
            {
                if (content.DockHandler.Pane == this)
                {
                    if (content == this.ActiveContent)
                    {
                        content.DockHandler.Form.Bounds = contentRectangle;
                    }
                    else
                    {
                        content.DockHandler.Form.Bounds = rectangle2;
                    }
                }
            }
        }

        public void SetContentIndex(IDockContent content, int index)
        {
            int num = this.Contents.IndexOf(content);
            if (num == -1)
            {
                throw new ArgumentException(Strings.DockPane_SetContentIndex_InvalidContent);
            }
            if (((index < 0) || (index > (this.Contents.Count - 1))) && (index != -1))
            {
                throw new ArgumentOutOfRangeException(Strings.DockPane_SetContentIndex_InvalidIndex);
            }
            if ((num != index) && ((num != (this.Contents.Count - 1)) || (index != -1)))
            {
                this.Contents.Remove(content);
                if (index == -1)
                {
                    this.Contents.Add(content);
                }
                else if (num < index)
                {
                    this.Contents.AddAt(content, index - 1);
                }
                else
                {
                    this.Contents.AddAt(content, index);
                }
                this.RefreshChanges();
            }
        }

        public DockPane SetDockState(DockState value)
        {
            int num;
            IDockContent content2;
            if ((value == DockState.Unknown) || (value == DockState.Hidden))
            {
                throw new InvalidOperationException(Strings.DockPane_SetDockState_InvalidState);
            }
            if ((value == DockState.Float) == this.IsFloat)
            {
                this.InternalSetDockState(value);
                return this;
            }
            if (this.DisplayingContents.Count == 0)
            {
                return null;
            }
            IDockContent content = null;
            for (num = 0; num < this.DisplayingContents.Count; num++)
            {
                content2 = this.DisplayingContents[num];
                if (content2.DockHandler.IsDockStateValid(value))
                {
                    content = content2;
                    break;
                }
            }
            if (content == null)
            {
                return null;
            }
            content.DockHandler.DockState = value;
            DockPane pane = content.DockHandler.Pane;
            this.DockPanel.SuspendLayout(true);
            for (num = 0; num < this.DisplayingContents.Count; num++)
            {
                content2 = this.DisplayingContents[num];
                if (content2.DockHandler.IsDockStateValid(value))
                {
                    content2.DockHandler.Pane = pane;
                }
            }
            this.DockPanel.ResumeLayout(true, true);
            return pane;
        }

        internal void SetIsActivated(bool value)
        {
            if (this.m_isActivated != value)
            {
                this.m_isActivated = value;
                if (this.DockState != DockState.Document)
                {
                    this.RefreshChanges(false);
                }
                this.OnIsActivatedChanged(EventArgs.Empty);
            }
        }

        internal void SetIsActiveDocumentPane(bool value)
        {
            if (this.m_isActiveDocumentPane != value)
            {
                this.m_isActiveDocumentPane = value;
                if (this.DockState == DockState.Document)
                {
                    this.RefreshChanges();
                }
                this.OnIsActiveDocumentPaneChanged(EventArgs.Empty);
            }
        }

        private void SetIsHidden(bool value)
        {
            if (this.m_isHidden != value)
            {
                this.m_isHidden = value;
                if (DockHelper.IsDockStateAutoHide(this.DockState))
                {
                    this.DockPanel.RefreshAutoHideStrip();
                    this.DockPanel.PerformLayout();
                }
                else if (this.NestedPanesContainer != null)
                {
                    ((Control) this.NestedPanesContainer).PerformLayout();
                }
            }
        }

        public void SetNestedDockingProportion(double proportion)
        {
            this.NestedDockingStatus.SetStatus(this.NestedDockingStatus.NestedPanes, this.NestedDockingStatus.PreviousPane, this.NestedDockingStatus.Alignment, proportion);
            if (this.NestedPanesContainer != null)
            {
                ((Control) this.NestedPanesContainer).PerformLayout();
            }
        }

        private void SetParent()
        {
            if ((this.DockState == DockState.Unknown) || (this.DockState == DockState.Hidden))
            {
                this.SetParent(null);
                this.Splitter.Parent = null;
            }
            else if (this.DockState == DockState.Float)
            {
                this.SetParent(this.FloatWindow);
                this.Splitter.Parent = this.FloatWindow;
            }
            else if (DockHelper.IsDockStateAutoHide(this.DockState))
            {
                this.SetParent(this.DockPanel.AutoHideControl);
                this.Splitter.Parent = null;
            }
            else
            {
                this.SetParent(this.DockPanel.DockWindows[this.DockState]);
                this.Splitter.Parent = base.Parent;
            }
        }

        private void SetParent(Control value)
        {
            if (base.Parent != value)
            {
                IDockContent focusedContent = this.GetFocusedContent();
                if (focusedContent != null)
                {
                    this.DockPanel.SaveFocus();
                }
                base.Parent = value;
                if (focusedContent != null)
                {
                    focusedContent.DockHandler.Activate();
                }
            }
        }

        private void SetVisibleContentsToPane(DockPane pane)
        {
            this.SetVisibleContentsToPane(pane, this.ActiveContent);
        }

        private void SetVisibleContentsToPane(DockPane pane, IDockContent activeContent)
        {
            for (int i = 0; i < this.DisplayingContents.Count; i++)
            {
                IDockContent content = this.DisplayingContents[i];
                if (content.DockHandler.IsDockStateValid(pane.DockState))
                {
                    content.DockHandler.Pane = pane;
                    i--;
                }
            }
            if (activeContent.DockHandler.Pane == pane)
            {
                pane.ActiveContent = activeContent;
            }
        }

        public void Show()
        {
            this.Activate();
        }

        internal void ShowTabPageContextMenu(Control control, Point position)
        {
            object tabPageContextMenu = this.TabPageContextMenu;
            if (tabPageContextMenu != null)
            {
                ContextMenuStrip strip = tabPageContextMenu as ContextMenuStrip;
                if (strip != null)
                {
                    strip.Show(control, position);
                }
                else
                {
                    ContextMenu menu = tabPageContextMenu as ContextMenu;
                    if (menu != null)
                    {
                        menu.Show(this, position);
                    }
                }
            }
        }

        private void SuspendRefreshStateChange()
        {
            this.m_countRefreshStateChange++;
            this.DockPanel.SuspendLayout(true);
        }

        internal void TestDrop(IDockDragSource dragSource, DockOutlineBase dockOutline)
        {
            if (dragSource.CanDockTo(this))
            {
                Point mousePosition = Control.MousePosition;
                HitTestResult hitTest = this.GetHitTest(mousePosition);
                if (hitTest.HitArea == HitTestArea.Caption)
                {
                    dockOutline.Show(this, -1);
                }
                else if ((hitTest.HitArea == HitTestArea.TabStrip) && (hitTest.Index != -1))
                {
                    dockOutline.Show(this, hitTest.Index);
                }
            }
        }

        internal void ValidateActiveContent()
        {
            if (this.ActiveContent == null)
            {
                if (this.DisplayingContents.Count != 0)
                {
                    this.ActiveContent = this.DisplayingContents[0];
                }
            }
            else if (this.DisplayingContents.IndexOf(this.ActiveContent) < 0)
            {
                int num;
                IDockContent content = null;
                for (num = this.Contents.IndexOf(this.ActiveContent) - 1; num >= 0; num--)
                {
                    if (this.Contents[num].DockHandler.DockState == this.DockState)
                    {
                        content = this.Contents[num];
                        break;
                    }
                }
                IDockContent content2 = null;
                for (num = this.Contents.IndexOf(this.ActiveContent) + 1; num < this.Contents.Count; num++)
                {
                    if (this.Contents[num].DockHandler.DockState == this.DockState)
                    {
                        content2 = this.Contents[num];
                        break;
                    }
                }
                if (content != null)
                {
                    this.ActiveContent = content;
                }
                else if (content2 != null)
                {
                    this.ActiveContent = content2;
                }
                else
                {
                    this.ActiveContent = null;
                }
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 33)
            {
                this.Activate();
            }
            base.WndProc(ref m);
        }

        public virtual IDockContent ActiveContent
        {
            get
            {
                return this.m_activeContent;
            }
            set
            {
                if (this.ActiveContent != value)
                {
                    if (value != null)
                    {
                        if (!this.DisplayingContents.Contains(value))
                        {
                            throw new InvalidOperationException(Strings.DockPane_ActiveContent_InvalidValue);
                        }
                    }
                    else if (this.DisplayingContents.Count != 0)
                    {
                        throw new InvalidOperationException(Strings.DockPane_ActiveContent_InvalidValue);
                    }
                    IDockContent activeContent = this.m_activeContent;
                    if (this.DockPanel.ActiveAutoHideContent == activeContent)
                    {
                        this.DockPanel.ActiveAutoHideContent = null;
                    }
                    this.m_activeContent = value;
                    if ((this.DockPanel.DocumentStyle == DocumentStyle.DockingMdi) && (this.DockState == DockState.Document))
                    {
                        if (this.m_activeContent != null)
                        {
                            this.m_activeContent.DockHandler.Form.BringToFront();
                        }
                    }
                    else
                    {
                        if (this.m_activeContent != null)
                        {
                            this.m_activeContent.DockHandler.SetVisible();
                        }
                        if ((activeContent != null) && this.DisplayingContents.Contains(activeContent))
                        {
                            activeContent.DockHandler.SetVisible();
                        }
                        if (this.IsActivated && (this.m_activeContent != null))
                        {
                            this.m_activeContent.DockHandler.Activate();
                        }
                    }
                    if (this.FloatWindow != null)
                    {
                        this.FloatWindow.SetText();
                    }
                    if ((this.DockPanel.DocumentStyle == DocumentStyle.DockingMdi) && (this.DockState == DockState.Document))
                    {
                        this.RefreshChanges(false);
                    }
                    else
                    {
                        this.RefreshChanges();
                    }
                    if (this.m_activeContent != null)
                    {
                        this.TabStripControl.EnsureTabVisible(this.m_activeContent);
                    }
                }
            }
        }

        public virtual bool AllowDockDragAndDrop
        {
            get
            {
                return this.m_allowDockDragAndDrop;
            }
            set
            {
                this.m_allowDockDragAndDrop = value;
            }
        }

        public AppearanceStyle Appearance
        {
            get
            {
                return ((this.DockState == DockState.Document) ? AppearanceStyle.Document : AppearanceStyle.ToolWindow);
            }
        }

        internal IDisposable AutoHidePane
        {
            get
            {
                return this.m_autoHidePane;
            }
            set
            {
                this.m_autoHidePane = value;
            }
        }

        internal object AutoHideTabs
        {
            get
            {
                return this.m_autoHideTabs;
            }
            set
            {
                this.m_autoHideTabs = value;
            }
        }

        private DockPaneCaptionBase CaptionControl
        {
            get
            {
                return this.m_captionControl;
            }
        }

        private Rectangle CaptionRectangle
        {
            get
            {
                if (!this.HasCaption)
                {
                    return Rectangle.Empty;
                }
                Rectangle displayingRectangle = this.DisplayingRectangle;
                int x = displayingRectangle.X;
                int y = displayingRectangle.Y;
                int width = displayingRectangle.Width;
                return new Rectangle(x, y, width, this.CaptionControl.MeasureHeight());
            }
        }

        public virtual string CaptionText
        {
            get
            {
                return ((this.ActiveContent == null) ? string.Empty : this.ActiveContent.DockHandler.TabText);
            }
        }

        internal Rectangle ContentRectangle
        {
            get
            {
                Rectangle displayingRectangle = this.DisplayingRectangle;
                Rectangle captionRectangle = this.CaptionRectangle;
                Rectangle tabStripRectangle = this.TabStripRectangle;
                int x = displayingRectangle.X;
                int y = (displayingRectangle.Y + (captionRectangle.IsEmpty ? 0 : captionRectangle.Height)) + ((this.DockState == DockState.Document) ? tabStripRectangle.Height : 0);
                int width = displayingRectangle.Width;
                return new Rectangle(x, y, width, (displayingRectangle.Height - captionRectangle.Height) - tabStripRectangle.Height);
            }
        }

        public DockContentCollection Contents
        {
            get
            {
                return this.m_contents;
            }
        }

        public DockContentCollection DisplayingContents
        {
            get
            {
                return this.m_displayingContents;
            }
        }

        internal Rectangle DisplayingRectangle
        {
            get
            {
                return base.ClientRectangle;
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
            set
            {
                this.SetDockState(value);
            }
        }

        public DockWindow DockWindow
        {
            get
            {
                return ((this.m_nestedDockingStatus.NestedPanes == null) ? null : (this.m_nestedDockingStatus.NestedPanes.Container as DockWindow));
            }
            set
            {
                if (this.DockWindow != value)
                {
                    this.DockTo(value);
                }
            }
        }

        public FloatWindow FloatWindow
        {
            get
            {
                return ((this.m_nestedDockingStatus.NestedPanes == null) ? null : (this.m_nestedDockingStatus.NestedPanes.Container as FloatWindow));
            }
            set
            {
                if (this.FloatWindow != value)
                {
                    this.DockTo(value);
                }
            }
        }

        private bool HasCaption
        {
            get
            {
                if ((((this.DockState == DockState.Document) || (this.DockState == DockState.Hidden)) || (this.DockState == DockState.Unknown)) || ((this.DockState == DockState.Float) && (this.FloatWindow.VisibleNestedPanes.Count <= 1)))
                {
                    return false;
                }
                return true;
            }
        }

        internal bool HasTabPageContextMenu
        {
            get
            {
                return (this.TabPageContextMenu != null);
            }
        }

        public bool IsActivated
        {
            get
            {
                return this.m_isActivated;
            }
        }

        public bool IsActiveDocumentPane
        {
            get
            {
                return this.m_isActiveDocumentPane;
            }
        }

        public bool IsAutoHide
        {
            get
            {
                return DockHelper.IsDockStateAutoHide(this.DockState);
            }
        }

        public bool IsFloat
        {
            get
            {
                return this.m_isFloat;
            }
        }

        public bool IsHidden
        {
            get
            {
                return this.m_isHidden;
            }
        }

        private bool IsRefreshStateChangeSuspended
        {
            get
            {
                return (this.m_countRefreshStateChange != 0);
            }
        }

        Control IDragSource.DragControl
        {
            get
            {
                return this;
            }
        }

        public NestedDockingStatus NestedDockingStatus
        {
            get
            {
                return this.m_nestedDockingStatus;
            }
        }

        public INestedPanesContainer NestedPanesContainer
        {
            get
            {
                if (this.NestedDockingStatus.NestedPanes == null)
                {
                    return null;
                }
                return this.NestedDockingStatus.NestedPanes.Container;
            }
        }

        private SplitterControl Splitter
        {
            get
            {
                return this.m_splitter;
            }
        }

        internal DockAlignment SplitterAlignment
        {
            set
            {
                this.Splitter.Alignment = value;
            }
        }

        internal Rectangle SplitterBounds
        {
            set
            {
                this.Splitter.Bounds = value;
            }
        }

        private object TabPageContextMenu
        {
            get
            {
                IDockContent activeContent = this.ActiveContent;
                if (activeContent != null)
                {
                    if (activeContent.DockHandler.TabPageContextMenuStrip != null)
                    {
                        return activeContent.DockHandler.TabPageContextMenuStrip;
                    }
                    if (activeContent.DockHandler.TabPageContextMenu != null)
                    {
                        return activeContent.DockHandler.TabPageContextMenu;
                    }
                }
                return null;
            }
        }

        internal DockPaneStripBase TabStripControl
        {
            get
            {
                return this.m_tabStripControl;
            }
        }

        internal Rectangle TabStripRectangle
        {
            get
            {
                if (this.Appearance == AppearanceStyle.ToolWindow)
                {
                    return this.TabStripRectangle_ToolWindow;
                }
                return this.TabStripRectangle_Document;
            }
        }

        private Rectangle TabStripRectangle_Document
        {
            get
            {
                if (this.DisplayingContents.Count == 0)
                {
                    return Rectangle.Empty;
                }
                if ((this.DisplayingContents.Count == 1) && (this.DockPanel.DocumentStyle == DocumentStyle.DockingSdi))
                {
                    return Rectangle.Empty;
                }
                Rectangle displayingRectangle = this.DisplayingRectangle;
                int x = displayingRectangle.X;
                int y = displayingRectangle.Y;
                int width = displayingRectangle.Width;
                return new Rectangle(x, y, width, this.TabStripControl.MeasureHeight());
            }
        }

        private Rectangle TabStripRectangle_ToolWindow
        {
            get
            {
                if ((this.DisplayingContents.Count <= 1) || this.IsAutoHide)
                {
                    return Rectangle.Empty;
                }
                Rectangle displayingRectangle = this.DisplayingRectangle;
                int width = displayingRectangle.Width;
                int height = this.TabStripControl.MeasureHeight();
                int x = displayingRectangle.X;
                int y = displayingRectangle.Bottom - height;
                Rectangle captionRectangle = this.CaptionRectangle;
                if (captionRectangle.Contains(x, y))
                {
                    y = captionRectangle.Y + captionRectangle.Height;
                }
                return new Rectangle(x, y, width, height);
            }
        }

        public enum AppearanceStyle
        {
            ToolWindow,
            Document
        }

        private enum HitTestArea
        {
            Caption,
            TabStrip,
            Content,
            None
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HitTestResult
        {
            public DockPane.HitTestArea HitArea;
            public int Index;
            public HitTestResult(DockPane.HitTestArea hitTestArea, int index)
            {
                this.HitArea = hitTestArea;
                this.Index = index;
            }
        }

        private class SplitterControl : Control, ISplitterDragSource, IDragSource
        {
            private DockAlignment m_alignment;
            private DockPane m_pane;

            public SplitterControl(DockPane pane)
            {
                base.SetStyle(ControlStyles.Selectable, false);
                this.m_pane = pane;
            }

            void ISplitterDragSource.BeginDrag(Rectangle rectSplitter)
            {
            }

            void ISplitterDragSource.EndDrag()
            {
            }

            void ISplitterDragSource.MoveSplitter(int offset)
            {
                NestedDockingStatus nestedDockingStatus = this.DockPane.NestedDockingStatus;
                double proportion = nestedDockingStatus.Proportion;
                if ((nestedDockingStatus.LogicalBounds.Width > 0) && (nestedDockingStatus.LogicalBounds.Height > 0))
                {
                    if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Left)
                    {
                        proportion += ((double) offset) / ((double) nestedDockingStatus.LogicalBounds.Width);
                    }
                    else if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Right)
                    {
                        proportion -= ((double) offset) / ((double) nestedDockingStatus.LogicalBounds.Width);
                    }
                    else if (nestedDockingStatus.DisplayingAlignment == DockAlignment.Top)
                    {
                        proportion += ((double) offset) / ((double) nestedDockingStatus.LogicalBounds.Height);
                    }
                    else
                    {
                        proportion -= ((double) offset) / ((double) nestedDockingStatus.LogicalBounds.Height);
                    }
                    this.DockPane.SetNestedDockingProportion(proportion);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                if (e.Button == MouseButtons.Left)
                {
                    this.DockPane.DockPanel.BeginDrag(this, base.Parent.RectangleToScreen(base.Bounds));
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (this.DockPane.DockState == DockState.Document)
                {
                    Graphics graphics = e.Graphics;
                    Rectangle clientRectangle = base.ClientRectangle;
                    if ((this.Alignment == DockAlignment.Top) || (this.Alignment == DockAlignment.Bottom))
                    {
                        graphics.DrawLine(SystemPens.ControlDark, clientRectangle.Left, clientRectangle.Bottom - 1, clientRectangle.Right, clientRectangle.Bottom - 1);
                    }
                    else if ((this.Alignment == DockAlignment.Left) || (this.Alignment == DockAlignment.Right))
                    {
                        graphics.DrawLine(SystemPens.ControlDarkDark, clientRectangle.Right - 1, clientRectangle.Top, clientRectangle.Right - 1, clientRectangle.Bottom);
                    }
                }
            }

            public DockAlignment Alignment
            {
                get
                {
                    return this.m_alignment;
                }
                set
                {
                    this.m_alignment = value;
                    if ((this.m_alignment == DockAlignment.Left) || (this.m_alignment == DockAlignment.Right))
                    {
                        this.Cursor = Cursors.VSplit;
                    }
                    else if ((this.m_alignment == DockAlignment.Top) || (this.m_alignment == DockAlignment.Bottom))
                    {
                        this.Cursor = Cursors.HSplit;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                    if (this.DockPane.DockState == DockState.Document)
                    {
                        base.Invalidate();
                    }
                }
            }

            public DockPane DockPane
            {
                get
                {
                    return this.m_pane;
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
                    NestedDockingStatus nestedDockingStatus = this.DockPane.NestedDockingStatus;
                    Rectangle rectangle = base.Parent.RectangleToScreen(nestedDockingStatus.LogicalBounds);
                    if (((ISplitterDragSource) this).IsVertical)
                    {
                        rectangle.X += 24;
                        rectangle.Width -= 48;
                        return rectangle;
                    }
                    rectangle.Y += 24;
                    rectangle.Height -= 48;
                    return rectangle;
                }
            }

            bool ISplitterDragSource.IsVertical
            {
                get
                {
                    NestedDockingStatus nestedDockingStatus = this.DockPane.NestedDockingStatus;
                    return ((nestedDockingStatus.DisplayingAlignment == DockAlignment.Left) || (nestedDockingStatus.DisplayingAlignment == DockAlignment.Right));
                }
            }
        }
    }
}

