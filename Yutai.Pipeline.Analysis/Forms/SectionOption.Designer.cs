using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class SectionOption
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
			this.txBoxTitle = new TextBox();
			this.label2 = new Label();
			this.txBoxRoad = new TextBox();
			this.label3 = new Label();
			this.txBoxNo = new TextBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.checkBox1 = new CheckBox();
			this.FalseBox = new CheckBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "剖面图标题(&T):";
			this.txBoxTitle.Location = new System.Drawing.Point(104, 14);
			this.txBoxTitle.MaxLength = 20;
			this.txBoxTitle.Name = "txBoxTitle";
			this.txBoxTitle.Size = new System.Drawing.Size(144, 21);
			this.txBoxTitle.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "所在道路(&R):";
			this.txBoxRoad.Location = new System.Drawing.Point(104, 56);
			this.txBoxRoad.MaxLength = 20;
			this.txBoxRoad.Name = "txBoxRoad";
			this.txBoxRoad.Size = new System.Drawing.Size(144, 21);
			this.txBoxRoad.TabIndex = 2;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "断面号(&S):";
			this.txBoxNo.Location = new System.Drawing.Point(104, 99);
			this.txBoxNo.MaxLength = 16;
			this.txBoxNo.Name = "txBoxNo";
			this.txBoxNo.Size = new System.Drawing.Size(144, 21);
			this.txBoxNo.TabIndex = 3;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(33, 163);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(150, 163);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(15, 136);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "信息竖排";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.FalseBox.AutoSize = true;
			this.FalseBox.Checked = true;
			this.FalseBox.CheckState = CheckState.Checked;
			this.FalseBox.Location = new System.Drawing.Point(104, 136);
			this.FalseBox.Name = "FalseBox";
			this.FalseBox.Size = new System.Drawing.Size(120, 16);
			this.FalseBox.TabIndex = 7;
			this.FalseBox.Text = "管线间距错位排列";
			this.FalseBox.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(274, 198);
			base.Controls.Add(this.FalseBox);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.txBoxTitle);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txBoxRoad);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txBoxNo);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.Name = "SectionOption";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "断面设置";
			base.Load += new EventHandler(this.SectionOption_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private Label label1;
		private TextBox txBoxTitle;
		private Label label2;
		private TextBox txBoxRoad;
		private Label label3;
		private TextBox txBoxNo;
		private Button btnOK;
		private Button btnCancel;
		private CheckBox checkBox1;
		private CheckBox FalseBox;
		private string string_0;
		private string string_1;
		private string string_2;
    }
}