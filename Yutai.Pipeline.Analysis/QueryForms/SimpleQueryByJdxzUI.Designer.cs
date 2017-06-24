using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
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
	    partial class SimpleQueryByJdxzUI
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
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.CloseBut = new Button();
			this.QueryBut = new Button();
			this.groupBox2 = new GroupBox();
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.AllBut = new Button();
			this.ValueBox = new CheckedListBox();
			this.groupBox1 = new GroupBox();
			this.SqlBox = new TextBox();
			this.GeometrySet = new CheckBox();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(54, 12);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 11;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(5, 15);
			this.lable.Name = "lable";
			this.lable.Size = new Size(41, 12);
			this.lable.TabIndex = 10;
			this.lable.Text = "管点层";
			this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(321, 236);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(67, 25);
			this.CloseBut.TabIndex = 9;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.QueryBut.Location = new System.Drawing.Point(321, 25);
			this.QueryBut.Name = "QueryBut";
			this.QueryBut.Size = new Size(67, 25);
			this.QueryBut.TabIndex = 8;
			this.QueryBut.Text = "查询(&Q)";
			this.QueryBut.UseVisualStyleBackColor = true;
			this.QueryBut.Click += new EventHandler(this.QueryBut_Click);
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Location = new System.Drawing.Point(5, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 191);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查询对象";
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
			this.groupBox1.Controls.Add(this.SqlBox);
			this.groupBox1.Location = new System.Drawing.Point(0, 43);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(303, 41);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查询条件";
			this.SqlBox.Location = new System.Drawing.Point(10, 13);
			this.SqlBox.Name = "SqlBox";
			this.SqlBox.ReadOnly = true;
			this.SqlBox.Size = new Size(287, 21);
			this.SqlBox.TabIndex = 0;
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(213, 14);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 12;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(395, 297);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.QueryBut);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByJdxzUI";
			base.ShowInTaskbar = false;
			this.Text = "快速查询－按点性";
			base.Load += new EventHandler(this.SimpleQueryByJdxzUI_Load);
			base.VisibleChanged += new EventHandler(this.SimpleQueryByJdxzUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByJdxzUI_HelpRequested);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer components = null;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private string strDX;
		private IField myfieldDX;
		private QueryResult resultDlg;
		private ComboBox LayerBox;
		private Label lable;
		private Button CloseBut;
		private Button QueryBut;
		private GroupBox groupBox2;
		private Button NoneBut;
		private Button AllBut;
		private CheckedListBox ValueBox;
		private GroupBox groupBox1;
		private TextBox SqlBox;
		private Button RevBut;
		private CheckBox GeometrySet;
    }
}