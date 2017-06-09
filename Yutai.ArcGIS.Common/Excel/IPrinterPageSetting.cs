using System.Drawing.Printing;

namespace Yutai.ArcGIS.Common.Excel
{
    public interface IPrinterPageSetting
    {
        event PrintPageDelegate PrintPage;

        PageSettings ShowPageSetupDialog();
        void ShowPrintPreviewDialog();
        PrinterSettings ShowPrintSetupDialog();

        ImportExcelDelegate ImportExcelValue { get; set; }

        System.Drawing.Printing.PrintDocument PrintDocument { get; set; }

        PrintPageDelegate PrintPageValue { get; set; }
    }
}

