using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    partial class TrailsSetCtrl
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtDistance = new TextBox();
            this.txtPointNum = new TextBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.btnPointSymbol = new NewSymbolButton();
            this.chkPointTrail = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.txtLineLength = new TextBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.btnLineSymbol = new NewSymbolButton();
            this.chkLineTrail = new CheckBox();
            this.cboSpeedColorRamp = new ColorRampComboBox();
            this.label6 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboSpeedColorRamp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDistance);
            this.groupBox1.Controls.Add(this.txtPointNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPointSymbol);
            this.groupBox1.Controls.Add(this.chkPointTrail);
            this.groupBox1.Location = new Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(298, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "点轨迹";
            this.txtDistance.Location = new Point(92, 161);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new Size(95, 21);
            this.txtDistance.TabIndex = 6;
            this.txtDistance.TextChanged += new EventHandler(this.txtDistance_TextChanged);
            this.txtPointNum.Location = new Point(92, 137);
            this.txtPointNum.Name = "txtPointNum";
            this.txtPointNum.Size = new Size(95, 21);
            this.txtPointNum.TabIndex = 5;
            this.txtPointNum.TextChanged += new EventHandler(this.txtPointNum_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(10, 164);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "间距:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 140);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "点数:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "符号:";
            this.btnPointSymbol.Location = new Point(92, 50);
            this.btnPointSymbol.Name = "btnPointSymbol";
            this.btnPointSymbol.Size = new Size(70, 39);
            this.btnPointSymbol.Style = null;
            this.btnPointSymbol.TabIndex = 1;
            this.btnPointSymbol.Click += new EventHandler(this.btnPointSymbol_Click);
            this.chkPointTrail.AutoSize = true;
            this.chkPointTrail.Location = new Point(12, 26);
            this.chkPointTrail.Name = "chkPointTrail";
            this.chkPointTrail.Size = new Size(84, 16);
            this.chkPointTrail.TabIndex = 0;
            this.chkPointTrail.Text = "显示点轨迹";
            this.chkPointTrail.UseVisualStyleBackColor = true;
            this.chkPointTrail.CheckedChanged += new EventHandler(this.chkPointTrail_CheckedChanged);
            this.groupBox2.Controls.Add(this.txtLineLength);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnLineSymbol);
            this.groupBox2.Controls.Add(this.chkLineTrail);
            this.groupBox2.Location = new Point(8, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(298, 135);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线轨迹";
            this.txtLineLength.Location = new Point(76, 98);
            this.txtLineLength.Name = "txtLineLength";
            this.txtLineLength.Size = new Size(95, 21);
            this.txtLineLength.TabIndex = 6;
            this.txtLineLength.TextChanged += new EventHandler(this.txtLineLength_TextChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(10, 101);
            this.label5.Name = "label5";
            this.label5.Size = new Size(47, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "线长度:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(10, 58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "符号:";
            this.btnLineSymbol.Location = new Point(76, 42);
            this.btnLineSymbol.Name = "btnLineSymbol";
            this.btnLineSymbol.Size = new Size(100, 39);
            this.btnLineSymbol.Style = null;
            this.btnLineSymbol.TabIndex = 2;
            this.btnLineSymbol.Click += new EventHandler(this.btnLineSymbol_Click);
            this.chkLineTrail.AutoSize = true;
            this.chkLineTrail.Location = new Point(12, 20);
            this.chkLineTrail.Name = "chkLineTrail";
            this.chkLineTrail.Size = new Size(84, 16);
            this.chkLineTrail.TabIndex = 1;
            this.chkLineTrail.Text = "显示线轨迹";
            this.chkLineTrail.UseVisualStyleBackColor = true;
            this.chkLineTrail.CheckedChanged += new EventHandler(this.chkLineTrail_CheckedChanged);
            this.cboSpeedColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboSpeedColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSpeedColorRamp.FormattingEnabled = true;
            this.cboSpeedColorRamp.Location = new Point(92, 102);
            this.cboSpeedColorRamp.Name = "cboSpeedColorRamp";
            this.cboSpeedColorRamp.Size = new Size(189, 22);
            this.cboSpeedColorRamp.TabIndex = 13;
            this.cboSpeedColorRamp.SelectedIndexChanged += new EventHandler(this.cboSpeedColorRamp_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(10, 105);
            this.label6.Name = "label6";
            this.label6.Size = new Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "符号范围";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "TrailsSetCtrl";
            base.Size = new Size(319, 369);
            base.Load += new EventHandler(this.TrailsSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

       

       
        private IContainer components = null;
        private NewSymbolButton btnLineSymbol;
        private NewSymbolButton btnPointSymbol;
        private ColorRampComboBox cboSpeedColorRamp;
        private CheckBox chkLineTrail;
        private CheckBox chkPointTrail;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtDistance;
        private TextBox txtLineLength;
        private TextBox txtPointNum;
    }
}