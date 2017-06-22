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
    partial class MapAndPageLayoutForm
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
            this.panel1.Size = new Size(292, 271);
            this.panel1.TabIndex = 21;
            this.axMapControl.Dock = DockStyle.Fill;
            this.axMapControl.Location = new Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = (AxHost.State) resources.GetObject("axMapControl.OcxState");
            this.axMapControl.Size = new Size(292, 271);
            this.axMapControl.TabIndex = 20;
            this.panel2.Controls.Add(this.axPageLayoutControl);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(292, 271);
            this.panel2.TabIndex = 22;
            this.axPageLayoutControl.Dock = DockStyle.Fill;
            this.axPageLayoutControl.Location = new Point(0, 0);
            this.axPageLayoutControl.Name = "axPageLayoutControl";
            this.axPageLayoutControl.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl.OcxState");
            this.axPageLayoutControl.Size = new Size(292, 271);
            this.axPageLayoutControl.TabIndex = 19;
            base.ClientSize = new Size(292, 271);
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

       
        private IContainer components = null;
        private AxMapControl axMapControl;
        private AxPageLayoutControl axPageLayoutControl;
        private Panel panel1;
        private Panel panel2;
    }
}