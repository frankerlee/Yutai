using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class frmPromptQuerying : Form
	{
		private IContainer components = null;

		private Label label;

		private string _StatusInfo = "";

		internal PictureBox pictureBox;

		public string StatusInfo
		{
			get
			{
				return this._StatusInfo;
			}
			set
			{
				this._StatusInfo = value;
				this.ChangeStatusText();
			}
		}

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
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(290, 52);
			base.ControlBox = false;
			base.Controls.Add(this.label);
			base.Controls.Add(this.pictureBox);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmPromptQuerying";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			((ISupportInitialize)this.pictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmPromptQuerying()
		{
			this.InitializeComponent();
		}

		public frmPromptQuerying(int i)
		{
			this.InitializeComponent();
			if (i == 1)
			{
				this.label.Text = "正在查询，请稍候...";
			}
			if (i == 2)
			{
				this.label.Text = "正在上传数据，请稍候...";
			}
			if (i == 3)
			{
				this.label.Text = "正在进行处理，请稍候...";
			}
			if (i == 4)
			{
				this.label.Text = "构造查询条件，请稍候...";
			}
			if (i == 5)
			{
				this.label.Text = "进行空间查询，请稍候...";
			}
		}

		public void SetMessage(string strMesage)
		{
			this.label.Text = strMesage;
		}

		public void ChangeStatusText()
		{
			try
			{
				if (base.InvokeRequired)
				{
					base.Invoke(new MethodInvoker(this.ChangeStatusText));
				}
				else
				{
					this.label.Text = this._StatusInfo;
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
