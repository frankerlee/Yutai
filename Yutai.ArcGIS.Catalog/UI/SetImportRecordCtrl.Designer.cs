using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class SetImportRecordCtrl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.btnQueryDialog = new SimpleButton();
            this.memoEdit = new MemoEdit();
            this.memoEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(34, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(95, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "导入所有记录";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(34, 59);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(155, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "导入满足查询条件的记录";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.btnQueryDialog.Enabled = false;
            this.btnQueryDialog.Location = new Point(74, 92);
            this.btnQueryDialog.Name = "btnQueryDialog";
            this.btnQueryDialog.Size = new Size(88, 24);
            this.btnQueryDialog.TabIndex = 4;
            this.btnQueryDialog.Text = "查询生成器";
            this.btnQueryDialog.Click += new EventHandler(this.btnQueryDialog_Click);
            this.memoEdit.EditValue = "";
            this.memoEdit.Location = new Point(34, 129);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Properties.ReadOnly = true;
            this.memoEdit.Size = new Size(288, 136);
            this.memoEdit.TabIndex = 3;
            this.memoEdit.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnQueryDialog);
            base.Controls.Add(this.memoEdit);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Name = "SetImportRecordCtrl";
            base.Size = new Size(408, 286);
            this.memoEdit.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnQueryDialog;
        private ITable itable_0;
        private MemoEdit memoEdit;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
    }
}