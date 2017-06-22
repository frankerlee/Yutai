using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Framework.Docking;
using Yutai.Plugins.Events;
using IDockContent = Yutai.ArcGIS.Framework.Docking.IDockContent;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class SnapConfigControl : UserControl, IDockContent
    {
        private DataTable dt = new DataTable();
        private bool m_CanDo = false;
        private bool m_InitOk = false;
        private IArray m_pArray = new ArrayClass();

        public SnapConfigControl()
        {
            this.InitializeComponent();
            this.dt.Columns.Clear();
            this.dt.Rows.Clear();
            this.dt.Columns.Add("层名");
            this.dt.Columns["层名"].ReadOnly = true;
            this.dt.Columns.Add("顶点");
            this.dt.Columns["顶点"].DataType = System.Type.GetType("System.Boolean");
            this.dt.Columns.Add("边");
            this.dt.Columns["边"].DataType = System.Type.GetType("System.Boolean");
            this.dt.Columns.Add("端点");
            this.dt.Columns["端点"].DataType = System.Type.GetType("System.Boolean");
            this.dt.Columns.Add("垂足点");
            this.dt.Columns["垂足点"].DataType = System.Type.GetType("System.Boolean");
            this.dt.DefaultView.AllowDelete = false;
            this.dt.DefaultView.AllowNew = false;
            this.dt.DefaultView.AllowEdit = true;
            DataGridTableStyle table = new DataGridTableStyle();
            DataGridTextBoxColumn column = new DataGridTextBoxColumn {
                MappingName = "层名",
                HeaderText = "层名",
                Alignment = HorizontalAlignment.Center
            };
            table.GridColumnStyles.Add(column);
            DataGridBoolColumn column2 = new DataGridBoolColumn {
                MappingName = "顶点",
                Width = 50,
                HeaderText = "顶点",
                AllowNull = false,
                Alignment = HorizontalAlignment.Center
            };
            table.GridColumnStyles.Add(column2);
            DataGridBoolColumn column3 = new DataGridBoolColumn {
                MappingName = "边",
                Width = 50,
                AllowNull = false,
                HeaderText = "边",
                Alignment = HorizontalAlignment.Center
            };
            table.GridColumnStyles.Add(column3);
            DataGridBoolColumn column4 = new DataGridBoolColumn {
                MappingName = "端点",
                Width = 50,
                HeaderText = "端点",
                AllowNull = false,
                Alignment = HorizontalAlignment.Center
            };
            table.GridColumnStyles.Add(column4);
            DataGridBoolColumn column5 = new DataGridBoolColumn {
                MappingName = "垂足点",
                Width = 50,
                HeaderText = "垂足点",
                AllowNull = false,
                Alignment = HorizontalAlignment.Center
            };
            table.GridColumnStyles.Add(column5);
            this.dataGrid1.TableStyles.Clear();
            this.dataGrid1.TableStyles.Add(table);
            this.dataGrid1.DataSource = this.dt;
            this.dt.ColumnChanged += new DataColumnChangeEventHandler(this.dt_ColumnChanged);
            this.Text = "捕捉配置";
        }

        private void AddFromGroupLayer(IGroupLayer pGroupLayer)
        {
            for (int i = 0; i < (pGroupLayer as ICompositeLayer).Count; i++)
            {
                ILayer layer = (pGroupLayer as ICompositeLayer).get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.AddFromGroupLayer(layer as IGroupLayer);
                }
                else if ((layer is IFeatureLayer) && !this.LayerIsInSnapConfig(layer as IFeatureLayer))
                {
                    LayerSnapInfo lSInfo = new LayerSnapInfo {
                        Layer = layer as IFeatureLayer
                    };
                    this.GetSnapInfo(ref lSInfo);
                    this.m_pArray.Add(lSInfo);
                    object[] values = new object[] { layer.Name, lSInfo.bSnapVertex, lSInfo.bSnapBoundary, lSInfo.bSnapEndPoint, lSInfo.bVerticalSnap };
                    this.dt.Rows.Add(values);
                }
            }
        }

        private void AddNewLayer()
        {
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer layer = this.m_pMap.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.AddFromGroupLayer(layer as IGroupLayer);
                }
                else if ((layer is IFeatureLayer) && !this.LayerIsInSnapConfig(layer as IFeatureLayer))
                {
                    LayerSnapInfo lSInfo = new LayerSnapInfo {
                        Layer = layer as IFeatureLayer
                    };
                    this.GetSnapInfo(ref lSInfo);
                    this.m_pArray.Add(lSInfo);
                    object[] values = new object[] { layer.Name, lSInfo.bSnapVertex, lSInfo.bSnapBoundary, lSInfo.bSnapEndPoint, lSInfo.bVerticalSnap };
                    this.dt.Rows.Add(values);
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.InitSnapEnvironment();
        }

        private void cboSnapUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void DeleteLayer(IFeatureLayer pFeatureLayer)
        {
            for (int i = 0; i < this.m_pArray.Count; i++)
            {
                LayerSnapInfo info = this.m_pArray.get_Element(i) as LayerSnapInfo;
                if (pFeatureLayer == info.Layer)
                {
                    this.m_pArray.Remove(i);
                    this.dt.Rows.RemoveAt(i);
                }
            }
        }

 private void dt_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (this.m_CanDo)
            {
                int rowNumber = this.dataGrid1.CurrentCell.RowNumber;
                int columnNumber = this.dataGrid1.CurrentCell.ColumnNumber;
                object obj2 = this.dataGrid1[rowNumber, columnNumber];
                LayerSnapInfo info = (LayerSnapInfo) this.m_pArray.get_Element(rowNumber);
                switch (columnNumber)
                {
                    case 1:
                        info.bSnapVertex = (bool) obj2;
                        break;

                    case 2:
                        info.bSnapBoundary = (bool) obj2;
                        break;

                    case 3:
                        info.bSnapEndPoint = (bool) obj2;
                        break;

                    case 4:
                        info.bVerticalSnap = (bool) obj2;
                        break;
                }
            }
        }

        private void GetSnapInfo(ref LayerSnapInfo LSInfo)
        {
            int num;
            int geometryHitType;
            IFeatureClass featureClass = LSInfo.Layer.FeatureClass;
            if (this.m_pSnapEnvironment != null)
            {
                IFeatureSnapAgent agent = null;
                for (num = 0; num < this.m_pSnapEnvironment.SnapAgentCount; num++)
                {
                    agent = this.m_pSnapEnvironment.get_SnapAgent(num) as IFeatureSnapAgent;
                    if ((agent != null) && (featureClass == agent.FeatureClass))
                    {
                        if (agent is IVerticalSnapAgent)
                        {
                            LSInfo.bVerticalSnap = true;
                        }
                        else
                        {
                            geometryHitType = agent.GeometryHitType;
                            if ((geometryHitType & 4) != 0)
                            {
                                LSInfo.bSnapBoundary = true;
                            }
                            if ((geometryHitType & 32) != 0)
                            {
                            }
                            if ((geometryHitType & 16) != 0)
                            {
                                LSInfo.bSnapEndPoint = true;
                            }
                            if ((geometryHitType & 8) != 0)
                            {
                            }
                            if ((geometryHitType & 1) != 0)
                            {
                                LSInfo.bSnapVertex = true;
                            }
                        }
                    }
                }
            }
            else
            {
                IEngineFeatureSnapAgent agent2 = null;
                for (num = 0; num < this.m_pEngineSnapEnvironment.SnapAgentCount; num++)
                {
                    agent2 = this.m_pEngineSnapEnvironment.get_SnapAgent(num) as IEngineFeatureSnapAgent;
                    if ((agent2 != null) && (featureClass == agent2.FeatureClass))
                    {
                        if (agent2 is IVerticalSnapAgent)
                        {
                            LSInfo.bVerticalSnap = true;
                        }
                        else
                        {
                            geometryHitType = (int) agent2.HitType;
                            if ((geometryHitType & 4) != 0)
                            {
                                LSInfo.bSnapBoundary = true;
                            }
                            if ((geometryHitType & 32) != 0)
                            {
                            }
                            if ((geometryHitType & 16) != 0)
                            {
                                LSInfo.bSnapEndPoint = true;
                            }
                            if ((geometryHitType & 8) != 0)
                            {
                            }
                            if ((geometryHitType & 1) != 0)
                            {
                                LSInfo.bSnapVertex = true;
                            }
                        }
                    }
                }
            }
        }

        private void InitContrl()
        {
            this.m_pArray.RemoveAll();
            this.dt.Rows.Clear();
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer layer2 = this.m_pMap.get_Layer(i);
                if (layer2 is IGroupLayer)
                {
                    this.ReadGroupLayer(layer2 as IGroupLayer);
                }
                else if (layer2 is IFeatureLayer)
                {
                    IFeatureLayer layer = layer2 as IFeatureLayer;
                    LayerSnapInfo lSInfo = new LayerSnapInfo {
                        Layer = layer
                    };
                    this.GetSnapInfo(ref lSInfo);
                    this.m_pArray.Add(lSInfo);
                    object[] values = new object[] { layer.Name, lSInfo.bSnapVertex, lSInfo.bSnapBoundary, lSInfo.bSnapEndPoint, lSInfo.bVerticalSnap };
                    this.dt.Rows.Add(values);
                }
            }
            if (this.m_pSnapEnvironment != null)
            {
                this.txtRadio.Text = this.m_pSnapEnvironment.SnapTolerance.ToString();
                this.cboSnapUnits.SelectedIndex = (int) this.m_pSnapEnvironment.SnapToleranceUnits;
                this.chkStartSnap.Checked = ApplicationRef.Application.UseSnap;
            }
            else
            {
                this.txtRadio.Text = this.m_pEngineSnapEnvironment.SnapTolerance.ToString();
                this.cboSnapUnits.SelectedIndex = (int) this.m_pEngineSnapEnvironment.SnapToleranceUnits;
            }
        }

 public void InitSnapEnvironment()
        {
            SnapConfigControl.LayerSnapInfo element;
            int i;
            int hitType;
            FeatureSnapAgent featureSnapAgent;
            try
            {
                double num = double.Parse(this.txtRadio.Text);
                if (num <= 0)
                {
                    MessageBox.Show("数据输入错误!");
                }
                else if (this.m_pEngineSnapEnvironment == null)
                {
                    this.m_pSnapEnvironment.SnapTolerance = num;
                }
                else
                {
                    this.m_pEngineSnapEnvironment.SnapTolerance = num;
                }
            }
            catch
            {
                MessageBox.Show("数据输入错误!");
            }
            if (this.m_pEngineSnapEnvironment == null)
            {
                this.m_pSnapEnvironment.SnapToleranceUnits = (esriEngineSnapToleranceUnits)this.cboSnapUnits.SelectedIndex;
                ApplicationRef.Application.UseSnap = this.chkStartSnap.Checked;
                this.m_pSnapEnvironment.ClearSnapAgents();
                for (i = 0; i < this.m_pArray.Count; i++)
                {
                    element = (SnapConfigControl.LayerSnapInfo)this.m_pArray.Element[i];
                    hitType = element.HitType;
                    if (hitType != 0)
                    {
                        featureSnapAgent = new FeatureSnapAgent()
                        {
                            FeatureClass = element.Layer.FeatureClass,
                            GeometryHitType = hitType
                        };
                        this.m_pSnapEnvironment.AddSnapAgent(featureSnapAgent);
                    }
                    if (element.bVerticalSnap)
                    {
                        featureSnapAgent = new FeatureSnapAgent()
                        {
                            FeatureClass = element.Layer.FeatureClass
                        };
                        this.m_pSnapEnvironment.AddSnapAgent(featureSnapAgent);
                    }
                }
            }
            else
            {
                this.m_pEngineSnapEnvironment.ClearSnapAgents();
                this.m_pEngineSnapEnvironment.SnapToleranceUnits = (esriEngineSnapToleranceUnits)this.cboSnapUnits.SelectedIndex;
                for (i = 0; i < this.m_pArray.Count; i++)
                {
                    element = (SnapConfigControl.LayerSnapInfo)this.m_pArray.Element[i];
                    hitType = element.HitType;
                    if (hitType != 0)
                    {
                        IEngineFeatureSnapAgent engineFeatureSnapClass = new EngineFeatureSnapClass()
                        {
                            FeatureClass = element.Layer.FeatureClass,
                            HitType = (esriGeometryHitPartType)hitType
                        };
                        this.m_pEngineSnapEnvironment.AddSnapAgent(engineFeatureSnapClass);
                    }
                    if (element.bVerticalSnap)
                    {
                        featureSnapAgent = new FeatureSnapAgent();
                        featureSnapAgent.FeatureClass = element.Layer.FeatureClass;
                    }
                }
            }
        }

       

       

        private bool LayerIsExit(IGroupLayer pGroupLayer, ILayer pFindLayer)
        {
            for (int i = 0; i < (pGroupLayer as ICompositeLayer).Count; i++)
            {
                ILayer layer = (pGroupLayer as ICompositeLayer).get_Layer(i);
                if (layer == pFindLayer)
                {
                    return true;
                }
                if ((layer is IGroupLayer) && this.LayerIsExit(layer as IGroupLayer, pFindLayer))
                {
                    return true;
                }
            }
            return false;
        }

        private bool LayerIsExit(IMap pMap, ILayer pFindLayer)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer layer = pMap.get_Layer(i);
                if (layer == pFindLayer)
                {
                    return true;
                }
                if ((layer is IGroupLayer) && this.LayerIsExit(layer as IGroupLayer, pFindLayer))
                {
                    return true;
                }
            }
            return false;
        }

        private bool LayerIsInSnapConfig(IFeatureLayer pFindLayer)
        {
            for (int i = 0; i < this.m_pArray.Count; i++)
            {
                LayerSnapInfo info = this.m_pArray.get_Element(i) as LayerSnapInfo;
                if (pFindLayer == info.Layer)
                {
                    return true;
                }
            }
            return false;
        }

        private void ReadGroupLayer(IGroupLayer pGroupLayer)
        {
            for (int i = 0; i < (pGroupLayer as ICompositeLayer).Count; i++)
            {
                ILayer layer = (pGroupLayer as ICompositeLayer).get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.ReadGroupLayer(layer as IGroupLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = layer as IFeatureLayer;
                    LayerSnapInfo lSInfo = new LayerSnapInfo {
                        Layer = layer2
                    };
                    this.GetSnapInfo(ref lSInfo);
                    this.m_pArray.Add(lSInfo);
                    object[] values = new object[] { layer2.Name, lSInfo.bSnapVertex, lSInfo.bSnapBoundary, lSInfo.bSnapEndPoint, lSInfo.bVerticalSnap };
                    this.dt.Rows.Add(values);
                }
            }
        }

        private void SnapConfigControl_ItemAdded(object Item)
        {
            if (Item is IFeatureLayer)
            {
                IFeatureLayer pFindLayer = Item as IFeatureLayer;
                if (!this.LayerIsInSnapConfig(pFindLayer))
                {
                    LayerSnapInfo lSInfo = new LayerSnapInfo {
                        Layer = pFindLayer
                    };
                    this.GetSnapInfo(ref lSInfo);
                    this.m_pArray.Add(lSInfo);
                    object[] values = new object[] { pFindLayer.Name, lSInfo.bSnapVertex, lSInfo.bSnapBoundary, lSInfo.bSnapEndPoint, lSInfo.bVerticalSnap };
                    this.dt.Rows.Add(values);
                }
            }
        }

        private void SnapConfigControl_ItemDeleted(object Item)
        {
            if (Item is IFeatureLayer)
            {
                for (int i = 0; i < this.m_pArray.Count; i++)
                {
                    LayerSnapInfo info = this.m_pArray.get_Element(i) as LayerSnapInfo;
                    if (Item == info.Layer)
                    {
                        this.m_pArray.Remove(i);
                        this.dt.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void SnapConfigControl_Load(object sender, EventArgs e)
        {
            this.m_InitOk = true;
            this.InitContrl();
            this.m_CanDo = true;
        }

        private void SnapConfigControl_OnLayerDeleted(ILayer pLayer)
        {
            if (pLayer is IFeatureLayer)
            {
                this.DeleteLayer(pLayer as IFeatureLayer);
            }
        }

        private void SnapConfigControl_OnMapDocumentChangedEvent()
        {
            if (this.m_pMap.LayerCount == 0)
            {
                this.m_pArray.RemoveAll();
                this.dt.Rows.Clear();
            }
            else
            {
                for (int i = 0; i < this.m_pArray.Count; i++)
                {
                    LayerSnapInfo info = this.m_pArray.get_Element(i) as LayerSnapInfo;
                    if (!this.LayerIsExit(this.m_pMap, info.Layer))
                    {
                        this.m_pArray.Remove(i);
                        this.dt.Rows.RemoveAt(i);
                    }
                }
                this.AddNewLayer();
            }
        }

        private void txtRadio_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IApplication Application
        {
            set
            {
                bool flag = false;
                if (this.m_InitOk && (this.m_pApp != value))
                {
                    flag = true;
                }
                this.m_pApp = value;
                this.m_pMap = this.m_pApp.FocusMap;
                (this.m_pMap as IActiveViewEvents_Event).ItemAdded+=(new IActiveViewEvents_ItemAddedEventHandler(this.SnapConfigControl_ItemAdded));
                (this.m_pMap as IActiveViewEvents_Event).ItemDeleted+=(new IActiveViewEvents_ItemDeletedEventHandler(this.SnapConfigControl_ItemDeleted));
                (this.m_pApp as IApplicationEvents).OnLayerDeleted += new OnLayerDeletedHandler(this.SnapConfigControl_OnLayerDeleted);
                (this.m_pApp as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.SnapConfigControl_OnMapDocumentChangedEvent);
                if (flag)
                {
                    this.m_CanDo = false;
                    this.InitContrl();
                    this.m_CanDo = true;
                }
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Left;
            }
        }

        public IEngineSnapEnvironment EngineSnapEnvironment
        {
            set
            {
                this.m_pEngineSnapEnvironment = value;
            }
        }

        public DockContentHandler DockHandler { get; }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        public int Width { get; set; }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
            set { base.Width = value; }
        }

        public IMap Map
        {
            set
            {
                bool flag = false;
                if (this.m_InitOk && (this.m_pMap != value))
                {
                    flag = true;
                }
                this.m_pMap = value;
                (this.m_pMap as IActiveViewEvents_Event).ItemAdded+=(new IActiveViewEvents_ItemAddedEventHandler(this.SnapConfigControl_ItemAdded));
                (this.m_pMap as IActiveViewEvents_Event).ItemDeleted+=(new IActiveViewEvents_ItemDeletedEventHandler(this.SnapConfigControl_ItemDeleted));
                if (flag)
                {
                    this.m_CanDo = false;
                    this.InitContrl();
                    this.m_CanDo = true;
                }
            }
        }

        public ISnapEnvironment SnapEnvironment
        {
            set
            {
                this.m_pSnapEnvironment = value;
            }
        }

        private partial class LayerSnapInfo
        {
            public bool bSnapBoundary = false;
            public bool bSnapEndPoint = false;
            public bool bSnapVertex = false;
            public bool bVerticalSnap = false;
            public IFeatureLayer Layer = null;

            public int HitType
            {
                get
                {
                    int num = 0;
                    if (this.bSnapVertex)
                    {
                        num |= 1;
                    }
                    if (this.bSnapBoundary)
                    {
                        num |= 4;
                    }
                    if (this.bSnapEndPoint)
                    {
                        num |= 16;
                    }
                    return num;
                }
            }
        }
    }
}

