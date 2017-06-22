using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class FieldTypeRasterCtrl
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
            this.txtDescription = new TextEdit();
            this.textEdit6 = new TextEdit();
            this.textEdit15 = new TextEdit();
            this.txtSpatialReference = new TextEdit();
            this.btnSelectSR = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            this.textEdit6.Properties.BeginInit();
            this.textEdit15.Properties.BeginInit();
            this.txtSpatialReference.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "别名";
            this.textEdit1.Location = new Point(8, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AllowFocused = false;
            this.textEdit1.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(88, 19);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.Location = new Point(96, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(112, 19);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(96, 27);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.BorderStyle = BorderStyles.Simple;
            this.txtDescription.Size = new Size(112, 19);
            this.txtDescription.TabIndex = 8;
            this.txtDescription.EditValueChanged += new EventHandler(this.txtDescription_EditValueChanged);
            this.textEdit6.EditValue = "描述";
            this.textEdit6.Location = new Point(8, 27);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit6.Properties.ReadOnly = true;
            this.textEdit6.Size = new Size(88, 19);
            this.textEdit6.TabIndex = 7;
            this.textEdit15.EditValue = "空间参考";
            this.textEdit15.Location = new Point(8, 46);
            this.textEdit15.Name = "textEdit15";
            this.textEdit15.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit15.Properties.ReadOnly = true;
            this.textEdit15.Size = new Size(88, 19);
            this.textEdit15.TabIndex = 21;
            this.txtSpatialReference.EditValue = "Unknown";
            this.txtSpatialReference.Location = new Point(96, 46);
            this.txtSpatialReference.Name = "txtSpatialReference";
            this.txtSpatialReference.Properties.BorderStyle = BorderStyles.Simple;
            this.txtSpatialReference.Properties.ReadOnly = true;
            this.txtSpatialReference.Size = new Size(112, 19);
            this.txtSpatialReference.TabIndex = 22;
            this.btnSelectSR.Location = new Point(208, 46);
            this.btnSelectSR.Name = "btnSelectSR";
            this.btnSelectSR.Size = new Size(24, 19);
            this.btnSelectSR.TabIndex = 23;
            this.btnSelectSR.Text = "...";
            this.btnSelectSR.Click += new EventHandler(this.btnSelectSR_Click);
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.btnSelectSR);
            base.Controls.Add(this.txtSpatialReference);
            base.Controls.Add(this.textEdit15);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.textEdit6);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeRasterCtrl";
            base.Size = new Size(240, 208);
            base.VisibleChanged += new EventHandler(this.FieldTypeRasterCtrl_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeRasterCtrl_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            this.textEdit6.Properties.EndInit();
            this.textEdit15.Properties.EndInit();
            this.txtSpatialReference.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnSelectSR;
        private IFieldEdit ifieldEdit_0;
        private TextEdit textEdit1;
        private TextEdit textEdit15;
        private TextEdit textEdit6;
        private TextEdit txtAlias;
        private TextEdit txtDescription;
        private TextEdit txtSpatialReference;
    }
}