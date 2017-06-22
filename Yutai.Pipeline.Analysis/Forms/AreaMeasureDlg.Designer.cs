using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class AreaMeasureDlg
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
			this.txBoxCurArea = new TextBox();
			this.label2 = new Label();
			this.txBoxPerimeter = new TextBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前面积:";
			this.label1.Click += new EventHandler(this.label1_Click);
			this.txBoxCurArea.Location = new System.Drawing.Point(74, 13);
			this.txBoxCurArea.Name = "txBoxCurArea";
			this.txBoxCurArea.Size = new System.Drawing.Size(119, 21);
			this.txBoxCurArea.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "当前周长:";
			this.txBoxPerimeter.Location = new System.Drawing.Point(74, 50);
			this.txBoxPerimeter.Name = "txBoxPerimeter";
			this.txBoxPerimeter.Size = new System.Drawing.Size(119, 21);
			this.txBoxPerimeter.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(216, 84);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txBoxPerimeter);
			base.Controls.Add(this.txBoxCurArea);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AreaMeasureDlg";
			base.ShowInTaskbar = false;
			this.Text = "面积量算";
			base.TopMost = true;
			base.FormClosing += new FormClosingEventHandler(this.AreaMeasureDlg_FormClosing);
			base.Load += new EventHandler(this.AreaMeasureDlg_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private Label label1;
		private TextBox txBoxCurArea;
		private Label label2;
		private TextBox txBoxPerimeter;
    }
}