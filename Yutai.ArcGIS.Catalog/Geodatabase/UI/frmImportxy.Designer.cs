using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmImportxy
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
            this.label2 = new Label();
            this.cmdFind = new SimpleButton();
            this.label1 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cboXField = new ComboBoxEdit();
            this.cboYField = new ComboBoxEdit();
            this.cboZField = new ComboBoxEdit();
            this.ExcelFullPath = new TextEdit();
            this.btnExport = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.openFileDialog_0 = new OpenFileDialog();
            this.cboXField.Properties.BeginInit();
            this.cboYField.Properties.BeginInit();
            this.cboZField.Properties.BeginInit();
            this.ExcelFullPath.Properties.BeginInit();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new Size(85, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "输入Excel文件";
            this.cmdFind.Location = new System.Drawing.Point(304, 40);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new Size(32, 23);
            this.cmdFind.TabIndex = 8;
            this.cmdFind.Text = "...";
            this.cmdFind.Click += new EventHandler(this.cmdFind_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 80);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "X坐标字段";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 112);
            this.label3.Name = "label3";
            this.label3.Size = new Size(60, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Y坐标字段";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 144);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Z坐标字段";
            this.cboXField.EditValue = "";
            this.cboXField.Location = new System.Drawing.Point(96, 72);
            this.cboXField.Name = "cboXField";
            this.cboXField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboXField.Size = new Size(200, 23);
            this.cboXField.TabIndex = 12;
            this.cboYField.EditValue = "";
            this.cboYField.Location = new System.Drawing.Point(96, 104);
            this.cboYField.Name = "cboYField";
            this.cboYField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboYField.Size = new Size(200, 23);
            this.cboYField.TabIndex = 13;
            this.cboZField.EditValue = "";
            this.cboZField.Location = new System.Drawing.Point(96, 136);
            this.cboZField.Name = "cboZField";
            this.cboZField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboZField.Size = new Size(200, 23);
            this.cboZField.TabIndex = 14;
            this.ExcelFullPath.EditValue = "";
            this.ExcelFullPath.Location = new System.Drawing.Point(16, 40);
            this.ExcelFullPath.Name = "ExcelFullPath";
            this.ExcelFullPath.Properties.ReadOnly = true;
            this.ExcelFullPath.Size = new Size(280, 23);
            this.ExcelFullPath.TabIndex = 15;
            this.btnExport.Location = new System.Drawing.Point(184, 176);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(48, 23);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "导入";
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton2.Location = new System.Drawing.Point(256, 176);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(48, 23);
            this.simpleButton2.TabIndex = 17;
            this.simpleButton2.Text = "取消";
            this.openFileDialog_0.DefaultExt = "xls";
            this.openFileDialog_0.Filter = "Excel (*.xls)|*.xls";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(344, 213);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnExport);
            base.Controls.Add(this.ExcelFullPath);
            base.Controls.Add(this.cboZField);
            base.Controls.Add(this.cboYField);
            base.Controls.Add(this.cboXField);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cmdFind);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmImportxy";
            this.Text = "从excel导入xy坐标点";
            this.cboXField.Properties.EndInit();
            this.cboYField.Properties.EndInit();
            this.cboZField.Properties.EndInit();
            this.ExcelFullPath.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnExport;
        private ComboBoxEdit cboXField;
        private ComboBoxEdit cboYField;
        private ComboBoxEdit cboZField;
        private SimpleButton cmdFind;
        private TextEdit ExcelFullPath;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private OpenFileDialog openFileDialog_0;
        private SimpleButton simpleButton2;
        private string string_0;
        private string string_1;
    }
}