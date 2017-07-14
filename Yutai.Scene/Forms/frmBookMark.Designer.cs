using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmBookMark
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
            this.txtBookMarker = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookMarker.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "书签名称:";
            // 
            // txtBookMarker
            // 
            this.txtBookMarker.EditValue = "";
            this.txtBookMarker.Location = new System.Drawing.Point(104, 8);
            this.txtBookMarker.Name = "txtBookMarker";
            this.txtBookMarker.Size = new System.Drawing.Size(160, 20);
            this.txtBookMarker.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(136, 40);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(208, 40);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(64, 24);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            // 
            // frmBookMark
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 76);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBookMarker);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBookMark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建书签";
            this.Load += new System.EventHandler(this.frmBookMark_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBookMarker.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private System.Windows.Forms.Label label1;
		private TextEdit txtBookMarker;
		private SimpleButton btnOK;
		private SimpleButton simpleButton2;
    }
}