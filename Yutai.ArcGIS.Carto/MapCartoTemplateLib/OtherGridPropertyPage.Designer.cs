using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
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
    partial class OtherGridPropertyPage
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
            this.groupBox1 = new GroupBox();
            this.rdoNone = new RadioButton();
            this.btnStyle = new StyleButton();
            this.rdoTick = new RadioButton();
            this.rdoLine = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.label4 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBox();
            this.cboFontName = new ComboBox();
            this.groupBox3 = new GroupBox();
            this.txtAnnUnitScale = new TextBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.cboAnnoUnit = new ComboBox();
            this.chkDrawCornerShortLine = new CheckBox();
            this.chkDrawRoundLineShortLine = new CheckBox();
            this.chkDrawJWD = new CheckBox();
            this.chkDrawRoundText = new CheckBox();
            this.chkRoundText = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoNone);
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.rdoTick);
            this.groupBox1.Controls.Add(this.rdoLine);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(264, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "格网显示属性";
            this.rdoNone.AutoSize = true;
            this.rdoNone.Location = new Point(199, 21);
            this.rdoNone.Name = "rdoNone";
            this.rdoNone.Size = new Size(35, 16);
            this.rdoNone.TabIndex = 3;
            this.rdoNone.Text = "无";
            this.rdoNone.UseVisualStyleBackColor = true;
            this.rdoNone.CheckedChanged += new EventHandler(this.rdoNone_CheckedChanged);
            this.btnStyle.Location = new Point(64, 43);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 32);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 2;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.rdoTick.AutoSize = true;
            this.rdoTick.Checked = true;
            this.rdoTick.Location = new Point(129, 21);
            this.rdoTick.Name = "rdoTick";
            this.rdoTick.Size = new Size(47, 16);
            this.rdoTick.TabIndex = 1;
            this.rdoTick.TabStop = true;
            this.rdoTick.Text = "十字";
            this.rdoTick.UseVisualStyleBackColor = true;
            this.rdoTick.CheckedChanged += new EventHandler(this.rdoTick_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Location = new Point(19, 21);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(59, 16);
            this.rdoLine.TabIndex = 0;
            this.rdoLine.Text = "网格线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboFontSize);
            this.groupBox2.Controls.Add(this.cboFontName);
            this.groupBox2.Location = new Point(8, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(264, 74);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注样式";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(17, 41);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "大小:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "字体:";
            this.cboFontSize.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Location = new Point(57, 41);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new Size(64, 20);
            this.cboFontSize.TabIndex = 32;
            this.cboFontSize.Text = "5";
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.cboFontName.Location = new Point(57, 17);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(184, 20);
            this.cboFontName.TabIndex = 31;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.txtAnnUnitScale);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cboAnnoUnit);
            this.groupBox3.Location = new Point(8, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(264, 74);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "标注样式";
            this.groupBox3.Visible = false;
            this.txtAnnUnitScale.Location = new Point(76, 44);
            this.txtAnnUnitScale.Name = "txtAnnUnitScale";
            this.txtAnnUnitScale.Size = new Size(165, 21);
            this.txtAnnUnitScale.TabIndex = 35;
            this.txtAnnUnitScale.Text = "1";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(17, 41);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "单位倍数";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "标注单位";
            this.cboAnnoUnit.Items.AddRange(new object[] { "米", "公里" });
            this.cboAnnoUnit.Location = new Point(76, 17);
            this.cboAnnoUnit.Name = "cboAnnoUnit";
            this.cboAnnoUnit.Size = new Size(165, 20);
            this.cboAnnoUnit.TabIndex = 31;
            this.cboAnnoUnit.Text = "米";
            this.chkDrawCornerShortLine.AutoSize = true;
            this.chkDrawCornerShortLine.Location = new Point(289, 14);
            this.chkDrawCornerShortLine.Name = "chkDrawCornerShortLine";
            this.chkDrawCornerShortLine.Size = new Size(96, 16);
            this.chkDrawCornerShortLine.TabIndex = 8;
            this.chkDrawCornerShortLine.Text = "绘制四角短线";
            this.chkDrawCornerShortLine.UseVisualStyleBackColor = true;
            this.chkDrawCornerShortLine.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkDrawRoundLineShortLine.AutoSize = true;
            this.chkDrawRoundLineShortLine.Location = new Point(289, 62);
            this.chkDrawRoundLineShortLine.Name = "chkDrawRoundLineShortLine";
            this.chkDrawRoundLineShortLine.Size = new Size(96, 16);
            this.chkDrawRoundLineShortLine.TabIndex = 9;
            this.chkDrawRoundLineShortLine.Text = "绘制四周短线";
            this.chkDrawRoundLineShortLine.UseVisualStyleBackColor = true;
            this.chkDrawRoundLineShortLine.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkDrawJWD.AutoSize = true;
            this.chkDrawJWD.Location = new Point(289, 106);
            this.chkDrawJWD.Name = "chkDrawJWD";
            this.chkDrawJWD.Size = new Size(96, 16);
            this.chkDrawJWD.TabIndex = 10;
            this.chkDrawJWD.Text = "绘制经纬度值";
            this.chkDrawJWD.UseVisualStyleBackColor = true;
            this.chkDrawJWD.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkDrawRoundText.AutoSize = true;
            this.chkDrawRoundText.Location = new Point(289, 37);
            this.chkDrawRoundText.Name = "chkDrawRoundText";
            this.chkDrawRoundText.Size = new Size(96, 16);
            this.chkDrawRoundText.TabIndex = 11;
            this.chkDrawRoundText.Text = "绘制四角注记";
            this.chkDrawRoundText.UseVisualStyleBackColor = true;
            this.chkDrawRoundText.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkRoundText.AutoSize = true;
            this.chkRoundText.Location = new Point(289, 84);
            this.chkRoundText.Name = "chkRoundText";
            this.chkRoundText.Size = new Size(96, 16);
            this.chkRoundText.TabIndex = 12;
            this.chkRoundText.Text = "绘制四周注记";
            this.chkRoundText.UseVisualStyleBackColor = true;
            this.chkRoundText.CheckedChanged += new EventHandler(this.chkRoundText_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.chkRoundText);
            base.Controls.Add(this.chkDrawRoundText);
            base.Controls.Add(this.chkDrawJWD);
            base.Controls.Add(this.chkDrawRoundLineShortLine);
            base.Controls.Add(this.chkDrawCornerShortLine);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "OtherGridPropertyPage";
            base.Size = new Size(493, 251);
            base.Load += new EventHandler(this.OtherGridPropertyPage_Load);
            base.VisibleChanged += new EventHandler(this.OtherGridPropertyPage_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private StyleButton btnStyle;
        private ComboBox cboAnnoUnit;
        private ComboBox cboFontName;
        private ComboBox cboFontSize;
        private CheckBox chkDrawCornerShortLine;
        private CheckBox chkDrawJWD;
        private CheckBox chkDrawRoundLineShortLine;
        private CheckBox chkDrawRoundText;
        private CheckBox chkRoundText;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ILineSymbol ilineSymbol_0;
        private IMarkerSymbol imarkerSymbol_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioButton rdoLine;
        private RadioButton rdoNone;
        private RadioButton rdoTick;
        private TextBox txtAnnUnitScale;
    }
}