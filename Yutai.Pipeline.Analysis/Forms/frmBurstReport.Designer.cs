using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class frmBurstReport
    {
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.gIlaRpybmq = new System.Windows.Forms.GroupBox();
            this.lblPickPipeInfo = new System.Windows.Forms.Label();
            this.listOutBarriers = new System.Windows.Forms.ListBox();
            this.btnDelThroughBarrier = new System.Windows.Forms.Button();
            this.btnStartAnalysis = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioUpAndDown = new System.Windows.Forms.RadioButton();
            this.radioDown = new System.Windows.Forms.RadioButton();
            this.radioUp = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDomainValues = new System.Windows.Forms.ComboBox();
            this.listFieldValues = new System.Windows.Forms.ListBox();
            this.btnDelFieldValue = new System.Windows.Forms.Button();
            this.btnAddFieldValue = new System.Windows.Forms.Button();
            this.btnAddThroughValve = new System.Windows.Forms.RadioButton();
            this.btnPickBrokePipe = new System.Windows.Forms.RadioButton();
            this.timer_0 = new System.Windows.Forms.Timer(this.components);
            this.checkReflushView = new System.Windows.Forms.CheckBox();
            this.ClearGra = new System.Windows.Forms.Button();
            this.gIlaRpybmq.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(326, 254);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退出(&X)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(176, 20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(203, 192);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // treeView1
            // 
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point(7, 20);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(168, 192);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            // 
            // gIlaRpybmq
            // 
            this.gIlaRpybmq.Controls.Add(this.treeView1);
            this.gIlaRpybmq.Controls.Add(this.listView1);
            this.gIlaRpybmq.Location = new System.Drawing.Point(12, 283);
            this.gIlaRpybmq.Name = "gIlaRpybmq";
            this.gIlaRpybmq.Size = new System.Drawing.Size(389, 219);
            this.gIlaRpybmq.TabIndex = 0;
            this.gIlaRpybmq.TabStop = false;
            this.gIlaRpybmq.Text = "分析结果";
            // 
            // lblPickPipeInfo
            // 
            this.lblPickPipeInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPickPipeInfo.Location = new System.Drawing.Point(82, 9);
            this.lblPickPipeInfo.Name = "lblPickPipeInfo";
            this.lblPickPipeInfo.Size = new System.Drawing.Size(319, 26);
            this.lblPickPipeInfo.TabIndex = 6;
            // 
            // listOutBarriers
            // 
            this.listOutBarriers.FormattingEnabled = true;
            this.listOutBarriers.ItemHeight = 12;
            this.listOutBarriers.Location = new System.Drawing.Point(235, 67);
            this.listOutBarriers.Name = "listOutBarriers";
            this.listOutBarriers.Size = new System.Drawing.Size(166, 64);
            this.listOutBarriers.TabIndex = 8;
            // 
            // btnDelThroughBarrier
            // 
            this.btnDelThroughBarrier.Location = new System.Drawing.Point(377, 38);
            this.btnDelThroughBarrier.Name = "btnDelThroughBarrier";
            this.btnDelThroughBarrier.Size = new System.Drawing.Size(24, 23);
            this.btnDelThroughBarrier.TabIndex = 9;
            this.btnDelThroughBarrier.Text = "×";
            this.btnDelThroughBarrier.UseVisualStyleBackColor = true;
            this.btnDelThroughBarrier.Click += new System.EventHandler(this.btnDelThroughBarrier_Click);
            // 
            // btnStartAnalysis
            // 
            this.btnStartAnalysis.Location = new System.Drawing.Point(12, 254);
            this.btnStartAnalysis.Name = "btnStartAnalysis";
            this.btnStartAnalysis.Size = new System.Drawing.Size(75, 23);
            this.btnStartAnalysis.TabIndex = 10;
            this.btnStartAnalysis.Text = "分析";
            this.btnStartAnalysis.UseVisualStyleBackColor = true;
            this.btnStartAnalysis.Click += new System.EventHandler(this.btnStartAnalysis_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioUpAndDown);
            this.groupBox2.Controls.Add(this.radioDown);
            this.groupBox2.Controls.Add(this.radioUp);
            this.groupBox2.Location = new System.Drawing.Point(228, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 97);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查找方向设置";
            // 
            // radioUpAndDown
            // 
            this.radioUpAndDown.AutoSize = true;
            this.radioUpAndDown.Location = new System.Drawing.Point(7, 67);
            this.radioUpAndDown.Name = "radioUpAndDown";
            this.radioUpAndDown.Size = new System.Drawing.Size(71, 16);
            this.radioUpAndDown.TabIndex = 2;
            this.radioUpAndDown.TabStop = true;
            this.radioUpAndDown.Text = "查找联通";
            this.radioUpAndDown.UseVisualStyleBackColor = true;
            // 
            // radioDown
            // 
            this.radioDown.AutoSize = true;
            this.radioDown.Location = new System.Drawing.Point(7, 44);
            this.radioDown.Name = "radioDown";
            this.radioDown.Size = new System.Drawing.Size(71, 16);
            this.radioDown.TabIndex = 1;
            this.radioDown.TabStop = true;
            this.radioDown.Text = "查找下游";
            this.radioDown.UseVisualStyleBackColor = true;
            // 
            // radioUp
            // 
            this.radioUp.AutoSize = true;
            this.radioUp.Location = new System.Drawing.Point(7, 21);
            this.radioUp.Name = "radioUp";
            this.radioUp.Size = new System.Drawing.Size(71, 16);
            this.radioUp.TabIndex = 0;
            this.radioUp.TabStop = true;
            this.radioUp.Text = "查找上游";
            this.radioUp.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbDomainValues);
            this.groupBox3.Controls.Add(this.listFieldValues);
            this.groupBox3.Controls.Add(this.btnDelFieldValue);
            this.groupBox3.Controls.Add(this.btnAddFieldValue);
            this.groupBox3.Location = new System.Drawing.Point(13, 47);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 197);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查找内容设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // cmbDomainValues
            // 
            this.cmbDomainValues.FormattingEnabled = true;
            this.cmbDomainValues.Location = new System.Drawing.Point(10, 162);
            this.cmbDomainValues.Name = "cmbDomainValues";
            this.cmbDomainValues.Size = new System.Drawing.Size(163, 20);
            this.cmbDomainValues.TabIndex = 10;
            // 
            // listFieldValues
            // 
            this.listFieldValues.FormattingEnabled = true;
            this.listFieldValues.ItemHeight = 12;
            this.listFieldValues.Location = new System.Drawing.Point(8, 39);
            this.listFieldValues.Name = "listFieldValues";
            this.listFieldValues.Size = new System.Drawing.Size(167, 112);
            this.listFieldValues.TabIndex = 2;
            // 
            // btnDelFieldValue
            // 
            this.btnDelFieldValue.Location = new System.Drawing.Point(178, 128);
            this.btnDelFieldValue.Name = "btnDelFieldValue";
            this.btnDelFieldValue.Size = new System.Drawing.Size(24, 23);
            this.btnDelFieldValue.TabIndex = 9;
            this.btnDelFieldValue.Text = "×";
            this.btnDelFieldValue.UseVisualStyleBackColor = true;
            this.btnDelFieldValue.Click += new System.EventHandler(this.btnDelFieldValue_Click);
            // 
            // btnAddFieldValue
            // 
            this.btnAddFieldValue.Location = new System.Drawing.Point(178, 157);
            this.btnAddFieldValue.Name = "btnAddFieldValue";
            this.btnAddFieldValue.Size = new System.Drawing.Size(24, 23);
            this.btnAddFieldValue.TabIndex = 9;
            this.btnAddFieldValue.Text = "+";
            this.btnAddFieldValue.UseVisualStyleBackColor = true;
            this.btnAddFieldValue.Click += new System.EventHandler(this.btnAddFieldValue_Click);
            // 
            // btnAddThroughValve
            // 
            this.btnAddThroughValve.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnAddThroughValve.AutoSize = true;
            this.btnAddThroughValve.Location = new System.Drawing.Point(235, 38);
            this.btnAddThroughValve.Name = "btnAddThroughValve";
            this.btnAddThroughValve.Size = new System.Drawing.Size(87, 22);
            this.btnAddThroughValve.TabIndex = 10;
            this.btnAddThroughValve.Text = "拾取穿越阀门";
            this.btnAddThroughValve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddThroughValve.UseVisualStyleBackColor = true;
            this.btnAddThroughValve.CheckedChanged += new System.EventHandler(this.btnAddThroughValve_CheckedChanged);
            // 
            // btnPickBrokePipe
            // 
            this.btnPickBrokePipe.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPickBrokePipe.AutoSize = true;
            this.btnPickBrokePipe.Checked = true;
            this.btnPickBrokePipe.Location = new System.Drawing.Point(13, 13);
            this.btnPickBrokePipe.Name = "btnPickBrokePipe";
            this.btnPickBrokePipe.Size = new System.Drawing.Size(63, 22);
            this.btnPickBrokePipe.TabIndex = 16;
            this.btnPickBrokePipe.TabStop = true;
            this.btnPickBrokePipe.Text = "拾取爆管";
            this.btnPickBrokePipe.UseVisualStyleBackColor = true;
            this.btnPickBrokePipe.CheckedChanged += new System.EventHandler(this.btnPickBrokePipe_CheckedChanged);
            this.btnPickBrokePipe.Click += new System.EventHandler(this.btnPickBrokePipe_Click);
            // 
            // timer_0
            // 
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
            // 
            // checkReflushView
            // 
            this.checkReflushView.AutoSize = true;
            this.checkReflushView.Checked = true;
            this.checkReflushView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkReflushView.Location = new System.Drawing.Point(202, 258);
            this.checkReflushView.Name = "checkReflushView";
            this.checkReflushView.Size = new System.Drawing.Size(120, 16);
            this.checkReflushView.TabIndex = 18;
            this.checkReflushView.Text = "退出清除分析结果";
            this.checkReflushView.UseVisualStyleBackColor = true;
            // 
            // ClearGra
            // 
            this.ClearGra.Location = new System.Drawing.Point(93, 254);
            this.ClearGra.Name = "ClearGra";
            this.ClearGra.Size = new System.Drawing.Size(75, 23);
            this.ClearGra.TabIndex = 10;
            this.ClearGra.Text = "清除结果";
            this.ClearGra.UseVisualStyleBackColor = true;
            this.ClearGra.Click += new System.EventHandler(this.ClearGra_Click);
            // 
            // frmBurstReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 511);
            this.Controls.Add(this.checkReflushView);
            this.Controls.Add(this.btnDelThroughBarrier);
            this.Controls.Add(this.listOutBarriers);
            this.Controls.Add(this.btnAddThroughValve);
            this.Controls.Add(this.btnPickBrokePipe);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ClearGra);
            this.Controls.Add(this.btnStartAnalysis);
            this.Controls.Add(this.lblPickPipeInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gIlaRpybmq);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBurstReport";
            this.ShowIcon = false;
            this.Text = "关阀分析";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBurstReport_FormClosed);
            this.Load += new System.EventHandler(this.frmBurstReport_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.frmBurstReport_HelpRequested);
            this.gIlaRpybmq.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private Button btnCancel;
		private TreeView treeView1;
		private GroupBox gIlaRpybmq;
		private Label lblPickPipeInfo;
		private ListBox listOutBarriers;
		private Button btnDelThroughBarrier;
		private Button btnStartAnalysis;
		private GroupBox groupBox2;
		private RadioButton radioUpAndDown;
		private RadioButton radioDown;
		private RadioButton radioUp;
		private GroupBox groupBox3;
		private ListBox listFieldValues;
		private Button btnDelFieldValue;
		private Button btnAddFieldValue;
		private RadioButton btnAddThroughValve;
		private RadioButton btnPickBrokePipe;
		private ComboBox cmbDomainValues;
		private Label label1;
		private CheckBox checkReflushView;
		private Button ClearGra;
		private ListView listView1;
		private Timer timer_0;
		private IPoint ipoint_0;
		private IElement ielement_0;
		private IGeometry BuoawIbkuD;
        private IContainer components;
    }
}