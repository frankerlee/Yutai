using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class DistMeausreDlg : XtraForm
	{
		public IAppContext m_app;

		private IContainer icontainer_0 = null;





		public double CurDist
		{
			set
			{
				double num = value;
				if (value < 1000)
				{
					this.txBoxCurLen.Text = string.Concat(value.ToString("f3"), "米");
				}
				else
				{
					num = num / 1000;
					this.txBoxCurLen.Text = string.Concat(num.ToString("f3"), "千米");
				}
			}
		}

		public double TotalDist
		{
			set
			{
				double num = value;
				if (value < 1000)
				{
					this.txBoxTotalLen.Text = string.Concat(value.ToString("f3"), "米");
				}
				else
				{
					num = num / 1000;
					this.txBoxTotalLen.Text = string.Concat(num.ToString("f3"), "千米");
				}
			}
		}

		public DistMeausreDlg()
		{
			this.InitializeComponent();
		}

	private void DistMeausreDlg_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
		{
		}

		private void DistMeausreDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
		}

		private void DistMeausreDlg_Load(object obj, EventArgs eventArg)
		{
		}

	public void InitMeasureDlg()
		{
			this.txBoxCurLen.Text = "";
			this.txBoxTotalLen.Text = "";
		}

		private void method_0(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "距离量算");
		}
	}
}