using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.PipeConfig;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class PreAlarmDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreAlarmDlg));
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.label1 = new Label();
			this.txBoxExpireTime = new TextBox();
			this.btnAnalyse = new Button();
			this.btnClose = new Button();
			base.SuspendLayout();
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(76, 10);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 19;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(15, 14);
			this.lable.Name = "lable";
			this.lable.Size = new Size(53, 12);
			this.lable.TabIndex = 18;
			this.lable.Text = "选择层：";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 44);
			this.label1.Name = "label1";
			this.label1.Size = new Size(65, 12);
			this.label1.TabIndex = 20;
			this.label1.Text = "报废年限：";
			this.txBoxExpireTime.Location = new System.Drawing.Point(76, 41);
			this.txBoxExpireTime.Name = "txBoxExpireTime";
			this.txBoxExpireTime.Size = new Size(148, 21);
			this.txBoxExpireTime.TabIndex = 21;
			this.txBoxExpireTime.Text = "10";
			this.txBoxExpireTime.KeyPress += new KeyPressEventHandler(this.txBoxExpireTime_KeyPress);
			this.btnAnalyse.Location = new System.Drawing.Point(30, 77);
			this.btnAnalyse.Name = "btnAnalyse";
			this.btnAnalyse.Size = new Size(75, 23);
			this.btnAnalyse.TabIndex = 22;
			this.btnAnalyse.Text = "分析(&A)";
			this.btnAnalyse.UseVisualStyleBackColor = true;
			this.btnAnalyse.Click += new EventHandler(this.btnAnalyse_Click);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(141, 77);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(75, 23);
			this.btnClose.TabIndex = 23;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(255, 118);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnAnalyse);
			base.Controls.Add(this.txBoxExpireTime);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.LayerBox);
			base.Controls.Add(this.lable);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (Icon)resources.GetObject("$Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PreAlarmDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "预警分析";
			base.TopMost = true;
			base.Load += new EventHandler(this.PreAlarmDlg_Load);
			base.FormClosing += new FormClosingEventHandler(this.PreAlarmDlg_FormClosing);
			base.HelpRequested += new HelpEventHandler(this.PreAlarmDlg_HelpRequested);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private ComboBox LayerBox;
		private Label lable;
		private Label label1;
		private TextBox txBoxExpireTime;
		private Button btnAnalyse;
		private Button btnClose;
		private PreAlarmResult preAlarmResult_0;
    }
}