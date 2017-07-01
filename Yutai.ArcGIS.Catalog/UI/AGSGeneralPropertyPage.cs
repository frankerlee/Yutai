using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class AGSGeneralPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;

        public AGSGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        private void AGSGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public void Apply()
        {
            IPropertySet properties = this.iagsserverConnectionAdmin_0.ServerObjectAdmin.Properties;
            try
            {
                int num = int.Parse(this.txtTimeOut.Text.Trim());
                properties.SetProperty("ConfigurationStartTimeout", num);
            }
            catch
            {
            }
            properties.SetProperty("LogPath", this.txtLogPath.Text);
            try
            {
                int num2 = int.Parse(this.txtLogSize.Text);
                properties.SetProperty("LogSize", num2);
            }
            catch
            {
            }
            properties.SetProperty("LogLevel", this.cboLogLevel.SelectedIndex);
        }

        private void method_0()
        {
            object obj2;
            object obj3;
            IPropertySet properties = this.iagsserverConnectionAdmin_0.ServerObjectAdmin.Properties;
            properties.GetAllProperties(out obj2, out obj3);
            this.txtTimeOut.Text = properties.GetProperty("ConfigurationStartTimeout").ToString();
            this.txtLogPath.Text = properties.GetProperty("LogPath").ToString();
            this.txtLogSize.Text = properties.GetProperty("LogSize").ToString();
            this.cboLogLevel.SelectedIndex = (int) properties.GetProperty("LogLevel");
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            set { this.iagsserverConnectionAdmin_0 = value; }
        }
    }
}