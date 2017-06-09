using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class MapTemplateGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button btnClear;
        private Button button1;
        private CheckBox chkMapGrid;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private MapTemplate mapTemplate_0 = null;
        private RadioButton rdoFill;
        private RadioButton rdoLine;
        private StyleButton styleButton1;
        private TextBox textBox2;
        private TextBox txtHeight;
        private TextBox txtLengendInfo;
        private TextBox txtName;
        private TextBox txtOutBorderWidth;
        private TextBox txtScale;
        private TextBox txtStartX;
        private TextBox txtStartY;
        private TextBox txtWidth;
        private TextBox txtXInterval;
        private TextBox txtYInterval;

        public event OnValueChangeEventHandler OnValueChange;

        public MapTemplateGeneralPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.mapTemplate_0.Width = double.Parse(this.txtWidth.Text);
                this.mapTemplate_0.Height = double.Parse(this.txtHeight.Text);
                this.mapTemplate_0.Scale = double.Parse(this.txtScale.Text);
                this.mapTemplate_0.StartX = double.Parse(this.txtStartX.Text);
                this.mapTemplate_0.StartY = double.Parse(this.txtStartY.Text);
                this.mapTemplate_0.XInterval = double.Parse(this.txtXInterval.Text);
                this.mapTemplate_0.YInterval = double.Parse(this.txtYInterval.Text);
                this.mapTemplate_0.Name = this.txtName.Text;
                if ((this.txtLengendInfo.Text.Length > 0) && File.Exists(this.txtLengendInfo.Text))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(this.txtLengendInfo.Text);
                    this.mapTemplate_0.LegendInfo = document.InnerXml;
                }
                this.mapTemplate_0.InOutSpace = double.Parse(this.textBox2.Text);
                this.mapTemplate_0.BorderSymbol = this.styleButton1.Style as ISymbol;
                this.mapTemplate_0.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtLengendInfo.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtLengendInfo.Text = dialog.FileName;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                this.bool_1 = true;
            }
        }

        public bool CanApply()
        {
            try
            {
                if (double.Parse(this.txtWidth.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtHeight.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtScale.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtXInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtYInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtOutBorderWidth.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.textBox2.Text) < 0.0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.txtWidth = new TextBox();
            this.txtHeight = new TextBox();
            this.label2 = new Label();
            this.groupBox2 = new GroupBox();
            this.label13 = new Label();
            this.label12 = new Label();
            this.txtStartY = new TextBox();
            this.txtStartX = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox3 = new GroupBox();
            this.label11 = new Label();
            this.label10 = new Label();
            this.txtYInterval = new TextBox();
            this.txtXInterval = new TextBox();
            this.label5 = new Label();
            this.label6 = new Label();
            this.txtScale = new TextBox();
            this.label7 = new Label();
            this.label14 = new Label();
            this.txtName = new TextBox();
            this.button1 = new Button();
            this.txtLengendInfo = new TextBox();
            this.label15 = new Label();
            this.btnClear = new Button();
            this.groupBox4 = new GroupBox();
            this.rdoFill = new RadioButton();
            this.rdoLine = new RadioButton();
            this.label18 = new Label();
            this.txtOutBorderWidth = new TextBox();
            this.label20 = new Label();
            this.label16 = new Label();
            this.styleButton1 = new StyleButton();
            this.label17 = new Label();
            this.textBox2 = new TextBox();
            this.label19 = new Label();
            this.chkMapGrid = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高";
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(14, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x9b, 0x5c);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "页面尺寸";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x77, 0x37);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "厘米";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x77, 0x1b);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "厘米";
            this.txtWidth.Location = new Point(0x27, 0x34);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(0x4a, 0x15);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.Text = "50";
            this.txtHeight.Location = new Point(0x26, 0x18);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(0x4b, 0x15);
            this.txtHeight.TabIndex = 2;
            this.txtHeight.Text = "50";
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 0x37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽";
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtStartY);
            this.groupBox2.Controls.Add(this.txtStartX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new Point(0xaf, 0x94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x9b, 0x5c);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "左小角坐标基数";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x84, 0x37);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "米";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x83, 0x1b);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "米";
            this.txtStartY.Location = new Point(0x3e, 0x34);
            this.txtStartY.Name = "txtStartY";
            this.txtStartY.Size = new Size(0x40, 0x15);
            this.txtStartY.TabIndex = 3;
            this.txtStartY.Text = "0";
            this.txtStartY.TextChanged += new EventHandler(this.txtStartY_TextChanged);
            this.txtStartX.Location = new Point(0x3e, 0x18);
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.Size = new Size(0x40, 0x15);
            this.txtStartX.TabIndex = 2;
            this.txtStartX.Text = "0";
            this.txtStartX.TextChanged += new EventHandler(this.txtStartX_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(15, 0x37);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "横坐标";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(15, 0x1b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "纵坐标";
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtYInterval);
            this.groupBox3.Controls.Add(this.txtXInterval);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new Point(0xaf, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x9b, 0x5c);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "坐标轴间隔";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(120, 0x37);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x1d, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "厘米";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(120, 0x1b);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "厘米";
            this.txtYInterval.Location = new Point(0x3e, 0x34);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new Size(0x34, 0x15);
            this.txtYInterval.TabIndex = 3;
            this.txtYInterval.Text = "10";
            this.txtYInterval.TextChanged += new EventHandler(this.txtYInterval_TextChanged);
            this.txtXInterval.Location = new Point(0x3e, 0x18);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new Size(0x34, 0x15);
            this.txtXInterval.TabIndex = 2;
            this.txtXInterval.Text = "10";
            this.txtXInterval.TextChanged += new EventHandler(this.txtXInterval_TextChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(15, 0x37);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "横坐标";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(15, 0x1b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "纵坐标";
            this.txtScale.Location = new Point(0xeb, 0xfd);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(100, 0x15);
            this.txtScale.TabIndex = 5;
            this.txtScale.Text = "500";
            this.txtScale.TextChanged += new EventHandler(this.txtScale_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xb2, 0x100);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "比例尺1:";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(12, 13);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "名称";
            this.txtName.Location = new Point(0x35, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xf8, 0x15);
            this.txtName.TabIndex = 7;
            this.txtName.Text = "模板";
            this.txtName.TextChanged += new EventHandler(this.txtName_TextChanged);
            this.button1.Location = new Point(0xff, 0x121);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x22, 0x13);
            this.button1.TabIndex = 0x56;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.txtLengendInfo.Location = new Point(0x5f, 0x121);
            this.txtLengendInfo.Name = "txtLengendInfo";
            this.txtLengendInfo.ReadOnly = true;
            this.txtLengendInfo.Size = new Size(0x9a, 0x15);
            this.txtLengendInfo.TabIndex = 0x55;
            this.txtLengendInfo.Visible = false;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(12, 0x124);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x4d, 12);
            this.label15.TabIndex = 0x54;
            this.label15.Text = "图例配置文件";
            this.label15.Visible = false;
            this.btnClear.Location = new Point(0x127, 0x121);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x2d, 0x13);
            this.btnClear.TabIndex = 0x57;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.groupBox4.Controls.Add(this.rdoFill);
            this.groupBox4.Controls.Add(this.rdoLine);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txtOutBorderWidth);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.styleButton1);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Location = new Point(14, 0x94);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x9b, 0x87);
            this.groupBox4.TabIndex = 0x58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "外框设置";
            this.rdoFill.AutoSize = true;
            this.rdoFill.Location = new Point(0x4b, 0x4c);
            this.rdoFill.Name = "rdoFill";
            this.rdoFill.Size = new Size(0x23, 0x10);
            this.rdoFill.TabIndex = 15;
            this.rdoFill.Text = "面";
            this.rdoFill.UseVisualStyleBackColor = true;
            this.rdoFill.CheckedChanged += new EventHandler(this.rdoFill_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Checked = true;
            this.rdoLine.Location = new Point(0x11, 0x4c);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(0x23, 0x10);
            this.rdoLine.TabIndex = 14;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x79, 0x34);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x1d, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(0x4b, 0x31);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(0x2b, 0x15);
            this.txtOutBorderWidth.TabIndex = 12;
            this.txtOutBorderWidth.Text = "1";
            this.txtOutBorderWidth.TextChanged += new EventHandler(this.txtOutBorderWidth_TextChanged);
            this.label20.AutoSize = true;
            this.label20.Location = new Point(4, 0x34);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x29, 12);
            this.label20.TabIndex = 11;
            this.label20.Text = "外框宽";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(6, 0x6c);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x1d, 12);
            this.label16.TabIndex = 10;
            this.label16.Text = "符号";
            this.styleButton1.Location = new Point(0x35, 0x62);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(0x60, 0x1f);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 9;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x7b, 0x1b);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1d, 12);
            this.label17.TabIndex = 8;
            this.label17.Text = "厘米";
            this.textBox2.Location = new Point(0x4d, 0x18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x2b, 0x15);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "1";
            this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
            this.label19.AutoSize = true;
            this.label19.Location = new Point(6, 0x1b);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x41, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "与内框间隔";
            this.chkMapGrid.AutoSize = true;
            this.chkMapGrid.Location = new Point(14, 0x120);
            this.chkMapGrid.Name = "chkMapGrid";
            this.chkMapGrid.Size = new Size(0x48, 0x10);
            this.chkMapGrid.TabIndex = 0x59;
            this.chkMapGrid.Text = "使用格网";
            this.chkMapGrid.UseVisualStyleBackColor = true;
            base.Controls.Add(this.chkMapGrid);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.txtLengendInfo);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.txtScale);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox3);
            base.Name = "MapTemplateGeneralPage";
            base.Size = new Size(0x187, 0x159);
            base.Load += new EventHandler(this.MapTemplateGeneralPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void MapTemplateGeneralPage_Load(object sender, EventArgs e)
        {
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.mapTemplate_0 != null)
            {
                this.txtWidth.Text = this.mapTemplate_0.Width.ToString();
                this.txtHeight.Text = this.mapTemplate_0.Height.ToString();
                this.txtScale.Text = this.mapTemplate_0.Scale.ToString();
                this.txtStartX.Text = this.mapTemplate_0.StartX.ToString();
                this.txtStartY.Text = this.mapTemplate_0.StartY.ToString();
                this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
                this.txtName.Text = this.mapTemplate_0.Name;
                this.textBox2.Text = this.mapTemplate_0.InOutSpace.ToString();
                this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                if (this.mapTemplate_0.BorderSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                    this.ilineSymbol_0 = this.mapTemplate_0.BorderSymbol as ILineSymbol;
                }
                if (this.mapTemplate_0.BorderSymbol is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.mapTemplate_0.BorderSymbol as IFillSymbol;
                    this.rdoFill.Checked = true;
                }
            }
            this.bool_0 = true;
        }

        private void rdoFill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoFill.Checked)
            {
                if (this.styleButton1.Style is ILineSymbol)
                {
                    this.ilineSymbol_0 = this.styleButton1.Style as ILineSymbol;
                }
                this.styleButton1.Style = this.ifillSymbol_0;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                if (this.styleButton1.Style is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                }
                this.styleButton1.Style = this.ilineSymbol_0;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetSymbol(this.styleButton1.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.styleButton1.Style = selector.GetSymbol();
                    if (this.rdoLine.Checked)
                    {
                        this.ilineSymbol_0 = this.styleButton1.Style as ILineSymbol;
                    }
                    else
                    {
                        this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                    }
                    this.bool_1 = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtOutBorderWidth_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtScale_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtStartX_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtStartY_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtXInterval_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtYInterval_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public MapTemplate MapTemplate
        {
            get
            {
                return this.mapTemplate_0;
            }
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "常规";
            }
            set
            {
            }
        }

        public bool UseMapGrid
        {
            get
            {
                return this.chkMapGrid.Checked;
            }
            set
            {
                this.chkMapGrid.Visible = false;
            }
        }
    }
}

