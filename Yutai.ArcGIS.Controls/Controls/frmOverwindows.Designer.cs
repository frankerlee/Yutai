using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Overview;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmOverwindows
    {
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
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOverwindows));
            this.axMapControl1 = new AxMapControl();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.属性ToolStripMenuItem = new ToolStripMenuItem();
            this.axMapControl1.BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(292, 273);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnFullExtentUpdated += new IMapControlEvents2_Ax_OnFullExtentUpdatedEventHandler(this.axMapControl1_OnFullExtentUpdated);
            this.axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.属性ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(95, 26);
            this.属性ToolStripMenuItem.Name = "属性ToolStripMenuItem";
            this.属性ToolStripMenuItem.Size = new Size(94, 22);
            this.属性ToolStripMenuItem.Text = "属性";
            this.属性ToolStripMenuItem.Click += new EventHandler(this.属性ToolStripMenuItem_Click);
            base.ClientSize = new Size(292, 273);
            base.Controls.Add(this.axMapControl1);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            
            base.Name = "frmOverwindows";
            base.ShowHint = DockState.DockBottom;
            base.TabText = "鹰眼视图";
            this.Text = "鹰眼视图";
            base.Load += new EventHandler(this.frmOverwindows_Load);
            this.axMapControl1.EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private AxMapControl axMapControl1;
        private ContextMenuStrip contextMenuStrip1;
        private IFillSymbol m_pFillSymbol;
        private ToolStripMenuItem 属性ToolStripMenuItem;
    }
}