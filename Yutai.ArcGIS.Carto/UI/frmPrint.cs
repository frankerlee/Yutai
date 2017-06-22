using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmPrint : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IActiveView iactiveView_0 = null;
        private IPageLayoutControl ipageLayoutControl_0 = null;
        private IPrinter iprinter_0 = null;

        public frmPrint(IActiveView iactiveView_1)
        {
            this.InitializeComponent();
            this.iactiveView_0 = iactiveView_1;
        }

        private void btnprinterAttribute_Click(object sender, EventArgs e)
        {
            frmPageAndPrinterSetup setup = new frmPageAndPrinterSetup();
            if (this.iactiveView_0 is IPageLayout)
            {
                setup.setPageLayout(this.iactiveView_0 as IPageLayout);
            }
            else
            {
                setup.setPrinter(this.iprinter_0);
            }
            if (setup.ShowDialog() == DialogResult.OK)
            {
                this.method_2();
                this.method_3();
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            this.PrintActiveViewParameterized((long) this.trackBar1.Value);
            base.DialogResult = DialogResult.OK;
        }

        private void comboBox_printer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IPrinter printer;
            if (this.comboBox_printer.SelectedIndex == 0)
            {
                if (!(this.iprinter_0 is IEmfPrinter))
                {
                    printer = new EmfPrinterClass {
                        Paper = this.iprinter_0.Paper
                    };
                    this.iprinter_0 = printer;
                    if (this.ipageLayoutControl_0 != null)
                    {
                        this.ipageLayoutControl_0.Printer = printer;
                    }
                    this.method_2();
                }
            }
            else if (!(this.iprinter_0 is IPsPrinter))
            {
                printer = new PsPrinterClass {
                    Paper = this.iprinter_0.Paper
                };
                this.iprinter_0 = printer;
                if (this.ipageLayoutControl_0 != null)
                {
                    this.ipageLayoutControl_0.Printer = printer;
                }
                this.method_2();
            }
        }

        [DllImport("GDI32.dll")]
        public static extern int CreateDC(string string_0, string string_1, string string_2, IntPtr intptr_0);
 private void frmPrint_Load(object sender, EventArgs e)
        {
            PrintDocument document;
            IPaper paper;
            if (this.iactiveView_0 is IPageLayout)
            {
                if ((this.iactiveView_0 as IPageLayout2).Printer != null)
                {
                    this.iprinter_0 = (this.iactiveView_0 as IPageLayout2).Printer;
                }
                else
                {
                    this.iprinter_0 = new EmfPrinterClass();
                    document = new PrintDocument();
                    paper = new PaperClass {
                        PrinterName = document.PrinterSettings.PrinterName,
                        Orientation = (this.iactiveView_0 as IPageLayout2).Page.Orientation
                    };
                    this.iprinter_0.Paper = paper;
                }
            }
            else
            {
                this.iprinter_0 = new EmfPrinterClass();
                document = new PrintDocument();
                paper = new PaperClass {
                    PrinterName = document.PrinterSettings.PrinterName
                };
                this.iprinter_0.Paper = paper;
            }
            this.trackBar1.Value = (this.iactiveView_0.ScreenDisplay.DisplayTransformation as IOutputRasterSettings).ResampleRatio;
            this.txtQulity.Text = this.trackBar1.Value.ToString();
            this.method_2();
            this.method_3();
            this.bool_0 = true;
            if (this.iactiveView_0 is IPageLayout)
            {
                this.rdoTitle.Checked = (this.iactiveView_0 as IPageLayout).Page.PageToPrinterMapping == esriPageToPrinterMapping.esriPageMappingTile;
            }
        }

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int int_0, int int_1);
 private double method_0(esriUnits esriUnits_0, double double_0)
        {
            switch (esriUnits_0)
            {
                case esriUnits.esriMillimeters:
                    return (double_0 * 0.1);

                case esriUnits.esriCentimeters:
                    return double_0;

                case esriUnits.esriMeters:
                    return (double_0 * 100.0);

                case esriUnits.esriKilometers:
                    return (double_0 * 100000.0);

                case esriUnits.esriDecimalDegrees:
                    return double_0;

                case esriUnits.esriDecimeters:
                    return (double_0 * 10.0);

                case esriUnits.esriInches:
                    return (double_0 * 2.54);
            }
            return double_0;
        }

        private double method_1(double double_0, esriUnits esriUnits_0)
        {
            switch (esriUnits_0)
            {
                case esriUnits.esriMillimeters:
                    return (double_0 * 10.0);

                case esriUnits.esriCentimeters:
                    return double_0;

                case esriUnits.esriMeters:
                    return (double_0 * 0.01);

                case esriUnits.esriKilometers:
                    return (double_0 * 1E-05);

                case esriUnits.esriDecimalDegrees:
                    return double_0;

                case esriUnits.esriDecimeters:
                    return (double_0 * 0.1);

                case esriUnits.esriInches:
                    return (double_0 / 2.54);
            }
            return double_0;
        }

        private void method_2()
        {
            double num;
            double num2;
            IPrinter printer = this.iprinter_0;
            if (printer.Paper.Orientation == 1)
            {
                this.lblPrinterOrientation.Text = "纸张方向：纵向";
            }
            else
            {
                this.lblPrinterOrientation.Text = "纸张方向：横向";
            }
            this.lblPrinterName.Text = printer.Paper.PrinterName;
            printer.Paper.QueryPaperSize(out num, out num2);
            num = this.method_0(printer.Paper.Units, num);
            num2 = this.method_0(printer.Paper.Units, num2);
            this.lblPrinterSize.Text = "纸张大小：" + num.ToString("###.000") + " \x00d7 " + num2.ToString("###.000") + " ";
            if (this.iprinter_0 is IEmfPrinter)
            {
                this.comboBox_printer.SelectedIndex = 0;
            }
            else
            {
                this.comboBox_printer.SelectedIndex = 1;
            }
        }

        private void method_3()
        {
            this.textBox_copied.Text = this.printDialog_0.PrinterSettings.Copies.ToString();
            if (this.iactiveView_0 is IPageLayout)
            {
                short num2;
                (this.iactiveView_0 as IPageLayout2).Page.PrinterPageCount(this.iprinter_0, Convert.ToDouble(this.txbOverlap.Text), out num2);
                this.lblPageCount.Text = "页数: " + num2.ToString();
                int num3 = Convert.ToInt32(this.txbStartPage.Text);
                int num4 = Convert.ToInt32(this.txbEndPage.Text);
                if ((num3 < 1) || (num3 > num2))
                {
                    this.txbStartPage.Value = 1M;
                }
                if ((num4 < 1) || (num4 > num2))
                {
                    this.txbEndPage.Value = num2;
                }
            }
            else
            {
                this.fraPrint.Enabled = false;
            }
        }

        private void method_4(IActiveView iactiveView_1, long long_0)
        {
            IOutputRasterSettings displayTransformation;
            if (iactiveView_1 is IMap)
            {
                displayTransformation = iactiveView_1.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                displayTransformation.ResampleRatio = (int) long_0;
            }
            else if (iactiveView_1 is IPageLayout)
            {
                IMapFrame frame;
                IActiveView map;
                displayTransformation = iactiveView_1.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                displayTransformation.ResampleRatio = (int) long_0;
                IGraphicsContainer container = iactiveView_1 as IGraphicsContainer;
                container.Reset();
                for (IElement element = container.Next(); element != null; element = container.Next())
                {
                    if (element is IMapFrame)
                    {
                        frame = element as IMapFrame;
                        map = frame.Map as IActiveView;
                        displayTransformation = map.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
                        displayTransformation.ResampleRatio = (int) long_0;
                    }
                }
                frame = null;
                container = null;
                map = null;
            }
            displayTransformation = null;
        }

        private bool method_5(string string_0)
        {
            if (string_0 == null)
            {
                return false;
            }
            if (string_0.Trim().Length == 0)
            {
                return false;
            }
            for (int i = 0; i < string_0.Trim().Length; i++)
            {
                if (!char.IsNumber(string_0, i))
                {
                    return false;
                }
            }
            return !string_0.StartsWith("0");
        }

        public void PrintActiveViewParameterized(long long_0)
        {
            short num6;
            tagRECT grect;
            IActiveView view = this.iactiveView_0;
            IEnvelope deviceBounds = new EnvelopeClass();
            IEnvelope pageBounds = new EnvelopeClass();
            IOutputRasterSettings displayTransformation = view.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
            long resampleRatio = displayTransformation.ResampleRatio;
            this.method_4(view, long_0);
            this.iprinter_0.SpoolFileName = "打印地图";
            int num2 = CreateDC(this.iprinter_0.DriverName, this.iprinter_0.Paper.PrinterName, "", IntPtr.Zero);
            if (this.iactiveView_0 is IPageLayout)
            {
                double overlap = 0.0;
                short num4 = 1;
                short pageCount = 1;
                if (this.rdoTitle.Checked)
                {
                    overlap = double.Parse(this.txbOverlap.Text);
                    (this.iactiveView_0 as IPageLayout).Page.PrinterPageCount(this.iprinter_0, 0.0, out num6);
                    if (this.rdoAll.Checked)
                    {
                        (this.iactiveView_0 as IPageLayout2).Page.PrinterPageCount(this.iprinter_0, Convert.ToDouble(this.txbOverlap.Text), out pageCount);
                    }
                    else
                    {
                        num4 = (short) this.txbStartPage.Value;
                        pageCount = (short) this.txbEndPage.Value;
                    }
                }
                num4 = (short) (num4 - 1);
                int hDC = 0;
                for (short i = num4; i < pageCount; i = (short) (i + 1))
                {
                    (this.iactiveView_0 as IPageLayout).Page.GetDeviceBounds(this.iprinter_0, i, overlap, this.iprinter_0.Resolution, deviceBounds);
                    grect.bottom = ((int) deviceBounds.YMax) - GetDeviceCaps(num2, 113);
                    grect.left = ((int) deviceBounds.XMin) - GetDeviceCaps(num2, 112);
                    grect.right = ((int) deviceBounds.XMax) - GetDeviceCaps(num2, 112);
                    grect.top = ((int) deviceBounds.YMin) - GetDeviceCaps(num2, 113);
                    deviceBounds.PutCoords(0.0, 0.0, (double) (grect.right - grect.left), (double) (grect.bottom - grect.top));
                    (this.iactiveView_0 as IPageLayout).Page.GetPageBounds(this.iprinter_0, i, overlap, pageBounds);
                    hDC = this.iprinter_0.StartPrinting(deviceBounds, 0);
                    this.iactiveView_0.Output(hDC, this.iprinter_0.Resolution, ref grect, pageBounds, null);
                    this.iprinter_0.FinishPrinting();
                }
            }
            else
            {
                num6 = 1;
                pageBounds = null;
                if (this.ipageLayoutControl_0 != null)
                {
                    this.ipageLayoutControl_0.Page.GetDeviceBounds(this.iprinter_0, 0, 0.0, this.iprinter_0.Resolution, deviceBounds);
                    grect.bottom = ((int) deviceBounds.YMax) - GetDeviceCaps(num2, 113);
                    grect.left = ((int) deviceBounds.XMin) - GetDeviceCaps(num2, 112);
                    grect.right = ((int) deviceBounds.XMax) - GetDeviceCaps(num2, 112);
                    grect.top = ((int) deviceBounds.YMin) - GetDeviceCaps(num2, 113);
                }
                else
                {
                    WKSEnvelope envelope3;
                    this.iprinter_0.PrintableBounds.QueryWKSCoords(out envelope3);
                    double a = envelope3.XMin * this.iprinter_0.Resolution;
                    double num10 = envelope3.XMax * this.iprinter_0.Resolution;
                    double num11 = envelope3.YMin * this.iprinter_0.Resolution;
                    double num12 = envelope3.YMax * this.iprinter_0.Resolution;
                    grect.left = (int) Math.Round(a);
                    grect.top = (int) Math.Round(num11);
                    grect.right = (int) Math.Round(num10);
                    grect.bottom = (int) Math.Round(num12);
                }
                this.iactiveView_0.Output(this.iprinter_0.StartPrinting(deviceBounds, 0), this.iprinter_0.Resolution, ref grect, pageBounds, null);
                this.iprinter_0.FinishPrinting();
            }
            this.method_4(view, resampleRatio);
            ReleaseDC(0, num2);
        }

        private void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            this.txbEndPage.Enabled = !this.rdoAll.Checked;
            this.txbStartPage.Enabled = !this.rdoAll.Checked;
        }

        private void rdoSpecify_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoTitle_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = this.rdoTitle.Checked;
            if (this.bool_0 && (this.iactiveView_0 is IPageLayout))
            {
                if (this.rdoTitle.Checked)
                {
                    (this.iactiveView_0 as IPageLayout).Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
                    this.method_3();
                }
                else
                {
                    (this.iactiveView_0 as IPageLayout).Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
                }
            }
        }

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(int int_0, int int_1);
        private void textBox_copied_Leave(object sender, EventArgs e)
        {
            if (!this.method_5(this.textBox_copied.Text))
            {
                this.textBox_copied.Text = this.printDialog_0.PrinterSettings.Copies.ToString();
            }
            else
            {
                short num2 = Convert.ToInt16(this.textBox_copied.Text);
                num2 = (num2 < 1) ? ((short) 1) : num2;
                this.printDialog_0.PrinterSettings.Copies = num2;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.txtQulity.Text = this.trackBar1.Value.ToString();
            }
        }

        private void txbOverlap_Leave(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txbOverlap.Text);
            }
            catch
            {
                this.txbOverlap.Text = "0";
            }
            this.method_3();
        }

        private void txtQulity_Leave(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(this.txtQulity.Text);
                if (num < 1)
                {
                    num = 1;
                    this.txtQulity.Text = "1";
                }
                else if (num > 5)
                {
                    num = 5;
                    this.txtQulity.Text = "5";
                }
                this.bool_0 = false;
                this.trackBar1.Value = num;
                this.bool_0 = true;
            }
            catch
            {
                this.txtQulity.Text = this.trackBar1.Value.ToString();
            }
        }

        private void txtQulity_TextChanged(object sender, EventArgs e)
        {
        }

        public IPageLayoutControl PageLayoutControl
        {
            set
            {
                this.ipageLayoutControl_0 = value;
            }
        }
    }
}

