using System.Configuration;
using System.Drawing.Printing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class PrinterPageSetting
    {
        private IPrinterPageSetting iprinterPageSetting_0;
        private PrintModeFlag printModeFlag_0;

        public event PrintPageDelegate PrintPage
        {
            add { this.iprinterPageSetting_0.PrintPage += new PrintPageDelegate(value.Invoke); }
            remove { this.iprinterPageSetting_0.PrintPage -= new PrintPageDelegate(value.Invoke); }
        }

        public PrinterPageSetting() : this(null)
        {
        }

        public PrinterPageSetting(System.Drawing.Printing.PrintDocument printDocument_0)
        {
            string str = "";
            str = ConfigurationManager.AppSettings["PrintMode"];
            if (str == null)
            {
                str = "Win";
            }
            if (str.ToUpper() == "WIN")
            {
                this.PrintMode = PrintModeFlag.Win;
            }
            else
            {
                this.PrintMode = PrintModeFlag.Web;
            }
            if (printDocument_0 != null)
            {
                this.iprinterPageSetting_0.PrintDocument = printDocument_0;
            }
        }

        public PageSettings ShowPageSetupDialog()
        {
            return this.iprinterPageSetting_0.ShowPageSetupDialog();
        }

        public void ShowPrintPreviewDialog()
        {
            this.iprinterPageSetting_0.ShowPrintPreviewDialog();
        }

        public PrinterSettings ShowPrintSetupDialog()
        {
            return this.iprinterPageSetting_0.ShowPrintSetupDialog();
        }

        public ImportExcelDelegate ImportExcelValue
        {
            get { return this.iprinterPageSetting_0.ImportExcelValue; }
            set { this.iprinterPageSetting_0.ImportExcelValue = value; }
        }

        public System.Drawing.Printing.PrintDocument PrintDocument
        {
            get { return this.iprinterPageSetting_0.PrintDocument; }
            set { this.iprinterPageSetting_0.PrintDocument = value; }
        }

        public PrintModeFlag PrintMode
        {
            get { return this.printModeFlag_0; }
            set
            {
                this.printModeFlag_0 = value;
                if (this.printModeFlag_0 == PrintModeFlag.Win)
                {
                    this.iprinterPageSetting_0 = new WinPrinterPageSetting();
                }
                else if (this.printModeFlag_0 == PrintModeFlag.Web)
                {
                    this.iprinterPageSetting_0 = new WebPrinterPageSetting();
                }
            }
        }

        public PrintPageDelegate PrintPageValue
        {
            get { return this.iprinterPageSetting_0.PrintPageValue; }
            set { this.iprinterPageSetting_0.PrintPageValue = value; }
        }
    }
}