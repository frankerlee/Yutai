using System.Drawing;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public abstract class StyleDraw : IStyleDraw
    {
        protected object m_pStyle;

        public StyleDraw(object object_0)
        {
            this.m_pStyle = object_0;
        }

        public abstract void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1);

        public Bitmap StyleGalleryItemToBmp(Size size_0, double double_0, double double_1)
        {
            Bitmap bitmap = new Bitmap(size_0.Width, size_0.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            System.IntPtr hdc = graphics.GetHdc();
            Rectangle rectangle_ = new Rectangle(0, 0, size_0.Width, size_0.Height);
            this.Draw(hdc.ToInt32(), rectangle_, double_0, double_1);
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
            return bitmap;
        }

        public void StyleGalleryItemToBmp(Bitmap bitmap_0, double double_0, double double_1)
        {
            Size size = bitmap_0.Size;
            Graphics graphics = Graphics.FromImage(bitmap_0);
            System.IntPtr hdc = graphics.GetHdc();
            Rectangle rectangle_ = new Rectangle(0, 0, size.Width, size.Height);
            this.Draw(hdc.ToInt32(), rectangle_, double_0, double_1);
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
        }
    }
}