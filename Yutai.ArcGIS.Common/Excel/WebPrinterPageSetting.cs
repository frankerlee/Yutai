using System.Drawing.Printing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class WebPrinterPageSetting : IPrinterPageSetting
    {
        public event PrintPageDelegate PrintPage;

        public PageSettings ShowPageSetupDialog()
        {
            return null;
        }

        public void ShowPrintPreviewDialog()
        {
        }

        public PrinterSettings ShowPrintSetupDialog()
        {
            return null;
        }

        public ImportExcelDelegate ImportExcelValue
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public System.Drawing.Printing.PrintDocument PrintDocument
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public PrintPageDelegate PrintPageValue
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}

