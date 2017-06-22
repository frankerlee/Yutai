using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class GroupLayerPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private ICompositeLayer icompositeLayer_0 = null;
        private IGroupLayer igroupLayer_0 = null;

        public GroupLayerPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                IGroupLayer layer = this.icompositeLayer_0 as IGroupLayer;
                ICompositeLayer layer2 = this.igroupLayer_0 as ICompositeLayer;
                layer.Clear();
                for (int i = 0; i < layer2.Count; i++)
                {
                    layer.Add(layer2.get_Layer(i));
                }
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.method_3(this.igroupLayer_0);
            ICompositeLayer layer = this.igroupLayer_0 as ICompositeLayer;
            for (int i = this.listBox1.Items.Count; i < layer.Count; i++)
            {
                this.listBox1.Items.Add(new LayerWrapObject(layer.get_Layer(i)));
                this.bool_1 = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem is LayerWrapObject)
            {
                this.igroupLayer_0.Delete((this.listBox1.SelectedItem as LayerWrapObject).Layer);
                this.listBox1.Items.Remove(this.listBox1.SelectedItem);
                this.bool_1 = true;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem is LayerWrapObject)
            {
                object selectedItem = this.listBox1.SelectedItem;
                int num = this.method_1(this.igroupLayer_0 as ICompositeLayer, (this.listBox1.SelectedItem as LayerWrapObject).Layer) + 1;
                this.method_2(this.igroupLayer_0, (this.listBox1.SelectedItem as LayerWrapObject).Layer, num);
                this.listBox1.Items.Remove(selectedItem);
                this.listBox1.Items.Insert(num, selectedItem);
                this.bool_1 = true;
            }
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem is LayerWrapObject)
            {
                frmLayerPropertyEx ex = new frmLayerPropertyEx {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = (this.listBox1.SelectedItem as LayerWrapObject).Layer
                };
                if (ex.ShowDialog() == DialogResult.OK)
                {
                    this.bool_1 = true;
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem is LayerWrapObject)
            {
                object selectedItem = this.listBox1.SelectedItem;
                int num = this.method_1(this.igroupLayer_0 as ICompositeLayer, (this.listBox1.SelectedItem as LayerWrapObject).Layer);
                if (num != 0)
                {
                    num--;
                    this.method_2(this.igroupLayer_0, (this.listBox1.SelectedItem as LayerWrapObject).Layer, num);
                    this.listBox1.Items.Remove(selectedItem);
                    this.listBox1.Items.Insert(num, selectedItem);
                    this.bool_1 = true;
                }
            }
        }

 private void GroupLayerPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

 private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndices.Count > 0)
            {
                this.btnDelete.Enabled = true;
                this.btnProperty.Enabled = true;
                if (this.listBox1.SelectedIndex == 0)
                {
                    this.btnUp.Enabled = false;
                    if (this.listBox1.Items.Count > 1)
                    {
                        this.btnDown.Enabled = true;
                    }
                    else
                    {
                        this.btnDown.Enabled = false;
                    }
                }
                else if (this.listBox1.SelectedIndex == (this.listBox1.Items.Count - 1))
                {
                    this.btnUp.Enabled = true;
                    this.btnDown.Enabled = false;
                }
                else
                {
                    this.btnUp.Enabled = true;
                    this.btnDown.Enabled = true;
                }
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnProperty.Enabled = false;
                this.btnDown.Enabled = false;
                this.btnUp.Enabled = false;
            }
        }

        private void method_0()
        {
            this.igroupLayer_0 = new GroupLayerClass();
            for (int i = 0; i < this.icompositeLayer_0.Count; i++)
            {
                ILayer layer = this.icompositeLayer_0.get_Layer(i);
                this.igroupLayer_0.Add(layer);
                this.listBox1.Items.Add(new LayerWrapObject(layer));
            }
        }

        private int method_1(ICompositeLayer icompositeLayer_1, ILayer ilayer_0)
        {
            for (int i = 0; i < icompositeLayer_1.Count; i++)
            {
                if (icompositeLayer_1.get_Layer(i) == ilayer_0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void method_2(IGroupLayer igroupLayer_1, ILayer ilayer_0, int int_0)
        {
            ICompositeLayer layer = igroupLayer_1 as ICompositeLayer;
            if ((layer.Count - 1) == int_0)
            {
                igroupLayer_1.Delete(ilayer_0);
                igroupLayer_1.Add(ilayer_0);
            }
            else
            {
                int num;
                IArray array = new ArrayClass();
                for (num = 0; num < layer.Count; num++)
                {
                    array.Add(layer.get_Layer(num));
                }
                igroupLayer_1.Clear();
                for (num = 0; num < array.Count; num++)
                {
                    if (layer.Count == int_0)
                    {
                        igroupLayer_1.Add(ilayer_0);
                    }
                    ILayer layer2 = array.get_Element(num) as ILayer;
                    if (layer2 != ilayer_0)
                    {
                        igroupLayer_1.Add(layer2);
                    }
                }
            }
        }

        private void method_3(object object_0)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "添加数据",
                AllowMultiSelect = true
            };
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IFeatureClass featureClass;
                IFeatureLayer layer;
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                IArray array = new ArrayClass();
                IArray items = file.Items;
                int index = 0;
                while (true)
                {
                    if (index >= items.Count)
                    {
                        break;
                    }
                    IGxDataset dataset = items.get_Element(index) as IGxDataset;
                    if (dataset != null)
                    {
                        IDataset dataset2 = dataset.Dataset;
                        if (dataset2 != null)
                        {
                            if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                            {
                                featureClass = (IFeatureClass) dataset2;
                                if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                                {
                                    layer = new FDOGraphicsLayerClass {
                                        FeatureClass = featureClass,
                                        Name = featureClass.AliasName
                                    };
                                }
                                else
                                {
                                    layer = new FeatureLayerClass {
                                        FeatureClass = featureClass,
                                        Name = featureClass.AliasName
                                    };
                                }
                                array.Add(layer);
                            }
                            else
                            {
                                IFDOGraphicsLayerFactory factory;
                                if (dataset2.Type == esriDatasetType.esriDTCadDrawing)
                                {
                                    IEnumGxObject children = (dataset as IGxObjectContainer).Children;
                                    children.Reset();
                                    for (IGxDataset dataset3 = children.Next() as IGxDataset; dataset3 != null; dataset3 = children.Next() as IGxDataset)
                                    {
                                        featureClass = dataset3.Dataset as IFeatureClass;
                                        if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                                        {
                                            factory = new FDOGraphicsLayerFactoryClass();
                                            layer = (IFeatureLayer) factory.OpenGraphicsLayer((IFeatureWorkspace) featureClass.FeatureDataset.Workspace, featureClass.FeatureDataset, featureClass.AliasName);
                                        }
                                        else
                                        {
                                            layer = new FeatureLayerClass {
                                                FeatureClass = featureClass,
                                                Name = featureClass.AliasName
                                            };
                                        }
                                        array.Add(layer);
                                    }
                                }
                                else if (dataset2.Type == esriDatasetType.esriDTFeatureDataset)
                                {
                                    IFeatureClassContainer container = (IFeatureClassContainer) dataset2;
                                    index = 0;
                                    while (index < (container.ClassCount - 1))
                                    {
                                        featureClass = container.get_Class(index);
                                        if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                                        {
                                            factory = new FDOGraphicsLayerFactoryClass();
                                            layer = (IFeatureLayer) factory.OpenGraphicsLayer((IFeatureWorkspace) featureClass.FeatureDataset.Workspace, featureClass.FeatureDataset, featureClass.AliasName);
                                        }
                                        else
                                        {
                                            layer = new FeatureLayerClass {
                                                FeatureClass = featureClass,
                                                Name = featureClass.AliasName
                                            };
                                        }
                                        array.Add(layer);
                                        index++;
                                    }
                                }
                                else if (dataset2.Type == esriDatasetType.esriDTTin)
                                {
                                    ITinLayer unk = new TinLayerClass {
                                        Dataset = (ITin) dataset2,
                                        Name = dataset2.Name
                                    };
                                    array.Add(unk);
                                }
                                else if ((dataset2.Type == esriDatasetType.esriDTRasterDataset) || (dataset2.Type == esriDatasetType.esriDTRasterBand))
                                {
                                    bool flag = true;
                                    if (!((IRasterPyramid) dataset2).Present)
                                    {
                                        if (ApplicationRef.PyramidPromptType == PyramidPromptType.AlwaysBuildNoPrompt)
                                        {
                                            ((IRasterPyramid) dataset2).Create();
                                        }
                                        else if (ApplicationRef.PyramidPromptType == PyramidPromptType.AlwaysPrompt)
                                        {
                                            switch (BuildPyramidSet.Show())
                                            {
                                                case DialogResult.Yes:
                                                    ((IRasterPyramid) dataset2).Create();
                                                    break;

                                                case DialogResult.Cancel:
                                                    flag = false;
                                                    break;
                                            }
                                        }
                                    }
                                    if (flag)
                                    {
                                        RasterLayerClass class3 = new RasterLayerClass {
                                            Cached = true
                                        };
                                        IRasterLayer layer3 = class3;
                                        layer3.CreateFromDataset((IRasterDataset) dataset2);
                                        layer3.Name = dataset2.Name;
                                        array.Add(layer3);
                                    }
                                }
                                else if (dataset2.Type == esriDatasetType.esriDTTable)
                                {
                                    try
                                    {
                                        IRasterCatalogTable pCatalog = new RasterCatalogTableClass {
                                            Table = (ITable) dataset2
                                        };
                                        pCatalog.Update();
                                        IRasterCatalogLayer layer4 = new RasterCatalogLayerClass();
                                        layer4.Create(pCatalog);
                                        layer4.Name = dataset2.BrowseName;
                                        array.Add(layer4);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    index++;
                }
                int count = array.Count;
                ILayer layer5 = null;
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (layer5 is IRasterCatalogLayer)
                    {
                        if (object_0 is IMap)
                        {
                            (object_0 as IMap).AddLayer(layer5);
                        }
                        else if (object_0 is IGroupLayer)
                        {
                            (object_0 as IGroupLayer).Add(layer5);
                        }
                        array.Remove(index);
                        count--;
                    }
                }
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (layer5 is IRasterLayer)
                    {
                        if (object_0 is IMap)
                        {
                            (object_0 as IMap).AddLayer(layer5);
                        }
                        else if (object_0 is IGroupLayer)
                        {
                            (object_0 as IGroupLayer).Add(layer5);
                        }
                        array.Remove(index);
                        count--;
                    }
                }
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (layer5 is ITinLayer)
                    {
                        if (object_0 is IMap)
                        {
                            (object_0 as IMap).AddLayer(layer5);
                        }
                        else if (object_0 is IGroupLayer)
                        {
                            (object_0 as IGroupLayer).Add(layer5);
                        }
                        array.Remove(index);
                        count--;
                    }
                }
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (layer5 is IFeatureLayer)
                    {
                        layer = layer5 as IFeatureLayer;
                        featureClass = layer.FeatureClass;
                        if ((featureClass.ShapeType == esriGeometryType.esriGeometryPolygon) || (featureClass.ShapeType == esriGeometryType.esriGeometryEnvelope))
                        {
                            if (object_0 is IMap)
                            {
                                (object_0 as IMap).AddLayer(layer5);
                            }
                            else if (object_0 is IGroupLayer)
                            {
                                (object_0 as IGroupLayer).Add(layer5);
                            }
                            array.Remove(index);
                            count--;
                        }
                    }
                }
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (layer5 is IFeatureLayer)
                    {
                        layer = layer5 as IFeatureLayer;
                        featureClass = layer.FeatureClass;
                        if (((((featureClass.ShapeType == esriGeometryType.esriGeometryLine) || (featureClass.ShapeType == esriGeometryType.esriGeometryBezier3Curve)) || ((featureClass.ShapeType == esriGeometryType.esriGeometryCircularArc) || (featureClass.ShapeType == esriGeometryType.esriGeometryEllipticArc))) || (featureClass.ShapeType == esriGeometryType.esriGeometryPath)) || (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline))
                        {
                            if (object_0 is IMap)
                            {
                                (object_0 as IMap).AddLayer(layer5);
                            }
                            else if (object_0 is IGroupLayer)
                            {
                                (object_0 as IGroupLayer).Add(layer5);
                            }
                            array.Remove(index);
                            count--;
                        }
                    }
                }
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (layer5 is IFeatureLayer)
                    {
                        layer = layer5 as IFeatureLayer;
                        featureClass = layer.FeatureClass;
                        if ((featureClass.ShapeType == esriGeometryType.esriGeometryMultipoint) || (featureClass.ShapeType == esriGeometryType.esriGeometryPoint))
                        {
                            if (object_0 is IMap)
                            {
                                (object_0 as IMap).AddLayer(layer5);
                            }
                            else if (object_0 is IGroupLayer)
                            {
                                (object_0 as IGroupLayer).Add(layer5);
                            }
                            array.Remove(index);
                            count--;
                        }
                    }
                }
                for (index = count - 1; index >= 0; index--)
                {
                    layer5 = array.get_Element(index) as ILayer;
                    if (object_0 is IMap)
                    {
                        (object_0 as IMap).AddLayer(layer5);
                    }
                    else if (object_0 is IGroupLayer)
                    {
                        (object_0 as IGroupLayer).Add(layer5);
                    }
                    array.Remove(index);
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
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
                return this.bool_1;
            }
        }

        public object SelectItem
        {
            set
            {
                this.icompositeLayer_0 = value as ICompositeLayer;
            }
        }

        protected partial class LayerWrapObject
        {
            private ILayer ilayer_0 = null;

            public LayerWrapObject(ILayer ilayer_1)
            {
                this.ilayer_0 = ilayer_1;
            }

            public override string ToString()
            {
                return this.ilayer_0.Name;
            }

            public ILayer Layer
            {
                get
                {
                    return this.ilayer_0;
                }
            }
        }
    }
}

