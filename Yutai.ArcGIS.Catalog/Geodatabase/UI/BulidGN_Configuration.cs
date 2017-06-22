using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_Configuration : UserControl
    {
        private Container container_0 = null;

        public BulidGN_Configuration()
        {
            this.InitializeComponent();
        }

        private void BulidGN_Configuration_Load(object sender, EventArgs e)
        {
            if (BulidGeometryNetworkHelper.BulidGNHelper.UseDefaultConfigKey)
            {
                this.radioGroup1.SelectedIndex = 0;
                BulidGeometryNetworkHelper.BulidGNHelper.ConfigurationKeyword = "";
            }
            else
            {
                this.radioGroup1.SelectedIndex = 1;
                BulidGeometryNetworkHelper.BulidGNHelper.ConfigurationKeyword = this.comboBoxEdit.Text;
            }
            IWorkspaceConfiguration workspace = BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace as IWorkspaceConfiguration;
            if (workspace != null)
            {
                IEnumConfigurationKeyword configurationKeywords = workspace.ConfigurationKeywords;
                configurationKeywords.Reset();
                for (IConfigurationKeyword keyword2 = configurationKeywords.Next(); keyword2 != null; keyword2 = configurationKeywords.Next())
                {
                    if (keyword2.KeywordType == esriConfigurationKeywordType.esriConfigurationKeywordNetwork)
                    {
                        this.comboBoxEdit.Properties.Items.Add(keyword2.Name);
                    }
                    this.comboBoxEdit.SelectedIndex = 0;
                }
            }
        }

        private void comboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 1)
            {
                BulidGeometryNetworkHelper.BulidGNHelper.ConfigurationKeyword = this.comboBoxEdit.Text;
            }
        }

 private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                BulidGeometryNetworkHelper.BulidGNHelper.UseDefaultConfigKey = true;
            }
            else
            {
                BulidGeometryNetworkHelper.BulidGNHelper.UseDefaultConfigKey = false;
            }
        }
    }
}

