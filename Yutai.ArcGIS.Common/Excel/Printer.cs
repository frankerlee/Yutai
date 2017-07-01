using System;
using System.Drawing;


namespace Yutai.ArcGIS.Common.Excel
{
    public class Printer : PrinterBase
    {
        private bool bool_0 = false;
        private System.Drawing.Font font_0 = new System.Drawing.Font("宋体", 10f);
        private int int_6 = 0;
        private int int_7 = 0;
        private int int_8 = 0;
        private Sewing sewing_0 = new Sewing();

        protected virtual void CheckGraphics()
        {
            if (base.Graphics == null)
            {
                throw new Exception("绘图表面不能为空，请设置Graphics属性！");
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            this.sewing_0.Dispose();
            this.font_0.Dispose();
        }

        public override void Draw()
        {
            this.CheckGraphics();
        }

        public virtual void DrawPrinterMargins()
        {
            this.CheckGraphics();
            Rectangle rect = new Rectangle(base.PrinterMargins.Left, base.PrinterMargins.Top, base.PrinterMargins.Width,
                base.PrinterMargins.Height);
            base.Graphics.DrawRectangle(base.Pen, rect);
        }

        public virtual void DrawSewing()
        {
            this.CheckGraphics();
            this.Sewing.LineLen = this.method_2();
            this.Sewing.Draw(base.Graphics);
        }

        private int method_2()
        {
            int pageWidth = 0;
            if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
            {
                return base.PageHeight;
            }
            if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
            {
                pageWidth = base.PageWidth;
            }
            return pageWidth;
        }

        public int TextHeight()
        {
            return this.Font.Height;
        }

        public int TextHeight(string string_0)
        {
            return (int) base.Graphics.MeasureString(string_0, this.Font, base.PrinterMargins.Width).Height;
        }

        public int TextWidth(string string_0)
        {
            return (int) base.Graphics.MeasureString(string_0, this.Font, base.PrinterMargins.Width).Width;
        }

        public virtual Font Font
        {
            get { return this.font_0; }
            set
            {
                if (value != null)
                {
                    this.font_0 = value;
                }
            }
        }

        public virtual int Height
        {
            get { return this.int_6; }
            set
            {
                this.int_6 = value;
                if (this.int_6 < 0)
                {
                    this.int_6 = 0;
                }
            }
        }

        public virtual bool IsDrawAllPage
        {
            get { return this.bool_0; }
            set { this.bool_0 = value; }
        }

        public int MoveX
        {
            get { return this.int_7; }
            set { this.int_7 = value; }
        }

        public int MoveY
        {
            get { return this.int_8; }
            set { this.int_8 = value; }
        }

        public virtual Sewing Sewing
        {
            get { return this.sewing_0; }
            set
            {
                if (value != null)
                {
                    this.sewing_0 = value;
                }
            }
        }
    }
}