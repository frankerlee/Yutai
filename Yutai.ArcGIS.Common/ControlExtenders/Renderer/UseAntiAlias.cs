using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Yutai.ArcGIS.Common.ControlExtenders.Renderer
{
    public class UseAntiAlias : IDisposable
    {
        private Graphics graphics_0;
        private SmoothingMode smoothingMode_0;

        public UseAntiAlias(Graphics graphics_1)
        {
            this.graphics_0 = graphics_1;
            this.smoothingMode_0 = this.graphics_0.SmoothingMode;
            this.graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void Dispose()
        {
            this.graphics_0.SmoothingMode = this.smoothingMode_0;
        }
    }
}

