using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class AreaMeasureDlg : XtraForm
	{
		private IContainer icontainer_0 = null;





		public IAppContext m_app;

		public double CurArea
		{
			set
			{
				double num = value;
				if (num <= 1000000)
				{
					this.txBoxCurArea.Text = string.Concat(value.ToString("f3"), "平方米");
				}
				else
				{
					num = num / 1000000;
					this.txBoxCurArea.Text = string.Concat(num.ToString("f3"), "平方千米");
				}
			}
		}

		public double CurPerimeter
		{
			set
			{
				double num = value;
				if (value < 1000)
				{
					this.txBoxPerimeter.Text = string.Concat(value.ToString("f3"), "米");
				}
				else
				{
					num = num / 1000;
					this.txBoxPerimeter.Text = string.Concat(num.ToString("f3"), "千米");
				}
			}
		}

		public AreaMeasureDlg()
		{
			this.InitializeComponent();
		}

		private void AreaMeasureDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
		}

		private void AreaMeasureDlg_Load(object obj, EventArgs eventArg)
		{
		}

	public void InitMeasureDlg()
		{
			this.txBoxCurArea.Text = "";
			this.txBoxPerimeter.Text = "";
		}

		private void label1_Click(object obj, EventArgs eventArg)
		{
		}

		private void method_0(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "面积量算");
		}
	}
}