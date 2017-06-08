namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GeometryNetGeneralPropertyPage : UserControl
    {
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;
        private Label label1;
        private ListView listView1;
        private TextEdit textEdit1;

        public GeometryNetGeneralPropertyPage()
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(0x38, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(0xb8, 0x17);
            this.textEdit1.TabIndex = 1;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.Location = new Point(0x10, 0x30);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(280, 0xb0);
            this.listView1.TabIndex = 2;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "要素类名称";
            this.columnHeader_0.Width = 0x7f;
            this.columnHeader_1.Text = "规则";
            this.columnHeader_1.Width = 0x79;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.Name = "GeometryNetGeneralPropertyPage";
            base.Size = new Size(320, 0x100);
            base.Load += new EventHandler(this.GeometryNetGeneralPropertyPage_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
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

