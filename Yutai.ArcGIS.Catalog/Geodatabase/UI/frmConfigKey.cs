using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmConfigKey : Form
    {
        private Container container_0 = null;

        public frmConfigKey()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void frmConfigKey_Load(object sender, EventArgs e)
        {
            this.method_0();
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
            set { this.iworkspaceConfiguration_0 = value; }
        }
    }
}