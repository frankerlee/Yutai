namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.GISClient;
    using ESRI.ArcGIS.Server;
    
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ServerDirectoryPropertyPage : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private Button btnEdit;
        private ComboBox cboDirectoryType;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
       
        private IContainer icontainer_0 = null;
        private Label label4;
        private ListView lstDir;

        public ServerDirectoryPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboDirectoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumServerDirectory serverDirectories = this.AGSServerConnectionAdmin.ServerObjectAdmin.GetServerDirectories();
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
                else if ((this.cboDirectoryType.SelectedIndex == 3) && ((directory2 as IServerDirectory2).Type == esriServerDirectoryType.esriSDTypeSystem))
                {
                    this.lstDir.Items.Add(this.method_0(directory2));
                }
                directory2 = serverDirectories.Next();
            }
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
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnAdd = new Button();
            this.lstDir = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.cboDirectoryType = new ComboBox();
            this.label4 = new Label();
 
            base.SuspendLayout();
            this.btnEdit.Location = new Point(0x157, 0x84);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x38, 0x18);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "编辑...";
            this.btnDelete.Location = new Point(0x157, 100);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x38, 0x18);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除";
            this.btnAdd.Location = new Point(0x157, 0x44);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "添加...";
            this.lstDir.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.lstDir.Location = new Point(0x12, 0x44);
            this.lstDir.Name = "lstDir";
            this.lstDir.Size = new Size(0x13f, 0x70);
            this.lstDir.TabIndex = 10;
            this.lstDir.UseCompatibleStateImageBehavior = false;
            this.lstDir.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 0x73;
            this.columnHeader_1.Text = "位置";
            this.columnHeader_1.Width = 160;
            this.cboDirectoryType.Text = "缓存目录";
            this.cboDirectoryType.Location = new Point(0x51, 40);
            this.cboDirectoryType.Name = "cboDirectoryType";
       
            this.cboDirectoryType.Items.AddRange(new object[] { "缓存目录", "作业目录", "输出目录", "系统目录" });
            this.cboDirectoryType.Size = new Size(0x100, 0x15);
            this.cboDirectoryType.TabIndex = 15;
            this.cboDirectoryType.SelectedIndexChanged += new EventHandler(this.cboDirectoryType_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x2b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "目录类型:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cboDirectoryType);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.lstDir);
            base.Name = "ServerDirectoryPropertyPage";
            base.Size = new Size(0x19f, 0xe1);
            base.Load += new EventHandler(this.ServerDirectoryPropertyPage_Load);
      
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private ListViewItem method_0(IServerDirectory iserverDirectory_0)
        {
            string[] items = new string[2];
            string[] strArray2 = iserverDirectory_0.URL.Split(new char[] { '/' });
            items[0] = strArray2[strArray2.Length - 1];
            items[1] = iserverDirectory_0.Path;
            return new ListViewItem(items) { Tag = iserverDirectory_0 };
        }

        private void ServerDirectoryPropertyPage_Load(object sender, EventArgs e)
        {
            this.cboDirectoryType_SelectedIndexChanged(this, e);
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            get; set;
        }
    }
}

