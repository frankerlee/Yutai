using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class AttributeControl : UserControl, IDockContent
    {
        private AnnoEditControl m_pAnnoEditControl = new AnnoEditControl();
        private AttributeListControl m_pAttributeListControl = new AttributeListControl();
        private AttributeListControl m_pAttributeListControl1 = new AttributeListControl();
        private IWorkspace m_pEditWorkspace = null;
        private int m_SelectType = 0;
        private SysGrants m_sysGrants = new SysGrants();

        public AttributeControl()
        {
            this.InitializeComponent();
            this.m_pAnnoEditControl.Dock = DockStyle.Fill;
            this.m_pAttributeListControl.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(this.m_pAnnoEditControl);
            this.tabPage2.Controls.Add(this.m_pAttributeListControl);
            this.m_pAttributeListControl1.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.m_pAttributeListControl1);
            this.Text = "属性编辑";
        }

        private void AddReleationClass(IFeature pFeature, TreeNode pParentNode)
        {
            IRelationshipClass class3;
            IObjectClass destinationClass;
            TreeNode node;
            IEnumRelationship relationshipsForObject;
            IRelationship relationship2;
            TreeNode node2;
            IEnumRelationshipClass class2 = pFeature.Class.get_RelationshipClasses(esriRelRole.esriRelRoleOrigin);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                destinationClass = class3.DestinationClass;
                if (this.CheckIsEdit(destinationClass as IDataset))
                {
                    node = new TreeNode(destinationClass.AliasName) {
                        Tag = destinationClass
                    };
                    relationshipsForObject = class3.GetRelationshipsForObject(pFeature);
                    relationshipsForObject.Reset();
                    relationship2 = relationshipsForObject.Next();
                    while (relationship2 != null)
                    {
                        if (relationship2.DestinationObject != null)
                        {
                            node2 = new TreeNode(relationship2.DestinationObject.OID.ToString()) {
                                Tag = relationship2.DestinationObject
                            };
                            node.Nodes.Add(node2);
                        }
                        relationship2 = relationshipsForObject.Next();
                    }
                    if (node.Nodes.Count > 0)
                    {
                        pParentNode.Nodes.Add(node);
                    }
                }
            }
            class2 = pFeature.Class.get_RelationshipClasses(esriRelRole.esriRelRoleDestination);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                destinationClass = class3.OriginClass;
                if (this.CheckIsEdit(destinationClass as IDataset))
                {
                    node = new TreeNode(destinationClass.AliasName) {
                        Tag = destinationClass
                    };
                    relationshipsForObject = class3.GetRelationshipsForObject(pFeature);
                    relationshipsForObject.Reset();
                    for (relationship2 = relationshipsForObject.Next(); relationship2 != null; relationship2 = relationshipsForObject.Next())
                    {
                        if (relationship2.OriginObject != null)
                        {
                            node2 = new TreeNode(relationship2.OriginObject.OID.ToString()) {
                                Tag = relationship2.OriginObject
                            };
                            node.Nodes.Add(node2);
                        }
                    }
                    if (node.Nodes.Count > 0)
                    {
                        pParentNode.Nodes.Add(node);
                    }
                }
            }
        }

        private void AttributeControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private bool CheckIsEdit(IFeatureLayer pFeatLayer)
        {
            return this.CheckIsEdit(pFeatLayer.FeatureClass as IDataset);
        }

        private bool CheckIsEdit(IDataset pDataset)
        {
            if (this.m_pEditWorkspace != null)
            {
                IWorkspace workspace = pDataset.Workspace;
                if (workspace.ConnectionProperties.IsEqual(this.m_pEditWorkspace.ConnectionProperties))
                {
                    if (workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (workspace is IVersionedWorkspace)
                        {
                            IVersionedObject obj2 = pDataset as IVersionedObject;
                            if (obj2.IsRegisteredAsVersioned)
                            {
                                return (((AppConfigInfo.UserID.Length == 0) || (AppConfigInfo.UserID.ToLower() == "admin")) || this.m_sysGrants.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2, pDataset.Name));
                            }
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

 public void FlashGeometry(IScreenDisplay pScreenDisplay, IGeometry pGeometry)
        {
            pScreenDisplay.StartDrawing(0, -1);
            switch (pGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    this.FlashPoint(pScreenDisplay, pGeometry);
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    this.FlashLine(pScreenDisplay, pGeometry);
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    this.FlashPolygon(pScreenDisplay, pGeometry);
                    break;
            }
            pScreenDisplay.FinishDrawing();
        }

        public void FlashLine(IScreenDisplay pDisplay, IGeometry pGeometry)
        {
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                Width = 4.0
            };
            IRgbColor color = new RgbColorClass {
                Green = 128
            };
            ISymbol sym = (ISymbol) symbol;
            sym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pDisplay.SetSymbol(sym);
            pDisplay.DrawPolyline(pGeometry);
            Thread.Sleep(300);
            pDisplay.DrawPolyline(pGeometry);
        }

        public void FlashPoint(IScreenDisplay pDisplay, IGeometry pGeometry)
        {
            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                Style = esriSimpleMarkerStyle.esriSMSCircle
            };
            IRgbColor color = new RgbColorClass {
                Green = 128
            };
            ISymbol sym = (ISymbol) symbol;
            sym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pDisplay.SetSymbol(sym);
            pDisplay.DrawPoint(pGeometry);
            Thread.Sleep(300);
            pDisplay.DrawPoint(pGeometry);
        }

        public void FlashPolygon(IScreenDisplay pDisplay, IGeometry pGeometry)
        {
            ISimpleFillSymbol symbol = new SimpleFillSymbolClass {
                Outline = null
            };
            IRgbColor color = new RgbColorClass {
                Green = 128
            };
            ISymbol sym = (ISymbol) symbol;
            sym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pDisplay.SetSymbol(sym);
            pDisplay.DrawPolygon(pGeometry);
            Thread.Sleep(300);
            pDisplay.DrawPolygon(pGeometry);
        }

        public void InitControl()
        {
            try
            {
                this.treeView1.Nodes.Clear();
                this.textBox1.Text = "编辑" + this.m_pMap.SelectionCount.ToString() + "个要素";
                if (this.m_pMap.SelectionCount == 0)
                {
                    this.m_pAttributeListControl1.SelectObject = null;
                }
                else
                {
                    TreeNode node2 = null;
                    IEnumFeature featureSelection = this.m_pMap.FeatureSelection as IEnumFeature;
                    featureSelection.Reset();
                    IFeature pFeature = featureSelection.Next();
                    IClass class2 = null;
                    int num = 0;
                    while (pFeature != null)
                    {
                        if (this.CheckIsEdit(pFeature.Class as IDataset))
                        {
                            if (class2 != pFeature.Class)
                            {
                                class2 = pFeature.Class;
                                node2 = new TreeNode((class2 as IObjectClass).AliasName) {
                                    Tag = class2
                                };
                                this.treeView1.Nodes.Add(node2);
                            }
                            TreeNode node = new TreeNode(pFeature.OID.ToString()) {
                                Tag = pFeature
                            };
                            node2.Nodes.Add(node);
                            this.AddReleationClass(pFeature, node);
                            num++;
                        }
                        pFeature = featureSelection.Next();
                    }
                    if ((this.treeView1.Nodes.Count > 0) && (this.treeView1.Nodes[0].Nodes.Count > 0))
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
                    }
                    this.textBox1.Text = "编辑" + num.ToString() + "个要素";
                }
            }
            catch (Exception exception)
            {
               Logger.Current.Error("", exception, "");
            }
        }

 private void m_pActiveViewEvents_SelectionChanged()
        {
            this.InitControl();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IObject tag = e.Node.Tag as IObject;
            if (tag == null)
            {
                this.m_SelectType = 0;
                this.tabControl1.Visible = false;
                this.panel2.Visible = false;
                this.m_pAttributeListControl1.SelectObject = null;
            }
            else
            {
                if ((tag is IFeature) && base.Visible)
                {
                    this.FlashGeometry((this.m_pMap as IActiveView).ScreenDisplay, (tag as IFeature).Shape);
                }
                if (tag is IAnnotationFeature)
                {
                    this.m_pAttributeListControl.SelectObject = tag;
                    this.m_pAnnoEditControl.AnnotationFeature = tag as IAnnotationFeature;
                    if (this.m_SelectType != 1)
                    {
                        this.tabControl1.Visible = true;
                        this.panel2.Visible = false;
                        this.m_SelectType = 1;
                    }
                }
                else
                {
                    this.m_pAttributeListControl1.SelectObject = tag;
                    if (this.m_SelectType != 2)
                    {
                        this.tabControl1.Visible = false;
                        this.panel2.Visible = true;
                        this.m_SelectType = 2;
                    }
                }
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Right;
            }
        }

        public IWorkspace EditWorkspace
        {
            set
            {
                this.m_pEditWorkspace = value;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pMap = value;
                this.m_pActiveViewEvents = this.m_pMap as IActiveViewEvents_Event;
                this.m_pActiveViewEvents.SelectionChanged+=(new IActiveViewEvents_SelectionChangedEventHandler(this.m_pActiveViewEvents_SelectionChanged));
                this.m_pAnnoEditControl.ActiveView = this.m_pMap as IActiveView;
                this.m_pAttributeListControl.ActiveView = this.m_pMap as IActiveView;
                this.m_pAttributeListControl1.ActiveView = this.m_pMap as IActiveView;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }
    }
}

