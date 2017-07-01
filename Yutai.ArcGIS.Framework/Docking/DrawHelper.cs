using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal static class DrawHelper
    {
        public static GraphicsPath CalculateGraphicsPathFromBitmap(Bitmap bitmap)
        {
            return CalculateGraphicsPathFromBitmap(bitmap, Color.Empty);
        }

        public static GraphicsPath CalculateGraphicsPathFromBitmap(Bitmap bitmap, Color colorTransparent)
        {
            GraphicsPath path = new GraphicsPath();
            if (colorTransparent == Color.Empty)
            {
                colorTransparent = bitmap.GetPixel(0, 0);
            }
            for (int i = 0; i < bitmap.Height; i++)
            {
                int x = 0;
                for (int j = 0; j < bitmap.Width; j++)
                {
                    if (!(bitmap.GetPixel(j, i) != colorTransparent))
                    {
                        continue;
                    }
                    x = j;
                    int num4 = j;
                    num4 = x;
                    while (num4 < bitmap.Width)
                    {
                        if (bitmap.GetPixel(num4, i) == colorTransparent)
                        {
                            break;
                        }
                        num4++;
                    }
                    path.AddRectangle(new Rectangle(x, i, num4 - x, 1));
                    j = num4;
                }
            }
            return path;
        }

        public static GraphicsPath GetRoundedCornerTab(GraphicsPath graphicsPath, Rectangle rect, bool upCorner)
        {
            if (graphicsPath == null)
            {
                graphicsPath = new GraphicsPath();
            }
            else
            {
                graphicsPath.Reset();
            }
            int width = 6;
            if (upCorner)
            {
                graphicsPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + (width/2));
                graphicsPath.AddArc(new Rectangle(rect.Left, rect.Top, width, width), 180f, 90f);
                graphicsPath.AddLine(rect.Left + (width/2), rect.Top, rect.Right - (width/2), rect.Top);
                graphicsPath.AddArc(new Rectangle(rect.Right - width, rect.Top, width, width), -90f, 90f);
                graphicsPath.AddLine(rect.Right, rect.Top + (width/2), rect.Right, rect.Bottom);
                return graphicsPath;
            }
            graphicsPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom - (width/2));
            graphicsPath.AddArc(new Rectangle(rect.Right - width, rect.Bottom - width, width, width), 0f, 90f);
            graphicsPath.AddLine(rect.Right - (width/2), rect.Bottom, rect.Left + (width/2), rect.Bottom);
            graphicsPath.AddArc(new Rectangle(rect.Left, rect.Bottom - width, width, width), 90f, 90f);
            graphicsPath.AddLine(rect.Left, rect.Bottom - (width/2), rect.Left, rect.Top);
            return graphicsPath;
        }

        public static Point RtlTransform(Control control, Point point)
        {
            if (control.RightToLeft != RightToLeft.Yes)
            {
                return point;
            }
            return new Point(control.Right - point.X, point.Y);
        }

        public static Rectangle RtlTransform(Control control, Rectangle rectangle)
        {
            if (control.RightToLeft != RightToLeft.Yes)
            {
                return rectangle;
            }
            return new Rectangle(control.ClientRectangle.Right - rectangle.Right, rectangle.Y, rectangle.Width,
                rectangle.Height);
        }
    }
}