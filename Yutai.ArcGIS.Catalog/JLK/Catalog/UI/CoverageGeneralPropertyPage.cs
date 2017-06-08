namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CoverageGeneralPropertyPage : UserControl
    {
        private SimpleButton btnBulid;
        private SimpleButton btnClean;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Container container_0 = null;
        private ICoverageName icoverageName_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listView1;
        private string[] string_0 = new string[] { "Not Applicable", "Preliminary", "Exists", "Unknown" };
        private TextEdit txtFeatureCount;
        private TextEdit txtName;
        private TextEdit txtPercise;

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
                CErrorLog.writeErrorLog(this, exception, "");
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
                CErrorLog.writeErrorLog(this, exception, "");
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
                ListViewItem item = new ListViewItem(items) {
                    Tag = name2
                };
                this.listView1.Items.Add(item);
                name2 = featureClassNames.Next();
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtName = new TextEdit();
            this.txtPercise = new TextEdit();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label4 = new Label();
            this.txtFeatureCount = new TextEdit();
            this.btnBulid = new SimpleButton();
            this.btnClean = new SimpleButton();
            this.txtName.Properties.BeginInit();
            this.txtPercise.Properties.BeginInit();
            this.txtFeatureCount.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "精度:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x30, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "要素类:";
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x40, 8);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtName.Size = new Size(160, 0x13);
            this.txtName.TabIndex = 3;
            this.txtPercise.EditValue = "双精度";
            this.txtPercise.Location = new Point(60, 40);
            this.txtPercise.Name = "txtPercise";
            this.txtPercise.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtPercise.Properties.Appearance.Options.UseBackColor = true;
            this.txtPercise.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtPercise.Size = new Size(160, 0x13);
            this.txtPercise.TabIndex = 4;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(0x10, 0x60);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(240, 120);
            this.listView1.TabIndex = 6;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 0x53;
            this.columnHeader_1.Text = "拓扑";
            this.columnHeader_1.Width = 0x51;
            this.columnHeader_2.Text = "有FAT吗";
            this.columnHeader_2.Width = 0x44;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0xe0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 7;
            this.label4.Text = "要素数量:";
            this.label4.Visible = false;
            this.txtFeatureCount.EditValue = "";
            this.txtFeatureCount.Location = new Point(0x58, 0xe0);
            this.txtFeatureCount.Name = "txtFeatureCount";
            this.txtFeatureCount.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtFeatureCount.Properties.Appearance.Options.UseBackColor = true;
            this.txtFeatureCount.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtFeatureCount.Size = new Size(160, 0x13);
            this.txtFeatureCount.TabIndex = 8;
            this.btnBulid.Location = new Point(0x108, 0x68);
            this.btnBulid.Name = "btnBulid";
            this.btnBulid.Size = new Size(0x40, 0x18);
            this.btnBulid.TabIndex = 9;
            this.btnBulid.Text = "Bulid";
            this.btnBulid.Click += new EventHandler(this.btnBulid_Click);
            this.btnClean.Location = new Point(0x108, 0x88);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new Size(0x40, 0x18);
            this.btnClean.TabIndex = 10;
            this.btnClean.Text = "Clean";
            this.btnClean.Click += new EventHandler(this.btnClean_Click);
            base.Controls.Add(this.btnClean);
            base.Controls.Add(this.btnBulid);
            base.Controls.Add(this.txtFeatureCount);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.txtPercise);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CoverageGeneralPropertyPage";
            base.Size = new Size(0x158, 0x110);
            base.Load += new EventHandler(this.CoverageGeneralPropertyPage_Load);
            this.txtName.Properties.EndInit();
            this.txtPercise.Properties.EndInit();
            this.txtFeatureCount.Properties.EndInit();
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

