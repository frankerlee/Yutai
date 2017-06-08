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
    public class FormPageSetup : Form
    {
        private bool bool_0 = false;
        private Button button_printerAttribute;
        private ComboBox cboPageSize;
        private ComboBox cboPageToPrinterMapping;
        private ComboBox cboPrinterPageSize;
        private CheckBox checkBox_usePrinterPageSize;
        private Button cmdCancel;
        private Button cmdOK;
        private ComboBox comboBox_printer;
        private Container container_0 = null;
        private GroupBox Frame2;
        private GroupBox fraPrint;
        private GroupBox fraPrinter;
        private IPageLayoutControl ipageLayoutControl_0;
        private Label label_copied;
        private Label Label1;
        private Label Label2;
        private Label label3;
        private Label Label5;
        private Label Label6;
        private Label Label8;
        private Label lblPageCount;
        private Label lblPrinterName;
        private Label lblPrinterOrientation;
        private Label lblPrinterSize;
        private RadioButton optLandscape;
        private RadioButton optPortrait;
        private Panel panel1;
        private PrintDialog printDialog_0;
        private PrinterSettings printerSettings_0;
        private TextBox textBox_copied;
        private TextBox txbEndPage;
        private TextBox txbOverlap;
        private TextBox txbStartPage;

        public FormPageSetup()
        {
            this.InitializeComponent();
            this.method_0();
        }

        private void button_printerAttribute_Click(object sender, EventArgs e)
        {
            this.printDialog_0.ShowDialog();
            if (this.ipageLayoutControl_0.Printer.Paper.PrinterName != this.printDialog_0.PrinterSettings.PrinterName.Trim())
            {
                this.ipageLayoutControl_0.Printer.Paper.PrinterName = this.printDialog_0.PrinterSettings.PrinterName.Trim();
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
                this.ipageLayoutControl_0.Page.PageToPrinterMapping = (esriPageToPrinterMapping) this.cboPageToPrinterMapping.SelectedIndex;
                this.method_1();
            }
        }

        private void cboPrinterPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                PaperSize paperSize = (this.cboPrinterPageSize.SelectedItem as Class0).PaperSize;
                this.printDialog_0.PrinterSettings.DefaultPageSettings.PaperSize = (this.cboPrinterPageSize.SelectedItem as Class0).PaperSize;
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
                this.printDialog_0.PrinterSettings.DefaultPageSettings.PaperSize = this.printDialog_0.PrinterSettings.PaperSizes[0];
                this.method_3();
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
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
            this.checkBox_usePrinterPageSize.Checked = this.ipageLayoutControl_0.Page.FormID == esriPageFormID.esriPageFormSameAsPrinter;
            this.method_2();
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPageSetup));
            this.panel1 = new Panel();
            this.fraPrint = new GroupBox();
            this.txbOverlap = new TextBox();
            this.Label2 = new Label();
            this.textBox_copied = new TextBox();
            this.lblPageCount = new Label();
            this.label_copied = new Label();
            this.txbStartPage = new TextBox();
            this.txbEndPage = new TextBox();
            this.Label5 = new Label();
            this.Label1 = new Label();
            this.fraPrinter = new GroupBox();
            this.checkBox_usePrinterPageSize = new CheckBox();
            this.button_printerAttribute = new Button();
            this.comboBox_printer = new ComboBox();
            this.lblPrinterOrientation = new Label();
            this.lblPrinterName = new Label();
            this.lblPrinterSize = new Label();
            this.Frame2 = new GroupBox();
            this.cboPrinterPageSize = new ComboBox();
            this.label3 = new Label();
            this.optLandscape = new RadioButton();
            this.optPortrait = new RadioButton();
            this.cboPageToPrinterMapping = new ComboBox();
            this.cboPageSize = new ComboBox();
            this.Label8 = new Label();
            this.Label6 = new Label();
            this.printDialog_0 = new PrintDialog();
            this.cmdOK = new Button();
            this.cmdCancel = new Button();
            this.panel1.SuspendLayout();
            this.fraPrint.SuspendLayout();
            this.fraPrinter.SuspendLayout();
            this.Frame2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.fraPrint);
            this.panel1.Controls.Add(this.fraPrinter);
            this.panel1.Controls.Add(this.Frame2);
            this.panel1.Location = new Point(5, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1f3, 0x162);
            this.panel1.TabIndex = 0x17;
            this.fraPrint.BackColor = SystemColors.Control;
            this.fraPrint.Controls.Add(this.txbOverlap);
            this.fraPrint.Controls.Add(this.Label2);
            this.fraPrint.Controls.Add(this.textBox_copied);
            this.fraPrint.Controls.Add(this.lblPageCount);
            this.fraPrint.Controls.Add(this.label_copied);
            this.fraPrint.Controls.Add(this.txbStartPage);
            this.fraPrint.Controls.Add(this.txbEndPage);
            this.fraPrint.Controls.Add(this.Label5);
            this.fraPrint.Controls.Add(this.Label1);
            this.fraPrint.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fraPrint.ForeColor = SystemColors.ControlText;
            this.fraPrint.Location = new Point(0x102, 0x9a);
            this.fraPrint.Name = "fraPrint";
            this.fraPrint.RightToLeft = RightToLeft.No;
            this.fraPrint.Size = new Size(0xe8, 0xc0);
            this.fraPrint.TabIndex = 0x19;
            this.fraPrint.TabStop = false;
            this.fraPrint.Text = "打印页设置：";
            this.txbOverlap.AcceptsReturn = true;
            this.txbOverlap.BackColor = SystemColors.Window;
            this.txbOverlap.Cursor = Cursors.IBeam;
            this.txbOverlap.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbOverlap.ForeColor = SystemColors.WindowText;
            this.txbOverlap.Location = new Point(150, 0x98);
            this.txbOverlap.MaxLength = 0;
            this.txbOverlap.Name = "txbOverlap";
            this.txbOverlap.RightToLeft = RightToLeft.No;
            this.txbOverlap.Size = new Size(0x44, 20);
            this.txbOverlap.TabIndex = 0x1a;
            this.txbOverlap.Text = "0";
            this.txbOverlap.Leave += new EventHandler(this.txbOverlap_Leave);
            this.Label2.AutoSize = true;
            this.Label2.BackColor = SystemColors.Control;
            this.Label2.Cursor = Cursors.Default;
            this.Label2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label2.ForeColor = SystemColors.ControlText;
            this.Label2.Location = new Point(12, 0x9b);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = RightToLeft.No;
            this.Label2.Size = new Size(0x2b, 14);
            this.Label2.TabIndex = 0x1b;
            this.Label2.Text = "重叠度";
            this.textBox_copied.Location = new Point(0x72, 0x1a);
            this.textBox_copied.Name = "textBox_copied";
            this.textBox_copied.Size = new Size(0x69, 20);
            this.textBox_copied.TabIndex = 0x19;
            this.textBox_copied.Text = "1";
            this.textBox_copied.Leave += new EventHandler(this.textBox_copied_Leave);
            this.lblPageCount.BackColor = SystemColors.Control;
            this.lblPageCount.Cursor = Cursors.Default;
            this.lblPageCount.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPageCount.ForeColor = SystemColors.ControlText;
            this.lblPageCount.Location = new Point(12, 60);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.RightToLeft = RightToLeft.No;
            this.lblPageCount.Size = new Size(0x81, 14);
            this.lblPageCount.TabIndex = 0x18;
            this.lblPageCount.Text = "页            数： ";
            this.label_copied.AutoSize = true;
            this.label_copied.Location = new Point(10, 0x1f);
            this.label_copied.Name = "label_copied";
            this.label_copied.Size = new Size(0x4c, 14);
            this.label_copied.TabIndex = 13;
            this.label_copied.Text = "打 印 份 数：";
            this.txbStartPage.AcceptsReturn = true;
            this.txbStartPage.BackColor = SystemColors.Window;
            this.txbStartPage.Cursor = Cursors.IBeam;
            this.txbStartPage.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbStartPage.ForeColor = SystemColors.WindowText;
            this.txbStartPage.Location = new Point(0x74, 0x56);
            this.txbStartPage.MaxLength = 0;
            this.txbStartPage.Name = "txbStartPage";
            this.txbStartPage.RightToLeft = RightToLeft.No;
            this.txbStartPage.Size = new Size(0x68, 20);
            this.txbStartPage.TabIndex = 7;
            this.txbStartPage.Text = "1";
            this.txbStartPage.Leave += new EventHandler(this.txbStartPage_Leave);
            this.txbEndPage.AcceptsReturn = true;
            this.txbEndPage.BackColor = SystemColors.Window;
            this.txbEndPage.Cursor = Cursors.IBeam;
            this.txbEndPage.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbEndPage.ForeColor = SystemColors.WindowText;
            this.txbEndPage.Location = new Point(0x74, 120);
            this.txbEndPage.MaxLength = 0;
            this.txbEndPage.Name = "txbEndPage";
            this.txbEndPage.RightToLeft = RightToLeft.No;
            this.txbEndPage.Size = new Size(0x68, 20);
            this.txbEndPage.TabIndex = 6;
            this.txbEndPage.Text = "0";
            this.txbEndPage.Leave += new EventHandler(this.txbEndPage_Leave);
            this.Label5.AutoSize = true;
            this.Label5.BackColor = SystemColors.Control;
            this.Label5.Cursor = Cursors.Default;
            this.Label5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label5.ForeColor = SystemColors.ControlText;
            this.Label5.Location = new Point(12, 90);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = RightToLeft.No;
            this.Label5.Size = new Size(0x4f, 14);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "打印开始页：";
            this.Label1.AutoSize = true;
            this.Label1.BackColor = SystemColors.Control;
            this.Label1.Cursor = Cursors.Default;
            this.Label1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label1.ForeColor = SystemColors.ControlText;
            this.Label1.Location = new Point(12, 0x7a);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = RightToLeft.No;
            this.Label1.Size = new Size(0x4f, 14);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "打印结束页：";
            this.fraPrinter.BackColor = SystemColors.Control;
            this.fraPrinter.Controls.Add(this.checkBox_usePrinterPageSize);
            this.fraPrinter.Controls.Add(this.button_printerAttribute);
            this.fraPrinter.Controls.Add(this.comboBox_printer);
            this.fraPrinter.Controls.Add(this.lblPrinterOrientation);
            this.fraPrinter.Controls.Add(this.lblPrinterName);
            this.fraPrinter.Controls.Add(this.lblPrinterSize);
            this.fraPrinter.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fraPrinter.ForeColor = SystemColors.ControlText;
            this.fraPrinter.Location = new Point(10, 12);
            this.fraPrinter.Name = "fraPrinter";
            this.fraPrinter.RightToLeft = RightToLeft.No;
            this.fraPrinter.Size = new Size(0x1d9, 0x84);
            this.fraPrinter.TabIndex = 0x17;
            this.fraPrinter.TabStop = false;
            this.fraPrinter.Text = "打印机信息：";
            this.checkBox_usePrinterPageSize.Checked = true;
            this.checkBox_usePrinterPageSize.CheckState = CheckState.Checked;
            this.checkBox_usePrinterPageSize.Location = new Point(12, 0x6b);
            this.checkBox_usePrinterPageSize.Name = "checkBox_usePrinterPageSize";
            this.checkBox_usePrinterPageSize.Size = new Size(0x87, 0x10);
            this.checkBox_usePrinterPageSize.TabIndex = 0x1b;
            this.checkBox_usePrinterPageSize.Text = "使用打印机纸张大小";
            this.checkBox_usePrinterPageSize.Click += new EventHandler(this.checkBox_usePrinterPageSize_Click);
            this.checkBox_usePrinterPageSize.CheckedChanged += new EventHandler(this.checkBox_usePrinterPageSize_CheckedChanged);
            this.button_printerAttribute.Location = new Point(0x18e, 0x15);
            this.button_printerAttribute.Name = "button_printerAttribute";
            this.button_printerAttribute.Size = new Size(0x43, 0x17);
            this.button_printerAttribute.TabIndex = 0x1a;
            this.button_printerAttribute.Text = "属性";
            this.button_printerAttribute.Click += new EventHandler(this.button_printerAttribute_Click);
            this.comboBox_printer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_printer.Location = new Point(0x47, 0x16);
            this.comboBox_printer.Name = "comboBox_printer";
            this.comboBox_printer.Size = new Size(0x13d, 0x16);
            this.comboBox_printer.TabIndex = 0x19;
            this.comboBox_printer.SelectedIndexChanged += new EventHandler(this.comboBox_printer_SelectedIndexChanged);
            this.lblPrinterOrientation.BackColor = SystemColors.Control;
            this.lblPrinterOrientation.Cursor = Cursors.Default;
            this.lblPrinterOrientation.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPrinterOrientation.ForeColor = SystemColors.ControlText;
            this.lblPrinterOrientation.Location = new Point(10, 0x51);
            this.lblPrinterOrientation.Name = "lblPrinterOrientation";
            this.lblPrinterOrientation.RightToLeft = RightToLeft.No;
            this.lblPrinterOrientation.Size = new Size(0xd8, 0x13);
            this.lblPrinterOrientation.TabIndex = 0x18;
            this.lblPrinterOrientation.Text = "纸张方向：";
            this.lblPrinterName.AutoSize = true;
            this.lblPrinterName.BackColor = SystemColors.Control;
            this.lblPrinterName.Cursor = Cursors.Default;
            this.lblPrinterName.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPrinterName.ForeColor = SystemColors.ControlText;
            this.lblPrinterName.Location = new Point(10, 0x1a);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.RightToLeft = RightToLeft.No;
            this.lblPrinterName.Size = new Size(0x40, 14);
            this.lblPrinterName.TabIndex = 3;
            this.lblPrinterName.Text = "名       称：";
            this.lblPrinterSize.BackColor = SystemColors.Control;
            this.lblPrinterSize.Cursor = Cursors.Default;
            this.lblPrinterSize.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPrinterSize.ForeColor = SystemColors.ControlText;
            this.lblPrinterSize.Location = new Point(8, 0x37);
            this.lblPrinterSize.Name = "lblPrinterSize";
            this.lblPrinterSize.RightToLeft = RightToLeft.No;
            this.lblPrinterSize.Size = new Size(0xd8, 0x12);
            this.lblPrinterSize.TabIndex = 1;
            this.lblPrinterSize.Text = "纸张大小：";
            this.Frame2.BackColor = SystemColors.Control;
            this.Frame2.Controls.Add(this.cboPrinterPageSize);
            this.Frame2.Controls.Add(this.label3);
            this.Frame2.Controls.Add(this.optLandscape);
            this.Frame2.Controls.Add(this.optPortrait);
            this.Frame2.Controls.Add(this.cboPageToPrinterMapping);
            this.Frame2.Controls.Add(this.cboPageSize);
            this.Frame2.Controls.Add(this.Label8);
            this.Frame2.Controls.Add(this.Label6);
            this.Frame2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Frame2.ForeColor = SystemColors.ControlText;
            this.Frame2.Location = new Point(7, 0x9a);
            this.Frame2.Name = "Frame2";
            this.Frame2.RightToLeft = RightToLeft.No;
            this.Frame2.Size = new Size(0xe8, 190);
            this.Frame2.TabIndex = 0x16;
            this.Frame2.TabStop = false;
            this.Frame2.Text = "纸张：";
            this.cboPrinterPageSize.BackColor = SystemColors.Window;
            this.cboPrinterPageSize.Cursor = Cursors.Default;
            this.cboPrinterPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPrinterPageSize.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPrinterPageSize.ForeColor = SystemColors.WindowText;
            this.cboPrinterPageSize.Location = new Point(9, 40);
            this.cboPrinterPageSize.Name = "cboPrinterPageSize";
            this.cboPrinterPageSize.RightToLeft = RightToLeft.No;
            this.cboPrinterPageSize.Size = new Size(0xd6, 0x16);
            this.cboPrinterPageSize.TabIndex = 0x18;
            this.cboPrinterPageSize.SelectedIndexChanged += new EventHandler(this.cboPrinterPageSize_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.BackColor = SystemColors.Control;
            this.label3.Cursor = Cursors.Default;
            this.label3.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.ForeColor = SystemColors.ControlText;
            this.label3.Location = new Point(10, 20);
            this.label3.Name = "label3";
            this.label3.RightToLeft = RightToLeft.No;
            this.label3.Size = new Size(0x67, 14);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "打印机纸张大小：";
            this.optLandscape.BackColor = SystemColors.Control;
            this.optLandscape.Cursor = Cursors.Default;
            this.optLandscape.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optLandscape.ForeColor = SystemColors.ControlText;
            this.optLandscape.Location = new Point(0x53, 0x99);
            this.optLandscape.Name = "optLandscape";
            this.optLandscape.RightToLeft = RightToLeft.No;
            this.optLandscape.Size = new Size(0x75, 0x1b);
            this.optLandscape.TabIndex = 0x16;
            this.optLandscape.TabStop = true;
            this.optLandscape.Text = "横向";
            this.optLandscape.UseVisualStyleBackColor = false;
            this.optLandscape.Click += new EventHandler(this.optLandscape_Click);
            this.optPortrait.BackColor = SystemColors.Control;
            this.optPortrait.Cursor = Cursors.Default;
            this.optPortrait.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optPortrait.ForeColor = SystemColors.ControlText;
            this.optPortrait.Location = new Point(7, 0x98);
            this.optPortrait.Name = "optPortrait";
            this.optPortrait.RightToLeft = RightToLeft.No;
            this.optPortrait.Size = new Size(0x57, 0x1b);
            this.optPortrait.TabIndex = 0x15;
            this.optPortrait.TabStop = true;
            this.optPortrait.Text = "纵向";
            this.optPortrait.UseVisualStyleBackColor = false;
            this.optPortrait.Click += new EventHandler(this.optPortrait_Click);
            this.cboPageToPrinterMapping.BackColor = SystemColors.Window;
            this.cboPageToPrinterMapping.Cursor = Cursors.Default;
            this.cboPageToPrinterMapping.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPageToPrinterMapping.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPageToPrinterMapping.ForeColor = SystemColors.WindowText;
            this.cboPageToPrinterMapping.Location = new Point(9, 130);
            this.cboPageToPrinterMapping.Name = "cboPageToPrinterMapping";
            this.cboPageToPrinterMapping.RightToLeft = RightToLeft.No;
            this.cboPageToPrinterMapping.Size = new Size(0xd6, 0x16);
            this.cboPageToPrinterMapping.TabIndex = 20;
            this.cboPageToPrinterMapping.SelectedIndexChanged += new EventHandler(this.cboPageToPrinterMapping_SelectedIndexChanged);
            this.cboPageSize.BackColor = SystemColors.Window;
            this.cboPageSize.Cursor = Cursors.Default;
            this.cboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPageSize.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPageSize.ForeColor = SystemColors.WindowText;
            this.cboPageSize.Location = new Point(10, 0x55);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.RightToLeft = RightToLeft.No;
            this.cboPageSize.Size = new Size(0xd4, 0x16);
            this.cboPageSize.TabIndex = 0x12;
            this.cboPageSize.SelectedIndexChanged += new EventHandler(this.cboPageSize_SelectedIndexChanged);
            this.Label8.AutoSize = true;
            this.Label8.BackColor = SystemColors.Control;
            this.Label8.Cursor = Cursors.Default;
            this.Label8.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label8.ForeColor = SystemColors.ControlText;
            this.Label8.Location = new Point(12, 0x6f);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = RightToLeft.No;
            this.Label8.Size = new Size(0x7f, 14);
            this.Label8.TabIndex = 0x13;
            this.Label8.Text = "页面到打印机映射模式";
            this.Label6.AutoSize = true;
            this.Label6.BackColor = SystemColors.Control;
            this.Label6.Cursor = Cursors.Default;
            this.Label6.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label6.ForeColor = SystemColors.ControlText;
            this.Label6.Location = new Point(13, 0x43);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = RightToLeft.No;
            this.Label6.Size = new Size(0x5b, 14);
            this.Label6.TabIndex = 0x11;
            this.Label6.Text = "系统纸张大小：";
            this.cmdOK.BackColor = SystemColors.Control;
            this.cmdOK.Cursor = Cursors.Default;
            this.cmdOK.DialogResult = DialogResult.OK;
            this.cmdOK.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cmdOK.ForeColor = SystemColors.ControlText;
            this.cmdOK.Location = new Point(0x133, 0x16d);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.RightToLeft = RightToLeft.No;
            this.cmdOK.Size = new Size(0x4b, 0x17);
            this.cmdOK.TabIndex = 0x18;
            this.cmdOK.Text = "确定";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
            this.cmdCancel.DialogResult = DialogResult.Cancel;
            this.cmdCancel.Location = new Point(0x1ad, 0x16d);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new Size(0x4b, 0x17);
            this.cmdCancel.TabIndex = 0x19;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x202, 0x194);
            base.Controls.Add(this.cmdCancel);
            base.Controls.Add(this.cmdOK);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormPageSetup";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "打印设置窗口";
            base.Load += new EventHandler(this.FormPageSetup_Load);
            this.panel1.ResumeLayout(false);
            this.fraPrint.ResumeLayout(false);
            this.fraPrint.PerformLayout();
            this.fraPrinter.ResumeLayout(false);
            this.fraPrinter.PerformLayout();
            this.Frame2.ResumeLayout(false);
            this.Frame2.PerformLayout();
            base.ResumeLayout(false);
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
                this.lblPrinterSize.Text = "纸张大小：" + num.ToString("###.000") + " by " + num2.ToString("###.000") + " Inches";
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
            if (this.optLandscape.Checked && (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
            {
                this.ipageLayoutControl_0.Page.Orientation = 2;
            }
        }

        private void optPortrait_Click(object sender, EventArgs e)
        {
            if (this.optPortrait.Checked && (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
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
                this.txbEndPage.Text = this.ipageLayoutControl_0.get_PrinterPageCount(Convert.ToDouble(this.txbOverlap.Text)).ToString();
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

        private class Class0
        {
            private System.Drawing.Printing.PaperSize paperSize_0;

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
                get
                {
                    return this.paperSize_0;
                }
                set
                {
                }
            }
        }
    }
}

