using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Controls.Controls
{
    internal class MapFrame : DockContent
    {
        public AxMapControl axMapControl;
        public AxPageLayoutControl axPageLayoutControl;
        private Container components = null;
        private const int WM_ENTERSIZEMOVE = 0x231;
        private const int WM_EXITSIZEMOVE = 0x232;

        public MapFrame()
        {
            this.InitializeComponent();
            this.axPageLayoutControl.OnPageLayoutReplaced += new IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl_OnPageLayoutReplaced);
        }

        private void axPageLayoutControl_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {
            int num;
            IMap focusMap = this.axPageLayoutControl.ActiveView.FocusMap;
            this.axMapControl.Map.ClearLayers();
            (this.axMapControl.Map as IGraphicsContainer).DeleteAllElements();
            (this.axMapControl.Map as ITableCollection).RemoveAllTables();
            this.axMapControl.Map.ClearLayers();
            (this.axMapControl.Map as IActiveView).ContentsChanged();
            this.axMapControl.Map.MapUnits = focusMap.MapUnits;
            this.axMapControl.Map.SpatialReferenceLocked = false;
            this.axMapControl.Map.SpatialReference = focusMap.SpatialReference;
            this.axMapControl.Map.Name = focusMap.Name;
            for (num = 0; num < focusMap.LayerCount; num++)
            {
                ILayer layer = focusMap.get_Layer(num);
                this.axMapControl.AddLayer(layer, num);
            }
            IGraphicsContainer container = focusMap as IGraphicsContainer;
            container.Reset();
            IElement element = container.Next();
            int zorder = 0;
            while (element != null)
            {
                (this.axMapControl.Map as IGraphicsContainer).AddElement(element, zorder);
                zorder++;
                element = container.Next();
            }
            ITableCollection tables = focusMap as ITableCollection;
            for (num = 0; num < tables.TableCount; num++)
            {
                (this.axMapControl.Map as ITableCollection).AddTable(tables.get_Table(num));
            }
            this.axMapControl.ActiveView.Extent = (focusMap as IActiveView).Extent;
            this.axMapControl.ActiveView.Refresh();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapFrame));
            this.axMapControl = new AxMapControl();
            this.axPageLayoutControl = new AxPageLayoutControl();
            base.SuspendLayout();
            this.axMapControl.Dock = DockStyle.Fill;
            this.axMapControl.Location = new Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = (AxHost.State) resources.GetObject("axMapControl.OcxState");
            this.axMapControl.Size = new Size(0x124, 0x111);
            this.axMapControl.TabIndex = 13;
            this.axPageLayoutControl.Dock = DockStyle.Fill;
            this.axPageLayoutControl.Location = new Point(0, 0);
            this.axPageLayoutControl.Name = "axPageLayoutControl";
            this.axPageLayoutControl.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl.OcxState");
            this.axPageLayoutControl.Size = new Size(0x124, 0x111);
            this.axPageLayoutControl.TabIndex = 12;
            base.DockAreas = DockAreas.Document;
            base.ShowHint = DockState.Document;
            base.TabText = "地图视图";
            this.Text = "地图视图";
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.axMapControl);
            base.Controls.Add(this.axPageLayoutControl);
            base.Name = "MapFrame";
            base.Load += new EventHandler(this.MapFrame_Load);
            base.ResumeLayout(false);
        }

        private void MapFrame_Load(object sender, EventArgs e)
        {
            DocumentManager.Register(this.axPageLayoutControl.Object);
            this.axMapControl.ShowMapTips = true;
            this.axPageLayoutControl.Visible = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            IMap map = this.axMapControl.Map;
            if (Editor.Editor.EditMap == map)
            {
                bool hasEdits = false;
                Editor.Editor.EditWorkspace.HasEdits(ref hasEdits);
                if (hasEdits)
                {
                    DialogResult result = MessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Editor.Editor.EditWorkspace.StopEditing(true);
                        Editor.Editor.EditWorkspace = null;
                        Editor.Editor.EditMap = null;
                    }
                    else
                    {
                        if (result == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            base.OnClosing(e);
                            return;
                        }
                        Editor.Editor.EditWorkspace.StopEditing(false);
                        Editor.Editor.EditWorkspace = null;
                        Editor.Editor.EditMap = null;
                    }
                }
                else
                {
                    Editor.Editor.EditWorkspace.StopEditing(false);
                    Editor.Editor.EditWorkspace = null;
                    Editor.Editor.EditMap = null;
                }
            }
            if (DocumentManager.UnRegister(this.axPageLayoutControl.Object))
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        protected override void OnNotifyMessage(Message m)
        {
            base.OnNotifyMessage(m);
            if (m.Msg == 0x231)
            {
                this.axMapControl.SuppressResizeDrawing(true, 0);
                this.axPageLayoutControl.SuppressResizeDrawing(true, 0);
            }
            else if (m.Msg == 0x232)
            {
                this.axMapControl.SuppressResizeDrawing(false, 0);
                this.axPageLayoutControl.SuppressResizeDrawing(false, 0);
            }
        }

        public AxMapControl MapControl
        {
            get
            {
                return this.axMapControl;
            }
            set
            {
                this.axMapControl = value;
            }
        }

        public AxPageLayoutControl PageLayoutControl
        {
            get
            {
                return this.axPageLayoutControl;
            }
            set
            {
                this.axPageLayoutControl = value;
            }
        }
    }
}

