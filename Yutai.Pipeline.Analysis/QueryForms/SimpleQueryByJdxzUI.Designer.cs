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
            this.LayerBox = new System.Windows.Forms.ComboBox();
            this.lable = new System.Windows.Forms.Label();
            this.CloseBut = new System.Windows.Forms.Button();
            this.QueryBut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnGetUniqueValue = new System.Windows.Forms.Button();
            this.RevBut = new System.Windows.Forms.Button();
            this.NoneBut = new System.Windows.Forms.Button();
            this.AllBut = new System.Windows.Forms.Button();
            this.ValueBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SqlBox = new System.Windows.Forms.TextBox();
            this.GeometrySet = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayerBox
            // 
            this.LayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerBox.FormattingEnabled = true;
            this.LayerBox.Location = new System.Drawing.Point(54, 12);
            this.LayerBox.Name = "LayerBox";
            this.LayerBox.Size = new System.Drawing.Size(148, 20);
            this.LayerBox.TabIndex = 11;
            this.LayerBox.SelectedIndexChanged += new System.EventHandler(this.LayerBox_SelectedIndexChanged);
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Location = new System.Drawing.Point(5, 15);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(41, 12);
            this.lable.TabIndex = 10;
            this.lable.Text = "管点层";
            // 
            // CloseBut
            // 
            this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBut.Location = new System.Drawing.Point(321, 266);
            this.CloseBut.Name = "CloseBut";
            this.CloseBut.Size = new System.Drawing.Size(67, 25);
            this.CloseBut.TabIndex = 9;
            this.CloseBut.Text = "关闭(&C)";
            this.CloseBut.UseVisualStyleBackColor = true;
            this.CloseBut.Click += new System.EventHandler(this.CloseBut_Click);
            // 
            // QueryBut
            // 
            this.QueryBut.Location = new System.Drawing.Point(321, 9);
            this.QueryBut.Name = "QueryBut";
            this.QueryBut.Size = new System.Drawing.Size(67, 25);
            this.QueryBut.TabIndex = 8;
            this.QueryBut.Text = "查询(&Q)";
            this.QueryBut.UseVisualStyleBackColor = true;
            this.QueryBut.Click += new System.EventHandler(this.QueryBut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnGetUniqueValue);
            this.groupBox2.Controls.Add(this.RevBut);
            this.groupBox2.Controls.Add(this.NoneBut);
            this.groupBox2.Controls.Add(this.AllBut);
            this.groupBox2.Controls.Add(this.ValueBox);
            this.groupBox2.Location = new System.Drawing.Point(7, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 206);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择查询对象";
            // 
            // BtnGetUniqueValue
            // 
            this.BtnGetUniqueValue.Location = new System.Drawing.Point(206, 20);
            this.BtnGetUniqueValue.Name = "BtnGetUniqueValue";
            this.BtnGetUniqueValue.Size = new System.Drawing.Size(76, 23);
            this.BtnGetUniqueValue.TabIndex = 4;
            this.BtnGetUniqueValue.Text = "获取唯一值";
            this.BtnGetUniqueValue.UseVisualStyleBackColor = true;
            this.BtnGetUniqueValue.Click += new System.EventHandler(this.BtnGetUniqueValue_Click);
            // 
            // RevBut
            // 
            this.RevBut.Location = new System.Drawing.Point(206, 177);
            this.RevBut.Name = "RevBut";
            this.RevBut.Size = new System.Drawing.Size(76, 20);
            this.RevBut.TabIndex = 3;
            this.RevBut.Text = "反选(&I)";
            this.RevBut.UseVisualStyleBackColor = true;
            this.RevBut.Click += new System.EventHandler(this.RevBut_Click);
            // 
            // NoneBut
            // 
            this.NoneBut.Location = new System.Drawing.Point(206, 148);
            this.NoneBut.Name = "NoneBut";
            this.NoneBut.Size = new System.Drawing.Size(76, 20);
            this.NoneBut.TabIndex = 2;
            this.NoneBut.Text = "全不选(&N)";
            this.NoneBut.UseVisualStyleBackColor = true;
            this.NoneBut.Click += new System.EventHandler(this.NoneBut_Click);
            // 
            // AllBut
            // 
            this.AllBut.Location = new System.Drawing.Point(206, 119);
            this.AllBut.Name = "AllBut";
            this.AllBut.Size = new System.Drawing.Size(76, 20);
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
            this.ValueBox.Location = new System.Drawing.Point(10, 17);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(185, 180);
            this.ValueBox.Sorted = true;
            this.ValueBox.TabIndex = 0;
            this.ValueBox.SelectedIndexChanged += new System.EventHandler(this.ValueBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SqlBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 41);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // SqlBox
            // 
            this.SqlBox.Location = new System.Drawing.Point(6, 14);
            this.SqlBox.Name = "SqlBox";
            this.SqlBox.ReadOnly = true;
            this.SqlBox.Size = new System.Drawing.Size(276, 21);
            this.SqlBox.TabIndex = 0;
            // 
            // GeometrySet
            // 
            this.GeometrySet.AutoSize = true;
            this.GeometrySet.Location = new System.Drawing.Point(213, 14);
            this.GeometrySet.Name = "GeometrySet";
            this.GeometrySet.Size = new System.Drawing.Size(72, 16);
            this.GeometrySet.TabIndex = 12;
            this.GeometrySet.Text = "空间范围";
            this.GeometrySet.UseVisualStyleBackColor = true;
            // 
            // SimpleQueryByJdxzUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 297);
            this.Controls.Add(this.GeometrySet);
            this.Controls.Add(this.LayerBox);
            this.Controls.Add(this.lable);
            this.Controls.Add(this.CloseBut);
            this.Controls.Add(this.QueryBut);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SimpleQueryByJdxzUI";
            this.ShowInTaskbar = false;
            this.Text = "快速查询－按点性";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleQueryByDiaUI_FormClosed);
            this.Load += new System.EventHandler(this.SimpleQueryByJdxzUI_Load);
            this.VisibleChanged += new System.EventHandler(this.SimpleQueryByJdxzUI_VisibleChanged);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SimpleQueryByJdxzUI_HelpRequested);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button BtnGetUniqueValue;
    }
}