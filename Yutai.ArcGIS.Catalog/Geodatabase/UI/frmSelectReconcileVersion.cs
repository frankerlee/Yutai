using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmSelectReconcileVersion : Form
    {
        private SimpleButton btnCancle;
        private SimpleButton btnOK;
        private Container container_0 = null;
        public static bool HasConflict;
        private IVersion iversion_0;
        private ListBoxControl VersionListBox;

        static frmSelectReconcileVersion()
        {
            old_acctor_mc();
        }

        public frmSelectReconcileVersion()
        {
            this.InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                HasConflict = (this.iversion_0 as IVersionEdit).Reconcile(this.VersionListBox.SelectedItem.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

        private void frmSelectReconcileVersion_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.VersionListBox = new ListBoxControl();
            this.btnOK = new SimpleButton();
            this.btnCancle = new SimpleButton();
            ((ISupportInitialize) this.VersionListBox).BeginInit();
            base.SuspendLayout();
            this.VersionListBox.ItemHeight = 15;
            this.VersionListBox.Location = new Point(8, 8);
            this.VersionListBox.Name = "VersionListBox";
            this.VersionListBox.Size = new Size(0xb8, 0x90);
            this.VersionListBox.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x40, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.DialogResult = DialogResult.Cancel;
            this.btnCancle.Location = new Point(0x88, 160);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x38, 0x18);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(200, 0xbd);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.VersionListBox);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectReconcileVersion";
            this.Text = "选择协调版本";
            base.Load += new EventHandler(this.frmSelectReconcileVersion_Load);
            ((ISupportInitialize) this.VersionListBox).EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            if ((this.iversion_0 != null) && this.iversion_0.HasParent())
            {
                this.VersionListBox.Items.Clear();
                IEnumVersionInfo ancestors = this.iversion_0.VersionInfo.Ancestors;
                ancestors.Reset();
                for (IVersionInfo info2 = ancestors.Next(); info2 != null; info2 = ancestors.Next())
                {
                    this.VersionListBox.Items.Add(info2.VersionName);
                }
                this.VersionListBox.SelectedIndex = 0;
            }
        }

        private static void old_acctor_mc()
        {
            HasConflict = false;
        }

        public IVersion Version
        {
            set
            {
                this.iversion_0 = value;
            }
        }
    }
}

