using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleQueryByItemUI
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
			this.radioButton1 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.button1 = new Button();
			this.button2 = new Button();
			this.comboBox1 = new ComboBox();
			this.checkBox1 = new CheckBox();
			base.SuspendLayout();
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(12, 12);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new Size(71, 16);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "项目名称";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(100, 12);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new Size(71, 16);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "项目单位";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.button1.Location = new System.Drawing.Point(51, 69);
			this.button1.Name = "button1";
			this.button1.Size = new Size(85, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "查询";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(174, 69);
			this.button2.Name = "button2";
			this.button2.Size = new Size(85, 28);
			this.button2.TabIndex = 4;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 34);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(279, 20);
			this.comboBox1.TabIndex = 6;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(246, 12);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(72, 16);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "模糊查询";
			this.checkBox1.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(320, 111);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByItemUI";
			base.ShowIcon = false;
			this.Text = "项目查询";
			base.Load += new EventHandler(this.SimpleQueryByItemUI_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer components = null;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private QueryResult resultDlg;
		private RadioButton radioButton1;
		private RadioButton radioButton2;
		private Button button1;
		private Button button2;
		private ComboBox comboBox1;
		private CheckBox checkBox1;
    }
}