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
    internal class FieldTypeBlobCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBoxEdit cboAllowNull;
        private Container container_0 = null;
        private IFieldEdit ifieldEdit_0;
        private IWorkspace iworkspace_0 = null;
        private TextEdit textEdit1;
        private TextEdit textEdit4;
        private TextEdit textEdit8;
        private TextEdit txtAlias;
        private TextEdit txtLength;

        public event FieldChangedHandler FieldChanged;

        public event ValueChangedHandler ValueChanged;

        public FieldTypeBlobCtrl()
        {
            this.InitializeComponent();
        }

        private void cboAllowNull_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.IsNullable_2 = this.cboAllowNull.SelectedIndex == 1;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void FieldTypeBlobCtrl_Load(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.method_1();
            }
        }

        private void FieldTypeBlobCtrl_VisibleChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        public void Init()
        {
            this.method_1();
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
            this.textEdit1.Size = new Size(0x58, 0x15);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.Location = new Point(0x60, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(0x70, 0x15);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.textEdit4.EditValue = "允许空值";
            this.textEdit4.Location = new Point(8, 0x1b);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit4.Properties.ReadOnly = true;
            this.textEdit4.Size = new Size(0x58, 0x15);
            this.textEdit4.TabIndex = 2;
            this.cboAllowNull.EditValue = "是";
            this.cboAllowNull.Location = new Point(0x60, 0x1b);
            this.cboAllowNull.Name = "cboAllowNull";
            this.cboAllowNull.Properties.BorderStyle = BorderStyles.Simple;
            this.cboAllowNull.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboAllowNull.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboAllowNull.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboAllowNull.Size = new Size(0x70, 0x15);
            this.cboAllowNull.TabIndex = 4;
            this.cboAllowNull.SelectedIndexChanged += new EventHandler(this.cboAllowNull_SelectedIndexChanged);
            this.txtLength.EditValue = "0";
            this.txtLength.Location = new Point(0x60, 0x2e);
            this.txtLength.Name = "txtLength";
            this.txtLength.Properties.BorderStyle = BorderStyles.Simple;
            this.txtLength.Size = new Size(0x70, 0x15);
            this.txtLength.TabIndex = 10;
            this.txtLength.EditValueChanged += new EventHandler(this.txtLength_EditValueChanged);
            this.textEdit8.EditValue = "长度";
            this.textEdit8.Location = new Point(8, 0x2e);
            this.textEdit8.Name = "textEdit8";
            this.textEdit8.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit8.Properties.ReadOnly = true;
            this.textEdit8.Size = new Size(0x58, 0x15);
            this.textEdit8.TabIndex = 9;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.txtLength);
            base.Controls.Add(this.textEdit8);
            base.Controls.Add(this.cboAllowNull);
            base.Controls.Add(this.textEdit4);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeBlobCtrl";
            base.Size = new Size(240, 0x90);
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

        private void method_0(IField ifield_0, FieldChangeType fieldChangeType_0)
        {
            if (this.FieldChanged != null)
            {
                this.FieldChanged(ifield_0, fieldChangeType_0);
            }
        }

        private void method_1()
        {
            this.bool_0 = false;
            this.txtAlias.Text = this.ifieldEdit_0.AliasName;
            this.txtLength.Text = this.ifieldEdit_0.Length.ToString();
            this.cboAllowNull.SelectedIndex = Convert.ToInt16(this.ifieldEdit_0.IsNullable);
            this.txtLength.Properties.ReadOnly = this.bool_1;
            this.cboAllowNull.Enabled = !this.bool_1;
            this.bool_0 = true;
        }

        private void txtAlias_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.AliasName_2 = this.txtAlias.Text;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
                this.method_0(this.ifieldEdit_0, FieldChangeType.FCTAlias);
            }
        }

        private void txtLength_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (CommonHelper.IsNmuber(this.txtLength.Text))
                {
                    this.txtLength.ForeColor = SystemColors.WindowText;
                    this.ifieldEdit_0.Length_2 = Convert.ToInt32(this.txtLength.Text);
                }
                else
                {
                    this.txtLength.ForeColor = Color.Red;
                }
            }
        }

        public IField Filed
        {
            set
            {
                this.ifieldEdit_0 = value as IFieldEdit;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

