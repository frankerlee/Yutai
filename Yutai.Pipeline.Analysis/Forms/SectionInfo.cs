using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class SectionInfo : XtraForm
	{

		private IContainer icontainer_0 = null;




		public ArrayList FillData
		{
			get
			{
				return this.arrayList_0;
			}
			set
			{
				this.arrayList_0 = value;
			}
		}

		public SectionInfo()
		{
			this.InitializeComponent();
		}

	public void RefreshDialog()
		{
			int count = this.arrayList_0.Count;
			this.dataGridView1.Rows.Clear();
			this.dataGridView1.ColumnCount = 2;
			this.dataGridView1.Columns[0].Name = "字段";
			this.dataGridView1.Columns[1].Name = "值";
			for (int i = 0; i < count; i++)
			{
				SectionInfoStore item = (SectionInfoStore)this.arrayList_0[i];
				this.dataGridView1.Rows.Add(new object[] { "" });
				this.dataGridView1[0, i].Value = item.strField;
				this.dataGridView1[1, i].Value = item.strVal;
			}
			base.Height = SystemInformation.CaptionHeight + this.dataGridView1.ColumnHeadersHeight + this.dataGridView1.RowTemplate.Height * (count + 1);
		}

		private void SectionInfo_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
		}

		private void SectionInfo_Load(object obj, EventArgs eventArg)
		{
			this.dataGridView1.ColumnCount = 2;
			this.dataGridView1.Columns[0].Name = "字段";
			this.dataGridView1.Columns[1].Name = "值";
		}
	}
}