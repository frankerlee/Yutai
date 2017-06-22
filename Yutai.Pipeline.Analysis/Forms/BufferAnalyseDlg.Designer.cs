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
			this.label1 = new Label();
			this.txBoxRadius = new TextBox();
			this.btnAnalyse = new Button();
			this.btnClose = new Button();
			this.分析对象选择 = new GroupBox();
			this.radBtnOther = new RadioButton();
			this.radBtnLn = new RadioButton();
			this.radBtnPt = new RadioButton();
			this.chkLstLayers = new CheckedListBox();
			this.button1 = new Button();
			this.btnSelectNone = new Button();
			this.btnConvertSelect = new Button();
			this.btnSelectAll = new Button();
			this.bGeo = new CheckBox();
			this.分析对象选择.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(262, 257);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "缓冲区半径：";
			this.txBoxRadius.Location = new System.Drawing.Point(351, 254);
			this.txBoxRadius.Name = "txBoxRadius";
			this.txBoxRadius.Size = new System.Drawing.Size(67, 21);
			this.txBoxRadius.TabIndex = 1;
			this.txBoxRadius.Text = "10";
			this.txBoxRadius.KeyPress += new KeyPressEventHandler(this.txBoxRadius_KeyPress);
			this.btnAnalyse.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAnalyse.Location = new System.Drawing.Point(124, 291);
			this.btnAnalyse.Name = "btnAnalyse";
			this.btnAnalyse.Size = new System.Drawing.Size(100, 23);
			this.btnAnalyse.TabIndex = 2;
			this.btnAnalyse.Text = "查看缓冲区(&O)";
			this.btnAnalyse.UseVisualStyleBackColor = true;
			this.btnAnalyse.Click += new EventHandler(this.btnAnalyse_Click);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(365, 300);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.分析对象选择.Controls.Add(this.radBtnOther);
			this.分析对象选择.Controls.Add(this.radBtnLn);
			this.分析对象选择.Controls.Add(this.radBtnPt);
			this.分析对象选择.Location = new System.Drawing.Point(264, 18);
			this.分析对象选择.Name = "分析对象选择";
			this.分析对象选择.Size = new System.Drawing.Size(155, 114);
			this.分析对象选择.TabIndex = 21;
			this.分析对象选择.TabStop = false;
			this.分析对象选择.Text = "分析对象 方式";
			this.radBtnOther.AutoSize = true;
			this.radBtnOther.Location = new System.Drawing.Point(25, 78);
			this.radBtnOther.Name = "radBtnOther";
			this.radBtnOther.Size = new System.Drawing.Size(89, 16);
			this.radBtnOther.TabIndex = 21;
			this.radBtnOther.Text = "其它数据(&D)";
			this.radBtnOther.UseVisualStyleBackColor = true;
			this.radBtnOther.CheckedChanged += new EventHandler(this.radBtnOther_CheckedChanged);
			this.radBtnLn.AutoSize = true;
			this.radBtnLn.Location = new System.Drawing.Point(25, 48);
			this.radBtnLn.Name = "radBtnLn";
			this.radBtnLn.Size = new System.Drawing.Size(89, 16);
			this.radBtnLn.TabIndex = 20;
			this.radBtnLn.Text = "管线数据(&L)";
			this.radBtnLn.UseVisualStyleBackColor = true;
			this.radBtnLn.CheckedChanged += new EventHandler(this.radBtnLn_CheckedChanged);
			this.radBtnPt.AutoSize = true;
			this.radBtnPt.Checked = true;
			this.radBtnPt.Location = new System.Drawing.Point(25, 20);
			this.radBtnPt.Name = "radBtnPt";
			this.radBtnPt.Size = new System.Drawing.Size(101, 16);
			this.radBtnPt.TabIndex = 19;
			this.radBtnPt.TabStop = true;
			this.radBtnPt.Text = "管线点数据(&P)";
			this.radBtnPt.UseVisualStyleBackColor = true;
			this.radBtnPt.CheckedChanged += new EventHandler(this.radBtnPt_CheckedChanged);
			this.chkLstLayers.CheckOnClick = true;
			this.chkLstLayers.FormattingEnabled = true;
			this.chkLstLayers.Location = new System.Drawing.Point(12, 9);
			this.chkLstLayers.Name = "chkLstLayers";
			this.chkLstLayers.Size = new System.Drawing.Size(206, 260);
			this.chkLstLayers.TabIndex = 20;
			this.chkLstLayers.ItemCheck += new ItemCheckEventHandler(this.chkLstLayers_ItemCheck);
			this.chkLstLayers.Click += new EventHandler(this.chkLstLayers_Click);
			this.button1.Location = new System.Drawing.Point(229, 291);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(102, 23);
			this.button1.TabIndex = 22;
			this.button1.Text = "查看明细(&V)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.btnSelectNone.Location = new System.Drawing.Point(263, 195);
			this.btnSelectNone.Name = "btnSelectNone";
			this.btnSelectNone.Size = new System.Drawing.Size(77, 28);
			this.btnSelectNone.TabIndex = 25;
			this.btnSelectNone.Text = "全不选(&N)";
			this.btnSelectNone.UseVisualStyleBackColor = true;
			this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
			this.btnConvertSelect.Location = new System.Drawing.Point(351, 149);
			this.btnConvertSelect.Name = "btnConvertSelect";
			this.btnConvertSelect.Size = new System.Drawing.Size(77, 28);
			this.btnConvertSelect.TabIndex = 24;
			this.btnConvertSelect.Text = "反选(&I)";
			this.btnConvertSelect.UseVisualStyleBackColor = true;
			this.btnConvertSelect.Click += new EventHandler(this.btnConvertSelect_Click);
			this.btnSelectAll.Location = new System.Drawing.Point(263, 149);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(77, 28);
			this.btnSelectAll.TabIndex = 23;
			this.btnSelectAll.Text = "全选(&A)";
			this.btnSelectAll.UseVisualStyleBackColor = true;
			this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
			this.bGeo.AutoSize = true;
			this.bGeo.Location = new System.Drawing.Point(12, 295);
			this.bGeo.Name = "bGeo";
			this.bGeo.Size = new System.Drawing.Size(96, 16);
			this.bGeo.TabIndex = 26;
			this.bGeo.Text = "绘制空间范围";
			this.bGeo.UseVisualStyleBackColor = true;
			this.bGeo.Click += new EventHandler(this.bGeo_Click);
			this.bGeo.CheckedChanged += new EventHandler(this.bGeo_CheckedChanged);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(452, 335);
			base.Controls.Add(this.bGeo);
			base.Controls.Add(this.btnSelectNone);
			base.Controls.Add(this.btnConvertSelect);
			base.Controls.Add(this.btnSelectAll);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.分析对象选择);
			base.Controls.Add(this.chkLstLayers);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnAnalyse);
			base.Controls.Add(this.txBoxRadius);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "BufferAnalyseDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "缓冲区分析";
			base.TopMost = true;
			base.Load += new EventHandler(this.BufferAnalyseDlg_Load);
			base.VisibleChanged += new EventHandler(this.BufferAnalyseDlg_VisibleChanged);
			base.FormClosing += new FormClosingEventHandler(this.BufferAnalyseDlg_FormClosing);
			this.分析对象选择.ResumeLayout(false);
			this.分析对象选择.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
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