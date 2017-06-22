using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class SectionInfo
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle window = new DataGridViewCellStyle();
			DataGridViewCellStyle control = new DataGridViewCellStyle();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeight = 20;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2 });
			
			window.Alignment = DataGridViewContentAlignment.MiddleLeft;
			window.BackColor = SystemColors.Window;
			window.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			window.ForeColor = SystemColors.ControlText;
			window.SelectionBackColor = SystemColors.Highlight;
			window.SelectionForeColor = SystemColors.HighlightText;
			window.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = window;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			control.Alignment = DataGridViewContentAlignment.MiddleLeft;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			control.ForeColor = SystemColors.WindowText;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = control;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 19;
			this.dataGridView1.RowTemplate.Resizable = DataGridViewTriState.False;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(209, 181);
			this.dataGridView1.TabIndex = 0;
			this.Column1.HeaderText = "字段";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Column2.HeaderText = "值";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(209, 181);
			base.Controls.Add(this.dataGridView1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SectionInfo";
			base.ShowIcon = false;
			this.Text = "属性";
			base.FormClosing += new FormClosingEventHandler(this.SectionInfo_FormClosing);
			base.Load += new EventHandler(this.SectionInfo_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

	
		private ArrayList arrayList_0;
		private DataGridView dataGridView1;
		private DataGridViewTextBoxColumn Column1;
		private DataGridViewTextBoxColumn Column2;
    }
}