using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Controls.Editor.UI;
using Yutai.ArcGIS.Framework.Docking;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class frmMap : DockContent
    {
        internal AxMapControl axMapControl1;
        private Bar bar2;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private IContainer components = null;
        private esriControlsDragDropEffect m_Effect;
        private bool m_IsRegisterAsDocument = true;
        private ToolStrip toolStrip1;
        private const int WM_ENTERSIZEMOVE = 0x231;
        private const int WM_EXITSIZEMOVE = 0x232;

        public frmMap()
        {
            this.InitializeComponent();
            base.CloseButton = false;
            this.TJID = "";
            this.GHFAGuid = "";
            base.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if ((e.button == 2) && (this.axMapControl1.ContextMenuStrip != null))
            {
                this.axMapControl1.ContextMenuStrip.Show(this.axMapControl1, e.x, e.y);
            }
        }

        private void axMapControl1_OnOleDrop(object sender, IMapControlEvents2_OnOleDropEvent e)
        {
            IDataObjectHelper dataObjectHelper = (IDataObjectHelper) e.dataObjectHelper;
            esriControlsDropAction dropAction = e.dropAction;
            e.effect = 0;
            switch (dropAction)
            {
                case esriControlsDropAction.esriDropEnter:
                    if (dataObjectHelper.CanGetFiles() | dataObjectHelper.CanGetNames())
                    {
                        this.m_Effect = esriControlsDragDropEffect.esriDragDropCopy;
                    }
                    else if (dataObjectHelper.InternalObject is IDataObject)
                    {
                        this.m_Effect = esriControlsDragDropEffect.esriDragDropCopy;
                    }
                    break;

                case esriControlsDropAction.esriDropOver:
                    e.effect = (int) this.m_Effect;
                    break;
            }
            if (dropAction == esriControlsDropAction.esriDropped)
            {
                int num;
                if (dataObjectHelper.CanGetFiles())
                {
                    System.Array files = System.Array.CreateInstance(typeof(string), 0, 0);
                    files = (System.Array) dataObjectHelper.GetFiles();
                    for (num = 0; num <= (files.Length - 1); num++)
                    {
                        if (this.axMapControl1.CheckMxFile(files.GetValue(num).ToString()))
                        {
                            try
                            {
                                this.axMapControl1.LoadMxFile(files.GetValue(num).ToString(), System.Type.Missing, "");
                                if (ApplicationRef.Application != null)
                                {
                                    ApplicationRef.Application.MapDocumentChanged();
                                }
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("Error:" + exception.Message);
                                return;
                            }
                        }
                        else
                        {
                            IFileName name = new FileNameClass {
                                Path = files.GetValue(num).ToString()
                            };
                            this.CreateLayer((IName) name);
                            if (ApplicationRef.Application != null)
                            {
                                ApplicationRef.Application.MapDocumentChanged();
                            }
                        }
                    }
                }
                else if (dataObjectHelper.CanGetNames())
                {
                    IEnumName names = dataObjectHelper.GetNames();
                    names.Reset();
                    for (IName name3 = names.Next(); name3 != null; name3 = names.Next())
                    {
                        this.CreateLayer(name3);
                    }
                    if (ApplicationRef.Application != null)
                    {
                        ApplicationRef.Application.MapDocumentChanged();
                    }
                }
                if (dataObjectHelper.InternalObject is IDataObject)
                {
                    IList data = (dataObjectHelper.InternalObject as IDataObject).GetData("System.Collections.ArrayList") as IList;
                    for (num = 0; num < data.Count; num++)
                    {
                        IWorkspaceName workspaceName;
                        IWorkspaceName name6;
                        object obj3 = data[num];
                        if (obj3 is IFeatureDatasetName)
                        {
                            IFeatureDatasetName name4 = new FeatureDatasetNameClass();
                            workspaceName = (obj3 as IDatasetName).WorkspaceName;
                            name6 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = workspaceName.WorkspaceFactoryProgID
                            };
                            if (workspaceName.ConnectionProperties != null)
                            {
                                name6.ConnectionProperties = workspaceName.ConnectionProperties;
                            }
                            else
                            {
                                name6.PathName = workspaceName.PathName;
                            }
                            (name4 as IDatasetName).WorkspaceName = name6;
                            (name4 as IDatasetName).Name = (obj3 as IDatasetName).Name;
                            this.CreateLayer(name4 as IName);
                        }
                        else if (obj3 is IRasterDatasetName)
                        {
                            IRasterDatasetName name7 = new RasterDatasetNameClass();
                            workspaceName = (obj3 as IDatasetName).WorkspaceName;
                            name6 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = workspaceName.WorkspaceFactoryProgID
                            };
                            if (workspaceName.ConnectionProperties != null)
                            {
                                name6.ConnectionProperties = workspaceName.ConnectionProperties;
                            }
                            else
                            {
                                name6.PathName = workspaceName.PathName;
                            }
                            (name7 as IDatasetName).WorkspaceName = name6;
                            (name7 as IDatasetName).Name = (obj3 as IDatasetName).Name;
                            this.CreateLayer(name7 as IName);
                        }
                        else if (obj3 is IDatasetName)
                        {
                            IDatasetName name8 = null;
                            if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTTin)
                            {
                                name8 = new TinNameClass();
                            }
                            if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTFeatureClass)
                            {
                                name8 = new FeatureClassNameClass();
                                (name8 as IFeatureClassName).FeatureDatasetName = (obj3 as IFeatureClassName).FeatureDatasetName;
                            }
                            else if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTTopology)
                            {
                                name8 = new TopologyNameClass();
                                (name8 as ITopologyName).FeatureDatasetName = (obj3 as ITopologyName).FeatureDatasetName;
                            }
                            else if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTRasterBand)
                            {
                                name8 = new RasterBandNameClass();
                                (name8 as IRasterBandName).RasterDatasetName = (obj3 as IRasterBandName).RasterDatasetName;
                            }
                            else if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTGeometricNetwork)
                            {
                                name8 = new GeometricNetworkNameClass();
                                (name8 as IGeometricNetworkName).FeatureDatasetName = (obj3 as IGeometricNetworkName).FeatureDatasetName;
                            }
                            else if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTRasterCatalog)
                            {
                                name8 = new RasterCatalogNameClass();
                                (name8 as IFeatureClassName).FeatureDatasetName = (obj3 as IFeatureClassName).FeatureDatasetName;
                            }
                            else if ((obj3 as IDatasetName).Type == esriDatasetType.esriDTNetworkDataset)
                            {
                                name8 = new NetworkDatasetNameClass();
                                (name8 as INetworkDatasetName).FeatureDatasetName = (obj3 as INetworkDatasetName).FeatureDatasetName;
                            }
                            if (name8 != null)
                            {
                                if (name8.WorkspaceName == null)
                                {
                                    workspaceName = (obj3 as IDatasetName).WorkspaceName;
                                    name6 = new WorkspaceNameClass {
                                        WorkspaceFactoryProgID = workspaceName.WorkspaceFactoryProgID
                                    };
                                    if (workspaceName.ConnectionProperties != null)
                                    {
                                        name6.ConnectionProperties = workspaceName.ConnectionProperties;
                                    }
                                    else
                                    {
                                        name6.PathName = workspaceName.PathName;
                                    }
                                    name8.WorkspaceName = name6;
                                }
                                name8.Name = (obj3 as IDatasetName).Name;
                                this.CreateLayer(name8 as IName);
                            }
                        }
                        else if (obj3 is IWorkspaceName)
                        {
                            workspaceName = obj3 as IWorkspaceName;
                            name6 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = workspaceName.WorkspaceFactoryProgID
                            };
                            if (workspaceName.ConnectionProperties != null)
                            {
                                name6.ConnectionProperties = workspaceName.ConnectionProperties;
                            }
                            else
                            {
                                name6.PathName = workspaceName.PathName;
                            }
                            this.CreateLayer(name6 as IName);
                        }
                        else if (obj3 is IPropertySet)
                        {
                            IPropertySet set = obj3 as IPropertySet;
                            ILayer property = set.GetProperty("Layer") as ILayer;
                            int toIndex = 0;
                            toIndex = LocalCommonHelper.GetLayerIndex(this.axMapControl1.Map, property);
                            this.axMapControl1.AddLayer(property, toIndex);
                        }
                    }
                    if (ApplicationRef.Application != null)
                    {
                        ApplicationRef.Application.MapDocumentChanged();
                    }
                }
            }
        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag is ICommand)
            {
                ICommand tag = e.Item.Tag as ICommand;
                tag.OnClick();
                if (tag is ITool)
                {
                    this.axMapControl1.CurrentTool = tag as ITool;
                }
                ApplicationRef.Application.UpdateUI();
            }
        }

        private void CreateLayer(IName name)
        {
            ILayerFactoryHelper helper = new LayerFactoryHelperClass();
            try
            {
                IEnumLayer layer = helper.CreateLayersFromName(name);
                layer.Reset();
                for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
                {
                    int toIndex = 0;
                    toIndex = LocalCommonHelper.GetLayerIndex(this.axMapControl1.Map, layer2);
                    this.axMapControl1.AddLayer(layer2, toIndex);
                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, layer2, null);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("无法打开指定文档!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DataEditControl_OnUpdateUIEvent()
        {
            try
            {
                int num;
                if (this.toolStrip1.Visible)
                {
                    for (num = 0; num < this.toolStrip1.Items.Count; num++)
                    {
                        ICommand tag = this.toolStrip1.Items[num].Tag as ICommand;
                        if (tag != null)
                        {
                            this.toolStrip1.Items[num].Enabled = tag.Enabled;
                        }
                        if (this.axMapControl1.CurrentTool == tag)
                        {
                            (this.toolStrip1.Items[num] as ToolStripButton).Checked = true;
                            (this.toolStrip1.Items[num] as ToolStripButton).CheckState = CheckState.Checked;
                        }
                        else if (tag.Checked)
                        {
                            (this.toolStrip1.Items[num] as ToolStripButton).Checked = true;
                            (this.toolStrip1.Items[num] as ToolStripButton).CheckState = CheckState.Checked;
                        }
                        else
                        {
                            (this.toolStrip1.Items[num] as ToolStripButton).Checked = false;
                            (this.toolStrip1.Items[num] as ToolStripButton).CheckState = CheckState.Unchecked;
                        }
                    }
                }
                else if (this.bar2.Visible)
                {
                    for (num = 0; num < this.barManager1.Items.Count; num++)
                    {
                        BarItem item = this.barManager1.Items[num];
                        if (item.Tag is ICommand)
                        {
                            ICommand command2 = item.Tag as ICommand;
                            try
                            {
                                item.Enabled = command2.Enabled;
                            }
                            catch
                            {
                            }
                            BarButtonItem item2 = item as BarButtonItem;
                            if (this.axMapControl1.CurrentTool == command2)
                            {
                                item2.ButtonStyle = BarButtonStyle.Check;
                                item2.Down = true;
                            }
                            else if (command2.Checked)
                            {
                                item2.ButtonStyle = BarButtonStyle.Check;
                                item2.Down = true;
                            }
                            else
                            {
                                item2.ButtonStyle = BarButtonStyle.Default;
                                item2.Down = false;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmMap_Load(object sender, EventArgs e)
        {
            (ApplicationRef.Application as IApplicationEvents).OnUpdateUIEvent += new OnUpdateUIEventHandler(this.DataEditControl_OnUpdateUIEvent);
            this.InitToolsBar();
            if (this.m_IsRegisterAsDocument)
            {
                DocumentManager.Register(this.axMapControl1.Object);
            }
            this.axMapControl1.OleDropEnabled = true;
            this.axMapControl1.ShowMapTips = true;
        }

        private void frmMap_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void frmMap_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMap));
            this.toolStrip1 = new ToolStrip();
            this.axMapControl1 = new AxMapControl();
            this.barManager1 = new BarManager(this.components);
            this.bar2 = new Bar();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.axMapControl1.BeginInit();
            this.barManager1.BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x124, 0x19);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new Point(0, 0x18);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(0x124, 0xf9);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMapControl1.OnOleDrop += new IMapControlEvents2_Ax_OnOleDropEventHandler(this.axMapControl1_OnOleDrop);
            this.barManager1.Bars.AddRange(new Bar[] { this.bar2 });
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 0;
            this.barManager1.ItemClick += new ItemClickEventHandler(this.barManager1_ItemClick);
            this.bar2.BarName = "主菜单";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            this.bar2.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.axMapControl1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Controls.Add(this.toolStrip1);
            base.DockAreas = DockAreas.Document;
            base.Name = "frmMap";
            base.TabText = "地图视图";
            this.Text = "地图视图";
            base.Load += new EventHandler(this.frmMap_Load);
            base.ResizeBegin += new EventHandler(this.frmMap_ResizeBegin);
            base.ResizeEnd += new EventHandler(this.frmMap_ResizeEnd);
            this.axMapControl1.EndInit();
            this.barManager1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void InitPopuMenu(string xml)
        {
        }

        public void InitToolsBar()
        {
            if (ApplicationRef.Application.MapCommands.Count > 0)
            {
                if (ApplicationRef.ControlType == 0)
                {
                    this.toolStrip1.Visible = true;
                    foreach (object obj2 in ApplicationRef.Application.MapCommands)
                    {
                        ToolStripItem item = CommonHelper.CreateBarItem(obj2 as ICommand);
                        this.toolStrip1.Items.Add(item);
                    }
                }
                else
                {
                    this.bar2.Visible = true;
                    foreach (object obj2 in ApplicationRef.Application.MapCommands)
                    {
                        //BarButtonItem item2 = CommonHelper.CreateYTBarItem(this.barManager1, obj2 as ICommand);
                        //this.bar2.AddItem(item2);
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            IMap key = this.axMapControl1.Map;
            if ((Yutai.ArcGIS.Common.Editor.Editor.EditMap == key) && !Yutai.ArcGIS.Common.Editor.Editor.StopEditing())
            {
                e.Cancel = true;
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = DocumentManager.UnRegister(this.axMapControl1.Object);
                if (!e.Cancel && EditorTemplateManageCtrl.EditMap.ContainsKey(key))
                {
                    EditorTemplateManageCtrl.EditMap.Remove(key);
                }
                base.OnClosing(e);
            }
        }

        protected override void OnNotifyMessage(System.Windows.Forms.Message m)
        {
            base.OnNotifyMessage(m);
            if ((m.Msg != 0x231) && (m.Msg == 0x232))
            {
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag is ICommand)
            {
                ICommand tag = e.ClickedItem.Tag as ICommand;
                tag.OnClick();
                if (tag is ITool)
                {
                    this.axMapControl1.CurrentTool = tag as ITool;
                }
                ApplicationRef.Application.UpdateUI();
            }
        }

        public string GHFAGuid { get; set; }

        public bool IsPopMenu { get; set; }

        public bool IsRegisterAsDocument
        {
            set
            {
                this.m_IsRegisterAsDocument = value;
            }
        }

        public AxMapControl MapControl
        {
            get
            {
                return this.axMapControl1;
            }
        }

        public string TJID { get; set; }
    }
}

