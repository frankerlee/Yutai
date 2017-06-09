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
    internal class AGSHostsPropertyPage : UserControl
    {
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private ListView Hostlist;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private Label label1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.Hostlist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnEdit = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xb2, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "可以运行server objects的主机";
            this.Hostlist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.Hostlist.Location = new Point(0x10, 0x30);
            this.Hostlist.Name = "Hostlist";
            this.Hostlist.Size = new Size(0x100, 0x70);
            this.Hostlist.TabIndex = 1;
            this.Hostlist.View = View.Details;
            this.Hostlist.SelectedIndexChanged += new EventHandler(this.Hostlist_SelectedIndexChanged);
            this.columnHeader_0.Text = "主机";
            this.columnHeader_0.Width = 0x73;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 0x80;
            this.btnAdd.Location = new Point(280, 0x30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加...";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(280, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x38, 0x18);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnEdit.Location = new Point(280, 0x70);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x38, 0x18);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "编辑...";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.Hostlist);
            base.Controls.Add(this.label1);
            base.Name = "AGSHostsPropertyPage";
            base.Size = new Size(0x160, 0x100);
            base.Load += new EventHandler(this.AGSHostsPropertyPage_Load);
            base.ResumeLayout(false);
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

