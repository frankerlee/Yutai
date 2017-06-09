using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class WeightPropertyPage : UserControl
    {
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;
        private ListView listView1;

        public WeightPropertyPage()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.Location = new Point(8, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x180, 0x90);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "权重名称";
            this.columnHeader_0.Width = 0xe9;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 0x84;
            base.Controls.Add(this.listView1);
            base.Name = "WeightPropertyPage";
            base.Size = new Size(0x1a0, 0x100);
            base.Load += new EventHandler(this.WeightPropertyPage_Load);
            base.ResumeLayout(false);
        }

        private string method_0(esriWeightType esriWeightType_0)
        {
            switch (esriWeightType_0)
            {
                case esriWeightType.esriWTNull:
                    return "空";

                case esriWeightType.esriWTBitGate:
                    return "位门";

                case esriWeightType.esriWTInteger:
                    return "长整型";

                case esriWeightType.esriWTSingle:
                    return "单精度";

                case esriWeightType.esriWTDouble:
                    return "双精度";

                case esriWeightType.esriWTBoolean:
                    return "布尔型";
            }
            return "";
        }

        private void WeightPropertyPage_Load(object sender, EventArgs e)
        {
            INetwork network = this.igeometricNetwork_0.Network;
            string[] items = new string[2];
            for (int i = 0; i < (network as INetSchema).WeightCount; i++)
            {
                INetWeight weight = (network as INetSchema).get_Weight(i);
                items[0] = weight.WeightName;
                items[1] = this.method_0(weight.WeightType);
                ListViewItem item = new ListViewItem(items);
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

