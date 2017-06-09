namespace Yutai.ArcGIS.Common.Excel
{
    public class PrinterSingleton
    {
        private static System.Drawing.Printing.PrintDocument thePrintDocumentInstance;
        private static PrinterMargins thePrinterMarginsInstance;

        static PrinterSingleton()
        {
            old_acctor_mc();
        }

        private PrinterSingleton()
        {
        }

        private static System.Drawing.Printing.PrintDocument GetPrintDocumentInstance()
        {
            if (thePrintDocumentInstance == null)
            {
                thePrintDocumentInstance = new System.Drawing.Printing.PrintDocument();
            }
            return thePrintDocumentInstance;
        }

        private static PrinterMargins GetPrinterMarginsInstance()
        {
            if (thePrinterMarginsInstance == null)
            {
                thePrinterMarginsInstance = new PrinterMargins(GetPrintDocumentInstance());
            }
            return thePrinterMarginsInstance;
        }

        private static void old_acctor_mc()
        {
            thePrintDocumentInstance = null;
            thePrinterMarginsInstance = null;
        }

        public static void Reset()
        {
            if (thePrintDocumentInstance != null)
            {
                thePrintDocumentInstance.Dispose();
            }
            thePrintDocumentInstance = null;
            thePrinterMarginsInstance = null;
        }

        public static System.Drawing.Printing.PrintDocument PrintDocument
        {
            get
            {
                return GetPrintDocumentInstance();
            }
            set
            {
                thePrintDocumentInstance = value;
            }
        }

        public static PrinterMargins PrinterMargins
        {
            get
            {
                return GetPrinterMarginsInstance();
            }
            set
            {
                thePrinterMarginsInstance = value;
            }
        }
    }
}

