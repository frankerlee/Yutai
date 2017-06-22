using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class FieldTypeBlobCtrl
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
            this.txtLength = new TextEdit();
            this.textEdit8 = new TextEdit();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            this.textEdit4.Properties.BeginInit();
            this.cboAllowNull.Properties.BeginInit();
            this.txtLength.Properties.BeginInit();
            this.textEdit8.Properties.BeginInit();
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
            this.txtLength.EditValue = "0";
            this.txtLength.Location = new Point(96, 46);
            this.txtLength.Name = "txtLength";
            this.txtLength.Properties.BorderStyle = BorderStyles.Simple;
            this.txtLength.Size = new Size(112, 21);
            this.txtLength.TabIndex = 10;
            this.txtLength.EditValueChanged += new EventHandler(this.txtLength_EditValueChanged);
            this.textEdit8.EditValue = "长度";
            this.textEdit8.Location = new Point(8, 46);
            this.textEdit8.Name = "textEdit8";
            this.textEdit8.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit8.Properties.ReadOnly = true;
            this.textEdit8.Size = new Size(88, 21);
            this.textEdit8.TabIndex = 9;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.txtLength);
            base.Controls.Add(this.textEdit8);
            base.Controls.Add(this.cboAllowNull);
            base.Controls.Add(this.textEdit4);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeBlobCtrl";
            base.Size = new Size(240, 144);
            base.VisibleChanged += new EventHandler(this.FieldTypeBlobCtrl_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeBlobCtrl_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            this.textEdit4.Properties.EndInit();
            this.cboAllowNull.Properties.EndInit();
            this.txtLength.Properties.EndInit();
            this.textEdit8.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit cboAllowNull;
        private IFieldEdit ifieldEdit_0;
        private TextEdit textEdit1;
        private TextEdit textEdit4;
        private TextEdit textEdit8;
        private TextEdit txtAlias;
        private TextEdit txtLength;
    }
}