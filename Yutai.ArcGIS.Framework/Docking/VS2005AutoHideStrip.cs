using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal class VS2005AutoHideStrip : AutoHideStripBase
    {
        private static DockState[] _dockStates;
        private static System.Drawing.Drawing2D.GraphicsPath _graphicsPath;
        private const int _ImageGapBottom = 2;
        private const int _ImageGapLeft = 4;
        private const int _ImageGapRight = 2;
        private const int _ImageGapTop = 2;
        private const int _ImageHeight = 16;
        private const int _ImageWidth = 16;
        private static Matrix _matrixIdentity = new Matrix();
        private static StringFormat _stringFormatTabHorizontal;
        private static StringFormat _stringFormatTabVertical;
        private const int _TabGapBetween = 10;
        private const int _TabGapLeft = 4;
        private const int _TabGapTop = 3;
        private const int _TextGapLeft = 0;
        private const int _TextGapRight = 0;

        public VS2005AutoHideStrip(DockPanel panel) : base(panel)
        {
            base.SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
            this.BackColor = SystemColors.ControlLight;
        }

        private void CalculateTabs()
        {
            this.CalculateTabs(DockState.DockTopAutoHide);
            this.CalculateTabs(DockState.DockBottomAutoHide);
            this.CalculateTabs(DockState.DockLeftAutoHide);
            this.CalculateTabs(DockState.DockRightAutoHide);
        }

        private void CalculateTabs(DockState dockState)
        {
            Rectangle logicalTabStripRectangle = this.GetLogicalTabStripRectangle(dockState);
            int num = (logicalTabStripRectangle.Height - ImageGapTop) - ImageGapBottom;
            int imageWidth = ImageWidth;
            if (num > ImageHeight)
            {
                imageWidth = ImageWidth*(num/ImageHeight);
            }
            int num3 = TabGapLeft + logicalTabStripRectangle.X;
            foreach (AutoHideStripBase.Pane pane in (IEnumerable<AutoHideStripBase.Pane>) base.GetPanes(dockState))
            {
                foreach (TabVS2005 bvs in (IEnumerable<AutoHideStripBase.Tab>) pane.AutoHideTabs)
                {
                    int num4 = ((((imageWidth + ImageGapLeft) + ImageGapRight) +
                                 TextRenderer.MeasureText(bvs.Content.DockHandler.TabText, TextFont).Width) +
                                TextGapLeft) + TextGapRight;
                    bvs.TabX = num3;
                    bvs.TabWidth = num4;
                    num3 += num4;
                }
                num3 += TabGapBetween;
            }
        }

        protected override AutoHideStripBase.Tab CreateTab(IDockContent content)
        {
            return new TabVS2005(content);
        }

        private void DrawTab(Graphics g, TabVS2005 tab)
        {
            Rectangle tabRectangle = this.GetTabRectangle(tab);
            if (!tabRectangle.IsEmpty)
            {
                DockState dockState = tab.Content.DockHandler.DockState;
                IDockContent content = tab.Content;
                System.Drawing.Drawing2D.GraphicsPath path = this.GetTabOutline(tab, false, true);
                g.FillPath(BrushTabBackground, path);
                g.DrawPath(PenTabBorder, path);
                Matrix transform = g.Transform;
                g.Transform = MatrixIdentity;
                Rectangle rect = tabRectangle;
                rect.X += ImageGapLeft;
                rect.Y += ImageGapTop;
                int num = (tabRectangle.Height - ImageGapTop) - ImageGapBottom;
                int imageWidth = ImageWidth;
                if (num > ImageHeight)
                {
                    imageWidth = ImageWidth*(num/ImageHeight);
                }
                rect.Height = num;
                rect.Width = imageWidth;
                rect = this.GetTransformedRectangle(dockState, rect);
                g.DrawIcon(((Form) content).Icon, this.RtlTransform(rect, dockState));
                Rectangle rectangle3 = tabRectangle;
                rectangle3.X += ((ImageGapLeft + imageWidth) + ImageGapRight) + TextGapLeft;
                rectangle3.Width -= ((ImageGapLeft + imageWidth) + ImageGapRight) + TextGapLeft;
                rectangle3 = this.RtlTransform(this.GetTransformedRectangle(dockState, rectangle3), dockState);
                if ((dockState == DockState.DockLeftAutoHide) || (dockState == DockState.DockRightAutoHide))
                {
                    g.DrawString(content.DockHandler.TabText, TextFont, BrushTabText, rectangle3,
                        this.StringFormatTabVertical);
                }
                else
                {
                    g.DrawString(content.DockHandler.TabText, TextFont, BrushTabText, rectangle3,
                        this.StringFormatTabHorizontal);
                }
                g.Transform = transform;
            }
        }

        private void DrawTabStrip(Graphics g)
        {
            this.DrawTabStrip(g, DockState.DockTopAutoHide);
            this.DrawTabStrip(g, DockState.DockBottomAutoHide);
            this.DrawTabStrip(g, DockState.DockLeftAutoHide);
            this.DrawTabStrip(g, DockState.DockRightAutoHide);
        }

        private void DrawTabStrip(Graphics g, DockState dockState)
        {
            Rectangle logicalTabStripRectangle = this.GetLogicalTabStripRectangle(dockState);
            if (!logicalTabStripRectangle.IsEmpty)
            {
                Matrix transform = g.Transform;
                if ((dockState == DockState.DockLeftAutoHide) || (dockState == DockState.DockRightAutoHide))
                {
                    Matrix matrix2 = new Matrix();
                    matrix2.RotateAt(90f,
                        new PointF(logicalTabStripRectangle.X + (((float) logicalTabStripRectangle.Height)/2f),
                            logicalTabStripRectangle.Y + (((float) logicalTabStripRectangle.Height)/2f)));
                    g.Transform = matrix2;
                }
                foreach (AutoHideStripBase.Pane pane in (IEnumerable<AutoHideStripBase.Pane>) base.GetPanes(dockState))
                {
                    foreach (TabVS2005 bvs in (IEnumerable<AutoHideStripBase.Tab>) pane.AutoHideTabs)
                    {
                        this.DrawTab(g, bvs);
                    }
                }
                g.Transform = transform;
            }
        }

        private Rectangle GetLogicalTabStripRectangle(DockState dockState)
        {
            return this.GetLogicalTabStripRectangle(dockState, false);
        }

        private Rectangle GetLogicalTabStripRectangle(DockState dockState, bool transformed)
        {
            int num5;
            int num6;
            int num7;
            int num8;
            if (DockHelper.IsDockStateAutoHide(dockState))
            {
                int count = base.GetPanes(DockState.DockLeftAutoHide).Count;
                int num2 = base.GetPanes(DockState.DockRightAutoHide).Count;
                int num3 = base.GetPanes(DockState.DockTopAutoHide).Count;
                int num4 = base.GetPanes(DockState.DockBottomAutoHide).Count;
                num8 = this.MeasureHeight();
                if ((dockState == DockState.DockLeftAutoHide) && (count > 0))
                {
                    num5 = 0;
                    num6 = (num3 == 0) ? 0 : num8;
                    num7 = (base.Height - ((num3 == 0) ? 0 : num8)) - ((num4 == 0) ? 0 : num8);
                    goto Label_01A2;
                }
                if ((dockState == DockState.DockRightAutoHide) && (num2 > 0))
                {
                    num5 = base.Width - num8;
                    if ((count != 0) && (num5 < num8))
                    {
                        num5 = num8;
                    }
                    num6 = (num3 == 0) ? 0 : num8;
                    num7 = (base.Height - ((num3 == 0) ? 0 : num8)) - ((num4 == 0) ? 0 : num8);
                    goto Label_01A2;
                }
                if ((dockState == DockState.DockTopAutoHide) && (num3 > 0))
                {
                    num5 = (count == 0) ? 0 : num8;
                    num6 = 0;
                    num7 = (base.Width - ((count == 0) ? 0 : num8)) - ((num2 == 0) ? 0 : num8);
                    goto Label_01A2;
                }
                if ((dockState == DockState.DockBottomAutoHide) && (num4 > 0))
                {
                    num5 = (count == 0) ? 0 : num8;
                    num6 = base.Height - num8;
                    if ((num3 != 0) && (num6 < num8))
                    {
                        num6 = num8;
                    }
                    num7 = (base.Width - ((count == 0) ? 0 : num8)) - ((num2 == 0) ? 0 : num8);
                    goto Label_01A2;
                }
            }
            return Rectangle.Empty;
            Label_01A2:
            if (!transformed)
            {
                return new Rectangle(num5, num6, num7, num8);
            }
            return this.GetTransformedRectangle(dockState, new Rectangle(num5, num6, num7, num8));
        }

        private System.Drawing.Drawing2D.GraphicsPath GetTabOutline(TabVS2005 tab, bool transformed, bool rtlTransform)
        {
            DockState dockState = tab.Content.DockHandler.DockState;
            Rectangle tabRectangle = this.GetTabRectangle(tab, transformed);
            if (rtlTransform)
            {
                tabRectangle = this.RtlTransform(tabRectangle, dockState);
            }
            bool upCorner = (dockState == DockState.DockLeftAutoHide) || (dockState == DockState.DockBottomAutoHide);
            DrawHelper.GetRoundedCornerTab(GraphicsPath, tabRectangle, upCorner);
            return GraphicsPath;
        }

        private Rectangle GetTabRectangle(TabVS2005 tab)
        {
            return this.GetTabRectangle(tab, false);
        }

        private Rectangle GetTabRectangle(TabVS2005 tab, bool transformed)
        {
            DockState dockState = tab.Content.DockHandler.DockState;
            Rectangle logicalTabStripRectangle = this.GetLogicalTabStripRectangle(dockState);
            if (logicalTabStripRectangle.IsEmpty)
            {
                return Rectangle.Empty;
            }
            int tabX = tab.TabX;
            int y = logicalTabStripRectangle.Y +
                    (((dockState == DockState.DockTopAutoHide) || (dockState == DockState.DockRightAutoHide))
                        ? 0
                        : TabGapTop);
            int tabWidth = tab.TabWidth;
            int height = logicalTabStripRectangle.Height - TabGapTop;
            if (!transformed)
            {
                return new Rectangle(tabX, y, tabWidth, height);
            }
            return this.GetTransformedRectangle(dockState, new Rectangle(tabX, y, tabWidth, height));
        }

        private Rectangle GetTransformedRectangle(DockState dockState, Rectangle rect)
        {
            if ((dockState != DockState.DockLeftAutoHide) && (dockState != DockState.DockRightAutoHide))
            {
                return rect;
            }
            PointF[] pts = new PointF[1];
            pts[0].X = rect.X + (((float) rect.Width)/2f);
            pts[0].Y = rect.Y + (((float) rect.Height)/2f);
            Rectangle logicalTabStripRectangle = this.GetLogicalTabStripRectangle(dockState);
            Matrix matrix = new Matrix();
            matrix.RotateAt(90f,
                new PointF(logicalTabStripRectangle.X + (((float) logicalTabStripRectangle.Height)/2f),
                    logicalTabStripRectangle.Y + (((float) logicalTabStripRectangle.Height)/2f)));
            matrix.TransformPoints(pts);
            return new Rectangle((int) ((pts[0].X - (((float) rect.Height)/2f)) + 0.5f),
                (int) ((pts[0].Y - (((float) rect.Width)/2f)) + 0.5f), rect.Height, rect.Width);
        }

        protected override IDockContent HitTest(Point ptMouse)
        {
            foreach (DockState state in DockStates)
            {
                if (this.GetLogicalTabStripRectangle(state, true).Contains(ptMouse))
                {
                    foreach (AutoHideStripBase.Pane pane in (IEnumerable<AutoHideStripBase.Pane>) base.GetPanes(state))
                    {
                        DockState dockState = pane.DockPane.DockState;
                        foreach (TabVS2005 bvs in (IEnumerable<AutoHideStripBase.Tab>) pane.AutoHideTabs)
                        {
                            if (this.GetTabOutline(bvs, true, true).IsVisible(ptMouse))
                            {
                                return bvs.Content;
                            }
                        }
                    }
                }
            }
            return null;
        }

        protected internal override int MeasureHeight()
        {
            return (Math.Max((ImageGapBottom + ImageGapTop) + ImageHeight, TextFont.Height) + TabGapTop);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.CalculateTabs();
            base.OnLayout(levent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.DrawTabStrip(g);
        }

        protected override void OnRefreshChanges()
        {
            this.CalculateTabs();
            base.Invalidate();
        }

        private Rectangle RtlTransform(Rectangle rect, DockState dockState)
        {
            if ((dockState == DockState.DockLeftAutoHide) || (dockState == DockState.DockRightAutoHide))
            {
                return rect;
            }
            return DrawHelper.RtlTransform(this, rect);
        }

        private static Brush BrushTabBackground
        {
            get { return SystemBrushes.Control; }
        }

        private static Brush BrushTabText
        {
            get { return SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark); }
        }

        private static DockState[] DockStates
        {
            get
            {
                if (_dockStates == null)
                {
                    _dockStates = new DockState[]
                    {
                        DockState.DockLeftAutoHide, DockState.DockRightAutoHide, DockState.DockTopAutoHide,
                        DockState.DockBottomAutoHide
                    };
                }
                return _dockStates;
            }
        }

        internal static System.Drawing.Drawing2D.GraphicsPath GraphicsPath
        {
            get
            {
                if (_graphicsPath == null)
                {
                    _graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
                }
                return _graphicsPath;
            }
        }

        private static int ImageGapBottom
        {
            get { return 2; }
        }

        private static int ImageGapLeft
        {
            get { return 4; }
        }

        private static int ImageGapRight
        {
            get { return 2; }
        }

        private static int ImageGapTop
        {
            get { return 2; }
        }

        private static int ImageHeight
        {
            get { return 16; }
        }

        private static int ImageWidth
        {
            get { return 16; }
        }

        private static Matrix MatrixIdentity
        {
            get { return _matrixIdentity; }
        }

        private static Pen PenTabBorder
        {
            get { return SystemPens.GrayText; }
        }

        private StringFormat StringFormatTabHorizontal
        {
            get
            {
                if (_stringFormatTabHorizontal == null)
                {
                    _stringFormatTabHorizontal = new StringFormat();
                    _stringFormatTabHorizontal.Alignment = StringAlignment.Near;
                    _stringFormatTabHorizontal.LineAlignment = StringAlignment.Center;
                    _stringFormatTabHorizontal.FormatFlags = StringFormatFlags.NoWrap;
                }
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    _stringFormatTabHorizontal.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                else
                {
                    _stringFormatTabHorizontal.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
                }
                return _stringFormatTabHorizontal;
            }
        }

        private StringFormat StringFormatTabVertical
        {
            get
            {
                if (_stringFormatTabVertical == null)
                {
                    _stringFormatTabVertical = new StringFormat();
                    _stringFormatTabVertical.Alignment = StringAlignment.Near;
                    _stringFormatTabVertical.LineAlignment = StringAlignment.Center;
                    _stringFormatTabVertical.FormatFlags = StringFormatFlags.NoWrap |
                                                           StringFormatFlags.DirectionVertical;
                }
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    _stringFormatTabVertical.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                else
                {
                    _stringFormatTabVertical.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
                }
                return _stringFormatTabVertical;
            }
        }

        private static int TabGapBetween
        {
            get { return 10; }
        }

        private static int TabGapLeft
        {
            get { return 4; }
        }

        private static int TabGapTop
        {
            get { return 3; }
        }

        private static Font TextFont
        {
            get { return SystemInformation.MenuFont; }
        }

        private static int TextGapLeft
        {
            get { return 0; }
        }

        private static int TextGapRight
        {
            get { return 0; }
        }

        private class TabVS2005 : AutoHideStripBase.Tab
        {
            private int m_tabWidth;
            private int m_tabX;

            internal TabVS2005(IDockContent content) : base(content)
            {
                this.m_tabX = 0;
                this.m_tabWidth = 0;
            }

            public int TabWidth
            {
                get { return this.m_tabWidth; }
                set { this.m_tabWidth = value; }
            }

            public int TabX
            {
                get { return this.m_tabX; }
                set { this.m_tabX = value; }
            }
        }
    }
}