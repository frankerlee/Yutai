using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public class MapCoordinatePage : UserControl, IPropertyPage
    {
        private bool bool_0 = false;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IContainer icontainer_0 = null;
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
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private RadioButton radioButton1;
        private RadioButton rdoCoordinate;
        private RadioButton rdoMapNo;
        private TextBox textBox1;
        private TextBox txtJC;
        private TextBox txtJD;
        private TextBox txtLeftLowX;
        private TextBox txtLeftLowY;
        private TextBox txtLeftUpperX;
        private TextBox txtLeftUpperY;
        private TextBox txtMapNo;
        private TextBox txtProjScale;
        private TextBox txtRightLowX;
        private TextBox txtRightLowY;
        private TextBox txtRightUpperX;
        private TextBox txtRightUpperY;
        private TextBox txtScale;
        private TextBox txtWC;
        private TextBox txtWD;

        public MapCoordinatePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.rdoMapNo.Checked)
            {
                this.mapTemplateApplyHelp_0.MapNo = this.txtMapNo.Text;
            }
            else if (this.radioButton1.Checked)
            {
                this.mapTemplateApplyHelp_0.HasStrip = this.checkBox1.Checked;
                this.mapTemplateApplyHelp_0.XOffset = double.Parse(this.textBox1.Text);
                this.mapTemplateApplyHelp_0.SetJWD(double.Parse(this.txtJD.Text), double.Parse(this.txtWD.Text), double.Parse(this.txtJC.Text), double.Parse(this.txtWC.Text), double.Parse(this.txtScale.Text));
            }
            else
            {
                IPoint point = new PointClass();
                point.PutCoords(double.Parse(this.txtLeftUpperX.Text), double.Parse(this.txtLeftUpperY.Text));
                IPoint point2 = new PointClass();
                point2.PutCoords(double.Parse(this.txtRightUpperX.Text), double.Parse(this.txtRightUpperY.Text));
                IPoint point3 = new PointClass();
                point3.PutCoords(double.Parse(this.txtRightLowX.Text), double.Parse(this.txtRightLowY.Text));
                IPoint point4 = new PointClass();
                point4.PutCoords(double.Parse(this.txtLeftLowX.Text), double.Parse(this.txtLeftLowY.Text));
                this.mapTemplateApplyHelp_0.SetRouneCoordinate(point4, point, point2, point3, double.Parse(this.txtProjScale.Text));
            }
        }

        public bool CanApply()
        {
            if (this.rdoMapNo.Checked)
            {
                MapNoAssistant assistant = MapNoAssistantFactory.CreateMapNoAssistant(this.txtMapNo.Text);
                if (assistant == null)
                {
                    MessageBox.Show("图号输入不正确!");
                    return false;
                }
                if (!assistant.Validate())
                {
                    MessageBox.Show("图号输入不正确!");
                    return false;
                }
                return true;
            }
            if (this.radioButton1.Checked)
            {
                try
                {
                    double.Parse(this.txtJD.Text);
                    double.Parse(this.txtWD.Text);
                    double.Parse(this.txtJC.Text);
                    double.Parse(this.txtWC.Text);
                    double.Parse(this.txtScale.Text);
                    goto Label_0160;
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                double.Parse(this.txtLeftUpperX.Text);
                double.Parse(this.txtLeftUpperY.Text);
                double.Parse(this.txtRightUpperX.Text);
                double.Parse(this.txtRightUpperY.Text);
                double.Parse(this.txtRightLowX.Text);
                double.Parse(this.txtRightLowY.Text);
                double.Parse(this.txtLeftLowX.Text);
                double.Parse(this.txtLeftLowY.Text);
            }
            catch
            {
                return false;
            }
        Label_0160:
            return true;
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.txtMapNo = new TextBox();
            this.panel2 = new Panel();
            this.checkBox1 = new CheckBox();
            this.label29 = new Label();
            this.textBox1 = new TextBox();
            this.label28 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.txtScale = new TextBox();
            this.label6 = new Label();
            this.txtWC = new TextBox();
            this.label4 = new Label();
            this.txtJC = new TextBox();
            this.label5 = new Label();
            this.txtWD = new TextBox();
            this.label3 = new Label();
            this.txtJD = new TextBox();
            this.label2 = new Label();
            this.rdoMapNo = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.panel3 = new Panel();
            this.groupBox3 = new GroupBox();
            this.label20 = new Label();
            this.label21 = new Label();
            this.label22 = new Label();
            this.txtRightLowX = new TextBox();
            this.label23 = new Label();
            this.txtRightLowY = new TextBox();
            this.groupBox4 = new GroupBox();
            this.label24 = new Label();
            this.txtRightUpperX = new TextBox();
            this.label25 = new Label();
            this.label26 = new Label();
            this.label27 = new Label();
            this.txtRightUpperY = new TextBox();
            this.groupBox2 = new GroupBox();
            this.label11 = new Label();
            this.label17 = new Label();
            this.label12 = new Label();
            this.txtLeftLowX = new TextBox();
            this.label16 = new Label();
            this.txtLeftLowY = new TextBox();
            this.txtProjScale = new TextBox();
            this.groupBox1 = new GroupBox();
            this.label19 = new Label();
            this.txtLeftUpperX = new TextBox();
            this.label13 = new Label();
            this.label18 = new Label();
            this.label14 = new Label();
            this.txtLeftUpperY = new TextBox();
            this.label15 = new Label();
            this.rdoCoordinate = new RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图号";
            this.panel1.Controls.Add(this.txtMapNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 0x1a);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(200, 0x20);
            this.panel1.TabIndex = 1;
            this.txtMapNo.Location = new System.Drawing.Point(0x31, 5);
            this.txtMapNo.Name = "txtMapNo";
            this.txtMapNo.Size = new Size(0x83, 0x15);
            this.txtMapNo.TabIndex = 1;
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtScale);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtWC);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtJC);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtWD);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtJD);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(9, 0x1b);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(200, 0xc5);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0x10, 0xa8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x6c, 0x10);
            this.checkBox1.TabIndex = 0x10;
            this.checkBox1.Text = "横坐标包含带号";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(0xaf, 0x8b);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x11, 12);
            this.label29.TabIndex = 15;
            this.label29.Text = "米";
            this.textBox1.Location = new System.Drawing.Point(0x55, 0x88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x54, 0x15);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "500000";
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(14, 0x8b);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x41, 12);
            this.label28.TabIndex = 13;
            this.label28.Text = "横坐标偏移";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0xaf, 0x58);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x11, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "度";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0xaf, 0x3e);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "度";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0xaf, 0x24);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "度";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0xaf, 11);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "度";
            this.txtScale.Location = new System.Drawing.Point(70, 0x70);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(0x63, 0x15);
            this.txtScale.TabIndex = 9;
            this.txtScale.Text = "10000";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 0x73);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "比例尺1:";
            this.txtWC.Location = new System.Drawing.Point(0x31, 0x55);
            this.txtWC.Name = "txtWC";
            this.txtWC.Size = new Size(120, 0x15);
            this.txtWC.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "纬差";
            this.txtJC.Location = new System.Drawing.Point(0x31, 0x3b);
            this.txtJC.Name = "txtJC";
            this.txtJC.Size = new Size(120, 0x15);
            this.txtJC.TabIndex = 5;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 0x3e);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "经差";
            this.txtWD.Location = new System.Drawing.Point(0x31, 0x21);
            this.txtWD.Name = "txtWD";
            this.txtWD.Size = new Size(120, 0x15);
            this.txtWD.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 0x24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "纬度";
            this.txtJD.Location = new System.Drawing.Point(0x31, 8);
            this.txtJD.Name = "txtJD";
            this.txtJD.Size = new Size(120, 0x15);
            this.txtJD.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "经度";
            this.rdoMapNo.AutoSize = true;
            this.rdoMapNo.Checked = true;
            this.rdoMapNo.Location = new System.Drawing.Point(0x12, 4);
            this.rdoMapNo.Name = "rdoMapNo";
            this.rdoMapNo.Size = new Size(0x2f, 0x10);
            this.rdoMapNo.TabIndex = 3;
            this.rdoMapNo.TabStop = true;
            this.rdoMapNo.Text = "图号";
            this.rdoMapNo.UseVisualStyleBackColor = true;
            this.rdoMapNo.Visible = false;
            this.rdoMapNo.CheckedChanged += new EventHandler(this.rdoMapNo_CheckedChanged);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(0x47, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x5f, 0x10);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Text = "左下角经纬度";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.txtProjScale);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Location = new System.Drawing.Point(9, 0x1f);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(410, 0xc1);
            this.panel3.TabIndex = 5;
            this.panel3.Visible = false;
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtRightLowX);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txtRightLowY);
            this.groupBox3.Location = new System.Drawing.Point(200, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xbf, 0x4f);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "右下角坐标";
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(0xa8, 0x31);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x11, 12);
            this.label20.TabIndex = 12;
            this.label20.Text = "米";
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 0x17);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x29, 12);
            this.label21.TabIndex = 4;
            this.label21.Text = "横坐标";
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(0xa8, 0x17);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x11, 12);
            this.label22.TabIndex = 11;
            this.label22.Text = "米";
            this.txtRightLowX.Location = new System.Drawing.Point(0x35, 20);
            this.txtRightLowX.Name = "txtRightLowX";
            this.txtRightLowX.Size = new Size(0x6d, 0x15);
            this.txtRightLowX.TabIndex = 5;
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 0x31);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x29, 12);
            this.label23.TabIndex = 6;
            this.label23.Text = "纵坐标";
            this.txtRightLowY.Location = new System.Drawing.Point(0x35, 0x2e);
            this.txtRightLowY.Name = "txtRightLowY";
            this.txtRightLowY.Size = new Size(0x6d, 0x15);
            this.txtRightLowY.TabIndex = 7;
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.txtRightUpperX);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.txtRightUpperY);
            this.groupBox4.Location = new System.Drawing.Point(200, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xbf, 0x44);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "右上角坐标";
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 0x11);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x29, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "横坐标";
            this.txtRightUpperX.Location = new System.Drawing.Point(0x35, 14);
            this.txtRightUpperX.Name = "txtRightUpperX";
            this.txtRightUpperX.Size = new Size(0x6c, 0x15);
            this.txtRightUpperX.TabIndex = 1;
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(0xa7, 0x2a);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x11, 12);
            this.label25.TabIndex = 10;
            this.label25.Text = "米";
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 0x2a);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x29, 12);
            this.label26.TabIndex = 2;
            this.label26.Text = "纵坐标";
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(0xa7, 0x11);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x11, 12);
            this.label27.TabIndex = 5;
            this.label27.Text = "米";
            this.txtRightUpperY.Location = new System.Drawing.Point(0x35, 0x27);
            this.txtRightUpperY.Name = "txtRightUpperY";
            this.txtRightUpperY.Size = new Size(0x6c, 0x15);
            this.txtRightUpperY.TabIndex = 3;
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtLeftLowX);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtLeftLowY);
            this.groupBox2.Location = new System.Drawing.Point(3, 0x4e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xbf, 0x4f);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "左下角坐标";
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0xa8, 0x31);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x11, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "米";
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 0x17);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x29, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "横坐标";
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0xa8, 0x17);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "米";
            this.txtLeftLowX.Location = new System.Drawing.Point(0x35, 20);
            this.txtLeftLowX.Name = "txtLeftLowX";
            this.txtLeftLowX.Size = new Size(0x6d, 0x15);
            this.txtLeftLowX.TabIndex = 5;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 0x31);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x29, 12);
            this.label16.TabIndex = 6;
            this.label16.Text = "纵坐标";
            this.txtLeftLowY.Location = new System.Drawing.Point(0x35, 0x2e);
            this.txtLeftLowY.Name = "txtLeftLowY";
            this.txtLeftLowY.Size = new Size(0x6d, 0x15);
            this.txtLeftLowY.TabIndex = 7;
            this.txtProjScale.Location = new System.Drawing.Point(0x44, 0xa2);
            this.txtProjScale.Name = "txtProjScale";
            this.txtProjScale.Size = new Size(0x63, 0x15);
            this.txtProjScale.TabIndex = 9;
            this.txtProjScale.Text = "10000";
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtLeftUpperX);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtLeftUpperY);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xbf, 0x44);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "左上角坐标";
            this.groupBox1.Visible = false;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 0x11);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x29, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "横坐标";
            this.txtLeftUpperX.Location = new System.Drawing.Point(0x35, 14);
            this.txtLeftUpperX.Name = "txtLeftUpperX";
            this.txtLeftUpperX.Size = new Size(0x6c, 0x15);
            this.txtLeftUpperX.TabIndex = 1;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0xa7, 0x2a);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "米";
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 0x2a);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x29, 12);
            this.label18.TabIndex = 2;
            this.label18.Text = "纵坐标";
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(0xa7, 0x11);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x11, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "米";
            this.txtLeftUpperY.Location = new System.Drawing.Point(0x35, 0x27);
            this.txtLeftUpperY.Name = "txtLeftUpperY";
            this.txtLeftUpperY.Size = new Size(0x6c, 0x15);
            this.txtLeftUpperY.TabIndex = 3;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 0xa5);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x35, 12);
            this.label15.TabIndex = 8;
            this.label15.Text = "比例尺1:";
            this.rdoCoordinate.AutoSize = true;
            this.rdoCoordinate.Location = new System.Drawing.Point(0xbd, 4);
            this.rdoCoordinate.Name = "rdoCoordinate";
            this.rdoCoordinate.Size = new Size(0x47, 0x10);
            this.rdoCoordinate.TabIndex = 6;
            this.rdoCoordinate.Text = "四角坐标";
            this.rdoCoordinate.UseVisualStyleBackColor = true;
            this.rdoCoordinate.Visible = false;
            this.rdoCoordinate.CheckedChanged += new EventHandler(this.rdoCoordinate_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.rdoCoordinate);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.radioButton1);
            base.Controls.Add(this.rdoMapNo);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "MapCoordinatePage";
            base.Size = new Size(0x1bc, 0xf3);
            base.Load += new EventHandler(this.MapCoordinatePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void MapCoordinatePage_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.panel2.Visible = this.radioButton1.Checked;
        }

        private void rdoCoordinate_CheckedChanged(object sender, EventArgs e)
        {
            this.panel3.Visible = this.rdoCoordinate.Checked;
        }

        private void rdoMapNo_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = this.rdoMapNo.Checked;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
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

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "坐标";
            }
            set
            {
            }
        }
    }
}

