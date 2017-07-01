using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class FormPageSetup : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;

        public FormPageSetup()
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
                this.method_3();
            }
            this.method_2();
            this.method_1();
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    this.ipageLayoutControl_0.Page.FormID = (esriPageFormID) this.cboPageSize.SelectedIndex;
                }
                this.method_1();
            }
        }

        private void cboPageToPrinterMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Page.PageToPrinterMapping =
                    (esriPageToPrinterMapping) this.cboPageToPrinterMapping.SelectedIndex;
                this.method_1();
            }
        }

        private void cboPrinterPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                PaperSize paperSize = (this.cboPrinterPageSize.SelectedItem as Class0).PaperSize;
                this.printDialog_0.PrinterSettings.DefaultPageSettings.PaperSize =
                    (this.cboPrinterPageSize.SelectedItem as Class0).PaperSize;
            }
        }

        private void checkBox_usePrinterPageSize_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox_usePrinterPageSize_Click(object sender, EventArgs e)
        {
            if (this.checkBox_usePrinterPageSize.Checked)
            {
                this.cboPrinterPageSize.Enabled = true;
                this.cboPageSize.Enabled = false;
                this.ipageLayoutControl_0.Page.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                this.method_3();
            }
            else
            {
                this.cboPageSize.Enabled = true;
                this.cboPrinterPageSize.Enabled = false;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void comboBox_printer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Printer.Paper.PrinterName = this.comboBox_printer.Text;
                this.printDialog_0.PrinterSettings.PrinterName = this.comboBox_printer.Text;
                this.printDialog_0.PrinterSettings.DefaultPageSettings.PaperSize =
                    this.printDialog_0.PrinterSettings.PaperSizes[0];
                this.method_3();
            }
        }

        private void FormPageSetup_Load(object sender, EventArgs e)
        {
            this.cboPageSize.Items.Add("Letter - 8.5in x 11in.");
            this.cboPageSize.Items.Add("Legal - 8.5in x 14in.");
            this.cboPageSize.Items.Add("Tabloid - 11in x 17in.");
            this.cboPageSize.Items.Add("C - 17in x 22in.");
            this.cboPageSize.Items.Add("D - 22in x 34in.");
            this.cboPageSize.Items.Add("E - 34in x 44in.");
            this.cboPageSize.Items.Add("A5 - 148mm x 210mm.");
            this.cboPageSize.Items.Add("A4 - 210mm x 297mm.");
            this.cboPageSize.Items.Add("A3 - 297mm x 420mm.");
            this.cboPageSize.Items.Add("A2 - 420mm x 594mm.");
            this.cboPageSize.Items.Add("A1 - 594mm x 841mm.");
            this.cboPageSize.Items.Add("A0 - 841mm x 1189mm.");
            this.cboPageSize.Items.Add("自定义大小");
            if (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
            {
                this.cboPageSize.SelectedIndex = (int) this.ipageLayoutControl_0.Page.FormID;
            }
            this.cboPageToPrinterMapping.Items.Add("裁剪地图");
            this.cboPageToPrinterMapping.Items.Add("缩放地图");
            this.cboPageToPrinterMapping.Items.Add("分割地图");
            this.cboPageToPrinterMapping.SelectedIndex = (int) this.ipageLayoutControl_0.Page.PageToPrinterMapping;
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
            this.printDialog_0.PrinterSettings.PrinterName = this.ipageLayoutControl_0.Printer.Paper.PrinterName;
            this.comboBox_printer.Text = this.printDialog_0.PrinterSettings.PrinterName;
            this.method_3();
            this.checkBox_usePrinterPageSize.Checked = this.ipageLayoutControl_0.Page.FormID ==
                                                       esriPageFormID.esriPageFormSameAsPrinter;
            this.method_2();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.printerSettings_0 = new PrinterSettings();
            this.printDialog_0.PrinterSettings = this.printerSettings_0;
        }

        private void method_1()
        {
            this.textBox_copied.Text = this.printDialog_0.PrinterSettings.Copies.ToString();
            short num2 = this.ipageLayoutControl_0.get_PrinterPageCount(Convert.ToDouble(this.txbOverlap.Text));
            this.lblPageCount.Text = "页数： " + num2.ToString();
            int num3 = Convert.ToInt32(this.txbStartPage.Text);
            int num4 = Convert.ToInt32(this.txbEndPage.Text);
            if ((num3 < 1) | (num3 > num2))
            {
                this.txbStartPage.Text = "1";
            }
            if ((num4 < 1) | (num4 > num2))
            {
                this.txbEndPage.Text = num2.ToString();
            }
        }

        private void method_2()
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
                printer.Paper.QueryPaperSize(out num, out num2);
                this.lblPrinterSize.Text = "纸张大小：" + num.ToString("###.000") + " by " + num2.ToString("###.000") +
                                           " Inches";
            }
        }

        private void method_3()
        {
            this.cboPrinterPageSize.Items.Clear();
            foreach (PaperSize size in this.printDialog_0.PrinterSettings.PaperSizes)
            {
                this.cboPrinterPageSize.Items.Add(new Class0(size));
            }
            PaperSize paperSize = this.printDialog_0.PrinterSettings.DefaultPageSettings.PaperSize;
            this.cboPrinterPageSize.Text = paperSize.PaperName;
        }

        private bool method_4(string string_0)
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

        private void optLandscape_Click(object sender, EventArgs e)
        {
            if (this.optLandscape.Checked &&
                (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
            {
                this.ipageLayoutControl_0.Page.Orientation = 2;
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
        }

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

        private void txbEndPage_Leave(object sender, EventArgs e)
        {
            if (!this.method_5(this.txbEndPage.Text))
            {
                this.txbEndPage.Text =
                    this.ipageLayoutControl_0.get_PrinterPageCount(Convert.ToDouble(this.txbOverlap.Text)).ToString();
            }
        }

        private void txbOverlap_Leave(object sender, EventArgs e)
        {
            if (!this.method_4(this.txbOverlap.Text))
            {
                this.txbOverlap.Text = "0";
            }
            this.method_1();
        }

        private void txbStartPage_Leave(object sender, EventArgs e)
        {
            if (!this.method_5(this.txbStartPage.Text))
            {
                this.txbStartPage.Text = "1";
            }
        }

        private partial class Class0
        {
            private PaperSize paperSize_0;

            public Class0(System.Drawing.Printing.PaperSize paperSize_1)
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
    }
}