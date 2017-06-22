using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmSelectVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectVersion));
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.txtDescription = new TextEdit();
            this.cboVersions = new ComboBoxEdit();
            this.txtDescription.Properties.BeginInit();
            this.cboVersions.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(185, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择该连接将要访问的数据库版本";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 52);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "版本:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 88);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "说明:";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(72, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(152, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(64, 88);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtDescription.Properties.Appearance.Options.UseBackColor = true;
            this.txtDescription.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new Size(168, 19);
            this.txtDescription.TabIndex = 6;
            this.cboVersions.EditValue = "";
            this.cboVersions.Location = new Point(64, 48);
            this.cboVersions.Name = "cboVersions";
            this.cboVersions.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboVersions.Size = new Size(160, 21);
            this.cboVersions.TabIndex = 7;
            this.cboVersions.SelectedIndexChanged += new EventHandler(this.cboVersions_SelectedIndexChanged);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(250, 157);
            base.Controls.Add(this.cboVersions);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectVersion";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "选择版本";
            base.Load += new EventHandler(this.frmSelectVersion_Load);
            this.txtDescription.Properties.EndInit();
            this.cboVersions.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ComboBoxEdit cboVersions;
        private IEnumVersionInfo ienumVersionInfo_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private string string_0;
        private TextEdit txtDescription;
    }
}