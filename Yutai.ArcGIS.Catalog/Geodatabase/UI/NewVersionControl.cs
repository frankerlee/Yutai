using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class NewVersionControl : UserControl
    {
        private Container container_0 = null;

        public NewVersionControl()
        {
            this.InitializeComponent();
        }

        public void CreateVersion()
        {
            if (this.iarray_0 != null)
            {
                IVersion version = this.iarray_0.get_Element(this.comboParentVersion.SelectedIndex) as IVersion;
                try
                {
                    IVersion version2 = version.CreateVersion(this.txtName.Text);
                    version2.Description = this.txtDescription.Text;
                    version2.Access = (esriVersionAccess) this.radioAccessType.SelectedIndex;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void method_0()
        {
            if (this.iarray_0 != null)
            {
                this.comboParentVersion.Properties.Items.Clear();
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    IVersion version = this.iarray_0.get_Element(i) as IVersion;
                    this.comboParentVersion.Properties.Items.Add(version.VersionName);
                }
                this.comboParentVersion.SelectedIndex = 0;
            }
        }

        private void NewVersionControl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public IArray ParentVersions
        {
            set { this.iarray_0 = value; }
        }
    }
}