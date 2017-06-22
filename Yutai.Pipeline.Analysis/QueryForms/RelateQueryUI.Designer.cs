
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
			this.cmbPipeLine = new ComboBox();
			this.cmbPipePoint = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.lstBoxPipeLineValues = new ListBox();
			this.label3 = new Label();
			this.cmbPipeLineFields = new ComboBox();
			this.groupBox2 = new GroupBox();
			this.lstBoxPipePointValues = new ListBox();
			this.cmbPipePointFields = new ComboBox();
			this.label4 = new Label();
			this.btnPipeLineQuery = new Button();
			this.btnPipePointQuery = new Button();
			this.btnClose = new Button();
			this.GeometrySet = new CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.cmbPipeLine.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipeLine.FormattingEnabled = true;
			this.cmbPipeLine.Location = new System.Drawing.Point(17, 27);
			this.cmbPipeLine.Name = "cmbPipeLine";
			this.cmbPipeLine.Size = new Size(121, 20);
			this.cmbPipeLine.TabIndex = 1;
			this.cmbPipeLine.SelectedIndexChanged += new EventHandler(this.cmbPipeLine_SelectedIndexChanged);
			this.cmbPipePoint.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipePoint.FormattingEnabled = true;
			this.cmbPipePoint.Location = new System.Drawing.Point(20, 27);
			this.cmbPipePoint.Name = "cmbPipePoint";
			this.cmbPipePoint.Size = new Size(121, 20);
			this.cmbPipePoint.TabIndex = 3;
			this.cmbPipePoint.SelectedIndexChanged += new EventHandler(this.cmbPipePoint_SelectedIndexChanged);
			this.groupBox1.Controls.Add(this.lstBoxPipeLineValues);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmbPipeLineFields);
			this.groupBox1.Controls.Add(this.cmbPipeLine);
			this.groupBox1.Location = new System.Drawing.Point(7, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(165, 227);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "线层";
			this.lstBoxPipeLineValues.FormattingEnabled = true;
			this.lstBoxPipeLineValues.ItemHeight = 12;
			this.lstBoxPipeLineValues.Location = new System.Drawing.Point(17, 118);
			this.lstBoxPipeLineValues.Name = "lstBoxPipeLineValues";
			this.lstBoxPipeLineValues.Size = new Size(121, 88);
			this.lstBoxPipeLineValues.TabIndex = 4;
			this.lstBoxPipeLineValues.SelectedIndexChanged += new EventHandler(this.lstBoxPipeLineValues_SelectedIndexChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 59);
			this.label3.Name = "label3";
			this.label3.Size = new Size(41, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "字段：";
			this.cmbPipeLineFields.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipeLineFields.FormattingEnabled = true;
			this.cmbPipeLineFields.Location = new System.Drawing.Point(17, 79);
			this.cmbPipeLineFields.Name = "cmbPipeLineFields";
			this.cmbPipeLineFields.Size = new Size(121, 20);
			this.cmbPipeLineFields.TabIndex = 1;
			this.cmbPipeLineFields.SelectedIndexChanged += new EventHandler(this.cmbPipeLineFields_SelectedIndexChanged);
			this.groupBox2.Controls.Add(this.lstBoxPipePointValues);
			this.groupBox2.Controls.Add(this.cmbPipePointFields);
			this.groupBox2.Controls.Add(this.cmbPipePoint);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(204, 10);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(167, 227);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "点层";
			this.lstBoxPipePointValues.FormattingEnabled = true;
			this.lstBoxPipePointValues.ItemHeight = 12;
			this.lstBoxPipePointValues.Location = new System.Drawing.Point(21, 118);
			this.lstBoxPipePointValues.Name = "lstBoxPipePointValues";
			this.lstBoxPipePointValues.Size = new Size(121, 88);
			this.lstBoxPipePointValues.TabIndex = 4;
			this.lstBoxPipePointValues.SelectedIndexChanged += new EventHandler(this.lstBoxPipePointValues_SelectedIndexChanged);
			this.cmbPipePointFields.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPipePointFields.FormattingEnabled = true;
			this.cmbPipePointFields.Location = new System.Drawing.Point(21, 79);
			this.cmbPipePointFields.Name = "cmbPipePointFields";
			this.cmbPipePointFields.Size = new Size(121, 20);
			this.cmbPipePointFields.TabIndex = 3;
			this.cmbPipePointFields.SelectedIndexChanged += new EventHandler(this.cmbPipePointFields_SelectedIndexChanged);
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 59);
			this.label4.Name = "label4";
			this.label4.Size = new Size(41, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "字段：";
			this.btnPipeLineQuery.Location = new System.Drawing.Point(84, 256);
			this.btnPipeLineQuery.Name = "btnPipeLineQuery";
			this.btnPipeLineQuery.Size = new Size(93, 23);
			this.btnPipeLineQuery.TabIndex = 5;
			this.btnPipeLineQuery.Text = "查询管线(&L)";
			this.btnPipeLineQuery.UseVisualStyleBackColor = true;
			this.btnPipeLineQuery.Click += new EventHandler(this.btnPipeLineQuery_Click);
			this.btnPipePointQuery.Location = new System.Drawing.Point(181, 256);
			this.btnPipePointQuery.Name = "btnPipePointQuery";
			this.btnPipePointQuery.Size = new Size(93, 23);
			this.btnPipePointQuery.TabIndex = 6;
			this.btnPipePointQuery.Text = "查询管点(&P)";
			this.btnPipePointQuery.UseVisualStyleBackColor = true;
			this.btnPipePointQuery.Click += new EventHandler(this.btnPipePointQuery_Click);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.Location = new System.Drawing.Point(278, 256);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(93, 23);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.GeometrySet.AutoSize = true;
			this.GeometrySet.Location = new System.Drawing.Point(12, 260);
			this.GeometrySet.Name = "GeometrySet";
			this.GeometrySet.Size = new Size(72, 16);
			this.GeometrySet.TabIndex = 23;
			this.GeometrySet.Text = "空间范围";
			this.GeometrySet.UseVisualStyleBackColor = true;
			this.GeometrySet.Click += new EventHandler(this.GeometrySet_Click_1);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(383, 295);
			base.Controls.Add(this.GeometrySet);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnPipePointQuery);
			base.Controls.Add(this.btnPipeLineQuery);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "RelateQueryUI";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "关联查询";
			base.Load += new EventHandler(this.RelateQueryUI_Load);
			base.VisibleChanged += new EventHandler(this.RelateQueryUI_VisibleChanged);
			base.HelpRequested += new HelpEventHandler(this.RelateQueryUI_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
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
    }
}