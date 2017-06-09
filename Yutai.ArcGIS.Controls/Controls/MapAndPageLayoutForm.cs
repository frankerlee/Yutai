using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Framework.Docking;
using Editor2=Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class MapAndPageLayoutForm : DockContent
    {
        private AxMapControl axMapControl;
        private AxPageLayoutControl axPageLayoutControl;
        private IContainer components = null;
        private bool m_IsMapCtrlactive = true;
        private ITool m_mapActiveTool = null;
        private ITool m_pageLayoutActiveTool = null;
        private Panel panel1;
        private Panel panel2;

        public MapAndPageLayoutForm()
        {
            this.InitializeComponent();
            this.axPageLayoutControl.OnPageLayoutReplaced += new IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl_OnPageLayoutReplaced);
        }

        public void ActivateMap()
        {
            try
            {
                if ((this.axPageLayoutControl == null) || (this.axMapControl == null))
                {
                    throw new Exception("ControlsSynchronizer::ActivateMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
                }
                this.panel1.Visible = true;
                this.panel2.Visible = false;
                if (this.axPageLayoutControl.CurrentTool != null)
                {
                    this.m_pageLayoutActiveTool = this.axPageLayoutControl.CurrentTool;
                }
                this.axPageLayoutControl.ActiveView.Deactivate();
                this.axMapControl.ActiveView.Activate(this.axMapControl.hWnd);
                if (this.m_mapActiveTool != null)
                {
                    this.axMapControl.CurrentTool = this.m_mapActiveTool;
                }
                this.m_IsMapCtrlactive = true;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivateMap:\r\n{0}", exception.Message));
            }
        }

        public void ActivatePageLayout()
        {
            try
            {
                if ((this.axPageLayoutControl == null) || (this.axMapControl == null))
                {
                    throw new Exception("ControlsSynchronizer::ActivatePageLayout:\r\nEither MapControl or PageLayoutControl are not initialized!");
                }
                this.panel2.Visible = true;
                this.panel1.Visible = false;
                if (this.axMapControl.CurrentTool != null)
                {
                    this.m_mapActiveTool = this.axMapControl.CurrentTool;
                }
                this.axMapControl.ActiveView.Deactivate();
                this.axPageLayoutControl.ActiveView.Activate(this.axPageLayoutControl.hWnd);
                if (this.m_pageLayoutActiveTool != null)
                {
                    this.axPageLayoutControl.CurrentTool = this.m_pageLayoutActiveTool;
                }
                this.m_IsMapCtrlactive = false;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", exception.Message));
            }
        }

        private void axMapControl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if ((e.button == 2) && (this.axMapControl.ContextMenuStrip != null))
            {
                this.axMapControl.ContextMenuStrip.Show(this.axMapControl, e.x, e.y);
            }
        }

        private void axPageLayoutControl_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {
            IMap focusMap = this.axPageLayoutControl.ActiveView.FocusMap;
            this.axMapControl.Map = focusMap;
            if (this.m_IsMapCtrlactive)
            {
                this.ActivateMap();
                this.axMapControl.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                this.axPageLayoutControl.ActiveView.Refresh();
            }
        }

        public void BindControls(bool activateMapFirst)
        {
            if ((this.axPageLayoutControl == null) || (this.axMapControl == null))
            {
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");
            }
            IMap map = new MapClass {
                Name = "Map"
            };
            IMaps maps = new Maps();
            maps.Add(map);
            this.axPageLayoutControl.PageLayout.ReplaceMaps(maps);
            this.axMapControl.Map = map;
            this.m_pageLayoutActiveTool = null;
            this.m_mapActiveTool = null;
            if (activateMapFirst)
            {
                this.ActivateMap();
            }
            else
            {
                this.ActivatePageLayout();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            DocumentManager.Register(this.axPageLayoutControl.Object);
            this.axMapControl.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl_OnMouseUp);
            this.axMapControl.ShowMapTips = true;
            this.BindControls(true);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapAndPageLayoutForm));
            this.panel1 = new Panel();
            this.axMapControl = new AxMapControl();
            this.panel2 = new Panel();
            this.axPageLayoutControl = new AxPageLayoutControl();
            this.panel1.SuspendLayout();
            this.axMapControl.BeginInit();
            this.panel2.SuspendLayout();
            this.axPageLayoutControl.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.axMapControl);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x124, 0x10f);
            this.panel1.TabIndex = 0x15;
            this.axMapControl.Dock = DockStyle.Fill;
            this.axMapControl.Location = new Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = (AxHost.State) resources.GetObject("axMapControl.OcxState");
            this.axMapControl.Size = new Size(0x124, 0x10f);
            this.axMapControl.TabIndex = 20;
            this.panel2.Controls.Add(this.axPageLayoutControl);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x124, 0x10f);
            this.panel2.TabIndex = 0x16;
            this.axPageLayoutControl.Dock = DockStyle.Fill;
            this.axPageLayoutControl.Location = new Point(0, 0);
            this.axPageLayoutControl.Name = "axPageLayoutControl";
            this.axPageLayoutControl.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl.OcxState");
            this.axPageLayoutControl.Size = new Size(0x124, 0x10f);
            this.axPageLayoutControl.TabIndex = 0x13;
            base.ClientSize = new Size(0x124, 0x10f);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.DockAreas = DockAreas.Document;
            base.Name = "MapAndPageLayoutForm";
            base.ShowHint = DockState.Document;
            base.TabText = "地图视图";
            this.Text = "地图视图";
            base.Load += new EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.axMapControl.EndInit();
            this.panel2.ResumeLayout(false);
            this.axPageLayoutControl.EndInit();
            base.ResumeLayout(false);
        }

        public void NewDocument()
        {
            (this.axPageLayoutControl.PageLayout as IGraphicsContainer).DeleteAllElements();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            IMap map = this.axMapControl.Map;
            if (Editor2.Editor.EditMap == map)
            {
                bool hasEdits = false;
                Editor2.Editor.EditWorkspace.HasEdits(ref hasEdits);
                if (hasEdits)
                {
                    DialogResult result = MessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Editor2.Editor.EditWorkspace.StopEditing(true);
                        Editor2.Editor.EditWorkspace = null;
                        Editor2.Editor.EditMap = null;
                    }
                    else
                    {
                        if (result == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            base.OnClosing(e);
                            return;
                        }
                        Editor2.Editor.EditWorkspace.StopEditing(false);
                        Editor2.Editor.EditWorkspace = null;
                        Editor2.Editor.EditMap = null;
                    }
                }
                else
                {
                    Editor2.Editor.EditWorkspace.StopEditing(false);
                    Editor2.Editor.EditWorkspace = null;
                    Editor2.Editor.EditMap = null;
                }
            }
            if (DocumentManager.UnRegister(this.axPageLayoutControl.Object))
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        public void ReplacePageLayout(IPageLayout newMap)
        {
            if (newMap == null)
            {
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");
            }
            if ((this.axPageLayoutControl == null) || (this.axMapControl == null))
            {
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
            }
            bool isMapCtrlactive = this.m_IsMapCtrlactive;
            this.ActivatePageLayout();
            this.axPageLayoutControl.PageLayout = newMap;
            this.axMapControl.Map = (newMap as IActiveView).FocusMap;
            this.m_pageLayoutActiveTool = null;
            this.m_mapActiveTool = null;
            if (isMapCtrlactive)
            {
                this.ActivateMap();
                this.axMapControl.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                this.axPageLayoutControl.ActiveView.Refresh();
            }
        }

        public bool IsMapCtrlactive
        {
            get
            {
                return this.m_IsMapCtrlactive;
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

