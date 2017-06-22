
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class SimpleQueryByJMDUI
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
			this.checkBox1 = new CheckBox();
			this.comboBox1 = new ComboBox();
			this.button2 = new Button();
			this.button1 = new Button();
			this.label1 = new Label();
			base.SuspendLayout();
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(256, 49);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(72, 16);
			this.checkBox1.TabIndex = 11;
			this.checkBox1.Text = "模糊查询";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(82, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(246, 20);
			this.comboBox1.TabIndex = 10;
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(186, 72);
			this.button2.Name = "button2";
			this.button2.Size = new Size(85, 28);
			this.button2.TabIndex = 9;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.Location = new System.Drawing.Point(65, 72);
			this.button1.Name = "button1";
			this.button1.Size = new Size(85, 28);
			this.button1.TabIndex = 8;
			this.button1.Text = "查询";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(1, 15);
			this.label1.Name = "label1";
			this.label1.Size = new Size(65, 12);
			this.label1.TabIndex = 12;
			this.label1.Text = "建筑物名称";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(333, 104);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByJMDUI";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "按建筑物查询";
			base.Load += new EventHandler(this.SimpleQueryByJMDUI_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer components = null;
		private IFeatureLayer SelectLayer;
		private IFields myfields;
		private QueryResult resultDlg;
		private CheckBox checkBox1;
		private ComboBox comboBox1;
		private Button button2;
		private Button button1;
		private Label label1;
    }
}