using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class VersionPropertyCtrl : UserControl
    {
        private Container container_0 = null;

        public VersionPropertyCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                this.iversion_0.Description = this.txtDescription.Text;
                this.iversion_0.Access = (esriVersionAccess) this.radioAccessType.SelectedIndex;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void method_0()
        {
            string[] strArray = this.iversion_0.VersionName.Split(new char[] {'.'});
            this.txtName.Text = strArray[1];
            this.txtOwner.Text = strArray[0];
            IVersionInfo parent = null;
            try
            {
                parent = this.iversion_0.VersionInfo.Parent;
            }
            catch
            {
            }
            if (parent != null)
            {
                this.txtParentVersion.Text = parent.VersionName;
            }
            else
            {
                this.txtParentVersion.Text = "";
            }
            this.lblCreatTime.Text = this.iversion_0.VersionInfo.Created.ToString();
            this.lblLastModifyTime.Text = this.iversion_0.VersionInfo.Modified.ToString();
            this.txtDescription.Text = this.iversion_0.Description;
            this.radioAccessType.SelectedIndex = (int) this.iversion_0.Access;
        }

        private void VersionPropertyCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public IVersion Version
        {
            set { this.iversion_0 = value; }
        }
    }
}