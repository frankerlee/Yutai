using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Yutai.ArcGIS.Common.Renderer
{
    public class UseClipping : IDisposable
    {
        private Graphics graphics_0;
        private Region region_0;

        public UseClipping(Graphics graphics_1, GraphicsPath graphicsPath_0)
        {
            this.graphics_0 = graphics_1;
            this.region_0 = graphics_1.Clip;
            Region region = this.region_0.Clone();
            region.Intersect(graphicsPath_0);
            this.graphics_0.Clip = region;
        }

        public UseClipping(Graphics graphics_1, Region region_1)
        {
            this.graphics_0 = graphics_1;
            this.region_0 = graphics_1.Clip;
            Region region = this.region_0.Clone();
            region.Intersect(region_1);
            this.graphics_0.Clip = region;
        }

        public void Dispose()
        {
            this.graphics_0.Clip = this.region_0;
        }
    }
}

