using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class ServerObjectDocumentPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "Started";
        private string string_1 = "";

        public ServerObjectDocumentPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                this.iserverObjectConfiguration_0.Properties.SetProperty("FilePath", this.txtDocument.Text);
                this.bool_0 = false;
            }
        }

        private void btnOpenDocment_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "地图文档 (*.mxd;*.pmf)|*.mxd;*.pmf"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtDocument.Text = dialog.FileName;
                this.string_1 = this.txtDocument.Text;
                this.bool_0 = true;
            }
        }

        private void method_0()
        {
            object obj2;
            object obj3;
            IPropertySet properties = this.iserverObjectConfiguration_0.Properties;
            properties.GetAllProperties(out obj2, out obj3);
            this.txtDocument.Text = properties.GetProperty("FilePath").ToString();
            if (this.string_0 == "Stoped")
            {
                this.txtDocument.Properties.ReadOnly = false;
                this.btnOpenDocment.Enabled = true;
            }
            else
            {
                this.txtDocument.Properties.ReadOnly = true;
                this.btnOpenDocment.Enabled = false;
            }
        }

        private void ServerObjectDocumentPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_1 = true;
        }

        private void txtDocument_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.string_1 = this.txtDocument.Text;
                this.bool_0 = true;
            }
        }

        private void txtVirtualDirectory_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IAGSServerConnectionAdmin AGSConnectionAdmin
        {
            set { this.iagsserverConnectionAdmin_0 = value; }
        }

        public string Docunment
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get { return this.iserverObjectConfiguration_0; }
            set { this.iserverObjectConfiguration_0 = value; }
        }

        public string Status
        {
            set { this.string_0 = value; }
        }
    }
}