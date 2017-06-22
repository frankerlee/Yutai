using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmSelectReconcileVersion : Form
    {
        private Container container_0 = null;
        public static bool HasConflict;

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

 private void frmSelectReconcileVersion_Load(object sender, EventArgs e)
        {
            this.method_0();
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

