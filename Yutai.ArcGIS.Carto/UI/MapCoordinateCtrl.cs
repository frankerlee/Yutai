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
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Controls.Controls;


namespace Yutai.ArcGIS.Carto.UI
{
    public partial class MapCoordinateCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private string string_0 = "坐标系统";
        private TreeNode treeNode_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public MapCoordinateCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.ibasicMap_0 is IMap)
                {
                    (this.ibasicMap_0 as IMap).SpatialReferenceLocked = false;
                }
                this.ibasicMap_0.SpatialReference = this.ispatialReference_0;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ispatialReference_0 = null;
            this.method_6();
            this.method_4(this.ispatialReference_0);
        }

        private void btnGeoTransformation_Click(object sender, EventArgs e)
        {
            frmGeographicTransformationConvert convert = new frmGeographicTransformationConvert
            {
                Map = this.ibasicMap_0
            };
            if (convert.ShowDialog() != DialogResult.OK)
            {
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterDatasets(), true);
            file.AllowMultiSelect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    IGeoDataset dataset2 = dataset.Dataset as IGeoDataset;
                    if (dataset2 != null)
                    {
                        this.ispatialReference_0 = dataset2.SpatialReference;
                        TreeNode node = new TreeNode(this.ispatialReference_0.Name, 2, 2)
                        {
                            Tag = this.ispatialReference_0
                        };
                        this.treeNode_0.Nodes.Add(node);
                        this.method_4(this.ispatialReference_0);
                        this.method_6();
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence
            {
                SpatialRefrence = this.ispatialReference_0
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_5();
                this.method_4(this.ispatialReference_0);
                this.method_6();
            }
            refrence.Dispose();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            System.Drawing.Point pos = new System.Drawing.Point(this.btnNew.Location.X,
                this.btnNew.Location.Y + this.btnNew.Height);
            this.contextMenu_0.Show(this, pos);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "空间参考文件 (*.prj)|*.prj"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                this.ispatialReference_0 =
                    ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0).CreateESRISpatialReferenceFromPRJFile(
                        fileName);
                this.method_4(this.ispatialReference_0);
                this.method_6();
                TreeNode node = new TreeNode(this.ispatialReference_0.Name, 2, 2)
                {
                    Tag = this.ispatialReference_0
                };
                this.treeNode_0.Nodes.Add(node);
            }
            dialog = null;
        }

        public void Cancel()
        {
        }

        private void MapCoordinateCtrl_Load(object sender, EventArgs e)
        {
            if (this.ispatialReference_0 == null)
            {
                this.ispatialReference_0 = new UnknownCoordinateSystemClass();
            }
            this.method_4(this.ispatialReference_0);
            this.method_2();
        }

        private void menuItem_0_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence
            {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumGeographicCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_5();
                this.method_4(this.ispatialReference_0);
                this.method_6();
                TreeNode node = new TreeNode(this.ispatialReference_0.Name, 2, 2)
                {
                    Tag = this.ispatialReference_0
                };
                this.treeNode_0.Nodes.Add(node);
                this.treeView1.SelectedNode = node;
            }
        }

        private void menuItem_1_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence
            {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumProjectCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_5();
                this.method_4(this.ispatialReference_0);
                this.method_6();
                TreeNode node = new TreeNode(this.ispatialReference_0.Name, 2, 2)
                {
                    Tag = this.ispatialReference_0
                };
                this.treeNode_0.Nodes.Add(node);
                this.treeView1.SelectedNode = node;
            }
        }

        private void method_0(TreeNode treeNode_1)
        {
        }

        private void method_1(ILayer ilayer_0, TreeNode treeNode_1)
        {
            IGeoDataset dataset;
            TreeNode node;
            TreeNode node2;
            if (ilayer_0 is IFDOGraphicsLayer)
            {
                dataset = ilayer_0 as IGeoDataset;
                if (dataset.SpatialReference != null)
                {
                    node = new TreeNode(ilayer_0.Name, 0, 1);
                    treeNode_1.Nodes.Add(node);
                    node2 = new TreeNode(dataset.SpatialReference.Name, 2, 2)
                    {
                        Tag = dataset.SpatialReference
                    };
                    node.Nodes.Add(node2);
                }
            }
            else if (ilayer_0 is ICompositeLayer)
            {
                for (int i = 0; i < (ilayer_0 as ICompositeLayer).Count; i++)
                {
                    this.method_1((ilayer_0 as ICompositeLayer).get_Layer(i), treeNode_1);
                }
            }
            else if (ilayer_0 is IGeoDataset)
            {
                dataset = ilayer_0 as IGeoDataset;
                if (dataset.SpatialReference != null)
                {
                    node = new TreeNode(ilayer_0.Name, 0, 1);
                    treeNode_1.Nodes.Add(node);
                    node2 = new TreeNode(dataset.SpatialReference.Name, 2, 2)
                    {
                        Tag = dataset.SpatialReference
                    };
                    node.Nodes.Add(node2);
                }
            }
        }

        private void method_2()
        {
            TreeNode node2;
            TreeNode node = new TreeNode("预定义", 0, 1);
            this.treeView1.Nodes.Add(node);
            this.method_0(node);
            if (this.ibasicMap_0.LayerCount > 0)
            {
                node = new TreeNode("图层", 0, 1);
                this.treeView1.Nodes.Add(node);
                for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
                {
                    IGeoDataset dataset;
                    TreeNode node3;
                    ILayer layer = this.ibasicMap_0.get_Layer(i);
                    if (layer is IFDOGraphicsLayer)
                    {
                        dataset = layer as IGeoDataset;
                        if (dataset.SpatialReference != null)
                        {
                            node2 = new TreeNode(layer.Name, 0, 1);
                            node.Nodes.Add(node2);
                            node3 = new TreeNode(dataset.SpatialReference.Name, 2, 2)
                            {
                                Tag = dataset.SpatialReference
                            };
                            node2.Nodes.Add(node3);
                        }
                    }
                    else if (layer is IGroupLayer)
                    {
                        for (int j = 0; j < (layer as ICompositeLayer).Count; j++)
                        {
                            this.method_1((layer as ICompositeLayer).get_Layer(j), node);
                        }
                    }
                    else if (layer is IGeoDataset)
                    {
                        try
                        {
                            dataset = layer as IGeoDataset;
                            if (dataset.SpatialReference != null)
                            {
                                node2 = new TreeNode(layer.Name, 0, 1);
                                node.Nodes.Add(node2);
                                node3 = new TreeNode(dataset.SpatialReference.Name, 2, 2)
                                {
                                    Tag = dataset.SpatialReference
                                };
                                node2.Nodes.Add(node3);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            node = new TreeNode("<定制>", 0, 1);
            this.treeNode_0 = node;
            this.treeView1.Nodes.Add(node);
            if (this.ibasicMap_0.SpatialReference != null)
            {
                node2 = new TreeNode(this.ibasicMap_0.SpatialReference.Name, 2, 2)
                {
                    Tag = this.ibasicMap_0.SpatialReference
                };
                node.Nodes.Add(node2);
                this.treeView1.SelectedNode = node2;
            }
            else
            {
                this.treeView1.SelectedNode = node;
            }
            this.btnGeoTransformation.Enabled = this.ibasicMap_0.LayerCount > 0;
        }

        private void method_3()
        {
        }

        private void method_4(ISpatialReference ispatialReference_1)
        {
            if (ispatialReference_1 == null)
            {
                this.btnClear.Enabled = false;
                this.btnModify.Enabled = false;
                this.textBoxDetail.Text = "无投影";
            }
            else
            {
                IGeographicCoordinateSystem geographicCoordinateSystem;
                string str;
                if (ispatialReference_1 is IGeographicCoordinateSystem)
                {
                    geographicCoordinateSystem = (IGeographicCoordinateSystem) ispatialReference_1;
                    str = ((ispatialReference_1.Name + "\r\n") + "别名: " + geographicCoordinateSystem.Alias + "\r\n") +
                          "缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n";
                    string[] strArray = new string[21];
                    strArray[0] = str;
                    strArray[1] = "说明: ";
                    strArray[2] = geographicCoordinateSystem.Remarks;
                    strArray[3] = "\r\n角度单位: ";
                    strArray[4] = geographicCoordinateSystem.CoordinateUnit.Name;
                    strArray[5] = " (";
                    strArray[6] = geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString();
                    strArray[7] = ")\r\n本初子午线: ";
                    strArray[8] = geographicCoordinateSystem.PrimeMeridian.Name;
                    strArray[9] = " (";
                    strArray[10] = geographicCoordinateSystem.PrimeMeridian.Longitude.ToString();
                    strArray[11] = ")\r\n数据: ";
                    strArray[12] = geographicCoordinateSystem.Datum.Name;
                    strArray[13] = "\r\n  椭球体: ";
                    strArray[14] = geographicCoordinateSystem.Datum.Spheroid.Name;
                    strArray[15] = "\r\n    长半轴: ";
                    strArray[16] = geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString();
                    strArray[17] = "\r\n    短半轴: ";
                    strArray[18] = geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString();
                    strArray[19] = "\r\n    扁率倒数: ";
                    double num = 1.0/geographicCoordinateSystem.Datum.Spheroid.Flattening;
                    strArray[20] = num.ToString();
                    str = string.Concat(strArray);
                    this.btnModify.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.textBoxDetail.Text = str;
                }
                else if (!(ispatialReference_1 is IProjectedCoordinateSystem))
                {
                    this.btnClear.Enabled = true;
                    this.btnModify.Enabled = false;
                    this.textBoxDetail.Text = "未知\r\n注意: 一个或多个层的空间参考信息丢失.  这些层的数据不能被投影.";
                }
                else
                {
                    IProjectedCoordinateSystem system2 = (IProjectedCoordinateSystem) ispatialReference_1;
                    geographicCoordinateSystem = system2.GeographicCoordinateSystem;
                    IProjection projection = system2.Projection;
                    IParameter[] parameters = new IParameter[25];
                    ((IProjectedCoordinateSystem4GEN) system2).GetParameters(ref parameters);
                    string str2 = "  ";
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i] == null)
                        {
                            break;
                        }
                        str2 = str2 + parameters[i].Name + ": " + parameters[i].Value.ToString() + "\r\n ";
                    }
                    str = (((((ispatialReference_1.Name + "\r\n") + "别名: " + system2.Alias + "\r\n") + "缩略名: " +
                             system2.Abbreviation + "\r\n") + "说明: " + system2.Remarks + "\r\n") + "投影: " +
                           system2.Projection.Name + "\r\n") + "参数:\r\n" + str2;
                    str = ((((str + "线性单位: " + system2.CoordinateUnit.Name + " (" +
                              system2.CoordinateUnit.MetersPerUnit.ToString() + ")\r\n") + "地理坐标系:\r\n") + "  名称: " +
                            geographicCoordinateSystem.Name + "\r\n") + "  缩略名: " +
                           geographicCoordinateSystem.Abbreviation + "\r\n") + "  说明: " +
                          geographicCoordinateSystem.Remarks + "\r\n";
                    str = str + "  角度单位: " + geographicCoordinateSystem.CoordinateUnit.Name + " (" +
                          geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")\r\n";
                    str = (((((str + "  本初子午线: " + geographicCoordinateSystem.PrimeMeridian.Name + " (" +
                               geographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")\r\n") + "  数据: " +
                              geographicCoordinateSystem.Datum.Name + "\r\n") + "    椭球体: " +
                             geographicCoordinateSystem.Datum.Spheroid.Name + "\r\n") + "    长半轴: " +
                            geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + "\r\n") + "    短半轴: " +
                           geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + "\r\n") + "    扁率倒数: " +
                          ((1.0/geographicCoordinateSystem.Datum.Spheroid.Flattening)).ToString();
                    this.btnModify.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.textBoxDetail.Text = str;
                }
            }
        }

        private void method_5()
        {
            ESRI.ArcGIS.esriSystem.IPersistStream stream =
                (ESRI.ArcGIS.esriSystem.IPersistStream) this.ispatialReference_0;
            IXMLStream stream2 = new XMLStreamClass();
            stream.Save((ESRI.ArcGIS.esriSystem.IStream) stream2, 1);
            string str = stream2.SaveToString();
            int index = str.IndexOf("[");
            str = str.Substring(index - 6);
            index = str.LastIndexOf("]");
            str = str.Substring(0, index + 1);
        }

        private void method_6()
        {
            this.bool_0 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            if (object_0 is IBasicMap)
            {
                this.ibasicMap_0 = object_0 as IBasicMap;
            }
            else
            {
                if (!(object_0 is IMapFrame))
                {
                    return;
                }
                this.ibasicMap_0 = (object_0 as IMapFrame).Map as IBasicMap;
            }
            this.ispatialReference_0 = this.ibasicMap_0.SpatialReference;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ispatialReference_0 = e.Node.Tag as ISpatialReference;
            this.method_6();
            this.method_4(this.ispatialReference_0);
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
                this.ispatialReference_0 = this.ibasicMap_0.SpatialReference;
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_0; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}