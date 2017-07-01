using System;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public abstract class DrawBase : IDraw, IDisposable
    {
        private System.Drawing.Brush brush_0 = Brushes.Black;
        private System.Drawing.Graphics graphics_0;
        private System.Drawing.Pen pen_0;
        private System.Drawing.Rectangle rectangle_0 = new System.Drawing.Rectangle(0, 0, 0, 0);

        public DrawBase()
        {
            this.pen_0 = new System.Drawing.Pen(this.brush_0);
            this.pen_0 = Pens.Black;
        }

        public virtual void Dispose()
        {
            this.brush_0.Dispose();
            this.pen_0.Dispose();
        }

        public abstract void Draw();

        public System.Drawing.Brush Brush
        {
            get { return this.brush_0; }
            set
            {
                if (value != null)
                {
                    this.brush_0 = value;
                }
            }
        }

        public System.Drawing.Graphics Graphics
        {
            get { return this.graphics_0; }
            set { this.graphics_0 = value; }
        }

        public System.Drawing.Pen Pen
        {
            get { return this.pen_0; }
            set
            {
                if (value != null)
                {
                    this.pen_0 = value;
                }
            }
        }

        public System.Drawing.Rectangle Rectangle
        {
            get { return this.rectangle_0; }
            set { this.rectangle_0 = value; }
        }
    }
}