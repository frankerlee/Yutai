using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class ExportToExcelWizard
    {

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportToExcelWizard));
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.btnLast = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(376, 216);
            this.panel1.TabIndex = 0;
            this.groupBox1.Location = new Point(0, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(368, 8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.btnLast.Location = new Point(152, 232);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(64, 24);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new Point(224, 232);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 24);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(296, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(376, 269);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
          
            base.Name = "ExportToExcelWizard";
            this.Text = "导出数据到Excel向导";
            base.Load += new EventHandler(this.ExportToExcelWizard_Load);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private GroupBox groupBox1;
        private Panel panel1;
    }
}