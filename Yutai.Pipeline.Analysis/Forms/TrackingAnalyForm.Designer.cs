
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class TrackingAnalyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.LayerCom = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.WayCom = new System.Windows.Forms.ComboBox();
            this.SetButton = new System.Windows.Forms.Button();
            this.ClearBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择追踪层";
            // 
            // LayerCom
            // 
            this.LayerCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerCom.FormattingEnabled = true;
            this.LayerCom.Location = new System.Drawing.Point(78, 11);
            this.LayerCom.Name = "LayerCom";
            this.LayerCom.Size = new System.Drawing.Size(113, 20);
            this.LayerCom.TabIndex = 1;
            this.LayerCom.SelectedIndexChanged += new System.EventHandler(this.LayerCom_SelectedIndexChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(203, 1);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "点标志";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(203, 23);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "线标志";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(268, 1);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 16);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "点阻断";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(268, 23);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(59, 16);
            this.radioButton4.TabIndex = 5;
            this.radioButton4.Text = "线阻断";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "追踪方式";
            // 
            // WayCom
            // 
            this.WayCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WayCom.FormattingEnabled = true;
            this.WayCom.Items.AddRange(new object[] {
            "追踪上游",
            "追踪下游"});
            this.WayCom.Location = new System.Drawing.Point(425, 11);
            this.WayCom.Name = "WayCom";
            this.WayCom.Size = new System.Drawing.Size(85, 20);
            this.WayCom.TabIndex = 7;
            // 
            // SetButton
            // 
            this.SetButton.Location = new System.Drawing.Point(516, 8);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(64, 23);
            this.SetButton.TabIndex = 8;
            this.SetButton.Text = "分析";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // ClearBut
            // 
            this.ClearBut.Location = new System.Drawing.Point(329, 9);
            this.ClearBut.Name = "ClearBut";
            this.ClearBut.Size = new System.Drawing.Size(39, 23);
            this.ClearBut.TabIndex = 9;
            this.ClearBut.Text = "清除";
            this.ClearBut.UseVisualStyleBackColor = true;
            this.ClearBut.Click += new System.EventHandler(this.ClearBut_Click);
            // 
            // TrackingAnalyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 49);
            this.Controls.Add(this.ClearBut);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.WayCom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.LayerCom);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrackingAnalyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "流向追踪";
            this.Load += new System.EventHandler(this.TrackingAnalyForm_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.TrackingAnalyForm_HelpRequested);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private Label label1;
		private ComboBox LayerCom;
		private RadioButton radioButton1;
		private RadioButton radioButton2;
		private RadioButton radioButton3;
		private RadioButton radioButton4;
		private Label label2;
		private ComboBox WayCom;
		private Button SetButton;
		private Button ClearBut;
    }
}