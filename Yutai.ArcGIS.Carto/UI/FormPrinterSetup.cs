using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class FormPrinterSetup : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IPage ipage_0 = null;
        private IPrinter iprinter_0 = null;

        public FormPrinterSetup()
        {
            this.InitializeComponent();
            this.method_0();
        }

        private void button_printerAttribute_Click(object sender, EventArgs e)
        {
            this.printDialog_0.ShowDialog();
            if (this.ipageLayoutControl_0.Printer.Paper.PrinterName !=
                this.printDialog_0.PrinterSettings.PrinterName.Trim())
            {
                this.ipageLayoutControl_0.Printer.Paper.PrinterName =
                    this.printDialog_0.PrinterSettings.PrinterName.Trim();
                this.method_4();
            }
            this.method_3();
            this.method_2();
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    double num;
                    double num2;
                    this.ipageLayoutControl_0.Page.FormID = (esriPageFormID) this.cboPageSize.SelectedIndex;
                    this.ipageLayoutControl_0.Page.QuerySize(out num, out num2);
                    num = this.method_7(this.ipageLayoutControl_0.Page.Units, num);
                    num2 = this.method_7(this.ipageLayoutControl_0.Page.Units, num2);
                    this.bool_0 = false;
                    this.txtPageWidth.Text = num.ToString("0.#");
                    this.txtPageHeight.Text = num2.ToString("0.#");
                    this.bool_0 = true;
                }
                this.method_2();
            }
        }

        private void cboPageToPrinterMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Page.PageToPrinterMapping =
                    (esriPageToPrinterMapping) this.cboPageToPrinterMapping.SelectedIndex;
                this.method_2();
            }
        }

        private void cboPaperTrays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Printer.Paper.TrayID =
                    (short) (this.cboPaperTrays.SelectedItem as Class4).TrayID;
            }
        }

        private void cboPrinterPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Printer.Paper.FormID =
                    (short) (this.cboPrinterPageSize.SelectedItem as Class3).FormID;
                if (this.checkBox_usePrinterPageSize.Checked)
                {
                    double num;
                    double num2;
                    this.ipageLayoutControl_0.Printer.Paper.QueryPaperSize(out num, out num2);
                    num = this.method_7(this.ipageLayoutControl_0.Printer.Units, num);
                    num2 = this.method_7(this.ipageLayoutControl_0.Printer.Units, num2);
                    this.bool_0 = false;
                    this.txtPageWidth.Text = num.ToString("0.#");
                    this.txtPageHeight.Text = num2.ToString("0.#");
                    this.bool_0 = true;
                    num = this.method_8(num, this.ipageLayoutControl_0.Page.Units);
                    num2 = this.method_8(num2, this.ipageLayoutControl_0.Page.Units);
                    this.ipageLayoutControl_0.Page.PutCustomSize(num, num2);
                    this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                }
                this.method_2();
                this.method_3();
            }
        }

        private void checkBox_usePrinterPageSize_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox_usePrinterPageSize_Click(object sender, EventArgs e)
        {
            double num;
            double num2;
            if (this.checkBox_usePrinterPageSize.Checked)
            {
                this.lblInfo.Text = "使用与打印机纸张相同的页面设置";
                this.cboPageSize.Visible = false;
                this.txtPageHeight.Enabled = false;
                this.txtPageWidth.Enabled = false;
                this.optLandscape.Enabled = false;
                this.optPortrait.Enabled = false;
                this.ipageLayoutControl_0.Page.Orientation = this.ipageLayoutControl_0.Printer.Paper.Orientation;
                this.ipageLayoutControl_0.Printer.Paper.QueryPaperSize(out num, out num2);
                num = this.method_7(this.ipageLayoutControl_0.Printer.Units, num);
                num2 = this.method_7(this.ipageLayoutControl_0.Printer.Units, num2);
                this.bool_0 = false;
                this.txtPageWidth.Text = num.ToString("0.#");
                this.txtPageHeight.Text = num2.ToString("0.#");
                this.bool_0 = true;
                num = this.method_8(num, this.ipageLayoutControl_0.Page.Units);
                num2 = this.method_8(num2, this.ipageLayoutControl_0.Page.Units);
                this.ipageLayoutControl_0.Page.PutCustomSize(num, num2);
                this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                if (this.ipageLayoutControl_0.Page.Orientation == 1)
                {
                    this.optPortrait.Checked = true;
                    this.optLandscape.Checked = false;
                }
                else
                {
                    this.optPortrait.Checked = false;
                    this.optLandscape.Checked = true;
                }
            }
            else
            {
                this.lblInfo.Text = "标准尺寸:";
                this.txtPageHeight.Enabled = true;
                this.txtPageWidth.Enabled = true;
                this.optLandscape.Enabled = true;
                this.optPortrait.Enabled = true;
                this.ipageLayoutControl_0.Printer.Paper.QueryPaperSize(out num, out num2);
                num = this.method_7(this.ipageLayoutControl_0.Printer.Paper.Units, num);
                num2 = this.method_7(this.ipageLayoutControl_0.Printer.Paper.Units, num2);
                num = this.method_8(num, this.ipageLayoutControl_0.Page.Units);
                num2 = this.method_8(num2, this.ipageLayoutControl_0.Page.Units);
                this.ipageLayoutControl_0.Page.PutCustomSize(num, num2);
                this.cboPageSize.Visible = true;
                this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormCUSTOM;
                this.cboPageSize.SelectedIndex = (int) this.ipageLayoutControl_0.Page.FormID;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.method_1();
            base.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (this.ipageLayoutControl_0.Printer != null)
            {
                this.cmdCancel.Enabled = false;
                IPrinter printer = this.ipageLayoutControl_0.Printer;
                try
                {
                    esriPageToPrinterMapping pageToPrinterMapping = this.ipageLayoutControl_0.Page.PageToPrinterMapping;
                    short startPage = Convert.ToInt16(this.txbStartPage.Text);
                    short endPage = Convert.ToInt16(this.txbEndPage.Text);
                    double overlap = Convert.ToDouble(this.txbOverlap.Text);
                    this.ipageLayoutControl_0.PrintPageLayout(startPage, endPage, overlap);
                }
                catch (Exception)
                {
                }
                this.cmdCancel.Enabled = true;
                base.Close();
            }
        }

        private void comboBox_printer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.printDialog_0.PrinterSettings.PrinterName = this.comboBox_printer.Text;
                this.ipageLayoutControl_0.Printer.Paper.PrinterName =
                    this.printDialog_0.PrinterSettings.PrinterName.Trim();
                this.method_4();
                this.method_3();
                this.method_2();
            }
        }

        private void FormPrinterSetup_Load(object sender, EventArgs e)
        {
            this.checkBox_usePrinterPageSize.Checked = this.ipageLayoutControl_0.Page.FormID ==
                                                       esriPageFormID.esriPageFormSameAsPrinter;
            this.cboPageSize.Items.Add("Letter");
            this.cboPageSize.Items.Add("Legal");
            this.cboPageSize.Items.Add("Tabloid");
            this.cboPageSize.Items.Add("ANSI C");
            this.cboPageSize.Items.Add("ANSI D");
            this.cboPageSize.Items.Add("ANSI E");
            this.cboPageSize.Items.Add("A5");
            this.cboPageSize.Items.Add("A4");
            this.cboPageSize.Items.Add("A3");
            this.cboPageSize.Items.Add("A2");
            this.cboPageSize.Items.Add("A1");
            this.cboPageSize.Items.Add("A0");
            this.cboPageSize.Items.Add("自定义大小");
            if (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
            {
                this.cboPageSize.SelectedIndex = (int) this.ipageLayoutControl_0.Page.FormID;
                this.lblInfo.Text = "标准尺寸";
                this.cboPageSize.Visible = true;
                this.optLandscape.Checked = this.ipageLayoutControl_0.Page.Orientation == 2;
                this.optPortrait.Checked = this.ipageLayoutControl_0.Page.Orientation == 1;
                this.cboPageSize.SelectedIndex = 0;
            }
            else
            {
                this.lblInfo.Text = "使用与打印机纸张相同的页面设置";
                this.cboPrinterPageSize.Enabled = true;
                this.cboPageSize.Visible = false;
                this.txtPageHeight.Enabled = false;
                this.txtPageWidth.Enabled = false;
                this.optLandscape.Enabled = false;
                this.optPortrait.Enabled = false;
            }
            this.cboPageToPrinterMapping.Items.Add("裁剪地图");
            this.cboPageToPrinterMapping.Items.Add("缩放地图");
            this.cboPageToPrinterMapping.Items.Add("分割地图");
            this.cboPageToPrinterMapping.SelectedIndex = (int) this.ipageLayoutControl_0.Page.PageToPrinterMapping;
            if (this.ipageLayoutControl_0.Printer.Paper.Orientation == 1)
            {
                this.optPaperPortrait.Checked = true;
            }
            else
            {
                this.optLandscape.Checked = true;
            }
            if (this.ipageLayoutControl_0.Page.Orientation == 1)
            {
                this.optPortrait.Checked = true;
            }
            else
            {
                this.optLandscape.Checked = true;
            }
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                this.comboBox_printer.Items.Add(str.Trim());
            }
            this.method_4();
            this.bool_0 = true;
            this.method_3();
            this.method_2();
        }

        private void method_0()
        {
            this.printerSettings_0 = new PrinterSettings();
            this.printDialog_0.PrinterSettings = this.printerSettings_0;
        }

        private void method_1()
        {
            if (this.iprinter_0 != null)
            {
                IPrinter printer = this.ipageLayoutControl_0.Printer;
                printer.Paper.FormID = this.iprinter_0.Paper.FormID;
                printer.Paper.Orientation = this.iprinter_0.Paper.Orientation;
                printer.Paper.PrinterName = this.iprinter_0.Paper.PrinterName;
                printer.Paper.TrayID = this.iprinter_0.Paper.TrayID;
            }
            IPage page = this.ipageLayoutControl_0.Page;
            if (page.FormID == esriPageFormID.esriPageFormCUSTOM)
            {
                double num;
                double num2;
                this.ipage_0.QuerySize(out num, out num2);
                page.PutCustomSize(num, num2);
            }
            else
            {
                page.FormID = this.ipage_0.FormID;
            }
            page.PageToPrinterMapping = this.ipage_0.PageToPrinterMapping;
            page.Orientation = this.ipage_0.Orientation;
        }

        private void method_2()
        {
            this.textBox_copied.Text = this.printDialog_0.PrinterSettings.Copies.ToString();
            short num2 = this.ipageLayoutControl_0.get_PrinterPageCount(Convert.ToDouble(this.txbOverlap.Text));
            this.lblPageCount.Text = "页数： " + num2.ToString();
            int num3 = Convert.ToInt32(this.txbStartPage.Text);
            int num4 = Convert.ToInt32(this.txbEndPage.Text);
            if ((num3 < 1) || (num3 > num2))
            {
                this.txbStartPage.Text = "1";
            }
            if ((num4 < 1) || (num4 > num2))
            {
                this.txbEndPage.Text = num2.ToString();
            }
        }

        private void method_3()
        {
            if (this.ipageLayoutControl_0.Printer != null)
            {
                double num;
                double num2;
                IPrinter printer = this.ipageLayoutControl_0.Printer;
                if (printer.Paper.Orientation == 1)
                {
                    this.lblPrinterOrientation.Text = "纸张方向：纵向";
                }
                else
                {
                    this.lblPrinterOrientation.Text = "纸张方向：横向";
                }
                this.comboBox_printer.Text = printer.Paper.PrinterName;
                printer.Paper.QueryPaperSize(out num, out num2);
                num = this.method_7(printer.Paper.Units, num);
                num2 = this.method_7(printer.Paper.Units, num2);
                this.lblPrinterSize.Text = "纸张大小：" + num.ToString("###.000") + " \x00d7 " + num2.ToString("###.000") +
                                           " ";
            }
        }

        private void method_4()
        {
            string str;
            int num;
            double num3;
            double num4;
            this.cboPrinterPageSize.Items.Clear();
            IEnumNamedID forms = this.ipageLayoutControl_0.Printer.Paper.Forms;
            forms.Reset();
            for (num = forms.Next(out str); str != null; num = forms.Next(out str))
            {
                this.cboPrinterPageSize.Items.Add(new Class3(str, num));
            }
            this.cboPrinterPageSize.Text = this.ipageLayoutControl_0.Printer.Paper.FormName;
            this.cboPaperTrays.Items.Clear();
            forms = this.ipageLayoutControl_0.Printer.Paper.Trays;
            forms.Reset();
            num = forms.Next(out str);
            int num2 = 0;
            while (str != null)
            {
                this.cboPaperTrays.Items.Add(new Class4(str, num));
                if (num == this.ipageLayoutControl_0.Printer.Paper.TrayID)
                {
                    num2 = this.cboPaperTrays.Items.Count - 1;
                }
                num = forms.Next(out str);
            }
            if (this.cboPaperTrays.Items.Count > 0)
            {
                this.cboPaperTrays.SelectedIndex = num2;
            }
            else
            {
                this.cboPaperTrays.Enabled = false;
            }
            if (this.checkBox_usePrinterPageSize.Checked)
            {
                this.ipageLayoutControl_0.Printer.Paper.QueryPaperSize(out num3, out num4);
                num3 = this.method_7(this.ipageLayoutControl_0.Printer.Units, num3);
                num4 = this.method_7(this.ipageLayoutControl_0.Printer.Units, num4);
                this.ipageLayoutControl_0.Page.PutCustomSize(num3, num4);
                this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormSameAsPrinter;
            }
            else
            {
                this.ipageLayoutControl_0.Page.QuerySize(out num3, out num4);
                num3 = this.method_7(this.ipageLayoutControl_0.Page.Units, num3);
                num4 = this.method_7(this.ipageLayoutControl_0.Page.Units, num4);
            }
            this.bool_0 = false;
            this.txtPageWidth.Text = num3.ToString("0.##");
            this.txtPageHeight.Text = num4.ToString("0.##");
            this.bool_0 = true;
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
            int num = 0;
            for (int i = 0; i < string_0.Trim().Length; i++)
            {
                if (string_0.Substring(i, 1) == ".")
                {
                    num++;
                    if (num > 1)
                    {
                        return false;
                    }
                }
                else if (!char.IsNumber(string_0, i))
                {
                    return false;
                }
            }
            if (string_0.StartsWith("0") && (string_0.IndexOf('.') != 1))
            {
                return false;
            }
            return true;
        }

        private bool method_6(string string_0)
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

        private double method_7(esriUnits esriUnits_0, double double_0)
        {
            switch (esriUnits_0)
            {
                case esriUnits.esriMillimeters:
                    return (double_0*0.1);

                case esriUnits.esriCentimeters:
                    return double_0;

                case esriUnits.esriMeters:
                    return (double_0*100.0);

                case esriUnits.esriKilometers:
                    return (double_0*100000.0);

                case esriUnits.esriDecimalDegrees:
                    return double_0;

                case esriUnits.esriDecimeters:
                    return (double_0*10.0);

                case esriUnits.esriInches:
                    return (double_0*2.54);
            }
            return double_0;
        }

        private double method_8(double double_0, esriUnits esriUnits_0)
        {
            switch (esriUnits_0)
            {
                case esriUnits.esriMillimeters:
                    return (double_0*10.0);

                case esriUnits.esriCentimeters:
                    return double_0;

                case esriUnits.esriMeters:
                    return (double_0*0.01);

                case esriUnits.esriKilometers:
                    return (double_0*1E-05);

                case esriUnits.esriDecimalDegrees:
                    return double_0;

                case esriUnits.esriDecimeters:
                    return (double_0*0.1);

                case esriUnits.esriInches:
                    return (double_0/2.54);
            }
            return double_0;
        }

        private void optLandscape_Click(object sender, EventArgs e)
        {
            if (this.optLandscape.Checked &&
                (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
            {
                this.ipageLayoutControl_0.Page.Orientation = 2;
            }
        }

        private void optPaperLandscape_Click(object sender, EventArgs e)
        {
            if (this.optPaperLandscape.Checked)
            {
                this.ipageLayoutControl_0.Printer.Paper.Orientation = 2;
                if (this.ipageLayoutControl_0.Page.FormID == esriPageFormID.esriPageFormSameAsPrinter)
                {
                    this.ipageLayoutControl_0.Page.Orientation = 2;
                    this.optLandscape.Checked = true;
                    this.optPortrait.Checked = false;
                }
                this.method_3();
                this.method_2();
            }
        }

        private void optPaperPortrait_Click(object sender, EventArgs e)
        {
            if (this.optPaperPortrait.Checked)
            {
                this.ipageLayoutControl_0.Printer.Paper.Orientation = 1;
                if (this.ipageLayoutControl_0.Page.FormID == esriPageFormID.esriPageFormSameAsPrinter)
                {
                    this.ipageLayoutControl_0.Page.Orientation = 1;
                    this.optLandscape.Checked = false;
                    this.optPortrait.Checked = true;
                }
                this.method_3();
                this.method_2();
            }
        }

        private void optPortrait_Click(object sender, EventArgs e)
        {
            if (this.optPortrait.Checked &&
                (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
            {
                this.ipageLayoutControl_0.Page.Orientation = 1;
            }
        }

        public void setPageLayout(ref IPageLayoutControl ipageLayoutControl_1)
        {
            this.ipageLayoutControl_0 = ipageLayoutControl_1;
            try
            {
                this.iprinter_0 = (this.ipageLayoutControl_0.Printer as IClone).Clone() as IPrinter;
                this.ipage_0 = (this.ipageLayoutControl_0.Page as IClone).Clone() as IPage;
            }
            catch (Exception)
            {
            }
        }

        public void setPrinter(IPrinter iprinter_1)
        {
            this.ipageLayoutControl_0 = null;
            this.iprinter_0 = iprinter_1;
        }

        private void textBox_copied_Leave(object sender, EventArgs e)
        {
            if (!this.method_6(this.textBox_copied.Text))
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

        private void txbEndPage_Leave(object sender, EventArgs e)
        {
            if (!this.method_6(this.txbEndPage.Text))
            {
                this.txbEndPage.Text =
                    this.ipageLayoutControl_0.get_PrinterPageCount(Convert.ToDouble(this.txbOverlap.Text)).ToString();
            }
        }

        private void txbOverlap_Leave(object sender, EventArgs e)
        {
            if (!this.method_5(this.txbOverlap.Text))
            {
                this.txbOverlap.Text = "0";
            }
            this.method_2();
        }

        private void txbStartPage_Leave(object sender, EventArgs e)
        {
            if (!this.method_6(this.txbStartPage.Text))
            {
                this.txbStartPage.Text = "1";
            }
        }

        private void txtPageHeight_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num;
                    double num2;
                    this.ipageLayoutControl_0.Page.QuerySize(out num, out num2);
                    num2 = Convert.ToDouble(this.txtPageHeight.Text);
                    num2 = this.method_8(num2, this.ipageLayoutControl_0.Page.Units);
                    this.ipageLayoutControl_0.Page.PutCustomSize(num, num2);
                    this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormCUSTOM;
                    this.method_2();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void txtPageWidth_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num;
                    double num2;
                    this.ipageLayoutControl_0.Page.QuerySize(out num, out num2);
                    num = Convert.ToDouble(this.txtPageWidth.Text);
                    num = this.method_8(num, this.ipageLayoutControl_0.Page.Units);
                    this.ipageLayoutControl_0.Page.PutCustomSize(num, num2);
                    this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormCUSTOM;
                    this.method_2();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private partial class Class2
        {
            private PaperSize paperSize_0;

            public Class2(System.Drawing.Printing.PaperSize paperSize_1)
            {
                this.paperSize_0 = paperSize_1;
            }

            public override string ToString()
            {
                return this.paperSize_0.PaperName;
            }

            public System.Drawing.Printing.PaperSize PaperSize
            {
                get { return this.paperSize_0; }
                set { }
            }
        }

        private partial class Class3
        {
            private string string_0;
            private int int_0;

            public Class3(string string_1, int int_1)
            {
                this.string_0 = string_1;
                this.int_0 = int_1;
            }

            public override string ToString()
            {
                return this.string_0;
            }

            public int FormID
            {
                get { return this.int_0; }
            }

            public string FormName
            {
                get { return this.string_0; }
            }
        }

        private partial class Class4
        {
            private string string_0;
            private int int_0;

            public Class4(string string_1, int int_1)
            {
                this.string_0 = string_1;
                this.int_0 = int_1;
            }

            public override string ToString()
            {
                return this.string_0;
            }

            public int TrayID
            {
                get { return this.int_0; }
            }

            public string TrayName
            {
                get { return this.string_0; }
            }
        }
    }
}