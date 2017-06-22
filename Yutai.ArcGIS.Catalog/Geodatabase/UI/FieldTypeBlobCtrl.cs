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
    internal partial class FieldTypeBlobCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IWorkspace iworkspace_0 = null;

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

