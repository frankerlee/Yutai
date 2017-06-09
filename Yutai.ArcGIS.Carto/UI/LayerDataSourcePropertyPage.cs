using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;


namespace Yutai.ArcGIS.Carto.UI
{
    public class LayerDataSourcePropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private SimpleButton btnSetDatasources;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IBasicMap ibasicMap_0 = null;
        private IDataLayer idataLayer_0 = null;
        private Label lblBottom;
        private Label lblLR;
        private Label lblTop;
        private MemoEdit memoEdit1;

        public LayerDataSourcePropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
        }

        private void btnSetDatasources_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "设置数据源",
                AllowMultiSelect = false
            };
            if (this.idataLayer_0 is IFeatureLayer)
            {
                file.AddFilter(new MyGxFilterFeatureClasses(), true);
            }
            if ((file.DoModalOpen() == DialogResult.OK) && (file.Items.Count == 1))
            {
                IGxObject obj2 = file.Items.get_Element(0) as IGxObject;
                IName internalObjectName = obj2.InternalObjectName;
                bool flag = false;
                if ((this.idataLayer_0.get_DataSourceSupported(internalObjectName) && (this.idataLayer_0 is IFeatureLayer)) && (obj2 is IGxDataset))
                {
                    IDataset dataset = (obj2 as IGxDataset).Dataset;
                    if (dataset is IFeatureClass)
                    {
                        try
                        {
                            (this.idataLayer_0 as IFeatureLayer).FeatureClass = dataset as IFeatureClass;
                            flag = true;
                            this.method_0();
                        }
                        catch
                        {
                        }
                    }
                }
                if (!flag)
                {
                    MessageBox.Show("无法把图层数据源设置为选择的数据！");
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.lblLR = new Label();
            this.lblBottom = new Label();
            this.lblTop = new Label();
            this.groupBox2 = new GroupBox();
            this.btnSetDatasources = new SimpleButton();
            this.memoEdit1 = new MemoEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.memoEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.lblLR);
            this.groupBox1.Controls.Add(this.lblBottom);
            this.groupBox1.Controls.Add(this.lblTop);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x130, 0x58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "范围";
            this.lblLR.Location = new System.Drawing.Point(20, 0x22);
            this.lblLR.Name = "lblLR";
            this.lblLR.Size = new Size(0x108, 15);
            this.lblLR.TabIndex = 2;
            this.lblLR.TextAlign = ContentAlignment.MiddleLeft;
            this.lblBottom.Location = new System.Drawing.Point(0x18, 0x40);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new Size(0x108, 15);
            this.lblBottom.TabIndex = 1;
            this.lblBottom.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTop.Location = new System.Drawing.Point(0x18, 0x11);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new Size(0x108, 15);
            this.lblTop.TabIndex = 0;
            this.lblTop.TextAlign = ContentAlignment.MiddleCenter;
            this.groupBox2.Controls.Add(this.btnSetDatasources);
            this.groupBox2.Controls.Add(this.memoEdit1);
            this.groupBox2.Location = new System.Drawing.Point(0x10, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x130, 0x98);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据源";
            this.btnSetDatasources.Location = new System.Drawing.Point(0xc0, 120);
            this.btnSetDatasources.Name = "btnSetDatasources";
            this.btnSetDatasources.Size = new Size(0x58, 0x18);
            this.btnSetDatasources.TabIndex = 1;
            this.btnSetDatasources.Text = "设置数据源...";
            this.btnSetDatasources.Click += new EventHandler(this.btnSetDatasources_Click);
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new System.Drawing.Point(0x10, 0x18);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new Size(0x110, 0x58);
            this.memoEdit1.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LayerDataSourcePropertyPage";
            base.Size = new Size(0x150, 0x128);
            base.Load += new EventHandler(this.LayerDataSourcePropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.memoEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LayerDataSourcePropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            if (this.idataLayer_0 is IGeoDataset)
            {
                IEnvelope extent = (this.idataLayer_0 as IGeoDataset).Extent;
                if (!extent.IsEmpty)
                {
                    this.lblTop.Text = "顶: " + extent.YMax.ToString("0.######");
                    this.lblBottom.Text = "底: " + extent.YMin.ToString("0.######");
                    this.lblLR.Text = "左: " + extent.XMin.ToString("0.######") + "        右: " + extent.XMax.ToString("0.######");
                }
                IName dataSourceName = this.idataLayer_0.DataSourceName;
                if (dataSourceName is IDatasetName)
                {
                    IDatasetName name2 = dataSourceName as IDatasetName;
                    string nameString = dataSourceName.NameString;
                    IWorkspaceName workspaceName = name2.WorkspaceName;
                    if (workspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        object obj2;
                        object obj3;
                        nameString = nameString + "数据类型:sde空间数据库";
                        switch (name2.Type)
                        {
                            case esriDatasetType.esriDTFeatureClass:
                                nameString = nameString + " 要素类\r\n要素类: " + name2.Name + "\r\n";
                                break;

                            case esriDatasetType.esriDTTable:
                                nameString = nameString + " 表\r\n要素类: " + name2.Name + "\r\n";
                                break;
                        }
                        workspaceName.ConnectionProperties.GetAllProperties(out obj2, out obj3);
                        string[] strArray = obj2 as string[];
                        object[] objArray = obj3 as object[];
                        nameString = nameString + "位置:\r\n";
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (string.Compare(strArray[i], "PASSWORD", true) != 0)
                            {
                                nameString = string.Concat(new object[] { nameString, "   ", strArray[i], ": ", objArray[i], "\r\n" });
                            }
                        }
                    }
                    else if (workspaceName.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        nameString = nameString + "数据类型:个人空间数据库";
                        switch (name2.Type)
                        {
                            case esriDatasetType.esriDTFeatureClass:
                                nameString = nameString + " 要素类\r\n要素类: " + name2.Name + "\r\n";
                                break;

                            case esriDatasetType.esriDTTable:
                                nameString = nameString + " 表\r\n要素类: " + name2.Name + "\r\n";
                                break;
                        }
                        nameString = nameString + "位置:" + workspaceName.PathName;
                    }
                    else
                    {
                        switch (name2.Type)
                        {
                            case esriDatasetType.esriDTTable:
                                nameString = nameString + "数据类型:DBF表\r\n";
                                nameString = nameString + "DBF表:" + workspaceName.PathName + @"\" + name2.Name;
                                break;

                            case esriDatasetType.esriDTRasterDataset:
                                nameString = nameString + "数据类型:栅格数据集\r\n";
                                nameString = nameString + "栅格数据集:" + workspaceName.PathName + @"\" + name2.Name;
                                break;

                            case esriDatasetType.esriDTFeatureClass:
                                nameString = nameString + "数据类型:shapefile\r\n";
                                nameString = nameString + "shapefile:" + workspaceName.PathName + @"\" + name2.Name;
                                break;
                        }
                    }
                    this.memoEdit1.Text = nameString;
                }
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.IsPageDirty;
            }
        }

        public object SelectItem
        {
            set
            {
                this.idataLayer_0 = value as IDataLayer;
            }
        }
    }
}

