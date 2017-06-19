using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Editor.Forms
{
	internal class frmTrackSet : XtraForm
	{
		private Label label1;

		private TextEdit txtOffset;

		private SimpleButton btnOK;

		private SimpleButton simpleButton1;

		private GroupBox groupBox1;

		private RadioGroup radioGroup1;

		private CheckEdit checkEdit1;

		private System.ComponentModel.Container container_0 = null;

		public double offset = 0;

		public int ConstructOffset = 0;

		public frmTrackSet()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				this.offset = double.Parse(this.txtOffset.Text);
				if (!this.checkEdit1.Checked)
				{
					this.ConstructOffset = 0;
				}
				else
				{
					this.ConstructOffset = 1;
				}
				switch (this.radioGroup1.SelectedIndex)
				{
					case 0:
					{
						frmTrackSet constructOffset = this;
						constructOffset.ConstructOffset = constructOffset.ConstructOffset | 4;
						break;
					}
					case 1:
					{
						frmTrackSet _frmTrackSet = this;
						_frmTrackSet.ConstructOffset = _frmTrackSet.ConstructOffset | 2;
						break;
					}
					case 2:
					{
						frmTrackSet constructOffset1 = this;
						constructOffset1.ConstructOffset = constructOffset1.ConstructOffset | 8;
						break;
					}
				}
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
			catch
			{
				MessageBox.Show("请输入数字!");
			}
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void frmTrackSet_Load(object sender, EventArgs e)
		{
			this.txtOffset.Text = this.offset.ToString();
			if ((this.ConstructOffset & 1) == 0)
			{
				this.checkEdit1.Checked = false;
			}
			else
			{
				this.checkEdit1.Checked = true;
			}
			if ((this.ConstructOffset & 4) != 0)
			{
				this.radioGroup1.SelectedIndex = 0;
			}
			else if ((this.ConstructOffset & 2) != 0)
			{
				this.radioGroup1.SelectedIndex = 1;
			}
			else if ((this.ConstructOffset & 8) != 0)
			{
				this.radioGroup1.SelectedIndex = 2;
			}
		}

		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.txtOffset = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffset.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "偏移:";
            // 
            // txtOffset
            // 
            this.txtOffset.EditValue = "";
            this.txtOffset.Location = new System.Drawing.Point(56, 9);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(128, 20);
            this.txtOffset.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(48, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(120, 200);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(64, 25);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "取消";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new System.Drawing.Point(8, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "拐角设置";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(8, 17);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Menu;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "斜接的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "平切的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "圆的")});
            this.radioGroup1.Size = new System.Drawing.Size(152, 77);
            this.radioGroup1.TabIndex = 5;
            // 
            // checkEdit1
            // 
            this.checkEdit1.EditValue = true;
            this.checkEdit1.Location = new System.Drawing.Point(16, 163);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "删除自相交的环";
            this.checkEdit1.Size = new System.Drawing.Size(112, 19);
            this.checkEdit1.TabIndex = 6;
            // 
            // frmTrackSet
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(193, 237);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrackSet";
            this.Text = "跟踪设置";
            this.Load += new System.EventHandler(this.frmTrackSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtOffset.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}