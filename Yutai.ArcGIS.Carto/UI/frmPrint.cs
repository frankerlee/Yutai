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
    public class frmPrint : Form
    {
        private bool bool_0 = false;
        private Button btnprinterAttribute;
        private Button cmdCancel;
        private Button cmdPrint;
        private ComboBox comboBox_printer;
        private Container container_0 = null;
        private GroupBox fraPrint;
        private GroupBox fraPrinter;
        private GroupBox frmPaper;
        private IActiveView iactiveView_0 = null;
        private IPageLayoutControl ipageLayoutControl_0 = null;
        private IPrinter iprinter_0 = null;
        private Label label_copied;
        private Label label1;
        private Label label13;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label8;
        private Label lblPageCount;
        private Label lblPrinterName;
        private Label lblPrinterName2;
        private Label lblPrinterOrientation;
        private Label lblPrinterSize;
        private Panel panel1;
        private PrintDialog printDialog_0;
        private RadioButton radioButton1;
        private RadioButton rdoAll;
        private RadioButton rdoSpecify;
        private RadioButton rdoTitle;
        private TextBox textBox_copied;
        private TrackBar trackBar1;
        private NumericUpDown txbEndPage;
        private TextBox txbOverlap;
        private NumericUpDown txbStartPage;
        private TextBox txtQulity;

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
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

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
        private void InitializeComponent()
        {
            this.fraPrinter = new GroupBox();
            this.lblPrinterName = new Label();
            this.btnprinterAttribute = new Button();
            this.lblPrinterOrientation = new Label();
            this.lblPrinterName2 = new Label();
            this.lblPrinterSize = new Label();
            this.label1 = new Label();
            this.comboBox_printer = new ComboBox();
            this.frmPaper = new GroupBox();
            this.txtQulity = new TextBox();
            this.label4 = new Label();
            this.label2 = new Label();
            this.trackBar1 = new TrackBar();
            this.label13 = new Label();
            this.label3 = new Label();
            this.fraPrint = new GroupBox();
            this.panel1 = new Panel();
            this.txbEndPage = new NumericUpDown();
            this.label8 = new Label();
            this.rdoSpecify = new RadioButton();
            this.txbOverlap = new TextBox();
            this.rdoAll = new RadioButton();
            this.label5 = new Label();
            this.txbStartPage = new NumericUpDown();
            this.lblPageCount = new Label();
            this.radioButton1 = new RadioButton();
            this.rdoTitle = new RadioButton();
            this.textBox_copied = new TextBox();
            this.label_copied = new Label();
            this.printDialog_0 = new PrintDialog();
            this.cmdCancel = new Button();
            this.cmdPrint = new Button();
            this.fraPrinter.SuspendLayout();
            this.frmPaper.SuspendLayout();
            this.trackBar1.BeginInit();
            this.fraPrint.SuspendLayout();
            this.panel1.SuspendLayout();
            this.txbEndPage.BeginInit();
            this.txbStartPage.BeginInit();
            base.SuspendLayout();
            this.fraPrinter.BackColor = SystemColors.Control;
            this.fraPrinter.Controls.Add(this.lblPrinterName);
            this.fraPrinter.Controls.Add(this.btnprinterAttribute);
            this.fraPrinter.Controls.Add(this.lblPrinterOrientation);
            this.fraPrinter.Controls.Add(this.lblPrinterName2);
            this.fraPrinter.Controls.Add(this.lblPrinterSize);
            this.fraPrinter.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fraPrinter.ForeColor = SystemColors.ControlText;
            this.fraPrinter.Location = new System.Drawing.Point(8, 8);
            this.fraPrinter.Name = "fraPrinter";
            this.fraPrinter.RightToLeft = RightToLeft.No;
            this.fraPrinter.Size = new Size(0x1f3, 0x70);
            this.fraPrinter.TabIndex = 0x18;
            this.fraPrinter.TabStop = false;
            this.fraPrinter.Text = "打印机信息：";
            this.lblPrinterName.Location = new System.Drawing.Point(0x48, 0x18);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.Size = new Size(0x130, 0x18);
            this.lblPrinterName.TabIndex = 0x1b;
            this.btnprinterAttribute.Location = new System.Drawing.Point(0x1a8, 0x15);
            this.btnprinterAttribute.Name = "btnprinterAttribute";
            this.btnprinterAttribute.Size = new Size(0x43, 0x17);
            this.btnprinterAttribute.TabIndex = 0x1a;
            this.btnprinterAttribute.Text = "属性";
            this.btnprinterAttribute.Click += new EventHandler(this.btnprinterAttribute_Click);
            this.lblPrinterOrientation.BackColor = SystemColors.Control;
            this.lblPrinterOrientation.Cursor = Cursors.Default;
            this.lblPrinterOrientation.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPrinterOrientation.ForeColor = SystemColors.ControlText;
            this.lblPrinterOrientation.Location = new System.Drawing.Point(10, 0x51);
            this.lblPrinterOrientation.Name = "lblPrinterOrientation";
            this.lblPrinterOrientation.RightToLeft = RightToLeft.No;
            this.lblPrinterOrientation.Size = new Size(0xd8, 0x13);
            this.lblPrinterOrientation.TabIndex = 0x18;
            this.lblPrinterOrientation.Text = "纸张方向：";
            this.lblPrinterName2.AutoSize = true;
            this.lblPrinterName2.BackColor = SystemColors.Control;
            this.lblPrinterName2.Cursor = Cursors.Default;
            this.lblPrinterName2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPrinterName2.ForeColor = SystemColors.ControlText;
            this.lblPrinterName2.Location = new System.Drawing.Point(10, 0x1a);
            this.lblPrinterName2.Name = "lblPrinterName2";
            this.lblPrinterName2.RightToLeft = RightToLeft.No;
            this.lblPrinterName2.Size = new Size(0x40, 14);
            this.lblPrinterName2.TabIndex = 3;
            this.lblPrinterName2.Text = "名       称：";
            this.lblPrinterSize.BackColor = SystemColors.Control;
            this.lblPrinterSize.Cursor = Cursors.Default;
            this.lblPrinterSize.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPrinterSize.ForeColor = SystemColors.ControlText;
            this.lblPrinterSize.Location = new System.Drawing.Point(8, 0x37);
            this.lblPrinterSize.Name = "lblPrinterSize";
            this.lblPrinterSize.RightToLeft = RightToLeft.No;
            this.lblPrinterSize.Size = new Size(0xd8, 0x12);
            this.lblPrinterSize.TabIndex = 1;
            this.lblPrinterSize.Text = "纸张大小：";
            this.label1.AutoSize = true;
            this.label1.BackColor = SystemColors.Control;
            this.label1.Cursor = Cursors.Default;
            this.label1.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.ForeColor = SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(0x10, 0x85);
            this.label1.Name = "label1";
            this.label1.RightToLeft = RightToLeft.No;
            this.label1.Size = new Size(0x2e, 14);
            this.label1.TabIndex = 0x19;
            this.label1.Text = "打印机:";
            this.comboBox_printer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_printer.Items.AddRange(new object[] { "Windows打印机", "PostScript打印机" });
            this.comboBox_printer.Location = new System.Drawing.Point(0x44, 0x83);
            this.comboBox_printer.Name = "comboBox_printer";
            this.comboBox_printer.Size = new Size(0x9a, 20);
            this.comboBox_printer.TabIndex = 0x1a;
            this.comboBox_printer.SelectedIndexChanged += new EventHandler(this.comboBox_printer_SelectedIndexChanged);
            this.frmPaper.BackColor = SystemColors.Control;
            this.frmPaper.Controls.Add(this.txtQulity);
            this.frmPaper.Controls.Add(this.label4);
            this.frmPaper.Controls.Add(this.label2);
            this.frmPaper.Controls.Add(this.trackBar1);
            this.frmPaper.Controls.Add(this.label13);
            this.frmPaper.Controls.Add(this.label3);
            this.frmPaper.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.frmPaper.ForeColor = SystemColors.ControlText;
            this.frmPaper.Location = new System.Drawing.Point(0x15, 0x9d);
            this.frmPaper.Name = "frmPaper";
            this.frmPaper.RightToLeft = RightToLeft.No;
            this.frmPaper.Size = new Size(220, 0x7e);
            this.frmPaper.TabIndex = 0x1b;
            this.frmPaper.TabStop = false;
            this.frmPaper.Text = "输出影像品质";
            this.txtQulity.Location = new System.Drawing.Point(0x4c, 0x5d);
            this.txtQulity.Name = "txtQulity";
            this.txtQulity.Size = new Size(0x3e, 20);
            this.txtQulity.TabIndex = 0x2e;
            this.txtQulity.Text = "1";
            this.txtQulity.Leave += new EventHandler(this.txtQulity_Leave);
            this.txtQulity.TextChanged += new EventHandler(this.txtQulity_TextChanged);
            this.label4.AutoSize = true;
            this.label4.BackColor = SystemColors.Control;
            this.label4.Cursor = Cursors.Default;
            this.label4.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label4.ForeColor = SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(0xa9, 0x44);
            this.label4.Name = "label4";
            this.label4.RightToLeft = RightToLeft.No;
            this.label4.Size = new Size(0x13, 14);
            this.label4.TabIndex = 0x2d;
            this.label4.Text = "差";
            this.label2.AutoSize = true;
            this.label2.BackColor = SystemColors.Control;
            this.label2.Cursor = Cursors.Default;
            this.label2.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.ForeColor = SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(0x61, 0x44);
            this.label2.Name = "label2";
            this.label2.RightToLeft = RightToLeft.No;
            this.label2.Size = new Size(0x1f, 14);
            this.label2.TabIndex = 0x2c;
            this.label2.Text = "正常";
            this.trackBar1.Location = new System.Drawing.Point(0x1d, 0x17);
            this.trackBar1.Maximum = 5;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new Size(0xab, 0x2a);
            this.trackBar1.TabIndex = 0x2b;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
            this.label13.AutoSize = true;
            this.label13.BackColor = SystemColors.Control;
            this.label13.Cursor = Cursors.Default;
            this.label13.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label13.ForeColor = SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(6, 0x5d);
            this.label13.Name = "label13";
            this.label13.RightToLeft = RightToLeft.No;
            this.label13.Size = new Size(0x37, 14);
            this.label13.TabIndex = 0x2a;
            this.label13.Text = "比率:    1:";
            this.label3.AutoSize = true;
            this.label3.BackColor = SystemColors.Control;
            this.label3.Cursor = Cursors.Default;
            this.label3.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.ForeColor = SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(0x21, 0x44);
            this.label3.Name = "label3";
            this.label3.RightToLeft = RightToLeft.No;
            this.label3.Size = new Size(0x13, 14);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "好";
            this.fraPrint.BackColor = SystemColors.Control;
            this.fraPrint.Controls.Add(this.panel1);
            this.fraPrint.Controls.Add(this.radioButton1);
            this.fraPrint.Controls.Add(this.rdoTitle);
            this.fraPrint.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fraPrint.ForeColor = SystemColors.ControlText;
            this.fraPrint.Location = new System.Drawing.Point(0x100, 0x85);
            this.fraPrint.Name = "fraPrint";
            this.fraPrint.RightToLeft = RightToLeft.No;
            this.fraPrint.Size = new Size(0xfb, 150);
            this.fraPrint.TabIndex = 0x1c;
            this.fraPrint.TabStop = false;
            this.fraPrint.Text = "打印页设置";
            this.panel1.Controls.Add(this.txbEndPage);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.rdoSpecify);
            this.panel1.Controls.Add(this.txbOverlap);
            this.panel1.Controls.Add(this.rdoAll);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txbStartPage);
            this.panel1.Controls.Add(this.lblPageCount);
            this.panel1.Location = new System.Drawing.Point(0x1b, 0x1f);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xd8, 0x4f);
            this.panel1.TabIndex = 0x1d;
            this.txbEndPage.Enabled = false;
            this.txbEndPage.Location = new System.Drawing.Point(0xa7, 0x1a);
            int[] bits = new int[4];
            bits[0] = 1;
            this.txbEndPage.Minimum = new decimal(bits);
            this.txbEndPage.Name = "txbEndPage";
            this.txbEndPage.Size = new Size(0x2e, 20);
            this.txbEndPage.TabIndex = 30;
            int[] bits2 = new int[4];
            bits2[0] = 1;
            this.txbEndPage.Value = new decimal(bits2);
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0x93, 30);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x13, 14);
            this.label8.TabIndex = 30;
            this.label8.Text = "到";
            this.rdoSpecify.AutoSize = true;
            this.rdoSpecify.Location = new System.Drawing.Point(8, 0x1c);
            this.rdoSpecify.Name = "rdoSpecify";
            this.rdoSpecify.Size = new Size(0x55, 0x12);
            this.rdoSpecify.TabIndex = 0x1a;
            this.rdoSpecify.Text = "指定页，从";
            this.rdoSpecify.UseVisualStyleBackColor = true;
            this.rdoSpecify.CheckedChanged += new EventHandler(this.rdoSpecify_CheckedChanged);
            this.txbOverlap.AcceptsReturn = true;
            this.txbOverlap.BackColor = SystemColors.Window;
            this.txbOverlap.Cursor = Cursors.IBeam;
            this.txbOverlap.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txbOverlap.ForeColor = SystemColors.WindowText;
            this.txbOverlap.Location = new System.Drawing.Point(0x3f, 0x35);
            this.txbOverlap.MaxLength = 0;
            this.txbOverlap.Name = "txbOverlap";
            this.txbOverlap.RightToLeft = RightToLeft.No;
            this.txbOverlap.Size = new Size(0x7a, 20);
            this.txbOverlap.TabIndex = 0x1a;
            this.txbOverlap.Text = "0";
            this.txbOverlap.Leave += new EventHandler(this.txbOverlap_Leave);
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Location = new System.Drawing.Point(8, 5);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new Size(0x31, 0x12);
            this.rdoAll.TabIndex = 0x19;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "所有";
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.CheckedChanged += new EventHandler(this.rdoAll_CheckedChanged);
            this.label5.AutoSize = true;
            this.label5.BackColor = SystemColors.Control;
            this.label5.Cursor = Cursors.Default;
            this.label5.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.ForeColor = SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(11, 0x36);
            this.label5.Name = "label5";
            this.label5.RightToLeft = RightToLeft.No;
            this.label5.Size = new Size(0x2e, 14);
            this.label5.TabIndex = 0x1b;
            this.label5.Text = "重叠度:";
            this.txbStartPage.Enabled = false;
            this.txbStartPage.Location = new System.Drawing.Point(0x61, 0x1b);
            int[] bits3 = new int[4];
            bits3[0] = 1;
            this.txbStartPage.Minimum = new decimal(bits3);
            this.txbStartPage.Name = "txbStartPage";
            this.txbStartPage.Size = new Size(0x2c, 20);
            this.txbStartPage.TabIndex = 0x1d;
            int[] bits4 = new int[4];
            bits4[0] = 1;
            this.txbStartPage.Value = new decimal(bits4);
            this.lblPageCount.BackColor = SystemColors.Control;
            this.lblPageCount.Cursor = Cursors.Default;
            this.lblPageCount.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPageCount.ForeColor = SystemColors.ControlText;
            this.lblPageCount.Location = new System.Drawing.Point(0x4b, 7);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.RightToLeft = RightToLeft.No;
            this.lblPageCount.Size = new Size(0x81, 14);
            this.lblPageCount.TabIndex = 0x18;
            this.lblPageCount.Text = "页数: ";
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 0x74);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x61, 0x12);
            this.radioButton1.TabIndex = 0x1d;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "缩放地图输出";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.rdoTitle.AutoSize = true;
            this.rdoTitle.Checked = true;
            this.rdoTitle.Location = new System.Drawing.Point(12, 0x10);
            this.rdoTitle.Name = "rdoTitle";
            this.rdoTitle.Size = new Size(0x61, 0x12);
            this.rdoTitle.TabIndex = 0x1c;
            this.rdoTitle.TabStop = true;
            this.rdoTitle.Text = "分割地图输出";
            this.rdoTitle.UseVisualStyleBackColor = true;
            this.rdoTitle.CheckedChanged += new EventHandler(this.rdoTitle_CheckedChanged);
            this.textBox_copied.Location = new System.Drawing.Point(0x67, 0x123);
            this.textBox_copied.Name = "textBox_copied";
            this.textBox_copied.Size = new Size(0x7a, 0x15);
            this.textBox_copied.TabIndex = 0x19;
            this.textBox_copied.Text = "1";
            this.textBox_copied.Leave += new EventHandler(this.textBox_copied_Leave);
            this.label_copied.AutoSize = true;
            this.label_copied.Location = new System.Drawing.Point(0x1c, 0x127);
            this.label_copied.Name = "label_copied";
            this.label_copied.Size = new Size(0x4d, 12);
            this.label_copied.TabIndex = 13;
            this.label_copied.Text = "打 印 份 数:";
            this.cmdCancel.Location = new System.Drawing.Point(0x1ab, 0x132);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new Size(0x4b, 0x17);
            this.cmdCancel.TabIndex = 30;
            this.cmdCancel.Text = "取消";
            this.cmdPrint.BackColor = SystemColors.Control;
            this.cmdPrint.Cursor = Cursors.Default;
            this.cmdPrint.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cmdPrint.ForeColor = SystemColors.ControlText;
            this.cmdPrint.Location = new System.Drawing.Point(0x14e, 0x132);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.RightToLeft = RightToLeft.No;
            this.cmdPrint.Size = new Size(0x4b, 0x17);
            this.cmdPrint.TabIndex = 0x1d;
            this.cmdPrint.Text = "打  印";
            this.cmdPrint.UseVisualStyleBackColor = false;
            this.cmdPrint.Click += new EventHandler(this.cmdPrint_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x202, 0x155);
            base.Controls.Add(this.cmdCancel);
            base.Controls.Add(this.cmdPrint);
            base.Controls.Add(this.fraPrint);
            base.Controls.Add(this.frmPaper);
            base.Controls.Add(this.textBox_copied);
            base.Controls.Add(this.comboBox_printer);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label_copied);
            base.Controls.Add(this.fraPrinter);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmPrint";
            this.Text = "打印";
            base.Load += new EventHandler(this.frmPrint_Load);
            this.fraPrinter.ResumeLayout(false);
            this.fraPrinter.PerformLayout();
            this.frmPaper.ResumeLayout(false);
            this.frmPaper.PerformLayout();
            this.trackBar1.EndInit();
            this.fraPrint.ResumeLayout(false);
            this.fraPrint.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.txbEndPage.EndInit();
            this.txbStartPage.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

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
                    grect.bottom = ((int) deviceBounds.YMax) - GetDeviceCaps(num2, 0x71);
                    grect.left = ((int) deviceBounds.XMin) - GetDeviceCaps(num2, 0x70);
                    grect.right = ((int) deviceBounds.XMax) - GetDeviceCaps(num2, 0x70);
                    grect.top = ((int) deviceBounds.YMin) - GetDeviceCaps(num2, 0x71);
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
                    grect.bottom = ((int) deviceBounds.YMax) - GetDeviceCaps(num2, 0x71);
                    grect.left = ((int) deviceBounds.XMin) - GetDeviceCaps(num2, 0x70);
                    grect.right = ((int) deviceBounds.XMax) - GetDeviceCaps(num2, 0x70);
                    grect.top = ((int) deviceBounds.YMin) - GetDeviceCaps(num2, 0x71);
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

