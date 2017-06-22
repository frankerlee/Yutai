using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LayerDefinitionExpressionCtrl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.memoEdit = new MemoEdit();
            this.btnQueryDialog = new SimpleButton();
            this.memoEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "定义查询";
            this.memoEdit.EditValue = "";
            this.memoEdit.Location = new Point(16, 32);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Size = new Size(288, 136);
            this.memoEdit.TabIndex = 1;
            this.memoEdit.EditValueChanged += new EventHandler(this.memoEdit_EditValueChanged);
            this.btnQueryDialog.Location = new Point(24, 184);
            this.btnQueryDialog.Name = "btnQueryDialog";
            this.btnQueryDialog.Size = new Size(88, 24);
            this.btnQueryDialog.TabIndex = 2;
            this.btnQueryDialog.Text = "查询生成器";
            this.btnQueryDialog.Click += new EventHandler(this.btnQueryDialog_Click);
            base.Controls.Add(this.btnQueryDialog);
            base.Controls.Add(this.memoEdit);
            base.Controls.Add(this.label1);
            base.Name = "LayerDefinitionExpressionCtrl";
            base.Size = new Size(328, 232);
            base.Load += new EventHandler(this.LayerDefinitionExpressionCtrl_Load);
            this.memoEdit.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnQueryDialog;
        private Label label1;
        private MemoEdit memoEdit;
    }
}