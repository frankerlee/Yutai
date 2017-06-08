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
    public class FormPrinterSetup : Form
    {
        private bool bool_0 = false;
        private Button button_printerAttribute;
        private ComboBox cboPageSize;
        private ComboBox cboPageToPrinterMapping;
        private ComboBox cboPaperTrays;
        private ComboBox cboPrinterPageSize;
        private CheckBox checkBox_usePrinterPageSize;
        private Button cmdCancel;
        private Button cmdPrint;
        private ComboBox comboBox_printer;
        private Container container_0 = null;
        private GroupBox fraPrint;
        private GroupBox fraPrinter;
        private GroupBox frmPaper;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IPage ipage_0 = null;
        private IPageLayoutControl ipageLayoutControl_0;
        private IPrinter iprinter_0 = null;
        private Label label_copied;
        private Label Label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label Label2;
        private Label label3;
        private Label label4;
        private Label Label5;
        private Label label7;
        private Label Label8;
        private Label label9;
        private Label lblInfo;
        private Label lblPageCount;
        private Label lblPrinterName;
        private Label lblPrinterOrientation;
        private Label lblPrinterSize;
        private RadioButton optLandscape;
        private RadioButton optPaperLandscape;
        private RadioButton optPaperPortrait;
        private RadioButton optPortrait;
        private Panel panel1;
        private PrintDialog printDialog_0;
        private PrinterSettings printerSettings_0;
        private TextBox textBox_copied;
        private TextBox txbEndPage;
        private TextBox txbOverlap;
        private TextBox txbStartPage;
        private TextBox txtPageHeight;
        private TextBox txtPageWidth;

        public FormPrinterSetup()
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
                this.ipageLayoutControl_0.Page.PageToPrinterMapping = (esriPageToPrinterMapping) this.cboPageToPrinterMapping.SelectedIndex;
                this.method_2();
            }
        }

        private void cboPaperTrays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Printer.Paper.TrayID = (short) (this.cboPaperTrays.SelectedItem as Class4).TrayID;
            }
        }

        private void cboPrinterPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ipageLayoutControl_0.Printer.Paper.FormID = (short)(this.cboPrinterPageSize.SelectedItem as Class3).FormID;
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
                this.ipageLayoutControl_0.Printer.Paper.PrinterName = this.printDialog_0.PrinterSettings.PrinterName.Trim();
                this.method_4();
                this.method_3();
                this.method_2();
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

        private void FormPrinterSetup_Load(object sender, EventArgs e)
        {
            this.checkBox_usePrinterPageSize.Checked = this.ipageLayoutControl_0.Page.FormID == esriPageFormID.esriPageFormSameAsPrinter;
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrinterSetup));
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.cboPageToPrinterMapping = new ComboBox();
            this.Label8 = new Label();
            this.checkBox_usePrinterPageSize = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.label11 = new Label();
            this.optLandscape = new RadioButton();
            this.optPortrait = new RadioButton();
            this.lblInfo = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.txtPageHeight = new TextBox();
            this.label7 = new Label();
            this.txtPageWidth = new TextBox();
            this.label4 = new Label();
            this.cboPageSize = new ComboBox();
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
            this.button_printerAttribute = new Button();
            this.comboBox_printer = new ComboBox();
            this.lblPrinterOrientation = new Label();
            this.lblPrinterName = new Label();
            this.lblPrinterSize = new Label();
            this.frmPaper = new GroupBox();
            this.cboPaperTrays = new ComboBox();
            this.label12 = new Label();
            this.optPaperLandscape = new RadioButton();
            this.label13 = new Label();
            this.optPaperPortrait = new RadioButton();
            this.cboPrinterPageSize = new ComboBox();
            this.label3 = new Label();
            this.printDialog_0 = new PrintDialog();
            this.cmdPrint = new Button();
            this.cmdCancel = new Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.fraPrint.SuspendLayout();
            this.fraPrinter.SuspendLayout();
            this.frmPaper.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.fraPrint);
            this.panel1.Controls.Add(this.fraPrinter);
            this.panel1.Controls.Add(this.frmPaper);
            this.panel1.Location = new Point(5, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x20a, 0x195);
            this.panel1.TabIndex = 0x17;
            this.groupBox1.Controls.Add(this.cboPageToPrinterMapping);
            this.groupBox1.Controls.Add(this.Label8);
            this.groupBox1.Controls.Add(this.checkBox_usePrinterPageSize);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new Point(11, 0x99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x114, 240);
            this.groupBox1.TabIndex = 0x1a;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "地图页面尺寸";
            this.cboPageToPrinterMapping.BackColor = SystemColors.Window;
            this.cboPageToPrinterMapping.Cursor = Cursors.Default;
            this.cboPageToPrinterMapping.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPageToPrinterMapping.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPageToPrinterMapping.ForeColor = SystemColors.WindowText;
            this.cboPageToPrinterMapping.Location = new Point(0x83, 0xcd);
            this.cboPageToPrinterMapping.Name = "cboPageToPrinterMapping";
            this.cboPageToPrinterMapping.RightToLeft = RightToLeft.No;
            this.cboPageToPrinterMapping.Size = new Size(0x87, 0x16);
            this.cboPageToPrinterMapping.TabIndex = 30;
            this.cboPageToPrinterMapping.SelectedIndexChanged += new EventHandler(this.cboPageToPrinterMapping_SelectedIndexChanged);
            this.Label8.AutoSize = true;
            this.Label8.BackColor = SystemColors.Control;
            this.Label8.Cursor = Cursors.Default;
            this.Label8.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label8.ForeColor = SystemColors.ControlText;
            this.Label8.Location = new Point(9, 0xcf);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = RightToLeft.No;
            this.Label8.Size = new Size(0x76, 14);
            this.Label8.TabIndex = 0x1d;
            this.Label8.Text = "页面到纸张映射模式:";
            this.checkBox_usePrinterPageSize.Checked = true;
            this.checkBox_usePrinterPageSize.CheckState = CheckState.Checked;
            this.checkBox_usePrinterPageSize.Location = new Point(12, 0x19);
            this.checkBox_usePrinterPageSize.Name = "checkBox_usePrinterPageSize";
            this.checkBox_usePrinterPageSize.Size = new Size(0x89, 0x13);
            this.checkBox_usePrinterPageSize.TabIndex = 0x1c;
            this.checkBox_usePrinterPageSize.Text = "使用打印机纸张设置";
            this.checkBox_usePrinterPageSize.Click += new EventHandler(this.checkBox_usePrinterPageSize_Click);
            this.checkBox_usePrinterPageSize.CheckedChanged += new EventHandler(this.checkBox_usePrinterPageSize_CheckedChanged);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.optLandscape);
            this.groupBox2.Controls.Add(this.optPortrait);
            this.groupBox2.Controls.Add(this.lblInfo);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtPageHeight);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtPageWidth);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboPageSize);
            this.groupBox2.Location = new Point(10, 0x33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xfe, 0x8d);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "页面";
            this.label11.AutoSize = true;
            this.label11.BackColor = SystemColors.Control;
            this.label11.Cursor = Cursors.Default;
            this.label11.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label11.ForeColor = SystemColors.ControlText;
            this.label11.Location = new Point(10, 0x6d);
            this.label11.Name = "label11";
            this.label11.RightToLeft = RightToLeft.No;
            this.label11.Size = new Size(0x22, 14);
            this.label11.TabIndex = 0x29;
            this.label11.Text = "方向:";
            this.optLandscape.BackColor = SystemColors.Control;
            this.optLandscape.Cursor = Cursors.Default;
            this.optLandscape.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optLandscape.ForeColor = SystemColors.ControlText;
            this.optLandscape.Location = new Point(0x9b, 0x69);
            this.optLandscape.Name = "optLandscape";
            this.optLandscape.RightToLeft = RightToLeft.No;
            this.optLandscape.Size = new Size(50, 0x17);
            this.optLandscape.TabIndex = 40;
            this.optLandscape.TabStop = true;
            this.optLandscape.Text = "垂直";
            this.optLandscape.UseVisualStyleBackColor = false;
            this.optLandscape.Click += new EventHandler(this.optLandscape_Click);
            this.optPortrait.BackColor = SystemColors.Control;
            this.optPortrait.Cursor = Cursors.Default;
            this.optPortrait.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optPortrait.ForeColor = SystemColors.ControlText;
            this.optPortrait.Location = new Point(0x4f, 0x69);
            this.optPortrait.Name = "optPortrait";
            this.optPortrait.RightToLeft = RightToLeft.No;
            this.optPortrait.Size = new Size(50, 0x17);
            this.optPortrait.TabIndex = 0x27;
            this.optPortrait.TabStop = true;
            this.optPortrait.Text = "水平";
            this.optPortrait.UseVisualStyleBackColor = false;
            this.optPortrait.Click += new EventHandler(this.optPortrait_Click);
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = SystemColors.Control;
            this.lblInfo.Cursor = Cursors.Default;
            this.lblInfo.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblInfo.ForeColor = SystemColors.ControlText;
            this.lblInfo.Location = new Point(11, 0x15);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.RightToLeft = RightToLeft.No;
            this.lblInfo.Size = new Size(0x3a, 14);
            this.lblInfo.TabIndex = 0x26;
            this.lblInfo.Text = "标准尺寸:";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0xd4, 0x2f);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 0x25;
            this.label10.Text = "厘米";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xd4, 0x4c);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 0x24;
            this.label9.Text = "厘米";
            this.txtPageHeight.Location = new Point(0x49, 0x49);
            this.txtPageHeight.Name = "txtPageHeight";
            this.txtPageHeight.Size = new Size(0x87, 0x15);
            this.txtPageHeight.TabIndex = 0x23;
            this.txtPageHeight.TextChanged += new EventHandler(this.txtPageHeight_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(11, 0x4c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x17, 12);
            this.label7.TabIndex = 0x22;
            this.label7.Text = "高:";
            this.txtPageWidth.Location = new Point(0x49, 0x2d);
            this.txtPageWidth.Name = "txtPageWidth";
            this.txtPageWidth.Size = new Size(0x87, 0x15);
            this.txtPageWidth.TabIndex = 0x21;
            this.txtPageWidth.TextChanged += new EventHandler(this.txtPageWidth_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(11, 0x2f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x17, 12);
            this.label4.TabIndex = 0x20;
            this.label4.Text = "宽:";
            this.cboPageSize.BackColor = SystemColors.Window;
            this.cboPageSize.Cursor = Cursors.Default;
            this.cboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPageSize.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPageSize.ForeColor = SystemColors.WindowText;
            this.cboPageSize.Location = new Point(0x49, 0x11);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.RightToLeft = RightToLeft.No;
            this.cboPageSize.Size = new Size(0xa9, 0x16);
            this.cboPageSize.TabIndex = 0x1f;
            this.cboPageSize.SelectedIndexChanged += new EventHandler(this.cboPageSize_SelectedIndexChanged);
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
            this.fraPrint.Location = new Point(0x123, 0xfe);
            this.fraPrint.Name = "fraPrint";
            this.fraPrint.RightToLeft = RightToLeft.No;
            this.fraPrint.Size = new Size(220, 140);
            this.fraPrint.TabIndex = 0x19;
            this.fraPrint.TabStop = false;
            this.fraPrint.Text = "打印页设置";
            this.txbOverlap.AcceptsReturn = true;
            this.txbOverlap.BackColor = SystemColors.Window;
            this.txbOverlap.Cursor = Cursors.IBeam;
            this.txbOverlap.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbOverlap.ForeColor = SystemColors.WindowText;
            this.txbOverlap.Location = new Point(0x55, 0x71);
            this.txbOverlap.MaxLength = 0;
            this.txbOverlap.Name = "txbOverlap";
            this.txbOverlap.RightToLeft = RightToLeft.No;
            this.txbOverlap.Size = new Size(0x7a, 20);
            this.txbOverlap.TabIndex = 0x1a;
            this.txbOverlap.Text = "0";
            this.txbOverlap.Leave += new EventHandler(this.txbOverlap_Leave);
            this.Label2.AutoSize = true;
            this.Label2.BackColor = SystemColors.Control;
            this.Label2.Cursor = Cursors.Default;
            this.Label2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label2.ForeColor = SystemColors.ControlText;
            this.Label2.Location = new Point(11, 0x71);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = RightToLeft.No;
            this.Label2.Size = new Size(0x2e, 14);
            this.Label2.TabIndex = 0x1b;
            this.Label2.Text = "重叠度:";
            this.textBox_copied.Location = new Point(0x55, 15);
            this.textBox_copied.Name = "textBox_copied";
            this.textBox_copied.Size = new Size(0x7a, 20);
            this.textBox_copied.TabIndex = 0x19;
            this.textBox_copied.Text = "1";
            this.textBox_copied.Leave += new EventHandler(this.textBox_copied_Leave);
            this.lblPageCount.BackColor = SystemColors.Control;
            this.lblPageCount.Cursor = Cursors.Default;
            this.lblPageCount.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPageCount.ForeColor = SystemColors.ControlText;
            this.lblPageCount.Location = new Point(10, 0x2b);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.RightToLeft = RightToLeft.No;
            this.lblPageCount.Size = new Size(0x81, 14);
            this.lblPageCount.TabIndex = 0x18;
            this.lblPageCount.Text = "页            数: ";
            this.label_copied.AutoSize = true;
            this.label_copied.Location = new Point(10, 0x13);
            this.label_copied.Name = "label_copied";
            this.label_copied.Size = new Size(0x43, 14);
            this.label_copied.TabIndex = 13;
            this.label_copied.Text = "打 印 份 数:";
            this.txbStartPage.AcceptsReturn = true;
            this.txbStartPage.BackColor = SystemColors.Window;
            this.txbStartPage.Cursor = Cursors.IBeam;
            this.txbStartPage.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbStartPage.ForeColor = SystemColors.WindowText;
            this.txbStartPage.Location = new Point(0x55, 0x39);
            this.txbStartPage.MaxLength = 0;
            this.txbStartPage.Name = "txbStartPage";
            this.txbStartPage.RightToLeft = RightToLeft.No;
            this.txbStartPage.Size = new Size(0x7a, 20);
            this.txbStartPage.TabIndex = 7;
            this.txbStartPage.Text = "1";
            this.txbStartPage.Leave += new EventHandler(this.txbStartPage_Leave);
            this.txbEndPage.AcceptsReturn = true;
            this.txbEndPage.BackColor = SystemColors.Window;
            this.txbEndPage.Cursor = Cursors.IBeam;
            this.txbEndPage.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbEndPage.ForeColor = SystemColors.WindowText;
            this.txbEndPage.Location = new Point(0x55, 0x53);
            this.txbEndPage.MaxLength = 0;
            this.txbEndPage.Name = "txbEndPage";
            this.txbEndPage.RightToLeft = RightToLeft.No;
            this.txbEndPage.Size = new Size(0x7a, 20);
            this.txbEndPage.TabIndex = 6;
            this.txbEndPage.Text = "0";
            this.txbEndPage.Leave += new EventHandler(this.txbEndPage_Leave);
            this.Label5.AutoSize = true;
            this.Label5.BackColor = SystemColors.Control;
            this.Label5.Cursor = Cursors.Default;
            this.Label5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label5.ForeColor = SystemColors.ControlText;
            this.Label5.Location = new Point(6, 0x41);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = RightToLeft.No;
            this.Label5.Size = new Size(70, 14);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "打印开始页:";
            this.Label1.AutoSize = true;
            this.Label1.BackColor = SystemColors.Control;
            this.Label1.Cursor = Cursors.Default;
            this.Label1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label1.ForeColor = SystemColors.ControlText;
            this.Label1.Location = new Point(9, 0x59);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = RightToLeft.No;
            this.Label1.Size = new Size(70, 14);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "打印结束页:";
            this.fraPrinter.BackColor = SystemColors.Control;
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
            this.fraPrinter.Size = new Size(0x1f3, 0x84);
            this.fraPrinter.TabIndex = 0x17;
            this.fraPrinter.TabStop = false;
            this.fraPrinter.Text = "打印机信息：";
            this.button_printerAttribute.Location = new Point(0x1a8, 0x15);
            this.button_printerAttribute.Name = "button_printerAttribute";
            this.button_printerAttribute.Size = new Size(0x43, 0x17);
            this.button_printerAttribute.TabIndex = 0x1a;
            this.button_printerAttribute.Text = "属性";
            this.button_printerAttribute.Click += new EventHandler(this.button_printerAttribute_Click);
            this.comboBox_printer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_printer.Location = new Point(0x47, 0x16);
            this.comboBox_printer.Name = "comboBox_printer";
            this.comboBox_printer.Size = new Size(0x15a, 0x16);
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
            this.frmPaper.BackColor = SystemColors.Control;
            this.frmPaper.Controls.Add(this.cboPaperTrays);
            this.frmPaper.Controls.Add(this.label12);
            this.frmPaper.Controls.Add(this.optPaperLandscape);
            this.frmPaper.Controls.Add(this.label13);
            this.frmPaper.Controls.Add(this.optPaperPortrait);
            this.frmPaper.Controls.Add(this.cboPrinterPageSize);
            this.frmPaper.Controls.Add(this.label3);
            this.frmPaper.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.frmPaper.ForeColor = SystemColors.ControlText;
            this.frmPaper.Location = new Point(0x123, 0x99);
            this.frmPaper.Name = "frmPaper";
            this.frmPaper.RightToLeft = RightToLeft.No;
            this.frmPaper.Size = new Size(220, 0x61);
            this.frmPaper.TabIndex = 0x16;
            this.frmPaper.TabStop = false;
            this.frmPaper.Text = "纸张";
            this.cboPaperTrays.BackColor = SystemColors.Window;
            this.cboPaperTrays.Cursor = Cursors.Default;
            this.cboPaperTrays.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPaperTrays.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPaperTrays.ForeColor = SystemColors.WindowText;
            this.cboPaperTrays.Location = new Point(0x4a, 0x2a);
            this.cboPaperTrays.Name = "cboPaperTrays";
            this.cboPaperTrays.RightToLeft = RightToLeft.No;
            this.cboPaperTrays.Size = new Size(140, 0x16);
            this.cboPaperTrays.TabIndex = 0x2b;
            this.cboPaperTrays.SelectedIndexChanged += new EventHandler(this.cboPaperTrays_SelectedIndexChanged);
            this.label12.AutoSize = true;
            this.label12.BackColor = SystemColors.Control;
            this.label12.Cursor = Cursors.Default;
            this.label12.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label12.ForeColor = SystemColors.ControlText;
            this.label12.Location = new Point(10, 0x48);
            this.label12.Name = "label12";
            this.label12.RightToLeft = RightToLeft.No;
            this.label12.Size = new Size(0x22, 14);
            this.label12.TabIndex = 0x2e;
            this.label12.Text = "方向:";
            this.optPaperLandscape.BackColor = SystemColors.Control;
            this.optPaperLandscape.Cursor = Cursors.Default;
            this.optPaperLandscape.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optPaperLandscape.ForeColor = SystemColors.ControlText;
            this.optPaperLandscape.Location = new Point(0x99, 0x44);
            this.optPaperLandscape.Name = "optPaperLandscape";
            this.optPaperLandscape.RightToLeft = RightToLeft.No;
            this.optPaperLandscape.Size = new Size(50, 0x17);
            this.optPaperLandscape.TabIndex = 0x2d;
            this.optPaperLandscape.TabStop = true;
            this.optPaperLandscape.Text = "垂直";
            this.optPaperLandscape.UseVisualStyleBackColor = false;
            this.optPaperLandscape.Click += new EventHandler(this.optPaperLandscape_Click);
            this.label13.AutoSize = true;
            this.label13.BackColor = SystemColors.Control;
            this.label13.Cursor = Cursors.Default;
            this.label13.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label13.ForeColor = SystemColors.ControlText;
            this.label13.Location = new Point(10, 0x2a);
            this.label13.Name = "label13";
            this.label13.RightToLeft = RightToLeft.No;
            this.label13.Size = new Size(0x3a, 14);
            this.label13.TabIndex = 0x2a;
            this.label13.Text = "纸张来源:";
            this.optPaperPortrait.BackColor = SystemColors.Control;
            this.optPaperPortrait.Cursor = Cursors.Default;
            this.optPaperPortrait.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optPaperPortrait.ForeColor = SystemColors.ControlText;
            this.optPaperPortrait.Location = new Point(0x4b, 0x44);
            this.optPaperPortrait.Name = "optPaperPortrait";
            this.optPaperPortrait.RightToLeft = RightToLeft.No;
            this.optPaperPortrait.Size = new Size(50, 0x17);
            this.optPaperPortrait.TabIndex = 0x2c;
            this.optPaperPortrait.TabStop = true;
            this.optPaperPortrait.Text = "水平";
            this.optPaperPortrait.UseVisualStyleBackColor = false;
            this.optPaperPortrait.Click += new EventHandler(this.optPaperPortrait_Click);
            this.cboPrinterPageSize.BackColor = SystemColors.Window;
            this.cboPrinterPageSize.Cursor = Cursors.Default;
            this.cboPrinterPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPrinterPageSize.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPrinterPageSize.ForeColor = SystemColors.WindowText;
            this.cboPrinterPageSize.Location = new Point(0x4a, 0x10);
            this.cboPrinterPageSize.Name = "cboPrinterPageSize";
            this.cboPrinterPageSize.RightToLeft = RightToLeft.No;
            this.cboPrinterPageSize.Size = new Size(140, 0x16);
            this.cboPrinterPageSize.TabIndex = 0x18;
            this.cboPrinterPageSize.SelectedIndexChanged += new EventHandler(this.cboPrinterPageSize_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.BackColor = SystemColors.Control;
            this.label3.Cursor = Cursors.Default;
            this.label3.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.ForeColor = SystemColors.ControlText;
            this.label3.Location = new Point(10, 0x12);
            this.label3.Name = "label3";
            this.label3.RightToLeft = RightToLeft.No;
            this.label3.Size = new Size(0x22, 14);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "纸型:";
            this.cmdPrint.BackColor = SystemColors.Control;
            this.cmdPrint.Cursor = Cursors.Default;
            this.cmdPrint.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cmdPrint.ForeColor = SystemColors.ControlText;
            this.cmdPrint.Location = new Point(0x161, 0x19e);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.RightToLeft = RightToLeft.No;
            this.cmdPrint.Size = new Size(0x4b, 0x17);
            this.cmdPrint.TabIndex = 0x18;
            this.cmdPrint.Text = "打印";
            this.cmdPrint.UseVisualStyleBackColor = false;
            this.cmdPrint.Click += new EventHandler(this.cmdPrint_Click);
            this.cmdCancel.Location = new Point(0x1be, 0x19e);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new Size(0x4b, 0x17);
            this.cmdCancel.TabIndex = 0x19;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x217, 0x1bd);
            base.Controls.Add(this.cmdCancel);
            base.Controls.Add(this.cmdPrint);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormPrinterSetup";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "打印设置窗口";
            base.Load += new EventHandler(this.FormPrinterSetup_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.fraPrint.ResumeLayout(false);
            this.fraPrint.PerformLayout();
            this.fraPrinter.ResumeLayout(false);
            this.fraPrinter.PerformLayout();
            this.frmPaper.ResumeLayout(false);
            this.frmPaper.PerformLayout();
            base.ResumeLayout(false);
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
                this.lblPrinterSize.Text = "纸张大小：" + num.ToString("###.000") + " \x00d7 " + num2.ToString("###.000") + " ";
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

        private double method_8(double double_0, esriUnits esriUnits_0)
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

        private void optLandscape_Click(object sender, EventArgs e)
        {
            if (this.optLandscape.Checked && (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
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
            if (this.optPortrait.Checked && (this.ipageLayoutControl_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter))
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
                this.txbEndPage.Text = this.ipageLayoutControl_0.get_PrinterPageCount(Convert.ToDouble(this.txbOverlap.Text)).ToString();
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

        private class Class2
        {
            private System.Drawing.Printing.PaperSize paperSize_0;

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
                get
                {
                    return this.paperSize_0;
                }
                set
                {
                }
            }
        }

        private class Class3
        {
            private int int_0;
            private string string_0;

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
                get
                {
                    return this.int_0;
                }
            }

            public string FormName
            {
                get
                {
                    return this.string_0;
                }
            }
        }

        private class Class4
        {
            private int int_0;
            private string string_0;

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
                get
                {
                    return this.int_0;
                }
            }

            public string TrayName
            {
                get
                {
                    return this.string_0;
                }
            }
        }
    }
}

