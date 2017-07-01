using System;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.Excel
{
    public class WinPrinterPageSetting : IPrinterPageSetting
    {
        private ImportExcelDelegate importExcelDelegate_0;
        private System.Drawing.Printing.PrintDocument printDocument_0;
        private PrintPageDelegate printPageDelegate_0;

        public event PrintPageDelegate PrintPage
        {
            add
            {
                this.printDocument_0.PrintPage += new PrintPageEventHandler(value.Invoke);
                this.printPageDelegate_0 = value;
            }
            remove
            {
                this.printDocument_0.PrintPage -= new PrintPageEventHandler(value.Invoke);
                this.printPageDelegate_0 = null;
            }
        }

        public WinPrinterPageSetting() : this(null)
        {
        }

        public WinPrinterPageSetting(System.Drawing.Printing.PrintDocument printDocument_1)
        {
            if (printDocument_1 != null)
            {
                this.printDocument_0 = printDocument_1;
            }
            else
            {
                this.printDocument_0 = new System.Drawing.Printing.PrintDocument();
            }
        }

        private void method_0(object sender, ToolBarButtonClickEventArgs e)
        {
            if ((e.Button.ToolTipText == "Import Excel") && (this.importExcelDelegate_0 != null))
            {
                this.importExcelDelegate_0.BeginInvoke(sender, null, null, null);
            }
        }

        protected virtual void ShowInvalidPrinterException(InvalidPrinterException invalidPrinterException_0)
        {
            MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！", "打印", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public PageSettings ShowPageSetupDialog()
        {
            return this.ShowPageSetupDialog(this.printDocument_0);
        }

        protected virtual PageSettings ShowPageSetupDialog(System.Drawing.Printing.PrintDocument printDocument_1)
        {
            this.ThrowPrintDocumentNullException(printDocument_1);
            PageSettings defaultPageSettings = new PageSettings();
            PageSetupDialog dialog = new PageSetupDialog();
            defaultPageSettings = printDocument_1.DefaultPageSettings;
            try
            {
                dialog.Document = printDocument_1;
                Margins margins = printDocument_1.DefaultPageSettings.Margins;
                if (RegionInfo.CurrentRegion.IsMetric)
                {
                    margins = PrinterUnitConvert.Convert(margins, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
                }
                PageSettings settings2 = (PageSettings) printDocument_1.DefaultPageSettings.Clone();
                dialog.PageSettings = settings2;
                dialog.PageSettings.Margins = margins;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    defaultPageSettings = dialog.PageSettings;
                    printDocument_1.DefaultPageSettings = dialog.PageSettings;
                }
            }
            catch (InvalidPrinterException exception)
            {
                this.ShowInvalidPrinterException(exception);
            }
            catch (Exception exception2)
            {
                this.ShowPrinterException(exception2);
            }
            finally
            {
                dialog.Dispose();
                dialog = null;
            }
            return defaultPageSettings;
        }

        protected virtual void ShowPrinterException(Exception exception_0)
        {
            MessageBox.Show(exception_0.Message, "打印", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowPrintPreviewDialog()
        {
            this.ShowPrintPreviewDialog(this.printDocument_0);
        }

        protected virtual void ShowPrintPreviewDialog(System.Drawing.Printing.PrintDocument printDocument_1)
        {
            this.ThrowPrintDocumentNullException(printDocument_1);
            PrintPreviewDialog dialog = new PrintPreviewDialog
            {
                Text = printDocument_1.DocumentName,
                WindowState = FormWindowState.Maximized
            };
            if (this.importExcelDelegate_0 != null)
            {
                ToolBar bar = null;
                if (dialog.Controls[1] is ToolBar)
                {
                    bar = (ToolBar) dialog.Controls[1];
                    ToolBarButton button = new ToolBarButton
                    {
                        ToolTipText = "Import Excel",
                        ImageIndex = 2
                    };
                    bar.ButtonClick += new ToolBarButtonClickEventHandler(this.method_0);
                    bar.Buttons.Add(button);
                }
            }
            try
            {
                dialog.Document = printDocument_1;
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                }
            }
            catch (InvalidPrinterException exception)
            {
                this.ShowInvalidPrinterException(exception);
            }
            catch (Exception exception2)
            {
                this.ShowPrinterException(exception2);
            }
            finally
            {
                dialog.Dispose();
                dialog = null;
            }
        }

        public PrinterSettings ShowPrintSetupDialog()
        {
            return this.ShowPrintSetupDialog(this.printDocument_0);
        }

        protected virtual PrinterSettings ShowPrintSetupDialog(System.Drawing.Printing.PrintDocument printDocument_1)
        {
            this.ThrowPrintDocumentNullException(printDocument_1);
            PrinterSettings printerSettings = new PrinterSettings();
            PrintDialog dialog = new PrintDialog();
            try
            {
                dialog.AllowSomePages = true;
                dialog.Document = printDocument_1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    printerSettings = dialog.PrinterSettings;
                    printDocument_1.Print();
                }
            }
            catch (InvalidPrinterException exception)
            {
                this.ShowInvalidPrinterException(exception);
            }
            catch (Exception exception2)
            {
                this.ShowPrinterException(exception2);
            }
            finally
            {
                dialog.Dispose();
                dialog = null;
            }
            return printerSettings;
        }

        protected virtual void ThrowPrintDocumentNullException(System.Drawing.Printing.PrintDocument printDocument_1)
        {
            if (printDocument_1 == null)
            {
                throw new Exception("关联的打印文档不能为空！");
            }
        }

        public ImportExcelDelegate ImportExcelValue
        {
            get { return this.importExcelDelegate_0; }
            set { this.importExcelDelegate_0 = value; }
        }

        public System.Drawing.Printing.PrintDocument PrintDocument
        {
            get { return this.printDocument_0; }
            set { this.printDocument_0 = value; }
        }

        public PrintPageDelegate PrintPageValue
        {
            get { return this.printPageDelegate_0; }
            set
            {
                this.printPageDelegate_0 = value;
                this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printPageDelegate_0.Invoke);
            }
        }
    }
}