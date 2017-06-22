using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class IndexSetCtrl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IFeatureClass ifeatureClass_0 = null;

        public IndexSetCtrl()
        {
            this.InitializeComponent();
        }

        private void btnAddGridIndex_Click(object sender, EventArgs e)
        {
        }

        private void btnDeleteGridIndex_Click(object sender, EventArgs e)
        {
        }

 private void IndexSetCtrl_Load(object sender, EventArgs e)
        {
            int num;
            if ((this.ifeatureClass_0 as IDataset).Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                this.groupBox2.Visible = true;
            }
            else
            {
                this.groupBox2.Visible = false;
            }
            IIndexes indexes = this.ifeatureClass_0.Indexes;
            for (num = 0; num < indexes.IndexCount; num++)
            {
                IIndex index = indexes.get_Index(num);
                if (index.Fields.get_Field(0).Type != esriFieldType.esriFieldTypeGeometry)
                {
                    this.listBoxIndexName.Items.Add(new IndexWrap(index));
                }
            }
            int num2 = this.ifeatureClass_0.FindField(this.ifeatureClass_0.ShapeFieldName);
            IGeometryDef geometryDef = this.ifeatureClass_0.Fields.get_Field(num2).GeometryDef;
            double[] numArray2 = new double[3];
            for (num = 0; num < geometryDef.GridCount; num++)
            {
                numArray2[num] = geometryDef.get_GridSize(num);
            }
            this.lblGridSize1.Text = "Grid 1:" + numArray2[0].ToString("0.##");
            this.lblGridSize2.Text = "Grid 2:" + numArray2[1].ToString("0.##");
            this.lblGridSize3.Text = "Grid 3:" + numArray2[2].ToString("0.##");
            this.btnAddGridIndex.Enabled = geometryDef.GridCount == 0;
        }

 internal partial class IndexWrap
        {
            private IIndex iindex_0 = null;

            internal IndexWrap(IIndex iindex_1)
            {
                this.iindex_0 = iindex_1;
            }

            public override string ToString()
            {
                return this.iindex_0.Name;
            }

            public IIndex Index
            {
                get
                {
                    return this.iindex_0;
                }
            }
        }
    }
}

