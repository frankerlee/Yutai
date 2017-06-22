
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleQueryByAddressUI
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
			this.CloseBut = new Button();
			this.QueryBut = new Button();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.BlurCheck = new CheckBox();
			this.FieldBox = new TextBox();
			this.SqlBox = new TextBox();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.groupBox2 = new GroupBox();
			this.ValueBox = new CheckedListBox();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.CloseBut.Location = new System.Drawing.Point(327, 41);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 27;
			this.CloseBut.Text = "关闭";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.QueryBut.Location = new System.Drawing.Point(327, 12);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(75, 23);
			this.QueryBut.TabIndex = 26;
			this.QueryBut.Text = "查询";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(142, 12);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(59, 16);
			this.radioButton2.TabIndex = 25;
			this.radioButton2.Text = "管线层";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(23, 12);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(59, 16);
			this.radioButton1.TabIndex = 24;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "管点层";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(43, 35);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 23;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(12, 39);
			this.lable.Name = "lable";
			this.lable.Size = new Size(29, 12);
			this.lable.TabIndex = 22;
			this.lable.Text = "图层";
			this.BlurCheck.AutoSize = true;
			this.BlurCheck.Location = new System.Drawing.Point(332, 278);
			this.BlurCheck.Name = "BlurCheck";
			this.BlurCheck.Size = new Size(72, 16);
			this.BlurCheck.TabIndex = 28;
			this.BlurCheck.Text = "模糊查询";
			this.BlurCheck.UseVisualStyleBackColor = true;
			this.BlurCheck.CheckedChanged += new EventHandler(this.BlurCheck_CheckedChanged);
			this.FieldBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.FieldBox.Location = new System.Drawing.Point(15, 73);
			this.FieldBox.Name = "FieldBox";
			this.FieldBox.ReadOnly = true;
			this.FieldBox.Size = new Size(55, 14);
			this.FieldBox.TabIndex = 29;
			this.FieldBox.Text = "FieldBox";
			this.SqlBox.Location = new System.Drawing.Point(76, 66);
			this.SqlBox.Name = "SqlBox";
			this.SqlBox.ReadOnly = true;
			this.SqlBox.Size = new Size(222, 21);
			this.SqlBox.TabIndex = 30;
			this.RevBut.Location = new System.Drawing.Point(208, 140);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(76, 23);
			this.RevBut.TabIndex = 3;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(208, 95);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(76, 23);
			this.NoneBut.TabIndex = 2;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.AllBut.Location = new System.Drawing.Point(208, 50);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(76, 23);
			this.AllBut.TabIndex = 1;
			this.AllBut.Text = "全选(&A)";
			this.AllBut.UseVisualStyleBackColor = true;
			this.AllBut.Click += new EventHandler(this.AllBut_Click);
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(14, 103);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 191);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查询对象";
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
			this.ValueBox.SelectedIndexChanged += new EventHandler(this.ValueBox_SelectedIndexChanged);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(416, 306);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.SqlBox);
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
			base.Name = "SimpleQueryByAddressUI";
			this.Text = "快速查询－按地址";
			base.Load += new EventHandler(this.SimpleQueryByAddressUI_Load);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByAddressUI_HelpRequested);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer components = null;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private QueryResult resultDlg;
		private Button CloseBut;
		private Button QueryBut;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private ComboBox LayerBox;
		private Label lable;
		private CheckBox BlurCheck;
		private TextBox FieldBox;
		private TextBox SqlBox;
		private Button RevBut;
		private Button NoneBut;
		private Button AllBut;
		private GroupBox groupBox2;
		private CheckedListBox ValueBox;
    }
}