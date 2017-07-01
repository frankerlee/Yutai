using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    public class DockContent : Form, IDockContent
    {
        private static readonly object DockStateChangedEvent = new object();
        private DockContentHandler m_dockHandler = null;

        [LocalizedCategory("Category_PropertyChanged"), LocalizedDescription("Pane_DockStateChanged_Description")]
        public event EventHandler DockStateChanged
        {
            add { base.Events.AddHandler(DockStateChangedEvent, value); }
            remove { base.Events.RemoveHandler(DockStateChangedEvent, value); }
        }

        public DockContent()
        {
            this.m_dockHandler = new DockContentHandler(this, new GetPersistStringCallback(this.GetPersistString));
            this.m_dockHandler.DockStateChanged += new EventHandler(this.DockHandler_DockStateChanged);
        }

        public void Activate()
        {
            this.DockHandler.Activate();
        }

        private void DockHandler_DockStateChanged(object sender, EventArgs e)
        {
            this.OnDockStateChanged(e);
        }

        public void DockTo(DockPanel panel, DockStyle dockStyle)
        {
            this.DockHandler.DockTo(panel, dockStyle);
        }

        public void DockTo(DockPane paneTo, DockStyle dockStyle, int contentIndex)
        {
            this.DockHandler.DockTo(paneTo, dockStyle, contentIndex);
        }

        public void FloatAt(Rectangle floatWindowBounds)
        {
            this.DockHandler.FloatAt(floatWindowBounds);
        }

        protected virtual string GetPersistString()
        {
            return base.GetType().ToString();
        }

        public void Hide()
        {
            this.DockHandler.Hide();
        }

        public bool IsDockStateValid(DockState dockState)
        {
            return this.DockHandler.IsDockStateValid(dockState);
        }

        protected virtual void OnDockStateChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[DockStateChangedEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private bool ShouldSerializeTabText()
        {
            return (this.DockHandler.TabText != null);
        }

        public void Show()
        {
            this.DockHandler.Show();
        }

        public void Show(DockPanel dockPanel)
        {
            this.DockHandler.Show(dockPanel);
        }

        public void Show(DockPane pane, IDockContent beforeContent)
        {
            this.DockHandler.Show(pane, beforeContent);
        }

        public void Show(DockPanel dockPanel, DockState dockState)
        {
            this.DockHandler.Show(dockPanel, dockState);
        }

        public void Show(DockPanel dockPanel, Rectangle floatWindowBounds)
        {
            this.DockHandler.Show(dockPanel, floatWindowBounds);
        }

        public void Show(DockPane previousPane, DockAlignment alignment, double proportion)
        {
            this.DockHandler.Show(previousPane, alignment, proportion);
        }

        [DefaultValue(true), LocalizedCategory("Category_Docking"),
         LocalizedDescription("DockContent_AllowEndUserDocking_Description")]
        public bool AllowEndUserDocking
        {
            get { return this.DockHandler.AllowEndUserDocking; }
            set { this.DockHandler.AllowEndUserDocking = value; }
        }

        [LocalizedDescription("DockContent_AutoHidePortion_Description"), DefaultValue((double) 0.25),
         LocalizedCategory("Category_Docking")]
        public double AutoHidePortion
        {
            get { return this.DockHandler.AutoHidePortion; }
            set { this.DockHandler.AutoHidePortion = value; }
        }

        [LocalizedDescription("DockContent_CloseButton_Description"), LocalizedCategory("Category_Docking"),
         DefaultValue(true)]
        public bool CloseButton
        {
            get { return this.DockHandler.CloseButton; }
            set { this.DockHandler.CloseButton = value; }
        }

        [DefaultValue(63), LocalizedDescription("DockContent_DockAreas_Description"),
         LocalizedCategory("Category_Docking")]
        public DockAreas DockAreas
        {
            get { return this.DockHandler.DockAreas; }
            set { this.DockHandler.DockAreas = value; }
        }

        [Browsable(false)]
        public DockContentHandler DockHandler
        {
            get { return this.m_dockHandler; }
        }

        [Browsable(false)]
        public DockPanel DockPanel
        {
            get { return this.DockHandler.DockPanel; }
            set { this.DockHandler.DockPanel = value; }
        }

        [Browsable(false)]
        public DockState DockState
        {
            get { return this.DockHandler.DockState; }
            set { this.DockHandler.DockState = value; }
        }

        [Browsable(false)]
        public DockPane FloatPane
        {
            get { return this.DockHandler.FloatPane; }
            set { this.DockHandler.FloatPane = value; }
        }

        [LocalizedCategory("Category_Docking"), DefaultValue(false),
         LocalizedDescription("DockContent_HideOnClose_Description")]
        public bool HideOnClose
        {
            get { return this.DockHandler.HideOnClose; }
            set { this.DockHandler.HideOnClose = value; }
        }

        [Browsable(false)]
        public bool IsActivated
        {
            get { return this.DockHandler.IsActivated; }
        }

        [Browsable(false)]
        public bool IsFloat
        {
            get { return this.DockHandler.IsFloat; }
            set { this.DockHandler.IsFloat = value; }
        }

        [Browsable(false)]
        public bool IsHidden
        {
            get { return this.DockHandler.IsHidden; }
            set { this.DockHandler.IsHidden = value; }
        }

        [Browsable(false)]
        public DockPane Pane
        {
            get { return this.DockHandler.Pane; }
            set { this.DockHandler.Pane = value; }
        }

        [Browsable(false)]
        public DockPane PanelPane
        {
            get { return this.DockHandler.PanelPane; }
            set { this.DockHandler.PanelPane = value; }
        }

        [LocalizedCategory("Category_Docking"), DefaultValue(0),
         LocalizedDescription("DockContent_ShowHint_Description")]
        public DockState ShowHint
        {
            get { return this.DockHandler.ShowHint; }
            set { this.DockHandler.ShowHint = value; }
        }

        [LocalizedDescription("DockContent_TabPageContextMenu_Description"), DefaultValue((string) null),
         LocalizedCategory("Category_Docking")]
        public ContextMenu TabPageContextMenu
        {
            get { return this.DockHandler.TabPageContextMenu; }
            set { this.DockHandler.TabPageContextMenu = value; }
        }

        [LocalizedDescription("DockContent_TabPageContextMenuStrip_Description"), LocalizedCategory("Category_Docking"),
         DefaultValue((string) null)]
        public ContextMenuStrip TabPageContextMenuStrip
        {
            get { return this.DockHandler.TabPageContextMenuStrip; }
            set { this.DockHandler.TabPageContextMenuStrip = value; }
        }

        [Localizable(true), LocalizedDescription("DockContent_TabText_Description"), DefaultValue((string) null),
         LocalizedCategory("Category_Docking")]
        public string TabText
        {
            get { return this.DockHandler.TabText; }
            set { this.DockHandler.TabText = value; }
        }

        [Category("Appearance"), LocalizedDescription("DockContent_ToolTipText_Description"),
         DefaultValue((string) null), Localizable(true)]
        public string ToolTipText
        {
            get { return this.DockHandler.ToolTipText; }
            set { this.DockHandler.ToolTipText = value; }
        }

        [Browsable(false)]
        public DockState VisibleState
        {
            get { return this.DockHandler.VisibleState; }
            set { this.DockHandler.VisibleState = value; }
        }
    }
}