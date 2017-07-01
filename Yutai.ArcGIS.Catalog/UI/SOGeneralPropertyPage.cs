using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class SOGeneralPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IServerObject iserverObject_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "";
        private string string_1 = "";

        public SOGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.iserverObjectConfiguration_0 == null)
            {
                this.iserverObjectConfiguration_0 =
                    (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.CreateConfiguration();
            }
            this.iserverObjectConfiguration_0.Name = this.txtSOName.Text;
            this.iserverObjectConfiguration_0.TypeName = this.cboSOType.Text;
            this.iserverObjectConfiguration_0.Description = this.txtDescription.Text;
            this.iserverObjectConfiguration_0.StartupType = (esriStartupType) this.cboStartupType.SelectedIndex;
        }

        public IServerObjectConfiguration CreateServerObjectConfig()
        {
            if (this.iserverObjectConfiguration_0 == null)
            {
                this.iserverObjectConfiguration_0 =
                    (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.CreateConfiguration();
            }
            this.iserverObjectConfiguration_0.Name = this.txtSOName.Text;
            this.iserverObjectConfiguration_0.TypeName = this.cboSOType.Text;
            this.iserverObjectConfiguration_0.Description = this.txtDescription.Text;
            this.iserverObjectConfiguration_0.StartupType = (esriStartupType) this.cboStartupType.SelectedIndex;
            return this.iserverObjectConfiguration_0;
        }

        private void SOGeneralPropertyPage_Load(object sender, EventArgs e)
        {
        }

        private void txtSOName_EditValueChanged(object sender, EventArgs e)
        {
            this.string_0 = this.txtSOName.Text;
        }

        public IAGSServerConnection AGSServerConnection
        {
            set { this.iagsserverConnection_0 = value; }
        }

        public IServerObject ServerObject
        {
            get { return this.iserverObject_0; }
            set { this.iserverObject_0 = value; }
        }

        public string SOName
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public string SOType
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }
    }
}