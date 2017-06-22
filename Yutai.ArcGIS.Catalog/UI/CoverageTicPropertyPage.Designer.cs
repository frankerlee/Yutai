using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class CoverageTicPropertyPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.dataGrid1 = new DataGrid();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtMinX = new TextEdit();
            this.txtMinY = new TextEdit();
            this.txtMaxX = new TextEdit();
            this.txtMaxY = new TextEdit();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.dataGrid1.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtMinX.Properties.BeginInit();
            this.txtMinY.Properties.BeginInit();
            this.txtMaxX.Properties.BeginInit();
            this.txtMaxY.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.dataGrid1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 128);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tic点";
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(16, 24);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new Size(272, 88);
            this.dataGrid1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.txtMaxY);
            this.groupBox2.Controls.Add(this.txtMaxX);
            this.groupBox2.Controls.Add(this.txtMinY);
            this.groupBox2.Controls.Add(this.txtMinX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 144);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "范围";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(66, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小X坐标:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(66, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "最小Y坐标:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 112);
            this.label3.Name = "label3";
            this.label3.Size = new Size(66, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "最大Y坐标:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(66, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "最大X坐标:";
            this.txtMinX.EditValue = "";
            this.txtMinX.Location = new System.Drawing.Point(88, 18);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Properties.ReadOnly = true;
            this.txtMinX.Size = new Size(200, 23);
            this.txtMinX.TabIndex = 4;
            this.txtMinY.EditValue = "";
            this.txtMinY.Location = new System.Drawing.Point(88, 48);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Properties.ReadOnly = true;
            this.txtMinY.Size = new Size(200, 23);
            this.txtMinY.TabIndex = 5;
            this.txtMaxX.EditValue = "";
            this.txtMaxX.Location = new System.Drawing.Point(88, 78);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Properties.ReadOnly = true;
            this.txtMaxX.Size = new Size(200, 23);
            this.txtMaxX.TabIndex = 6;
            this.txtMaxY.EditValue = "";
            this.txtMaxY.Location = new System.Drawing.Point(88, 109);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Properties.ReadOnly = true;
            this.txtMaxY.Size = new Size(200, 23);
            this.txtMaxY.TabIndex = 7;
            this.btnAdd.Location = new System.Drawing.Point(304, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(48, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "增加";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new System.Drawing.Point(304, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(48, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "CoverageTicPropertyPage";
            base.Size = new Size(392, 312);
            base.Load += new EventHandler(this.CoverageTicPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.dataGrid1.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtMinX.Properties.EndInit();
            this.txtMinY.Properties.EndInit();
            this.txtMaxX.Properties.EndInit();
            this.txtMaxY.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private DataGrid dataGrid1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtMaxX;
        private TextEdit txtMaxY;
        private TextEdit txtMinX;
        private TextEdit txtMinY;
    }
}