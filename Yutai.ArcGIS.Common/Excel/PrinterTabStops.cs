using System;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class PrinterTabStops : IDisposable
    {
        public System.Drawing.Font Font;
        private int int_0;
        public System.Drawing.Rectangle Rectangle;
        private string string_0;
        private string string_1;

        public PrinterTabStops()
        {
            this.Font = new System.Drawing.Font("宋体", 10f);
            this.Rectangle = new System.Drawing.Rectangle(0, 0, this.Font.Height, 100);
        }

        public PrinterTabStops(string string_2) : this()
        {
            this.Text = string_2;
        }

        public void Dispose()
        {
            this.Font.Dispose();
        }

        public void Draw(Graphics graphics_0)
        {
            StringFormat format = new StringFormat {
                FormatFlags = StringFormatFlags.NoWrap
            };
            float num = this.Rectangle.Width / this.int_0;
            float[] tabStops = new float[this.int_0];
            for (int i = 0; i < this.int_0; i++)
            {
                tabStops[i] = num;
            }
            format.SetTabStops(0f, tabStops);
            graphics_0.DrawString(this.string_1, this.Font, Brushes.Black, this.Rectangle, format);
        }

        private string method_0(string string_2)
        {
            string str = string_2;
            return str.Replace("|", "\t");
        }

        public int Cols
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
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
                string str = this.string_0;
                this.string_1 = this.method_0(str);
            }
        }
    }
}

