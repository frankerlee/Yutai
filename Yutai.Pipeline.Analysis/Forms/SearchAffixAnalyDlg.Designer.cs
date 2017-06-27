using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class SearchAffixAnalyDlg
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
            this.RevBut = new System.Windows.Forms.Button();
            this.NoneBut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBoxSet = new System.Windows.Forms.CheckBox();
            this.txBoxY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txBoxX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LayerBox = new System.Windows.Forms.ComboBox();
            this.lable = new System.Windows.Forms.Label();
            this.AllBut = new System.Windows.Forms.Button();
            this.CloseBut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checAnaPipeline = new System.Windows.Forms.CheckBox();
            this.ValueBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txBoxRadius = new System.Windows.Forms.TextBox();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.checkReView = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RevBut
            // 
            this.RevBut.Location = new System.Drawing.Point(211, 163);
            this.RevBut.Name = "RevBut";
            this.RevBut.Size = new System.Drawing.Size(76, 23);
            this.RevBut.TabIndex = 3;
            this.RevBut.Text = "反选(&I)";
            this.RevBut.UseVisualStyleBackColor = true;
            this.RevBut.Click += new System.EventHandler(this.RevBut_Click);
            // 
            // NoneBut
            // 
            this.NoneBut.Location = new System.Drawing.Point(211, 118);
            this.NoneBut.Name = "NoneBut";
            this.NoneBut.Size = new System.Drawing.Size(76, 23);
            this.NoneBut.TabIndex = 2;
            this.NoneBut.Text = "全不选(&N)";
            this.NoneBut.UseVisualStyleBackColor = true;
            this.NoneBut.Click += new System.EventHandler(this.NoneBut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBoxSet);
            this.groupBox1.Controls.Add(this.txBoxY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txBoxX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 51);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找设置";
            // 
            // chkBoxSet
            // 
            this.chkBoxSet.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBoxSet.AutoSize = true;
            this.chkBoxSet.Location = new System.Drawing.Point(237, 16);
            this.chkBoxSet.Name = "chkBoxSet";
            this.chkBoxSet.Size = new System.Drawing.Size(39, 22);
            this.chkBoxSet.TabIndex = 4;
            this.chkBoxSet.Text = "设置";
            this.chkBoxSet.UseVisualStyleBackColor = true;
            // 
            // txBoxY
            // 
            this.txBoxY.Location = new System.Drawing.Point(129, 16);
            this.txBoxY.Name = "txBoxY";
            this.txBoxY.Size = new System.Drawing.Size(72, 21);
            this.txBoxY.TabIndex = 3;
            this.txBoxY.TextChanged += new System.EventHandler(this.txBoxY_TextChanged);
            this.txBoxY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txBoxY_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "米";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y:";
            // 
            // txBoxX
            // 
            this.txBoxX.Location = new System.Drawing.Point(32, 16);
            this.txBoxX.Name = "txBoxX";
            this.txBoxX.Size = new System.Drawing.Size(74, 21);
            this.txBoxX.TabIndex = 1;
            this.txBoxX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txBoxX_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // LayerBox
            // 
            this.LayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerBox.FormattingEnabled = true;
            this.LayerBox.Location = new System.Drawing.Point(56, 18);
            this.LayerBox.Name = "LayerBox";
            this.LayerBox.Size = new System.Drawing.Size(145, 20);
            this.LayerBox.TabIndex = 17;
            this.LayerBox.SelectedIndexChanged += new System.EventHandler(this.LayerBox_SelectedIndexChanged);
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Location = new System.Drawing.Point(6, 22);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(41, 12);
            this.lable.TabIndex = 16;
            this.lable.Text = "管点层";
            // 
            // AllBut
            // 
            this.AllBut.Location = new System.Drawing.Point(211, 73);
            this.AllBut.Name = "AllBut";
            this.AllBut.Size = new System.Drawing.Size(76, 23);
            this.AllBut.TabIndex = 1;
            this.AllBut.Text = "全选(&A)";
            this.AllBut.UseVisualStyleBackColor = true;
            this.AllBut.Click += new System.EventHandler(this.AllBut_Click);
            // 
            // CloseBut
            // 
            this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBut.Location = new System.Drawing.Point(224, 292);
            this.CloseBut.Name = "CloseBut";
            this.CloseBut.Size = new System.Drawing.Size(60, 23);
            this.CloseBut.TabIndex = 15;
            this.CloseBut.Text = "关闭(&C)";
            this.CloseBut.UseVisualStyleBackColor = true;
            this.CloseBut.Click += new System.EventHandler(this.CloseBut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checAnaPipeline);
            this.groupBox2.Controls.Add(this.RevBut);
            this.groupBox2.Controls.Add(this.NoneBut);
            this.groupBox2.Controls.Add(this.AllBut);
            this.groupBox2.Controls.Add(this.ValueBox);
            this.groupBox2.Controls.Add(this.LayerBox);
            this.groupBox2.Controls.Add(this.lable);
            this.groupBox2.Location = new System.Drawing.Point(8, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 226);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择查找对象";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // checAnaPipeline
            // 
            this.checAnaPipeline.AutoSize = true;
            this.checAnaPipeline.Checked = true;
            this.checAnaPipeline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checAnaPipeline.Location = new System.Drawing.Point(212, 21);
            this.checAnaPipeline.Name = "checAnaPipeline";
            this.checAnaPipeline.Size = new System.Drawing.Size(72, 16);
            this.checAnaPipeline.TabIndex = 18;
            this.checAnaPipeline.Text = "分析管线";
            this.checAnaPipeline.UseVisualStyleBackColor = true;
            // 
            // ValueBox
            // 
            this.ValueBox.CheckOnClick = true;
            this.ValueBox.FormattingEnabled = true;
            this.ValueBox.Items.AddRange(new object[] {
            "sdfsfsfs",
            "sdsdf",
            "sfdsdf"});
            this.ValueBox.Location = new System.Drawing.Point(13, 53);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(188, 164);
            this.ValueBox.Sorted = true;
            this.ValueBox.TabIndex = 0;
            this.ValueBox.SelectedIndexChanged += new System.EventHandler(this.ValueBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "查找半径：";
            // 
            // txBoxRadius
            // 
            this.txBoxRadius.Location = new System.Drawing.Point(86, 293);
            this.txBoxRadius.Name = "txBoxRadius";
            this.txBoxRadius.Size = new System.Drawing.Size(123, 21);
            this.txBoxRadius.TabIndex = 19;
            this.txBoxRadius.Text = "50";
            this.txBoxRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txBoxRadius_KeyPress);
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Location = new System.Drawing.Point(224, 320);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(60, 23);
            this.btnAnalyse.TabIndex = 22;
            this.btnAnalyse.Text = "分析(&A)";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // checkReView
            // 
            this.checkReView.AutoSize = true;
            this.checkReView.Checked = true;
            this.checkReView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkReView.Location = new System.Drawing.Point(21, 323);
            this.checkReView.Name = "checkReView";
            this.checkReView.Size = new System.Drawing.Size(120, 16);
            this.checkReView.TabIndex = 24;
            this.checkReView.Text = "关闭清除分析结果";
            this.checkReView.UseVisualStyleBackColor = true;
            // 
            // SearchAffixAnalyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 347);
            this.Controls.Add(this.checkReView);
            this.Controls.Add(this.btnAnalyse);
            this.Controls.Add(this.txBoxRadius);
            this.Controls.Add(this.CloseBut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchAffixAnalyDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "设施搜索";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchAffixAnalyDlg_FormClosing);
            this.Load += new System.EventHandler(this.SearchAffixAnalyDlg_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SearchAffixAnalyDlg_HelpRequested);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	
		private IFeatureLayer ifeatureLayer_0;
		private IFields ifields_0;
		private string string_0;
		private IField ifield_0;
		private Button RevBut;
		private Button NoneBut;
		private GroupBox groupBox1;
		private ComboBox LayerBox;
		private Label lable;
		private Button AllBut;
		private Button CloseBut;
		private GroupBox groupBox2;
		private CheckedListBox ValueBox;
		private TextBox txBoxY;
		private Label label2;
		private TextBox txBoxX;
		private Label label1;
		private Label label3;
		private TextBox txBoxRadius;
		private Label label4;
		private Button btnAnalyse;
		private CheckBox chkBoxSet;
		private CheckBox checkReView;
		private CheckBox checAnaPipeline;
    }
}