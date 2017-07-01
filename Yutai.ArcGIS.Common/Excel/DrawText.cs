using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class DrawText : DrawBase
    {
        private bool bool_0;
        private System.Drawing.Font font_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private static int intCurrentCharIndex;
        private string string_0;
        private System.Drawing.StringFormat stringFormat_0;

        public DrawText()
        {
            this.string_0 = "";
            this.font_0 = new System.Drawing.Font("宋体", 10f);
            this.int_0 = 0;
            this.int_1 = 0;
            this.int_2 = 0;
            this.stringFormat_0 = new System.Drawing.StringFormat(StringFormatFlags.LineLimit);
        }

        public DrawText(string string_1) : this()
        {
            this.string_0 = string_1;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.font_0.Dispose();
            this.stringFormat_0.Dispose();
        }

        public override void Draw()
        {
            this.method_0();
        }

        private void method_0()
        {
            if (base.Graphics != null)
            {
                int num;
                int num2;
                base.Graphics.MeasureString(this.string_0.Substring(this.int_0), this.font_0,
                    new SizeF((float) base.Rectangle.Width, (float) base.Rectangle.Height), this.stringFormat_0, out num,
                    out num2);
                base.Graphics.DrawString(this.string_0.Substring(this.int_0), this.font_0, base.Brush, base.Rectangle,
                    this.stringFormat_0);
                this.int_1 = num2;
                this.int_2 = num;
            }
        }

        private bool method_1(DrawText drawText_0, string string_1)
        {
            DrawText text = new DrawText(string_1);
            text = this;
            text.StartChar = intCurrentCharIndex;
            intCurrentCharIndex += text.CharsFitted;
            if (intCurrentCharIndex < string_1.Length)
            {
                return true;
            }
            intCurrentCharIndex = 0;
            return false;
        }

        public int CharsFitted
        {
            get { return this.int_2; }
        }

        public System.Drawing.Font Font
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

        public int LinesFilled
        {
            get { return this.int_1; }
        }

        public int StartChar
        {
            get { return this.int_0; }
            set
            {
                this.int_0 = value;
                if (this.int_0 < 0)
                {
                    this.int_0 = 0;
                }
                else if (this.int_0 >= this.Text.Length)
                {
                    this.int_0 = this.Text.Length;
                }
            }
        }

        public System.Drawing.StringFormat StringFormat
        {
            get { return this.stringFormat_0; }
            set { this.stringFormat_0 = value; }
        }

        public string Text
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}