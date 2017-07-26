using System;
using System.ComponentModel;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmDGXFeatureClassToTin
    {
		protected override void Dispose(bool bool_1)
		{
			if (bool_1 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_1);
		}

	
	private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDGXFeatureClassToTin));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cboTagValueField = new System.Windows.Forms.ComboBox();
			this.cboTinSurfaceType = new System.Windows.Forms.ComboBox();
			this.cboHeightField = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdCancle = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.cboTagValueField);
			this.groupBox2.Controls.Add(this.cboTinSurfaceType);
			this.groupBox2.Controls.Add(this.cboHeightField);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(3, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 78);
			this.groupBox2.TabIndex = 26;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "图层设置";
			this.cboTagValueField.Location = new System.Drawing.Point(73, 78);
			this.cboTagValueField.Name = "cboTagValueField";
			this.cboTagValueField.Size = new System.Drawing.Size(88, 20);
			this.cboTagValueField.TabIndex = 7;
			this.cboTinSurfaceType.Location = new System.Drawing.Point(73, 50);
			this.cboTinSurfaceType.Name = "cboTinSurfaceType";
			this.cboTinSurfaceType.Size = new System.Drawing.Size(88, 20);
			this.cboTinSurfaceType.TabIndex = 6;
			this.cboHeightField.Location = new System.Drawing.Point(73, 22);
			this.cboHeightField.Name = "cboHeightField";
			this.cboHeightField.Size = new System.Drawing.Size(88, 20);
			this.cboHeightField.TabIndex = 5;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "标识字段";
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "三角网作为";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "高度源";
			this.cmdCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancle.Location = new System.Drawing.Point(128, 107);
			this.cmdCancle.Name = "cmdCancle";
			this.cmdCancle.Size = new System.Drawing.Size(48, 27);
			this.cmdCancle.TabIndex = 28;
			this.cmdCancle.Text = "取消";
			this.cmdOK.Location = new System.Drawing.Point(64, 107);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(48, 27);
			this.cmdOK.TabIndex = 27;
			this.cmdOK.Text = "确定";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(200, 147);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.cmdCancle);
			base.Controls.Add(this.cmdOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;			
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmDGXFeatureClassToTin";
			this.Text = "生成TIN设置";
			base.Load += new EventHandler(this.frmDGXFeatureClassToTin_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
		}

	
		
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cboHeightField;
		private System.Windows.Forms.ComboBox cboTinSurfaceType;
		private System.Windows.Forms.ComboBox cboTagValueField;
		private System.Windows.Forms.Button cmdCancle;
		private System.Windows.Forms.Button cmdOK;
    }
}