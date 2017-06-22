using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class SectionOption : XtraForm
	{
		private IContainer icontainer_0 = null;














		public bool bFalse
		{
			get
			{
				return this.FalseBox.Checked;
			}
			set
			{
				this.FalseBox.Checked = value;
			}
		}

		public bool bRotate
		{
			get
			{
				return this.checkBox1.Checked;
			}
			set
			{
				this.checkBox1.Checked = value;
			}
		}

		public string RoadName
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public string SectionNo
		{
			get
			{
				return this.string_2;
			}
			set
			{
				this.string_2 = value;
			}
		}

		public string Title
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public SectionOption()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object obj, EventArgs eventArg)
		{
		}

		private void btnOK_Click(object obj, EventArgs eventArg)
		{
			this.Title = this.txBoxTitle.Text.Trim();
			this.RoadName = this.txBoxRoad.Text.Trim();
			this.SectionNo = this.txBoxNo.Text.Trim();
		}

	private void method_0(object obj, EventArgs eventArg)
		{
		}

		private void SectionOption_Load(object obj, EventArgs eventArg)
		{
			this.txBoxTitle.Text = this.Title;
			this.txBoxRoad.Text = this.RoadName;
			this.txBoxNo.Text = this.SectionNo;
		}
	}
}