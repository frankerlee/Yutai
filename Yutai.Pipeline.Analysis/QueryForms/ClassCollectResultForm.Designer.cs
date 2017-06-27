using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Controls;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class ClassCollectResultForm
    {
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
            this.components = new System.ComponentModel.Container();
            this._dataGridView = new Yutai.Pipeline.Analysis.Controls.DataGridViewEx(this.components);
            this.PrintButton = new System.Windows.Forms.Button();
            this.ToExcel = new System.Windows.Forms.Button();
            this.CloseBut = new System.Windows.Forms.Button();
            this.SaveExcelDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.Location = new System.Drawing.Point(0, 43);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.Size = new System.Drawing.Size(493, 261);
            this._dataGridView.TabIndex = 0;
            this._dataGridView.Text = "_dataGridView";
            this._dataGridView.Title = null;
            // 
            // PrintButton
            // 
            this.PrintButton.Location = new System.Drawing.Point(4, 7);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(75, 23);
            this.PrintButton.TabIndex = 12;
            this.PrintButton.Text = "打印";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // ToExcel
            // 
            this.ToExcel.Location = new System.Drawing.Point(81, 7);
            this.ToExcel.Name = "ToExcel";
            this.ToExcel.Size = new System.Drawing.Size(75, 23);
            this.ToExcel.TabIndex = 11;
            this.ToExcel.Text = "转出EXCEL";
            this.ToExcel.UseVisualStyleBackColor = true;
            this.ToExcel.Click += new System.EventHandler(this.ToExcel_Click);
            // 
            // CloseBut
            // 
            this.CloseBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBut.Location = new System.Drawing.Point(409, 7);
            this.CloseBut.Name = "CloseBut";
            this.CloseBut.Size = new System.Drawing.Size(75, 23);
            this.CloseBut.TabIndex = 13;
            this.CloseBut.Text = "关闭";
            this.CloseBut.UseVisualStyleBackColor = true;
            this.CloseBut.Click += new System.EventHandler(this.CloseBut_Click);
            // 
            // SaveExcelDlg
            // 
            this.SaveExcelDlg.DefaultExt = "xls";
            this.SaveExcelDlg.FileName = "Result.xls";
            this.SaveExcelDlg.Filter = "Excel文件|*.xls";
            // 
            // ClassCollectResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 304);
            this.Controls.Add(this.CloseBut);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.ToExcel);
            this.Controls.Add(this._dataGridView);
            this.Name = "ClassCollectResultForm";
            this.ShowIcon = false;
            this.Text = "汇总结果";
            this.Load += new System.EventHandler(this.ClassCollectResultForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);

		}
	
		private IContainer components = null;
		private Button PrintButton;
		private Button ToExcel;
		private Button CloseBut;
		private SaveFileDialog SaveExcelDlg;
        private DataGridViewEx _dataGridView;
    }
}