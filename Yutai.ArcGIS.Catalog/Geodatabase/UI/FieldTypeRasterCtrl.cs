using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class FieldTypeRasterCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnSelectSR;
        private Container container_0 = null;
        private IFieldEdit ifieldEdit_0;
        private IWorkspace iworkspace_0 = null;
        private TextEdit textEdit1;
        private TextEdit textEdit15;
        private TextEdit textEdit6;
        private TextEdit txtAlias;
        private TextEdit txtDescription;
        private TextEdit txtSpatialReference;

        public event FieldChangedHandler FieldChanged;

        public event ValueChangedHandler ValueChanged;

        public FieldTypeRasterCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectSR_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void FieldTypeRasterCtrl_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void FieldTypeRasterCtrl_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.method_1();
            }
        }

        public void Init()
        {
            this.method_1();
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
            this.textEdit1.Size = new Size(0x58, 0x13);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.Location = new Point(0x60, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(0x70, 0x13);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x60, 0x1b);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.BorderStyle = BorderStyles.Simple;
            this.txtDescription.Size = new Size(0x70, 0x13);
            this.txtDescription.TabIndex = 8;
            this.txtDescription.EditValueChanged += new EventHandler(this.txtDescription_EditValueChanged);
            this.textEdit6.EditValue = "描述";
            this.textEdit6.Location = new Point(8, 0x1b);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit6.Properties.ReadOnly = true;
            this.textEdit6.Size = new Size(0x58, 0x13);
            this.textEdit6.TabIndex = 7;
            this.textEdit15.EditValue = "空间参考";
            this.textEdit15.Location = new Point(8, 0x2e);
            this.textEdit15.Name = "textEdit15";
            this.textEdit15.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit15.Properties.ReadOnly = true;
            this.textEdit15.Size = new Size(0x58, 0x13);
            this.textEdit15.TabIndex = 0x15;
            this.txtSpatialReference.EditValue = "Unknown";
            this.txtSpatialReference.Location = new Point(0x60, 0x2e);
            this.txtSpatialReference.Name = "txtSpatialReference";
            this.txtSpatialReference.Properties.BorderStyle = BorderStyles.Simple;
            this.txtSpatialReference.Properties.ReadOnly = true;
            this.txtSpatialReference.Size = new Size(0x70, 0x13);
            this.txtSpatialReference.TabIndex = 0x16;
            this.btnSelectSR.Location = new Point(0xd0, 0x2e);
            this.btnSelectSR.Name = "btnSelectSR";
            this.btnSelectSR.Size = new Size(0x18, 0x13);
            this.btnSelectSR.TabIndex = 0x17;
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
            base.Size = new Size(240, 0xd0);
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

        private void txtDescription_EditValueChanged(object sender, EventArgs e)
        {
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

