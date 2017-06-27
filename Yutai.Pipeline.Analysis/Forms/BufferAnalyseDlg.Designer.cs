using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;


namespace Yutai.Pipeline.Analysis.Forms
{

	    partial class BufferAnalyseDlg
    {
		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.txBoxRadius = new System.Windows.Forms.TextBox();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.分析对象选择 = new System.Windows.Forms.GroupBox();
            this.radBtnOther = new System.Windows.Forms.RadioButton();
            this.radBtnLn = new System.Windows.Forms.RadioButton();
            this.radBtnPt = new System.Windows.Forms.RadioButton();
            this.chkLstLayers = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnConvertSelect = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.bGeo = new System.Windows.Forms.CheckBox();
            this.分析对象选择.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "缓冲区半径：";
            // 
            // txBoxRadius
            // 
            this.txBoxRadius.Location = new System.Drawing.Point(382, 240);
            this.txBoxRadius.Name = "txBoxRadius";
            this.txBoxRadius.Size = new System.Drawing.Size(77, 22);
            this.txBoxRadius.TabIndex = 1;
            this.txBoxRadius.Text = "10";
            this.txBoxRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txBoxRadius_KeyPress);
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAnalyse.Location = new System.Drawing.Point(126, 352);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(117, 27);
            this.btnAnalyse.TabIndex = 2;
            this.btnAnalyse.Text = "查看缓冲区(&O)";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(384, 352);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // 分析对象选择
            // 
            this.分析对象选择.Controls.Add(this.radBtnOther);
            this.分析对象选择.Controls.Add(this.radBtnLn);
            this.分析对象选择.Controls.Add(this.radBtnPt);
            this.分析对象选择.Location = new System.Drawing.Point(291, 10);
            this.分析对象选择.Name = "分析对象选择";
            this.分析对象选择.Size = new System.Drawing.Size(181, 133);
            this.分析对象选择.TabIndex = 21;
            this.分析对象选择.TabStop = false;
            this.分析对象选择.Text = "分析对象 方式";
            // 
            // radBtnOther
            // 
            this.radBtnOther.AutoSize = true;
            this.radBtnOther.Location = new System.Drawing.Point(29, 91);
            this.radBtnOther.Name = "radBtnOther";
            this.radBtnOther.Size = new System.Drawing.Size(91, 18);
            this.radBtnOther.TabIndex = 21;
            this.radBtnOther.Text = "其它数据(&D)";
            this.radBtnOther.UseVisualStyleBackColor = true;
            this.radBtnOther.CheckedChanged += new System.EventHandler(this.radBtnOther_CheckedChanged);
            // 
            // radBtnLn
            // 
            this.radBtnLn.AutoSize = true;
            this.radBtnLn.Location = new System.Drawing.Point(29, 56);
            this.radBtnLn.Name = "radBtnLn";
            this.radBtnLn.Size = new System.Drawing.Size(89, 18);
            this.radBtnLn.TabIndex = 20;
            this.radBtnLn.Text = "管线数据(&L)";
            this.radBtnLn.UseVisualStyleBackColor = true;
            this.radBtnLn.CheckedChanged += new System.EventHandler(this.radBtnLn_CheckedChanged);
            // 
            // radBtnPt
            // 
            this.radBtnPt.AutoSize = true;
            this.radBtnPt.Checked = true;
            this.radBtnPt.Location = new System.Drawing.Point(29, 23);
            this.radBtnPt.Name = "radBtnPt";
            this.radBtnPt.Size = new System.Drawing.Size(102, 18);
            this.radBtnPt.TabIndex = 19;
            this.radBtnPt.TabStop = true;
            this.radBtnPt.Text = "管线点数据(&P)";
            this.radBtnPt.UseVisualStyleBackColor = true;
            this.radBtnPt.CheckedChanged += new System.EventHandler(this.radBtnPt_CheckedChanged);
            // 
            // chkLstLayers
            // 
            this.chkLstLayers.CheckOnClick = true;
            this.chkLstLayers.FormattingEnabled = true;
            this.chkLstLayers.Location = new System.Drawing.Point(14, 10);
            this.chkLstLayers.Name = "chkLstLayers";
            this.chkLstLayers.Size = new System.Drawing.Size(271, 293);
            this.chkLstLayers.TabIndex = 20;
            this.chkLstLayers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLayers_ItemCheck);
            this.chkLstLayers.Click += new System.EventHandler(this.chkLstLayers_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(254, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 27);
            this.button1.TabIndex = 22;
            this.button1.Text = "查看明细(&V)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(291, 188);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(90, 33);
            this.btnSelectNone.TabIndex = 25;
            this.btnSelectNone.Text = "全不选(&N)";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnConvertSelect
            // 
            this.btnConvertSelect.Location = new System.Drawing.Point(382, 149);
            this.btnConvertSelect.Name = "btnConvertSelect";
            this.btnConvertSelect.Size = new System.Drawing.Size(90, 33);
            this.btnConvertSelect.TabIndex = 24;
            this.btnConvertSelect.Text = "反选(&I)";
            this.btnConvertSelect.UseVisualStyleBackColor = true;
            this.btnConvertSelect.Click += new System.EventHandler(this.btnConvertSelect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(291, 149);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(90, 33);
            this.btnSelectAll.TabIndex = 23;
            this.btnSelectAll.Text = "全选(&A)";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // bGeo
            // 
            this.bGeo.AutoSize = true;
            this.bGeo.Location = new System.Drawing.Point(294, 285);
            this.bGeo.Name = "bGeo";
            this.bGeo.Size = new System.Drawing.Size(98, 18);
            this.bGeo.TabIndex = 26;
            this.bGeo.Text = "绘制空间范围";
            this.bGeo.UseVisualStyleBackColor = true;
            this.bGeo.CheckedChanged += new System.EventHandler(this.bGeo_CheckedChanged);
            this.bGeo.Click += new System.EventHandler(this.bGeo_Click);
            // 
            // BufferAnalyseDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 391);
            this.Controls.Add(this.bGeo);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnConvertSelect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.分析对象选择);
            this.Controls.Add(this.chkLstLayers);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAnalyse);
            this.Controls.Add(this.txBoxRadius);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BufferAnalyseDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "缓冲区分析";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BufferAnalyseDlg_FormClosing);
            this.Load += new System.EventHandler(this.BufferAnalyseDlg_Load);
            this.VisibleChanged += new System.EventHandler(this.BufferAnalyseDlg_VisibleChanged);
            this.分析对象选择.ResumeLayout(false);
            this.分析对象选择.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private double double_0;
		private ResultDialog resultDialog_0;
		private Label label1;
		private TextBox txBoxRadius;
		private Button btnAnalyse;
		private Button btnClose;
		private GroupBox 分析对象选择;
		private CheckedListBox chkLstLayers;
		private RadioButton radBtnOther;
		private RadioButton radBtnLn;
		private RadioButton radBtnPt;
		private Button button1;
		private Button btnSelectNone;
		private Button btnConvertSelect;
		private Button btnSelectAll;
		private CheckBox bGeo;
    }
}