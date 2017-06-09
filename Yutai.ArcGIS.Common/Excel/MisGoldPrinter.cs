using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class MisGoldPrinter : System.IDisposable
    {
        private const int CON_SPACE_TITLE_CAPTION = 5;

        private const int CON_SPACE_CAPTION_TOP = 20;

        private const int CON_SPACE_HEADER_BODY = 5;

        private const int CON_SPACE_BODY_FOOTER = 5;

        private int int_0;

        private int int_1;

        private int int_2;

        private float float_0 = 1f;

        private int int_3;

        private int int_4;

        private Graphics graphics_0;

        private Printer printer_0;

        private PrintDocument printDocument_0;

        private PrinterMargins printerMargins_0;

        private Sewing sewing_0;

        private bool bool_0 = true;

        public Color BackColor = Color.White;

        private int int_5 = -1;

        private bool bool_1 = false;

        private string string_0 = "";

        private bool bool_2 = false;

        private bool bool_3 = false;

        private GridBorderFlag gridBorderFlag_0 = GridBorderFlag.Double;

        private Title title_0;

        private Caption caption_0;

        private Top top_0;

        private Header header_0;

        private MultiHeader multiHeader_0;

        private Body body_0;

        protected Footer _footer;

        private Bottom bottom_0;

        public Sewing Sewing
        {
            get
            {
                return this.sewing_0;
            }
            set
            {
                if (value != null)
                {
                    this.sewing_0 = value;
                }
                else
                {
                    this.sewing_0.Margin = 0;
                }
            }
        }

        public string DocumentName
        {
            get
            {
                return this.printDocument_0.DocumentName;
            }
            set
            {
                this.printDocument_0.DocumentName = value;
            }
        }

        public int RowsPerPage
        {
            get
            {
                return this.int_5;
            }
            set
            {
                this.int_5 = value;
            }
        }

        public bool IsSubTotalPerPage
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

        public string SubTotalColsList
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

        public bool IsSewingLine
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool IsPrinterMargins
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        public GridBorderFlag GridBorder
        {
            get
            {
                return this.gridBorderFlag_0;
            }
            set
            {
                this.gridBorderFlag_0 = value;
            }
        }

        public object Title
        {
            get
            {
                return this.title_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String")
                    {
                        if (this.title_0 == null)
                        {
                            this.title_0 = new Title();
                        }
                        this.title_0.Text = (string)value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Title")
                    {
                        this.title_0 = (Title)value;
                    }
                }
            }
        }

        public object Caption
        {
            get
            {
                return this.caption_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String")
                    {
                        if (this.caption_0 == null)
                        {
                            this.caption_0 = new Caption();
                        }
                        this.caption_0.Text = (string)value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Caption")
                    {
                        this.caption_0 = (Caption)value;
                    }
                }
            }
        }

        public object Top
        {
            get
            {
                return this.top_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.String[]")
                    {
                        if (this.top_0 == null)
                        {
                            this.top_0 = new Top();
                        }
                        this.top_0.DataSource = value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Top")
                    {
                        this.top_0 = (Top)value;
                    }
                }
            }
        }

        public object Bottom
        {
            get
            {
                return this.bottom_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.String[]")
                    {
                        if (this.bottom_0 == null)
                        {
                            this.bottom_0 = new Bottom();
                        }
                        this.bottom_0.DataSource = (string)value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Bottom")
                    {
                        this.bottom_0 = (Bottom)value;
                    }
                }
            }
        }

        public object Header
        {
            get
            {
                return this.header_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
                    {
                        if (this.header_0 == null)
                        {
                            this.header_0 = new Header();
                        }
                        this.header_0.DataSource = value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Header")
                    {
                        this.header_0 = (Header)value;
                    }
                }
            }
        }

        public object Footer
        {
            get
            {
                return this._footer;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
                    {
                        if (this._footer == null)
                        {
                            this._footer = new Footer();
                        }
                        this._footer.DataSource = value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Footer")
                    {
                        this._footer = (Footer)value;
                    }
                }
            }
        }

        public object MultiHeader
        {
            get
            {
                return this.multiHeader_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
                    {
                        if (this.multiHeader_0 == null)
                        {
                            this.multiHeader_0 = new MultiHeader();
                        }
                        this.multiHeader_0.DataSource = value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.MultiHeader")
                    {
                        this.multiHeader_0 = (MultiHeader)value;
                    }
                }
            }
        }

        public object Body
        {
            get
            {
                return this.body_0;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
                    {
                        if (this.body_0 == null)
                        {
                            this.body_0 = new Body();
                        }
                        this.body_0.DataSource = value;
                    }
                    else if (value.GetType().ToString() == "JLK.Utility.Excel.Body")
                    {
                        this.body_0 = (Body)value;
                    }
                }
            }
        }

        public object DataSource
        {
            get
            {
                return this.body_0.DataSource;
            }
            set
            {
                this.body_0.DataSource = value;
            }
        }

        public MisGoldPrinter() : this(false)
        {
        }

        public MisGoldPrinter(bool bool_4)
        {
            PrinterSingleton.Reset();
            this.int_3 = 1;
            this.int_4 = 0;
            this.printDocument_0 = PrinterSingleton.PrintDocument;
            this.printDocument_0.DefaultPageSettings.Landscape = bool_4;
            this.printerMargins_0 = PrinterSingleton.PrinterMargins;
            this.printDocument_0.DocumentName = "JLKReportExcel报表打印";
            this.sewing_0 = new Sewing(30, SewingDirectionFlag.Left);
            this.printer_0 = new Printer();
            this.body_0 = new Body();
        }

        public virtual void Dispose()
        {
            try
            {
                this.graphics_0.Dispose();
                this.printDocument_0.Dispose();
            }
            catch
            {
            }
        }

        public PageSettings PageSetup()
        {
            PrinterPageSetting printerPageSetting = new PrinterPageSetting(this.printDocument_0);
            printerPageSetting.PrintPage += new PrintPageDelegate(this.method_0);
            PageSettings defaultPageSettings = this.printDocument_0.DefaultPageSettings;
            PageSettings pageSettings = printerPageSetting.ShowPageSetupDialog();
            if (defaultPageSettings != pageSettings)
            {
                PrinterSingleton.PrintDocument = this.printDocument_0;
                this.printerMargins_0 = new PrinterMargins(this.printDocument_0);
                PrinterSingleton.PrinterMargins = this.printerMargins_0;
            }
            return pageSettings;
        }

        public PrinterSettings Print()
        {
            this.int_3 = 1;
            this.int_4 = 0;
            PrinterPageSetting printerPageSetting = new PrinterPageSetting(this.printDocument_0);
            printerPageSetting.PrintPage += new PrintPageDelegate(this.method_0);
            return printerPageSetting.ShowPrintSetupDialog();
        }

        public void Preview()
        {
            this.int_3 = 1;
            this.int_4 = 0;
            PrinterPageSetting printerPageSetting = new PrinterPageSetting(this.printDocument_0);
            printerPageSetting.PrintPage += new PrintPageDelegate(this.method_0);
            printerPageSetting.ImportExcelValue = new ImportExcelDelegate(this.ImportExcelMethodHandler);
            printerPageSetting.ShowPrintPreviewDialog();
        }

        public void ImportExcelMethodHandler(object object_0, ImportExcelArgs importExcelArgs_0)
        {
            ExcelAccess excelAccess = new ExcelAccess();
            excelAccess.Open();
            excelAccess.MergeCells(1, 1, 1, this.body_0.Cols);
            excelAccess.SetFont(1, 1, 1, this.body_0.Cols, this.title_0.Font);
            excelAccess.SetCellText(1, 1, 1, this.body_0.Cols, this.title_0.Text);
            excelAccess.SetCellText((DataTable)this.DataSource, 3, 1, true);
            System.Windows.Forms.FileDialog fileDialog = new System.Windows.Forms.SaveFileDialog();
            fileDialog.AddExtension = true;
            fileDialog.DefaultExt = ".xls";
            fileDialog.Title = "保存到Excel文件";
            fileDialog.Filter = "Microsoft Office Excel 工作簿(*.xls)|*.xls|模板(*.xlt)|*.xlt";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && excelAccess.SaveAs(fileDialog.FileName, true))
            {
                System.Windows.Forms.MessageBox.Show("数据成功保存到Excel文件！", "JLK.Utility.Excel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            }
            fileDialog.Dispose();
            excelAccess.Close();
        }

        private void method_0(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            this.graphics_0 = graphics;
            try
            {
                if (this.method_2(graphics))
                {
                    e.HasMorePages = true;
                    this.int_3++;
                }
                else
                {
                    e.HasMorePages = false;
                    this.int_3 = 1;
                    this.int_4 = 0;
                }
            }
            catch (System.Exception)
            {
            }
        }

        private void method_1(Printer printer_1)
        {
            if (printer_1 != null)
            {
                printer_1.Graphics = this.graphics_0;
                printer_1.Rectangle = new Rectangle(this.int_0, this.int_1, this.int_2, printer_1.Height);
                if (this.bool_0)
                {
                    printer_1.Pen = Pens.Black;
                    printer_1.Brush = Brushes.Black;
                }
                printer_1.Draw();
                this.int_1 += printer_1.Rectangle.Height;
            }
        }

        private bool method_2(Graphics graphics_1)
        {
            bool flag = false;
            if (this.body_0.Rows < 0)
            {
                throw new System.Exception("打印主要网格不能为空，请用Body设置！");
            }
            this.printer_0.Graphics = graphics_1;
            this.printer_0.PrintDocument = this.printDocument_0;
            this.printer_0.Sewing = this.Sewing;
            this.printer_0.PrinterMargins = this.printerMargins_0;
            this.int_1 = this.printer_0.PrinterMargins.Top;
            this.int_0 = this.printer_0.PrinterMargins.Left;
            this.int_2 = this.printer_0.PrinterMargins.Width;
            this.method_3(this.printer_0);
            this.method_4(this.printer_0);
            if (this.title_0 != null && (this.int_3 == 1 || this.title_0.IsDrawAllPage))
            {
                this.title_0.PrinterMargins = this.printerMargins_0;
                this.method_1(this.title_0);
            }
            if (this.caption_0 != null && (this.int_3 == 1 || this.caption_0.IsDrawAllPage))
            {
                this.caption_0.MoveY = 0;
                if (this.title_0 != null && (this.int_3 == 1 || this.title_0.IsDrawAllPage))
                {
                    this.caption_0.MoveY = this.title_0.Height + 5;
                }
                this.caption_0.PrinterMargins = this.printerMargins_0;
                this.method_1(this.caption_0);
            }
            if (this.title_0 != null || this.caption_0 != null)
            {
                this.int_1 += 20;
            }
            int num = 0;
            int i;
            if (!this.body_0.IsAverageColsWidth)
            {
                for (i = 0; i < this.body_0.ColsWidth.Length; i++)
                {
                    num += this.body_0.ColsWidth[i];
                }
                if (num > this.printerMargins_0.Width)
                {
                    this.float_0 = (float)(this.printerMargins_0.Width / num);
                }
                else
                {
                    this.int_2 = num;
                    this.int_0 += (this.printerMargins_0.Width - this.int_2) / 2;
                }
            }
            if (this.top_0 != null && (this.int_3 == 1 || this.top_0.IsDrawAllPage))
            {
                this.method_1(this.top_0);
            }
            if (this.header_0 != null && (this.int_3 == 1 || this.header_0.IsDrawAllPage))
            {
                this.method_1(this.header_0);
            }
            if ((this.top_0 != null || this.header_0 != null) && (this.int_3 == 1 || (this.top_0 != null && this.top_0.IsDrawAllPage) || (this.header_0 != null && this.header_0.IsDrawAllPage)))
            {
                this.int_1 += 5;
            }
            if (this.multiHeader_0 != null && (this.int_3 == 1 || this.multiHeader_0.IsDrawAllPage))
            {
                this.method_1(this.multiHeader_0);
            }
            float num2 = (float)(this.printer_0.PrinterMargins.Height - (this.int_1 - this.printer_0.PrinterMargins.Top));
            if (this._footer != null && this._footer.IsDrawAllPage)
            {
                num2 -= (float)this._footer.Height;
            }
            if (this.bottom_0 != null && this.bottom_0.IsDrawAllPage)
            {
                num2 -= (float)this.bottom_0.Height;
            }
            if (num2 < 0f)
            {
                throw new System.Exception("预留给打印主要网格的空间太小，请适当调整！");
            }
            int num3 = (int)(num2 / (float)this.body_0.RowHeight);
            if (this.RowsPerPage > 0 && this.RowsPerPage < num3)
            {
                num3 = this.RowsPerPage;
            }
            if (this.IsSubTotalPerPage)
            {
                num3--;
            }
            Body body;
            if (this.RowsPerPage > 0 && this.RowsPerPage < num3)
            {
                body = new Body(num3, this.body_0.Cols);
            }
            else
            {
                if (num3 > this.body_0.Rows - this.int_4)
                {
                    num3 = this.body_0.Rows - this.int_4;
                }
                body = new Body(num3, this.body_0.Cols);
            }
            string[,] array = new string[num3, this.body_0.Cols];
            i = 0;
            while (i < num3 && this.int_4 < this.body_0.Rows)
            {
                for (int j = 0; j < this.body_0.Cols; j++)
                {
                    array[i, j] = this.body_0.GetText(this.int_4, j);
                }
                this.int_4++;
                i++;
            }
            body.GridText = array;
            body.ColsAlignString = this.body_0.ColsAlignString;
            body.ColsWidth = this.body_0.ColsWidth;
            body.IsAverageColsWidth = this.body_0.IsAverageColsWidth;
            body.Font = this.body_0.Font;
            this.method_1(body);
            if (this.int_4 < this.body_0.Rows)
            {
                flag = true;
            }
            if (this.bool_1 && this.string_0 != "")
            {
                try
                {
                    MultiHeader multiHeader = new MultiHeader(1, this.body_0.Cols);
                    multiHeader.ColsWidth = this.body_0.ColsWidth;
                    multiHeader.Graphics = graphics_1;
                    multiHeader.PrintDocument = this.printDocument_0;
                    multiHeader.Sewing = this.sewing_0;
                    multiHeader.Rectangle = new Rectangle(this.int_0, this.int_1, this.int_2, multiHeader.Height);
                    multiHeader.SetText(0, 0, "本页小计");
                    multiHeader.SetText(0, 1, "本页小计");
                    string[] array2 = this.string_0.Split(new char[]
                    {
                        ';'
                    });
                    double num4 = 0.0;
                    for (i = 0; i < array2.Length; i++)
                    {
                        int num5 = int.Parse(array2[i]);
                        for (int j = 0; j < array.GetLength(0); j++)
                        {
                            num4 += double.Parse(array[j, num5]);
                        }
                        multiHeader.SetText(0, num5, num4.ToString());
                        num4 = 0.0;
                    }
                    multiHeader.Draw();
                    this.int_1 += multiHeader.Height;
                }
                catch
                {
                }
            }
            if ((this._footer != null || this.bottom_0 != null) && (this.int_3 == 1 || (this.top_0 != null && this.top_0.IsDrawAllPage) || (this.header_0 != null && this.header_0.IsDrawAllPage)))
            {
                this.int_1 += 5;
            }
            if (this._footer != null && (!flag || this._footer.IsDrawAllPage) && this._footer.IsDrawAllPage)
            {
                this.method_1(this._footer);
            }
            if (this.bottom_0 != null && (!flag || this.bottom_0.IsDrawAllPage))
            {
                if (this.bottom_0.IsDrawAllPage)
                {
                    this.method_1(this.bottom_0);
                }
                else
                {
                    num2 = (float)(this.printer_0.PrinterMargins.Height - (this.int_1 - this.printer_0.PrinterMargins.Top));
                    if (num2 < (float)this.bottom_0.Height)
                    {
                        flag = true;
                    }
                    else
                    {
                        this.method_1(this.bottom_0);
                    }
                }
            }
            this.method_5(graphics_1, this.multiHeader_0, body);
            body.Dispose();
            body = null;
            return flag;
        }

        private void method_3(Printer printer_1)
        {
            if (this.IsPrinterMargins)
            {
                printer_1.DrawPrinterMargins();
            }
        }

        private void method_4(Printer printer_1)
        {
            if (this.IsSewingLine && this.Sewing.Margin > 0)
            {
                if (this.Sewing.LineLen <= 0)
                {
                    if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
                    {
                        this.Sewing.LineLen = printer_1.PageHeight;
                    }
                    else if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
                    {
                        this.Sewing.LineLen = printer_1.PageWidth;
                    }
                }
                printer_1.Sewing = this.Sewing;
                printer_1.DrawSewing();
            }
        }

        private void method_5(Graphics graphics_1, MultiHeader multiHeader_1, Body body_1)
        {
            int width = body_1.Rectangle.Width;
            int num = body_1.Rectangle.Height;
            int x;
            int y;
            if (multiHeader_1 != null)
            {
                x = multiHeader_1.Rectangle.X;
                y = multiHeader_1.Rectangle.Y;
                num += multiHeader_1.Rectangle.Height;
            }
            else
            {
                x = body_1.Rectangle.X;
                y = body_1.Rectangle.Y;
            }
            if (this.IsSubTotalPerPage)
            {
                MultiHeader multiHeader = new MultiHeader(1, 1);
                num += multiHeader.RowHeight;
            }
            Rectangle rectangle = new Rectangle(x, y, width, num);
            Pen pen = new Pen(Color.Black, 1f);
            DrawRectangle drawRectangle = new DrawRectangle();
            drawRectangle.Graphics = graphics_1;
            drawRectangle.Rectangle = rectangle;
            drawRectangle.Pen = pen;
            switch (this.GridBorder)
            {
                case GridBorderFlag.Single:
                    drawRectangle.Draw();
                    break;
                case GridBorderFlag.SingleBold:
                    drawRectangle.Pen.Width = 2f;
                    drawRectangle.Draw();
                    if (multiHeader_1 != null)
                    {
                        drawRectangle.Rectangle = body_1.Rectangle;
                        drawRectangle.DrawTopLine();
                    }
                    break;
                case GridBorderFlag.Double:
                    drawRectangle.Draw();
                    rectangle = new Rectangle(x - 2, y - 2, width + 4, num + 4);
                    drawRectangle.Rectangle = rectangle;
                    drawRectangle.Draw();
                    break;
                case GridBorderFlag.DoubleBold:
                    drawRectangle.Draw();
                    rectangle = new Rectangle(x - 2, y - 2, width + 4, num + 4);
                    drawRectangle.Rectangle = rectangle;
                    drawRectangle.Pen.Width = 2f;
                    drawRectangle.Draw();
                    break;
            }
        }

        private void method_6()
        {
            if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
            {
                this.printerMargins_0.Left += this.Sewing.Margin;
                this.printerMargins_0.Width -= this.Sewing.Margin;
            }
            else if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
            {
                this.printerMargins_0.Top += this.Sewing.Margin;
                this.printerMargins_0.Height -= this.Sewing.Margin;
            }
        }

        private void method_7(PrintPageEventArgs printPageEventArgs_0)
        {
            Graphics arg_06_0 = printPageEventArgs_0.Graphics;
            System.Console.WriteLine("*****Information about the printer*****");
            System.Console.WriteLine("纸张的大小  ev.PageSettings.PaperSize:" + printPageEventArgs_0.PageSettings.PaperSize);
            System.Console.WriteLine("打印分辨率  ev.PageSettings.PrinterResolution:" + printPageEventArgs_0.PageSettings.PrinterResolution);
            System.Console.WriteLine("旋转的角度  ev.PageSettings.PrinterSettings.LandscapeAngle" + printPageEventArgs_0.PageSettings.PrinterSettings.LandscapeAngle);
            System.Console.WriteLine("");
            System.Console.WriteLine("*****Information about the page*****");
            System.Console.WriteLine("页面的大小  ev.PageSettings.Bounds:" + printPageEventArgs_0.PageSettings.Bounds);
            System.Console.WriteLine("页面(同上)  ev.PageBounds:" + printPageEventArgs_0.PageBounds);
            System.Console.WriteLine("页面的边距    ev.PageSettings.Margins.:" + printPageEventArgs_0.PageSettings.Margins);
            System.Console.WriteLine("页面的边距    ev.MarginBounds:" + printPageEventArgs_0.MarginBounds);
            System.Console.WriteLine("水平分辨率    ev.Graphics.DpiX:" + printPageEventArgs_0.Graphics.DpiX);
            System.Console.WriteLine("垂直分辨率    ev.Graphics.DpiY:" + printPageEventArgs_0.Graphics.DpiY);
            printPageEventArgs_0.Graphics.SetClip(printPageEventArgs_0.PageBounds);
            System.Console.WriteLine("ev.Graphics.VisibleClipBounds:" + printPageEventArgs_0.Graphics.VisibleClipBounds);
            SizeF sizeF = new SizeF(printPageEventArgs_0.Graphics.VisibleClipBounds.Width * printPageEventArgs_0.Graphics.DpiX / 100f, printPageEventArgs_0.Graphics.VisibleClipBounds.Height * printPageEventArgs_0.Graphics.DpiY / 100f);
            System.Console.WriteLine("drawing Surface Size in Pixels" + sizeF);
        }
    }
}
