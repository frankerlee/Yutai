using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class Caption : Printer
    {
        private bool bool_1;
        private int int_9;
        private string string_0;

        public Caption()
        {
            this.string_0 = "";
            this.int_9 = 1;
            this.bool_1 = false;
            this.Font = new Font("宋体", 13f, FontStyle.Italic);
            this.IsDrawAllPage = true;
        }

        public Caption(string string_1) : this()
        {
            this.string_0 = string_1;
        }

        public override void Draw()
        {
            base.Draw();
            int x = base.PrinterMargins.X1;
            int y = base.PrinterMargins.Y1;
            x += base.MoveX;
            y += base.MoveY;
            int width = base.PrinterMargins.Width;
            int height = base.TextHeight(this.string_0);
            if (height > base.PrinterMargins.Height)
            {
                height = base.PrinterMargins.Height;
            }
            if ((this.int_9 > 0) && (height > (this.Font.Height * this.int_9)))
            {
                height = this.Font.Height * this.int_9;
            }
            Rectangle layoutRectangle = new Rectangle(x, y, width, height);
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            layoutRectangle.X = (((base.PrinterMargins.Width - base.TextWidth(this.Text)) / 2) + base.PrinterMargins.Left) + base.MoveX;
            layoutRectangle.Y = y;
            if (base.TextWidth(this.Text) < base.PrinterMargins.Width)
            {
                layoutRectangle.Width = base.TextWidth(this.Text);
            }
            else
            {
                layoutRectangle.Width = base.PrinterMargins.Width;
            }
            layoutRectangle.Height = height;
            base.Rectangle = layoutRectangle;
            if (this.bool_1)
            {
                base.Graphics.DrawRectangle(base.Pen, base.Rectangle.X, base.Rectangle.Y, base.Rectangle.Width, base.Rectangle.Height);
            }
            layoutRectangle.X--;
            layoutRectangle.Y--;
            layoutRectangle.Width += 2;
            layoutRectangle.Height += 2;
            base.Graphics.DrawString(this.string_0, this.Font, base.Brush, layoutRectangle, format);
            this.Height = height;
        }

        public bool HasBorder
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public int MaxRows
        {
            get
            {
                return this.int_9;
            }
            set
            {
                this.int_9 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

