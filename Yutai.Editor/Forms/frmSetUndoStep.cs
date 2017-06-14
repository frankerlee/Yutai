using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Plugins.Editor.Forms
{
	internal class frmSetUndoStep : XtraForm
	{
		private IContainer icontainer_0 = null;

		private Button button1;

		private Label label1;

		private TextBox textBox1;

		private Button button2;

		private int int_0 = 1;

		public int Step
		{
			get
			{
				return this.int_0;
			}
		}

		public frmSetUndoStep()
		{
			this.InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				this.int_0 = int.Parse(this.textBox1.Text);
				if (this.int_0 >= 1)
				{
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
				}
				else
				{
					MessageBox.Show("请输入大于或等于1的整数!");
				}
			}
			catch
			{
				MessageBox.Show("请输入整数!");
			}
		}

		protected override void Dispose(bool bool_0)
		{
			if ((!bool_0 ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(frmSetUndoStep));
			this.button1 = new Button();
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.button2 = new Button();
			base.SuspendLayout();
			this.button1.Location = new Point(17, 29);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(67, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(7, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "撤销步数";
			this.textBox1.Location = new Point(66, 2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(105, 21);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "2";
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new Point(104, 29);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(67, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(183, 55);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSetUndoStep";
			this.Text = "输入";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}