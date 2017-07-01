using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class CoverageGeneralPropertyPage : UserControl
    {
        private Container container_0 = null;
        private ICoverageName icoverageName_0 = null;
        private string[] string_0 = new string[] {"Not Applicable", "Preliminary", "Exists", "Unknown"};

        public CoverageGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnBulid_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedIndices.Count > 0)
                {
                    ICoverage coverage = (this.icoverageName_0 as IName).Open() as ICoverage;
                    ListViewItem item = this.listView1.SelectedItems[0];
                    ICoverageFeatureClassName tag = item.Tag as ICoverageFeatureClassName;
                    coverage.Build(tag.FeatureClassType, "");
                    coverage = null;
                    MessageBox.Show("Bulid成功!");
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
                MessageBox.Show("Bulid失败!");
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedIndices.Count > 0)
                {
                    ICoverage coverage = (this.icoverageName_0 as IName).Open() as ICoverage;
                    double dangleTolerance = coverage.get_Tolerance(esriCoverageToleranceType.esriCTTFuzzy);
                    double fuzzyTolerance = coverage.get_Tolerance(esriCoverageToleranceType.esriCTTDangle);
                    ListViewItem item = this.listView1.SelectedItems[0];
                    ICoverageFeatureClassName tag = item.Tag as ICoverageFeatureClassName;
                    coverage.Clean(dangleTolerance, fuzzyTolerance, tag.FeatureClassType);
                    coverage = null;
                    MessageBox.Show("Clean成功!");
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
                MessageBox.Show("Clean失败!");
            }
        }

        private void CoverageGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.txtName.Text = (this.icoverageName_0 as IDatasetName).Name;
            IEnumDatasetName featureClassNames = (this.icoverageName_0 as IFeatureDatasetName2).FeatureClassNames;
            featureClassNames.Reset();
            IDatasetName name2 = featureClassNames.Next();
            string[] items = new string[3];
            while (name2 != null)
            {
                items[0] = name2.Name;
                items[1] = this.string_0[(int) (name2 as ICoverageFeatureClassName).Topology];
                if ((name2 as ICoverageFeatureClassName).HasFAT)
                {
                    items[2] = "True";
                }
                else
                {
                    items[2] = "False";
                }
                ListViewItem item = new ListViewItem(items)
                {
                    Tag = name2
                };
                this.listView1.Items.Add(item);
                name2 = featureClassNames.Next();
            }
        }

        public ICoverageName CoverageName
        {
            set { this.icoverageName_0 = value; }
        }
    }
}