using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Editor.Forms
{
	internal class frmMaxAllowableOffset : XtraForm
	{
		private double double_0 = 0;

		private Label label1;

		private TextEdit txtMaxAllowOffset;

		private SimpleButton btnOK;

		private SimpleButton btnCancel;

		private System.ComponentModel.Container container_0 = null;

		public double MaxAllowableOffset
		{
			get
			{
				return this.double_0;
			}
		}

		public frmMaxAllowableOffset()
		{
			this.InitializeComponent();
		}

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
            this.txtMaxAllowOffset = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxAllowOffset.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "最大允许偏移:";
            // 
            // txtMaxAllowOffset
            // 
            this.txtMaxAllowOffset.EditValue = "";
            this.txtMaxAllowOffset.Location = new System.Drawing.Point(112, 9);
            this.txtMaxAllowOffset.Name = "txtMaxAllowOffset";
            this.txtMaxAllowOffset.Size = new System.Drawing.Size(152, 20);
            this.txtMaxAllowOffset.TabIndex = 1;
            this.txtMaxAllowOffset.TextChanged += new System.EventHandler(this.txtMaxAllowOffset_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(112, 36);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 26);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(200, 36);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            // 
            // frmMaxAllowableOffset
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(274, 74);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtMaxAllowOffset);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMaxAllowableOffset";
            this.Text = "最大允许偏移";
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxAllowOffset.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void txtMaxAllowOffset_TextChanged(object sender, EventArgs e)
		{
			string text = this.txtMaxAllowOffset.Text;
			text.Trim();
			try
			{
				this.double_0 = Convert.ToDouble(text);
				if (this.double_0 <= 0)
				{
					this.btnOK.Enabled = false;
				}
				else
				{
					this.btnOK.Enabled = true;
				}
			}
			catch
			{
				this.btnOK.Enabled = false;
			}
		}
	}
}