using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class SetScaleDlg : XtraForm
	{
		private IContainer icontainer_0 = null;

		private Label label5;

		private Label label7;

		private Label label6;

		private Label label4;

		private Button btnCancel;

		private Button LtaOriuJT;

		private TextBox txBoxScaleY;

		private TextBox txBoxScaleX;

		private float float_0;

		private float float_1;

		public float ScaleX
		{
			get
			{
				return this.float_0;
			}
			set
			{
				this.float_0 = value;
			}
		}

		public float ScaleY
		{
			get
			{
				return this.float_1;
			}
			set
			{
				this.float_1 = value;
			}
		}

		public SetScaleDlg()
		{
			this.InitializeComponent();
		}

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
			this.label5 = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label4 = new Label();
			this.btnCancel = new Button();
			this.LtaOriuJT = new Button();
			this.txBoxScaleY = new TextBox();
			this.txBoxScaleX = new TextBox();
			base.SuspendLayout();
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 55);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "纵向比例尺：";
			this.label7.Location = new System.Drawing.Point(98, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(29, 12);
			this.label7.TabIndex = 12;
			this.label7.Text = "1 ：\u3000";
			this.label6.Location = new System.Drawing.Point(98, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 12);
			this.label6.TabIndex = 13;
			this.label6.Text = "1 ：\u3000";
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 12);
			this.label4.TabIndex = 14;
			this.label4.Text = "横向比例尺：";
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(153, 93);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.LtaOriuJT.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.LtaOriuJT.Location = new System.Drawing.Point(36, 93);
			this.LtaOriuJT.Name = "btnOK";
			this.LtaOriuJT.Size = new System.Drawing.Size(75, 23);
			this.LtaOriuJT.TabIndex = 2;
			this.LtaOriuJT.Text = "确定(&O)";
			this.LtaOriuJT.UseVisualStyleBackColor = true;
			this.LtaOriuJT.Click += new EventHandler(this.LtaOriuJT_Click);
			this.txBoxScaleY.Location = new System.Drawing.Point(139, 52);
			this.txBoxScaleY.MaxLength = 16;
			this.txBoxScaleY.Name = "txBoxScaleY";
			this.txBoxScaleY.Size = new System.Drawing.Size(99, 21);
			this.txBoxScaleY.TabIndex = 1;
			this.txBoxScaleY.KeyPress += new KeyPressEventHandler(this.txBoxScaleY_KeyPress);
			this.txBoxScaleX.Location = new System.Drawing.Point(139, 16);
			this.txBoxScaleX.MaxLength = 16;
			this.txBoxScaleX.Name = "txBoxScaleX";
			this.txBoxScaleX.Size = new System.Drawing.Size(99, 21);
			this.txBoxScaleX.TabIndex = 0;
			this.txBoxScaleX.KeyPress += new KeyPressEventHandler(this.txBoxScaleX_KeyPress);
			base.AcceptButton = this.LtaOriuJT;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(265, 128);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.LtaOriuJT);
			base.Controls.Add(this.txBoxScaleY);
			base.Controls.Add(this.txBoxScaleX);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetScaleDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "比例尺设置";
			base.Load += new EventHandler(this.SetScaleDlg_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LtaOriuJT_Click(object obj, EventArgs eventArg)
		{
			this.float_0 = Convert.ToSingle(this.txBoxScaleX.Text.Trim());
			this.float_1 = Convert.ToSingle(this.txBoxScaleY.Text.Trim());
		}

		private void SetScaleDlg_Load(object obj, EventArgs eventArg)
		{
			TextBox str = this.txBoxScaleX;
			float scaleX = this.ScaleX;
			str.Text = scaleX.ToString("f0");
			TextBox textBox = this.txBoxScaleY;
			scaleX = this.ScaleY;
			textBox.Text = scaleX.ToString("f0");
		}

		private void txBoxScaleX_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
		{
			char keyChar = keyPressEventArg.KeyChar;
			if (keyChar != '\b')
			{
				switch (keyChar)
				{
					case '0':
					{
						if (this.txBoxScaleX.SelectionStart != 0)
						{
							break;
						}
						keyPressEventArg.KeyChar = '\0';
						break;
					}
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						break;
					}
					default:
					{
						keyPressEventArg.KeyChar = '\0';
						break;
					}
				}
			}
		}

		private void txBoxScaleY_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
		{
			char keyChar = keyPressEventArg.KeyChar;
			if (keyChar != '\b')
			{
				switch (keyChar)
				{
					case '0':
					{
						if (this.txBoxScaleY.SelectionStart != 0)
						{
							break;
						}
						keyPressEventArg.KeyChar = '\0';
						break;
					}
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						break;
					}
					default:
					{
						keyPressEventArg.KeyChar = '\0';
						break;
					}
				}
			}
		}
	}
}