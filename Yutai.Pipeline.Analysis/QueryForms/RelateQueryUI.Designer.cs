
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class RelateQueryUI
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
            this.cmbPipeLine = new System.Windows.Forms.ComboBox();
            this.cmbPipePoint = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstBoxPipeLineValues = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPipeLineFields = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstBoxPipePointValues = new System.Windows.Forms.ListBox();
            this.cmbPipePointFields = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPipeLineQuery = new System.Windows.Forms.Button();
            this.btnPipePointQuery = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.GeometrySet = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPipeLine
            // 
            this.cmbPipeLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipeLine.FormattingEnabled = true;
            this.cmbPipeLine.Location = new System.Drawing.Point(6, 20);
            this.cmbPipeLine.Name = "cmbPipeLine";
            this.cmbPipeLine.Size = new System.Drawing.Size(153, 20);
            this.cmbPipeLine.TabIndex = 1;
            this.cmbPipeLine.SelectedIndexChanged += new System.EventHandler(this.cmbPipeLine_SelectedIndexChanged);
            // 
            // cmbPipePoint
            // 
            this.cmbPipePoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipePoint.FormattingEnabled = true;
            this.cmbPipePoint.Location = new System.Drawing.Point(6, 20);
            this.cmbPipePoint.Name = "cmbPipePoint";
            this.cmbPipePoint.Size = new System.Drawing.Size(155, 20);
            this.cmbPipePoint.TabIndex = 3;
            this.cmbPipePoint.SelectedIndexChanged += new System.EventHandler(this.cmbPipePoint_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lstBoxPipeLineValues);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbPipeLineFields);
            this.groupBox1.Controls.Add(this.cmbPipeLine);
            this.groupBox1.Location = new System.Drawing.Point(7, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 227);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "线层";
            // 
            // lstBoxPipeLineValues
            // 
            this.lstBoxPipeLineValues.FormattingEnabled = true;
            this.lstBoxPipeLineValues.ItemHeight = 12;
            this.lstBoxPipeLineValues.Location = new System.Drawing.Point(6, 85);
            this.lstBoxPipeLineValues.Name = "lstBoxPipeLineValues";
            this.lstBoxPipeLineValues.Size = new System.Drawing.Size(153, 112);
            this.lstBoxPipeLineValues.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "字段：";
            // 
            // cmbPipeLineFields
            // 
            this.cmbPipeLineFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipeLineFields.FormattingEnabled = true;
            this.cmbPipeLineFields.Location = new System.Drawing.Point(6, 59);
            this.cmbPipeLineFields.Name = "cmbPipeLineFields";
            this.cmbPipeLineFields.Size = new System.Drawing.Size(153, 20);
            this.cmbPipeLineFields.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.lstBoxPipePointValues);
            this.groupBox2.Controls.Add(this.cmbPipePointFields);
            this.groupBox2.Controls.Add(this.cmbPipePoint);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(204, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 227);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "点层";
            // 
            // lstBoxPipePointValues
            // 
            this.lstBoxPipePointValues.FormattingEnabled = true;
            this.lstBoxPipePointValues.ItemHeight = 12;
            this.lstBoxPipePointValues.Location = new System.Drawing.Point(6, 85);
            this.lstBoxPipePointValues.Name = "lstBoxPipePointValues";
            this.lstBoxPipePointValues.Size = new System.Drawing.Size(155, 112);
            this.lstBoxPipePointValues.TabIndex = 4;
            // 
            // cmbPipePointFields
            // 
            this.cmbPipePointFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipePointFields.FormattingEnabled = true;
            this.cmbPipePointFields.Location = new System.Drawing.Point(6, 59);
            this.cmbPipePointFields.Name = "cmbPipePointFields";
            this.cmbPipePointFields.Size = new System.Drawing.Size(155, 20);
            this.cmbPipePointFields.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "字段：";
            // 
            // btnPipeLineQuery
            // 
            this.btnPipeLineQuery.Location = new System.Drawing.Point(84, 256);
            this.btnPipeLineQuery.Name = "btnPipeLineQuery";
            this.btnPipeLineQuery.Size = new System.Drawing.Size(93, 23);
            this.btnPipeLineQuery.TabIndex = 5;
            this.btnPipeLineQuery.Text = "查询管线(&L)";
            this.btnPipeLineQuery.UseVisualStyleBackColor = true;
            this.btnPipeLineQuery.Click += new System.EventHandler(this.btnPipeLineQuery_Click);
            // 
            // btnPipePointQuery
            // 
            this.btnPipePointQuery.Location = new System.Drawing.Point(181, 256);
            this.btnPipePointQuery.Name = "btnPipePointQuery";
            this.btnPipePointQuery.Size = new System.Drawing.Size(93, 23);
            this.btnPipePointQuery.TabIndex = 6;
            this.btnPipePointQuery.Text = "查询管点(&P)";
            this.btnPipePointQuery.UseVisualStyleBackColor = true;
            this.btnPipePointQuery.Click += new System.EventHandler(this.btnPipePointQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(278, 256);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // GeometrySet
            // 
            this.GeometrySet.AutoSize = true;
            this.GeometrySet.Location = new System.Drawing.Point(12, 260);
            this.GeometrySet.Name = "GeometrySet";
            this.GeometrySet.Size = new System.Drawing.Size(72, 16);
            this.GeometrySet.TabIndex = 23;
            this.GeometrySet.Text = "空间范围";
            this.GeometrySet.UseVisualStyleBackColor = true;
            this.GeometrySet.Click += new System.EventHandler(this.GeometrySet_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "获取唯一值";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 198);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "获取唯一值";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RelateQueryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 295);
            this.Controls.Add(this.GeometrySet);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPipePointQuery);
            this.Controls.Add(this.btnPipeLineQuery);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RelateQueryUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "关联查询";
            this.Load += new System.EventHandler(this.RelateQueryUI_Load);
            this.VisibleChanged += new System.EventHandler(this.RelateQueryUI_VisibleChanged);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.RelateQueryUI_HelpRequested);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private IContainer components = null;
		private ComboBox cmbPipeLine;
		private ComboBox cmbPipePoint;
		private GroupBox groupBox1;
		private ListBox lstBoxPipeLineValues;
		private Label label3;
		private ComboBox cmbPipeLineFields;
		private GroupBox groupBox2;
		private ListBox lstBoxPipePointValues;
		private ComboBox cmbPipePointFields;
		private Label label4;
		private Button btnPipeLineQuery;
		private Button btnPipePointQuery;
		private Button btnClose;
		private CheckBox GeometrySet;
		private QueryResult resultDlg;
        private Button button1;
        private Button button2;
    }
}