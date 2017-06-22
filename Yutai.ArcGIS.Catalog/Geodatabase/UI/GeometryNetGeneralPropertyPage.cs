using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class GeometryNetGeneralPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;

        public GeometryNetGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

 private void GeometryNetGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            ListViewItem item;
            this.textEdit1.Text = (this.igeometricNetwork_0 as IDataset).Name;
            IEnumFeatureClass class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
            class2.Reset();
            IFeatureClass class3 = class2.Next();
            string[] items = new string[2];
            while (class3 != null)
            {
                items[0] = class3.AliasName;
                items[1] = "简单连接点";
                item = new ListViewItem(items);
                this.listView1.Items.Add(item);
                class3 = class2.Next();
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                items[0] = class3.AliasName;
                items[1] = "复杂连接点";
                item = new ListViewItem(items);
                this.listView1.Items.Add(item);
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                items[0] = class3.AliasName;
                items[1] = "简单边";
                item = new ListViewItem(items);
                this.listView1.Items.Add(item);
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                items[0] = class3.AliasName;
                items[1] = "复杂边";
                item = new ListViewItem(items);
                this.listView1.Items.Add(item);
            }
        }

 public IGeometricNetwork GeometricNetwork
        {
            set
            {
                this.igeometricNetwork_0 = value;
            }
        }
    }
}

