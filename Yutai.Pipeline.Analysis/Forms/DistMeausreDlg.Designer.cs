using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class DistMeausreDlg
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
			this.label2 = new Label();
			this.txBoxCurLen = new TextBox();
			this.txBoxTotalLen = new TextBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前距离:";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "总距离:";
			this.txBoxCurLen.Location = new System.Drawing.Point(78, 15);
			this.txBoxCurLen.Name = "txBoxCurLen";
			this.txBoxCurLen.Size = new System.Drawing.Size(100, 21);
			this.txBoxCurLen.TabIndex = 2;
			this.txBoxTotalLen.Location = new System.Drawing.Point(78, 46);
			this.txBoxTotalLen.Name = "txBoxTotalLen";
			this.txBoxTotalLen.Size = new System.Drawing.Size(100, 21);
			this.txBoxTotalLen.TabIndex = 3;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(200, 81);
			base.Controls.Add(this.txBoxTotalLen);
			base.Controls.Add(this.txBoxCurLen);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DistMeausreDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "量算距离";
			base.TopMost = true;
			base.FormClosed += new FormClosedEventHandler(this.DistMeausreDlg_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.DistMeausreDlg_FormClosing);
			base.Load += new EventHandler(this.DistMeausreDlg_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private Label label1;
		private Label label2;
		private TextBox txBoxCurLen;
		private TextBox txBoxTotalLen;
    }
}