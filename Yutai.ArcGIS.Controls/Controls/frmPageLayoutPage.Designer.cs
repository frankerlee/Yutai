using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmPageLayoutPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageLayoutPage));
            this.axPageLayoutControl1 = new AxPageLayoutControl();
            this.axPageLayoutControl1.BeginInit();
            base.SuspendLayout();
            this.axPageLayoutControl1.Dock = DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl1.OcxState");
            this.axPageLayoutControl1.Size = new Size(292, 266);
            this.axPageLayoutControl1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 266);
            base.Controls.Add(this.axPageLayoutControl1);
            base.Name = "frmPageLayoutPage";
            base.TabText = "打印视图";
            this.Text = "打印视图";
            base.Load += new EventHandler(this.frmPageLayoutPage_Load);
            this.axPageLayoutControl1.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
    }
}