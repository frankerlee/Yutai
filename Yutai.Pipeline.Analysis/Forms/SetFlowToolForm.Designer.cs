using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class SetFlowToolForm
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
			this.label1 = new Label();
			this.WayCombo = new ComboBox();
			this.SetBut = new Button();
			this.label2 = new Label();
			this.NetWorkCombo = new ComboBox();
			this.CloseBut = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
			this.label1.Name = "label1";
			this.label1.Size = new Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "流向方式";
			this.WayCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.WayCombo.FormattingEnabled = true;
			this.WayCombo.Items.AddRange(new object[]
			{
				"按几何属性",
				"按Z值"
			});
			this.WayCombo.Location = new System.Drawing.Point(79, 38);
			this.WayCombo.Name = "WayCombo";
			this.WayCombo.Size = new Size(114, 20);
			this.WayCombo.TabIndex = 1;
			this.SetBut.Location = new System.Drawing.Point(219, 7);
			this.SetBut.Name = "SetBut";
			this.SetBut.Size = new Size(75, 23);
			this.SetBut.TabIndex = 2;
			this.SetBut.Text = "设置(&P)";
			this.SetBut.UseVisualStyleBackColor = true;
			this.SetBut.Click += new EventHandler(this.SetBut_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 9);
			this.label2.Name = "label2";
			this.label2.Size = new Size(65, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "几何网络层";
			this.NetWorkCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.NetWorkCombo.FormattingEnabled = true;
			this.NetWorkCombo.Location = new System.Drawing.Point(79, 9);
			this.NetWorkCombo.Name = "NetWorkCombo";
			this.NetWorkCombo.Size = new Size(116, 20);
			this.NetWorkCombo.TabIndex = 4;
			this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(219, 36);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(75, 23);
			this.CloseBut.TabIndex = 5;
			this.CloseBut.Text = "关闭(&Q)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(297, 68);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.NetWorkCombo);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.SetBut);
			base.Controls.Add(this.WayCombo);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetFlowToolForm";
			base.ShowInTaskbar = false;
			this.Text = "流向设置";
			base.Load += new EventHandler(this.SetFlowToolForm_Load);
			base.HelpRequested += new HelpEventHandler(this.SetFlowToolForm_HelpRequested);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private Label label1;
		private ComboBox WayCombo;
		private Button SetBut;
		private Label label2;
		private ComboBox NetWorkCombo;
		private Button CloseBut;
    }
}