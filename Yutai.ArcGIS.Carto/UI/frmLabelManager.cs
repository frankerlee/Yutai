using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmLabelManager : Form
    {
        private AddClassCtrl addClassCtrl_0 = new AddClassCtrl();
        private Container container_0 = null;
        private FillLabelSetCtrl fillLabelSetCtrl_0 = new FillLabelSetCtrl();
        private IMap imap_0 = null;
        private LineLabelSetCtrl lineLabelSetCtrl_0 = new LineLabelSetCtrl();
        private PointLabelSetCtrl pointLabelSetCtrl_0 = new PointLabelSetCtrl();

        public frmLabelManager()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.treeView1.Nodes.Count; i++)
            {
                TreeNode node = this.treeView1.Nodes[i];
                this.method_1(node);
            }
            (this.imap_0 as IActiveView).Refresh();
        }

        private void frmLabelManager_Load(object sender, EventArgs e)
        {
            this.groupBox1.Controls.Add(this.pointLabelSetCtrl_0);
            this.pointLabelSetCtrl_0.Visible = false;
            this.pointLabelSetCtrl_0.Dock = DockStyle.Fill;
            this.groupBox1.Controls.Add(this.lineLabelSetCtrl_0);
            this.lineLabelSetCtrl_0.Visible = false;
            this.lineLabelSetCtrl_0.Dock = DockStyle.Fill;
            this.groupBox1.Controls.Add(this.fillLabelSetCtrl_0);
            this.fillLabelSetCtrl_0.Visible = false;
            this.fillLabelSetCtrl_0.Dock = DockStyle.Fill;
            this.groupBox1.Controls.Add(this.addClassCtrl_0);
            this.addClassCtrl_0.Visible = false;
            this.addClassCtrl_0.Dock = DockStyle.Fill;
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IGeoFeatureLayer)
                {
                    TreeNode node = new TreeNode(layer.Name)
                    {
                        Checked = (layer as IGeoFeatureLayer).DisplayAnnotation
                    };
                    GeoFeatureLayerWrap wrap = new GeoFeatureLayerWrap(layer as IGeoFeatureLayer);
                    node.Tag = wrap;
                    this.treeView1.Nodes.Add(node);
                    this.method_0(node, wrap.AnnoLayerPropertiesColn as IAnnotateLayerPropertiesCollection2);
                }
            }
            this.treeView1.ExpandAll();
            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            }
        }

        private void method_0(TreeNode treeNode_0,
            IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0)
        {
            for (int i = 0; i < iannotateLayerPropertiesCollection2_0.Count; i++)
            {
                IAnnotateLayerProperties properties;
                int num2;
                iannotateLayerPropertiesCollection2_0.QueryItem(i, out properties, out num2);
                TreeNode node = new TreeNode(properties.Class)
                {
                    Checked = properties.DisplayAnnotation,
                    Tag =
                        new AnnotateLayerPropertiesWrap((properties as IClone).Clone() as IAnnotateLayerProperties, num2,
                            false)
                };
                treeNode_0.Nodes.Add(node);
            }
        }

        private void method_1(TreeNode treeNode_0)
        {
            IAnnotateLayerPropertiesCollection2 annoLayerPropertiesColn =
                (treeNode_0.Tag as GeoFeatureLayerWrap).AnnoLayerPropertiesColn as IAnnotateLayerPropertiesCollection2;
            for (int i = 0; i < treeNode_0.Nodes.Count; i++)
            {
                TreeNode node = treeNode_0.Nodes[i];
                IAnnotateLayerProperties annoLayerProperty = (node.Tag as AnnotateLayerPropertiesWrap).AnnoLayerProperty;
                annoLayerProperty.DisplayAnnotation = node.Checked;
                if ((node.Tag as AnnotateLayerPropertiesWrap).IsNew)
                {
                    annoLayerPropertiesColn.Add(annoLayerProperty);
                }
                else
                {
                    annoLayerPropertiesColn.Replace((node.Tag as AnnotateLayerPropertiesWrap).ID, annoLayerProperty);
                }
            }
            (treeNode_0.Tag as GeoFeatureLayerWrap).GeoFeatureLayer.DisplayAnnotation = treeNode_0.Checked;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Tag is GeoFeatureLayerWrap)
                {
                    this.addClassCtrl_0.Node = selectedNode;
                    this.addClassCtrl_0.AnnoLayerPropsColl =
                        (selectedNode.Tag as GeoFeatureLayerWrap).AnnoLayerPropertiesColn as
                            IAnnotateLayerPropertiesCollection2;
                    this.addClassCtrl_0.Visible = true;
                    this.fillLabelSetCtrl_0.Visible = false;
                    this.lineLabelSetCtrl_0.Visible = false;
                    this.pointLabelSetCtrl_0.Visible = false;
                }
                else if (selectedNode.Tag is AnnotateLayerPropertiesWrap)
                {
                    this.addClassCtrl_0.Visible = false;
                    this.fillLabelSetCtrl_0.Visible = false;
                    this.lineLabelSetCtrl_0.Visible = false;
                    this.pointLabelSetCtrl_0.Visible = false;
                    IGeoFeatureLayer geoFeatureLayer = (selectedNode.Parent.Tag as GeoFeatureLayerWrap).GeoFeatureLayer;
                    switch (geoFeatureLayer.FeatureClass.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            this.pointLabelSetCtrl_0.GeoFeatureLayer = geoFeatureLayer;
                            this.pointLabelSetCtrl_0.AnnotateLayerProperties =
                                (selectedNode.Tag as AnnotateLayerPropertiesWrap).AnnoLayerProperty;
                            this.pointLabelSetCtrl_0.Visible = true;
                            break;

                        case esriGeometryType.esriGeometryMultipoint:
                            this.pointLabelSetCtrl_0.GeoFeatureLayer = geoFeatureLayer;
                            this.pointLabelSetCtrl_0.AnnotateLayerProperties =
                                (selectedNode.Tag as AnnotateLayerPropertiesWrap).AnnoLayerProperty;
                            this.pointLabelSetCtrl_0.Visible = true;
                            break;

                        case esriGeometryType.esriGeometryPolyline:
                            this.lineLabelSetCtrl_0.GeoFeatureLayer = geoFeatureLayer;
                            this.lineLabelSetCtrl_0.AnnotateLayerProperties =
                                (selectedNode.Tag as AnnotateLayerPropertiesWrap).AnnoLayerProperty;
                            this.lineLabelSetCtrl_0.Visible = true;
                            break;

                        case esriGeometryType.esriGeometryPolygon:
                            this.fillLabelSetCtrl_0.GeoFeatureLayer = geoFeatureLayer;
                            this.fillLabelSetCtrl_0.AnnotateLayerProperties =
                                (selectedNode.Tag as AnnotateLayerPropertiesWrap).AnnoLayerProperty;
                            this.fillLabelSetCtrl_0.Visible = true;
                            break;
                    }
                }
            }
        }

        public IMap FocusMap
        {
            set { this.imap_0 = value; }
        }

        internal partial class AnnotateLayerPropertiesWrap
        {
            private bool bool_0 = false;
            private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
            private int int_0 = 0;

            public AnnotateLayerPropertiesWrap(IAnnotateLayerProperties iannotateLayerProperties_1, int int_1,
                bool bool_1)
            {
                this.iannotateLayerProperties_0 = iannotateLayerProperties_1;
                this.int_0 = int_1;
                this.bool_0 = bool_1;
            }

            public IAnnotateLayerProperties AnnoLayerProperty
            {
                get { return this.iannotateLayerProperties_0; }
            }

            public int ID
            {
                get { return this.int_0; }
            }

            public bool IsNew
            {
                get { return this.bool_0; }
            }
        }

        internal partial class GeoFeatureLayerWrap
        {
            private IAnnotateLayerPropertiesCollection iannotateLayerPropertiesCollection_0 = null;
            private IGeoFeatureLayer igeoFeatureLayer_0 = null;

            public GeoFeatureLayerWrap(IGeoFeatureLayer igeoFeatureLayer_1)
            {
                this.igeoFeatureLayer_0 = igeoFeatureLayer_1;
                this.iannotateLayerPropertiesCollection_0 = this.igeoFeatureLayer_0.AnnotationProperties;
            }

            public IAnnotateLayerPropertiesCollection AnnoLayerPropertiesColn
            {
                get { return this.iannotateLayerPropertiesCollection_0; }
            }

            public IGeoFeatureLayer GeoFeatureLayer
            {
                get { return this.igeoFeatureLayer_0; }
            }
        }
    }
}