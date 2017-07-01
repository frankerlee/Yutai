using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class DrawRectangle : DrawBase
    {
        private BordersEdgeFlag bordersEdgeFlag_0 = BordersEdgeFlag.FourEdge;
        private Color color_0 = Color.White;

        public override void Draw()
        {
            switch (this.BordersEdge)
            {
                case BordersEdgeFlag.Left:
                    this.DrawLeftLine();
                    break;

                case BordersEdgeFlag.Right:
                    this.DrawRightLine();
                    break;

                case BordersEdgeFlag.Top:
                    this.DrawTopLine();
                    break;

                case BordersEdgeFlag.Bottom:
                    this.DrawBottomLine();
                    break;

                case BordersEdgeFlag.DiagonalDown:
                    this.DrawDiagonalDownLine();
                    break;

                case BordersEdgeFlag.DiagonalUp:
                    this.DrawDiagonalUpLine();
                    break;

                default:
                    this.Draw(base.Graphics, base.Pen, base.Rectangle);
                    break;
            }
            if (this.BackColor != Color.White)
            {
                this.FillRectangle();
            }
        }

        protected void Draw(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            graphics_1.DrawRectangle(pen_1, rectangle_1.X, rectangle_1.Y, rectangle_1.Width, rectangle_1.Height);
        }

        protected void DrawBackColor(Graphics graphics_1, Brush brush_1, Rectangle rectangle_1)
        {
            graphics_1.FillRectangle(brush_1, rectangle_1);
        }

        public void DrawBottomLine()
        {
            this.DrawBottomLine(base.Graphics, base.Pen, base.Rectangle);
        }

        protected void DrawBottomLine(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            int left = rectangle_1.Left;
            int right = rectangle_1.Right;
            int bottom = rectangle_1.Bottom;
            graphics_1.DrawLine(pen_1, left, bottom, right, bottom);
        }

        public void DrawDiagonalDownLine()
        {
            this.DrawDiagonalDownLine(base.Graphics, base.Pen, base.Rectangle);
        }

        protected void DrawDiagonalDownLine(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            int x = rectangle_1.X;
            int y = rectangle_1.Y;
            int right = rectangle_1.Right;
            int bottom = rectangle_1.Bottom;
            graphics_1.DrawLine(pen_1, x, y, right, bottom);
        }

        public void DrawDiagonalUpLine()
        {
            this.DrawDiagonalUpLine(base.Graphics, base.Pen, base.Rectangle);
        }

        protected void DrawDiagonalUpLine(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            int x = rectangle_1.X;
            int bottom = rectangle_1.Bottom;
            int right = rectangle_1.Right;
            int top = rectangle_1.Top;
            graphics_1.DrawLine(pen_1, x, bottom, right, top);
        }

        public void DrawLeftLine()
        {
            this.DrawLeftLine(base.Graphics, base.Pen, base.Rectangle);
        }

        protected void DrawLeftLine(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            int left = rectangle_1.Left;
            int top = rectangle_1.Top;
            int bottom = rectangle_1.Bottom;
            graphics_1.DrawLine(pen_1, left, top, left, bottom);
        }

        public void DrawRightLine()
        {
            this.DrawRightLine(base.Graphics, base.Pen, base.Rectangle);
        }

        protected void DrawRightLine(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            int right = rectangle_1.Right;
            int top = rectangle_1.Top;
            int bottom = rectangle_1.Bottom;
            graphics_1.DrawLine(pen_1, right, top, right, bottom);
        }

        public void DrawTopLine()
        {
            this.DrawTopLine(base.Graphics, base.Pen, base.Rectangle);
        }

        protected void DrawTopLine(Graphics graphics_1, Pen pen_1, Rectangle rectangle_1)
        {
            int left = rectangle_1.Left;
            int right = rectangle_1.Right;
            int top = rectangle_1.Top;
            graphics_1.DrawLine(pen_1, left, top, right, top);
        }

        public void FillRectangle()
        {
            Pen pen = new Pen(this.BackColor);
            base.Brush = pen.Brush;
            this.DrawBackColor(base.Graphics, base.Brush, base.Rectangle);
        }

        public Color BackColor
        {
            get { return this.color_0; }
            set { this.color_0 = value; }
        }

        public BordersEdgeFlag BordersEdge
        {
            get { return this.bordersEdgeFlag_0; }
            set { this.bordersEdgeFlag_0 = value; }
        }
    }
}