using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.CodeDomainEx;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class FieldTypeDateCtrl
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
            this.textEdit4 = new TextEdit();
            this.cboAllowNull = new ComboBoxEdit();
            this.cboDomain = new ComboBoxEdit();
            this.textEdit3 = new TextEdit();
            this.txtDefault = new TextEdit();
            this.textEdit6 = new TextEdit();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            this.textEdit4.Properties.BeginInit();
            this.cboAllowNull.Properties.BeginInit();
            this.cboDomain.Properties.BeginInit();
            this.textEdit3.Properties.BeginInit();
            this.txtDefault.Properties.BeginInit();
            this.textEdit6.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "别名";
            this.textEdit1.Location = new Point(8, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AllowFocused = false;
            this.textEdit1.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(88, 21);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.Location = new Point(96, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(112, 21);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.textEdit4.EditValue = "允许空值";
            this.textEdit4.Location = new Point(8, 27);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit4.Properties.ReadOnly = true;
            this.textEdit4.Size = new Size(88, 21);
            this.textEdit4.TabIndex = 2;
            this.cboAllowNull.EditValue = "是";
            this.cboAllowNull.Location = new Point(96, 27);
            this.cboAllowNull.Name = "cboAllowNull";
            this.cboAllowNull.Properties.BorderStyle = BorderStyles.Simple;
            this.cboAllowNull.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboAllowNull.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboAllowNull.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboAllowNull.Size = new Size(112, 21);
            this.cboAllowNull.TabIndex = 4;
            this.cboAllowNull.SelectedIndexChanged += new EventHandler(this.cboAllowNull_SelectedIndexChanged);
            this.cboDomain.EditValue = "";
            this.cboDomain.Location = new Point(96, 46);
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Properties.BorderStyle = BorderStyles.Simple;
            this.cboDomain.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDomain.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboDomain.Size = new Size(112, 21);
            this.cboDomain.TabIndex = 6;
            this.cboDomain.SelectedIndexChanged += new EventHandler(this.cboDomain_SelectedIndexChanged);
            this.textEdit3.EditValue = "域";
            this.textEdit3.Location = new Point(8, 46);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit3.Properties.ReadOnly = true;
            this.textEdit3.Size = new Size(88, 21);
            this.textEdit3.TabIndex = 5;
            this.txtDefault.EditValue = "";
            this.txtDefault.Location = new Point(96, 65);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Properties.BorderStyle = BorderStyles.Simple;
            this.txtDefault.Size = new Size(112, 21);
            this.txtDefault.TabIndex = 8;
            this.txtDefault.EditValueChanged += new EventHandler(this.txtDefault_EditValueChanged);
            this.textEdit6.EditValue = "默认值";
            this.textEdit6.Location = new Point(8, 65);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit6.Properties.ReadOnly = true;
            this.textEdit6.Size = new Size(88, 21);
            this.textEdit6.TabIndex = 7;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.txtDefault);
            base.Controls.Add(this.textEdit6);
            base.Controls.Add(this.cboDomain);
            base.Controls.Add(this.textEdit3);
            base.Controls.Add(this.cboAllowNull);
            base.Controls.Add(this.textEdit4);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeDateCtrl";
            base.Size = new Size(240, 144);
            base.VisibleChanged += new EventHandler(this.FieldTypeDateCtrl_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeDateCtrl_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            this.textEdit4.Properties.EndInit();
            this.cboAllowNull.Properties.EndInit();
            this.cboDomain.Properties.EndInit();
            this.textEdit3.Properties.EndInit();
            this.txtDefault.Properties.EndInit();
            this.textEdit6.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit cboAllowNull;
        private ComboBoxEdit cboDomain;
        private IFieldEdit ifieldEdit_0;
        private TextEdit textEdit1;
        private TextEdit textEdit3;
        private TextEdit textEdit4;
        private TextEdit textEdit6;
        private TextEdit txtAlias;
        private TextEdit txtDefault;
    }
}