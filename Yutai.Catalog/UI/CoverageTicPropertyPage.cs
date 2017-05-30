namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
  
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class CoverageTicPropertyPage : UserControl
    {
        private Button btnAdd;
        private Button btnDelete;
        private Container container_0 = null;
        private DataGrid dataGrid1;
        private DataTable dataTable_0 = new DataTable();
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ICoverageName icoverageName_0 = null;
        private IDatasetName idatasetName_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button txtMaxX;
        private Button txtMaxY;
        private Button txtMinX;
        private Button txtMinY;

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
            DataColumn column = new DataColumn("ID") {
                DataType = Type.GetType("System.Int32"),
                Caption = "ID"
            };
            this.dataTable_0.Columns.Add(column);
            column = new DataColumn("X") {
                DataType = Type.GetType("System.Double"),
                Caption = "X"
            };
            this.dataTable_0.Columns.Add(column);
            column = new DataColumn("Y") {
                DataType = Type.GetType("System.Double"),
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
            this.groupBox1 = new GroupBox();
            this.dataGrid1 = new DataGrid();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtMinX = new Button();
            this.txtMinY = new Button();
            this.txtMaxX = new Button();
            this.txtMaxY = new Button();
            this.btnAdd = new Button();
            this.btnDelete = new Button();
            this.groupBox1.SuspendLayout();
            this.dataGrid1.BeginInit();
            this.groupBox2.SuspendLayout();
         
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.dataGrid1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tic点";
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0x10, 0x18);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new Size(0x110, 0x58);
            this.dataGrid1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.txtMaxY);
            this.groupBox2.Controls.Add(this.txtMaxX);
            this.groupBox2.Controls.Add(this.txtMinY);
            this.groupBox2.Controls.Add(this.txtMinX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 0x90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 0x90);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "范围";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小X坐标:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "最小Y坐标:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x42, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "最大Y坐标:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x42, 0x11);
            this.label4.TabIndex = 2;
            this.label4.Text = "最大X坐标:";
            this.txtMinX.Text="";
            this.txtMinX.Location = new System.Drawing.Point(0x58, 0x12);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Enabled = true;
            this.txtMinX.Size = new Size(200, 0x17);
            this.txtMinX.TabIndex = 4;
            this.txtMinY.Text="";
            this.txtMinY.Location = new System.Drawing.Point(0x58, 0x30);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Enabled = true;
            this.txtMinY.Size = new Size(200, 0x17);
            this.txtMinY.TabIndex = 5;
            this.txtMaxX.Text="";
            this.txtMaxX.Location = new System.Drawing.Point(0x58, 0x4e);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Enabled = true;
            this.txtMaxX.Size = new Size(200, 0x17);
            this.txtMaxX.TabIndex = 6;
            this.txtMaxY.Text="";
            this.txtMaxY.Location = new System.Drawing.Point(0x58, 0x6d);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Enabled = true;
            this.txtMaxY.Size = new Size(200, 0x17);
            this.txtMaxY.TabIndex = 7;
            this.btnAdd.Location = new System.Drawing.Point(0x130, 0x18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x30, 0x18);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "增加";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new System.Drawing.Point(0x130, 0x38);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x30, 0x18);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "CoverageTicPropertyPage";
            base.Size = new Size(0x188, 0x138);
            base.Load += new EventHandler(this.CoverageTicPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.dataGrid1.EndInit();
            this.groupBox2.ResumeLayout(false);
          
            base.ResumeLayout(false);
        }

        public ICoverageName CoverageName
        {
            set
            {
                this.icoverageName_0 = value;
            }
        }
    }
}

