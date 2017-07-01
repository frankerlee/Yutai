using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI.Design;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Framework.Docking.Win32;

namespace Yutai.ArcGIS.Framework.Docking
{
    [Designer(typeof(ControlDesigner)), ToolboxBitmap(typeof(DockPanel), "Resources.DockPanel.bmp")]
    public class DockPanel : Panel
    {
        private static readonly object ActiveContentChangedEvent = new object();
        private static readonly object ActiveDocumentChangedEvent = new object();
        private static readonly object ActivePaneChangedEvent = new object();
        private static readonly object ContentAddedEvent = new object();
        private static readonly object ContentRemovedEvent = new object();
        private bool m_allowEndUserDocking = true;
        private bool m_allowEndUserNestedDocking = true;
        private AutoHideStripBase m_autoHideStripControl = null;
        private AutoHideWindowControl m_autoHideWindow;
        private Rectangle[] m_clipRects = null;
        private DockContentCollection m_contents = new DockContentCollection();
        private Size m_defaultFloatWindowSize = new Size(300, 300);
        private bool m_disposed = false;
        private double m_dockBottomPortion = 0.25;
        private DockDragHandler m_dockDragHandler = null;
        private double m_dockLeftPortion = 0.25;
        private double m_dockRightPortion = 0.25;
        private double m_dockTopPortion = 0.25;
        private DockWindowCollection m_dockWindows;
        private DocumentStyle m_documentStyle = DocumentStyle.DockingMdi;
        private DockContent m_dummyContent;
        private Control m_dummyControl;
        private PaintEventHandler m_dummyControlPaintEventHandler = null;
        private DockPanelExtender m_extender;
        private FloatWindowCollection m_floatWindows;
        private FocusManagerImpl m_focusManager;
        private MdiClientController m_mdiClientController = null;
        private DockPaneCollection m_panes;
        private bool m_rightToLeftLayout = false;
        private bool m_showDocumentIcon = false;
        private SplitterDragHandler m_splitterDragHandler = null;

        [LocalizedDescription("DockPanel_ActiveContentChanged_Description"),
         LocalizedCategory("Category_PropertyChanged")]
        public event EventHandler ActiveContentChanged
        {
            add { base.Events.AddHandler(ActiveContentChangedEvent, value); }
            remove { base.Events.RemoveHandler(ActiveContentChangedEvent, value); }
        }

        [LocalizedDescription("DockPanel_ActiveDocumentChanged_Description"),
         LocalizedCategory("Category_PropertyChanged")]
        public event EventHandler ActiveDocumentChanged
        {
            add { base.Events.AddHandler(ActiveDocumentChangedEvent, value); }
            remove { base.Events.RemoveHandler(ActiveDocumentChangedEvent, value); }
        }

        [LocalizedCategory("Category_PropertyChanged"), LocalizedDescription("DockPanel_ActivePaneChanged_Description")]
        public event EventHandler ActivePaneChanged
        {
            add { base.Events.AddHandler(ActivePaneChangedEvent, value); }
            remove { base.Events.RemoveHandler(ActivePaneChangedEvent, value); }
        }

        [LocalizedCategory("Category_DockingNotification"), LocalizedDescription("DockPanel_ContentAdded_Description")]
        public event EventHandler<DockContentEventArgs> ContentAdded
        {
            add { base.Events.AddHandler(ContentAddedEvent, value); }
            remove { base.Events.RemoveHandler(ContentAddedEvent, value); }
        }

        [LocalizedCategory("Category_DockingNotification"), LocalizedDescription("DockPanel_ContentRemoved_Description")
        ]
        public event EventHandler<DockContentEventArgs> ContentRemoved
        {
            add { base.Events.AddHandler(ContentRemovedEvent, value); }
            remove { base.Events.RemoveHandler(ContentRemovedEvent, value); }
        }

        public DockPanel()
        {
            this.m_focusManager = new FocusManagerImpl(this);
            this.m_extender = new DockPanelExtender(this);
            this.m_panes = new DockPaneCollection();
            this.m_floatWindows = new FloatWindowCollection();
            base.SuspendLayout();
            this.Font = SystemInformation.MenuFont;
            this.m_autoHideWindow = new AutoHideWindowControl(this);
            this.m_autoHideWindow.Visible = false;
            this.m_dummyControl = new DummyControl();
            this.m_dummyControl.Bounds = new Rectangle(0, 0, 1, 1);
            base.Controls.Add(this.m_dummyControl);
            this.m_dockWindows = new DockWindowCollection(this);
            base.Controls.AddRange(new Control[]
            {
                this.DockWindows[DockState.Document], this.DockWindows[DockState.DockLeft],
                this.DockWindows[DockState.DockRight], this.DockWindows[DockState.DockTop],
                this.DockWindows[DockState.DockBottom]
            });
            this.m_dummyContent = new DockContent();
            base.ResumeLayout();
        }

        internal void AddContent(IDockContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            if (!this.Contents.Contains(content))
            {
                this.Contents.Add(content);
                this.OnContentAdded(new DockContentEventArgs(content));
            }
        }

        internal void AddFloatWindow(FloatWindow floatWindow)
        {
            if (!this.FloatWindows.Contains(floatWindow))
            {
                this.FloatWindows.Add(floatWindow);
            }
        }

        internal void AddPane(DockPane pane)
        {
            if (!this.Panes.Contains(pane))
            {
                this.Panes.Add(pane);
            }
        }

        internal void BeginDrag(IDockDragSource dragSource)
        {
            this.GetDockDragHandler().BeginDrag(dragSource);
        }

        internal void BeginDrag(ISplitterDragSource dragSource, Rectangle rectSplitter)
        {
            this.GetSplitterDragHandler().BeginDrag(dragSource, rectSplitter);
        }

        private void CalculateDockPadding()
        {
            base.DockPadding.All = 0;
            int num = this.AutoHideStripControl.MeasureHeight();
            if (this.AutoHideStripControl.GetNumberOfPanes(DockState.DockLeftAutoHide) > 0)
            {
                base.DockPadding.Left = num;
            }
            if (this.AutoHideStripControl.GetNumberOfPanes(DockState.DockRightAutoHide) > 0)
            {
                base.DockPadding.Right = num;
            }
            if (this.AutoHideStripControl.GetNumberOfPanes(DockState.DockTopAutoHide) > 0)
            {
                base.DockPadding.Top = num;
            }
            if (this.AutoHideStripControl.GetNumberOfPanes(DockState.DockBottomAutoHide) > 0)
            {
                base.DockPadding.Bottom = num;
            }
        }

        protected override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (!this.m_disposed && disposing)
                {
                    this.m_focusManager.Dispose();
                    if (this.m_mdiClientController != null)
                    {
                        this.m_mdiClientController.HandleAssigned -= new EventHandler(this.MdiClientHandleAssigned);
                        this.m_mdiClientController.MdiChildActivate -= new EventHandler(this.ParentFormMdiChildActivate);
                        this.m_mdiClientController.Layout -= new LayoutEventHandler(this.MdiClient_Layout);
                        this.m_mdiClientController.Dispose();
                    }
                    this.FloatWindows.Dispose();
                    this.Panes.Dispose();
                    this.DummyContent.Dispose();
                    this.m_disposed = true;
                }
                base.Dispose(disposing);
            }
        }

        public IDockContent[] DocumentsToArray()
        {
            IDockContent[] contentArray = new IDockContent[this.DocumentsCount];
            int index = 0;
            foreach (IDockContent content in this.Documents)
            {
                contentArray[index] = content;
                index++;
            }
            return contentArray;
        }

        private void DummyControl_Paint(object sender, PaintEventArgs e)
        {
            this.DummyControl.Paint -= this.m_dummyControlPaintEventHandler;
            this.UpdateWindowRegion();
        }

        internal Rectangle GetAutoHideWindowBounds(Rectangle rectAutoHideWindow)
        {
            if (base.Parent == null)
            {
                return Rectangle.Empty;
            }
            return base.Parent.RectangleToClient(base.RectangleToScreen(rectAutoHideWindow));
        }

        private DockDragHandler GetDockDragHandler()
        {
            if (this.m_dockDragHandler == null)
            {
                this.m_dockDragHandler = new DockDragHandler(this);
            }
            return this.m_dockDragHandler;
        }

        private int GetDockWindowSize(DockState dockState)
        {
            int num4;
            if ((dockState == DockState.DockLeft) || (dockState == DockState.DockRight))
            {
                int num = (base.ClientRectangle.Width - base.DockPadding.Left) - base.DockPadding.Right;
                int num2 = (this.m_dockLeftPortion >= 1.0)
                    ? ((int) this.m_dockLeftPortion)
                    : ((int) (num*this.m_dockLeftPortion));
                int num3 = (this.m_dockRightPortion >= 1.0)
                    ? ((int) this.m_dockRightPortion)
                    : ((int) (num*this.m_dockRightPortion));
                if (num2 < 24)
                {
                    num2 = 24;
                }
                if (num3 < 24)
                {
                    num3 = 24;
                }
                if ((num2 + num3) > (num - 24))
                {
                    num4 = (num2 + num3) - (num - 24);
                    num2 -= num4/2;
                    num3 -= num4/2;
                }
                return ((dockState == DockState.DockLeft) ? num2 : num3);
            }
            if ((dockState == DockState.DockTop) || (dockState == DockState.DockBottom))
            {
                int num5 = (base.ClientRectangle.Height - base.DockPadding.Top) - base.DockPadding.Bottom;
                int num6 = (this.m_dockTopPortion >= 1.0)
                    ? ((int) this.m_dockTopPortion)
                    : ((int) (num5*this.m_dockTopPortion));
                int num7 = (this.m_dockBottomPortion >= 1.0)
                    ? ((int) this.m_dockBottomPortion)
                    : ((int) (num5*this.m_dockBottomPortion));
                if (num6 < 24)
                {
                    num6 = 24;
                }
                if (num7 < 24)
                {
                    num7 = 24;
                }
                if ((num6 + num7) > (num5 - 24))
                {
                    num4 = (num6 + num7) - (num5 - 24);
                    num6 -= num4/2;
                    num7 -= num4/2;
                }
                return ((dockState == DockState.DockTop) ? num6 : num7);
            }
            return 0;
        }

        private MdiClientController GetMdiClientController()
        {
            if (this.m_mdiClientController == null)
            {
                this.m_mdiClientController = new MdiClientController();
                this.m_mdiClientController.HandleAssigned += new EventHandler(this.MdiClientHandleAssigned);
                this.m_mdiClientController.MdiChildActivate += new EventHandler(this.ParentFormMdiChildActivate);
                this.m_mdiClientController.Layout += new LayoutEventHandler(this.MdiClient_Layout);
            }
            return this.m_mdiClientController;
        }

        private SplitterDragHandler GetSplitterDragHandler()
        {
            if (this.m_splitterDragHandler == null)
            {
                this.m_splitterDragHandler = new SplitterDragHandler(this);
            }
            return this.m_splitterDragHandler;
        }

        internal Rectangle GetTabStripRectangle(DockState dockState)
        {
            return this.AutoHideStripControl.GetTabStripRectangle(dockState);
        }

        private void InvalidateWindowRegion()
        {
            if (!base.DesignMode)
            {
                if (this.m_dummyControlPaintEventHandler == null)
                {
                    this.m_dummyControlPaintEventHandler = new PaintEventHandler(this.DummyControl_Paint);
                }
                this.DummyControl.Paint += this.m_dummyControlPaintEventHandler;
                this.DummyControl.Invalidate();
            }
        }

        private bool IsClipRectsChanged(Rectangle[] clipRects)
        {
            if ((clipRects != null) || (this.m_clipRects != null))
            {
                bool flag;
                if ((clipRects == null) != (this.m_clipRects == null))
                {
                    return true;
                }
                foreach (Rectangle rectangle in clipRects)
                {
                    flag = false;
                    foreach (Rectangle rectangle2 in this.m_clipRects)
                    {
                        if (rectangle == rectangle2)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return true;
                    }
                }
                foreach (Rectangle rectangle2 in this.m_clipRects)
                {
                    flag = false;
                    foreach (Rectangle rectangle in clipRects)
                    {
                        if (rectangle == rectangle2)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsParentFormValid()
        {
            if ((this.DocumentStyle == DocumentStyle.DockingSdi) || (this.DocumentStyle == DocumentStyle.DockingWindow))
            {
                return true;
            }
            if (!this.MdiClientExists)
            {
                this.GetMdiClientController().RenewMdiClient();
            }
            return this.MdiClientExists;
        }

        public void LoadFromXml(Stream stream, DeserializeDockContent deserializeContent)
        {
            Persistor.LoadFromXml(this, stream, deserializeContent);
        }

        public void LoadFromXml(string fileName, DeserializeDockContent deserializeContent)
        {
            Persistor.LoadFromXml(this, fileName, deserializeContent);
        }

        public void LoadFromXml(Stream stream, DeserializeDockContent deserializeContent, bool closeStream)
        {
            Persistor.LoadFromXml(this, stream, deserializeContent, closeStream);
        }

        private void MdiClient_Layout(object sender, LayoutEventArgs e)
        {
            if (this.DocumentStyle == DocumentStyle.DockingMdi)
            {
                foreach (DockPane pane in this.Panes)
                {
                    if (pane.DockState == DockState.Document)
                    {
                        pane.SetContentBounds();
                    }
                }
                this.InvalidateWindowRegion();
            }
        }

        private void MdiClientHandleAssigned(object sender, EventArgs e)
        {
            this.SetMdiClient();
            base.PerformLayout();
        }

        protected void OnActiveContentChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[ActiveContentChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnActiveDocumentChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[ActiveDocumentChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnActivePaneChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[ActivePaneChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnContentAdded(DockContentEventArgs e)
        {
            EventHandler<DockContentEventArgs> handler =
                (EventHandler<DockContentEventArgs>) base.Events[ContentAddedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnContentRemoved(DockContentEventArgs e)
        {
            EventHandler<DockContentEventArgs> handler =
                (EventHandler<DockContentEventArgs>) base.Events[ContentRemovedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.SuspendLayout(true);
            this.AutoHideStripControl.Bounds = base.ClientRectangle;
            this.CalculateDockPadding();
            this.DockWindows[DockState.DockLeft].Width = this.GetDockWindowSize(DockState.DockLeft);
            this.DockWindows[DockState.DockRight].Width = this.GetDockWindowSize(DockState.DockRight);
            this.DockWindows[DockState.DockTop].Height = this.GetDockWindowSize(DockState.DockTop);
            this.DockWindows[DockState.DockBottom].Height = this.GetDockWindowSize(DockState.DockBottom);
            this.AutoHideWindow.Bounds = this.GetAutoHideWindowBounds(this.AutoHideWindowRectangle);
            this.DockWindows[DockState.Document].BringToFront();
            this.AutoHideWindow.BringToFront();
            base.OnLayout(levent);
            if ((this.DocumentStyle == DocumentStyle.SystemMdi) && this.MdiClientExists)
            {
                this.SetMdiClientBounds(this.SystemMdiClientBounds);
                this.InvalidateWindowRegion();
            }
            else if (this.DocumentStyle == DocumentStyle.DockingMdi)
            {
                this.InvalidateWindowRegion();
            }
            this.ResumeLayout(true, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(SystemBrushes.AppWorkspace, base.ClientRectangle);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            this.AutoHideWindow.Parent = base.Parent;
            this.GetMdiClientController().ParentForm = base.Parent as Form;
            this.AutoHideWindow.BringToFront();
            base.OnParentChanged(e);
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            foreach (FloatWindow window in this.FloatWindows)
            {
                if (window.RightToLeft != this.RightToLeft)
                {
                    window.RightToLeft = this.RightToLeft;
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (base.Visible)
            {
                this.SetMdiClient();
            }
        }

        private void ParentFormMdiChildActivate(object sender, EventArgs e)
        {
            if (this.GetMdiClientController().ParentForm != null)
            {
                IDockContent activeMdiChild = this.GetMdiClientController().ParentForm.ActiveMdiChild as IDockContent;
                if ((activeMdiChild != null) &&
                    ((activeMdiChild.DockHandler.DockPanel == this) && (activeMdiChild.DockHandler.Pane != null)))
                {
                    activeMdiChild.DockHandler.Pane.ActiveContent = activeMdiChild;
                }
            }
        }

        private void PerformMdiClientLayout()
        {
            if (this.GetMdiClientController().MdiClient != null)
            {
                this.GetMdiClientController().MdiClient.PerformLayout();
            }
        }

        internal Rectangle RectangleToMdiClient(Rectangle rect)
        {
            if (this.MdiClientExists)
            {
                return this.GetMdiClientController().MdiClient.RectangleToClient(rect);
            }
            return Rectangle.Empty;
        }

        internal void RefreshActiveAutoHideContent()
        {
            this.AutoHideWindow.RefreshActiveContent();
        }

        internal void RefreshAutoHideStrip()
        {
            this.AutoHideStripControl.RefreshChanges();
        }

        internal void RemoveContent(IDockContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            if (this.Contents.Contains(content))
            {
                this.Contents.Remove(content);
                this.OnContentRemoved(new DockContentEventArgs(content));
            }
        }

        internal void RemoveFloatWindow(FloatWindow floatWindow)
        {
            if (this.FloatWindows.Contains(floatWindow))
            {
                this.FloatWindows.Remove(floatWindow);
            }
        }

        internal void RemovePane(DockPane pane)
        {
            if (this.Panes.Contains(pane))
            {
                this.Panes.Remove(pane);
            }
        }

        public void ResumeLayout(bool performLayout, bool allWindows)
        {
            this.FocusManager.ResumeFocusTracking();
            base.ResumeLayout(performLayout);
            if (allWindows)
            {
                this.ResumeMdiClientLayout(performLayout);
            }
        }

        private void ResumeMdiClientLayout(bool perform)
        {
            if (this.GetMdiClientController().MdiClient != null)
            {
                this.GetMdiClientController().MdiClient.ResumeLayout(perform);
            }
        }

        public void SaveAsXml(string fileName)
        {
            Persistor.SaveAsXml(this, fileName);
        }

        public void SaveAsXml(Stream stream, Encoding encoding)
        {
            Persistor.SaveAsXml(this, stream, encoding);
        }

        public void SaveAsXml(string fileName, Encoding encoding)
        {
            Persistor.SaveAsXml(this, fileName, encoding);
        }

        public void SaveAsXml(Stream stream, Encoding encoding, bool upstream)
        {
            Persistor.SaveAsXml(this, stream, encoding, upstream);
        }

        internal void SaveFocus()
        {
            this.DummyControl.Focus();
        }

        private void SetMdiClient()
        {
            MdiClientController mdiClientController = this.GetMdiClientController();
            if (this.DocumentStyle == DocumentStyle.DockingMdi)
            {
                mdiClientController.AutoScroll = false;
                mdiClientController.BorderStyle = BorderStyle.None;
                if (this.MdiClientExists)
                {
                    mdiClientController.MdiClient.Dock = DockStyle.Fill;
                }
            }
            else if ((this.DocumentStyle == DocumentStyle.DockingSdi) || (this.DocumentStyle == DocumentStyle.DockingWindow))
            {
                mdiClientController.AutoScroll = true;
                mdiClientController.BorderStyle = BorderStyle.Fixed3D;
                if (this.MdiClientExists)
                {
                    mdiClientController.MdiClient.Dock = DockStyle.Fill;
                }
            }
            else if (this.DocumentStyle == DocumentStyle.SystemMdi)
            {
                mdiClientController.AutoScroll = true;
                mdiClientController.BorderStyle = BorderStyle.Fixed3D;
                if (mdiClientController.MdiClient != null)
                {
                    mdiClientController.MdiClient.Dock = DockStyle.None;
                    mdiClientController.MdiClient.Bounds = this.SystemMdiClientBounds;
                }
            }
        }

        private void SetMdiClientBounds(Rectangle bounds)
        {
            this.GetMdiClientController().MdiClient.Bounds = bounds;
        }

        public void SetPaneIndex(DockPane pane, int index)
        {
            int num = this.Panes.IndexOf(pane);
            if (num == -1)
            {
                throw new ArgumentException(Strings.DockPanel_SetPaneIndex_InvalidPane);
            }
            if (((index < 0) || (index > (this.Panes.Count - 1))) && (index != -1))
            {
                throw new ArgumentOutOfRangeException(Strings.DockPanel_SetPaneIndex_InvalidIndex);
            }
            if ((num != index) && ((num != (this.Panes.Count - 1)) || (index != -1)))
            {
                this.Panes.Remove(pane);
                if (index == -1)
                {
                    this.Panes.Add(pane);
                }
                else if (num < index)
                {
                    this.Panes.AddAt(pane, index - 1);
                }
                else
                {
                    this.Panes.AddAt(pane, index);
                }
            }
        }

        private void SetRegion(Rectangle[] clipRects)
        {
            if (this.IsClipRectsChanged(clipRects))
            {
                this.m_clipRects = clipRects;
                if ((this.m_clipRects == null) || (this.m_clipRects.GetLength(0) == 0))
                {
                    base.Region = null;
                }
                else
                {
                    Region region = new Region(new Rectangle(0, 0, base.Width, base.Height));
                    foreach (Rectangle rectangle in this.m_clipRects)
                    {
                        region.Exclude(rectangle);
                    }
                    base.Region = region;
                }
            }
        }

        private bool ShouldSerializeDefaultFloatWindowSize()
        {
            return (this.DefaultFloatWindowSize != new Size(300, 300));
        }

        public void SuspendLayout(bool allWindows)
        {
            this.FocusManager.SuspendFocusTracking();
            base.SuspendLayout();
            if (allWindows)
            {
                this.SuspendMdiClientLayout();
            }
        }

        private void SuspendMdiClientLayout()
        {
            if (this.GetMdiClientController().MdiClient != null)
            {
                this.GetMdiClientController().MdiClient.PerformLayout();
            }
        }

        public void UpdateDockWindowZOrder(DockStyle dockStyle, bool fullPanelEdge)
        {
            if (dockStyle == DockStyle.Left)
            {
                if (fullPanelEdge)
                {
                    this.DockWindows[DockState.DockLeft].SendToBack();
                }
                else
                {
                    this.DockWindows[DockState.DockLeft].BringToFront();
                }
            }
            else if (dockStyle == DockStyle.Right)
            {
                if (fullPanelEdge)
                {
                    this.DockWindows[DockState.DockRight].SendToBack();
                }
                else
                {
                    this.DockWindows[DockState.DockRight].BringToFront();
                }
            }
            else if (dockStyle == DockStyle.Top)
            {
                if (fullPanelEdge)
                {
                    this.DockWindows[DockState.DockTop].SendToBack();
                }
                else
                {
                    this.DockWindows[DockState.DockTop].BringToFront();
                }
            }
            else if (dockStyle == DockStyle.Bottom)
            {
                if (fullPanelEdge)
                {
                    this.DockWindows[DockState.DockBottom].SendToBack();
                }
                else
                {
                    this.DockWindows[DockState.DockBottom].BringToFront();
                }
            }
        }

        private void UpdateWindowRegion()
        {
            if (this.DocumentStyle == DocumentStyle.DockingMdi)
            {
                this.UpdateWindowRegion_ClipContent();
            }
            else if ((this.DocumentStyle == DocumentStyle.DockingSdi) || (this.DocumentStyle == DocumentStyle.DockingWindow))
            {
                this.UpdateWindowRegion_FullDocumentArea();
            }
            else if (this.DocumentStyle == DocumentStyle.SystemMdi)
            {
                this.UpdateWindowRegion_EmptyDocumentArea();
            }
        }

        private void UpdateWindowRegion_ClipContent()
        {
            int num = 0;
            foreach (DockPane pane in this.Panes)
            {
                if (pane.DockState == DockState.Document)
                {
                    num++;
                }
            }
            if (num == 0)
            {
                this.SetRegion(null);
            }
            else
            {
                Rectangle[] clipRects = new Rectangle[num];
                int index = 0;
                foreach (DockPane pane in this.Panes)
                {
                    if (pane.DockState == DockState.Document)
                    {
                        clipRects[index] = base.RectangleToClient(pane.RectangleToScreen(pane.ContentRectangle));
                        index++;
                    }
                }
                this.SetRegion(clipRects);
            }
        }

        private void UpdateWindowRegion_EmptyDocumentArea()
        {
            Rectangle documentWindowBounds = this.DocumentWindowBounds;
            Rectangle[] clipRects = new Rectangle[] {documentWindowBounds};
            this.SetRegion(clipRects);
        }

        private void UpdateWindowRegion_FullDocumentArea()
        {
            this.SetRegion(null);
        }

        [Browsable(false)]
        public IDockContent ActiveAutoHideContent
        {
            get { return this.AutoHideWindow.ActiveContent; }
            set { this.AutoHideWindow.ActiveContent = value; }
        }

        [Browsable(false)]
        public IDockContent ActiveContent
        {
            get { return this.FocusManager.ActiveContent; }
        }

        [Browsable(false)]
        public IDockContent ActiveDocument
        {
            get { return this.FocusManager.ActiveDocument; }
        }

        [Browsable(false)]
        public DockPane ActiveDocumentPane
        {
            get { return this.FocusManager.ActiveDocumentPane; }
        }

        [Browsable(false)]
        public DockPane ActivePane
        {
            get { return this.FocusManager.ActivePane; }
        }

        [LocalizedCategory("Category_Docking"), DefaultValue(true),
         LocalizedDescription("DockPanel_AllowEndUserDocking_Description")]
        public bool AllowEndUserDocking
        {
            get { return this.m_allowEndUserDocking; }
            set { this.m_allowEndUserDocking = value; }
        }

        [LocalizedDescription("DockPanel_AllowEndUserNestedDocking_Description"), LocalizedCategory("Category_Docking"),
         DefaultValue(true)]
        public bool AllowEndUserNestedDocking
        {
            get { return this.m_allowEndUserNestedDocking; }
            set { this.m_allowEndUserNestedDocking = value; }
        }

        internal Control AutoHideControl
        {
            get { return this.m_autoHideWindow; }
        }

        internal AutoHideStripBase AutoHideStripControl
        {
            get
            {
                if (this.m_autoHideStripControl == null)
                {
                    this.m_autoHideStripControl = this.AutoHideStripFactory.CreateAutoHideStrip(this);
                    base.Controls.Add(this.m_autoHideStripControl);
                }
                return this.m_autoHideStripControl;
            }
        }

        internal DockPanelExtender.IAutoHideStripFactory AutoHideStripFactory
        {
            get { return this.Extender.AutoHideStripFactory; }
        }

        private AutoHideWindowControl AutoHideWindow
        {
            get { return this.m_autoHideWindow; }
        }

        internal Rectangle AutoHideWindowRectangle
        {
            get
            {
                DockState dockState = this.AutoHideWindow.DockState;
                Rectangle dockArea = this.DockArea;
                if (this.ActiveAutoHideContent == null)
                {
                    return Rectangle.Empty;
                }
                if (base.Parent == null)
                {
                    return Rectangle.Empty;
                }
                Rectangle empty = Rectangle.Empty;
                double autoHidePortion = this.ActiveAutoHideContent.DockHandler.AutoHidePortion;
                switch (dockState)
                {
                    case DockState.DockLeftAutoHide:
                        if (autoHidePortion < 1.0)
                        {
                            autoHidePortion = dockArea.Width*autoHidePortion;
                        }
                        if (autoHidePortion > (dockArea.Width - 24))
                        {
                            autoHidePortion = dockArea.Width - 24;
                        }
                        empty.X = dockArea.X;
                        empty.Y = dockArea.Y;
                        empty.Width = (int) autoHidePortion;
                        empty.Height = dockArea.Height;
                        return empty;

                    case DockState.DockRightAutoHide:
                        if (autoHidePortion < 1.0)
                        {
                            autoHidePortion = dockArea.Width*autoHidePortion;
                        }
                        if (autoHidePortion > (dockArea.Width - 24))
                        {
                            autoHidePortion = dockArea.Width - 24;
                        }
                        empty.X = (dockArea.X + dockArea.Width) - ((int) autoHidePortion);
                        empty.Y = dockArea.Y;
                        empty.Width = (int) autoHidePortion;
                        empty.Height = dockArea.Height;
                        return empty;

                    case DockState.DockTopAutoHide:
                        if (autoHidePortion < 1.0)
                        {
                            autoHidePortion = dockArea.Height*autoHidePortion;
                        }
                        if (autoHidePortion > (dockArea.Height - 24))
                        {
                            autoHidePortion = dockArea.Height - 24;
                        }
                        empty.X = dockArea.X;
                        empty.Y = dockArea.Y;
                        empty.Width = dockArea.Width;
                        empty.Height = (int) autoHidePortion;
                        return empty;

                    case DockState.DockBottomAutoHide:
                        if (autoHidePortion < 1.0)
                        {
                            autoHidePortion = dockArea.Height*autoHidePortion;
                        }
                        if (autoHidePortion > (dockArea.Height - 24))
                        {
                            autoHidePortion = dockArea.Height - 24;
                        }
                        empty.X = dockArea.X;
                        empty.Y = (dockArea.Y + dockArea.Height) - ((int) autoHidePortion);
                        empty.Width = dockArea.Width;
                        empty.Height = (int) autoHidePortion;
                        return empty;
                }
                return empty;
            }
        }

        internal IContentFocusManager ContentFocusManager
        {
            get { return this.m_focusManager; }
        }

        [Browsable(false)]
        public DockContentCollection Contents
        {
            get { return this.m_contents; }
        }

        [LocalizedDescription("DockPanel_DefaultFloatWindowSize_Description"), Category("Layout")]
        public Size DefaultFloatWindowSize
        {
            get { return this.m_defaultFloatWindowSize; }
            set { this.m_defaultFloatWindowSize = value; }
        }

        internal Rectangle DockArea
        {
            get
            {
                return new Rectangle(base.DockPadding.Left, base.DockPadding.Top,
                    (base.ClientRectangle.Width - base.DockPadding.Left) - base.DockPadding.Right,
                    (base.ClientRectangle.Height - base.DockPadding.Top) - base.DockPadding.Bottom);
            }
        }

        [LocalizedCategory("Category_Docking"), LocalizedDescription("DockPanel_DockBottomPortion_Description"),
         DefaultValue((double) 0.25)]
        public double DockBottomPortion
        {
            get { return this.m_dockBottomPortion; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value != this.m_dockBottomPortion)
                {
                    this.m_dockBottomPortion = value;
                    if (((this.m_dockBottomPortion < 1.0) && (this.m_dockTopPortion < 1.0)) &&
                        ((this.m_dockTopPortion + this.m_dockBottomPortion) > 1.0))
                    {
                        this.m_dockTopPortion = 1.0 - this.m_dockBottomPortion;
                    }
                    base.PerformLayout();
                }
            }
        }

        [LocalizedCategory("Category_Docking"), LocalizedDescription("DockPanel_DockLeftPortion_Description"),
         DefaultValue((double) 0.25)]
        public double DockLeftPortion
        {
            get { return this.m_dockLeftPortion; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value != this.m_dockLeftPortion)
                {
                    this.m_dockLeftPortion = value;
                    if (((this.m_dockLeftPortion < 1.0) && (this.m_dockRightPortion < 1.0)) &&
                        ((this.m_dockLeftPortion + this.m_dockRightPortion) > 1.0))
                    {
                        this.m_dockRightPortion = 1.0 - this.m_dockLeftPortion;
                    }
                    base.PerformLayout();
                }
            }
        }

        internal DockPanelExtender.IDockPaneCaptionFactory DockPaneCaptionFactory
        {
            get { return this.Extender.DockPaneCaptionFactory; }
        }

        public DockPanelExtender.IDockPaneFactory DockPaneFactory
        {
            get { return this.Extender.DockPaneFactory; }
        }

        internal DockPanelExtender.IDockPaneStripFactory DockPaneStripFactory
        {
            get { return this.Extender.DockPaneStripFactory; }
        }

        [DefaultValue((double) 0.25), LocalizedDescription("DockPanel_DockRightPortion_Description"),
         LocalizedCategory("Category_Docking")]
        public double DockRightPortion
        {
            get { return this.m_dockRightPortion; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value != this.m_dockRightPortion)
                {
                    this.m_dockRightPortion = value;
                    if (((this.m_dockLeftPortion < 1.0) && (this.m_dockRightPortion < 1.0)) &&
                        ((this.m_dockLeftPortion + this.m_dockRightPortion) > 1.0))
                    {
                        this.m_dockLeftPortion = 1.0 - this.m_dockRightPortion;
                    }
                    base.PerformLayout();
                }
            }
        }

        [LocalizedCategory("Category_Docking"), LocalizedDescription("DockPanel_DockTopPortion_Description"),
         DefaultValue((double) 0.25)]
        public double DockTopPortion
        {
            get { return this.m_dockTopPortion; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value != this.m_dockTopPortion)
                {
                    this.m_dockTopPortion = value;
                    if (((this.m_dockTopPortion < 1.0) && (this.m_dockBottomPortion < 1.0)) &&
                        ((this.m_dockTopPortion + this.m_dockBottomPortion) > 1.0))
                    {
                        this.m_dockBottomPortion = 1.0 - this.m_dockTopPortion;
                    }
                    base.PerformLayout();
                }
            }
        }

        [Browsable(false)]
        public DockWindowCollection DockWindows
        {
            get { return this.m_dockWindows; }
        }

        private Rectangle DocumentRectangle
        {
            get
            {
                Rectangle dockArea = this.DockArea;
                if (this.DockWindows[DockState.DockLeft].VisibleNestedPanes.Count != 0)
                {
                    dockArea.X += (int) (this.DockArea.Width*this.DockLeftPortion);
                    dockArea.Width -= (int) (this.DockArea.Width*this.DockLeftPortion);
                }
                if (this.DockWindows[DockState.DockRight].VisibleNestedPanes.Count != 0)
                {
                    dockArea.Width -= (int) (this.DockArea.Width*this.DockRightPortion);
                }
                if (this.DockWindows[DockState.DockTop].VisibleNestedPanes.Count != 0)
                {
                    dockArea.Y += (int) (this.DockArea.Height*this.DockTopPortion);
                    dockArea.Height -= (int) (this.DockArea.Height*this.DockTopPortion);
                }
                if (this.DockWindows[DockState.DockBottom].VisibleNestedPanes.Count != 0)
                {
                    dockArea.Height -= (int) (this.DockArea.Height*this.DockBottomPortion);
                }
                return dockArea;
            }
        }

        public IEnumerable<IDockContent> Documents
        {
            get
            {
                foreach (IDockContent iteratorVariable0 in this.Contents)
                {
                    if (iteratorVariable0.DockHandler.DockState != DockState.Document)
                    {
                        continue;
                    }
                    yield return iteratorVariable0;
                }
            }
        }

        public int DocumentsCount
        {
            get
            {
                int num = 0;
                foreach (IDockContent content in this.Documents)
                {
                    num++;
                }
                return num;
            }
        }

        [DefaultValue(0), LocalizedDescription("DockPanel_DocumentStyle_Description"),
         LocalizedCategory("Category_Docking")]
        public DocumentStyle DocumentStyle
        {
            get { return this.m_documentStyle; }
            set
            {
                if (value != this.m_documentStyle)
                {
                    if (!Enum.IsDefined(typeof(DocumentStyle), value))
                    {
                        throw new InvalidEnumArgumentException();
                    }
                    if ((value == DocumentStyle.SystemMdi) &&
                        (this.DockWindows[DockState.Document].VisibleNestedPanes.Count > 0))
                    {
                        throw new InvalidEnumArgumentException();
                    }
                    this.m_documentStyle = value;
                    this.SuspendLayout(true);
                    this.SetMdiClient();
                    this.InvalidateWindowRegion();
                    foreach (IDockContent content in this.Contents)
                    {
                        if (content.DockHandler.DockState == DockState.Document)
                        {
                            content.DockHandler.SetPaneAndVisible(content.DockHandler.Pane);
                        }
                    }
                    this.PerformMdiClientLayout();
                    this.ResumeLayout(true, true);
                }
            }
        }

        internal Rectangle DocumentWindowBounds
        {
            get
            {
                Rectangle displayRectangle = this.DisplayRectangle;
                if (this.DockWindows[DockState.DockLeft].Visible)
                {
                    displayRectangle.X += this.DockWindows[DockState.DockLeft].Width;
                    displayRectangle.Width -= this.DockWindows[DockState.DockLeft].Width;
                }
                if (this.DockWindows[DockState.DockRight].Visible)
                {
                    displayRectangle.Width -= this.DockWindows[DockState.DockRight].Width;
                }
                if (this.DockWindows[DockState.DockTop].Visible)
                {
                    displayRectangle.Y += this.DockWindows[DockState.DockTop].Height;
                    displayRectangle.Height -= this.DockWindows[DockState.DockTop].Height;
                }
                if (this.DockWindows[DockState.DockBottom].Visible)
                {
                    displayRectangle.Height -= this.DockWindows[DockState.DockBottom].Height;
                }
                return displayRectangle;
            }
        }

        internal DockContent DummyContent
        {
            get { return this.m_dummyContent; }
        }

        private Control DummyControl
        {
            get { return this.m_dummyControl; }
        }

        [Browsable(false)]
        public DockPanelExtender Extender
        {
            get { return this.m_extender; }
        }

        public DockPanelExtender.IFloatWindowFactory FloatWindowFactory
        {
            get { return this.Extender.FloatWindowFactory; }
        }

        [Browsable(false)]
        public FloatWindowCollection FloatWindows
        {
            get { return this.m_floatWindows; }
        }

        private IFocusManager FocusManager
        {
            get { return this.m_focusManager; }
        }

        private bool MdiClientExists
        {
            get { return (this.GetMdiClientController().MdiClient != null); }
        }

        [Browsable(false)]
        public DockPaneCollection Panes
        {
            get { return this.m_panes; }
        }

        internal Form ParentForm
        {
            get
            {
                if (!this.IsParentFormValid())
                {
                    throw new InvalidOperationException(Strings.DockPanel_ParentForm_Invalid);
                }
                return this.GetMdiClientController().ParentForm;
            }
        }

        [LocalizedCategory("Appearance"), DefaultValue(false),
         LocalizedDescription("DockPanel_RightToLeftLayout_Description")]
        public bool RightToLeftLayout
        {
            get { return this.m_rightToLeftLayout; }
            set
            {
                if (this.m_rightToLeftLayout != value)
                {
                    this.m_rightToLeftLayout = value;
                    foreach (FloatWindow window in this.FloatWindows)
                    {
                        window.RightToLeftLayout = value;
                    }
                }
            }
        }

        [LocalizedCategory("Category_Docking"), DefaultValue(false),
         LocalizedDescription("DockPanel_ShowDocumentIcon_Description")]
        public bool ShowDocumentIcon
        {
            get { return this.m_showDocumentIcon; }
            set
            {
                if (this.m_showDocumentIcon != value)
                {
                    this.m_showDocumentIcon = value;
                    this.Refresh();
                }
            }
        }

        private Rectangle SystemMdiClientBounds
        {
            get
            {
                if (!(this.IsParentFormValid() && base.Visible))
                {
                    return Rectangle.Empty;
                }
                return this.ParentForm.RectangleToClient(base.RectangleToScreen(this.DocumentWindowBounds));
            }
        }


        private class AutoHideWindowControl : Panel, ISplitterDragSource, IDragSource
        {
            private const int ANIMATE_TIME = 100;
            private IDockContent m_activeContent = null;
            private DockPane m_activePane = null;
            private DockPanel m_dockPanel = null;
            private bool m_flagAnimate = true;
            private bool m_flagDragging = false;
            private SplitterControl m_splitter;
            private System.Windows.Forms.Timer m_timerMouseTrack;

            public AutoHideWindowControl(DockPanel dockPanel)
            {
                this.m_dockPanel = dockPanel;
                this.m_timerMouseTrack = new System.Windows.Forms.Timer();
                this.m_timerMouseTrack.Tick += new EventHandler(this.TimerMouseTrack_Tick);
                base.Visible = false;
                this.m_splitter = new SplitterControl(this);
                base.Controls.Add(this.m_splitter);
            }

            private void AnimateWindow(bool show)
            {
                if (!(this.FlagAnimate || (base.Visible == show)))
                {
                    base.Visible = show;
                }
                else
                {
                    int num2;
                    int num3;
                    int num4;
                    base.Parent.SuspendLayout();
                    Rectangle rect = this.GetRectangle(!show);
                    Rectangle rectangle = this.GetRectangle(show);
                    int num = num2 = num3 = num4 = 0;
                    if (this.DockState == DockState.DockTopAutoHide)
                    {
                        num4 = show ? 1 : -1;
                    }
                    else if (this.DockState == DockState.DockLeftAutoHide)
                    {
                        num3 = show ? 1 : -1;
                    }
                    else if (this.DockState == DockState.DockRightAutoHide)
                    {
                        num = show ? -1 : 1;
                        num3 = show ? 1 : -1;
                    }
                    else if (this.DockState == DockState.DockBottomAutoHide)
                    {
                        num2 = show ? -1 : 1;
                        num4 = show ? 1 : -1;
                    }
                    if (show)
                    {
                        base.Bounds =
                            this.DockPanel.GetAutoHideWindowBounds(new Rectangle(-rectangle.Width, -rectangle.Height,
                                rectangle.Width, rectangle.Height));
                        if (!base.Visible)
                        {
                            base.Visible = true;
                        }
                        base.PerformLayout();
                    }
                    base.SuspendLayout();
                    this.LayoutAnimateWindow(rect);
                    if (!base.Visible)
                    {
                        base.Visible = true;
                    }
                    int num5 = 1;
                    int num7 = (rect.Width != rectangle.Width)
                        ? Math.Abs((int) (rect.Width - rectangle.Width))
                        : Math.Abs((int) (rect.Height - rectangle.Height));
                    DateTime now = DateTime.Now;
                    while (rect != rectangle)
                    {
                        TimeSpan span;
                        bool flag;
                        DateTime time2 = DateTime.Now;
                        rect.X += num*num5;
                        rect.Y += num2*num5;
                        rect.Width += num3*num5;
                        rect.Height += num4*num5;
                        if (Math.Sign((int) (rectangle.X - rect.X)) != Math.Sign(num))
                        {
                            rect.X = rectangle.X;
                        }
                        if (Math.Sign((int) (rectangle.Y - rect.Y)) != Math.Sign(num2))
                        {
                            rect.Y = rectangle.Y;
                        }
                        if (Math.Sign((int) (rectangle.Width - rect.Width)) != Math.Sign(num3))
                        {
                            rect.Width = rectangle.Width;
                        }
                        if (Math.Sign((int) (rectangle.Height - rect.Height)) != Math.Sign(num4))
                        {
                            rect.Height = rectangle.Height;
                        }
                        this.LayoutAnimateWindow(rect);
                        if (base.Parent != null)
                        {
                            base.Parent.Update();
                        }
                        num7 -= num5;
                        goto Label_036B;
                        Label_02EE:
                        span = new TimeSpan(0, 0, 0, 0, 100);
                        TimeSpan span2 = (TimeSpan) (DateTime.Now - time2);
                        TimeSpan span3 = (TimeSpan) (DateTime.Now - now);
                        TimeSpan span4 = span - span3;
                        if (((int) span4.TotalMilliseconds) <= 0)
                        {
                            num5 = num7;
                            continue;
                        }
                        span4 = span - span3;
                        num5 = (num7*((int) span2.TotalMilliseconds))/((int) span4.TotalMilliseconds);
                        if (num5 >= 1)
                        {
                            continue;
                        }
                        Label_036B:
                        flag = true;
                        goto Label_02EE;
                    }
                    base.ResumeLayout();
                    base.Parent.ResumeLayout();
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.m_timerMouseTrack.Dispose();
                }
                base.Dispose(disposing);
            }

            private Rectangle GetRectangle(bool show)
            {
                if (this.DockState == DockState.Unknown)
                {
                    return Rectangle.Empty;
                }
                Rectangle autoHideWindowRectangle = this.DockPanel.AutoHideWindowRectangle;
                if (!show)
                {
                    if (this.DockState == DockState.DockLeftAutoHide)
                    {
                        autoHideWindowRectangle.Width = 0;
                    }
                    else
                    {
                        if (this.DockState == DockState.DockRightAutoHide)
                        {
                            autoHideWindowRectangle.X += autoHideWindowRectangle.Width;
                            autoHideWindowRectangle.Width = 0;
                            return autoHideWindowRectangle;
                        }
                        if (this.DockState == DockState.DockTopAutoHide)
                        {
                            autoHideWindowRectangle.Height = 0;
                        }
                        else
                        {
                            autoHideWindowRectangle.Y += autoHideWindowRectangle.Height;
                            autoHideWindowRectangle.Height = 0;
                        }
                    }
                }
                return autoHideWindowRectangle;
            }

            void ISplitterDragSource.BeginDrag(Rectangle rectSplitter)
            {
                this.FlagDragging = true;
            }

            void ISplitterDragSource.EndDrag()
            {
                this.FlagDragging = false;
            }

            void ISplitterDragSource.MoveSplitter(int offset)
            {
                Rectangle dockArea = this.DockPanel.DockArea;
                IDockContent activeContent = this.ActiveContent;
                if ((this.DockState == DockState.DockLeftAutoHide) && (dockArea.Width > 0))
                {
                    if (activeContent.DockHandler.AutoHidePortion < 1.0)
                    {
                        DockContentHandler dockHandler = activeContent.DockHandler;
                        dockHandler.AutoHidePortion += ((double) offset)/((double) dockArea.Width);
                    }
                    else
                    {
                        activeContent.DockHandler.AutoHidePortion = base.Width + offset;
                    }
                }
                else if ((this.DockState == DockState.DockRightAutoHide) && (dockArea.Width > 0))
                {
                    if (activeContent.DockHandler.AutoHidePortion < 1.0)
                    {
                        DockContentHandler handler2 = activeContent.DockHandler;
                        handler2.AutoHidePortion -= ((double) offset)/((double) dockArea.Width);
                    }
                    else
                    {
                        activeContent.DockHandler.AutoHidePortion = base.Width - offset;
                    }
                }
                else if ((this.DockState == DockState.DockBottomAutoHide) && (dockArea.Height > 0))
                {
                    if (activeContent.DockHandler.AutoHidePortion < 1.0)
                    {
                        DockContentHandler handler3 = activeContent.DockHandler;
                        handler3.AutoHidePortion -= ((double) offset)/((double) dockArea.Height);
                    }
                    else
                    {
                        activeContent.DockHandler.AutoHidePortion = base.Height - offset;
                    }
                }
                else if ((this.DockState == DockState.DockTopAutoHide) && (dockArea.Height > 0))
                {
                    if (activeContent.DockHandler.AutoHidePortion < 1.0)
                    {
                        DockContentHandler handler4 = activeContent.DockHandler;
                        handler4.AutoHidePortion += ((double) offset)/((double) dockArea.Height);
                    }
                    else
                    {
                        activeContent.DockHandler.AutoHidePortion = base.Height + offset;
                    }
                }
            }

            private void LayoutAnimateWindow(Rectangle rect)
            {
                base.Bounds = this.DockPanel.GetAutoHideWindowBounds(rect);
                Rectangle clientRectangle = base.ClientRectangle;
                if (this.DockState == DockState.DockLeftAutoHide)
                {
                    this.ActivePane.Location = new Point(((clientRectangle.Right - 2) - 4) - this.ActivePane.Width,
                        this.ActivePane.Location.Y);
                }
                else if (this.DockState == DockState.DockTopAutoHide)
                {
                    this.ActivePane.Location = new Point(this.ActivePane.Location.X,
                        ((clientRectangle.Bottom - 2) - 4) - this.ActivePane.Height);
                }
            }

            protected override void OnLayout(LayoutEventArgs levent)
            {
                base.DockPadding.All = 0;
                if (this.DockState == DockState.DockLeftAutoHide)
                {
                    base.DockPadding.Right = 2;
                    this.m_splitter.Dock = DockStyle.Right;
                }
                else if (this.DockState == DockState.DockRightAutoHide)
                {
                    base.DockPadding.Left = 2;
                    this.m_splitter.Dock = DockStyle.Left;
                }
                else if (this.DockState == DockState.DockTopAutoHide)
                {
                    base.DockPadding.Bottom = 2;
                    this.m_splitter.Dock = DockStyle.Bottom;
                }
                else if (this.DockState == DockState.DockBottomAutoHide)
                {
                    base.DockPadding.Top = 2;
                    this.m_splitter.Dock = DockStyle.Top;
                }
                Rectangle displayingRectangle = this.DisplayingRectangle;
                Rectangle rectangle2 = new Rectangle(-displayingRectangle.Width, displayingRectangle.Y,
                    displayingRectangle.Width, displayingRectangle.Height);
                foreach (Control control in base.Controls)
                {
                    DockPane pane = control as DockPane;
                    if (pane != null)
                    {
                        if (pane == this.ActivePane)
                        {
                            pane.Bounds = displayingRectangle;
                        }
                        else
                        {
                            pane.Bounds = rectangle2;
                        }
                    }
                }
                base.OnLayout(levent);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics graphics = e.Graphics;
                if (this.DockState == DockState.DockBottomAutoHide)
                {
                    graphics.DrawLine(SystemPens.ControlLightLight, 0, 1, base.ClientRectangle.Right, 1);
                }
                else if (this.DockState == DockState.DockRightAutoHide)
                {
                    graphics.DrawLine(SystemPens.ControlLightLight, 1, 0, 1, base.ClientRectangle.Bottom);
                }
                else if (this.DockState == DockState.DockTopAutoHide)
                {
                    graphics.DrawLine(SystemPens.ControlDark, 0, base.ClientRectangle.Height - 2,
                        base.ClientRectangle.Right, base.ClientRectangle.Height - 2);
                    graphics.DrawLine(SystemPens.ControlDarkDark, 0, base.ClientRectangle.Height - 1,
                        base.ClientRectangle.Right, base.ClientRectangle.Height - 1);
                }
                else if (this.DockState == DockState.DockLeftAutoHide)
                {
                    graphics.DrawLine(SystemPens.ControlDark, base.ClientRectangle.Width - 2, 0,
                        base.ClientRectangle.Width - 2, base.ClientRectangle.Bottom);
                    graphics.DrawLine(SystemPens.ControlDarkDark, base.ClientRectangle.Width - 1, 0,
                        base.ClientRectangle.Width - 1, base.ClientRectangle.Bottom);
                }
                base.OnPaint(e);
            }

            public void RefreshActiveContent()
            {
                if ((this.ActiveContent != null) &&
                    !DockHelper.IsDockStateAutoHide(this.ActiveContent.DockHandler.DockState))
                {
                    this.FlagAnimate = false;
                    this.ActiveContent = null;
                    this.FlagAnimate = true;
                }
            }

            public void RefreshActivePane()
            {
                this.SetTimerMouseTrack();
            }

            private void SetActivePane()
            {
                DockPane pane = (this.ActiveContent == null) ? null : this.ActiveContent.DockHandler.Pane;
                if (pane != this.m_activePane)
                {
                    this.m_activePane = pane;
                }
            }

            private void SetTimerMouseTrack()
            {
                if (((this.ActivePane == null) || this.ActivePane.IsActivated) || this.FlagDragging)
                {
                    this.m_timerMouseTrack.Enabled = false;
                }
                else
                {
                    int mouseHoverTime = SystemInformation.MouseHoverTime;
                    if (mouseHoverTime <= 0)
                    {
                        mouseHoverTime = 400;
                    }
                    this.m_timerMouseTrack.Interval = 2*mouseHoverTime;
                    this.m_timerMouseTrack.Enabled = true;
                }
            }

            private void TimerMouseTrack_Tick(object sender, EventArgs e)
            {
                if ((this.ActivePane == null) || this.ActivePane.IsActivated)
                {
                    this.m_timerMouseTrack.Enabled = false;
                }
                else
                {
                    DockPane activePane = this.ActivePane;
                    Point pt = base.PointToClient(Control.MousePosition);
                    Point point2 = this.DockPanel.PointToClient(Control.MousePosition);
                    Rectangle tabStripRectangle = this.DockPanel.GetTabStripRectangle(activePane.DockState);
                    if (!(base.ClientRectangle.Contains(pt) || tabStripRectangle.Contains(point2)))
                    {
                        this.ActiveContent = null;
                        this.m_timerMouseTrack.Enabled = false;
                    }
                }
            }

            public IDockContent ActiveContent
            {
                get { return this.m_activeContent; }
                set
                {
                    if (value != this.m_activeContent)
                    {
                        if ((value != null) &&
                            !(DockHelper.IsDockStateAutoHide(value.DockHandler.DockState) &&
                              (value.DockHandler.DockPanel == this.DockPanel)))
                        {
                            throw new InvalidOperationException(Strings.DockPanel_ActiveAutoHideContent_InvalidValue);
                        }
                        this.DockPanel.SuspendLayout();
                        if (this.m_activeContent != null)
                        {
                            if (this.m_activeContent.DockHandler.Form.ContainsFocus)
                            {
                                this.DockPanel.ContentFocusManager.GiveUpFocus(this.m_activeContent);
                            }
                            this.AnimateWindow(false);
                        }
                        this.m_activeContent = value;
                        this.SetActivePane();
                        if (this.ActivePane != null)
                        {
                            this.ActivePane.ActiveContent = this.m_activeContent;
                        }
                        if (this.m_activeContent != null)
                        {
                            this.AnimateWindow(true);
                        }
                        this.DockPanel.ResumeLayout();
                        this.DockPanel.RefreshAutoHideStrip();
                        this.SetTimerMouseTrack();
                    }
                }
            }

            public DockPane ActivePane
            {
                get { return this.m_activePane; }
            }

            protected virtual Rectangle DisplayingRectangle
            {
                get
                {
                    Rectangle clientRectangle = base.ClientRectangle;
                    if (this.DockState == DockState.DockBottomAutoHide)
                    {
                        clientRectangle.Y += 6;
                        clientRectangle.Height -= 6;
                        return clientRectangle;
                    }
                    if (this.DockState == DockState.DockRightAutoHide)
                    {
                        clientRectangle.X += 6;
                        clientRectangle.Width -= 6;
                        return clientRectangle;
                    }
                    if (this.DockState == DockState.DockTopAutoHide)
                    {
                        clientRectangle.Height -= 6;
                        return clientRectangle;
                    }
                    if (this.DockState == DockState.DockLeftAutoHide)
                    {
                        clientRectangle.Width -= 6;
                    }
                    return clientRectangle;
                }
            }

            public DockPanel DockPanel
            {
                get { return this.m_dockPanel; }
            }

            public DockState DockState
            {
                get
                {
                    return ((this.ActiveContent == null) ? DockState.Unknown : this.ActiveContent.DockHandler.DockState);
                }
            }

            private bool FlagAnimate
            {
                get { return this.m_flagAnimate; }
                set { this.m_flagAnimate = value; }
            }

            internal bool FlagDragging
            {
                get { return this.m_flagDragging; }
                set
                {
                    if (this.m_flagDragging != value)
                    {
                        this.m_flagDragging = value;
                        this.SetTimerMouseTrack();
                    }
                }
            }

            Control IDragSource.DragControl
            {
                get { return this; }
            }

            Rectangle ISplitterDragSource.DragLimitBounds
            {
                get
                {
                    Rectangle dockArea = this.DockPanel.DockArea;
                    if (((ISplitterDragSource) this).IsVertical)
                    {
                        dockArea.X += 24;
                        dockArea.Width -= 48;
                    }
                    else
                    {
                        dockArea.Y += 24;
                        dockArea.Height -= 48;
                    }
                    return this.DockPanel.RectangleToScreen(dockArea);
                }
            }

            bool ISplitterDragSource.IsVertical
            {
                get
                {
                    return ((this.DockState == DockState.DockLeftAutoHide) ||
                            (this.DockState == DockState.DockRightAutoHide));
                }
            }

            private class SplitterControl : SplitterBase
            {
                private DockPanel.AutoHideWindowControl m_autoHideWindow;

                public SplitterControl(DockPanel.AutoHideWindowControl autoHideWindow)
                {
                    this.m_autoHideWindow = autoHideWindow;
                }

                protected override void StartDrag()
                {
                    this.AutoHideWindow.DockPanel.BeginDrag(this.AutoHideWindow,
                        this.AutoHideWindow.RectangleToScreen(base.Bounds));
                }

                private DockPanel.AutoHideWindowControl AutoHideWindow
                {
                    get { return this.m_autoHideWindow; }
                }

                protected override int SplitterSize
                {
                    get { return 4; }
                }
            }
        }

        private sealed class DockDragHandler : DockPanel.DragHandler
        {
            private Rectangle m_floatOutlineBounds;
            private DockIndicator m_indicator;
            private DockOutlineBase m_outline;

            public DockDragHandler(DockPanel panel) : base(panel)
            {
            }

            public void BeginDrag(IDockDragSource dragSource)
            {
                this.DragSource = dragSource;
                if (!base.BeginDrag())
                {
                    this.DragSource = null;
                }
                else
                {
                    this.Outline = new DockOutline();
                    this.Indicator = new DockIndicator(this);
                    this.Indicator.Show(false);
                    this.FloatOutlineBounds = this.DragSource.BeginDrag(base.StartMousePosition);
                }
            }

            private void EndDrag(bool abort)
            {
                if (!abort)
                {
                    if (!this.Outline.FloatWindowBounds.IsEmpty)
                    {
                        this.DragSource.FloatAt(this.Outline.FloatWindowBounds);
                    }
                    else if (this.Outline.DockTo is DockPane)
                    {
                        DockPane dockTo = this.Outline.DockTo as DockPane;
                        this.DragSource.DockTo(dockTo, this.Outline.Dock, this.Outline.ContentIndex);
                    }
                    else if (this.Outline.DockTo is DockPanel)
                    {
                        DockPanel panel = this.Outline.DockTo as DockPanel;
                        panel.UpdateDockWindowZOrder(this.Outline.Dock, this.Outline.FlagFullEdge);
                        this.DragSource.DockTo(panel, this.Outline.Dock);
                    }
                }
            }

            protected override void OnDragging()
            {
                this.TestDrop();
            }

            protected override void OnEndDrag(bool abort)
            {
                base.DockPanel.SuspendLayout(true);
                this.Outline.Close();
                this.Indicator.Close();
                this.EndDrag(abort);
                base.DockPanel.ResumeLayout(true, true);
                this.DragSource = null;
            }

            private void TestDrop()
            {
                this.Outline.FlagTestDrop = false;
                this.Indicator.FullPanelEdge = (Control.ModifierKeys & Keys.Shift) != Keys.None;
                if ((Control.ModifierKeys & Keys.Control) == Keys.None)
                {
                    this.Indicator.TestDrop();
                    if (!this.Outline.FlagTestDrop)
                    {
                        DockPane pane = DockHelper.PaneAtPoint(Control.MousePosition, base.DockPanel);
                        if ((pane != null) && this.DragSource.IsDockStateValid(pane.DockState))
                        {
                            pane.TestDrop(this.DragSource, this.Outline);
                        }
                    }
                    if (!this.Outline.FlagTestDrop && this.DragSource.IsDockStateValid(DockState.Float))
                    {
                        FloatWindow window = DockHelper.FloatWindowAtPoint(Control.MousePosition, base.DockPanel);
                        if (window != null)
                        {
                            window.TestDrop(this.DragSource, this.Outline);
                        }
                    }
                }
                else
                {
                    this.Indicator.DockPane = DockHelper.PaneAtPoint(Control.MousePosition, base.DockPanel);
                }
                if (!this.Outline.FlagTestDrop && this.DragSource.IsDockStateValid(DockState.Float))
                {
                    Rectangle floatOutlineBounds = this.FloatOutlineBounds;
                    floatOutlineBounds.Offset(Control.MousePosition.X - base.StartMousePosition.X,
                        Control.MousePosition.Y - base.StartMousePosition.Y);
                    this.Outline.Show(floatOutlineBounds);
                }
                if (!this.Outline.FlagTestDrop)
                {
                    Cursor.Current = Cursors.No;
                    this.Outline.Show();
                }
                else
                {
                    Cursor.Current = this.DragControl.Cursor;
                }
            }

            public IDockDragSource DragSource
            {
                get { return (base.DragSource as IDockDragSource); }
                set { base.DragSource = value; }
            }

            private Rectangle FloatOutlineBounds
            {
                get { return this.m_floatOutlineBounds; }
                set { this.m_floatOutlineBounds = value; }
            }

            private DockIndicator Indicator
            {
                get { return this.m_indicator; }
                set { this.m_indicator = value; }
            }

            public DockOutlineBase Outline
            {
                get { return this.m_outline; }
                private set { this.m_outline = value; }
            }

            private class DockIndicator : DragForm
            {
                private int _PanelIndicatorMargin = 10;
                private DockPane m_dockPane = null;
                private DockPanel.DockDragHandler m_dragHandler;
                private bool m_fullPanelEdge = false;
                private IHitTest m_hitTest = null;
                private PaneIndicator m_paneDiamond = null;
                private PanelIndicator m_panelBottom = null;
                private PanelIndicator m_panelFill = null;
                private PanelIndicator m_panelLeft = null;
                private PanelIndicator m_panelRight = null;
                private PanelIndicator m_panelTop = null;

                public DockIndicator(DockPanel.DockDragHandler dragHandler)
                {
                    this.m_dragHandler = dragHandler;
                    base.Controls.AddRange(new Control[]
                    {
                        this.PaneDiamond, this.PanelLeft, this.PanelRight, this.PanelTop, this.PanelBottom,
                        this.PanelFill
                    });
                    base.Region = new Region(Rectangle.Empty);
                }

                private static Rectangle GetAllScreenBounds()
                {
                    Rectangle rectangle = new Rectangle(0, 0, 0, 0);
                    foreach (Screen screen in Screen.AllScreens)
                    {
                        Rectangle bounds = screen.Bounds;
                        if (bounds.Left < rectangle.Left)
                        {
                            rectangle.Width += rectangle.Left - bounds.Left;
                            rectangle.X = bounds.X;
                        }
                        if (bounds.Right > rectangle.Right)
                        {
                            rectangle.Width += bounds.Right - rectangle.Right;
                        }
                        if (bounds.Top < rectangle.Top)
                        {
                            rectangle.Height += rectangle.Top - bounds.Top;
                            rectangle.Y = bounds.Y;
                        }
                        if (bounds.Bottom > rectangle.Bottom)
                        {
                            rectangle.Height += bounds.Bottom - rectangle.Bottom;
                        }
                    }
                    return rectangle;
                }

                private void RefreshChanges()
                {
                    Region region = new Region(Rectangle.Empty);
                    Rectangle r = this.FullPanelEdge ? this.DockPanel.DockArea : this.DockPanel.DocumentWindowBounds;
                    r = base.RectangleToClient(this.DockPanel.RectangleToScreen(r));
                    if (this.ShouldPanelIndicatorVisible(DockState.DockLeft))
                    {
                        this.PanelLeft.Location = new Point(r.X + this._PanelIndicatorMargin,
                            r.Y + ((r.Height - this.PanelRight.Height)/2));
                        this.PanelLeft.Visible = true;
                        region.Union(this.PanelLeft.Bounds);
                    }
                    else
                    {
                        this.PanelLeft.Visible = false;
                    }
                    if (this.ShouldPanelIndicatorVisible(DockState.DockRight))
                    {
                        this.PanelRight.Location =
                            new Point(((r.X + r.Width) - this.PanelRight.Width) - this._PanelIndicatorMargin,
                                r.Y + ((r.Height - this.PanelRight.Height)/2));
                        this.PanelRight.Visible = true;
                        region.Union(this.PanelRight.Bounds);
                    }
                    else
                    {
                        this.PanelRight.Visible = false;
                    }
                    if (this.ShouldPanelIndicatorVisible(DockState.DockTop))
                    {
                        this.PanelTop.Location = new Point(r.X + ((r.Width - this.PanelTop.Width)/2),
                            r.Y + this._PanelIndicatorMargin);
                        this.PanelTop.Visible = true;
                        region.Union(this.PanelTop.Bounds);
                    }
                    else
                    {
                        this.PanelTop.Visible = false;
                    }
                    if (this.ShouldPanelIndicatorVisible(DockState.DockBottom))
                    {
                        this.PanelBottom.Location = new Point(r.X + ((r.Width - this.PanelBottom.Width)/2),
                            ((r.Y + r.Height) - this.PanelBottom.Height) - this._PanelIndicatorMargin);
                        this.PanelBottom.Visible = true;
                        region.Union(this.PanelBottom.Bounds);
                    }
                    else
                    {
                        this.PanelBottom.Visible = false;
                    }
                    if (this.ShouldPanelIndicatorVisible(DockState.Document))
                    {
                        Rectangle rectangle2 =
                            base.RectangleToClient(this.DockPanel.RectangleToScreen(this.DockPanel.DocumentWindowBounds));
                        this.PanelFill.Location = new Point(
                            rectangle2.X + ((rectangle2.Width - this.PanelFill.Width)/2),
                            rectangle2.Y + ((rectangle2.Height - this.PanelFill.Height)/2));
                        this.PanelFill.Visible = true;
                        region.Union(this.PanelFill.Bounds);
                    }
                    else
                    {
                        this.PanelFill.Visible = false;
                    }
                    if (this.ShouldPaneDiamondVisible())
                    {
                        Rectangle rectangle3 =
                            base.RectangleToClient(this.DockPane.RectangleToScreen(this.DockPane.ClientRectangle));
                        this.PaneDiamond.Location =
                            new Point(rectangle3.Left + ((rectangle3.Width - this.PaneDiamond.Width)/2),
                                rectangle3.Top + ((rectangle3.Height - this.PaneDiamond.Height)/2));
                        this.PaneDiamond.Visible = true;
                        using (GraphicsPath path = PaneIndicator.DisplayingGraphicsPath.Clone() as GraphicsPath)
                        {
                            Point[] plgpts = new Point[]
                            {
                                new Point(this.PaneDiamond.Left, this.PaneDiamond.Top),
                                new Point(this.PaneDiamond.Right, this.PaneDiamond.Top),
                                new Point(this.PaneDiamond.Left, this.PaneDiamond.Bottom)
                            };
                            using (Matrix matrix = new Matrix(this.PaneDiamond.ClientRectangle, plgpts))
                            {
                                path.Transform(matrix);
                            }
                            region.Union(path);
                        }
                    }
                    else
                    {
                        this.PaneDiamond.Visible = false;
                    }
                    base.Region = region;
                }

                private bool ShouldPaneDiamondVisible()
                {
                    if (this.DockPane == null)
                    {
                        return false;
                    }
                    if (!this.DockPanel.AllowEndUserNestedDocking)
                    {
                        return false;
                    }
                    return this.DragHandler.DragSource.CanDockTo(this.DockPane);
                }

                private bool ShouldPanelIndicatorVisible(DockState dockState)
                {
                    if (!base.Visible)
                    {
                        return false;
                    }
                    if (this.DockPanel.DockWindows[dockState].Visible)
                    {
                        return false;
                    }
                    return this.DragHandler.DragSource.IsDockStateValid(dockState);
                }

                public override void Show(bool bActivate)
                {
                    base.Bounds = GetAllScreenBounds();
                    base.Show(bActivate);
                    this.RefreshChanges();
                }

                public void TestDrop()
                {
                    Point mousePosition = Control.MousePosition;
                    this.DockPane = DockHelper.PaneAtPoint(mousePosition, this.DockPanel);
                    if (TestDrop(this.PanelLeft, mousePosition) != DockStyle.None)
                    {
                        this.HitTestResult = this.PanelLeft;
                    }
                    else if (TestDrop(this.PanelRight, mousePosition) != DockStyle.None)
                    {
                        this.HitTestResult = this.PanelRight;
                    }
                    else if (TestDrop(this.PanelTop, mousePosition) != DockStyle.None)
                    {
                        this.HitTestResult = this.PanelTop;
                    }
                    else if (TestDrop(this.PanelBottom, mousePosition) != DockStyle.None)
                    {
                        this.HitTestResult = this.PanelBottom;
                    }
                    else if (TestDrop(this.PanelFill, mousePosition) != DockStyle.None)
                    {
                        this.HitTestResult = this.PanelFill;
                    }
                    else if (TestDrop(this.PaneDiamond, mousePosition) != DockStyle.None)
                    {
                        this.HitTestResult = this.PaneDiamond;
                    }
                    else
                    {
                        this.HitTestResult = null;
                    }
                    if (this.HitTestResult != null)
                    {
                        if (this.HitTestResult is PaneIndicator)
                        {
                            this.DragHandler.Outline.Show(this.DockPane, this.HitTestResult.Status);
                        }
                        else
                        {
                            this.DragHandler.Outline.Show(this.DockPanel, this.HitTestResult.Status, this.FullPanelEdge);
                        }
                    }
                }

                private static DockStyle TestDrop(IHitTest hitTest, Point pt)
                {
                    return (hitTest.Status = hitTest.HitTest(pt));
                }

                private DockPane DisplayingPane
                {
                    get { return (this.ShouldPaneDiamondVisible() ? this.DockPane : null); }
                }

                public DockPane DockPane
                {
                    get { return this.m_dockPane; }
                    internal set
                    {
                        if (this.m_dockPane != value)
                        {
                            DockPane displayingPane = this.DisplayingPane;
                            this.m_dockPane = value;
                            if (displayingPane != this.DisplayingPane)
                            {
                                this.RefreshChanges();
                            }
                        }
                    }
                }

                public DockPanel DockPanel
                {
                    get { return this.DragHandler.DockPanel; }
                }

                public DockPanel.DockDragHandler DragHandler
                {
                    get { return this.m_dragHandler; }
                }

                public bool FullPanelEdge
                {
                    get { return this.m_fullPanelEdge; }
                    set
                    {
                        if (this.m_fullPanelEdge != value)
                        {
                            this.m_fullPanelEdge = value;
                            this.RefreshChanges();
                        }
                    }
                }

                private IHitTest HitTestResult
                {
                    get { return this.m_hitTest; }
                    set
                    {
                        if (this.m_hitTest != value)
                        {
                            if (this.m_hitTest != null)
                            {
                                this.m_hitTest.Status = DockStyle.None;
                            }
                            this.m_hitTest = value;
                        }
                    }
                }

                private PaneIndicator PaneDiamond
                {
                    get
                    {
                        if (this.m_paneDiamond == null)
                        {
                            this.m_paneDiamond = new PaneIndicator();
                        }
                        return this.m_paneDiamond;
                    }
                }

                private PanelIndicator PanelBottom
                {
                    get
                    {
                        if (this.m_panelBottom == null)
                        {
                            this.m_panelBottom = new PanelIndicator(DockStyle.Bottom);
                        }
                        return this.m_panelBottom;
                    }
                }

                private PanelIndicator PanelFill
                {
                    get
                    {
                        if (this.m_panelFill == null)
                        {
                            this.m_panelFill = new PanelIndicator(DockStyle.Fill);
                        }
                        return this.m_panelFill;
                    }
                }

                private PanelIndicator PanelLeft
                {
                    get
                    {
                        if (this.m_panelLeft == null)
                        {
                            this.m_panelLeft = new PanelIndicator(DockStyle.Left);
                        }
                        return this.m_panelLeft;
                    }
                }

                private PanelIndicator PanelRight
                {
                    get
                    {
                        if (this.m_panelRight == null)
                        {
                            this.m_panelRight = new PanelIndicator(DockStyle.Right);
                        }
                        return this.m_panelRight;
                    }
                }

                private PanelIndicator PanelTop
                {
                    get
                    {
                        if (this.m_panelTop == null)
                        {
                            this.m_panelTop = new PanelIndicator(DockStyle.Top);
                        }
                        return this.m_panelTop;
                    }
                }

                private interface IHitTest
                {
                    DockStyle HitTest(Point pt);

                    DockStyle Status { get; set; }
                }

                private class PaneIndicator : PictureBox, DockPanel.DockDragHandler.DockIndicator.IHitTest
                {
                    private static Bitmap _bitmapPaneDiamond = Resources.DockIndicator_PaneDiamond;
                    private static Bitmap _bitmapPaneDiamondBottom = Resources.DockIndicator_PaneDiamond_Bottom;
                    private static Bitmap _bitmapPaneDiamondFill = Resources.DockIndicator_PaneDiamond_Fill;
                    private static Bitmap _bitmapPaneDiamondHotSpot = Resources.DockIndicator_PaneDiamond_HotSpot;

                    private static Bitmap _bitmapPaneDiamondHotSpotIndex =
                        Resources.DockIndicator_PaneDiamond_HotSpotIndex;

                    private static Bitmap _bitmapPaneDiamondLeft = Resources.DockIndicator_PaneDiamond_Left;
                    private static Bitmap _bitmapPaneDiamondRight = Resources.DockIndicator_PaneDiamond_Right;
                    private static Bitmap _bitmapPaneDiamondTop = Resources.DockIndicator_PaneDiamond_Top;

                    private static GraphicsPath _displayingGraphicsPath =
                        DrawHelper.CalculateGraphicsPathFromBitmap(_bitmapPaneDiamond);

                    private static HotSpotIndex[] _hotSpots = new HotSpotIndex[]
                    {
                        new HotSpotIndex(1, 0, DockStyle.Top), new HotSpotIndex(0, 1, DockStyle.Left),
                        new HotSpotIndex(1, 1, DockStyle.Fill), new HotSpotIndex(2, 1, DockStyle.Right),
                        new HotSpotIndex(1, 2, DockStyle.Bottom)
                    };

                    private DockStyle m_status = DockStyle.None;

                    public PaneIndicator()
                    {
                        base.SizeMode = PictureBoxSizeMode.AutoSize;
                        base.Image = _bitmapPaneDiamond;
                        base.Region = new Region(DisplayingGraphicsPath);
                    }

                    public DockStyle HitTest(Point pt)
                    {
                        if (base.Visible)
                        {
                            pt = base.PointToClient(pt);
                            if (!base.ClientRectangle.Contains(pt))
                            {
                                return DockStyle.None;
                            }
                            for (int i = _hotSpots.GetLowerBound(0); i <= _hotSpots.GetUpperBound(0); i++)
                            {
                                if (_bitmapPaneDiamondHotSpot.GetPixel(pt.X, pt.Y) ==
                                    _bitmapPaneDiamondHotSpotIndex.GetPixel(_hotSpots[i].X, _hotSpots[i].Y))
                                {
                                    return _hotSpots[i].DockStyle;
                                }
                            }
                        }
                        return DockStyle.None;
                    }

                    public static GraphicsPath DisplayingGraphicsPath
                    {
                        get { return _displayingGraphicsPath; }
                    }

                    public DockStyle Status
                    {
                        get { return this.m_status; }
                        set
                        {
                            this.m_status = value;
                            if (this.m_status == DockStyle.None)
                            {
                                base.Image = _bitmapPaneDiamond;
                            }
                            else if (this.m_status == DockStyle.Left)
                            {
                                base.Image = _bitmapPaneDiamondLeft;
                            }
                            else if (this.m_status == DockStyle.Right)
                            {
                                base.Image = _bitmapPaneDiamondRight;
                            }
                            else if (this.m_status == DockStyle.Top)
                            {
                                base.Image = _bitmapPaneDiamondTop;
                            }
                            else if (this.m_status == DockStyle.Bottom)
                            {
                                base.Image = _bitmapPaneDiamondBottom;
                            }
                            else if (this.m_status == DockStyle.Fill)
                            {
                                base.Image = _bitmapPaneDiamondFill;
                            }
                        }
                    }

                    [StructLayout(LayoutKind.Sequential)]
                    private struct HotSpotIndex
                    {
                        private int m_x;
                        private int m_y;
                        private System.Windows.Forms.DockStyle m_dockStyle;

                        public HotSpotIndex(int x, int y, System.Windows.Forms.DockStyle dockStyle)
                        {
                            this.m_x = x;
                            this.m_y = y;
                            this.m_dockStyle = dockStyle;
                        }

                        public int X
                        {
                            get { return this.m_x; }
                        }

                        public int Y
                        {
                            get { return this.m_y; }
                        }

                        public System.Windows.Forms.DockStyle DockStyle
                        {
                            get { return this.m_dockStyle; }
                        }
                    }
                }

                private class PanelIndicator : PictureBox, DockPanel.DockDragHandler.DockIndicator.IHitTest
                {
                    private static Image _imagePanelBottom = Resources.DockIndicator_PanelBottom;
                    private static Image _imagePanelBottomActive = Resources.DockIndicator_PanelBottom_Active;
                    private static Image _imagePanelFill = Resources.DockIndicator_PanelFill;
                    private static Image _imagePanelFillActive = Resources.DockIndicator_PanelFill_Active;
                    private static Image _imagePanelLeft = Resources.DockIndicator_PanelLeft;
                    private static Image _imagePanelLeftActive = Resources.DockIndicator_PanelLeft_Active;
                    private static Image _imagePanelRight = Resources.DockIndicator_PanelRight;
                    private static Image _imagePanelRightActive = Resources.DockIndicator_PanelRight_Active;
                    private static Image _imagePanelTop = Resources.DockIndicator_PanelTop;
                    private static Image _imagePanelTopActive = Resources.DockIndicator_PanelTop_Active;
                    private System.Windows.Forms.DockStyle m_dockStyle;
                    private bool m_isActivated = false;
                    private System.Windows.Forms.DockStyle m_status;

                    public PanelIndicator(System.Windows.Forms.DockStyle dockStyle)
                    {
                        this.m_dockStyle = dockStyle;
                        base.SizeMode = PictureBoxSizeMode.AutoSize;
                        base.Image = this.ImageInactive;
                    }

                    public System.Windows.Forms.DockStyle HitTest(Point pt)
                    {
                        return (base.ClientRectangle.Contains(base.PointToClient(pt))
                            ? this.DockStyle
                            : System.Windows.Forms.DockStyle.None);
                    }

                    private System.Windows.Forms.DockStyle DockStyle
                    {
                        get { return this.m_dockStyle; }
                    }

                    private Image ImageActive
                    {
                        get
                        {
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Left)
                            {
                                return _imagePanelLeftActive;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Right)
                            {
                                return _imagePanelRightActive;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Top)
                            {
                                return _imagePanelTopActive;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Bottom)
                            {
                                return _imagePanelBottomActive;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Fill)
                            {
                                return _imagePanelFillActive;
                            }
                            return null;
                        }
                    }

                    private Image ImageInactive
                    {
                        get
                        {
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Left)
                            {
                                return _imagePanelLeft;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Right)
                            {
                                return _imagePanelRight;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Top)
                            {
                                return _imagePanelTop;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Bottom)
                            {
                                return _imagePanelBottom;
                            }
                            if (this.DockStyle == System.Windows.Forms.DockStyle.Fill)
                            {
                                return _imagePanelFill;
                            }
                            return null;
                        }
                    }

                    private bool IsActivated
                    {
                        get { return this.m_isActivated; }
                        set
                        {
                            this.m_isActivated = value;
                            base.Image = this.IsActivated ? this.ImageActive : this.ImageInactive;
                        }
                    }

                    public System.Windows.Forms.DockStyle Status
                    {
                        get { return this.m_status; }
                        set
                        {
                            if ((value != this.DockStyle) && (value != System.Windows.Forms.DockStyle.None))
                            {
                                throw new InvalidEnumArgumentException();
                            }
                            if (this.m_status != value)
                            {
                                this.m_status = value;
                                this.IsActivated = this.m_status != System.Windows.Forms.DockStyle.None;
                            }
                        }
                    }
                }
            }

            private class DockOutline : DockOutlineBase
            {
                private DragForm m_dragForm = new DragForm();

                public DockOutline()
                {
                    this.SetDragForm(Rectangle.Empty);
                    this.DragForm.BackColor = SystemColors.ActiveCaption;
                    this.DragForm.Opacity = 0.5;
                    this.DragForm.Show(false);
                }

                private void CalculateRegion()
                {
                    if (!base.SameAsOldValue)
                    {
                        if (!base.FloatWindowBounds.IsEmpty)
                        {
                            this.SetOutline(base.FloatWindowBounds);
                        }
                        else if (base.DockTo is DockPanel)
                        {
                            this.SetOutline(base.DockTo as DockPanel, base.Dock, base.ContentIndex != 0);
                        }
                        else if (base.DockTo is DockPane)
                        {
                            this.SetOutline(base.DockTo as DockPane, base.Dock, base.ContentIndex);
                        }
                        else
                        {
                            this.SetOutline();
                        }
                    }
                }

                protected override void OnClose()
                {
                    this.DragForm.Close();
                }

                protected override void OnShow()
                {
                    this.CalculateRegion();
                }

                private void SetDragForm(Rectangle rect)
                {
                    this.DragForm.Bounds = rect;
                    if (rect == Rectangle.Empty)
                    {
                        this.DragForm.Region = new Region(Rectangle.Empty);
                    }
                    else if (this.DragForm.Region != null)
                    {
                        this.DragForm.Region = null;
                    }
                }

                private void SetDragForm(Rectangle rect, Region region)
                {
                    this.DragForm.Bounds = rect;
                    this.DragForm.Region = region;
                }

                private void SetOutline()
                {
                    this.SetDragForm(Rectangle.Empty);
                }

                private void SetOutline(Rectangle floatWindowBounds)
                {
                    this.SetDragForm(floatWindowBounds);
                }

                private void SetOutline(DockPane pane, DockStyle dock, int contentIndex)
                {
                    Rectangle displayingRectangle;
                    if (dock != DockStyle.Fill)
                    {
                        displayingRectangle = pane.DisplayingRectangle;
                        if (dock == DockStyle.Right)
                        {
                            displayingRectangle.X += displayingRectangle.Width/2;
                        }
                        if (dock == DockStyle.Bottom)
                        {
                            displayingRectangle.Y += displayingRectangle.Height/2;
                        }
                        if ((dock == DockStyle.Left) || (dock == DockStyle.Right))
                        {
                            displayingRectangle.Width -= displayingRectangle.Width/2;
                        }
                        if ((dock == DockStyle.Top) || (dock == DockStyle.Bottom))
                        {
                            displayingRectangle.Height -= displayingRectangle.Height/2;
                        }
                        displayingRectangle.Location = pane.PointToScreen(displayingRectangle.Location);
                        this.SetDragForm(displayingRectangle);
                    }
                    else if (contentIndex == -1)
                    {
                        displayingRectangle = pane.DisplayingRectangle;
                        displayingRectangle.Location = pane.PointToScreen(displayingRectangle.Location);
                        this.SetDragForm(displayingRectangle);
                    }
                    else
                    {
                        using (GraphicsPath path = pane.TabStripControl.GetOutline(contentIndex))
                        {
                            RectangleF bounds = path.GetBounds();
                            displayingRectangle = new Rectangle((int) bounds.X, (int) bounds.Y, (int) bounds.Width,
                                (int) bounds.Height);
                            Point[] plgpts = new Point[]
                            {
                                new Point(0, 0), new Point(displayingRectangle.Width, 0),
                                new Point(0, displayingRectangle.Height)
                            };
                            using (Matrix matrix = new Matrix(displayingRectangle, plgpts))
                            {
                                path.Transform(matrix);
                            }
                            Region region = new Region(path);
                            this.SetDragForm(displayingRectangle, region);
                        }
                    }
                }

                private void SetOutline(DockPanel dockPanel, DockStyle dock, bool fullPanelEdge)
                {
                    int dockWindowSize;
                    Rectangle rect = fullPanelEdge ? dockPanel.DockArea : dockPanel.DocumentWindowBounds;
                    rect.Location = dockPanel.PointToScreen(rect.Location);
                    if (dock == DockStyle.Top)
                    {
                        dockWindowSize = dockPanel.GetDockWindowSize(DockState.DockTop);
                        rect = new Rectangle(rect.X, rect.Y, rect.Width, dockWindowSize);
                    }
                    else if (dock == DockStyle.Bottom)
                    {
                        dockWindowSize = dockPanel.GetDockWindowSize(DockState.DockBottom);
                        rect = new Rectangle(rect.X, rect.Bottom - dockWindowSize, rect.Width, dockWindowSize);
                    }
                    else
                    {
                        int num2;
                        if (dock == DockStyle.Left)
                        {
                            num2 = dockPanel.GetDockWindowSize(DockState.DockLeft);
                            rect = new Rectangle(rect.X, rect.Y, num2, rect.Height);
                        }
                        else if (dock == DockStyle.Right)
                        {
                            num2 = dockPanel.GetDockWindowSize(DockState.DockRight);
                            rect = new Rectangle(rect.Right - num2, rect.Y, num2, rect.Height);
                        }
                        else if (dock == DockStyle.Fill)
                        {
                            rect = dockPanel.DocumentWindowBounds;
                            rect.Location = dockPanel.PointToScreen(rect.Location);
                        }
                    }
                    this.SetDragForm(rect);
                }

                private DragForm DragForm
                {
                    get { return this.m_dragForm; }
                }
            }
        }

        private abstract class DragHandler : DockPanel.DragHandlerBase
        {
            private DockPanel m_dockPanel;
            private IDragSource m_dragSource;

            protected DragHandler(DockPanel dockPanel)
            {
                this.m_dockPanel = dockPanel;
            }

            protected sealed override bool OnPreFilterMessage(ref Message m)
            {
                if (((m.Msg == 256) || (m.Msg == 257)) && ((((int) m.WParam) == 17) || (((int) m.WParam) == 16)))
                {
                    this.OnDragging();
                }
                return base.OnPreFilterMessage(ref m);
            }

            public DockPanel DockPanel
            {
                get { return this.m_dockPanel; }
            }

            protected sealed override Control DragControl
            {
                get { return ((this.DragSource == null) ? null : this.DragSource.DragControl); }
            }

            protected IDragSource DragSource
            {
                get { return this.m_dragSource; }
                set { this.m_dragSource = value; }
            }
        }

        private abstract class DragHandlerBase : NativeWindow, IMessageFilter
        {
            private Point m_startMousePosition = Point.Empty;

            protected DragHandlerBase()
            {
            }

            protected bool BeginDrag()
            {
                lock (this)
                {
                    if (this.DragControl == null)
                    {
                        return false;
                    }
                    this.StartMousePosition = Control.MousePosition;
                    if (!NativeMethods.DragDetect(this.DragControl.Handle, this.StartMousePosition))
                    {
                        return false;
                    }
                    this.DragControl.FindForm().Capture = true;
                    base.AssignHandle(this.DragControl.FindForm().Handle);
                    Application.AddMessageFilter(this);
                    return true;
                }
            }

            private void EndDrag(bool abort)
            {
                this.ReleaseHandle();
                Application.RemoveMessageFilter(this);
                this.DragControl.FindForm().Capture = false;
                this.OnEndDrag(abort);
            }

            protected abstract void OnDragging();
            protected abstract void OnEndDrag(bool abort);

            protected virtual bool OnPreFilterMessage(ref Message m)
            {
                return false;
            }

            bool IMessageFilter.PreFilterMessage(ref Message m)
            {
                if (m.Msg == 512)
                {
                    this.OnDragging();
                }
                else if (m.Msg == 514)
                {
                    this.EndDrag(false);
                }
                else if (m.Msg == 533)
                {
                    this.EndDrag(true);
                }
                else if ((m.Msg == 256) && (((int) m.WParam) == 27))
                {
                    this.EndDrag(true);
                }
                return this.OnPreFilterMessage(ref m);
            }

            protected sealed override void WndProc(ref Message m)
            {
                if ((m.Msg == 31) || (m.Msg == 533))
                {
                    this.EndDrag(true);
                }
                base.WndProc(ref m);
            }

            protected abstract Control DragControl { get; }

            protected Point StartMousePosition
            {
                get { return this.m_startMousePosition; }
                private set { this.m_startMousePosition = value; }
            }
        }

        private class FocusManagerImpl : Component, IContentFocusManager, DockPanel.IFocusManager
        {
            private IDockContent m_activeContent = null;
            private IDockContent m_activeDocument = null;
            private DockPane m_activeDocumentPane = null;
            private DockPane m_activePane = null;
            private IDockContent m_contentActivating = null;
            private int m_countSuspendFocusTracking = 0;
            private bool m_disposed = false;
            private DockPanel m_dockPanel;
            private DockPanel.FocusManagerImpl.LocalWindowsHook.HookEventHandler m_hookEventHandler;
            private bool m_inRefreshActiveWindow = false;
            private IDockContent m_lastActiveContent = null;
            private List<IDockContent> m_listContent = new List<IDockContent>();
            private LocalWindowsHook m_localWindowsHook;

            public FocusManagerImpl(DockPanel dockPanel)
            {
                this.m_dockPanel = dockPanel;
                this.m_localWindowsHook = new LocalWindowsHook(HookType.WH_CALLWNDPROCRET);
                this.m_hookEventHandler =
                    new DockPanel.FocusManagerImpl.LocalWindowsHook.HookEventHandler(this.HookEventHandler);
                this.m_localWindowsHook.HookInvoked += this.m_hookEventHandler;
                this.m_localWindowsHook.Install();
            }

            public void Activate(IDockContent content)
            {
                if (this.IsFocusTrackingSuspended)
                {
                    this.ContentActivating = content;
                }
                else
                {
                    DockContentHandler dockHandler = content.DockHandler;
                    if (ContentContains(content, dockHandler.ActiveWindowHandle))
                    {
                        NativeMethods.SetFocus(dockHandler.ActiveWindowHandle);
                    }
                    if (!dockHandler.Form.ContainsFocus &&
                        !dockHandler.Form.SelectNextControl(dockHandler.Form.ActiveControl, true, true, true, true))
                    {
                        NativeMethods.SetFocus(dockHandler.Form.Handle);
                    }
                }
            }

            private void AddLastToActiveList(IDockContent content)
            {
                IDockContent lastActiveContent = this.LastActiveContent;
                if (lastActiveContent != content)
                {
                    DockContentHandler dockHandler = content.DockHandler;
                    if (this.IsInActiveList(content))
                    {
                        this.RemoveFromActiveList(content);
                    }
                    dockHandler.PreviousActive = lastActiveContent;
                    dockHandler.NextActive = null;
                    this.LastActiveContent = content;
                    if (lastActiveContent != null)
                    {
                        lastActiveContent.DockHandler.NextActive = this.LastActiveContent;
                    }
                }
            }

            public void AddToList(IDockContent content)
            {
                if (!this.ListContent.Contains(content) && !this.IsInActiveList(content))
                {
                    this.ListContent.Add(content);
                }
            }

            private static bool ContentContains(IDockContent content, IntPtr hWnd)
            {
                for (Control control2 = Control.FromChildHandle(hWnd); control2 != null; control2 = control2.Parent)
                {
                    if (control2 == content.DockHandler.Form)
                    {
                        return true;
                    }
                }
                return false;
            }

            protected override void Dispose(bool disposing)
            {
                lock (this)
                {
                    if (!(this.m_disposed || !disposing))
                    {
                        this.m_localWindowsHook.Dispose();
                        this.m_disposed = true;
                    }
                    base.Dispose(disposing);
                }
            }

            private DockPane GetPaneFromHandle(IntPtr hWnd)
            {
                Control parent = Control.FromChildHandle(hWnd);
                IDockContent content = null;
                DockPane pane = null;
                while (parent != null)
                {
                    content = parent as IDockContent;
                    if (content != null)
                    {
                        content.DockHandler.ActiveWindowHandle = hWnd;
                    }
                    if ((content != null) && (content.DockHandler.DockPanel == this.DockPanel))
                    {
                        return content.DockHandler.Pane;
                    }
                    pane = parent as DockPane;
                    if ((pane != null) && (pane.DockPanel == this.DockPanel))
                    {
                        return pane;
                    }
                    parent = parent.Parent;
                }
                return pane;
            }

            public void GiveUpFocus(IDockContent content)
            {
                DockContentHandler dockHandler = content.DockHandler;
                if (dockHandler.Form.ContainsFocus)
                {
                    if (this.IsFocusTrackingSuspended)
                    {
                        this.DockPanel.DummyControl.Focus();
                    }
                    if (this.LastActiveContent == content)
                    {
                        IDockContent previousActive = dockHandler.PreviousActive;
                        if (previousActive != null)
                        {
                            this.Activate(previousActive);
                        }
                        else if (this.ListContent.Count > 0)
                        {
                            this.Activate(this.ListContent[this.ListContent.Count - 1]);
                        }
                    }
                    else if (this.LastActiveContent != null)
                    {
                        this.Activate(this.LastActiveContent);
                    }
                    else if (this.ListContent.Count > 0)
                    {
                        this.Activate(this.ListContent[this.ListContent.Count - 1]);
                    }
                }
            }

            private void HookEventHandler(object sender, HookEventArgs e)
            {
                switch (((Msgs) Marshal.ReadInt32(e.lParam, IntPtr.Size*3)))
                {
                    case Msgs.WM_KILLFOCUS:
                    {
                        IntPtr hWnd = Marshal.ReadIntPtr(e.lParam, IntPtr.Size*2);
                        if (this.GetPaneFromHandle(hWnd) == null)
                        {
                            this.RefreshActiveWindow();
                        }
                        break;
                    }
                    case Msgs.WM_SETFOCUS:
                        this.RefreshActiveWindow();
                        break;
                }
            }

            private bool IsInActiveList(IDockContent content)
            {
                return ((content.DockHandler.NextActive != null) || (this.LastActiveContent == content));
            }

            private void RefreshActiveWindow()
            {
                this.SuspendFocusTracking();
                this.m_inRefreshActiveWindow = true;
                DockPane activePane = this.ActivePane;
                IDockContent activeContent = this.ActiveContent;
                IDockContent activeDocument = this.ActiveDocument;
                this.SetActivePane();
                this.SetActiveContent();
                this.SetActiveDocumentPane();
                this.SetActiveDocument();
                this.DockPanel.AutoHideWindow.RefreshActivePane();
                this.ResumeFocusTracking();
                this.m_inRefreshActiveWindow = false;
                if (activeContent != this.ActiveContent)
                {
                    this.DockPanel.OnActiveContentChanged(EventArgs.Empty);
                }
                if (activeDocument != this.ActiveDocument)
                {
                    this.DockPanel.OnActiveDocumentChanged(EventArgs.Empty);
                }
                if (activePane != this.ActivePane)
                {
                    this.DockPanel.OnActivePaneChanged(EventArgs.Empty);
                }
            }

            private void RemoveFromActiveList(IDockContent content)
            {
                if (this.LastActiveContent == content)
                {
                    this.LastActiveContent = content.DockHandler.PreviousActive;
                }
                IDockContent previousActive = content.DockHandler.PreviousActive;
                IDockContent nextActive = content.DockHandler.NextActive;
                if (previousActive != null)
                {
                    previousActive.DockHandler.NextActive = nextActive;
                }
                if (nextActive != null)
                {
                    nextActive.DockHandler.PreviousActive = previousActive;
                }
                content.DockHandler.PreviousActive = null;
                content.DockHandler.NextActive = null;
            }

            public void RemoveFromList(IDockContent content)
            {
                if (this.IsInActiveList(content))
                {
                    this.RemoveFromActiveList(content);
                }
                if (this.ListContent.Contains(content))
                {
                    this.ListContent.Remove(content);
                }
            }

            public void ResumeFocusTracking()
            {
                if (this.m_countSuspendFocusTracking > 0)
                {
                    this.m_countSuspendFocusTracking--;
                }
                if (this.m_countSuspendFocusTracking == 0)
                {
                    if (this.ContentActivating != null)
                    {
                        this.Activate(this.ContentActivating);
                        this.ContentActivating = null;
                    }
                    this.m_localWindowsHook.HookInvoked += this.m_hookEventHandler;
                    if (!this.InRefreshActiveWindow)
                    {
                        this.RefreshActiveWindow();
                    }
                }
            }

            internal void SetActiveContent()
            {
                IDockContent content = (this.ActivePane == null) ? null : this.ActivePane.ActiveContent;
                if (this.m_activeContent != content)
                {
                    if (this.m_activeContent != null)
                    {
                        this.m_activeContent.DockHandler.IsActivated = false;
                    }
                    this.m_activeContent = content;
                    if (this.m_activeContent != null)
                    {
                        this.m_activeContent.DockHandler.IsActivated = true;
                        if (!DockHelper.IsDockStateAutoHide(this.m_activeContent.DockHandler.DockState))
                        {
                            this.AddLastToActiveList(this.m_activeContent);
                        }
                    }
                }
            }

            private void SetActiveDocument()
            {
                IDockContent content = (this.ActiveDocumentPane == null) ? null : this.ActiveDocumentPane.ActiveContent;
                if (this.m_activeDocument != content)
                {
                    this.m_activeDocument = content;
                }
            }

            private void SetActiveDocumentPane()
            {
                DockPane activePane = null;
                if ((this.ActivePane != null) && (this.ActivePane.DockState == DockState.Document))
                {
                    activePane = this.ActivePane;
                }
                if (activePane == null)
                {
                    if (this.ActiveDocumentPane == null)
                    {
                        activePane = this.DockPanel.DockWindows[DockState.Document].DefaultPane;
                    }
                    else if ((this.ActiveDocumentPane.DockPanel != this.DockPanel) ||
                             (this.ActiveDocumentPane.DockState != DockState.Document))
                    {
                        activePane = this.DockPanel.DockWindows[DockState.Document].DefaultPane;
                    }
                    else
                    {
                        activePane = this.ActiveDocumentPane;
                    }
                }
                if (this.m_activeDocumentPane != activePane)
                {
                    if (this.m_activeDocumentPane != null)
                    {
                        this.m_activeDocumentPane.SetIsActiveDocumentPane(false);
                    }
                    this.m_activeDocumentPane = activePane;
                    if (this.m_activeDocumentPane != null)
                    {
                        this.m_activeDocumentPane.SetIsActiveDocumentPane(true);
                    }
                }
            }

            private void SetActivePane()
            {
                DockPane paneFromHandle = this.GetPaneFromHandle(NativeMethods.GetFocus());
                if (this.m_activePane != paneFromHandle)
                {
                    if (this.m_activePane != null)
                    {
                        this.m_activePane.SetIsActivated(false);
                    }
                    this.m_activePane = paneFromHandle;
                    if (this.m_activePane != null)
                    {
                        this.m_activePane.SetIsActivated(true);
                    }
                }
            }

            public void SuspendFocusTracking()
            {
                this.m_countSuspendFocusTracking++;
                this.m_localWindowsHook.HookInvoked -= this.m_hookEventHandler;
            }

            public IDockContent ActiveContent
            {
                get { return this.m_activeContent; }
            }

            public IDockContent ActiveDocument
            {
                get { return this.m_activeDocument; }
            }

            public DockPane ActiveDocumentPane
            {
                get { return this.m_activeDocumentPane; }
            }

            public DockPane ActivePane
            {
                get { return this.m_activePane; }
            }

            private IDockContent ContentActivating
            {
                get { return this.m_contentActivating; }
                set { this.m_contentActivating = value; }
            }

            public DockPanel DockPanel
            {
                get { return this.m_dockPanel; }
            }

            private bool InRefreshActiveWindow
            {
                get { return this.m_inRefreshActiveWindow; }
            }

            public bool IsFocusTrackingSuspended
            {
                get { return (this.m_countSuspendFocusTracking != 0); }
            }

            private IDockContent LastActiveContent
            {
                get { return this.m_lastActiveContent; }
                set { this.m_lastActiveContent = value; }
            }

            private List<IDockContent> ListContent
            {
                get { return this.m_listContent; }
            }

            private class HookEventArgs : EventArgs
            {
                public int HookCode;
                public IntPtr lParam;
                public IntPtr wParam;
            }

            private class LocalWindowsHook : IDisposable
            {
                private NativeMethods.HookProc m_filterFunc = null;
                private IntPtr m_hHook = IntPtr.Zero;
                private HookType m_hookType;

                public event HookEventHandler HookInvoked;

                public LocalWindowsHook(HookType hook)
                {
                    this.m_hookType = hook;
                    this.m_filterFunc = new NativeMethods.HookProc(this.CoreHookProc);
                }

                public IntPtr CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
                {
                    if (code >= 0)
                    {
                        DockPanel.FocusManagerImpl.HookEventArgs e = new DockPanel.FocusManagerImpl.HookEventArgs
                        {
                            HookCode = code,
                            wParam = wParam,
                            lParam = lParam
                        };
                        this.OnHookInvoked(e);
                    }
                    return NativeMethods.CallNextHookEx(this.m_hHook, code, wParam, lParam);
                }

                public void Dispose()
                {
                    this.Dispose(true);
                    GC.SuppressFinalize(this);
                }

                protected virtual void Dispose(bool disposing)
                {
                    this.Uninstall();
                }

                ~LocalWindowsHook()
                {
                    this.Dispose(false);
                }

                public void Install()
                {
                    if (this.m_hHook != IntPtr.Zero)
                    {
                        this.Uninstall();
                    }
                    int currentThreadId = NativeMethods.GetCurrentThreadId();
                    this.m_hHook = NativeMethods.SetWindowsHookEx(this.m_hookType, this.m_filterFunc, IntPtr.Zero,
                        currentThreadId);
                }

                protected void OnHookInvoked(DockPanel.FocusManagerImpl.HookEventArgs e)
                {
                    if (this.HookInvoked != null)
                    {
                        this.HookInvoked(this, e);
                    }
                }

                public void Uninstall()
                {
                    if (this.m_hHook != IntPtr.Zero)
                    {
                        NativeMethods.UnhookWindowsHookEx(this.m_hHook);
                        this.m_hHook = IntPtr.Zero;
                    }
                }

                public delegate void HookEventHandler(object sender, DockPanel.FocusManagerImpl.HookEventArgs e);
            }
        }

        private interface IFocusManager
        {
            void ResumeFocusTracking();
            void SuspendFocusTracking();

            IDockContent ActiveContent { get; }

            IDockContent ActiveDocument { get; }

            DockPane ActiveDocumentPane { get; }

            DockPane ActivePane { get; }

            bool IsFocusTrackingSuspended { get; }
        }

        private class MdiClientController : NativeWindow, IComponent, IDisposable
        {
            private bool m_autoScroll = true;
            private System.Windows.Forms.BorderStyle m_borderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            private System.Windows.Forms.MdiClient m_mdiClient = null;
            private Form m_parentForm = null;
            private ISite m_site = null;

            public event EventHandler Disposed;

            public event EventHandler HandleAssigned;

            public event LayoutEventHandler Layout;

            public event EventHandler MdiChildActivate;

            public event PaintEventHandler Paint;

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
                        if ((this.Site != null) && (this.Site.Container != null))
                        {
                            this.Site.Container.Remove(this);
                        }
                        if (this.Disposed != null)
                        {
                            this.Disposed(this, EventArgs.Empty);
                        }
                    }
                }
            }

            private void InitializeMdiClient()
            {
                if (this.MdiClient != null)
                {
                    this.MdiClient.HandleDestroyed -= new EventHandler(this.MdiClientHandleDestroyed);
                    this.MdiClient.Layout -= new LayoutEventHandler(this.MdiClientLayout);
                }
                if (this.ParentForm != null)
                {
                    foreach (Control control in this.ParentForm.Controls)
                    {
                        this.m_mdiClient = control as System.Windows.Forms.MdiClient;
                        if (this.m_mdiClient != null)
                        {
                            this.ReleaseHandle();
                            base.AssignHandle(this.MdiClient.Handle);
                            this.OnHandleAssigned(EventArgs.Empty);
                            this.MdiClient.HandleDestroyed += new EventHandler(this.MdiClientHandleDestroyed);
                            this.MdiClient.Layout += new LayoutEventHandler(this.MdiClientLayout);
                            break;
                        }
                    }
                }
            }

            private void MdiClientHandleDestroyed(object sender, EventArgs e)
            {
                if (this.m_mdiClient != null)
                {
                    this.m_mdiClient.HandleDestroyed -= new EventHandler(this.MdiClientHandleDestroyed);
                    this.m_mdiClient = null;
                }
                this.ReleaseHandle();
            }

            private void MdiClientLayout(object sender, LayoutEventArgs e)
            {
                this.OnLayout(e);
            }

            protected virtual void OnHandleAssigned(EventArgs e)
            {
                if (this.HandleAssigned != null)
                {
                    this.HandleAssigned(this, e);
                }
            }

            protected virtual void OnLayout(LayoutEventArgs e)
            {
                if (this.Layout != null)
                {
                    this.Layout(this, e);
                }
            }

            protected virtual void OnMdiChildActivate(EventArgs e)
            {
                if (this.MdiChildActivate != null)
                {
                    this.MdiChildActivate(this, e);
                }
            }

            protected virtual void OnPaint(PaintEventArgs e)
            {
                if (this.Paint != null)
                {
                    this.Paint(this, e);
                }
            }

            private void ParentFormHandleCreated(object sender, EventArgs e)
            {
                this.m_parentForm.HandleCreated -= new EventHandler(this.ParentFormHandleCreated);
                this.InitializeMdiClient();
                this.RefreshProperties();
            }

            private void ParentFormMdiChildActivate(object sender, EventArgs e)
            {
                this.OnMdiChildActivate(e);
            }

            private void RefreshProperties()
            {
                this.BorderStyle = this.m_borderStyle;
                this.AutoScroll = this.m_autoScroll;
            }

            public void RenewMdiClient()
            {
                this.InitializeMdiClient();
                this.RefreshProperties();
            }

            private void UpdateStyles()
            {
                NativeMethods.SetWindowPos(this.MdiClient.Handle, IntPtr.Zero, 0, 0, 0, 0,
                    FlagsSetWindowPos.SWP_DRAWFRAME | FlagsSetWindowPos.SWP_NOACTIVATE | FlagsSetWindowPos.SWP_NOMOVE |
                    FlagsSetWindowPos.SWP_NOOWNERZORDER | FlagsSetWindowPos.SWP_NOSIZE | FlagsSetWindowPos.SWP_NOZORDER);
            }

            protected override void WndProc(ref Message m)
            {
                if ((m.Msg == 131) && !this.AutoScroll)
                {
                    NativeMethods.ShowScrollBar(m.HWnd, 3, 0);
                }
                base.WndProc(ref m);
            }

            public bool AutoScroll
            {
                get { return this.m_autoScroll; }
                set
                {
                    this.m_autoScroll = value;
                    if (this.MdiClient != null)
                    {
                        this.UpdateStyles();
                    }
                }
            }

            public System.Windows.Forms.BorderStyle BorderStyle
            {
                set
                {
                    if (!Enum.IsDefined(typeof(System.Windows.Forms.BorderStyle), value))
                    {
                        throw new InvalidEnumArgumentException();
                    }
                    this.m_borderStyle = value;
                    if ((this.MdiClient != null) && ((this.Site == null) || !this.Site.DesignMode))
                    {
                        int windowLong = NativeMethods.GetWindowLong(this.MdiClient.Handle, -16);
                        int num2 = NativeMethods.GetWindowLong(this.MdiClient.Handle, -20);
                        switch (this.m_borderStyle)
                        {
                            case System.Windows.Forms.BorderStyle.None:
                                windowLong &= -8388609;
                                num2 &= -513;
                                break;

                            case System.Windows.Forms.BorderStyle.FixedSingle:
                                num2 &= -513;
                                windowLong |= 8388608;
                                break;

                            case System.Windows.Forms.BorderStyle.Fixed3D:
                                num2 |= 512;
                                windowLong &= -8388609;
                                break;
                        }
                        NativeMethods.SetWindowLong(this.MdiClient.Handle, -16, windowLong);
                        NativeMethods.SetWindowLong(this.MdiClient.Handle, -20, num2);
                        this.UpdateStyles();
                    }
                }
            }

            public System.Windows.Forms.MdiClient MdiClient
            {
                get { return this.m_mdiClient; }
            }

            [Browsable(false)]
            public Form ParentForm
            {
                get { return this.m_parentForm; }
                set
                {
                    if (this.m_parentForm != null)
                    {
                        this.m_parentForm.HandleCreated -= new EventHandler(this.ParentFormHandleCreated);
                        this.m_parentForm.MdiChildActivate -= new EventHandler(this.ParentFormMdiChildActivate);
                    }
                    this.m_parentForm = value;
                    if (this.m_parentForm != null)
                    {
                        if (this.m_parentForm.IsHandleCreated)
                        {
                            this.InitializeMdiClient();
                            this.RefreshProperties();
                        }
                        else
                        {
                            this.m_parentForm.HandleCreated += new EventHandler(this.ParentFormHandleCreated);
                        }
                        this.m_parentForm.MdiChildActivate += new EventHandler(this.ParentFormMdiChildActivate);
                    }
                }
            }

            public ISite Site
            {
                get { return this.m_site; }
                set
                {
                    this.m_site = value;
                    if (this.m_site != null)
                    {
                        IDesignerHost service = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                        if (service != null)
                        {
                            Form rootComponent = service.RootComponent as Form;
                            if (rootComponent != null)
                            {
                                this.ParentForm = rootComponent;
                            }
                        }
                    }
                }
            }
        }

        private static class Persistor
        {
            private static string[] CompatibleConfigFileVersions = new string[0];
            private const string ConfigFileVersion = "1.0";

            private static bool IsFormatVersionValid(string formatVersion)
            {
                if (formatVersion == "1.0")
                {
                    return true;
                }
                foreach (string str in CompatibleConfigFileVersions)
                {
                    if (str == formatVersion)
                    {
                        return true;
                    }
                }
                return false;
            }

            private static ContentStruct[] LoadContents(XmlTextReader xmlIn)
            {
                int num = Convert.ToInt32(xmlIn.GetAttribute("Count"), CultureInfo.InvariantCulture);
                ContentStruct[] structArray = new ContentStruct[num];
                MoveToNextElement(xmlIn);
                for (int i = 0; i < num; i++)
                {
                    int num3 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                    if ((xmlIn.Name != "Content") || (num3 != i))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    structArray[i].PersistString = xmlIn.GetAttribute("PersistString");
                    structArray[i].AutoHidePortion = Convert.ToDouble(xmlIn.GetAttribute("AutoHidePortion"),
                        CultureInfo.InvariantCulture);
                    structArray[i].IsHidden = Convert.ToBoolean(xmlIn.GetAttribute("IsHidden"),
                        CultureInfo.InvariantCulture);
                    structArray[i].IsFloat = Convert.ToBoolean(xmlIn.GetAttribute("IsFloat"),
                        CultureInfo.InvariantCulture);
                    MoveToNextElement(xmlIn);
                }
                return structArray;
            }

            private static DockWindowStruct[] LoadDockWindows(XmlTextReader xmlIn, DockPanel dockPanel)
            {
                EnumConverter converter = new EnumConverter(typeof(DockState));
                EnumConverter converter2 = new EnumConverter(typeof(DockAlignment));
                int count = dockPanel.DockWindows.Count;
                DockWindowStruct[] structArray = new DockWindowStruct[count];
                MoveToNextElement(xmlIn);
                for (int i = 0; i < count; i++)
                {
                    int num3 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                    if ((xmlIn.Name != "DockWindow") || (num3 != i))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    structArray[i].DockState = (DockState) converter.ConvertFrom(xmlIn.GetAttribute("DockState"));
                    structArray[i].ZOrderIndex = Convert.ToInt32(xmlIn.GetAttribute("ZOrderIndex"),
                        CultureInfo.InvariantCulture);
                    MoveToNextElement(xmlIn);
                    if ((xmlIn.Name != "DockList") && (xmlIn.Name != "NestedPanes"))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    int num4 = Convert.ToInt32(xmlIn.GetAttribute("Count"), CultureInfo.InvariantCulture);
                    structArray[i].NestedPanes = new NestedPane[num4];
                    MoveToNextElement(xmlIn);
                    for (int j = 0; j < num4; j++)
                    {
                        int num6 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                        if ((xmlIn.Name != "Pane") || (num6 != j))
                        {
                            throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                        }
                        structArray[i].NestedPanes[j].IndexPane = Convert.ToInt32(xmlIn.GetAttribute("RefID"),
                            CultureInfo.InvariantCulture);
                        structArray[i].NestedPanes[j].IndexPrevPane = Convert.ToInt32(xmlIn.GetAttribute("PrevPane"),
                            CultureInfo.InvariantCulture);
                        structArray[i].NestedPanes[j].Alignment =
                            (DockAlignment) converter2.ConvertFrom(xmlIn.GetAttribute("Alignment"));
                        structArray[i].NestedPanes[j].Proportion = Convert.ToDouble(xmlIn.GetAttribute("Proportion"),
                            CultureInfo.InvariantCulture);
                        MoveToNextElement(xmlIn);
                    }
                }
                return structArray;
            }

            private static FloatWindowStruct[] LoadFloatWindows(XmlTextReader xmlIn)
            {
                EnumConverter converter = new EnumConverter(typeof(DockAlignment));
                RectangleConverter converter2 = new RectangleConverter();
                int num = Convert.ToInt32(xmlIn.GetAttribute("Count"), CultureInfo.InvariantCulture);
                FloatWindowStruct[] structArray = new FloatWindowStruct[num];
                MoveToNextElement(xmlIn);
                for (int i = 0; i < num; i++)
                {
                    int num3 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                    if ((xmlIn.Name != "FloatWindow") || (num3 != i))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    structArray[i].Bounds =
                        (Rectangle) converter2.ConvertFromInvariantString(xmlIn.GetAttribute("Bounds"));
                    structArray[i].ZOrderIndex = Convert.ToInt32(xmlIn.GetAttribute("ZOrderIndex"),
                        CultureInfo.InvariantCulture);
                    MoveToNextElement(xmlIn);
                    if ((xmlIn.Name != "DockList") && (xmlIn.Name != "NestedPanes"))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    int num4 = Convert.ToInt32(xmlIn.GetAttribute("Count"), CultureInfo.InvariantCulture);
                    structArray[i].NestedPanes = new NestedPane[num4];
                    MoveToNextElement(xmlIn);
                    for (int j = 0; j < num4; j++)
                    {
                        int num6 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                        if ((xmlIn.Name != "Pane") || (num6 != j))
                        {
                            throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                        }
                        structArray[i].NestedPanes[j].IndexPane = Convert.ToInt32(xmlIn.GetAttribute("RefID"),
                            CultureInfo.InvariantCulture);
                        structArray[i].NestedPanes[j].IndexPrevPane = Convert.ToInt32(xmlIn.GetAttribute("PrevPane"),
                            CultureInfo.InvariantCulture);
                        structArray[i].NestedPanes[j].Alignment =
                            (DockAlignment) converter.ConvertFrom(xmlIn.GetAttribute("Alignment"));
                        structArray[i].NestedPanes[j].Proportion = Convert.ToDouble(xmlIn.GetAttribute("Proportion"),
                            CultureInfo.InvariantCulture);
                        MoveToNextElement(xmlIn);
                    }
                }
                return structArray;
            }

            public static void LoadFromXml(DockPanel dockPanel, Stream stream, DeserializeDockContent deserializeContent)
            {
                LoadFromXml(dockPanel, stream, deserializeContent, true);
            }

            public static void LoadFromXml(DockPanel dockPanel, string fileName,
                DeserializeDockContent deserializeContent)
            {
                FileStream stream = new FileStream(fileName, FileMode.Open);
                try
                {
                    LoadFromXml(dockPanel, stream, deserializeContent);
                }
                finally
                {
                    stream.Close();
                }
            }

            public static void LoadFromXml(DockPanel dockPanel, Stream stream, DeserializeDockContent deserializeContent,
                bool closeStream)
            {
                int num2;
                int num5;
                IDockContent content;
                DockPane pane;
                int indexPane;
                int indexPrevPane;
                DockPane pane2;
                DockAlignment alignment;
                double proportion;
                if (dockPanel.Contents.Count != 0)
                {
                    throw new InvalidOperationException(Strings.DockPanel_LoadFromXml_AlreadyInitialized);
                }
                XmlTextReader xmlIn = new XmlTextReader(stream)
                {
                    WhitespaceHandling = WhitespaceHandling.None
                };
                xmlIn.MoveToContent();
                while (!xmlIn.Name.Equals("DockPanel"))
                {
                    if (!MoveToNextElement(xmlIn))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                }
                if (!IsFormatVersionValid(xmlIn.GetAttribute("FormatVersion")))
                {
                    throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidFormatVersion);
                }
                DockPanelStruct struct2 = new DockPanelStruct
                {
                    DockLeftPortion =
                        Convert.ToDouble(xmlIn.GetAttribute("DockLeftPortion"), CultureInfo.InvariantCulture),
                    DockRightPortion =
                        Convert.ToDouble(xmlIn.GetAttribute("DockRightPortion"), CultureInfo.InvariantCulture),
                    DockTopPortion =
                        Convert.ToDouble(xmlIn.GetAttribute("DockTopPortion"), CultureInfo.InvariantCulture),
                    DockBottomPortion =
                        Convert.ToDouble(xmlIn.GetAttribute("DockBottomPortion"), CultureInfo.InvariantCulture),
                    IndexActiveDocumentPane =
                        Convert.ToInt32(xmlIn.GetAttribute("ActiveDocumentPane"), CultureInfo.InvariantCulture),
                    IndexActivePane = Convert.ToInt32(xmlIn.GetAttribute("ActivePane"), CultureInfo.InvariantCulture)
                };
                MoveToNextElement(xmlIn);
                if (xmlIn.Name != "Contents")
                {
                    throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                }
                ContentStruct[] structArray = LoadContents(xmlIn);
                if (xmlIn.Name != "Panes")
                {
                    throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                }
                PaneStruct[] structArray2 = LoadPanes(xmlIn);
                if (xmlIn.Name != "DockWindows")
                {
                    throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                }
                DockWindowStruct[] structArray3 = LoadDockWindows(xmlIn, dockPanel);
                if (xmlIn.Name != "FloatWindows")
                {
                    throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                }
                FloatWindowStruct[] structArray4 = LoadFloatWindows(xmlIn);
                if (closeStream)
                {
                    xmlIn.Close();
                }
                dockPanel.SuspendLayout(true);
                dockPanel.DockLeftPortion = struct2.DockLeftPortion;
                dockPanel.DockRightPortion = struct2.DockRightPortion;
                dockPanel.DockTopPortion = struct2.DockTopPortion;
                dockPanel.DockBottomPortion = struct2.DockBottomPortion;
                int num = 2147483647;
                for (num2 = 0; num2 < structArray3.Length; num2++)
                {
                    int zOrderIndex = -1;
                    int index = -1;
                    num5 = 0;
                    while (num5 < structArray3.Length)
                    {
                        if ((structArray3[num5].ZOrderIndex > zOrderIndex) && (structArray3[num5].ZOrderIndex < num))
                        {
                            zOrderIndex = structArray3[num5].ZOrderIndex;
                            index = num5;
                        }
                        num5++;
                    }
                    dockPanel.DockWindows[structArray3[index].DockState].BringToFront();
                    num = zOrderIndex;
                }
                for (num2 = 0; num2 < structArray.Length; num2++)
                {
                    content = deserializeContent(structArray[num2].PersistString);
                    if (content == null)
                    {
                        content = new DummyContent();
                    }
                    content.DockHandler.DockPanel = dockPanel;
                    content.DockHandler.AutoHidePortion = structArray[num2].AutoHidePortion;
                    content.DockHandler.IsHidden = true;
                    content.DockHandler.IsFloat = structArray[num2].IsFloat;
                }
                for (num2 = 0; num2 < structArray2.Length; num2++)
                {
                    pane = null;
                    num5 = 0;
                    while (num5 < structArray2[num2].IndexContents.Length)
                    {
                        content = dockPanel.Contents[structArray2[num2].IndexContents[num5]];
                        if (num5 == 0)
                        {
                            pane = dockPanel.DockPaneFactory.CreateDockPane(content, structArray2[num2].DockState, false);
                        }
                        else if (structArray2[num2].DockState == DockState.Float)
                        {
                            content.DockHandler.FloatPane = pane;
                        }
                        else
                        {
                            content.DockHandler.PanelPane = pane;
                        }
                        num5++;
                    }
                }
                for (num2 = 0; num2 < structArray3.Length; num2++)
                {
                    num5 = 0;
                    while (num5 < structArray3[num2].NestedPanes.Length)
                    {
                        DockWindow container = dockPanel.DockWindows[structArray3[num2].DockState];
                        indexPane = structArray3[num2].NestedPanes[num5].IndexPane;
                        pane = dockPanel.Panes[indexPane];
                        indexPrevPane = structArray3[num2].NestedPanes[num5].IndexPrevPane;
                        pane2 = (indexPrevPane == -1)
                            ? container.NestedPanes.GetDefaultPreviousPane(pane)
                            : dockPanel.Panes[indexPrevPane];
                        alignment = structArray3[num2].NestedPanes[num5].Alignment;
                        proportion = structArray3[num2].NestedPanes[num5].Proportion;
                        pane.DockTo(container, pane2, alignment, proportion);
                        if (structArray2[indexPane].DockState == container.DockState)
                        {
                            structArray2[indexPane].ZOrderIndex = structArray3[num2].ZOrderIndex;
                        }
                        num5++;
                    }
                }
                for (num2 = 0; num2 < structArray4.Length; num2++)
                {
                    FloatWindow window2 = null;
                    num5 = 0;
                    while (num5 < structArray4[num2].NestedPanes.Length)
                    {
                        indexPane = structArray4[num2].NestedPanes[num5].IndexPane;
                        pane = dockPanel.Panes[indexPane];
                        if (num5 == 0)
                        {
                            window2 = dockPanel.FloatWindowFactory.CreateFloatWindow(dockPanel, pane,
                                structArray4[num2].Bounds);
                        }
                        else
                        {
                            indexPrevPane = structArray4[num2].NestedPanes[num5].IndexPrevPane;
                            pane2 = (indexPrevPane == -1) ? null : dockPanel.Panes[indexPrevPane];
                            alignment = structArray4[num2].NestedPanes[num5].Alignment;
                            proportion = structArray4[num2].NestedPanes[num5].Proportion;
                            pane.DockTo(window2, pane2, alignment, proportion);
                            if (structArray2[indexPane].DockState == window2.DockState)
                            {
                                structArray2[indexPane].ZOrderIndex = structArray4[num2].ZOrderIndex;
                            }
                        }
                        num5++;
                    }
                }
                int[] numArray = null;
                if (structArray.Length > 0)
                {
                    numArray = new int[structArray.Length];
                    for (num2 = 0; num2 < structArray.Length; num2++)
                    {
                        numArray[num2] = num2;
                    }
                    int length = structArray.Length;
                    for (num2 = 0; num2 < (structArray.Length - 1); num2++)
                    {
                        for (num5 = num2 + 1; num5 < structArray.Length; num5++)
                        {
                            DockPane pane3 = dockPanel.Contents[numArray[num2]].DockHandler.Pane;
                            int num10 = (pane3 == null) ? 0 : structArray2[dockPanel.Panes.IndexOf(pane3)].ZOrderIndex;
                            DockPane pane4 = dockPanel.Contents[numArray[num5]].DockHandler.Pane;
                            int num11 = (pane4 == null) ? 0 : structArray2[dockPanel.Panes.IndexOf(pane4)].ZOrderIndex;
                            if (num10 > num11)
                            {
                                int num12 = numArray[num2];
                                numArray[num2] = numArray[num5];
                                numArray[num5] = num12;
                            }
                        }
                    }
                }
                num2 = 0;
                while (num2 < structArray.Length)
                {
                    content = dockPanel.Contents[numArray[num2]];
                    if ((content.DockHandler.Pane != null) && (content.DockHandler.Pane.DockState != DockState.Document))
                    {
                        content.DockHandler.IsHidden = structArray[numArray[num2]].IsHidden;
                    }
                    num2++;
                }
                for (num2 = 0; num2 < structArray.Length; num2++)
                {
                    content = dockPanel.Contents[numArray[num2]];
                    if ((content.DockHandler.Pane != null) && (content.DockHandler.Pane.DockState == DockState.Document))
                    {
                        content.DockHandler.IsHidden = structArray[numArray[num2]].IsHidden;
                    }
                }
                for (num2 = 0; num2 < structArray2.Length; num2++)
                {
                    dockPanel.Panes[num2].ActiveContent = (structArray2[num2].IndexActiveContent == -1)
                        ? null
                        : dockPanel.Contents[structArray2[num2].IndexActiveContent];
                }
                if (struct2.IndexActiveDocumentPane != -1)
                {
                    dockPanel.Panes[struct2.IndexActiveDocumentPane].Activate();
                }
                if (struct2.IndexActivePane != -1)
                {
                    dockPanel.Panes[struct2.IndexActivePane].Activate();
                }
                for (num2 = dockPanel.Contents.Count - 1; num2 >= 0; num2--)
                {
                    if (dockPanel.Contents[num2] is DummyContent)
                    {
                        dockPanel.Contents[num2].DockHandler.Form.Close();
                    }
                }
                dockPanel.ResumeLayout(true, true);
            }

            private static PaneStruct[] LoadPanes(XmlTextReader xmlIn)
            {
                EnumConverter converter = new EnumConverter(typeof(DockState));
                int num = Convert.ToInt32(xmlIn.GetAttribute("Count"), CultureInfo.InvariantCulture);
                PaneStruct[] structArray = new PaneStruct[num];
                MoveToNextElement(xmlIn);
                for (int i = 0; i < num; i++)
                {
                    int num3 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                    if ((xmlIn.Name != "Pane") || (num3 != i))
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    structArray[i].DockState = (DockState) converter.ConvertFrom(xmlIn.GetAttribute("DockState"));
                    structArray[i].IndexActiveContent = Convert.ToInt32(xmlIn.GetAttribute("ActiveContent"),
                        CultureInfo.InvariantCulture);
                    structArray[i].ZOrderIndex = -1;
                    MoveToNextElement(xmlIn);
                    if (xmlIn.Name != "Contents")
                    {
                        throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                    }
                    int num4 = Convert.ToInt32(xmlIn.GetAttribute("Count"), CultureInfo.InvariantCulture);
                    structArray[i].IndexContents = new int[num4];
                    MoveToNextElement(xmlIn);
                    for (int j = 0; j < num4; j++)
                    {
                        int num6 = Convert.ToInt32(xmlIn.GetAttribute("ID"), CultureInfo.InvariantCulture);
                        if ((xmlIn.Name != "Content") || (num6 != j))
                        {
                            throw new ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat);
                        }
                        structArray[i].IndexContents[j] = Convert.ToInt32(xmlIn.GetAttribute("RefID"),
                            CultureInfo.InvariantCulture);
                        MoveToNextElement(xmlIn);
                    }
                }
                return structArray;
            }

            private static bool MoveToNextElement(XmlTextReader xmlIn)
            {
                if (!xmlIn.Read())
                {
                    return false;
                }
                while (xmlIn.NodeType == XmlNodeType.EndElement)
                {
                    if (!xmlIn.Read())
                    {
                        return false;
                    }
                }
                return true;
            }

            public static void SaveAsXml(DockPanel dockPanel, string fileName)
            {
                SaveAsXml(dockPanel, fileName, Encoding.Unicode);
            }

            public static void SaveAsXml(DockPanel dockPanel, Stream stream, Encoding encoding)
            {
                SaveAsXml(dockPanel, stream, encoding, false);
            }

            public static void SaveAsXml(DockPanel dockPanel, string fileName, Encoding encoding)
            {
                FileStream stream = new FileStream(fileName, FileMode.Create);
                try
                {
                    SaveAsXml(dockPanel, stream, encoding);
                }
                finally
                {
                    stream.Close();
                }
            }

            public static void SaveAsXml(DockPanel dockPanel, Stream stream, Encoding encoding, bool upstream)
            {
                NestedDockingStatus nestedDockingStatus;
                XmlTextWriter writer = new XmlTextWriter(stream, encoding)
                {
                    Formatting = Formatting.Indented
                };
                if (!upstream)
                {
                    writer.WriteStartDocument();
                }
                writer.WriteComment(Strings.DockPanel_Persistor_XmlFileComment1);
                writer.WriteComment(Strings.DockPanel_Persistor_XmlFileComment2);
                writer.WriteStartElement("DockPanel");
                writer.WriteAttributeString("FormatVersion", "1.0");
                writer.WriteAttributeString("DockLeftPortion",
                    dockPanel.DockLeftPortion.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("DockRightPortion",
                    dockPanel.DockRightPortion.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("DockTopPortion",
                    dockPanel.DockTopPortion.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("DockBottomPortion",
                    dockPanel.DockBottomPortion.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("ActiveDocumentPane",
                    dockPanel.Panes.IndexOf(dockPanel.ActiveDocumentPane).ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("ActivePane",
                    dockPanel.Panes.IndexOf(dockPanel.ActivePane).ToString(CultureInfo.InvariantCulture));
                writer.WriteStartElement("Contents");
                writer.WriteAttributeString("Count", dockPanel.Contents.Count.ToString(CultureInfo.InvariantCulture));
                foreach (IDockContent content in dockPanel.Contents)
                {
                    writer.WriteStartElement("Content");
                    writer.WriteAttributeString("ID",
                        dockPanel.Contents.IndexOf(content).ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("PersistString", content.DockHandler.PersistString);
                    writer.WriteAttributeString("AutoHidePortion",
                        content.DockHandler.AutoHidePortion.ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("IsHidden",
                        content.DockHandler.IsHidden.ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("IsFloat",
                        content.DockHandler.IsFloat.ToString(CultureInfo.InvariantCulture));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteStartElement("Panes");
                writer.WriteAttributeString("Count", dockPanel.Panes.Count.ToString(CultureInfo.InvariantCulture));
                foreach (DockPane pane in dockPanel.Panes)
                {
                    writer.WriteStartElement("Pane");
                    writer.WriteAttributeString("ID",
                        dockPanel.Panes.IndexOf(pane).ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("DockState", pane.DockState.ToString());
                    writer.WriteAttributeString("ActiveContent",
                        dockPanel.Contents.IndexOf(pane.ActiveContent).ToString(CultureInfo.InvariantCulture));
                    writer.WriteStartElement("Contents");
                    writer.WriteAttributeString("Count", pane.Contents.Count.ToString(CultureInfo.InvariantCulture));
                    foreach (IDockContent content in pane.Contents)
                    {
                        writer.WriteStartElement("Content");
                        writer.WriteAttributeString("ID",
                            pane.Contents.IndexOf(content).ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("RefID",
                            dockPanel.Contents.IndexOf(content).ToString(CultureInfo.InvariantCulture));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteStartElement("DockWindows");
                int num = 0;
                foreach (DockWindow window in dockPanel.DockWindows)
                {
                    writer.WriteStartElement("DockWindow");
                    writer.WriteAttributeString("ID", num.ToString(CultureInfo.InvariantCulture));
                    num++;
                    writer.WriteAttributeString("DockState", window.DockState.ToString());
                    writer.WriteAttributeString("ZOrderIndex",
                        dockPanel.Controls.IndexOf(window).ToString(CultureInfo.InvariantCulture));
                    writer.WriteStartElement("NestedPanes");
                    writer.WriteAttributeString("Count", window.NestedPanes.Count.ToString(CultureInfo.InvariantCulture));
                    foreach (DockPane pane in window.NestedPanes)
                    {
                        writer.WriteStartElement("Pane");
                        writer.WriteAttributeString("ID",
                            window.NestedPanes.IndexOf(pane).ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("RefID",
                            dockPanel.Panes.IndexOf(pane).ToString(CultureInfo.InvariantCulture));
                        nestedDockingStatus = pane.NestedDockingStatus;
                        writer.WriteAttributeString("PrevPane",
                            dockPanel.Panes.IndexOf(nestedDockingStatus.PreviousPane)
                                .ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("Alignment", nestedDockingStatus.Alignment.ToString());
                        writer.WriteAttributeString("Proportion",
                            nestedDockingStatus.Proportion.ToString(CultureInfo.InvariantCulture));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                RectangleConverter converter = new RectangleConverter();
                writer.WriteStartElement("FloatWindows");
                writer.WriteAttributeString("Count", dockPanel.FloatWindows.Count.ToString(CultureInfo.InvariantCulture));
                foreach (FloatWindow window2 in dockPanel.FloatWindows)
                {
                    writer.WriteStartElement("FloatWindow");
                    writer.WriteAttributeString("ID",
                        dockPanel.FloatWindows.IndexOf(window2).ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("Bounds", converter.ConvertToInvariantString(window2.Bounds));
                    writer.WriteAttributeString("ZOrderIndex",
                        window2.DockPanel.FloatWindows.IndexOf(window2).ToString(CultureInfo.InvariantCulture));
                    writer.WriteStartElement("NestedPanes");
                    writer.WriteAttributeString("Count",
                        window2.NestedPanes.Count.ToString(CultureInfo.InvariantCulture));
                    foreach (DockPane pane in window2.NestedPanes)
                    {
                        writer.WriteStartElement("Pane");
                        writer.WriteAttributeString("ID",
                            window2.NestedPanes.IndexOf(pane).ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("RefID",
                            dockPanel.Panes.IndexOf(pane).ToString(CultureInfo.InvariantCulture));
                        nestedDockingStatus = pane.NestedDockingStatus;
                        writer.WriteAttributeString("PrevPane",
                            dockPanel.Panes.IndexOf(nestedDockingStatus.PreviousPane)
                                .ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("Alignment", nestedDockingStatus.Alignment.ToString());
                        writer.WriteAttributeString("Proportion",
                            nestedDockingStatus.Proportion.ToString(CultureInfo.InvariantCulture));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                if (!upstream)
                {
                    writer.WriteEndDocument();
                    writer.Close();
                }
                else
                {
                    writer.Flush();
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct ContentStruct
            {
                private string m_persistString;
                private double m_autoHidePortion;
                private bool m_isHidden;
                private bool m_isFloat;

                public string PersistString
                {
                    get { return this.m_persistString; }
                    set { this.m_persistString = value; }
                }

                public double AutoHidePortion
                {
                    get { return this.m_autoHidePortion; }
                    set { this.m_autoHidePortion = value; }
                }

                public bool IsHidden
                {
                    get { return this.m_isHidden; }
                    set { this.m_isHidden = value; }
                }

                public bool IsFloat
                {
                    get { return this.m_isFloat; }
                    set { this.m_isFloat = value; }
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct DockPanelStruct
            {
                private double m_dockLeftPortion;
                private double m_dockRightPortion;
                private double m_dockTopPortion;
                private double m_dockBottomPortion;
                private int m_indexActiveDocumentPane;
                private int m_indexActivePane;

                public double DockLeftPortion
                {
                    get { return this.m_dockLeftPortion; }
                    set { this.m_dockLeftPortion = value; }
                }

                public double DockRightPortion
                {
                    get { return this.m_dockRightPortion; }
                    set { this.m_dockRightPortion = value; }
                }

                public double DockTopPortion
                {
                    get { return this.m_dockTopPortion; }
                    set { this.m_dockTopPortion = value; }
                }

                public double DockBottomPortion
                {
                    get { return this.m_dockBottomPortion; }
                    set { this.m_dockBottomPortion = value; }
                }

                public int IndexActiveDocumentPane
                {
                    get { return this.m_indexActiveDocumentPane; }
                    set { this.m_indexActiveDocumentPane = value; }
                }

                public int IndexActivePane
                {
                    get { return this.m_indexActivePane; }
                    set { this.m_indexActivePane = value; }
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct DockWindowStruct
            {
                private DockState m_dockState;
                private int m_zOrderIndex;
                private DockPanel.Persistor.NestedPane[] m_nestedPanes;

                public DockState DockState
                {
                    get { return this.m_dockState; }
                    set { this.m_dockState = value; }
                }

                public int ZOrderIndex
                {
                    get { return this.m_zOrderIndex; }
                    set { this.m_zOrderIndex = value; }
                }

                public DockPanel.Persistor.NestedPane[] NestedPanes
                {
                    get { return this.m_nestedPanes; }
                    set { this.m_nestedPanes = value; }
                }
            }

            private class DummyContent : DockContent
            {
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct FloatWindowStruct
            {
                private Rectangle m_bounds;
                private int m_zOrderIndex;
                private DockPanel.Persistor.NestedPane[] m_nestedPanes;

                public Rectangle Bounds
                {
                    get { return this.m_bounds; }
                    set { this.m_bounds = value; }
                }

                public int ZOrderIndex
                {
                    get { return this.m_zOrderIndex; }
                    set { this.m_zOrderIndex = value; }
                }

                public DockPanel.Persistor.NestedPane[] NestedPanes
                {
                    get { return this.m_nestedPanes; }
                    set { this.m_nestedPanes = value; }
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct NestedPane
            {
                private int m_indexPane;
                private int m_indexPrevPane;
                private DockAlignment m_alignment;
                private double m_proportion;

                public int IndexPane
                {
                    get { return this.m_indexPane; }
                    set { this.m_indexPane = value; }
                }

                public int IndexPrevPane
                {
                    get { return this.m_indexPrevPane; }
                    set { this.m_indexPrevPane = value; }
                }

                public DockAlignment Alignment
                {
                    get { return this.m_alignment; }
                    set { this.m_alignment = value; }
                }

                public double Proportion
                {
                    get { return this.m_proportion; }
                    set { this.m_proportion = value; }
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct PaneStruct
            {
                private DockState m_dockState;
                private int m_indexActiveContent;
                private int[] m_indexContents;
                private int m_zOrderIndex;

                public DockState DockState
                {
                    get { return this.m_dockState; }
                    set { this.m_dockState = value; }
                }

                public int IndexActiveContent
                {
                    get { return this.m_indexActiveContent; }
                    set { this.m_indexActiveContent = value; }
                }

                public int[] IndexContents
                {
                    get { return this.m_indexContents; }
                    set { this.m_indexContents = value; }
                }

                public int ZOrderIndex
                {
                    get { return this.m_zOrderIndex; }
                    set { this.m_zOrderIndex = value; }
                }
            }
        }

        private sealed class SplitterDragHandler : DockPanel.DragHandler
        {
            private SplitterOutline m_outline;
            private Rectangle m_rectSplitter;

            public SplitterDragHandler(DockPanel dockPanel) : base(dockPanel)
            {
            }

            public void BeginDrag(ISplitterDragSource dragSource, Rectangle rectSplitter)
            {
                this.DragSource = dragSource;
                this.RectSplitter = rectSplitter;
                if (!base.BeginDrag())
                {
                    this.DragSource = null;
                }
                else
                {
                    this.Outline = new SplitterOutline();
                    this.Outline.Show(rectSplitter);
                    this.DragSource.BeginDrag(rectSplitter);
                }
            }

            private int GetMovingOffset(Point ptMouse)
            {
                Rectangle splitterOutlineBounds = this.GetSplitterOutlineBounds(ptMouse);
                if (this.DragSource.IsVertical)
                {
                    return (splitterOutlineBounds.X - this.RectSplitter.X);
                }
                return (splitterOutlineBounds.Y - this.RectSplitter.Y);
            }

            private Rectangle GetSplitterOutlineBounds(Point ptMouse)
            {
                Rectangle dragLimitBounds = this.DragSource.DragLimitBounds;
                Rectangle rectSplitter = this.RectSplitter;
                if ((dragLimitBounds.Width > 0) && (dragLimitBounds.Height > 0))
                {
                    if (this.DragSource.IsVertical)
                    {
                        rectSplitter.X += ptMouse.X - base.StartMousePosition.X;
                        rectSplitter.Height = dragLimitBounds.Height;
                    }
                    else
                    {
                        rectSplitter.Y += ptMouse.Y - base.StartMousePosition.Y;
                        rectSplitter.Width = dragLimitBounds.Width;
                    }
                    if (rectSplitter.Left < dragLimitBounds.Left)
                    {
                        rectSplitter.X = dragLimitBounds.X;
                    }
                    if (rectSplitter.Top < dragLimitBounds.Top)
                    {
                        rectSplitter.Y = dragLimitBounds.Y;
                    }
                    if (rectSplitter.Right > dragLimitBounds.Right)
                    {
                        rectSplitter.X -= rectSplitter.Right - dragLimitBounds.Right;
                    }
                    if (rectSplitter.Bottom > dragLimitBounds.Bottom)
                    {
                        rectSplitter.Y -= rectSplitter.Bottom - dragLimitBounds.Bottom;
                    }
                }
                return rectSplitter;
            }

            protected override void OnDragging()
            {
                this.Outline.Show(this.GetSplitterOutlineBounds(Control.MousePosition));
            }

            protected override void OnEndDrag(bool abort)
            {
                base.DockPanel.SuspendLayout(true);
                this.Outline.Close();
                if (!abort)
                {
                    this.DragSource.MoveSplitter(this.GetMovingOffset(Control.MousePosition));
                }
                this.DragSource.EndDrag();
                base.DockPanel.ResumeLayout(true, true);
            }

            public ISplitterDragSource DragSource
            {
                get { return (base.DragSource as ISplitterDragSource); }
                private set { base.DragSource = value; }
            }

            private SplitterOutline Outline
            {
                get { return this.m_outline; }
                set { this.m_outline = value; }
            }

            private Rectangle RectSplitter
            {
                get { return this.m_rectSplitter; }
                set { this.m_rectSplitter = value; }
            }

            private class SplitterOutline
            {
                private DragForm m_dragForm = new DragForm();

                public SplitterOutline()
                {
                    this.SetDragForm(Rectangle.Empty);
                    this.DragForm.BackColor = Color.Black;
                    this.DragForm.Opacity = 0.7;
                    this.DragForm.Show(false);
                }

                public void Close()
                {
                    this.DragForm.Close();
                }

                private void SetDragForm(Rectangle rect)
                {
                    this.DragForm.Bounds = rect;
                    if (rect == Rectangle.Empty)
                    {
                        this.DragForm.Region = new Region(Rectangle.Empty);
                    }
                    else if (this.DragForm.Region != null)
                    {
                        this.DragForm.Region = null;
                    }
                }

                public void Show(Rectangle rect)
                {
                    this.SetDragForm(rect);
                }

                private DragForm DragForm
                {
                    get { return this.m_dragForm; }
                }
            }
        }
    }
}