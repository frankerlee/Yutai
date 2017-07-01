using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class ServerHostPropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public ServerHostPropertyPage()
        {
            this.InitializeComponent();
        }

        private void ServerHostPropertyPage_Load(object sender, EventArgs e)
        {
            IEnumServerObjectConfiguration configurations =
                this.AGSServerConnectionAdmin.ServerObjectAdmin.GetConfigurations();
            configurations.Reset();
            for (IServerObjectConfiguration configuration2 = configurations.Next();
                configuration2 != null;
                configuration2 = configurations.Next())
            {
                object obj2;
                object obj3;
                configuration2.Properties.GetAllProperties(out obj2, out obj3);
            }
            IEnumServerMachine machines = this.AGSServerConnectionAdmin.ServerObjectAdmin.GetMachines();
            machines.Reset();
            IServerMachine machine2 = machines.Next();
            string[] items = new string[3];
            while (machine2 != null)
            {
                items[0] = (machine2 as IServerMachine3).Name;
                items[1] = (machine2 as IServerMachine3).AdminURL;
                items[2] = "已启动";
                ListViewItem item = new ListViewItem(items)
                {
                    Tag = machine2
                };
                this.lstDir.Items.Add(item);
                machine2 = machines.Next();
            }
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin { get; set; }
    }
}