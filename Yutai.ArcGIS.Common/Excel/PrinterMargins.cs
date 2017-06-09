using System.Drawing.Printing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class PrinterMargins : Margins
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;

        public PrinterMargins() : this(1, 1, 1, 1, 0, 0)
        {
        }

        public PrinterMargins(PrintDocument printDocument_0)
        {
            PrinterMargins margins = new PrinterMargins();
            margins = this.method_0(printDocument_0);
            base.Left = margins.Left;
            base.Right = margins.Right;
            base.Top = margins.Top;
            base.Bottom = margins.Bottom;
            this.Width = margins.Width;
            this.Height = margins.Height;
            margins = null;
            this.method_2();
        }

        public PrinterMargins(int int_6, int int_7, int int_8, int int_9, int int_10, int int_11) : base(int_6, int_7, int_8, int_9)
        {
            this.int_0 = int_10;
            this.int_1 = int_11;
            this.method_2();
        }

        private PrinterMargins method_0(PrintDocument printDocument_0)
        {
            int left = printDocument_0.DefaultPageSettings.Margins.Left;
            int right = printDocument_0.DefaultPageSettings.Margins.Right;
            int top = printDocument_0.DefaultPageSettings.Margins.Top;
            int bottom = printDocument_0.DefaultPageSettings.Margins.Bottom;
            int width = printDocument_0.DefaultPageSettings.PaperSize.Width;
            int height = printDocument_0.DefaultPageSettings.PaperSize.Height;
            if (printDocument_0.DefaultPageSettings.Landscape)
            {
                this.method_1(ref width, ref height);
            }
            width = (width - left) - right;
            return new PrinterMargins(left, right, top, bottom, width, (height - top) - bottom);
        }

        private void method_1(ref int int_6, ref int int_7)
        {
            int num = int_6;
            int_6 = int_7;
            int_7 = num;
        }

        private void method_2()
        {
            this.int_2 = base.Left;
            this.int_3 = base.Left + this.int_0;
            this.int_4 = base.Top;
            this.int_5 = base.Top + this.int_1;
        }

        public int Height
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int Width
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

        public int X1
        {
            get
            {
                return this.int_2;
            }
        }

        public int X2
        {
            get
            {
                return this.int_3;
            }
        }

        public int Y1
        {
            get
            {
                return this.int_4;
            }
        }

        public int Y2
        {
            get
            {
                return this.int_5;
            }
        }
    }
}

