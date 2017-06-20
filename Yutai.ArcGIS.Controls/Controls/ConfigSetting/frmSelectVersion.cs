using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal class frmSelectVersion : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ComboBoxEdit cboVersions;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private IEnumVersionInfo m_pEnumVersionInfo;
        private IList m_pList = new ArrayList();
        private string m_VersionName;
        private TextEdit txtDescription;

        public frmSelectVersion()
        {
            this.InitializeComponent();
        }

        private void cboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_VersionName = this.cboVersions.Text;
            this.txtDescription.Text = this.m_pList[this.cboVersions.SelectedIndex].ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelectVersion_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            if (this.m_pEnumVersionInfo != null)
            {
                this.m_pEnumVersionInfo.Reset();
                this.m_pList.Clear();
                for (IVersionInfo info = this.m_pEnumVersionInfo.Next(); info != null; info = this.m_pEnumVersionInfo.Next())
                {
                    this.cboVersions.Properties.Items.Add(info.VersionName);
                    this.m_pList.Add(info.Description);
                }
                if (this.cboVersions.Properties.Items.Count > 0)
                {
                    this.cboVersions.SelectedIndex = 0;
                }
            }
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
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xb9, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择该连接将要访问的数据库版本";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x34);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "版本:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x58);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "说明:";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x48, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x98, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x40, 0x58);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtDescription.Properties.Appearance.Options.UseBackColor = true;
            this.txtDescription.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new Size(0xa8, 0x13);
            this.txtDescription.TabIndex = 6;
            this.cboVersions.EditValue = "";
            this.cboVersions.Location = new Point(0x40, 0x30);
            this.cboVersions.Name = "cboVersions";
            this.cboVersions.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboVersions.Size = new Size(160, 0x15);
            this.cboVersions.TabIndex = 7;
            this.cboVersions.SelectedIndexChanged += new EventHandler(this.cboVersions_SelectedIndexChanged);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(250, 0x9d);
            base.Controls.Add(this.cboVersions);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
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

        public IEnumVersionInfo EnumVersionInfo
        {
            set
            {
                this.m_pEnumVersionInfo = value;
            }
        }

        public string VersionName
        {
            get
            {
                return this.m_VersionName;
            }
        }
    }
}

