using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Excel;
using Yutai.ArcGIS.Controls.Controls.Export;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmAnalystTable
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalystTable));
            this.dataGrid1 = new DataGrid();
            this.panel1 = new Panel();
            this.btnExportGraphic = new SimpleButton();
            this.btnPrint = new SimpleButton();
            this.btnExportToExcel = new SimpleButton();
            this.btnSaveEditing = new SimpleButton();
            this.btnStopEditing = new SimpleButton();
            this.btnStartEditing = new SimpleButton();
            this.btnEnd = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.txtIndex = new TextEdit();
            this.btnLast = new SimpleButton();
            this.btnFirstRecord = new SimpleButton();
            this.dataGrid1.BeginInit();
            this.panel1.SuspendLayout();
            this.txtIndex.Properties.BeginInit();
            base.SuspendLayout();
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = DockStyle.Fill;
            this.dataGrid1.HeaderForeColor = SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new Size(480, 249);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.CurrentCellChanged += new EventHandler(this.dataGrid1_CurrentCellChanged);
            this.panel1.Controls.Add(this.btnExportGraphic);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnExportToExcel);
            this.panel1.Controls.Add(this.btnSaveEditing);
            this.panel1.Controls.Add(this.btnStopEditing);
            this.panel1.Controls.Add(this.btnStartEditing);
            this.panel1.Controls.Add(this.btnEnd);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.txtIndex);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnFirstRecord);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(480, 24);
            this.panel1.TabIndex = 3;
            this.btnExportGraphic.Enabled = false;
            this.btnExportGraphic.Location = new System.Drawing.Point(385, 3);
            this.btnExportGraphic.Name = "btnExportGraphic";
            this.btnExportGraphic.Size = new Size(80, 21);
            this.btnExportGraphic.TabIndex = 13;
            this.btnExportGraphic.Text = "生成图表";
            this.btnExportGraphic.Visible = false;
            this.btnExportGraphic.Click += new EventHandler(this.btnExportGraphic_Click);
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(297, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(80, 21);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "打印预览";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.btnExportToExcel.Enabled = false;
            this.btnExportToExcel.Location = new System.Drawing.Point(206, 2);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new Size(80, 21);
            this.btnExportToExcel.TabIndex = 11;
            this.btnExportToExcel.Text = "导出到Excel";
            this.btnExportToExcel.Visible = false;
            this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
            this.btnSaveEditing.Enabled = false;
            this.btnSaveEditing.Location = new System.Drawing.Point(128, 0);
            this.btnSaveEditing.Name = "btnSaveEditing";
            this.btnSaveEditing.Size = new Size(64, 21);
            this.btnSaveEditing.TabIndex = 10;
            this.btnSaveEditing.Text = "保存编辑";
            this.btnSaveEditing.Visible = false;
            this.btnSaveEditing.Click += new EventHandler(this.btnSaveEditing_Click);
            this.btnStopEditing.Enabled = false;
            this.btnStopEditing.Location = new System.Drawing.Point(136, 0);
            this.btnStopEditing.Name = "btnStopEditing";
            this.btnStopEditing.Size = new Size(64, 21);
            this.btnStopEditing.TabIndex = 9;
            this.btnStopEditing.Text = "结束编辑";
            this.btnStopEditing.Visible = false;
            this.btnStopEditing.Click += new EventHandler(this.btnStopEditing_Click);
            this.btnStartEditing.Location = new System.Drawing.Point(104, 1);
            this.btnStartEditing.Name = "btnStartEditing";
            this.btnStartEditing.Size = new Size(64, 21);
            this.btnStartEditing.TabIndex = 8;
            this.btnStartEditing.Text = "开始编辑";
            this.btnStartEditing.Visible = false;
            this.btnStartEditing.Click += new EventHandler(this.btnStartEditing_Click);
            this.btnEnd.Image = (Image) resources.GetObject("btnEnd.Image");
            this.btnEnd.Location = new System.Drawing.Point(160, 1);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new Size(24, 21);
            this.btnEnd.TabIndex = 4;
            this.btnEnd.Visible = false;
            this.btnEnd.Click += new EventHandler(this.btnEnd_Click);
            this.btnNext.Image = (Image) resources.GetObject("btnNext.Image");
            this.btnNext.Location = new System.Drawing.Point(136, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(24, 21);
            this.btnNext.TabIndex = 3;
            this.btnNext.Visible = false;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.txtIndex.EditValue = "1";
            this.txtIndex.Location = new System.Drawing.Point(56, 1);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new Size(80, 21);
            this.txtIndex.TabIndex = 2;
            this.txtIndex.Visible = false;
            this.txtIndex.KeyUp += new KeyEventHandler(this.txtIndex_KeyUp);
            this.btnLast.Image = (Image) resources.GetObject("btnLast.Image");
            this.btnLast.Location = new System.Drawing.Point(32, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(24, 21);
            this.btnLast.TabIndex = 1;
            this.btnLast.Visible = false;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnFirstRecord.Image = (Image) resources.GetObject("btnFirstRecord.Image");
            this.btnFirstRecord.Location = new System.Drawing.Point(8, 1);
            this.btnFirstRecord.Name = "btnFirstRecord";
            this.btnFirstRecord.Size = new Size(24, 21);
            this.btnFirstRecord.TabIndex = 0;
            this.btnFirstRecord.Visible = false;
            this.btnFirstRecord.Click += new EventHandler(this.btnFirstRecord_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(480, 273);
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmAnalystTable";
            this.Text = "分析结果";
            base.Load += new EventHandler(this.frmTable_Load);
            this.dataGrid1.EndInit();
            this.panel1.ResumeLayout(false);
            this.txtIndex.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private SimpleButton btnEnd;
        private SimpleButton btnExportGraphic;
        private SimpleButton btnExportToExcel;
        private SimpleButton btnFirstRecord;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private SimpleButton btnPrint;
        private SimpleButton btnSaveEditing;
        private SimpleButton btnStartEditing;
        private SimpleButton btnStopEditing;
        private DataGrid dataGrid1;
        private Panel panel1;
        private TextEdit txtIndex;
    }
}