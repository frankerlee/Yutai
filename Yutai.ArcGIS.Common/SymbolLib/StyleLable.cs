using System.Drawing;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public class StyleLable : System.Windows.Forms.Label
    {
        private object object_0 = null;

        private IStyleDraw istyleDraw_0 = null;

        public object Style
        {
            get { return this.object_0; }
            set
            {
                this.object_0 = value;
                this.istyleDraw_0 = StyleDrawFactory.CreateStyleDraw(this.object_0);
                base.Invalidate(base.ClientRectangle);
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs paintEventArgs_0)
        {
            if (this.object_0 == null)
            {
                this.Text = "无法绘制符号!";
            }
            else
            {
                this.Text = "";
                System.IntPtr hdc = paintEventArgs_0.Graphics.GetHdc();
                Rectangle clipRectangle = paintEventArgs_0.ClipRectangle;
                clipRectangle.Inflate(-3, -3);
                if (this.istyleDraw_0 != null)
                {
                    this.istyleDraw_0.Draw(hdc.ToInt32(), clipRectangle, 72.0, 1.0);
                }
                else
                {
                    paintEventArgs_0.Graphics.DrawString(this.Text, this.Font, Brushes.Black, clipRectangle);
                }
                paintEventArgs_0.Graphics.ReleaseHdc(hdc);
                base.OnPaint(paintEventArgs_0);
            }
        }
    }
}