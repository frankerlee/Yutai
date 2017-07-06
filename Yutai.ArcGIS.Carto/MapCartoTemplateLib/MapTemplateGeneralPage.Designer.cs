using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class MapTemplateGeneralPage
    {
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPage = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtYInterval = new System.Windows.Forms.TextBox();
            this.txtXInterval = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rdoNoOutLine = new System.Windows.Forms.RadioButton();
            this.rdoLine = new System.Windows.Forms.RadioButton();
            this.styleButton1 = new Yutai.ArcGIS.Common.SymbolLib.StyleButton();
            this.rdoFill = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtRightSpace = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtLeftSpace = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtBottomSpace = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtTopSpace = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtOutBorderWidth = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.chkMapGrid = new System.Windows.Forms.CheckBox();
            this.cboStyle = new System.Windows.Forms.ComboBox();
            this.panelTKYS = new System.Windows.Forms.Panel();
            this.rdoTrapezoid = new System.Windows.Forms.RadioButton();
            this.rdoRect = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.panelScale = new System.Windows.Forms.Panel();
            this.groupBoxPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panelTKYS.SuspendLayout();
            this.panelScale.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高";
            // 
            // groupBoxPage
            // 
            this.groupBoxPage.Controls.Add(this.label9);
            this.groupBoxPage.Controls.Add(this.label8);
            this.groupBoxPage.Controls.Add(this.txtWidth);
            this.groupBoxPage.Controls.Add(this.txtHeight);
            this.groupBoxPage.Controls.Add(this.label2);
            this.groupBoxPage.Controls.Add(this.label1);
            this.groupBoxPage.Location = new System.Drawing.Point(14, 68);
            this.groupBoxPage.Name = "groupBoxPage";
            this.groupBoxPage.Size = new System.Drawing.Size(155, 105);
            this.groupBoxPage.TabIndex = 1;
            this.groupBoxPage.TabStop = false;
            this.groupBoxPage.Text = "页面尺寸";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(119, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "厘米";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "厘米";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(39, 52);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(74, 21);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.Text = "50";
            this.txtWidth.TextChanged += new System.EventHandler(this.txtTopSpace_TextChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(38, 24);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(75, 21);
            this.txtHeight.TabIndex = 2;
            this.txtHeight.Text = "50";
            this.txtHeight.TextChanged += new System.EventHandler(this.txtTopSpace_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtYInterval);
            this.groupBox3.Controls.Add(this.txtXInterval);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(14, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 92);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "坐标轴间隔";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(120, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "厘米";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(120, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "厘米";
            // 
            // txtYInterval
            // 
            this.txtYInterval.Location = new System.Drawing.Point(62, 52);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new System.Drawing.Size(52, 21);
            this.txtYInterval.TabIndex = 3;
            this.txtYInterval.Text = "10";
            this.txtYInterval.TextChanged += new System.EventHandler(this.txtYInterval_TextChanged);
            // 
            // txtXInterval
            // 
            this.txtXInterval.Location = new System.Drawing.Point(62, 24);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new System.Drawing.Size(52, 21);
            this.txtXInterval.TabIndex = 2;
            this.txtXInterval.Text = "10";
            this.txtXInterval.TextChanged += new System.EventHandler(this.txtXInterval_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "横坐标";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "纵坐标";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(62, 4);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(100, 21);
            this.txtScale.TabIndex = 5;
            this.txtScale.Text = "500";
            this.txtScale.TextChanged += new System.EventHandler(this.txtScale_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "比例尺1:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(53, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(388, 21);
            this.txtName.TabIndex = 7;
            this.txtName.Text = "模板";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txtOutBorderWidth);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Location = new System.Drawing.Point(175, 68);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(276, 242);
            this.groupBox4.TabIndex = 88;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "外框设置";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 103);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(168, 16);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "底部间距和页面宽度成比例";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rdoNoOutLine);
            this.groupBox6.Controls.Add(this.rdoLine);
            this.groupBox6.Controls.Add(this.styleButton1);
            this.groupBox6.Controls.Add(this.rdoFill);
            this.groupBox6.Location = new System.Drawing.Point(14, 152);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 79);
            this.groupBox6.TabIndex = 91;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "外框符号";
            // 
            // rdoNoOutLine
            // 
            this.rdoNoOutLine.AutoSize = true;
            this.rdoNoOutLine.Location = new System.Drawing.Point(109, 20);
            this.rdoNoOutLine.Name = "rdoNoOutLine";
            this.rdoNoOutLine.Size = new System.Drawing.Size(35, 16);
            this.rdoNoOutLine.TabIndex = 16;
            this.rdoNoOutLine.Text = "无";
            this.rdoNoOutLine.UseVisualStyleBackColor = true;
            this.rdoNoOutLine.CheckedChanged += new System.EventHandler(this.rdoNoOutLine_CheckedChanged);
            // 
            // rdoLine
            // 
            this.rdoLine.AutoSize = true;
            this.rdoLine.Checked = true;
            this.rdoLine.Location = new System.Drawing.Point(9, 21);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new System.Drawing.Size(35, 16);
            this.rdoLine.TabIndex = 14;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new System.EventHandler(this.rdoLine_CheckedChanged);
            // 
            // styleButton1
            // 
            this.styleButton1.Location = new System.Drawing.Point(6, 42);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new System.Drawing.Size(96, 31);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 9;
            this.styleButton1.Click += new System.EventHandler(this.styleButton1_Click);
            // 
            // rdoFill
            // 
            this.rdoFill.AutoSize = true;
            this.rdoFill.Location = new System.Drawing.Point(57, 20);
            this.rdoFill.Name = "rdoFill";
            this.rdoFill.Size = new System.Drawing.Size(35, 16);
            this.rdoFill.TabIndex = 15;
            this.rdoFill.Text = "面";
            this.rdoFill.UseVisualStyleBackColor = true;
            this.rdoFill.CheckedChanged += new System.EventHandler(this.rdoFill_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtRightSpace);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.txtLeftSpace);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.txtBottomSpace);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txtTopSpace);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(260, 76);
            this.groupBox5.TabIndex = 90;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "内外框间距(厘米)";
            // 
            // txtRightSpace
            // 
            this.txtRightSpace.Location = new System.Drawing.Point(173, 50);
            this.txtRightSpace.Name = "txtRightSpace";
            this.txtRightSpace.Size = new System.Drawing.Size(43, 21);
            this.txtRightSpace.TabIndex = 16;
            this.txtRightSpace.Text = "1";
            this.txtRightSpace.TextChanged += new System.EventHandler(this.txtTopSpace_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(114, 53);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 15;
            this.label25.Text = "右部间距";
            // 
            // txtLeftSpace
            // 
            this.txtLeftSpace.Location = new System.Drawing.Point(65, 50);
            this.txtLeftSpace.Name = "txtLeftSpace";
            this.txtLeftSpace.Size = new System.Drawing.Size(43, 21);
            this.txtLeftSpace.TabIndex = 14;
            this.txtLeftSpace.Text = "1";
            this.txtLeftSpace.TextChanged += new System.EventHandler(this.txtTopSpace_TextChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 53);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 13;
            this.label26.Text = "左部间距";
            // 
            // txtBottomSpace
            // 
            this.txtBottomSpace.Location = new System.Drawing.Point(173, 24);
            this.txtBottomSpace.Name = "txtBottomSpace";
            this.txtBottomSpace.Size = new System.Drawing.Size(43, 21);
            this.txtBottomSpace.TabIndex = 12;
            this.txtBottomSpace.Text = "1";
            this.txtBottomSpace.TextChanged += new System.EventHandler(this.txtTopSpace_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(114, 27);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 11;
            this.label23.Text = "底部间距";
            // 
            // txtTopSpace
            // 
            this.txtTopSpace.Location = new System.Drawing.Point(65, 24);
            this.txtTopSpace.Name = "txtTopSpace";
            this.txtTopSpace.Size = new System.Drawing.Size(43, 21);
            this.txtTopSpace.TabIndex = 10;
            this.txtTopSpace.Text = "1";
            this.txtTopSpace.TextChanged += new System.EventHandler(this.txtTopSpace_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 9;
            this.label24.Text = "顶部间距";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(275, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 18;
            this.label21.Text = "厘米";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(129, 133);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "厘米";
            // 
            // txtOutBorderWidth
            // 
            this.txtOutBorderWidth.Location = new System.Drawing.Point(74, 127);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new System.Drawing.Size(43, 21);
            this.txtOutBorderWidth.TabIndex = 12;
            this.txtOutBorderWidth.Text = "1";
            this.txtOutBorderWidth.TextChanged += new System.EventHandler(this.txtOutBorderWidth_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 11;
            this.label20.Text = "外框宽";
            // 
            // chkMapGrid
            // 
            this.chkMapGrid.AutoSize = true;
            this.chkMapGrid.Location = new System.Drawing.Point(195, 316);
            this.chkMapGrid.Name = "chkMapGrid";
            this.chkMapGrid.Size = new System.Drawing.Size(72, 16);
            this.chkMapGrid.TabIndex = 89;
            this.chkMapGrid.Text = "使用格网";
            this.chkMapGrid.UseVisualStyleBackColor = true;
            this.chkMapGrid.Visible = false;
            this.chkMapGrid.CheckedChanged += new System.EventHandler(this.chkMapGrid_CheckedChanged);
            // 
            // cboStyle
            // 
            this.cboStyle.BackColor = System.Drawing.SystemColors.Window;
            this.cboStyle.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyle.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboStyle.Items.AddRange(new object[] {
            "按地图页面大小生成",
            "按数据范围生成"});
            this.cboStyle.Location = new System.Drawing.Point(504, 191);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboStyle.Size = new System.Drawing.Size(45, 22);
            this.cboStyle.TabIndex = 94;
            this.cboStyle.SelectedIndexChanged += new System.EventHandler(this.cboStyle_SelectedIndexChanged);
            // 
            // panelTKYS
            // 
            this.panelTKYS.Controls.Add(this.rdoTrapezoid);
            this.panelTKYS.Controls.Add(this.rdoRect);
            this.panelTKYS.Controls.Add(this.label17);
            this.panelTKYS.Location = new System.Drawing.Point(15, 35);
            this.panelTKYS.Name = "panelTKYS";
            this.panelTKYS.Size = new System.Drawing.Size(425, 25);
            this.panelTKYS.TabIndex = 96;
            // 
            // rdoTrapezoid
            // 
            this.rdoTrapezoid.AutoSize = true;
            this.rdoTrapezoid.Location = new System.Drawing.Point(189, 6);
            this.rdoTrapezoid.Name = "rdoTrapezoid";
            this.rdoTrapezoid.Size = new System.Drawing.Size(47, 16);
            this.rdoTrapezoid.TabIndex = 9;
            this.rdoTrapezoid.Text = "梯形";
            this.rdoTrapezoid.UseVisualStyleBackColor = true;
            // 
            // rdoRect
            // 
            this.rdoRect.AutoSize = true;
            this.rdoRect.Checked = true;
            this.rdoRect.Location = new System.Drawing.Point(81, 6);
            this.rdoRect.Name = "rdoRect";
            this.rdoRect.Size = new System.Drawing.Size(47, 16);
            this.rdoRect.TabIndex = 8;
            this.rdoRect.TabStop = true;
            this.rdoRect.Text = "矩形";
            this.rdoRect.UseVisualStyleBackColor = true;
            this.rdoRect.CheckedChanged += new System.EventHandler(this.rdoRect_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 7;
            this.label17.Text = "图框样式";
            // 
            // panelScale
            // 
            this.panelScale.Controls.Add(this.txtScale);
            this.panelScale.Controls.Add(this.label7);
            this.panelScale.Location = new System.Drawing.Point(0, 276);
            this.panelScale.Name = "panelScale";
            this.panelScale.Size = new System.Drawing.Size(169, 34);
            this.panelScale.TabIndex = 97;
            // 
            // MapTemplateGeneralPage
            // 
            this.Controls.Add(this.panelScale);
            this.Controls.Add(this.panelTKYS);
            this.Controls.Add(this.cboStyle);
            this.Controls.Add(this.chkMapGrid);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBoxPage);
            this.Controls.Add(this.groupBox3);
            this.Name = "MapTemplateGeneralPage";
            this.Size = new System.Drawing.Size(479, 339);
            this.Load += new System.EventHandler(this.MapTemplateGeneralPage_Load);
            this.VisibleChanged += new System.EventHandler(this.MapTemplateGeneralPage_VisibleChanged);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private ComboBox cboStyle;
        private CheckBox checkBox1;
        private CheckBox chkMapGrid;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBoxPage;
        private IFillSymbol ifillSymbol_0;
        private ILineSymbol ilineSymbol_0;
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
    }
}