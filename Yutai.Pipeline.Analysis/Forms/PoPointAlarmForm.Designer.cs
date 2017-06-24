using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class PoPointAlarmForm
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
			this.btnClose = new Button();
			this.btnAnalyse = new Button();
			this.txBoxExpireTime = new TextBox();
			this.label1 = new Label();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			base.SuspendLayout();
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(138, 82);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(75, 23);
			this.btnClose.TabIndex = 29;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnAnalyse.Location = new System.Drawing.Point(27, 82);
			this.btnAnalyse.Name = "btnAnalyse";
			this.btnAnalyse.Size = new Size(75, 23);
			this.btnAnalyse.TabIndex = 28;
			this.btnAnalyse.Text = "分析(&A)";
			this.btnAnalyse.UseVisualStyleBackColor = true;
			this.btnAnalyse.Click += new EventHandler(this.btnAnalyse_Click);
			this.txBoxExpireTime.Location = new System.Drawing.Point(72, 46);
			this.txBoxExpireTime.Name = "txBoxExpireTime";
			this.txBoxExpireTime.Size = new Size(148, 21);
			this.txBoxExpireTime.TabIndex = 27;
			this.txBoxExpireTime.Text = "5";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 49);
			this.label1.Name = "label1";
			this.label1.Size = new Size(53, 12);
			this.label1.TabIndex = 26;
			this.label1.Text = "爆管次数";
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(73, 15);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 25;
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(12, 19);
			this.lable.Name = "lable";
			this.lable.Size = new Size(53, 12);
			this.lable.TabIndex = 24;
			this.lable.Text = "选择层：";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(234, 120);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnAnalyse);
			base.Controls.Add(this.txBoxExpireTime);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PoPointAlarmForm";
			base.ShowIcon = false;
			this.Text = "爆点预警";
			base.Load += new EventHandler(this.PoPointAlarmForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private Button btnClose;
		private Button btnAnalyse;
		private TextBox txBoxExpireTime;
		private Label label1;
		private ComboBox LayerBox;
		private Label lable;
		private PPAlarmResult ppalarmResult_0;
    }
}