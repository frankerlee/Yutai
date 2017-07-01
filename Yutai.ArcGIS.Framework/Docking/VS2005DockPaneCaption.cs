using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal class VS2005DockPaneCaption : DockPaneCaptionBase
    {
        private static Blend _activeBackColorGradientBlend;
        private const int _ButtonGapBetween = 1;
        private const int _ButtonGapBottom = 1;
        private const int _ButtonGapLeft = 1;
        private const int _ButtonGapRight = 2;
        private const int _ButtonGapTop = 2;
        private static Bitmap _imageButtonAutoHide;
        private static Bitmap _imageButtonClose;
        private static Bitmap _imageButtonDock;
        private static Bitmap _imageButtonOptions;

        private static TextFormatFlags _textFormat = (TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine |
                                                      TextFormatFlags.VerticalCenter);

        private const int _TextGapBottom = 0;
        private const int _TextGapLeft = 3;
        private const int _TextGapRight = 3;
        private const int _TextGapTop = 2;
        private static string _toolTipAutoHide;
        private static string _toolTipClose;
        private static string _toolTipOptions;
        private InertButton m_buttonAutoHide;
        private InertButton m_buttonClose;
        private InertButton m_buttonOptions;
        private IContainer m_components;
        private System.Windows.Forms.ToolTip m_toolTip;

        public VS2005DockPaneCaption(DockPane pane) : base(pane)
        {
            base.SuspendLayout();
            this.m_components = new Container();
            this.m_toolTip = new System.Windows.Forms.ToolTip(this.Components);
            base.ResumeLayout();
        }

        private void AutoHide_Click(object sender, EventArgs e)
        {
            base.DockPane.DockState = DockHelper.ToggleAutoHideState(base.DockPane.DockState);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            base.DockPane.CloseActiveContent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawCaption(Graphics g)
        {
            if (!base.ClientRectangle.IsEmpty)
            {
                if (base.DockPane.IsActivated)
                {
                    using (
                        LinearGradientBrush brush = new LinearGradientBrush(base.ClientRectangle,
                            ActiveBackColorGradientBegin, ActiveBackColorGradientEnd, LinearGradientMode.Vertical))
                    {
                        brush.Blend = ActiveBackColorGradientBlend;
                        g.FillRectangle(brush, base.ClientRectangle);
                    }
                }
                else
                {
                    using (SolidBrush brush2 = new SolidBrush(InactiveBackColor))
                    {
                        g.FillRectangle(brush2, base.ClientRectangle);
                    }
                }
                Rectangle clientRectangle = base.ClientRectangle;
                clientRectangle.X += TextGapLeft;
                clientRectangle.Width -= TextGapLeft + TextGapRight;
                clientRectangle.Width -= (ButtonGapLeft + this.ButtonClose.Width) + ButtonGapRight;
                if (this.ShouldShowAutoHideButton)
                {
                    clientRectangle.Width -= this.ButtonAutoHide.Width + ButtonGapBetween;
                }
                if (base.HasTabPageContextMenu)
                {
                    clientRectangle.Width -= this.ButtonOptions.Width + ButtonGapBetween;
                }
                clientRectangle.Y += TextGapTop;
                clientRectangle.Height -= TextGapTop + TextGapBottom;
                TextRenderer.DrawText(g, base.DockPane.CaptionText, TextFont,
                    DrawHelper.RtlTransform(this, clientRectangle), this.TextColor, this.TextFormat);
            }
        }

        protected internal override int MeasureHeight()
        {
            int num = (TextFont.Height + TextGapTop) + TextGapBottom;
            if (num < ((this.ButtonClose.Image.Height + ButtonGapTop) + ButtonGapBottom))
            {
                num = (this.ButtonClose.Image.Height + ButtonGapTop) + ButtonGapBottom;
            }
            return num;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.SetButtonsPosition();
            base.OnLayout(levent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawCaption(e.Graphics);
        }

        protected override void OnRefreshChanges()
        {
            this.SetButtons();
            base.Invalidate();
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            base.PerformLayout();
        }

        private void Options_Click(object sender, EventArgs e)
        {
            base.ShowTabPageContextMenu(base.PointToClient(Control.MousePosition));
        }

        private void SetButtons()
        {
            this.ButtonClose.Enabled = this.CloseButtonEnabled;
            this.ButtonAutoHide.Visible = this.ShouldShowAutoHideButton;
            this.ButtonOptions.Visible = base.HasTabPageContextMenu;
            this.ButtonClose.RefreshChanges();
            this.ButtonAutoHide.RefreshChanges();
            this.ButtonOptions.RefreshChanges();
            this.SetButtonsPosition();
        }

        private void SetButtonsPosition()
        {
            Rectangle clientRectangle = base.ClientRectangle;
            int width = this.ButtonClose.Image.Width;
            int height = this.ButtonClose.Image.Height;
            int num3 = (clientRectangle.Height - ButtonGapTop) - ButtonGapBottom;
            if (height < num3)
            {
                width *= num3/height;
                height = num3;
            }
            Size size = new Size(width, height);
            int x = (((clientRectangle.X + clientRectangle.Width) - 1) - ButtonGapRight) - this.m_buttonClose.Width;
            int y = clientRectangle.Y + ButtonGapTop;
            Point location = new Point(x, y);
            this.ButtonClose.Bounds = DrawHelper.RtlTransform(this, new Rectangle(location, size));
            location.Offset(-(width + ButtonGapBetween), 0);
            this.ButtonAutoHide.Bounds = DrawHelper.RtlTransform(this, new Rectangle(location, size));
            if (this.ShouldShowAutoHideButton)
            {
                location.Offset(-(width + ButtonGapBetween), 0);
            }
            this.ButtonOptions.Bounds = DrawHelper.RtlTransform(this, new Rectangle(location, size));
        }

        private static Color ActiveBackColorGradientBegin
        {
            get { return SystemColors.GradientActiveCaption; }
        }

        private static Blend ActiveBackColorGradientBlend
        {
            get
            {
                if (_activeBackColorGradientBlend == null)
                {
                    Blend blend = new Blend(2)
                    {
                        Factors = new float[] {0.5f, 1f}
                    };
                    float[] numArray = new float[2];
                    numArray[1] = 1f;
                    blend.Positions = numArray;
                    _activeBackColorGradientBlend = blend;
                }
                return _activeBackColorGradientBlend;
            }
        }

        private static Color ActiveBackColorGradientEnd
        {
            get { return SystemColors.ActiveCaption; }
        }

        private static Color ActiveTextColor
        {
            get { return SystemColors.ActiveCaptionText; }
        }

        private InertButton ButtonAutoHide
        {
            get
            {
                if (this.m_buttonAutoHide == null)
                {
                    this.m_buttonAutoHide = new InertButton(this, ImageButtonDock, ImageButtonAutoHide);
                    this.m_toolTip.SetToolTip(this.m_buttonAutoHide, ToolTipAutoHide);
                    this.m_buttonAutoHide.Click += new EventHandler(this.AutoHide_Click);
                    base.Controls.Add(this.m_buttonAutoHide);
                }
                return this.m_buttonAutoHide;
            }
        }

        private InertButton ButtonClose
        {
            get
            {
                if (this.m_buttonClose == null)
                {
                    this.m_buttonClose = new InertButton(this, ImageButtonClose, ImageButtonClose);
                    this.m_toolTip.SetToolTip(this.m_buttonClose, ToolTipClose);
                    this.m_buttonClose.Click += new EventHandler(this.Close_Click);
                    base.Controls.Add(this.m_buttonClose);
                }
                return this.m_buttonClose;
            }
        }

        private static int ButtonGapBetween
        {
            get { return 1; }
        }

        private static int ButtonGapBottom
        {
            get { return 1; }
        }

        private static int ButtonGapLeft
        {
            get { return 1; }
        }

        private static int ButtonGapRight
        {
            get { return 2; }
        }

        private static int ButtonGapTop
        {
            get { return 2; }
        }

        private InertButton ButtonOptions
        {
            get
            {
                if (this.m_buttonOptions == null)
                {
                    this.m_buttonOptions = new InertButton(this, ImageButtonOptions, ImageButtonOptions);
                    this.m_toolTip.SetToolTip(this.m_buttonOptions, ToolTipOptions);
                    this.m_buttonOptions.Click += new EventHandler(this.Options_Click);
                    base.Controls.Add(this.m_buttonOptions);
                }
                return this.m_buttonOptions;
            }
        }

        private bool CloseButtonEnabled
        {
            get
            {
                return ((base.DockPane.ActiveContent != null)
                    ? base.DockPane.ActiveContent.DockHandler.CloseButton
                    : false);
            }
        }

        private IContainer Components
        {
            get { return this.m_components; }
        }

        private static Bitmap ImageButtonAutoHide
        {
            get
            {
                if (_imageButtonAutoHide == null)
                {
                    _imageButtonAutoHide = Resources.DockPane_AutoHide;
                }
                return _imageButtonAutoHide;
            }
        }

        private static Bitmap ImageButtonClose
        {
            get
            {
                if (_imageButtonClose == null)
                {
                    _imageButtonClose = Resources.DockPane_Close;
                }
                return _imageButtonClose;
            }
        }

        private static Bitmap ImageButtonDock
        {
            get
            {
                if (_imageButtonDock == null)
                {
                    _imageButtonDock = Resources.DockPane_Dock;
                }
                return _imageButtonDock;
            }
        }

        private static Bitmap ImageButtonOptions
        {
            get
            {
                if (_imageButtonOptions == null)
                {
                    _imageButtonOptions = Resources.DockPane_Option;
                }
                return _imageButtonOptions;
            }
        }

        private static Color InactiveBackColor
        {
            get
            {
                switch (VisualStyleInformation.ColorScheme)
                {
                    case "HomeStead":
                    case "Metallic":
                        return SystemColors.GradientInactiveCaption;
                }
                return SystemColors.GrayText;
            }
        }

        private static Color InactiveTextColor
        {
            get { return SystemColors.ControlText; }
        }

        private bool ShouldShowAutoHideButton
        {
            get { return !base.DockPane.IsFloat; }
        }

        private Color TextColor
        {
            get { return (base.DockPane.IsActivated ? ActiveTextColor : InactiveTextColor); }
        }

        private static Font TextFont
        {
            get { return SystemInformation.MenuFont; }
        }

        private TextFormatFlags TextFormat
        {
            get
            {
                if (this.RightToLeft == RightToLeft.No)
                {
                    return _textFormat;
                }
                return ((_textFormat | TextFormatFlags.RightToLeft) | TextFormatFlags.Right);
            }
        }

        private static int TextGapBottom
        {
            get { return 0; }
        }

        private static int TextGapLeft
        {
            get { return 3; }
        }

        private static int TextGapRight
        {
            get { return 3; }
        }

        private static int TextGapTop
        {
            get { return 2; }
        }

        private static string ToolTipAutoHide
        {
            get
            {
                if (_toolTipAutoHide == null)
                {
                    _toolTipAutoHide = Strings.DockPaneCaption_ToolTipAutoHide;
                }
                return _toolTipAutoHide;
            }
        }

        private static string ToolTipClose
        {
            get
            {
                if (_toolTipClose == null)
                {
                    _toolTipClose = Strings.DockPaneCaption_ToolTipClose;
                }
                return _toolTipClose;
            }
        }

        private static string ToolTipOptions
        {
            get
            {
                if (_toolTipOptions == null)
                {
                    _toolTipOptions = Strings.DockPaneCaption_ToolTipOptions;
                }
                return _toolTipOptions;
            }
        }

        private sealed class InertButton : InertButtonBase
        {
            private VS2005DockPaneCaption m_dockPaneCaption;
            private Bitmap m_image;
            private Bitmap m_imageAutoHide;

            public InertButton(VS2005DockPaneCaption dockPaneCaption, Bitmap image, Bitmap imageAutoHide)
            {
                this.m_dockPaneCaption = dockPaneCaption;
                this.m_image = image;
                this.m_imageAutoHide = imageAutoHide;
                base.RefreshChanges();
            }

            protected override void OnRefreshChanges()
            {
                if (this.DockPaneCaption.TextColor != this.ForeColor)
                {
                    this.ForeColor = this.DockPaneCaption.TextColor;
                    base.Invalidate();
                }
            }

            private VS2005DockPaneCaption DockPaneCaption
            {
                get { return this.m_dockPaneCaption; }
            }

            public override Bitmap Image
            {
                get { return (this.IsAutoHide ? this.m_imageAutoHide : this.m_image); }
            }

            public bool IsAutoHide
            {
                get { return this.DockPaneCaption.DockPane.IsAutoHide; }
            }
        }
    }
}