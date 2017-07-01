using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class CoverageTicPropertyPage : UserControl
    {
        private Container container_0 = null;
        private DataTable dataTable_0 = new DataTable();
        private ICoverageName icoverageName_0 = null;
        private IDatasetName idatasetName_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 0;

        public CoverageTicPropertyPage()
        {
            this.InitializeComponent();
            this.dataGrid1.SetDataBinding(this.dataTable_0, "");
        }

        public void Apply()
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = this.dataTable_0.NewRow();
            int count = this.dataTable_0.Rows.Count;
            int num2 = 1;
            if (count > 0)
            {
                num2 = ((int) this.dataTable_0.Rows[count - 1][0]) + 1;
            }
            row[0] = num2;
            row[1] = 0;
            row[2] = 0;
            this.dataTable_0.Rows.Add(row);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGrid1.CurrentRowIndex >= 0)
            {
                this.dataTable_0.Rows.RemoveAt(this.dataGrid1.CurrentRowIndex);
            }
        }

        private void CoverageTicPropertyPage_Load(object sender, EventArgs e)
        {
            DataColumn column = new DataColumn("ID")
            {
                DataType = System.Type.GetType("System.Int32"),
                Caption = "ID"
            };
            this.dataTable_0.Columns.Add(column);
            column = new DataColumn("X")
            {
                DataType = System.Type.GetType("System.Double"),
                Caption = "X"
            };
            this.dataTable_0.Columns.Add(column);
            column = new DataColumn("Y")
            {
                DataType = System.Type.GetType("System.Double"),
                Caption = "Y"
            };
            this.dataTable_0.Columns.Add(column);
            IEnumDatasetName featureClassNames = (this.icoverageName_0 as IFeatureDatasetName2).FeatureClassNames;
            featureClassNames.Reset();
            for (IDatasetName name2 = featureClassNames.Next(); name2 != null; name2 = featureClassNames.Next())
            {
                if ((name2 as ICoverageFeatureClassName).FeatureClassType == esriCoverageFeatureClassType.esriCFCTTic)
                {
                    this.idatasetName_0 = name2;
                    break;
                }
            }
            if (this.idatasetName_0 != null)
            {
                IFeatureClass class2 = (this.idatasetName_0 as IName).Open() as IFeatureClass;
                IEnvelope extent = (class2 as IGeoDataset).Extent;
                if (extent.IsEmpty)
                {
                    this.txtMinX.Text = "-1";
                    this.txtMinY.Text = "-1";
                    this.txtMaxX.Text = "-1";
                    this.txtMaxY.Text = "-1";
                }
                else
                {
                    this.txtMinX.Text = extent.XMin.ToString();
                    this.txtMinY.Text = extent.YMin.ToString();
                    this.txtMaxX.Text = extent.XMax.ToString();
                    this.txtMaxY.Text = extent.YMax.ToString();
                }
                IFeatureCursor cursor = class2.Search(null, false);
                int index = class2.Fields.FindField("IDTIC");
                int num3 = class2.Fields.FindField("XTIC");
                int num4 = class2.Fields.FindField("YTIC");
                for (IFeature feature = cursor.NextFeature(); feature != null; feature = cursor.NextFeature())
                {
                    DataRow row = this.dataTable_0.NewRow();
                    row[0] = feature.get_Value(index);
                    row[1] = feature.get_Value(num3);
                    row[2] = feature.get_Value(num4);
                    this.dataTable_0.Rows.Add(row);
                }
                class2 = null;
                this.int_0 = this.dataTable_0.Rows.Count;
            }
        }

        public ICoverageName CoverageName
        {
            set { this.icoverageName_0 = value; }
        }
    }
}