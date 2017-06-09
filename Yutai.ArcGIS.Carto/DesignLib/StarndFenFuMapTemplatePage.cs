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
    public class StarndFenFuMapTemplatePage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button btnClear;
        private Button button1;
        private ComboBox cboDataum;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupSR;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private Label label1;
        private Label label10;
        private Label label11;
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
        private MapTemplate mapTemplate_0 = null;
        private RadioButton rdo3;
        private RadioButton rdo6;
        private RadioButton rdoFill;
        private RadioButton rdoLine;
        private StyleButton styleButton1;
        private TextBox textBox2;
        private TextBox txtLegendInfo;
        private TextBox txtName;
        private TextBox txtOutBorderWidth;
        private TextBox txtStartX;
        private TextBox txtStartY;
        private TextBox txtXInterval;
        private TextBox txtYInterval;
        private TextBox txtYOffset;

        public event OnValueChangeEventHandler OnValueChange;

        public StarndFenFuMapTemplatePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.mapTemplate_0.StartX = double.Parse(this.txtStartX.Text);
            this.mapTemplate_0.StartY = double.Parse(this.txtStartY.Text);
            this.mapTemplate_0.XInterval = double.Parse(this.txtXInterval.Text);
            this.mapTemplate_0.YInterval = double.Parse(this.txtYInterval.Text);
            this.mapTemplate_0.Name = this.txtName.Text;
            if ((this.txtLegendInfo.Text.Length > 0) && File.Exists(this.txtLegendInfo.Text))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.txtLegendInfo.Text);
                this.mapTemplate_0.LegendInfo = document.InnerXml;
            }
            this.mapTemplate_0.InOutSpace = double.Parse(this.textBox2.Text);
            this.mapTemplate_0.BorderSymbol = this.styleButton1.Style as ISymbol;
            this.mapTemplate_0.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
            this.mapTemplate_0.SpheroidType = (this.cboDataum.SelectedIndex == 0) ? SpheroidType.Beijing54 : SpheroidType.Xian1980;
            if (this.rdo3.Checked)
            {
                this.mapTemplate_0.StripType = StripType.STThreeDeg;
            }
            else
            {
                this.mapTemplate_0.StripType = StripType.STSixDeg;
            }
        }

        public bool CanApply()
        {
            try
            {
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

        private void cboDataum_SelectedIndexChanged(object sender, EventArgs e)
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
            this.groupSR = new GroupBox();
            this.cboDataum = new ComboBox();
            this.txtYOffset = new TextBox();
            this.rdo6 = new RadioButton();
            this.rdo3 = new RadioButton();
            this.label4 = new Label();
            this.label3 = new Label();
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
            this.groupBox3 = new GroupBox();
            this.label11 = new Label();
            this.label10 = new Label();
            this.txtXInterval = new TextBox();
            this.txtYInterval = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnClear = new Button();
            this.button1 = new Button();
            this.txtLegendInfo = new TextBox();
            this.label15 = new Label();
            this.txtName = new TextBox();
            this.label14 = new Label();
            this.groupBox2 = new GroupBox();
            this.label13 = new Label();
            this.label5 = new Label();
            this.txtStartY = new TextBox();
            this.txtStartX = new TextBox();
            this.label6 = new Label();
            this.label7 = new Label();
            this.groupSR.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupSR.Controls.Add(this.cboDataum);
            this.groupSR.Controls.Add(this.txtYOffset);
            this.groupSR.Controls.Add(this.rdo6);
            this.groupSR.Controls.Add(this.rdo3);
            this.groupSR.Controls.Add(this.label4);
            this.groupSR.Controls.Add(this.label3);
            this.groupSR.Location = new Point(8, 0x27);
            this.groupSR.Name = "groupSR";
            this.groupSR.Size = new Size(0x9b, 0x8a);
            this.groupSR.TabIndex = 0x17;
            this.groupSR.TabStop = false;
            this.groupSR.Text = "投影坐标系统参数";
            this.cboDataum.FormattingEnabled = true;
            this.cboDataum.Items.AddRange(new object[] { "北京54", "西安80" });
            this.cboDataum.Location = new Point(0x48, 0x1d);
            this.cboDataum.Name = "cboDataum";
            this.cboDataum.Size = new Size(0x4e, 20);
            this.cboDataum.TabIndex = 5;
            this.cboDataum.Text = "西安80";
            this.cboDataum.SelectedIndexChanged += new EventHandler(this.cboDataum_SelectedIndexChanged);
            this.txtYOffset.Location = new Point(0x48, 0x37);
            this.txtYOffset.Name = "txtYOffset";
            this.txtYOffset.Size = new Size(0x4e, 0x15);
            this.txtYOffset.TabIndex = 4;
            this.txtYOffset.Visible = false;
            this.txtYOffset.TextChanged += new EventHandler(this.txtYOffset_TextChanged);
            this.rdo6.AutoSize = true;
            this.rdo6.Location = new Point(0x4d, 0x61);
            this.rdo6.Name = "rdo6";
            this.rdo6.Size = new Size(0x41, 0x10);
            this.rdo6.TabIndex = 3;
            this.rdo6.Text = "6度分带";
            this.rdo6.UseVisualStyleBackColor = true;
            this.rdo6.CheckedChanged += new EventHandler(this.rdo6_CheckedChanged);
            this.rdo3.AutoSize = true;
            this.rdo3.Checked = true;
            this.rdo3.Location = new Point(13, 0x61);
            this.rdo3.Name = "rdo3";
            this.rdo3.Size = new Size(0x41, 0x10);
            this.rdo3.TabIndex = 2;
            this.rdo3.TabStop = true;
            this.rdo3.Text = "3度分带";
            this.rdo3.UseVisualStyleBackColor = true;
            this.rdo3.CheckedChanged += new EventHandler(this.rdo3_CheckedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(9, 0x3d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "坐标偏移:";
            this.label4.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 0x20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "参考椭球:";
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
            this.groupBox4.Location = new Point(0xa9, 0x30);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xaf, 0x83);
            this.groupBox4.TabIndex = 0x59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "边框设置";
            this.rdoFill.AutoSize = true;
            this.rdoFill.Location = new Point(0x4d, 0x4b);
            this.rdoFill.Name = "rdoFill";
            this.rdoFill.Size = new Size(0x23, 0x10);
            this.rdoFill.TabIndex = 0x16;
            this.rdoFill.Text = "面";
            this.rdoFill.UseVisualStyleBackColor = true;
            this.rdoFill.CheckedChanged += new EventHandler(this.rdoFill_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Checked = true;
            this.rdoLine.Location = new Point(0x13, 0x4b);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(0x23, 0x10);
            this.rdoLine.TabIndex = 0x15;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x7b, 0x33);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x1d, 12);
            this.label18.TabIndex = 20;
            this.label18.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(0x4d, 0x30);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(0x2b, 0x15);
            this.txtOutBorderWidth.TabIndex = 0x13;
            this.txtOutBorderWidth.Text = "1";
            this.txtOutBorderWidth.TextChanged += new EventHandler(this.txtOutBorderWidth_TextChanged);
            this.label20.AutoSize = true;
            this.label20.Location = new Point(6, 0x33);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x29, 12);
            this.label20.TabIndex = 0x12;
            this.label20.Text = "外框宽";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(12, 0x68);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x1d, 12);
            this.label16.TabIndex = 10;
            this.label16.Text = "符号";
            this.styleButton1.Location = new Point(0x3b, 0x5e);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(0x60, 0x1f);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 9;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x7b, 0x17);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1d, 12);
            this.label17.TabIndex = 8;
            this.label17.Text = "厘米";
            this.textBox2.Location = new Point(0x4d, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x2b, 0x15);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "1";
            this.textBox2.TextChanged += new EventHandler(this.textBox2_TextChanged);
            this.label19.AutoSize = true;
            this.label19.Location = new Point(6, 0x17);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x41, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "与内框间隔";
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtXInterval);
            this.groupBox3.Controls.Add(this.txtYInterval);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new Point(0xa9, 0xb5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xaf, 0x5c);
            this.groupBox3.TabIndex = 90;
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
            this.txtXInterval.Location = new Point(0x3e, 0x34);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new Size(0x34, 0x15);
            this.txtXInterval.TabIndex = 3;
            this.txtXInterval.Text = "10";
            this.txtXInterval.TextChanged += new EventHandler(this.txtXInterval_TextChanged);
            this.txtYInterval.Location = new Point(0x3e, 0x18);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new Size(0x34, 0x15);
            this.txtYInterval.TabIndex = 2;
            this.txtYInterval.Text = "10";
            this.txtYInterval.TextChanged += new EventHandler(this.txtYInterval_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x37);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "横坐标";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 0x1b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "纵坐标";
            this.btnClear.Location = new Point(0x12b, 0x128);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x2d, 0x13);
            this.btnClear.TabIndex = 0x60;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.button1.Location = new Point(0x103, 0x128);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x22, 0x13);
            this.button1.TabIndex = 0x5f;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.txtLegendInfo.Location = new Point(0x5c, 0x128);
            this.txtLegendInfo.Name = "txtLegendInfo";
            this.txtLegendInfo.ReadOnly = true;
            this.txtLegendInfo.Size = new Size(0x9a, 0x15);
            this.txtLegendInfo.TabIndex = 0x5e;
            this.txtLegendInfo.Visible = false;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(9, 0x12b);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x4d, 12);
            this.label15.TabIndex = 0x5d;
            this.label15.Text = "图例配置文件";
            this.label15.Visible = false;
            this.txtName.Location = new Point(0x31, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xf8, 0x15);
            this.txtName.TabIndex = 0x62;
            this.txtName.Text = "模板";
            this.txtName.TextChanged += new EventHandler(this.txtName_TextChanged);
            this.label14.AutoSize = true;
            this.label14.Location = new Point(8, 12);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 12);
            this.label14.TabIndex = 0x61;
            this.label14.Text = "名称";
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtStartY);
            this.groupBox2.Controls.Add(this.txtStartX);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new Point(10, 0xb5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x9b, 0x5c);
            this.groupBox2.TabIndex = 0x63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "左小角坐标基数";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x84, 0x37);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x83, 0x1b);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "米";
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
            this.label6.AutoSize = true;
            this.label6.Location = new Point(15, 0x37);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x29, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "横坐标";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(15, 0x1b);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "纵坐标";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.txtLegendInfo);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupSR);
            base.Name = "StarndFenFuMapTemplatePage";
            base.Size = new Size(0x161, 0x148);
            base.Load += new EventHandler(this.StarndFenFuMapTemplatePage_Load);
            this.groupSR.ResumeLayout(false);
            this.groupSR.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void rdo3_CheckedChanged(object sender, EventArgs e)
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

        private void rdo6_CheckedChanged(object sender, EventArgs e)
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

        private void StarndFenFuMapTemplatePage_Load(object sender, EventArgs e)
        {
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.mapTemplate_0 != null)
            {
                this.txtStartX.Text = this.mapTemplate_0.StartX.ToString();
                this.txtStartY.Text = this.mapTemplate_0.StartY.ToString();
                this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
                this.txtName.Text = this.mapTemplate_0.Name;
                this.textBox2.Text = this.mapTemplate_0.InOutSpace.ToString();
                this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                this.cboDataum.SelectedIndex = (this.mapTemplate_0.SpheroidType == SpheroidType.Beijing54) ? 0 : 1;
                if (this.mapTemplate_0.StripType == StripType.STThreeDeg)
                {
                    this.rdo3.Checked = true;
                }
                else
                {
                    this.rdo6.Checked = true;
                }
                if (this.mapTemplate_0.BorderSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                    this.ilineSymbol_0 = this.mapTemplate_0.BorderSymbol as ILineSymbol;
                }
                if (this.mapTemplate_0.BorderSymbol is IFillSymbol)
                {
                    this.rdoFill.Checked = true;
                    this.ifillSymbol_0 = this.mapTemplate_0.BorderSymbol as IFillSymbol;
                }
            }
            this.bool_0 = true;
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

        private void txtYOffset_TextChanged(object sender, EventArgs e)
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
    }
}

