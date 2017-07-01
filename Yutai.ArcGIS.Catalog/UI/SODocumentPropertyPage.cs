using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class SODocumentPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "";

        public SODocumentPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            object obj2;
            object obj3;
            IPropertySet properties = this.iserverObjectConfiguration_0.Properties;
            properties.SetProperty("FilePath", this.txtDocument.Text);
            properties.SetProperty("SupportedImageReturnTypes", "MIME");
            properties.SetProperty("MaxRecordCount", "500");
            properties.SetProperty("MaxBufferCount", "100");
            properties.SetProperty("MaxImageWidth", "2048");
            properties.SetProperty("MaxImageHeight", "2048");
            this.iserverObjectConfiguration_0.Properties.GetAllProperties(out obj2, out obj3);
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
                this.string_0 = this.txtDocument.Text;
            }
        }

        private void method_0()
        {
        }

        private void SODocumentPropertyPage_Load(object sender, EventArgs e)
        {
        }

        private void txtDocument_EditValueChanged(object sender, EventArgs e)
        {
            this.string_0 = this.txtDocument.Text;
        }

        private void txtVirtualDirectory_EditValueChanged(object sender, EventArgs e)
        {
        }

        public string Docunment
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get { return this.iserverObjectConfiguration_0; }
            set { this.iserverObjectConfiguration_0 = value; }
        }
    }
}