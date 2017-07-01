using System;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Printing.Forms
{
	    partial class frmInputText
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
	private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new SimpleButton();
			this.btnCancel = new SimpleButton();
			this.txtText = new TextEdit();
			this.txtText.Properties.BeginInit();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "输入文字:";
			this.btnOK.DialogResult=(System.Windows.Forms.DialogResult.OK);
			this.btnOK.Location = new System.Drawing.Point(136, 40);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 24);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.DialogResult=(System.Windows.Forms.DialogResult.Cancel);
			this.btnCancel.Location = new System.Drawing.Point(216, 40);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "取消";
			this.txtText.EditValue=("");
			this.txtText.Location = new System.Drawing.Point(72, 8);
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(208, 21);
			this.txtText.TabIndex = 4;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(292, 79);
			base.Controls.Add(this.txtText);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmInputText";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "输入文字";
			this.txtText.Properties.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private System.Windows.Forms.Label label1;
		private SimpleButton btnOK;
		private SimpleButton btnCancel;
    }
}