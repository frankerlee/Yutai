using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Framework.Docking;
using Editor2=Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class MapFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapFrame));
            this.axMapControl = new AxMapControl();
            this.axPageLayoutControl = new AxPageLayoutControl();
            base.SuspendLayout();
            this.axMapControl.Dock = DockStyle.Fill;
            this.axMapControl.Location = new Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = (AxHost.State) resources.GetObject("axMapControl.OcxState");
            this.axMapControl.Size = new Size(292, 273);
            this.axMapControl.TabIndex = 13;
            this.axPageLayoutControl.Dock = DockStyle.Fill;
            this.axPageLayoutControl.Location = new Point(0, 0);
            this.axPageLayoutControl.Name = "axPageLayoutControl";
            this.axPageLayoutControl.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl.OcxState");
            this.axPageLayoutControl.Size = new Size(292, 273);
            this.axPageLayoutControl.TabIndex = 12;
            base.DockAreas = DockAreas.Document;
            base.ShowHint = DockState.Document;
            base.TabText = "地图视图";
            this.Text = "地图视图";
            base.ClientSize = new Size(292, 273);
            base.Controls.Add(this.axMapControl);
            base.Controls.Add(this.axPageLayoutControl);
            base.Name = "MapFrame";
            base.Load += new EventHandler(this.MapFrame_Load);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
    }
}