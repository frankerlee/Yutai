using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class AGSHostsPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;

        public AGSHostsPropertyPage()
        {
            this.InitializeComponent();
        }

        private void AGSHostsPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmHost host = new frmHost();
            if (host.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] items = new string[] { host.HostName, host.Description };
                    IServerObjectAdmin serverObjectAdmin = this.iagsserverConnectionAdmin_0.ServerObjectAdmin;
                    IServerMachine machine = serverObjectAdmin.CreateMachine();
                    machine.Name = items[0];
                    machine.Description = items[1];
                    serverObjectAdmin.AddMachine(machine);
                    ListViewItem item = new ListViewItem(items);
                    this.Hostlist.Items.Add(item);
                }
                catch (COMException exception)
                {
                    if (exception.ErrorCode == -2147467259)
                    {
                        MessageBox.Show("服务器不存在!");
                        Logger.Current.Error("",exception, "");
                    }
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.ToString());
                    Logger.Current.Error("",exception2, "");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmHost host = new frmHost();
            ListViewItem item = this.Hostlist.SelectedItems[0];
            host.HostName = item.Text;
            host.Description = item.SubItems[1].Text;
            if (host.ShowDialog() == DialogResult.OK)
            {
                item.SubItems[1].Text = host.Description;
            }
        }

 private void Hostlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Hostlist.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
                this.btnEdit.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnEdit.Enabled = false;
            }
        }

 private void method_0()
        {
            IEnumServerMachine machines = this.iagsserverConnectionAdmin_0.ServerObjectAdmin.GetMachines();
            machines.Reset();
            IServerMachine machine2 = machines.Next();
            string[] items = new string[2];
            while (machine2 != null)
            {
                items[0] = machine2.Name;
                items[1] = machine2.Description;
                ListViewItem item = new ListViewItem(items);
                this.Hostlist.Items.Add(item);
                machine2 = machines.Next();
            }
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }
    }
}

