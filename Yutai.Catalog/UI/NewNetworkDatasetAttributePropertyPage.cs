namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Geodatabase;
    
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewNetworkDatasetAttributePropertyPage : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private Button btnDeleteAll;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private ColumnHeader columnHeader_5;
        private IContainer icontainer_0 = null;
        private ListView listView1;

        public NewNetworkDatasetAttributePropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            NewNetworkDatasetHelper.NewNetworkDataset.NetworkDirections = null;
            bool flag = NewNetworkDatasetHelper.NewNetworkDataset.HasLineFeatureClass();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem item = this.listView1.Items[i];
                if (flag && ((item.Tag as INetworkAttribute).UsageType == esriNetworkAttributeUsageType.esriNAUTCost))
                {
                    NewNetworkDatasetHelper.NewNetworkDataset.CreateDirection((item.Tag as INetworkAttribute).Name);
                }
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewNetworkAttribute attribute = new frmNewNetworkAttribute();
            if (attribute.ShowDialog() == DialogResult.OK)
            {
                string[] items = new string[] { "", "", attribute.NetworkAttribute.Name, Common.GetUsageTypeDescriptor(attribute.NetworkAttribute.UsageType), Common.GetNetworkUnitTypeDescriptor(attribute.NetworkAttribute.Units), Common.GetDataTypeDescriptor(attribute.NetworkAttribute.DataType) };
                INetworkConstantEvaluator evaluator = new NetworkConstantEvaluator() as INetworkConstantEvaluator;
                IEvaluatedNetworkAttribute networkAttribute = attribute.NetworkAttribute as IEvaluatedNetworkAttribute;
                evaluator.ConstantValue = 0;
                networkAttribute.set_DefaultEvaluator(esriNetworkElementType.esriNETEdge, evaluator as INetworkEvaluator);
                networkAttribute.set_DefaultEvaluator(esriNetworkElementType.esriNETJunction, evaluator as INetworkEvaluator);
                networkAttribute.set_DefaultEvaluator(esriNetworkElementType.esriNETTurn, evaluator as INetworkEvaluator);
                ListViewItem item = new ListViewItem(items) {
                    Tag = attribute.NetworkAttribute
                };
                this.listView1.Items.Add(item);
                NewNetworkDatasetHelper.NewNetworkDataset.Attributes.Add(attribute.NetworkAttribute);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int index = this.listView1.SelectedIndices[i];
                this.listView1.Items.RemoveAt(index);
                NewNetworkDatasetHelper.NewNetworkDataset.Attributes.Remove(index);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                this.listView1.Items.RemoveAt(i);
                NewNetworkDatasetHelper.NewNetworkDataset.Attributes.Remove(i);
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
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.columnHeader_5 = new ColumnHeader();
            this.btnAdd = new Button();
            this.btnDelete = new Button();
            this.btnDeleteAll = new Button();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3, this.columnHeader_4, this.columnHeader_5 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(13, 0x10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x130, 0x94);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "!";
            this.columnHeader_0.Width = 0x20;
            this.columnHeader_1.Text = "";
            this.columnHeader_1.Width = 0x24;
            this.columnHeader_2.Text = "名称";
            this.columnHeader_2.Width = 0x2d;
            this.columnHeader_3.Text = "使用方式";
            this.columnHeader_4.Text = "单位";
            this.columnHeader_5.Text = "数据类型";
            this.btnAdd.Location = new Point(0x143, 0x10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x4b, 0x17);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(0x143, 0x2d);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new Point(0x143, 0x4b);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(0x4b, 0x17);
            this.btnDeleteAll.TabIndex = 3;
            this.btnDeleteAll.Text = "删除所有";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.listView1);
            base.Name = "NewNetworkDatasetAttributePropertyPage";
            base.Size = new Size(0x19d, 0xe9);
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
        }
    }
}

