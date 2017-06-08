using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmPageAndPrinterSetup : Form
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
        private GroupBox fraPrinter;
        private GroupBox frmPaper;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IPage ipage_0 = null;
        private IPageLayout2 ipageLayout2_0;
        private IPrinter iprinter_0 = null;
        private IPrinter iprinter_1 = null;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label3;
        private Label label4;
        private Label label7;
        private Label Label8;
        private Label label9;
        private Label lblInfo;
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
        private TextBox txtPageHeight;
        private TextBox txtPageWidth;

        public frmPageAndPrinterSetup()
        {
            this.InitializeComponent();
            this.method_0();
        }

        private void button_printerAttribute_Click(object sender, EventArgs e)
        {
            this.printDialog_0.ShowDialog();
            if (this.iprinter_0.Paper.PrinterName != this.printDialog_0.PrinterSettings.PrinterName.Trim())
            {
                this.iprinter_0.Paper.PrinterName = this.printDialog_0.PrinterSettings.PrinterName.Trim();
                this.method_6();
            }
            this.method_3();
            this.method_2();
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.ipage_0.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    double num;
                    double num2;
                    this.ipage_0.FormID = (esriPageFormID) this.cboPageSize.SelectedIndex;
                    this.ipage_0.QuerySize(out num, out num2);
                    num = this.method_12(this.ipage_0.Units, num);
                    num2 = this.method_12(this.ipage_0.Units, num2);
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
                this.ipage_0.PageToPrinterMapping = (esriPageToPrinterMapping) this.cboPageToPrinterMapping.SelectedIndex;
                this.method_2();
            }
        }

        private void cboPaperTrays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.iprinter_0.Paper.TrayID = (short) (this.cboPaperTrays.SelectedItem as Class7).TrayID;
            }
        }

        private void cboPrinterPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.iprinter_0.Paper.FormID = (short) (this.cboPrinterPageSize.SelectedItem as Class6).FormID;
                if (this.checkBox_usePrinterPageSize.Checked)
                {
                    double num;
                    double num2;
                    this.iprinter_0.Paper.QueryPaperSize(out num, out num2);
                    num = this.method_12(this.iprinter_0.Units, num);
                    num2 = this.method_12(this.iprinter_0.Units, num2);
                    if (this.ipage_0 != null)
                    {
                        this.bool_0 = false;
                        this.txtPageWidth.Text = num.ToString("0.#");
                        this.txtPageHeight.Text = num2.ToString("0.#");
                        this.bool_0 = true;
                        num = this.method_13(num, this.ipage_0.Units);
                        num2 = this.method_13(num2, this.ipage_0.Units);
                        this.ipage_0.PutCustomSize(num, num2);
                        this.ipage_0.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                    }
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
                this.ipage_0.Orientation = this.iprinter_0.Paper.Orientation;
                this.iprinter_0.Paper.QueryPaperSize(out num, out num2);
                num = this.method_12(this.iprinter_0.Units, num);
                num2 = this.method_12(this.iprinter_0.Units, num2);
                this.bool_0 = false;
                this.txtPageWidth.Text = num.ToString("0.#");
                this.txtPageHeight.Text = num2.ToString("0.#");
                this.bool_0 = true;
                num = this.method_13(num, this.ipage_0.Units);
                num2 = this.method_13(num2, this.ipage_0.Units);
                this.ipage_0.PutCustomSize(num, num2);
                this.ipage_0.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                if (this.ipage_0.Orientation == 1)
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
                this.iprinter_0.Paper.QueryPaperSize(out num, out num2);
                num = this.method_12(this.iprinter_0.Paper.Units, num);
                num2 = this.method_12(this.iprinter_0.Paper.Units, num2);
                num = this.method_13(num, this.ipage_0.Units);
                num2 = this.method_13(num2, this.ipage_0.Units);
                this.ipage_0.PutCustomSize(num, num2);
                this.cboPageSize.Visible = true;
                this.ipage_0.FormID = esriPageFormID.esriPageFormCUSTOM;
                this.cboPageSize.SelectedIndex = (int) this.ipage_0.FormID;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            this.method_1();
            base.DialogResult = DialogResult.OK;
        }

        private void comboBox_printer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.printDialog_0.PrinterSettings.PrinterName = this.comboBox_printer.Text;
                this.iprinter_0.Paper.PrinterName = this.printDialog_0.PrinterSettings.PrinterName.Trim();
                this.method_6();
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

        private void frmPageAndPrinterSetup_Load(object sender, EventArgs e)
        {
            if (this.ipageLayout2_0 != null)
            {
                this.checkBox_usePrinterPageSize.Checked = this.ipageLayout2_0.Page.FormID == esriPageFormID.esriPageFormSameAsPrinter;
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
                if (this.ipageLayout2_0.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    this.cboPageSize.SelectedIndex = (int) this.ipageLayout2_0.Page.FormID;
                    this.lblInfo.Text = "标准尺寸";
                    this.cboPageSize.Visible = true;
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
                this.cboPageToPrinterMapping.SelectedIndex = (int) this.ipageLayout2_0.Page.PageToPrinterMapping;
                if (this.iprinter_0.Paper.Orientation == 1)
                {
                    this.optPaperPortrait.Checked = true;
                }
                else
                {
                    this.optPaperLandscape.Checked = true;
                }
                if (this.ipageLayout2_0.Page.Orientation == 1)
                {
                    this.optPortrait.Checked = true;
                }
                else
                {
                    this.optLandscape.Checked = true;
                }
            }
            else
            {
                this.groupBox1.Enabled = false;
            }
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                this.comboBox_printer.Items.Add(str.Trim());
            }
            this.method_6();
            this.bool_0 = true;
            this.method_3();
            this.method_2();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageAndPrinterSetup));
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
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
            this.cboPageToPrinterMapping = new ComboBox();
            this.Label8 = new Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.fraPrinter.SuspendLayout();
            this.frmPaper.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.fraPrinter);
            this.panel1.Controls.Add(this.frmPaper);
            this.panel1.Location = new Point(5, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1b6, 0x1bd);
            this.panel1.TabIndex = 0x17;
            this.groupBox1.Controls.Add(this.cboPageToPrinterMapping);
            this.groupBox1.Controls.Add(this.Label8);
            this.groupBox1.Controls.Add(this.checkBox_usePrinterPageSize);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new Point(10, 0xfd);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1a1, 0xbd);
            this.groupBox1.TabIndex = 0x1a;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "地图页面设置";
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
            this.groupBox2.Size = new Size(0x182, 0x80);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "页面";
            this.label11.AutoSize = true;
            this.label11.BackColor = SystemColors.Control;
            this.label11.Cursor = Cursors.Default;
            this.label11.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label11.ForeColor = SystemColors.ControlText;
            this.label11.Location = new Point(10, 0x6a);
            this.label11.Name = "label11";
            this.label11.RightToLeft = RightToLeft.No;
            this.label11.Size = new Size(0x22, 14);
            this.label11.TabIndex = 0x29;
            this.label11.Text = "方向:";
            this.optLandscape.BackColor = SystemColors.Control;
            this.optLandscape.Cursor = Cursors.Default;
            this.optLandscape.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.optLandscape.ForeColor = SystemColors.ControlText;
            this.optLandscape.Location = new Point(0x9b, 0x66);
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
            this.optPortrait.Location = new Point(0x4f, 0x66);
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
            this.label9.Location = new Point(0xd4, 0x4a);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 0x24;
            this.label9.Text = "厘米";
            this.txtPageHeight.Location = new Point(0x49, 0x47);
            this.txtPageHeight.Name = "txtPageHeight";
            this.txtPageHeight.Size = new Size(0x87, 0x15);
            this.txtPageHeight.TabIndex = 0x23;
            this.txtPageHeight.TextChanged += new EventHandler(this.txtPageHeight_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(11, 0x4a);
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
            this.fraPrinter.Size = new Size(0x1a1, 0x84);
            this.fraPrinter.TabIndex = 0x17;
            this.fraPrinter.TabStop = false;
            this.fraPrinter.Text = "打印机信息：";
            this.button_printerAttribute.Location = new Point(0x159, 0x15);
            this.button_printerAttribute.Name = "button_printerAttribute";
            this.button_printerAttribute.Size = new Size(0x34, 0x17);
            this.button_printerAttribute.TabIndex = 0x1a;
            this.button_printerAttribute.Text = "属性";
            this.button_printerAttribute.Click += new EventHandler(this.button_printerAttribute_Click);
            this.comboBox_printer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_printer.Location = new Point(0x47, 0x16);
            this.comboBox_printer.Name = "comboBox_printer";
            this.comboBox_printer.Size = new Size(0x10c, 0x16);
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
            this.frmPaper.Location = new Point(10, 0x95);
            this.frmPaper.Name = "frmPaper";
            this.frmPaper.RightToLeft = RightToLeft.No;
            this.frmPaper.Size = new Size(0x1a1, 0x61);
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
            this.cboPaperTrays.Size = new Size(0xb2, 0x16);
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
            this.cboPrinterPageSize.Size = new Size(0xb2, 0x16);
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
            this.cmdPrint.Location = new Point(0x117, 0x1c3);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.RightToLeft = RightToLeft.No;
            this.cmdPrint.Size = new Size(0x4b, 0x17);
            this.cmdPrint.TabIndex = 0x18;
            this.cmdPrint.Text = "确定";
            this.cmdPrint.UseVisualStyleBackColor = false;
            this.cmdPrint.Click += new EventHandler(this.cmdPrint_Click);
            this.cmdCancel.Location = new Point(0x165, 0x1c3);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new Size(0x4b, 0x17);
            this.cmdCancel.TabIndex = 0x19;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
            this.cboPageToPrinterMapping.BackColor = SystemColors.Window;
            this.cboPageToPrinterMapping.Cursor = Cursors.Default;
            this.cboPageToPrinterMapping.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPageToPrinterMapping.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboPageToPrinterMapping.ForeColor = SystemColors.WindowText;
            this.cboPageToPrinterMapping.Location = new Point(0x11c, 0x17);
            this.cboPageToPrinterMapping.Name = "cboPageToPrinterMapping";
            this.cboPageToPrinterMapping.RightToLeft = RightToLeft.No;
            this.cboPageToPrinterMapping.Size = new Size(0x71, 0x16);
            this.cboPageToPrinterMapping.TabIndex = 0x20;
            this.cboPageToPrinterMapping.SelectedIndexChanged += new EventHandler(this.cboPageToPrinterMapping_SelectedIndexChanged);
            this.Label8.AutoSize = true;
            this.Label8.BackColor = SystemColors.Control;
            this.Label8.Cursor = Cursors.Default;
            this.Label8.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label8.ForeColor = SystemColors.ControlText;
            this.Label8.Location = new Point(0xa2, 0x19);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = RightToLeft.No;
            this.Label8.Size = new Size(0x76, 14);
            this.Label8.TabIndex = 0x1f;
            this.Label8.Text = "页面到纸张映射模式:";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1c6, 0x1e6);
            base.Controls.Add(this.cmdCancel);
            base.Controls.Add(this.cmdPrint);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmPageAndPrinterSetup";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "打印设置窗口";
            base.Load += new EventHandler(this.frmPageAndPrinterSetup_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
                IPrinter printer;
                if (this.ipageLayout2_0 != null)
                {
                    printer = this.ipageLayout2_0.Printer;
                }
                else
                {
                    printer = this.iprinter_1;
                }
                printer.Paper.FormID = this.iprinter_0.Paper.FormID;
                printer.Paper.Orientation = this.iprinter_0.Paper.Orientation;
                printer.Paper.PrinterName = this.iprinter_0.Paper.PrinterName;
                printer.Paper.TrayID = this.iprinter_0.Paper.TrayID;
            }
            if (this.ipage_0 != null)
            {
                IPage page = this.ipageLayout2_0.Page;
                if (this.ipage_0.FormID == esriPageFormID.esriPageFormCUSTOM)
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
                page.PageToPrinterMapping = (esriPageToPrinterMapping) this.cboPageToPrinterMapping.SelectedIndex;
                page.Orientation = this.ipage_0.Orientation;
            }
        }

        private bool method_10(string string_0)
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

        private bool method_11(string string_0)
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

        private double method_12(esriUnits esriUnits_0, double double_0)
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

        private double method_13(double double_0, esriUnits esriUnits_0)
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
        }

        private void method_3()
        {
            if (this.iprinter_0 != null)
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
                this.comboBox_printer.Text = printer.Paper.PrinterName;
                printer.Paper.QueryPaperSize(out num, out num2);
                num = this.method_12(printer.Paper.Units, num);
                num2 = this.method_12(printer.Paper.Units, num2);
                this.lblPrinterSize.Text = "纸张大小：" + num.ToString("###.000") + " \x00d7 " + num2.ToString("###.000") + " ";
            }
        }

        private void method_4(object sender, EventArgs e)
        {
        }

        private void method_5(object sender, EventArgs e)
        {
        }

        private void method_6()
        {
            string str;
            int num;
            this.cboPrinterPageSize.Items.Clear();
            IEnumNamedID forms = this.iprinter_0.Paper.Forms;
            forms.Reset();
            for (num = forms.Next(out str); str != null; num = forms.Next(out str))
            {
                this.cboPrinterPageSize.Items.Add(new Class6(str, num));
            }
            this.cboPrinterPageSize.Text = this.iprinter_0.Paper.FormName;
            this.cboPaperTrays.Items.Clear();
            forms = this.iprinter_0.Paper.Trays;
            forms.Reset();
            num = forms.Next(out str);
            int num2 = 0;
            while (str != null)
            {
                this.cboPaperTrays.Items.Add(new Class7(str, num));
                if (num == this.iprinter_0.Paper.TrayID)
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
            if (this.ipage_0 != null)
            {
                double num3;
                double num4;
                if (this.checkBox_usePrinterPageSize.Checked)
                {
                    this.iprinter_0.Paper.QueryPaperSize(out num3, out num4);
                    num3 = this.method_12(this.iprinter_0.Units, num3);
                    num4 = this.method_12(this.iprinter_0.Units, num4);
                    this.ipage_0.PutCustomSize(num3, num4);
                    this.ipage_0.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                }
                else
                {
                    this.ipage_0.QuerySize(out num3, out num4);
                    num3 = this.method_12(this.ipage_0.Units, num3);
                    num4 = this.method_12(this.ipage_0.Units, num4);
                }
                this.bool_0 = false;
                this.txtPageWidth.Text = num3.ToString("0.##");
                this.txtPageHeight.Text = num4.ToString("0.##");
                this.bool_0 = true;
            }
        }

        private void method_7(object sender, EventArgs e)
        {
        }

        private void method_8(object sender, EventArgs e)
        {
        }

        private void method_9(object sender, EventArgs e)
        {
        }

        private void optLandscape_Click(object sender, EventArgs e)
        {
            if (this.optLandscape.Checked && (this.ipage_0.FormID != esriPageFormID.esriPageFormSameAsPrinter))
            {
                this.ipage_0.Orientation = 2;
            }
        }

        private void optPaperLandscape_Click(object sender, EventArgs e)
        {
            if (this.optPaperLandscape.Checked)
            {
                this.iprinter_0.Paper.Orientation = 2;
                if (this.ipage_0.FormID == esriPageFormID.esriPageFormSameAsPrinter)
                {
                    this.ipage_0.Orientation = 2;
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
                this.iprinter_0.Paper.Orientation = 1;
                if (this.ipage_0.FormID == esriPageFormID.esriPageFormSameAsPrinter)
                {
                    this.ipage_0.Orientation = 1;
                    this.optLandscape.Checked = false;
                    this.optPortrait.Checked = true;
                }
                this.method_3();
                this.method_2();
            }
        }

        private void optPortrait_Click(object sender, EventArgs e)
        {
            if (this.optPortrait.Checked && (this.ipage_0.FormID != esriPageFormID.esriPageFormSameAsPrinter))
            {
                this.ipage_0.Orientation = 2;
            }
        }

        public void setPageLayout(IPageLayout ipageLayout_0)
        {
            this.ipageLayout2_0 = ipageLayout_0 as IPageLayout2;
            this.iprinter_0 = (this.ipageLayout2_0.Printer as IClone).Clone() as IPrinter;
            this.ipage_0 = (this.ipageLayout2_0.Page as IClone).Clone() as IPage;
        }

        public void setPrinter(IPrinter iprinter_2)
        {
            this.ipageLayout2_0 = null;
            this.iprinter_1 = iprinter_2;
            this.iprinter_0 = (iprinter_2 as IClone).Clone() as IPrinter;
            this.ipage_0 = null;
        }

        private void txtPageHeight_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num;
                    double num2;
                    this.ipage_0.QuerySize(out num, out num2);
                    num2 = Convert.ToDouble(this.txtPageHeight.Text);
                    num2 = this.method_13(num2, this.ipage_0.Units);
                    this.ipage_0.PutCustomSize(num, num2);
                    this.ipage_0.FormID = esriPageFormID.esriPageFormCUSTOM;
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
                    this.ipage_0.QuerySize(out num, out num2);
                    num = Convert.ToDouble(this.txtPageWidth.Text);
                    num = this.method_13(num, this.ipage_0.Units);
                    this.ipage_0.PutCustomSize(num, num2);
                    this.ipage_0.FormID = esriPageFormID.esriPageFormCUSTOM;
                    this.method_2();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private class Class5
        {
            private System.Drawing.Printing.PaperSize paperSize_0;

            public Class5(System.Drawing.Printing.PaperSize paperSize_1)
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

        private class Class6
        {
            private int int_0;
            private string string_0;

            public Class6(string string_1, int int_1)
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

        private class Class7
        {
            private int int_0;
            private string string_0;

            public Class7(string string_1, int int_1)
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

