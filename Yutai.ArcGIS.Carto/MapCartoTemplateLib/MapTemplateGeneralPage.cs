using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBox cboStyle;
        private CheckBox checkBox1;
        private CheckBox chkMapGrid;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBoxPage;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0;
        private ILineSymbol ilineSymbol_0;
        [CompilerGenerated]
        private IMapFrame imapFrame_0;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label14;
        private Label label17;
        private Label label18;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0 = null;
        private Panel panelScale;
        private Panel panelTKYS;
        private RadioButton rdoFill;
        private RadioButton rdoLine;
        private RadioButton rdoNoOutLine;
        private RadioButton rdoRect;
        private RadioButton rdoTrapezoid;
        private StyleButton styleButton1;
        private TextBox txtBottomSpace;
        private TextBox txtHeight;
        private TextBox txtLeftSpace;
        private TextBox txtName;
        private TextBox txtOutBorderWidth;
        private TextBox txtRightSpace;
        private TextBox txtScale;
        private TextBox txtTopSpace;
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
                if (this.rdoRect.Checked)
                {
                    this.mapTemplate_0.MapFrameType = MapFrameType.MFTRect;
                }
                else
                {
                    this.mapTemplate_0.MapFrameType = MapFrameType.MFTTrapezoid;
                }
                this.mapTemplate_0.NewMapFrameTypeVal = this.mapTemplate_0.MapFrameType;
                this.mapTemplate_0.XInterval = double.Parse(this.txtXInterval.Text);
                this.mapTemplate_0.YInterval = double.Parse(this.txtYInterval.Text);
                this.mapTemplate_0.Name = this.txtName.Text;
                this.mapTemplate_0.LeftInOutSpace = double.Parse(this.txtLeftSpace.Text);
                this.mapTemplate_0.RightInOutSpace = double.Parse(this.txtRightSpace.Text);
                this.mapTemplate_0.TopInOutSpace = double.Parse(this.txtTopSpace.Text);
                this.mapTemplate_0.BottomInOutSpace = double.Parse(this.txtBottomSpace.Text);
                this.mapTemplate_0.BorderSymbol = this.styleButton1.Style as ISymbol;
                this.mapTemplate_0.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
                if (this.chkMapGrid.Checked)
                {
                    this.method_0();
                }
                else
                {
                    this.mapTemplate_0.MapGrid = null;
                }
                this.mapTemplate_0.FixedWidthAndBottomSpace = this.checkBox1.Checked;
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
                if (double.Parse(this.txtBottomSpace.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtTopSpace.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtLeftSpace.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtRightSpace.Text) < 0.0)
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

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboStyle.SelectedIndex == 0)
            {
                this.groupBoxPage.Visible = true;
                this.groupBoxPage.Enabled = true;
            }
            else if (this.cboStyle.SelectedIndex == 1)
            {
                this.groupBoxPage.Visible = true;
                this.groupBoxPage.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void chkMapGrid_CheckedChanged(object sender, EventArgs e)
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
            this.label1 = new Label();
            this.groupBoxPage = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.txtWidth = new TextBox();
            this.txtHeight = new TextBox();
            this.label2 = new Label();
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
            this.groupBox4 = new GroupBox();
            this.checkBox1 = new CheckBox();
            this.groupBox6 = new GroupBox();
            this.rdoNoOutLine = new RadioButton();
            this.rdoLine = new RadioButton();
            this.styleButton1 = new StyleButton();
            this.rdoFill = new RadioButton();
            this.groupBox5 = new GroupBox();
            this.txtRightSpace = new TextBox();
            this.label25 = new Label();
            this.txtLeftSpace = new TextBox();
            this.label26 = new Label();
            this.txtBottomSpace = new TextBox();
            this.label23 = new Label();
            this.txtTopSpace = new TextBox();
            this.label24 = new Label();
            this.label21 = new Label();
            this.label18 = new Label();
            this.txtOutBorderWidth = new TextBox();
            this.label20 = new Label();
            this.chkMapGrid = new CheckBox();
            this.cboStyle = new ComboBox();
            this.panelTKYS = new Panel();
            this.rdoTrapezoid = new RadioButton();
            this.rdoRect = new RadioButton();
            this.label17 = new Label();
            this.panelScale = new Panel();
            this.groupBoxPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panelTKYS.SuspendLayout();
            this.panelScale.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高";
            this.groupBoxPage.Controls.Add(this.label9);
            this.groupBoxPage.Controls.Add(this.label8);
            this.groupBoxPage.Controls.Add(this.txtWidth);
            this.groupBoxPage.Controls.Add(this.txtHeight);
            this.groupBoxPage.Controls.Add(this.label2);
            this.groupBoxPage.Controls.Add(this.label1);
            this.groupBoxPage.Location = new Point(14, 0x44);
            this.groupBoxPage.Name = "groupBoxPage";
            this.groupBoxPage.Size = new Size(0x9b, 0x69);
            this.groupBoxPage.TabIndex = 1;
            this.groupBoxPage.TabStop = false;
            this.groupBoxPage.Text = "页面尺寸";
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
            this.txtWidth.TextChanged += new EventHandler(this.txtTopSpace_TextChanged);
            this.txtHeight.Location = new Point(0x26, 0x18);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(0x4b, 0x15);
            this.txtHeight.TabIndex = 2;
            this.txtHeight.Text = "50";
            this.txtHeight.TextChanged += new EventHandler(this.txtTopSpace_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 0x37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽";
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtYInterval);
            this.groupBox3.Controls.Add(this.txtXInterval);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new Point(14, 0xb3);
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
            this.txtScale.Location = new Point(0x3e, 4);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(100, 0x15);
            this.txtScale.TabIndex = 5;
            this.txtScale.Text = "500";
            this.txtScale.TextChanged += new EventHandler(this.txtScale_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(5, 7);
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
            this.txtName.Size = new Size(0x184, 0x15);
            this.txtName.TabIndex = 7;
            this.txtName.Text = "模板";
            this.txtName.TextChanged += new EventHandler(this.txtName_TextChanged);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txtOutBorderWidth);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Location = new Point(0xaf, 0x44);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x114, 0xf2);
            this.groupBox4.TabIndex = 0x58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "外框设置";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(14, 0x67);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0xa8, 0x10);
            this.checkBox1.TabIndex = 0x11;
            this.checkBox1.Text = "底部间距和页面宽度成比例";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.groupBox6.Controls.Add(this.rdoNoOutLine);
            this.groupBox6.Controls.Add(this.rdoLine);
            this.groupBox6.Controls.Add(this.styleButton1);
            this.groupBox6.Controls.Add(this.rdoFill);
            this.groupBox6.Location = new Point(14, 0x98);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(200, 0x4f);
            this.groupBox6.TabIndex = 0x5b;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "外框符号";
            this.rdoNoOutLine.AutoSize = true;
            this.rdoNoOutLine.Location = new Point(0x6d, 20);
            this.rdoNoOutLine.Name = "rdoNoOutLine";
            this.rdoNoOutLine.Size = new Size(0x23, 0x10);
            this.rdoNoOutLine.TabIndex = 0x10;
            this.rdoNoOutLine.Text = "无";
            this.rdoNoOutLine.UseVisualStyleBackColor = true;
            this.rdoNoOutLine.CheckedChanged += new EventHandler(this.rdoNoOutLine_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Checked = true;
            this.rdoLine.Location = new Point(9, 0x15);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(0x23, 0x10);
            this.rdoLine.TabIndex = 14;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            this.styleButton1.Location = new Point(6, 0x2a);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(0x60, 0x1f);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 9;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.rdoFill.AutoSize = true;
            this.rdoFill.Location = new Point(0x39, 20);
            this.rdoFill.Name = "rdoFill";
            this.rdoFill.Size = new Size(0x23, 0x10);
            this.rdoFill.TabIndex = 15;
            this.rdoFill.Text = "面";
            this.rdoFill.UseVisualStyleBackColor = true;
            this.rdoFill.CheckedChanged += new EventHandler(this.rdoFill_CheckedChanged);
            this.groupBox5.Controls.Add(this.txtRightSpace);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.txtLeftSpace);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.txtBottomSpace);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txtTopSpace);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(260, 0x4c);
            this.groupBox5.TabIndex = 90;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "内外框间距(厘米)";
            this.txtRightSpace.Location = new Point(0xad, 50);
            this.txtRightSpace.Name = "txtRightSpace";
            this.txtRightSpace.Size = new Size(0x2b, 0x15);
            this.txtRightSpace.TabIndex = 0x10;
            this.txtRightSpace.Text = "1";
            this.txtRightSpace.TextChanged += new EventHandler(this.txtTopSpace_TextChanged);
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
            this.txtLeftSpace.TextChanged += new EventHandler(this.txtTopSpace_TextChanged);
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
            this.txtBottomSpace.TextChanged += new EventHandler(this.txtTopSpace_TextChanged);
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
            this.txtTopSpace.TextChanged += new EventHandler(this.txtTopSpace_TextChanged);
            this.label24.AutoSize = true;
            this.label24.Location = new Point(6, 0x1b);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x35, 12);
            this.label24.TabIndex = 9;
            this.label24.Text = "顶部间距";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x113, 0x27);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x1d, 12);
            this.label21.TabIndex = 0x12;
            this.label21.Text = "厘米";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x81, 0x85);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x1d, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(0x4a, 0x7f);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(0x2b, 0x15);
            this.txtOutBorderWidth.TabIndex = 12;
            this.txtOutBorderWidth.Text = "1";
            this.txtOutBorderWidth.TextChanged += new EventHandler(this.txtOutBorderWidth_TextChanged);
            this.label20.AutoSize = true;
            this.label20.Location = new Point(12, 0x85);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x29, 12);
            this.label20.TabIndex = 11;
            this.label20.Text = "外框宽";
            this.chkMapGrid.AutoSize = true;
            this.chkMapGrid.Location = new Point(0xc3, 0x13c);
            this.chkMapGrid.Name = "chkMapGrid";
            this.chkMapGrid.Size = new Size(0x48, 0x10);
            this.chkMapGrid.TabIndex = 0x59;
            this.chkMapGrid.Text = "使用格网";
            this.chkMapGrid.UseVisualStyleBackColor = true;
            this.chkMapGrid.Visible = false;
            this.chkMapGrid.CheckedChanged += new EventHandler(this.chkMapGrid_CheckedChanged);
            this.cboStyle.BackColor = SystemColors.Window;
            this.cboStyle.Cursor = Cursors.Default;
            this.cboStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStyle.Font = new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboStyle.ForeColor = SystemColors.WindowText;
            this.cboStyle.Items.AddRange(new object[] { "按地图页面大小生成", "按数据范围生成" });
            this.cboStyle.Location = new Point(0x1f8, 0xbf);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.RightToLeft = RightToLeft.No;
            this.cboStyle.Size = new Size(0x2d, 0x16);
            this.cboStyle.TabIndex = 0x5e;
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            this.panelTKYS.Controls.Add(this.rdoTrapezoid);
            this.panelTKYS.Controls.Add(this.rdoRect);
            this.panelTKYS.Controls.Add(this.label17);
            this.panelTKYS.Location = new Point(15, 0x23);
            this.panelTKYS.Name = "panelTKYS";
            this.panelTKYS.Size = new Size(0x1a9, 0x19);
            this.panelTKYS.TabIndex = 0x60;
            this.rdoTrapezoid.AutoSize = true;
            this.rdoTrapezoid.Location = new Point(0xbd, 6);
            this.rdoTrapezoid.Name = "rdoTrapezoid";
            this.rdoTrapezoid.Size = new Size(0x2f, 0x10);
            this.rdoTrapezoid.TabIndex = 9;
            this.rdoTrapezoid.Text = "梯形";
            this.rdoTrapezoid.UseVisualStyleBackColor = true;
            this.rdoRect.AutoSize = true;
            this.rdoRect.Checked = true;
            this.rdoRect.Location = new Point(0x51, 6);
            this.rdoRect.Name = "rdoRect";
            this.rdoRect.Size = new Size(0x2f, 0x10);
            this.rdoRect.TabIndex = 8;
            this.rdoRect.TabStop = true;
            this.rdoRect.Text = "矩形";
            this.rdoRect.UseVisualStyleBackColor = true;
            this.rdoRect.CheckedChanged += new EventHandler(this.rdoRect_CheckedChanged);
            this.label17.AutoSize = true;
            this.label17.Location = new Point(6, 5);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x35, 12);
            this.label17.TabIndex = 7;
            this.label17.Text = "图框样式";
            this.panelScale.Controls.Add(this.txtScale);
            this.panelScale.Controls.Add(this.label7);
            this.panelScale.Location = new Point(0, 0x114);
            this.panelScale.Name = "panelScale";
            this.panelScale.Size = new Size(0xa9, 0x22);
            this.panelScale.TabIndex = 0x61;
            base.Controls.Add(this.panelScale);
            base.Controls.Add(this.panelTKYS);
            base.Controls.Add(this.cboStyle);
            base.Controls.Add(this.chkMapGrid);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.groupBoxPage);
            base.Controls.Add(this.groupBox3);
            base.Name = "MapTemplateGeneralPage";
            base.Size = new Size(0x1df, 0x13f);
            base.Load += new EventHandler(this.MapTemplateGeneralPage_Load);
            base.VisibleChanged += new EventHandler(this.MapTemplateGeneralPage_VisibleChanged);
            this.groupBoxPage.ResumeLayout(false);
            this.groupBoxPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panelTKYS.ResumeLayout(false);
            this.panelTKYS.PerformLayout();
            this.panelScale.ResumeLayout(false);
            this.panelScale.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void MapTemplateGeneralPage_Load(object sender, EventArgs e)
        {
            this.cboStyle.SelectedIndex = 0;
            CartographicLineSymbolClass class2 = new CartographicLineSymbolClass {
                Cap = esriLineCapStyle.esriLCSSquare
            };
            RgbColorClass class3 = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class2.Color = class3;
            class2.Join = esriLineJoinStyle.esriLJSMitre;
            class2.Width = 1.0;
            this.ilineSymbol_0 = class2;
            SimpleFillSymbolClass class4 = new SimpleFillSymbolClass();
            RgbColorClass class5 = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class4.Color = class5;
            class4.Style = esriSimpleFillStyle.esriSFSSolid;
            SimpleLineSymbolClass class6 = new SimpleLineSymbolClass {
                Width = 0.0
            };
            RgbColorClass class7 = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class6.Color = class7;
            class4.Outline = class6;
            this.ifillSymbol_0 = class4;
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.mapTemplate_0 != null)
            {
                this.txtName.Text = this.mapTemplate_0.Name;
                if (this.mapTemplate_0.MapFramingType == MapFramingType.StandardFraming)
                {
                    this.groupBoxPage.Enabled = true;
                    this.panelScale.Enabled = true;
                }
                else
                {
                    this.groupBoxPage.Enabled = false;
                    this.panelScale.Enabled = false;
                }
                if (this.mapTemplate_0.MapFrameType == MapFrameType.MFTRect)
                {
                    this.rdoRect.Checked = true;
                }
                else
                {
                    this.rdoTrapezoid.Checked = true;
                }
                this.txtLeftSpace.Text = this.mapTemplate_0.LeftInOutSpace.ToString();
                this.txtRightSpace.Text = this.mapTemplate_0.RightInOutSpace.ToString();
                this.txtTopSpace.Text = this.mapTemplate_0.TopInOutSpace.ToString();
                this.txtBottomSpace.Text = this.mapTemplate_0.BottomInOutSpace.ToString();
                this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                this.cboStyle.SelectedIndex = (int) this.mapTemplate_0.TemplateSizeStyle;
                this.txtWidth.Text = this.mapTemplate_0.Width.ToString();
                this.txtHeight.Text = this.mapTemplate_0.Height.ToString();
                this.txtScale.Text = this.mapTemplate_0.Scale.ToString();
                this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
                if (this.mapTemplate_0.BorderSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                    this.ilineSymbol_0 = this.mapTemplate_0.BorderSymbol as ILineSymbol;
                }
                else if (this.mapTemplate_0.BorderSymbol is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.mapTemplate_0.BorderSymbol as IFillSymbol;
                    this.rdoFill.Checked = true;
                }
                else
                {
                    this.rdoNoOutLine.Checked = true;
                    this.styleButton1.Enabled = false;
                }
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                this.cboStyle.SelectedIndex = (int) this.mapTemplate_0.TemplateSizeStyle;
                this.chkMapGrid.Checked = this.mapTemplate_0.MapGrid != null;
                this.checkBox1.Checked = this.mapTemplate_0.FixedWidthAndBottomSpace;
            }
            this.bool_0 = true;
        }

        private void MapTemplateGeneralPage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.bool_0 = false;
                if (this.mapTemplate_0 != null)
                {
                    this.txtName.Text = this.mapTemplate_0.Name;
                    this.txtLeftSpace.Text = this.mapTemplate_0.LeftInOutSpace.ToString();
                    this.txtRightSpace.Text = this.mapTemplate_0.RightInOutSpace.ToString();
                    this.txtTopSpace.Text = this.mapTemplate_0.TopInOutSpace.ToString();
                    this.txtBottomSpace.Text = this.mapTemplate_0.BottomInOutSpace.ToString();
                    this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                    this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                    this.cboStyle.SelectedIndex = (int) this.mapTemplate_0.TemplateSizeStyle;
                    this.txtWidth.Text = this.mapTemplate_0.Width.ToString();
                    this.txtHeight.Text = this.mapTemplate_0.Height.ToString();
                    this.txtScale.Text = this.mapTemplate_0.Scale.ToString();
                    this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                    this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
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
                    this.cboStyle.SelectedIndex = (int) this.mapTemplate_0.TemplateSizeStyle;
                    this.chkMapGrid.Checked = this.mapTemplate_0.MapGrid != null;
                }
                this.bool_0 = true;
            }
        }

        private void method_0()
        {
            IMapGrid grid = new MeasuredGridClass();
            if (this.MapFrame != null)
            {
                grid.SetDefaults(this.MapFrame);
            }
            (grid as IMeasuredGrid).XOrigin = 0.0;
            (grid as IMeasuredGrid).Units = esriUnits.esriMeters;
            (grid as IMeasuredGrid).YOrigin = 0.0;
            (grid as IMeasuredGrid).XIntervalSize = 200.0;
            (grid as IMeasuredGrid).YIntervalSize = 200.0;
            (grid as IMeasuredGrid).FixedOrigin = true;
            IGridLabel labelFormat = grid.LabelFormat;
            ITextSymbol symbol = new TextSymbolClass {
                Font = labelFormat.Font,
                Color = labelFormat.Color,
                Text = labelFormat.DisplayName,
                VerticalAlignment = esriTextVerticalAlignment.esriTVABottom
            };
            labelFormat.Font = symbol.Font;
            labelFormat.Color = symbol.Color;
            labelFormat.LabelOffset = 6.0;
            grid.LabelFormat = labelFormat;
            if (labelFormat is IMixedFontGridLabel2)
            {
                (labelFormat as IMixedFontGridLabel2).NumGroupedDigits = 0;
            }
            this.MapTemplate.MapGrid = grid;
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

        private void rdoFill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoFill.Checked)
            {
                this.styleButton1.Enabled = true;
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
                this.styleButton1.Enabled = true;
                if (this.styleButton1.Style is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                }
                this.styleButton1.Style = this.ilineSymbol_0;
                this.txtOutBorderWidth.Text = (this.ilineSymbol_0.Width * 0.0352777778).ToString("0.##");
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoNoOutLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoNoOutLine.Checked)
            {
                this.styleButton1.Enabled = false;
                this.styleButton1.Style = null;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoRect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.mapTemplate_0.MapFramingType == MapFramingType.StandardFraming)
            {
                this.groupBoxPage.Enabled = this.rdoRect.Checked;
            }
            this.mapTemplate_0.NewMapFrameTypeVal = this.rdoRect.Checked ? MapFrameType.MFTRect : MapFrameType.MFTTrapezoid;
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
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
                        this.txtOutBorderWidth.Text = (this.ilineSymbol_0.Width * 0.0352777778).ToString("0.##");
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
                if (this.rdoLine.Checked)
                {
                    ILineSymbol style = this.styleButton1.Style as ILineSymbol;
                    if (style != null)
                    {
                        try
                        {
                            double num = Convert.ToDouble(this.txtOutBorderWidth.Text);
                            style.Width = num / 0.0352777778;
                            this.styleButton1.Style = style;
                        }
                        catch
                        {
                        }
                    }
                }
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

        private void txtTopSpace_TextChanged(object sender, EventArgs e)
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

        public IMapFrame MapFrame
        {
            [CompilerGenerated]
            protected get
            {
                return this.imapFrame_0;
            }
            [CompilerGenerated]
            set
            {
                this.imapFrame_0 = value;
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

        public bool UseMapGrid
        {
            get
            {
                return this.chkMapGrid.Checked;
            }
        }
    }
}

