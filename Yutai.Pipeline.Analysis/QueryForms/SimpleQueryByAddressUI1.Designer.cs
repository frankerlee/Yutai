
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleQueryByAddressUI1
    {
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FillAllBut = new System.Windows.Forms.Button();
            this.WipeBut = new System.Windows.Forms.Button();
            this.RevBut = new System.Windows.Forms.Button();
            this.NoneBut = new System.Windows.Forms.Button();
            this.AllBut = new System.Windows.Forms.Button();
            this.ValueBox = new System.Windows.Forms.CheckedListBox();
            this.FieldBox = new System.Windows.Forms.TextBox();
            this.BlurCheck = new System.Windows.Forms.CheckBox();
            this.CloseBut = new System.Windows.Forms.Button();
            this.QueryBut = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.LayerBox = new System.Windows.Forms.ComboBox();
            this.lable = new System.Windows.Forms.Label();
            this.FieldValueBox = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FillAllBut);
            this.groupBox2.Controls.Add(this.WipeBut);
            this.groupBox2.Controls.Add(this.RevBut);
            this.groupBox2.Controls.Add(this.NoneBut);
            this.groupBox2.Controls.Add(this.AllBut);
            this.groupBox2.Controls.Add(this.ValueBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 255);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择查询对象：点性";
            // 
            // FillAllBut
            // 
            this.FillAllBut.Location = new System.Drawing.Point(184, 225);
            this.FillAllBut.Name = "FillAllBut";
            this.FillAllBut.Size = new System.Drawing.Size(83, 23);
            this.FillAllBut.TabIndex = 5;
            this.FillAllBut.Text = "显示全部(&S)";
            this.FillAllBut.UseVisualStyleBackColor = true;
            this.FillAllBut.Click += new System.EventHandler(this.FillAllBut_Click);
            // 
            // WipeBut
            // 
            this.WipeBut.Location = new System.Drawing.Point(184, 196);
            this.WipeBut.Name = "WipeBut";
            this.WipeBut.Size = new System.Drawing.Size(83, 23);
            this.WipeBut.TabIndex = 4;
            this.WipeBut.Text = "滤除多余(&W)";
            this.WipeBut.UseVisualStyleBackColor = true;
            this.WipeBut.Click += new System.EventHandler(this.WipeBut_Click);
            // 
            // RevBut
            // 
            this.RevBut.Location = new System.Drawing.Point(184, 78);
            this.RevBut.Name = "RevBut";
            this.RevBut.Size = new System.Drawing.Size(83, 23);
            this.RevBut.TabIndex = 3;
            this.RevBut.Text = "反选(&I)";
            this.RevBut.UseVisualStyleBackColor = true;
            this.RevBut.Click += new System.EventHandler(this.RevBut_Click);
            // 
            // NoneBut
            // 
            this.NoneBut.Location = new System.Drawing.Point(184, 49);
            this.NoneBut.Name = "NoneBut";
            this.NoneBut.Size = new System.Drawing.Size(83, 23);
            this.NoneBut.TabIndex = 2;
            this.NoneBut.Text = "全不选(&N)";
            this.NoneBut.UseVisualStyleBackColor = true;
            this.NoneBut.Click += new System.EventHandler(this.NoneBut_Click);
            // 
            // AllBut
            // 
            this.AllBut.Location = new System.Drawing.Point(184, 20);
            this.AllBut.Name = "AllBut";
            this.AllBut.Size = new System.Drawing.Size(83, 23);
            this.AllBut.TabIndex = 1;
            this.AllBut.Text = "全选(&A)";
            this.AllBut.UseVisualStyleBackColor = true;
            this.AllBut.Click += new System.EventHandler(this.AllBut_Click);
            // 
            // ValueBox
            // 
            this.ValueBox.CheckOnClick = true;
            this.ValueBox.FormattingEnabled = true;
            this.ValueBox.Items.AddRange(new object[] {
            "sdfsfsfs",
            "sdsdf",
            "sfdsdf"});
            this.ValueBox.Location = new System.Drawing.Point(6, 20);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(172, 228);
            this.ValueBox.Sorted = true;
            this.ValueBox.TabIndex = 0;
            // 
            // FieldBox
            // 
            this.FieldBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FieldBox.Location = new System.Drawing.Point(12, 63);
            this.FieldBox.Name = "FieldBox";
            this.FieldBox.ReadOnly = true;
            this.FieldBox.Size = new System.Drawing.Size(55, 14);
            this.FieldBox.TabIndex = 39;
            this.FieldBox.Text = "FieldBox";
            // 
            // BlurCheck
            // 
            this.BlurCheck.AutoSize = true;
            this.BlurCheck.Location = new System.Drawing.Point(306, 325);
            this.BlurCheck.Name = "BlurCheck";
            this.BlurCheck.Size = new System.Drawing.Size(72, 16);
            this.BlurCheck.TabIndex = 38;
            this.BlurCheck.Text = "模糊查询";
            this.BlurCheck.UseVisualStyleBackColor = true;
            // 
            // CloseBut
            // 
            this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBut.Location = new System.Drawing.Point(306, 41);
            this.CloseBut.Name = "CloseBut";
            this.CloseBut.Size = new System.Drawing.Size(75, 23);
            this.CloseBut.TabIndex = 37;
            this.CloseBut.Text = "关闭(&C)";
            this.CloseBut.UseVisualStyleBackColor = true;
            this.CloseBut.Click += new System.EventHandler(this.CloseBut_Click);
            // 
            // QueryBut
            // 
            this.QueryBut.Location = new System.Drawing.Point(306, 12);
            this.QueryBut.Name = "QueryBut";
            this.QueryBut.Size = new System.Drawing.Size(75, 23);
            this.QueryBut.TabIndex = 36;
            this.QueryBut.Text = "查询(&Q)";
            this.QueryBut.UseVisualStyleBackColor = true;
            this.QueryBut.Click += new System.EventHandler(this.QueryBut_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(131, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 35;
            this.radioButton2.Text = "管线层";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 34;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "管点层";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // LayerBox
            // 
            this.LayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerBox.FormattingEnabled = true;
            this.LayerBox.Location = new System.Drawing.Point(73, 34);
            this.LayerBox.Name = "LayerBox";
            this.LayerBox.Size = new System.Drawing.Size(150, 20);
            this.LayerBox.TabIndex = 33;
            this.LayerBox.SelectedIndexChanged += new System.EventHandler(this.LayerBox_SelectedIndexChanged);
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Location = new System.Drawing.Point(12, 37);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(29, 12);
            this.lable.TabIndex = 32;
            this.lable.Text = "图层";
            // 
            // FieldValueBox
            // 
            this.FieldValueBox.FormattingEnabled = true;
            this.FieldValueBox.Location = new System.Drawing.Point(73, 60);
            this.FieldValueBox.Name = "FieldValueBox";
            this.FieldValueBox.Size = new System.Drawing.Size(227, 20);
            this.FieldValueBox.TabIndex = 42;
            // 
            // SimpleQueryByAddressUI1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 348);
            this.Controls.Add(this.FieldValueBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FieldBox);
            this.Controls.Add(this.BlurCheck);
            this.Controls.Add(this.CloseBut);
            this.Controls.Add(this.QueryBut);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.LayerBox);
            this.Controls.Add(this.lable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SimpleQueryByAddressUI1";
            this.ShowInTaskbar = false;
            this.Text = "快速查询-按地址";
            this.Load += new System.EventHandler(this.SimpleQueryByAddressUI1_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SimpleQueryByAddressUI1_HelpRequested);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private IContainer components = null;
		private GroupBox groupBox2;
		private Button RevBut;
		private Button NoneBut;
		private Button AllBut;
		private CheckedListBox ValueBox;
		private TextBox FieldBox;
		private CheckBox BlurCheck;
		private Button CloseBut;
		private Button QueryBut;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private ComboBox LayerBox;
		private Label lable;
		private ComboBox FieldValueBox;
		private Button WipeBut;
		private Button FillAllBut;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private QueryResult resultDlg;
		private bool bControlEvent;
    }
}