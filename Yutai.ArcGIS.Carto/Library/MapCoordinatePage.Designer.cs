using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class MapCoordinatePage
    {
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMapNo = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoMapNo = new System.Windows.Forms.RadioButton();
            this.rdoLeftDown = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRightLowX = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtRightLowY = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtRightUpperX = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtRightUpperY = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLeftLowX = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtLeftLowY = new System.Windows.Forms.TextBox();
            this.txtProjScale = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtLeftUpperX = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLeftUpperY = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rdoCoordinate = new System.Windows.Forms.RadioButton();
            this.rdoFieldSearch = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtSearchKey = new DevExpress.XtraEditors.TextEdit();
            this.label31 = new System.Windows.Forms.Label();
            this.cmbIndexMap = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.btnKeySearch = new DevExpress.XtraEditors.SimpleButton();
            this.lstIndexFeatures = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchKey.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图号";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMapNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 32);
            this.panel1.TabIndex = 1;
            // 
            // txtMapNo
            // 
            this.txtMapNo.Location = new System.Drawing.Point(49, 5);
            this.txtMapNo.Name = "txtMapNo";
            this.txtMapNo.Size = new System.Drawing.Size(131, 21);
            this.txtMapNo.TabIndex = 1;
            // 
            // panel2
            // 
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
            this.panel2.Location = new System.Drawing.Point(9, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 197);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 168);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "横坐标包含带号";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(175, 139);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(17, 12);
            this.label29.TabIndex = 15;
            this.label29.Text = "米";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 136);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(84, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "500000";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(14, 139);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 13;
            this.label28.Text = "横坐标偏移";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(175, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "度";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(175, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "度";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "度";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "度";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(70, 112);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(99, 21);
            this.txtScale.TabIndex = 9;
            this.txtScale.Text = "10000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "比例尺1:";
            // 
            // txtWC
            // 
            this.txtWC.Location = new System.Drawing.Point(49, 85);
            this.txtWC.Name = "txtWC";
            this.txtWC.Size = new System.Drawing.Size(120, 21);
            this.txtWC.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "纬差";
            // 
            // txtJC
            // 
            this.txtJC.Location = new System.Drawing.Point(49, 59);
            this.txtJC.Name = "txtJC";
            this.txtJC.Size = new System.Drawing.Size(120, 21);
            this.txtJC.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "经差";
            // 
            // txtWD
            // 
            this.txtWD.Location = new System.Drawing.Point(49, 33);
            this.txtWD.Name = "txtWD";
            this.txtWD.Size = new System.Drawing.Size(120, 21);
            this.txtWD.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "纬度";
            // 
            // txtJD
            // 
            this.txtJD.Location = new System.Drawing.Point(49, 8);
            this.txtJD.Name = "txtJD";
            this.txtJD.Size = new System.Drawing.Size(120, 21);
            this.txtJD.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "经度";
            // 
            // rdoMapNo
            // 
            this.rdoMapNo.AutoSize = true;
            this.rdoMapNo.Checked = true;
            this.rdoMapNo.Location = new System.Drawing.Point(18, 4);
            this.rdoMapNo.Name = "rdoMapNo";
            this.rdoMapNo.Size = new System.Drawing.Size(47, 16);
            this.rdoMapNo.TabIndex = 3;
            this.rdoMapNo.TabStop = true;
            this.rdoMapNo.Text = "图号";
            this.rdoMapNo.UseVisualStyleBackColor = true;
            this.rdoMapNo.Visible = false;
            this.rdoMapNo.CheckedChanged += new System.EventHandler(this.rdoMapNo_CheckedChanged);
            // 
            // rdoLeftDown
            // 
            this.rdoLeftDown.AutoSize = true;
            this.rdoLeftDown.Location = new System.Drawing.Point(71, 4);
            this.rdoLeftDown.Name = "rdoLeftDown";
            this.rdoLeftDown.Size = new System.Drawing.Size(95, 16);
            this.rdoLeftDown.TabIndex = 4;
            this.rdoLeftDown.Text = "左下角经纬度";
            this.rdoLeftDown.UseVisualStyleBackColor = true;
            this.rdoLeftDown.Visible = false;
            this.rdoLeftDown.CheckedChanged += new System.EventHandler(this.rdoLeftDown_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.txtProjScale);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Location = new System.Drawing.Point(9, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 193);
            this.panel3.TabIndex = 5;
            this.panel3.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtRightLowX);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txtRightLowY);
            this.groupBox3.Location = new System.Drawing.Point(200, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(191, 79);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "右下角坐标";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(168, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 12);
            this.label20.TabIndex = 12;
            this.label20.Text = "米";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
            this.label21.TabIndex = 4;
            this.label21.Text = "横坐标";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(168, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 11;
            this.label22.Text = "米";
            // 
            // txtRightLowX
            // 
            this.txtRightLowX.Location = new System.Drawing.Point(53, 20);
            this.txtRightLowX.Name = "txtRightLowX";
            this.txtRightLowX.Size = new System.Drawing.Size(109, 21);
            this.txtRightLowX.TabIndex = 5;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 49);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 6;
            this.label23.Text = "纵坐标";
            // 
            // txtRightLowY
            // 
            this.txtRightLowY.Location = new System.Drawing.Point(53, 46);
            this.txtRightLowY.Name = "txtRightLowY";
            this.txtRightLowY.Size = new System.Drawing.Size(109, 21);
            this.txtRightLowY.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.txtRightUpperX);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.txtRightUpperY);
            this.groupBox4.Location = new System.Drawing.Point(200, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 68);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "右上角坐标";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 17);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "横坐标";
            // 
            // txtRightUpperX
            // 
            this.txtRightUpperX.Location = new System.Drawing.Point(53, 14);
            this.txtRightUpperX.Name = "txtRightUpperX";
            this.txtRightUpperX.Size = new System.Drawing.Size(108, 21);
            this.txtRightUpperX.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(167, 42);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 12);
            this.label25.TabIndex = 10;
            this.label25.Text = "米";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 42);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(41, 12);
            this.label26.TabIndex = 2;
            this.label26.Text = "纵坐标";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(167, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 12);
            this.label27.TabIndex = 5;
            this.label27.Text = "米";
            // 
            // txtRightUpperY
            // 
            this.txtRightUpperY.Location = new System.Drawing.Point(53, 39);
            this.txtRightUpperY.Name = "txtRightUpperY";
            this.txtRightUpperY.Size = new System.Drawing.Size(108, 21);
            this.txtRightUpperY.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtLeftLowX);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtLeftLowY);
            this.groupBox2.Location = new System.Drawing.Point(3, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 79);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "左下角坐标";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(168, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "米";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "横坐标";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(168, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "米";
            // 
            // txtLeftLowX
            // 
            this.txtLeftLowX.Location = new System.Drawing.Point(53, 20);
            this.txtLeftLowX.Name = "txtLeftLowX";
            this.txtLeftLowX.Size = new System.Drawing.Size(109, 21);
            this.txtLeftLowX.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 6;
            this.label16.Text = "纵坐标";
            // 
            // txtLeftLowY
            // 
            this.txtLeftLowY.Location = new System.Drawing.Point(53, 46);
            this.txtLeftLowY.Name = "txtLeftLowY";
            this.txtLeftLowY.Size = new System.Drawing.Size(109, 21);
            this.txtLeftLowY.TabIndex = 7;
            // 
            // txtProjScale
            // 
            this.txtProjScale.Location = new System.Drawing.Point(68, 162);
            this.txtProjScale.Name = "txtProjScale";
            this.txtProjScale.Size = new System.Drawing.Size(99, 21);
            this.txtProjScale.TabIndex = 9;
            this.txtProjScale.Text = "10000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtLeftUpperX);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtLeftUpperY);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 68);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "左上角坐标";
            this.groupBox1.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 17);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "横坐标";
            // 
            // txtLeftUpperX
            // 
            this.txtLeftUpperX.Location = new System.Drawing.Point(53, 14);
            this.txtLeftUpperX.Name = "txtLeftUpperX";
            this.txtLeftUpperX.Size = new System.Drawing.Size(108, 21);
            this.txtLeftUpperX.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(167, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "米";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 42);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 2;
            this.label18.Text = "纵坐标";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(167, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "米";
            // 
            // txtLeftUpperY
            // 
            this.txtLeftUpperY.Location = new System.Drawing.Point(53, 39);
            this.txtLeftUpperY.Name = "txtLeftUpperY";
            this.txtLeftUpperY.Size = new System.Drawing.Size(108, 21);
            this.txtLeftUpperY.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 165);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 8;
            this.label15.Text = "比例尺1:";
            // 
            // rdoCoordinate
            // 
            this.rdoCoordinate.AutoSize = true;
            this.rdoCoordinate.Location = new System.Drawing.Point(189, 4);
            this.rdoCoordinate.Name = "rdoCoordinate";
            this.rdoCoordinate.Size = new System.Drawing.Size(71, 16);
            this.rdoCoordinate.TabIndex = 6;
            this.rdoCoordinate.Text = "四角坐标";
            this.rdoCoordinate.UseVisualStyleBackColor = true;
            this.rdoCoordinate.Visible = false;
            this.rdoCoordinate.CheckedChanged += new System.EventHandler(this.rdoCoordinate_CheckedChanged);
            // 
            // rdoFieldSearch
            // 
            this.rdoFieldSearch.AutoSize = true;
            this.rdoFieldSearch.Location = new System.Drawing.Point(266, 4);
            this.rdoFieldSearch.Name = "rdoFieldSearch";
            this.rdoFieldSearch.Size = new System.Drawing.Size(83, 16);
            this.rdoFieldSearch.TabIndex = 7;
            this.rdoFieldSearch.TabStop = true;
            this.rdoFieldSearch.Text = "关键字搜索";
            this.rdoFieldSearch.UseVisualStyleBackColor = true;
            this.rdoFieldSearch.Visible = false;
            this.rdoFieldSearch.CheckedChanged += new System.EventHandler(this.rdoFieldSearch_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lstIndexFeatures);
            this.panel4.Controls.Add(this.btnKeySearch);
            this.panel4.Controls.Add(this.txtSearchKey);
            this.panel4.Controls.Add(this.label31);
            this.panel4.Controls.Add(this.cmbIndexMap);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Location = new System.Drawing.Point(9, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(410, 205);
            this.panel4.TabIndex = 8;
            this.panel4.Visible = false;
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Location = new System.Drawing.Point(82, 43);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(229, 20);
            this.txtSearchKey.TabIndex = 4;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(21, 48);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 3;
            this.label31.Text = "搜索内容";
            // 
            // cmbIndexMap
            // 
            this.cmbIndexMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndexMap.FormattingEnabled = true;
            this.cmbIndexMap.Location = new System.Drawing.Point(77, 12);
            this.cmbIndexMap.Name = "cmbIndexMap";
            this.cmbIndexMap.Size = new System.Drawing.Size(314, 20);
            this.cmbIndexMap.TabIndex = 2;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(17, 16);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(41, 12);
            this.label30.TabIndex = 1;
            this.label30.Text = "索引图";
            // 
            // btnKeySearch
            // 
            this.btnKeySearch.Location = new System.Drawing.Point(317, 42);
            this.btnKeySearch.Name = "btnKeySearch";
            this.btnKeySearch.Size = new System.Drawing.Size(74, 24);
            this.btnKeySearch.TabIndex = 5;
            this.btnKeySearch.Text = "搜索";
            this.btnKeySearch.Click += new System.EventHandler(this.btnKeySearch_Click);
            // 
            // lstIndexFeatures
            // 
            this.lstIndexFeatures.FormattingEnabled = true;
            this.lstIndexFeatures.ItemHeight = 12;
            this.lstIndexFeatures.Location = new System.Drawing.Point(23, 68);
            this.lstIndexFeatures.Name = "lstIndexFeatures";
            this.lstIndexFeatures.Size = new System.Drawing.Size(368, 124);
            this.lstIndexFeatures.TabIndex = 6;
            // 
            // MapCoordinatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.rdoFieldSearch);
            this.Controls.Add(this.rdoCoordinate);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.rdoLeftDown);
            this.Controls.Add(this.rdoMapNo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MapCoordinatePage";
            this.Size = new System.Drawing.Size(439, 233);
            this.Load += new System.EventHandler(this.MapCoordinatePage_Load);
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
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchKey.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
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
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private RadioButton rdoLeftDown;
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
        private RadioButton rdoFieldSearch;
        private Panel panel4;
        private DevExpress.XtraEditors.TextEdit txtSearchKey;
        private Label label31;
        private ComboBox cmbIndexMap;
        private Label label30;
        private DevExpress.XtraEditors.SimpleButton btnKeySearch;
        private ListBox lstIndexFeatures;
    }
}