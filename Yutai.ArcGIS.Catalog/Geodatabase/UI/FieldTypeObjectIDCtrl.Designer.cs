using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class FieldTypeObjectIDCtrl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.textEdit1 = new TextEdit();
            this.txtAlias = new TextEdit();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "别名";
            this.textEdit1.Location = new Point(8, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AllowFocused = false;
            this.textEdit1.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(88, 19);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.EditValue = "OBJECTID";
            this.txtAlias.Location = new Point(96, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(112, 19);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeObjectIDCtrl";
            base.Size = new Size(240, 208);
            base.VisibleChanged += new EventHandler(this.FieldTypeObjectIDCtrl_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeObjectIDCtrl_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private IFieldEdit ifieldEdit_0;
        private TextEdit textEdit1;
        private TextEdit txtAlias;
    }
}