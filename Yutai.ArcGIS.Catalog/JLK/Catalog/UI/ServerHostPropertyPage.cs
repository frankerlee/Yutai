namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.GISClient;
    using ESRI.ArcGIS.Server;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ServerHostPropertyPage : UserControl
    {
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnStart;
        private SimpleButton btnStop;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        [CompilerGenerated]
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0;
        private IContainer icontainer_0 = null;
        private ListView lstDir;

        public ServerHostPropertyPage()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.btnStart = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.lstDir = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.btnStop = new SimpleButton();
            this.columnHeader_2 = new ColumnHeader();
            base.SuspendLayout();
            this.btnStart.Location = new Point(0x151, 0x6c);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(0x38, 0x18);
            this.btnStart.TabIndex = 0x11;
            this.btnStart.Text = "启动";
            this.btnDelete.Location = new Point(0x151, 0x4c);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x38, 0x18);
            this.btnDelete.TabIndex = 0x10;
            this.btnDelete.Text = "删除";
            this.btnAdd.Location = new Point(0x151, 0x2c);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "添加...";
            this.lstDir.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.lstDir.Location = new Point(12, 0x2c);
            this.lstDir.Name = "lstDir";
            this.lstDir.Size = new Size(0x13f, 0x70);
            this.lstDir.TabIndex = 14;
            this.lstDir.UseCompatibleStateImageBehavior = false;
            this.lstDir.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 0x73;
            this.columnHeader_1.Text = "管理URL";
            this.columnHeader_1.Width = 160;
            this.btnStop.Location = new Point(0x151, 0x8a);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(0x38, 0x18);
            this.btnStop.TabIndex = 0x12;
            this.btnStop.Text = "停止";
            this.columnHeader_2.Text = "状态";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.lstDir);
            base.Name = "ServerHostPropertyPage";
            base.Size = new Size(0x18e, 0xf4);
            base.Load += new EventHandler(this.ServerHostPropertyPage_Load);
            base.ResumeLayout(false);
        }

        private void ServerHostPropertyPage_Load(object sender, EventArgs e)
        {
            IEnumServerObjectConfiguration configurations = this.AGSServerConnectionAdmin.ServerObjectAdmin.GetConfigurations();
            configurations.Reset();
            for (IServerObjectConfiguration configuration2 = configurations.Next(); configuration2 != null; configuration2 = configurations.Next())
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
                ListViewItem item = new ListViewItem(items) {
                    Tag = machine2
                };
                this.lstDir.Items.Add(item);
                machine2 = machines.Next();
            }
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            [CompilerGenerated]
            get
            {
                return this.iagsserverConnectionAdmin_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }
    }
}

