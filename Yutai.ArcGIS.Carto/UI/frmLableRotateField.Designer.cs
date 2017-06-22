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
    partial class frmLableRotateField
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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
            this.pictureEdit2.Location = new Point(96, 40);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(59, 59);
            this.pictureEdit2.TabIndex = 15;
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(24, 40);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(59, 59);
            this.pictureEdit1.TabIndex = 14;
            this.chkPerpendicularToAngle.Location = new Point(24, 128);
            this.chkPerpendicularToAngle.Name = "chkPerpendicularToAngle";
            this.chkPerpendicularToAngle.Properties.Caption = "标注方向垂直于该角度";
            this.chkPerpendicularToAngle.Size = new Size(152, 19);
            this.chkPerpendicularToAngle.TabIndex = 13;
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(56, 8);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(112, 23);
            this.cboFields.TabIndex = 12;
            this.rdoRotationType.Location = new Point(24, 96);
            this.rdoRotationType.Name = "rdoRotationType";
            this.rdoRotationType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoRotationType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoRotationType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoRotationType.Properties.Columns = 2;
            this.rdoRotationType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "地理的"), new RadioGroupItem(null, "数学的") });
            this.rdoRotationType.Size = new Size(152, 32);
            this.rdoRotationType.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "字段";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(40, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(112, 152);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 17;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(200, 189);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.pictureEdit2);
            base.Controls.Add(this.pictureEdit1);
            base.Controls.Add(this.chkPerpendicularToAngle);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.rdoRotationType);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
    
        private SimpleButton btnOK;
        private ComboBoxEdit cboFields;
        private CheckEdit chkPerpendicularToAngle;
        private Label label2;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private RadioGroup rdoRotationType;
        private SimpleButton simpleButton2;
    }
}