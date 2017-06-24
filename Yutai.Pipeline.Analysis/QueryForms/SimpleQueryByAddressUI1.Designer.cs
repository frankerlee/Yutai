
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
			this.groupBox2 = new GroupBox();
			this.FillAllBut = new Button();
			this.WipeBut = new Button();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.ValueBox = new CheckedListBox();
			this.FieldBox = new TextBox();
			this.BlurCheck = new CheckBox();
			this.CloseBut = new Button();
			this.QueryBut = new Button();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.FieldValueBox = new ComboBox();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.FillAllBut);
			this.groupBox2.Controls.Add(this.WipeBut);
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(3, 101);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 191);
			this.groupBox2.TabIndex = 41;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查询对象：点性";
			this.FillAllBut.Location = new System.Drawing.Point(208, 162);
			this.FillAllBut.Name = "FillAllBut";
			this.FillAllBut.Size = new Size(84, 23);
			this.FillAllBut.TabIndex = 5;
			this.FillAllBut.Text = "显示全部(&S)";
			this.FillAllBut.UseVisualStyleBackColor = true;
			this.FillAllBut.Click += new EventHandler(this.FillAllBut_Click);
			this.WipeBut.Location = new System.Drawing.Point(209, 137);
			this.WipeBut.Name = "WipeBut";
			this.WipeBut.Size = new Size(83, 23);
			this.WipeBut.TabIndex = 4;
			this.WipeBut.Text = "滤除多余(&W)";
			this.WipeBut.UseVisualStyleBackColor = true;
			this.WipeBut.Click += new EventHandler(this.WipeBut_Click);
			this.RevBut.Location = new System.Drawing.Point(209, 81);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(83, 23);
			this.RevBut.TabIndex = 3;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(209, 52);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(83, 23);
			this.NoneBut.TabIndex = 2;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.AllBut.Location = new System.Drawing.Point(209, 24);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(83, 23);
			this.AllBut.TabIndex = 1;
			this.AllBut.Text = "全选(&A)";
			this.AllBut.UseVisualStyleBackColor = true;
			this.AllBut.Click += new EventHandler(this.AllBut_Click);
			this.ValueBox.CheckOnClick = true;
			this.ValueBox.FormattingEnabled = true;
			this.ValueBox.Items.AddRange(new object[]
			{
				"sdfsfsfs",
				"sdsdf",
				"sfdsdf"
			});
			this.ValueBox.Location = new System.Drawing.Point(10, 17);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(166, 164);
			this.ValueBox.Sorted = true;
			this.ValueBox.TabIndex = 0;
			this.FieldBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.FieldBox.Location = new System.Drawing.Point(4, 71);
			this.FieldBox.Name = "FieldBox";
			this.FieldBox.ReadOnly = true;
			this.FieldBox.Size = new Size(55, 14);
			this.FieldBox.TabIndex = 39;
			this.FieldBox.Text = "FieldBox";
			this.BlurCheck.AutoSize = true;
			this.BlurCheck.Location = new System.Drawing.Point(321, 276);
			this.BlurCheck.Name = "BlurCheck";
			this.BlurCheck.Size = new Size(72, 16);
			this.BlurCheck.TabIndex = 38;
			this.BlurCheck.Text = "模糊查询";
			this.BlurCheck.UseVisualStyleBackColor = true;
			this.BlurCheck.CheckedChanged += new EventHandler(this.BlurCheck_CheckedChanged);
			this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(316, 39);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 37;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.QueryBut.Location = new System.Drawing.Point(316, 10);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(75, 23);
			this.QueryBut.TabIndex = 36;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(131, 10);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(59, 16);
			this.radioButton2.TabIndex = 35;
			this.radioButton2.Text = "管线层";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(12, 10);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(59, 16);
			this.radioButton1.TabIndex = 34;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "管点层";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(32, 33);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 33;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(1, 37);
			this.lable.Name = "lable";
			this.lable.Size = new Size(29, 12);
			this.lable.TabIndex = 32;
			this.lable.Text = "图层";
			this.FieldValueBox.FormattingEnabled = true;
			this.FieldValueBox.Location = new System.Drawing.Point(63, 68);
			this.FieldValueBox.Name = "FieldValueBox";
			this.FieldValueBox.Size = new Size(237, 20);
			this.FieldValueBox.TabIndex = 42;
			this.FieldValueBox.SelectedIndexChanged += new EventHandler(this.FieldValueBox_SelectedIndexChanged);
			this.FieldValueBox.TextUpdate += new EventHandler(this.FieldValueBox_TextUpdate);
			this.FieldValueBox.TextChanged += new EventHandler(this.FieldValueBox_TextChanged);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(403, 310);
			base.Controls.Add(this.FieldValueBox);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.FieldBox);
			base.Controls.Add(this.BlurCheck);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByAddressUI1";
			base.ShowInTaskbar = false;
			this.Text = "快速查询-按地址";
			base.Load += new EventHandler(this.SimpleQueryByAddressUI1_Load);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByAddressUI1_HelpRequested);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
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