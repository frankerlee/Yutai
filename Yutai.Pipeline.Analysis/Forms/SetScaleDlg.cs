using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class SetScaleDlg : XtraForm
	{
		private IContainer icontainer_0 = null;











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