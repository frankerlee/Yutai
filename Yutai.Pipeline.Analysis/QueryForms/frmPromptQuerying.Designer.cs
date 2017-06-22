using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class frmPromptQuerying
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromptQuerying));
			this.pictureBox = new PictureBox();
			this.label = new Label();
			((ISupportInitialize)this.pictureBox).BeginInit();
			base.SuspendLayout();
			this.pictureBox.Image = (Image)resources.GetObject("pictureBox.Image");
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new Size(266, 10);
			this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(82, 31);
			this.label.Name = "label";
			this.label.Size = new Size(143, 12);
			this.label.TabIndex = 1;
			this.label.Text = "正在进行查询，请稍候...";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(290, 52);
			base.ControlBox = false;
			base.Controls.Add(this.label);
			base.Controls.Add(this.pictureBox);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmPromptQuerying";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			((ISupportInitialize)this.pictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private IContainer components = null;
		private Label label;
    }
}