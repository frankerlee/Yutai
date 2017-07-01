using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class ServerDirectoryPropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public ServerDirectoryPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboDirectoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumServerDirectory serverDirectories =
                this.AGSServerConnectionAdmin.ServerObjectAdmin.GetServerDirectories();
            serverDirectories.Reset();
            IServerDirectory directory2 = serverDirectories.Next();
            this.lstDir.Items.Clear();
            while (directory2 != null)
            {
                if (this.cboDirectoryType.SelectedIndex == 0)
                {
                    if ((directory2 as IServerDirectory2).Type == esriServerDirectoryType.esriSDTypeCache)
                    {
                        this.lstDir.Items.Add(this.method_0(directory2));
                    }
                }
                else if (this.cboDirectoryType.SelectedIndex == 1)
                {
                    if ((directory2 as IServerDirectory2).Type == esriServerDirectoryType.esriSDTypeJobs)
                    {
                        this.lstDir.Items.Add(this.method_0(directory2));
                    }
                }
                else if (this.cboDirectoryType.SelectedIndex == 2)
                {
                    if ((directory2 as IServerDirectory2).Type == esriServerDirectoryType.esriSDTypeOutput)
                    {
                        this.lstDir.Items.Add(this.method_0(directory2));
                    }
                }
                else if ((this.cboDirectoryType.SelectedIndex == 3) &&
                         ((directory2 as IServerDirectory2).Type == esriServerDirectoryType.esriSDTypeSystem))
                {
                    this.lstDir.Items.Add(this.method_0(directory2));
                }
                directory2 = serverDirectories.Next();
            }
        }

        private ListViewItem method_0(IServerDirectory iserverDirectory_0)
        {
            string[] items = new string[2];
            string[] strArray2 = iserverDirectory_0.URL.Split(new char[] {'/'});
            items[0] = strArray2[strArray2.Length - 1];
            items[1] = iserverDirectory_0.Path;
            return new ListViewItem(items) {Tag = iserverDirectory_0};
        }

        private void ServerDirectoryPropertyPage_Load(object sender, EventArgs e)
        {
            this.cboDirectoryType_SelectedIndexChanged(this, e);
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin { get; set; }
    }
}