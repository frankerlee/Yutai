using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Framework.Docking.Win32;

namespace Yutai.ArcGIS.Framework.Docking
{
    public class DockContentHandler : IDisposable, IDockDragSource, IDragSource
    {
        private static readonly object DockStateChangedEvent = new object();
        private IntPtr m_activeWindowHandle;
        private DockAreas m_allowedAreas;
        private bool m_allowEndUserDocking;
        private double m_autoHidePortion;
        private IDisposable m_autoHideTab;
        private bool m_closeButton;
        private int m_countSetDockState;
        private DockPanel m_dockPanel;
        private DockState m_dockState;
        private EventHandlerList m_events;
        private bool m_flagClipWindow;
        private DockPane m_floatPane;
        private System.Windows.Forms.Form m_form;
        private GetPersistStringCallback m_getPersistStringCallback;
        private bool m_hideOnClose;
        private bool m_isActivated;
        private bool m_isFloat;
        private bool m_isHidden;
        private IDockContent m_nextActive;
        private DockPane m_panelPane;
        private IDockContent m_previousActive;
        private DockState m_showHint;
        private DockPaneStripBase.Tab m_tab;
        private ContextMenu m_tabPageContextMenu;
        private ContextMenuStrip m_tabPageContextMenuStrip;
        private string m_tabText;
        private string m_toolTipText;
        private DockState m_visibleState;

        public event EventHandler DockStateChanged
        {
            add { this.Events.AddHandler(DockStateChangedEvent, value); }
            remove { this.Events.RemoveHandler(DockStateChangedEvent, value); }
        }

        public DockContentHandler(System.Windows.Forms.Form form) : this(form, null)
        {
        }

        public DockContentHandler(System.Windows.Forms.Form form, GetPersistStringCallback getPersistStringCallback)
        {
            this.m_previousActive = null;
            this.m_nextActive = null;
            this.m_allowEndUserDocking = true;
            this.m_autoHidePortion = 0.25;
            this.m_closeButton = true;
            this.m_allowedAreas = DockAreas.Document | DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight |
                                  DockAreas.DockLeft | DockAreas.Float;
            this.m_dockState = DockState.Unknown;
            this.m_dockPanel = null;
            this.m_isHidden = true;
            this.m_tabText = null;
            this.m_visibleState = DockState.Unknown;
            this.m_isFloat = false;
            this.m_panelPane = null;
            this.m_floatPane = null;
            this.m_countSetDockState = 0;
            this.m_getPersistStringCallback = null;
            this.m_hideOnClose = false;
            this.m_showHint = DockState.Unknown;
            this.m_isActivated = false;
            this.m_tabPageContextMenu = null;
            this.m_toolTipText = null;
            this.m_activeWindowHandle = IntPtr.Zero;
            this.m_tab = null;
            this.m_autoHideTab = null;
            this.m_flagClipWindow = false;
            this.m_tabPageContextMenuStrip = null;
            if (!(form is IDockContent))
            {
                throw new ArgumentException(Strings.DockContent_Constructor_InvalidForm, "form");
            }
            this.m_form = form;
            this.m_getPersistStringCallback = getPersistStringCallback;
            this.m_events = new EventHandlerList();
            this.Form.Disposed += new EventHandler(this.Form_Disposed);
            this.Form.TextChanged += new EventHandler(this.Form_TextChanged);
        }

        public void Activate()
        {
            if (this.DockPanel == null)
            {
                this.Form.Activate();
            }
            else if (this.Pane == null)
            {
                this.Show(this.DockPanel);
            }
            else
            {
                this.IsHidden = false;
                this.Pane.ActiveContent = this.Content;
                if ((this.DockState == DockState.Document) && (this.DockPanel.DocumentStyle == DocumentStyle.SystemMdi))
                {
                    this.Form.Activate();
                }
                else
                {
                    if (DockHelper.IsDockStateAutoHide(this.DockState))
                    {
                        this.DockPanel.ActiveAutoHideContent = this.Content;
                    }
                    if (!this.Form.ContainsFocus)
                    {
                        this.DockPanel.ContentFocusManager.Activate(this.Content);
                    }
                }
            }
        }

        public DockState CheckDockState(bool isFloat)
        {
            if (isFloat)
            {
                if (!this.IsDockStateValid(DockState.Float))
                {
                    return DockState.Unknown;
                }
                return DockState.Float;
            }
            return ((this.PanelPane != null) ? this.PanelPane.DockState : this.DefaultDockState);
        }

        public void Close()
        {
            DockPanel dockPanel = this.DockPanel;
            if (dockPanel != null)
            {
                dockPanel.SuspendLayout(true);
            }
            this.Form.Close();
            if (dockPanel != null)
            {
                dockPanel.ResumeLayout(true, true);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    this.DockPanel = null;
                    if (this.m_autoHideTab != null)
                    {
                        this.m_autoHideTab.Dispose();
                    }
                    if (this.m_tab != null)
                    {
                        this.m_tab.Dispose();
                    }
                    this.Form.Disposed -= new EventHandler(this.Form_Disposed);
                    this.Form.TextChanged -= new EventHandler(this.Form_TextChanged);
                    this.m_events.Dispose();
                }
            }
        }

        public void DockTo(DockPanel panel, DockStyle dockStyle)
        {
            DockPane pane;
            if (panel != this.DockPanel)
            {
                throw new ArgumentException(Strings.IDockDragSource_DockTo_InvalidPanel, "panel");
            }
            if (dockStyle == DockStyle.Top)
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.DockTop, true);
            }
            else if (dockStyle == DockStyle.Bottom)
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.DockBottom, true);
            }
            else if (dockStyle == DockStyle.Left)
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.DockLeft, true);
            }
            else if (dockStyle == DockStyle.Right)
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.DockRight, true);
            }
            else if (dockStyle == DockStyle.Fill)
            {
                pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.Document, true);
            }
        }

        public void DockTo(DockPane pane, DockStyle dockStyle, int contentIndex)
        {
            if (dockStyle == DockStyle.Fill)
            {
                bool flag = this.Pane == pane;
                if (!flag)
                {
                    this.Pane = pane;
                }
                if (!((contentIndex != -1) && flag))
                {
                    pane.SetContentIndex(this.Content, contentIndex);
                }
                else
                {
                    DockContentCollection contents = pane.Contents;
                    int index = contents.IndexOf(this.Content);
                    int num2 = contentIndex;
                    if (index < num2)
                    {
                        num2++;
                        if (num2 > (contents.Count - 1))
                        {
                            num2 = -1;
                        }
                    }
                    pane.SetContentIndex(this.Content, num2);
                }
            }
            else
            {
                DockPane pane2 = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, pane.DockState, true);
                INestedPanesContainer nestedPanesContainer = pane.NestedPanesContainer;
                if (dockStyle == DockStyle.Left)
                {
                    pane2.DockTo(nestedPanesContainer, pane, DockAlignment.Left, 0.5);
                }
                else if (dockStyle == DockStyle.Right)
                {
                    pane2.DockTo(nestedPanesContainer, pane, DockAlignment.Right, 0.5);
                }
                else if (dockStyle == DockStyle.Top)
                {
                    pane2.DockTo(nestedPanesContainer, pane, DockAlignment.Top, 0.5);
                }
                else if (dockStyle == DockStyle.Bottom)
                {
                    pane2.DockTo(nestedPanesContainer, pane, DockAlignment.Bottom, 0.5);
                }
                pane2.DockState = pane.DockState;
            }
        }

        public void FloatAt(Rectangle floatWindowBounds)
        {
            DockPane pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, floatWindowBounds, true);
        }

        private void Form_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_TextChanged(object sender, EventArgs e)
        {
            if (DockHelper.IsDockStateAutoHide(this.DockState))
            {
                this.DockPanel.RefreshAutoHideStrip();
            }
            else if (this.Pane != null)
            {
                if (this.Pane.FloatWindow != null)
                {
                    this.Pane.FloatWindow.SetText();
                }
                this.Pane.RefreshChanges();
            }
        }

        internal DockPaneStripBase.Tab GetTab(DockPaneStripBase dockPaneStrip)
        {
            if (this.m_tab == null)
            {
                this.m_tab = dockPaneStrip.CreateTab(this.Content);
            }
            return this.m_tab;
        }

        public void GiveUpFocus()
        {
            this.DockPanel.ContentFocusManager.GiveUpFocus(this.Content);
        }

        public void Hide()
        {
            this.IsHidden = true;
        }

        public bool IsDockStateValid(DockState dockState)
        {
            if (((this.DockPanel != null) && (dockState == DockState.Document)) &&
                (this.DockPanel.DocumentStyle == DocumentStyle.SystemMdi))
            {
                return false;
            }
            return DockHelper.IsDockStateValid(dockState, this.DockAreas);
        }

        Rectangle IDockDragSource.BeginDrag(Point ptMouse)
        {
            Size defaultFloatWindowSize;
            Point point = new Point();
            DockPane floatPane = this.FloatPane;
            if (((this.DockState == DockState.Float) || (floatPane == null)) ||
                (floatPane.FloatWindow.NestedPanes.Count != 1))
            {
                defaultFloatWindowSize = this.DockPanel.DefaultFloatWindowSize;
            }
            else
            {
                defaultFloatWindowSize = floatPane.FloatWindow.Size;
            }
            Rectangle clientRectangle = this.Pane.ClientRectangle;
            if (this.DockState == DockState.Document)
            {
                point = new Point(clientRectangle.Left, clientRectangle.Top);
            }
            else
            {
                point = new Point(clientRectangle.Left, clientRectangle.Bottom)
                {
                    Y = point.Y - defaultFloatWindowSize.Height
                };
            }
            point = this.Pane.PointToScreen(point);
            if (ptMouse.X > (point.X + defaultFloatWindowSize.Width))
            {
                point.X += (ptMouse.X - (point.X + defaultFloatWindowSize.Width)) + 4;
            }
            return new Rectangle(point, defaultFloatWindowSize);
        }

        bool IDockDragSource.CanDockTo(DockPane pane)
        {
            if (!this.IsDockStateValid(pane.DockState))
            {
                return false;
            }
            if ((this.Pane == pane) && (pane.DisplayingContents.Count == 1))
            {
                return false;
            }
            return true;
        }

        protected virtual void OnDockStateChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) this.Events[DockStateChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private static void RefreshDockPane(DockPane pane)
        {
            pane.RefreshChanges();
            pane.ValidateActiveContent();
        }

        private void RemoveFromPane(DockPane pane)
        {
            pane.RemoveContent(this.Content);
            this.SetPane(null);
            if (pane.Contents.Count == 0)
            {
                pane.Dispose();
            }
        }

        private void ResumeSetDockState()
        {
            this.m_countSetDockState--;
            if (this.m_countSetDockState < 0)
            {
                this.m_countSetDockState = 0;
            }
        }

        private void ResumeSetDockState(bool isHidden, DockState visibleState, DockPane oldPane)
        {
            this.ResumeSetDockState();
            this.SetDockState(isHidden, visibleState, oldPane);
        }

        internal void SetDockState(bool isHidden, DockState visibleState, DockPane oldPane)
        {
            if (!this.IsSuspendSetDockState)
            {
                if ((this.DockPanel == null) && (visibleState != DockState.Unknown))
                {
                    throw new InvalidOperationException(Strings.DockContentHandler_SetDockState_NullPanel);
                }
                if ((visibleState == DockState.Hidden) ||
                    ((visibleState != DockState.Unknown) && !this.IsDockStateValid(visibleState)))
                {
                    throw new InvalidOperationException(Strings.DockContentHandler_SetDockState_InvalidState);
                }
                DockPanel dockPanel = this.DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.SuspendLayout(true);
                }
                this.SuspendSetDockState();
                DockState dockState = this.DockState;
                if ((this.m_isHidden != isHidden) || (dockState == DockState.Unknown))
                {
                    this.m_isHidden = isHidden;
                }
                this.m_visibleState = visibleState;
                this.m_dockState = isHidden ? DockState.Hidden : visibleState;
                if (visibleState == DockState.Unknown)
                {
                    this.Pane = null;
                }
                else
                {
                    this.m_isFloat = this.m_visibleState == DockState.Float;
                    if (this.Pane == null)
                    {
                        this.Pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, visibleState, true);
                    }
                    else if (this.Pane.DockState != visibleState)
                    {
                        if (this.Pane.Contents.Count == 1)
                        {
                            this.Pane.SetDockState(visibleState);
                        }
                        else
                        {
                            this.Pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, visibleState, true);
                        }
                    }
                }
                if (this.Form.ContainsFocus &&
                    ((this.DockState == DockState.Hidden) || (this.DockState == DockState.Unknown)))
                {
                    this.DockPanel.ContentFocusManager.GiveUpFocus(this.Content);
                }
                this.SetPaneAndVisible(this.Pane);
                if (((oldPane != null) && !oldPane.IsDisposed) && (dockState == oldPane.DockState))
                {
                    RefreshDockPane(oldPane);
                }
                if (((this.Pane != null) && (this.DockState == this.Pane.DockState)) &&
                    ((this.Pane != oldPane) || ((this.Pane == oldPane) && (dockState != oldPane.DockState))))
                {
                    RefreshDockPane(this.Pane);
                }
                if (dockState != this.DockState)
                {
                    if (((this.DockState == DockState.Hidden) || (this.DockState == DockState.Unknown)) ||
                        DockHelper.IsDockStateAutoHide(this.DockState))
                    {
                        this.DockPanel.ContentFocusManager.RemoveFromList(this.Content);
                    }
                    else
                    {
                        this.DockPanel.ContentFocusManager.AddToList(this.Content);
                    }
                    this.OnDockStateChanged(EventArgs.Empty);
                }
                this.ResumeSetDockState();
                if (dockPanel != null)
                {
                    dockPanel.ResumeLayout(true, true);
                }
            }
        }

        private void SetPane(DockPane pane)
        {
            if (((pane != null) && (pane.DockState == DockState.Document)) &&
                (this.DockPanel.DocumentStyle == DocumentStyle.DockingMdi))
            {
                if (this.Form.Parent is DockPane)
                {
                    this.SetParent(null);
                }
                if (this.Form.MdiParent != this.DockPanel.ParentForm)
                {
                    this.FlagClipWindow = true;
                    this.Form.MdiParent = this.DockPanel.ParentForm;
                }
            }
            else
            {
                this.FlagClipWindow = true;
                if (this.Form.MdiParent != null)
                {
                    this.Form.MdiParent = null;
                }
                if (this.Form.TopLevel)
                {
                    this.Form.TopLevel = false;
                }
                this.SetParent(pane);
            }
        }

        internal void SetPaneAndVisible(DockPane pane)
        {
            this.SetPane(pane);
            this.SetVisible();
        }

        private void SetParent(Control value)
        {
            if (this.Form.Parent != value)
            {
                bool flag = false;
                if (this.Form.ContainsFocus)
                {
                    if (value == null)
                    {
                        this.DockPanel.ContentFocusManager.GiveUpFocus(this.Content);
                    }
                    else
                    {
                        this.DockPanel.SaveFocus();
                        flag = true;
                    }
                }
                this.Form.Parent = value;
                if (flag)
                {
                    this.Activate();
                }
            }
        }

        internal void SetVisible()
        {
            bool visible;
            if (this.IsHidden)
            {
                visible = false;
            }
            else if (((this.Pane != null) && (this.Pane.DockState == DockState.Document)) &&
                     (this.DockPanel.DocumentStyle == DocumentStyle.DockingMdi))
            {
                visible = true;
            }
            else if ((this.Pane != null) && (this.Pane.ActiveContent == this.Content))
            {
                visible = true;
            }
            else if ((this.Pane != null) && (this.Pane.ActiveContent != this.Content))
            {
                visible = false;
            }
            else
            {
                visible = this.Form.Visible;
            }
            if (this.Form.Visible != visible)
            {
                this.Form.Visible = visible;
            }
        }

        public void Show()
        {
            if (this.DockPanel == null)
            {
                this.Form.Show();
            }
            else
            {
                this.Show(this.DockPanel);
            }
        }

        public void Show(DockPanel dockPanel)
        {
            if (dockPanel == null)
            {
                throw new ArgumentNullException(Strings.DockContentHandler_Show_NullDockPanel);
            }
            if (this.DockState == DockState.Unknown)
            {
                this.Show(dockPanel, this.DefaultShowState);
            }
            else
            {
                this.Activate();
            }
        }

        public void Show(DockPane pane, IDockContent beforeContent)
        {
            if (pane == null)
            {
                throw new ArgumentNullException(Strings.DockContentHandler_Show_NullPane);
            }
            if ((beforeContent != null) && (pane.Contents.IndexOf(beforeContent) == -1))
            {
                throw new ArgumentException(Strings.DockContentHandler_Show_InvalidBeforeContent);
            }
            pane.DockPanel.SuspendLayout(true);
            this.DockPanel = pane.DockPanel;
            this.Pane = pane;
            pane.SetContentIndex(this.Content, pane.Contents.IndexOf(beforeContent));
            this.Show();
            pane.DockPanel.ResumeLayout(true, true);
        }

        public void Show(DockPanel dockPanel, DockState dockState)
        {
            if (dockPanel == null)
            {
                throw new ArgumentNullException(Strings.DockContentHandler_Show_NullDockPanel);
            }
            if ((dockState == DockState.Unknown) || (dockState == DockState.Hidden))
            {
                throw new ArgumentException(Strings.DockContentHandler_Show_InvalidDockState);
            }
            dockPanel.SuspendLayout(true);
            this.DockPanel = dockPanel;
            if ((dockState == DockState.Float) && (this.FloatPane == null))
            {
                this.Pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.Float, true);
            }
            else if (this.PanelPane == null)
            {
                DockPane pane = null;
                foreach (DockPane pane2 in this.DockPanel.Panes)
                {
                    if (pane2.DockState == dockState)
                    {
                        pane = pane2;
                        break;
                    }
                }
                if (pane == null)
                {
                    this.Pane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, dockState, true);
                }
                else
                {
                    this.Pane = pane;
                }
            }
            this.DockState = dockState;
            this.Activate();
            dockPanel.ResumeLayout(true, true);
        }

        public void Show(DockPanel dockPanel, Rectangle floatWindowBounds)
        {
            if (dockPanel == null)
            {
                throw new ArgumentNullException(Strings.DockContentHandler_Show_NullDockPanel);
            }
            dockPanel.SuspendLayout(true);
            this.DockPanel = dockPanel;
            if (this.FloatPane == null)
            {
                this.IsHidden = true;
                this.FloatPane = this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, DockState.Float, false);
                this.FloatPane.FloatWindow.StartPosition = FormStartPosition.Manual;
            }
            this.FloatPane.FloatWindow.Bounds = floatWindowBounds;
            this.Show(dockPanel, DockState.Float);
            this.Activate();
            dockPanel.ResumeLayout(true, true);
        }

        public void Show(DockPane previousPane, DockAlignment alignment, double proportion)
        {
            if (previousPane == null)
            {
                throw new ArgumentException(Strings.DockContentHandler_Show_InvalidPrevPane);
            }
            if (DockHelper.IsDockStateAutoHide(previousPane.DockState))
            {
                throw new ArgumentException(Strings.DockContentHandler_Show_InvalidPrevPane);
            }
            previousPane.DockPanel.SuspendLayout(true);
            this.DockPanel = previousPane.DockPanel;
            this.DockPanel.DockPaneFactory.CreateDockPane(this.Content, previousPane, alignment, proportion, true);
            this.Show();
            previousPane.DockPanel.ResumeLayout(true, true);
        }

        private void SuspendSetDockState()
        {
            this.m_countSetDockState++;
        }

        internal IntPtr ActiveWindowHandle
        {
            get { return this.m_activeWindowHandle; }
            set { this.m_activeWindowHandle = value; }
        }

        public bool AllowEndUserDocking
        {
            get { return this.m_allowEndUserDocking; }
            set { this.m_allowEndUserDocking = value; }
        }

        public double AutoHidePortion
        {
            get { return this.m_autoHidePortion; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException(Strings.DockContentHandler_AutoHidePortion_OutOfRange);
                }
                if (this.m_autoHidePortion != value)
                {
                    this.m_autoHidePortion = value;
                    if ((this.DockPanel != null) && (this.DockPanel.ActiveAutoHideContent == this.Content))
                    {
                        this.DockPanel.PerformLayout();
                    }
                }
            }
        }

        internal IDisposable AutoHideTab
        {
            get { return this.m_autoHideTab; }
            set { this.m_autoHideTab = value; }
        }

        public bool CloseButton
        {
            get { return this.m_closeButton; }
            set
            {
                if (this.m_closeButton != value)
                {
                    this.m_closeButton = value;
                    if ((this.Pane != null) && (this.Pane.ActiveContent.DockHandler == this))
                    {
                        this.Pane.RefreshChanges();
                    }
                }
            }
        }

        public IDockContent Content
        {
            get { return (this.Form as IDockContent); }
        }

        private DockState DefaultDockState
        {
            get
            {
                if ((this.ShowHint != DockState.Unknown) && (this.ShowHint != DockState.Hidden))
                {
                    return this.ShowHint;
                }
                if ((this.DockAreas & DockAreas.Document) != 0)
                {
                    return DockState.Document;
                }
                if ((this.DockAreas & DockAreas.DockRight) != 0)
                {
                    return DockState.DockRight;
                }
                if ((this.DockAreas & DockAreas.DockLeft) != 0)
                {
                    return DockState.DockLeft;
                }
                if ((this.DockAreas & DockAreas.DockBottom) != 0)
                {
                    return DockState.DockBottom;
                }
                if ((this.DockAreas & DockAreas.DockTop) != 0)
                {
                    return DockState.DockTop;
                }
                return DockState.Unknown;
            }
        }

        private DockState DefaultShowState
        {
            get
            {
                if (this.ShowHint != DockState.Unknown)
                {
                    return this.ShowHint;
                }
                if ((this.DockAreas & DockAreas.Document) != 0)
                {
                    return DockState.Document;
                }
                if ((this.DockAreas & DockAreas.DockRight) != 0)
                {
                    return DockState.DockRight;
                }
                if ((this.DockAreas & DockAreas.DockLeft) != 0)
                {
                    return DockState.DockLeft;
                }
                if ((this.DockAreas & DockAreas.DockBottom) != 0)
                {
                    return DockState.DockBottom;
                }
                if ((this.DockAreas & DockAreas.DockTop) != 0)
                {
                    return DockState.DockTop;
                }
                if ((this.DockAreas & DockAreas.Float) != 0)
                {
                    return DockState.Float;
                }
                return DockState.Unknown;
            }
        }

        public DockAreas DockAreas
        {
            get { return this.m_allowedAreas; }
            set
            {
                if (this.m_allowedAreas != value)
                {
                    if (!DockHelper.IsDockStateValid(this.DockState, value))
                    {
                        throw new InvalidOperationException(Strings.DockContentHandler_DockAreas_InvalidValue);
                    }
                    this.m_allowedAreas = value;
                    if (!DockHelper.IsDockStateValid(this.ShowHint, this.m_allowedAreas))
                    {
                        this.ShowHint = DockState.Unknown;
                    }
                }
            }
        }

        public DockPanel DockPanel
        {
            get { return this.m_dockPanel; }
            set
            {
                if (this.m_dockPanel != value)
                {
                    this.Pane = null;
                    if (this.m_dockPanel != null)
                    {
                        this.m_dockPanel.RemoveContent(this.Content);
                    }
                    if (this.m_tab != null)
                    {
                        this.m_tab.Dispose();
                        this.m_tab = null;
                    }
                    if (this.m_autoHideTab != null)
                    {
                        this.m_autoHideTab.Dispose();
                        this.m_autoHideTab = null;
                    }
                    this.m_dockPanel = value;
                    if (this.m_dockPanel != null)
                    {
                        this.m_dockPanel.AddContent(this.Content);
                        this.Form.TopLevel = false;
                        this.Form.FormBorderStyle = FormBorderStyle.None;
                        this.Form.ShowInTaskbar = false;
                        this.Form.WindowState = FormWindowState.Normal;
                        NativeMethods.SetWindowPos(this.Form.Handle, IntPtr.Zero, 0, 0, 0, 0,
                            FlagsSetWindowPos.SWP_DRAWFRAME | FlagsSetWindowPos.SWP_NOACTIVATE |
                            FlagsSetWindowPos.SWP_NOMOVE | FlagsSetWindowPos.SWP_NOOWNERZORDER |
                            FlagsSetWindowPos.SWP_NOSIZE | FlagsSetWindowPos.SWP_NOZORDER);
                    }
                }
            }
        }

        public DockState DockState
        {
            get { return this.m_dockState; }
            set
            {
                if (this.m_dockState != value)
                {
                    this.DockPanel.SuspendLayout(true);
                    if (value == DockState.Hidden)
                    {
                        this.IsHidden = true;
                    }
                    else
                    {
                        this.SetDockState(false, value, this.Pane);
                    }
                    this.DockPanel.ResumeLayout(true, true);
                }
            }
        }

        private EventHandlerList Events
        {
            get { return this.m_events; }
        }

        internal bool FlagClipWindow
        {
            get { return this.m_flagClipWindow; }
            set
            {
                if (this.m_flagClipWindow != value)
                {
                    this.m_flagClipWindow = value;
                    if (this.m_flagClipWindow)
                    {
                        this.Form.Region = new Region(Rectangle.Empty);
                    }
                    else
                    {
                        this.Form.Region = null;
                    }
                }
            }
        }

        public DockPane FloatPane
        {
            get { return this.m_floatPane; }
            set
            {
                if (this.m_floatPane != value)
                {
                    if ((value != null) && !(value.IsFloat && (value.DockPanel == this.DockPanel)))
                    {
                        throw new InvalidOperationException(Strings.DockContentHandler_FloatPane_InvalidValue);
                    }
                    DockPane oldPane = this.Pane;
                    if (this.m_floatPane != null)
                    {
                        this.RemoveFromPane(this.m_floatPane);
                    }
                    this.m_floatPane = value;
                    if (this.m_floatPane != null)
                    {
                        this.m_floatPane.AddContent(this.Content);
                        this.SetDockState(this.IsHidden, this.IsFloat ? DockState.Float : this.VisibleState, oldPane);
                    }
                    else
                    {
                        this.SetDockState(this.IsHidden, DockState.Unknown, oldPane);
                    }
                }
            }
        }

        public System.Windows.Forms.Form Form
        {
            get { return this.m_form; }
        }

        public GetPersistStringCallback GetPersistStringCallback
        {
            get { return this.m_getPersistStringCallback; }
            set { this.m_getPersistStringCallback = value; }
        }

        public bool HideOnClose
        {
            get { return this.m_hideOnClose; }
            set { this.m_hideOnClose = value; }
        }

        public System.Drawing.Icon Icon
        {
            get { return this.Form.Icon; }
        }

        public bool IsActivated
        {
            get { return this.m_isActivated; }
            internal set
            {
                if (this.m_isActivated != value)
                {
                    this.m_isActivated = value;
                }
            }
        }

        public bool IsFloat
        {
            get { return this.m_isFloat; }
            set
            {
                if (this.m_isFloat != value)
                {
                    DockState visibleState = this.CheckDockState(value);
                    if (visibleState == DockState.Unknown)
                    {
                        throw new InvalidOperationException(Strings.DockContentHandler_IsFloat_InvalidValue);
                    }
                    this.SetDockState(this.IsHidden, visibleState, this.Pane);
                }
            }
        }

        public bool IsHidden
        {
            get { return this.m_isHidden; }
            set
            {
                if (this.m_isHidden != value)
                {
                    this.SetDockState(value, this.VisibleState, this.Pane);
                }
            }
        }

        internal bool IsSuspendSetDockState
        {
            get { return (this.m_countSetDockState != 0); }
        }

        Control IDragSource.DragControl
        {
            get { return this.Form; }
        }

        public IDockContent NextActive
        {
            get { return this.m_nextActive; }
            internal set { this.m_nextActive = value; }
        }

        public DockPane Pane
        {
            get { return (this.IsFloat ? this.FloatPane : this.PanelPane); }
            set
            {
                if (this.Pane != value)
                {
                    this.DockPanel.SuspendLayout(true);
                    DockPane oldPane = this.Pane;
                    this.SuspendSetDockState();
                    this.FloatPane = (value == null) ? null : (value.IsFloat ? value : this.FloatPane);
                    this.PanelPane = (value == null) ? null : (value.IsFloat ? this.PanelPane : value);
                    this.ResumeSetDockState(this.IsHidden, (value != null) ? value.DockState : DockState.Unknown,
                        oldPane);
                    this.DockPanel.ResumeLayout(true, true);
                }
            }
        }

        public DockPane PanelPane
        {
            get { return this.m_panelPane; }
            set
            {
                if (this.m_panelPane != value)
                {
                    if ((value != null) && (value.IsFloat || (value.DockPanel != this.DockPanel)))
                    {
                        throw new InvalidOperationException(Strings.DockContentHandler_DockPane_InvalidValue);
                    }
                    DockPane oldPane = this.Pane;
                    if (this.m_panelPane != null)
                    {
                        this.RemoveFromPane(this.m_panelPane);
                    }
                    this.m_panelPane = value;
                    if (this.m_panelPane != null)
                    {
                        this.m_panelPane.AddContent(this.Content);
                        this.SetDockState(this.IsHidden, this.IsFloat ? DockState.Float : this.m_panelPane.DockState,
                            oldPane);
                    }
                    else
                    {
                        this.SetDockState(this.IsHidden, DockState.Unknown, oldPane);
                    }
                }
            }
        }

        internal string PersistString
        {
            get
            {
                return ((this.GetPersistStringCallback == null)
                    ? this.Form.GetType().ToString()
                    : this.GetPersistStringCallback());
            }
        }

        public IDockContent PreviousActive
        {
            get { return this.m_previousActive; }
            internal set { this.m_previousActive = value; }
        }

        public DockState ShowHint
        {
            get { return this.m_showHint; }
            set
            {
                if (!DockHelper.IsDockStateValid(value, this.DockAreas))
                {
                    throw new InvalidOperationException(Strings.DockContentHandler_ShowHint_InvalidValue);
                }
                if (this.m_showHint != value)
                {
                    this.m_showHint = value;
                }
            }
        }

        public ContextMenu TabPageContextMenu
        {
            get { return this.m_tabPageContextMenu; }
            set { this.m_tabPageContextMenu = value; }
        }

        public ContextMenuStrip TabPageContextMenuStrip
        {
            get { return this.m_tabPageContextMenuStrip; }
            set { this.m_tabPageContextMenuStrip = value; }
        }

        public string TabText
        {
            get { return ((this.m_tabText == null) ? this.Form.Text : this.m_tabText); }
            set
            {
                if (!(this.m_tabText == value))
                {
                    this.m_tabText = value;
                    if (this.Pane != null)
                    {
                        this.Pane.RefreshChanges();
                    }
                }
            }
        }

        public string ToolTipText
        {
            get { return this.m_toolTipText; }
            set { this.m_toolTipText = value; }
        }

        public DockState VisibleState
        {
            get { return this.m_visibleState; }
            set
            {
                if (this.m_visibleState != value)
                {
                    this.SetDockState(this.IsHidden, value, this.Pane);
                }
            }
        }
    }
}