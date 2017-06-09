using System;
using System.Drawing;
using System.Drawing.Text;

namespace Yutai.ArcGIS.Common.ControlExtenders.Renderer
{
    public class UseClearTypeGridFit : IDisposable
    {
        private Graphics graphics_0;
        private TextRenderingHint textRenderingHint_0;

        public UseClearTypeGridFit(Graphics graphics_1)
        {
            this.graphics_0 = graphics_1;
            this.textRenderingHint_0 = this.graphics_0.TextRenderingHint;
            this.graphics_0.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        public void Dispose()
        {
            this.graphics_0.TextRenderingHint = this.textRenderingHint_0;
        }
    }
}

