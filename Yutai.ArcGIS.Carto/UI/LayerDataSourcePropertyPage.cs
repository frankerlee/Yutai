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
    public partial class LayerDataSourcePropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IDataLayer idataLayer_0 = null;

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
            frmOpenFile file = new frmOpenFile
            {
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
                if ((this.idataLayer_0.get_DataSourceSupported(internalObjectName) &&
                     (this.idataLayer_0 is IFeatureLayer)) && (obj2 is IGxDataset))
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
                    this.lblLR.Text = "左: " + extent.XMin.ToString("0.######") + "        右: " +
                                      extent.XMax.ToString("0.######");
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
                                nameString =
                                    string.Concat(new object[]
                                        {nameString, "   ", strArray[i], ": ", objArray[i], "\r\n"});
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
            set { this.ibasicMap_0 = value; }
        }

        public bool IsPageDirty
        {
            get { return this.IsPageDirty; }
        }

        public object SelectItem
        {
            set { this.idataLayer_0 = value as IDataLayer; }
        }
    }
}