using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class StarndFenFuMapTemplatePage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button btnClear;
        private Button button1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
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
        private Label label18;
        private Label label2;
        private Label label20;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label5;
        private Label label6;
        private Label label7;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0 = null;
        private RadioButton rdoFill;
        private RadioButton rdoLine;
        private StyleButton styleButton1;
        private TextBox txtBottomSpace;
        private TextBox txtLeftSpace;
        private TextBox txtLegendInfo;
        private TextBox txtName;
        private TextBox txtOutBorderWidth;
        private TextBox txtRightSpace;
        private TextBox txtStartX;
        private TextBox txtStartY;
        private TextBox txtTopSpace;
        private TextBox txtXInterval;
        private TextBox txtYInterval;

        public event OnValueChangeEventHandler OnValueChange;

        public StarndFenFuMapTemplatePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.mapTemplate_0.XInterval = double.Parse(this.txtXInterval.Text);
            this.mapTemplate_0.YInterval = double.Parse(this.txtYInterval.Text);
            this.mapTemplate_0.Name = this.txtName.Text;
            if ((this.txtLegendInfo.Text.Length > 0) && File.Exists(this.txtLegendInfo.Text))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.txtLegendInfo.Text);
                this.mapTemplate_0.LegendInfo = document.InnerXml;
            }
            this.mapTemplate_0.BorderSymbol = this.styleButton1.Style as ISymbol;
            this.mapTemplate_0.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
        }

        public bool CanApply()
        {
            try
            {
                double num = 0.0;
                if (double.Parse(this.txtXInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtYInterval.Text) <= 0.0)
                {
                    return false;
                }
                num = double.Parse(this.txtOutBorderWidth.Text);
                if (num < 0.0)
                {
                    return false;
                }
                if (num < 0.0)
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
            this.groupBox4 = new GroupBox();
            this.groupBox5 = new GroupBox();
            this.txtRightSpace = new TextBox();
            this.label25 = new Label();
            this.txtLeftSpace = new TextBox();
            this.label26 = new Label();
            this.txtBottomSpace = new TextBox();
            this.label23 = new Label();
            this.txtTopSpace = new TextBox();
            this.label24 = new Label();
            this.rdoFill = new RadioButton();
            this.rdoLine = new RadioButton();
            this.label18 = new Label();
            this.txtOutBorderWidth = new TextBox();
            this.label20 = new Label();
            this.label16 = new Label();
            this.styleButton1 = new StyleButton();
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
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox4.Controls.Add(this.rdoFill);
            this.groupBox4.Controls.Add(this.rdoLine);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txtOutBorderWidth);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.styleButton1);
            this.groupBox4.Location = new Point(10, 0x8a);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x1d2, 0x60);
            this.groupBox4.TabIndex = 0x59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "边框设置";
            this.groupBox5.Controls.Add(this.txtRightSpace);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.txtLeftSpace);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.txtBottomSpace);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txtTopSpace);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new Point(190, 40);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x11e, 0x5c);
            this.groupBox5.TabIndex = 100;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "内外框间距(厘米)";
            this.txtRightSpace.Location = new Point(0xad, 50);
            this.txtRightSpace.Name = "txtRightSpace";
            this.txtRightSpace.Size = new Size(0x2b, 0x15);
            this.txtRightSpace.TabIndex = 0x10;
            this.txtRightSpace.Text = "1";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0x72, 0x35);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x35, 12);
            this.label25.TabIndex = 15;
            this.label25.Text = "右部间距";
            this.txtLeftSpace.Location = new Point(0x41, 50);
            this.txtLeftSpace.Name = "txtLeftSpace";
            this.txtLeftSpace.Size = new Size(0x2b, 0x15);
            this.txtLeftSpace.TabIndex = 14;
            this.txtLeftSpace.Text = "1";
            this.label26.AutoSize = true;
            this.label26.Location = new Point(6, 0x35);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x35, 12);
            this.label26.TabIndex = 13;
            this.label26.Text = "左部间距";
            this.txtBottomSpace.Location = new Point(0xad, 0x18);
            this.txtBottomSpace.Name = "txtBottomSpace";
            this.txtBottomSpace.Size = new Size(0x2b, 0x15);
            this.txtBottomSpace.TabIndex = 12;
            this.txtBottomSpace.Text = "1";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x72, 0x1b);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x35, 12);
            this.label23.TabIndex = 11;
            this.label23.Text = "底部间距";
            this.txtTopSpace.Location = new Point(0x41, 0x18);
            this.txtTopSpace.Name = "txtTopSpace";
            this.txtTopSpace.Size = new Size(0x2b, 0x15);
            this.txtTopSpace.TabIndex = 10;
            this.txtTopSpace.Text = "1";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(6, 0x1b);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x35, 12);
            this.label24.TabIndex = 9;
            this.label24.Text = "顶部间距";
            this.rdoFill.AutoSize = true;
            this.rdoFill.Location = new Point(0x11c, 0x11);
            this.rdoFill.Name = "rdoFill";
            this.rdoFill.Size = new Size(0x23, 0x10);
            this.rdoFill.TabIndex = 0x16;
            this.rdoFill.Text = "面";
            this.rdoFill.UseVisualStyleBackColor = true;
            this.rdoFill.CheckedChanged += new EventHandler(this.rdoFill_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Checked = true;
            this.rdoLine.Location = new Point(0xe2, 0x11);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(0x23, 0x10);
            this.rdoLine.TabIndex = 0x15;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x85, 0x11);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x1d, 12);
            this.label18.TabIndex = 20;
            this.label18.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(0x57, 14);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(0x2b, 0x15);
            this.txtOutBorderWidth.TabIndex = 0x13;
            this.txtOutBorderWidth.Text = "1";
            this.txtOutBorderWidth.TextChanged += new EventHandler(this.txtOutBorderWidth_TextChanged);
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0x10, 0x11);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x29, 12);
            this.label20.TabIndex = 0x12;
            this.label20.Text = "外框宽";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0xe0, 0x34);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x1d, 12);
            this.label16.TabIndex = 10;
            this.label16.Text = "符号";
            this.styleButton1.Location = new Point(0x10f, 0x2a);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(0x60, 0x1f);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 9;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtXInterval);
            this.groupBox3.Controls.Add(this.txtYInterval);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new Point(9, 40);
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
            this.btnClear.Location = new Point(0x1b5, 0x100);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x2d, 0x13);
            this.btnClear.TabIndex = 0x60;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.button1.Location = new Point(0x18d, 0x100);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x22, 0x13);
            this.button1.TabIndex = 0x5f;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.txtLegendInfo.Location = new Point(0x6b, 0x100);
            this.txtLegendInfo.Name = "txtLegendInfo";
            this.txtLegendInfo.ReadOnly = true;
            this.txtLegendInfo.Size = new Size(270, 0x15);
            this.txtLegendInfo.TabIndex = 0x5e;
            this.txtLegendInfo.Visible = false;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x18, 0x103);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x4d, 12);
            this.label15.TabIndex = 0x5d;
            this.label15.Text = "图例配置文件";
            this.label15.Visible = false;
            this.txtName.Location = new Point(0x31, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x1ab, 0x15);
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
            this.groupBox2.Location = new Point(380, 0x12e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xad, 0x5c);
            this.groupBox2.TabIndex = 0x63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "左下角坐标基数";
            this.groupBox2.Visible = false;
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
            base.Controls.Add(this.groupBox5);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.txtLegendInfo);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox4);
            base.Name = "StarndFenFuMapTemplatePage";
            base.Size = new Size(490, 0x128);
            base.Load += new EventHandler(this.StarndFenFuMapTemplatePage_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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

        private void method_0(object sender, EventArgs e)
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

        private void method_1(object sender, EventArgs e)
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

        private void method_2(object sender, EventArgs e)
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

        private void method_3(object sender, EventArgs e)
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

        private void method_4(object sender, EventArgs e)
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
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
        }

        private void StarndFenFuMapTemplatePage_Load(object sender, EventArgs e)
        {
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.mapTemplate_0 != null)
            {
                this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
                this.txtName.Text = this.mapTemplate_0.Name;
                this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
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
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
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

