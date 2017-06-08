using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmLableRotateField : Form
    {
        private SimpleButton btnOK;
        private ComboBoxEdit cboFields;
        private CheckEdit chkPerpendicularToAngle;
        private Container container_0 = null;
        private Label label2;
        public bool m_PerpendicularToAngle = false;
        public IFields m_pFields = null;
        public esriLabelRotationType m_RotationType = esriLabelRotationType.esriRotateLabelGeographic;
        public string m_RoteteField = "";
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private RadioGroup rdoRotationType;
        private SimpleButton simpleButton2;

        public frmLableRotateField()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_RoteteField = this.cboFields.Text;
            this.m_PerpendicularToAngle = this.chkPerpendicularToAngle.Checked;
            if (this.rdoRotationType.SelectedIndex == 0)
            {
                this.m_RotationType = esriLabelRotationType.esriRotateLabelGeographic;
            }
            else
            {
                this.m_RotationType = esriLabelRotationType.esriRotateLabelArithmetic;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmLableRotateField_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_pFields.FieldCount; i++)
            {
                IField field = this.m_pFields.get_Field(i);
                if (((((field.Type == esriFieldType.esriFieldTypeDate) || (field.Type == esriFieldType.esriFieldTypeDouble)) || ((field.Type == esriFieldType.esriFieldTypeGlobalID) || (field.Type == esriFieldType.esriFieldTypeGUID))) || (((field.Type == esriFieldType.esriFieldTypeInteger) || (field.Type == esriFieldType.esriFieldTypeOID)) || ((field.Type == esriFieldType.esriFieldTypeSingle) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)))) || (field.Type == esriFieldType.esriFieldTypeString))
                {
                    this.cboFields.Properties.Items.Add(field.Name);
                }
            }
            this.cboFields.Text = this.m_RoteteField;
            if (this.m_RotationType == esriLabelRotationType.esriRotateLabelGeographic)
            {
                this.rdoRotationType.SelectedIndex = 0;
            }
            else
            {
                this.rdoRotationType.SelectedIndex = 1;
            }
            this.chkPerpendicularToAngle.Checked = this.m_PerpendicularToAngle;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLableRotateField));
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit1 = new PictureEdit();
            this.chkPerpendicularToAngle = new CheckEdit();
            this.cboFields = new ComboBoxEdit();
            this.rdoRotationType = new RadioGroup();
            this.label2 = new Label();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.chkPerpendicularToAngle.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            this.rdoRotationType.Properties.BeginInit();
            base.SuspendLayout();
            this.pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(0x60, 40);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(0x3b, 0x3b);
            this.pictureEdit2.TabIndex = 15;
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0x18, 40);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(0x3b, 0x3b);
            this.pictureEdit1.TabIndex = 14;
            this.chkPerpendicularToAngle.Location = new Point(0x18, 0x80);
            this.chkPerpendicularToAngle.Name = "chkPerpendicularToAngle";
            this.chkPerpendicularToAngle.Properties.Caption = "标注方向垂直于该角度";
            this.chkPerpendicularToAngle.Size = new Size(0x98, 0x13);
            this.chkPerpendicularToAngle.TabIndex = 13;
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x38, 8);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x70, 0x17);
            this.cboFields.TabIndex = 12;
            this.rdoRotationType.Location = new Point(0x18, 0x60);
            this.rdoRotationType.Name = "rdoRotationType";
            this.rdoRotationType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoRotationType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoRotationType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoRotationType.Properties.Columns = 2;
            this.rdoRotationType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "地理的"), new RadioGroupItem(null, "数学的") });
            this.rdoRotationType.Size = new Size(0x98, 0x20);
            this.rdoRotationType.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 10;
            this.label2.Text = "字段";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(40, 0x98);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0x10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0x70, 0x98);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 0x11;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(200, 0xbd);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.pictureEdit2);
            base.Controls.Add(this.pictureEdit1);
            base.Controls.Add(this.chkPerpendicularToAngle);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.rdoRotationType);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLableRotateField";
            this.Text = "旋转";
            base.Load += new EventHandler(this.frmLableRotateField_Load);
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.chkPerpendicularToAngle.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            this.rdoRotationType.Properties.EndInit();
            base.ResumeLayout(false);
        }
    }
}

