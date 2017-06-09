namespace Yutai.ArcGIS.Common.Excel
{
    public abstract class PrinterBase : DrawBase
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private System.Drawing.Printing.PrintDocument printDocument_0 = PrinterSingleton.PrintDocument;
        private PrinterMargins printerMargins_0 = PrinterSingleton.PrinterMargins;

        public PrinterBase()
        {
            this.method_0();
        }

        public void CalculatePageInfo()
        {
            this.PrinterMargins = new PrinterMargins(this.PrintDocument);
            this.method_0();
        }

        private void method_0()
        {
            this.int_2 = this.PrinterMargins.Left;
            this.int_4 = this.PrinterMargins.Top;
            this.int_3 = this.PrinterMargins.Right;
            this.int_5 = this.PrinterMargins.Bottom;
            this.int_0 = (this.PrinterMargins.Width + this.int_2) + this.int_3;
            this.int_1 = (this.PrinterMargins.Height + this.int_4) + this.int_5;
        }

        private void method_1()
        {
        }

        public int BottomMargin
        {
            get
            {
                return this.int_5;
            }
        }

        public int LeftMargin
        {
            get
            {
                return this.int_2;
            }
        }

        public int PageHeight
        {
            get
            {
                return this.int_1;
            }
        }

        public int PageWidth
        {
            get
            {
                return this.int_0;
            }
        }

        public System.Drawing.Printing.PrintDocument PrintDocument
        {
            get
            {
                return this.printDocument_0;
            }
            set
            {
                if (value != null)
                {
                    this.printDocument_0 = value;
                }
            }
        }

        public PrinterMargins PrinterMargins
        {
            get
            {
                return this.printerMargins_0;
            }
            set
            {
                if (value != null)
                {
                    this.printerMargins_0 = value;
                    this.method_0();
                }
            }
        }

        public int RightMargin
        {
            get
            {
                return this.int_3;
            }
        }

        public int TopMargin
        {
            get
            {
                return this.int_4;
            }
        }
    }
}

