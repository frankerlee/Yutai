using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class ServerLogPropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public ServerLogPropertyPage()
        {
            this.InitializeComponent();
        }

        private void ServerLogPropertyPage_Load(object sender, EventArgs e)
        {
            IServerLog serverLog = (this.AGSServerConnectionAdmin.ServerObjectAdmin as IServerObjectAdmin8).ServerLog;
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin { get; set; }
    }
}