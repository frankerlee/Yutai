using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public class NewSymbolButton : Button
    {
        private object object_0 = null;

        private Container container_0 = null;

        public object Style
        {
            get { return this.object_0; }
            set
            {
                this.object_0 = value;
                base.Invalidate();
            }
        }

        public NewSymbolButton()
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && this.container_0 != null)
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void method_0()
        {
            base.Paint += new System.Windows.Forms.PaintEventHandler(this.NewSymbolButton_Paint);
        }

        private void NewSymbolButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.object_0 != null)
            {
                System.IntPtr hdc = e.Graphics.GetHdc();
                IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw(this.object_0);
                if (styleDraw != null)
                {
                    Rectangle clipRectangle = e.ClipRectangle;
                    clipRectangle.Inflate(-3, -3);
                    styleDraw.Draw(hdc.ToInt32(), clipRectangle, 96.0, 1.0);
                }
                e.Graphics.ReleaseHdc(hdc);
            }
        }
    }
}