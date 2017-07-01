using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Printing.Forms
{
	    partial class frmFractionTextElement
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
	private void InitializeComponent()
		{
			this.txtDenominatorText = new System.Windows.Forms.TextBox();
			this.txtNumeratorText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCancel = new SimpleButton();
			this.btnOK = new SimpleButton();
			base.SuspendLayout();
			this.txtDenominatorText.Location = new System.Drawing.Point(95, 45);
			this.txtDenominatorText.Name = "txtDenominatorText";
			this.txtDenominatorText.Size = new System.Drawing.Size(215, 21);
			this.txtDenominatorText.TabIndex = 13;
			this.txtNumeratorText.Location = new System.Drawing.Point(95, 14);
			this.txtNumeratorText.Name = "txtNumeratorText";
			this.txtNumeratorText.Size = new System.Drawing.Size(215, 21);
			this.txtNumeratorText.TabIndex = 12;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "分母文本";
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 10;
			this.label4.Text = "分子文本";
			this.btnCancel.DialogResult=(System.Windows.Forms.DialogResult.Cancel);
			this.btnCancel.Location = new System.Drawing.Point(244, 72);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 15;
			this.btnCancel.Text = "取消";
			this.btnOK.DialogResult=(System.Windows.Forms.DialogResult.OK);
			this.btnOK.Location = new System.Drawing.Point(164, 72);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 24);
			this.btnOK.TabIndex = 14;
			this.btnOK.Text = "确定";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(332, 105);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtDenominatorText);
			base.Controls.Add(this.txtNumeratorText);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmFractionTextElement";
			this.Text = "输入文本";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
        private IContainer icontainer_0 = null;
        public System.Windows.Forms.TextBox txtDenominatorText;
        public System.Windows.Forms.TextBox txtNumeratorText;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private SimpleButton btnCancel;
		private SimpleButton btnOK;
    }
}