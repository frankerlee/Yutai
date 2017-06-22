using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class CatoTemplateApplySelect : UserControl
    {
        private IContainer icontainer_0 = null;

        public CatoTemplateApplySelect()
        {
            this.InitializeComponent();
        }

        private void CatoTemplateApplySelect_Load(object sender, EventArgs e)
        {
            CartoTemplateTableStruct struct2 = new CartoTemplateTableStruct();
            ITable table = AppConfigInfo.OpenTable(struct2.TableName);
            if (table != null)
            {
                ICursor o = table.Search(null, false);
                IRow row = o.NextRow();
                string[] items = new string[2];
                while (row != null)
                {
                    CartoTemplateData data = new CartoTemplateData(row);
                    items[0] = data.Name;
                    items[1] = data.Description;
                    ListViewItem item = new ListViewItem(items) {
                        Tag = data
                    };
                    this.listView1.Items.Add(item);
                    row = o.NextRow();
                }
                ComReleaser.ReleaseCOMObject(o);
                o = null;
            }
        }

 internal CartoTemplateData CartoTemplateData
        {
            get
            {
                if (this.listView1.SelectedItems.Count == 0)
                {
                    return null;
                }
                return (this.listView1.SelectedItems[0].Tag as CartoTemplateData);
            }
        }
    }
}

