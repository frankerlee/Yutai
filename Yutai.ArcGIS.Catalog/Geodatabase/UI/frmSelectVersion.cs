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
    public partial class frmSelectVersion : Form
    {
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();

        public frmSelectVersion()
        {
            this.InitializeComponent();
        }

        private void cboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.string_0 = this.cboVersions.Text;
            this.txtDescription.Text = this.ilist_0[this.cboVersions.SelectedIndex].ToString();
        }

        private void frmSelectVersion_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            if (this.ienumVersionInfo_0 != null)
            {
                this.ienumVersionInfo_0.Reset();
                this.ilist_0.Clear();
                for (IVersionInfo info = this.ienumVersionInfo_0.Next();
                    info != null;
                    info = this.ienumVersionInfo_0.Next())
                {
                    this.cboVersions.Properties.Items.Add(info.VersionName);
                    this.ilist_0.Add(info.Description);
                }
                if (this.cboVersions.Properties.Items.Count > 0)
                {
                    this.cboVersions.SelectedIndex = 0;
                }
            }
        }

        public IEnumVersionInfo EnumVersionInfo
        {
            set { this.ienumVersionInfo_0 = value; }
        }

        public string VersionName
        {
            get { return this.string_0; }
        }
    }
}