using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class frmConfigKey : Form
    {
        private SimpleButton btnOK;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Container container_0 = null;
        private IWorkspaceConfiguration iworkspaceConfiguration_0;
        private Label label1;
        private ListView listView1;

        public frmConfigKey()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmConfigKey_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigKey));
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label1 = new Label();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(0x10, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x158, 200);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 0x83;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 0x55;
            this.columnHeader_2.Text = "描述";
            this.columnHeader_2.Width = 0x7b;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "配置关键字";
            this.btnOK.Location = new Point(0xa8, 0xf8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x188, 0x11a);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmConfigKey";
            this.Text = "配置关键字";
            base.Load += new EventHandler(this.frmConfigKey_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            if (this.iworkspaceConfiguration_0 != null)
            {
                IEnumConfigurationKeyword configurationKeywords = this.iworkspaceConfiguration_0.ConfigurationKeywords;
                configurationKeywords.Reset();
                IConfigurationKeyword keyword2 = configurationKeywords.Next();
                string[] items = new string[3];
                while (keyword2 != null)
                {
                    items[0] = keyword2.Name;
                    items[1] = this.method_1(keyword2.KeywordType);
                    items[2] = keyword2.Description;
                    ListViewItem item = new ListViewItem(items);
                    this.listView1.Items.Add(item);
                    keyword2 = configurationKeywords.Next();
                }
            }
        }

        private string method_1(esriConfigurationKeywordType esriConfigurationKeywordType_0)
        {
            switch (esriConfigurationKeywordType_0)
            {
                case esriConfigurationKeywordType.esriConfigurationKeywordGeneral:
                    return "常规";

                case esriConfigurationKeywordType.esriConfigurationKeywordNetwork:
                    return "网络";

                case esriConfigurationKeywordType.esriConfigurationKeywordTopology:
                    return "拓扑";
            }
            return "";
        }

        public IWorkspaceConfiguration Configuration
        {
            set
            {
                this.iworkspaceConfiguration_0 = value;
            }
        }
    }
}

