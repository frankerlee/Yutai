using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Shared;


namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class MultiAttributeControl : UserControl, IDockContent
    {
        private Container components = null;
        private IActiveViewEvents_Event m_pActiveViewEvents;
        private MultiAttributeListControl m_pAttributeListControl = new MultiAttributeListControl();
        private IWorkspace m_pEditWorkspace = null;
        private IMap m_pMap;
        private int m_SelectType = 0;
        private SysGrants m_sysGrants = new SysGrants();
        private Panel panel1;
        private Panel panel2;
        private Splitter splitter1;
        private TextBox textBox1;
        private TreeView treeView1;

        public MultiAttributeControl()
        {
            this.InitializeComponent();
            this.m_pAttributeListControl.Dock = DockStyle.Fill;
            this.m_pAttributeListControl.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.m_pAttributeListControl);
            this.Text = "属性编辑";
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
                if (this.m_pMap.SelectionCount != 0)
                {
                    UID uid = new UIDClass {
                        Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
                    };
                    IEnumLayer layer = this.m_pMap.get_Layers(uid, true);
                    layer.Reset();
                    for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
                    {
                        if (((layer2 is IFeatureLayer) && this.CheckIsEdit(layer2 as IFeatureLayer)) && ((layer2 as IFeatureSelection).SelectionSet.Count > 0))
                        {
                            TreeNode node = new TreeNode(layer2.Name) {
                                Tag = layer2
                            };
                            this.treeView1.Nodes.Add(node);
                        }
                    }
                    if (this.treeView1.Nodes.Count > 0)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                    }
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
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
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
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0x6b, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x12d, 0x130);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "MultiAttributeControl";
            base.Size = new Size(0x198, 0x130);
            base.Load += new EventHandler(this.AttributeControl_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void m_pActiveViewEvents_SelectionChanged()
        {
            if (base.Visible)
            {
                this.InitControl();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ILayer tag = e.Node.Tag as ILayer;
            if (tag != null)
            {
                this.m_pAttributeListControl.SelectLayer = tag as IFeatureLayer;
                this.panel2.Visible = true;
            }
            else
            {
                this.panel2.Visible = false;
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
                this.m_pAttributeListControl.ActiveView = this.m_pMap as IActiveView;
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

