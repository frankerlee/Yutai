using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class AttributeControl : UserControl, IDockContent
    {
        private Container components = null;
        private IActiveViewEvents_Event m_pActiveViewEvents;
        private AnnoEditControl m_pAnnoEditControl = new AnnoEditControl();
        private AttributeListControl m_pAttributeListControl = new AttributeListControl();
        private AttributeListControl m_pAttributeListControl1 = new AttributeListControl();
        private IWorkspace m_pEditWorkspace = null;
        private IMap m_pMap;
        private int m_SelectType = 0;
        private SysGrants m_sysGrants = new SysGrants();
        private Panel panel1;
        private Panel panel2;
        private Splitter splitter1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox textBox1;
        private TreeView treeView1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                Green = 0x80
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
                Green = 0x80
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
                Green = 0x80
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

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.textBox1 = new TextBox();
            this.treeView1 = new TreeView();
            this.splitter1 = new Splitter();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x68, 0x130);
            this.panel1.TabIndex = 0;
            this.textBox1.Dock = DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 0x11b);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x68, 0x15);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "";
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(0x68, 0x130);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.splitter1.Location = new System.Drawing.Point(0x68, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new Size(3, 0x130);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0x6b, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x12d, 0x130);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.Visible = false;
            this.tabPage1.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(0x125, 0x117);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "注记";
            this.tabPage2.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new Size(0x125, 0x117);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "属性";
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0x6b, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x12d, 0x130);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "AttributeControl";
            base.Size = new Size(0x198, 0x130);
            base.Load += new EventHandler(this.AttributeControl_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
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
                this.m_pActiveViewEvents.add_SelectionChanged(new IActiveViewEvents_SelectionChangedEventHandler(this.m_pActiveViewEvents_SelectionChanged));
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

