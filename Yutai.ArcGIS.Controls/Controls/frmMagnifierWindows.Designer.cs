using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmMagnifierWindows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMagnifierWindows));
            this.toolStrip1 = new ToolStrip();
            this.btnFixedZoomIn = new ToolStripButton();
            this.btnFixedZoomOut = new ToolStripButton();
            this.btnFullExtend = new ToolStripButton();
            this.btnLastExtend = new ToolStripButton();
            this.btnNextExtend = new ToolStripButton();
            this.cboMapScale = new ToolStripComboBox();
            this.toolStripDropDownButton1 = new ToolStripDropDownButton();
            this.mnuProperty = new ToolStripMenuItem();
            this.axMapControl1 = new AxMapControl();
            this.toolStrip1.SuspendLayout();
            this.axMapControl1.BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.btnFixedZoomIn, this.btnFixedZoomOut, this.btnFullExtend, this.btnLastExtend, this.btnNextExtend, this.cboMapScale, this.toolStripDropDownButton1 });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(292, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            this.btnFixedZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomIn.Image = (Image) resources.GetObject("btnFixedZoomIn.Image");
            this.btnFixedZoomIn.ImageTransparentColor = Color.Magenta;
            this.btnFixedZoomIn.Name = "btnFixedZoomIn";
            this.btnFixedZoomIn.Size = new Size(23, 22);
            this.btnFixedZoomIn.Text = "toolStripButton1";
            this.btnFixedZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomOut.Image = (Image) resources.GetObject("btnFixedZoomOut.Image");
            this.btnFixedZoomOut.ImageTransparentColor = Color.Magenta;
            this.btnFixedZoomOut.Name = "btnFixedZoomOut";
            this.btnFixedZoomOut.Size = new Size(23, 22);
            this.btnFixedZoomOut.Text = "toolStripButton2";
            this.btnFullExtend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFullExtend.Image = (Image) resources.GetObject("btnFullExtend.Image");
            this.btnFullExtend.ImageTransparentColor = Color.Magenta;
            this.btnFullExtend.Name = "btnFullExtend";
            this.btnFullExtend.Size = new Size(23, 22);
            this.btnFullExtend.Text = "toolStripButton3";
            this.btnLastExtend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnLastExtend.Image = (Image) resources.GetObject("btnLastExtend.Image");
            this.btnLastExtend.ImageTransparentColor = Color.Magenta;
            this.btnLastExtend.Name = "btnLastExtend";
            this.btnLastExtend.Size = new Size(23, 22);
            this.btnLastExtend.Text = "toolStripButton4";
            this.btnNextExtend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnNextExtend.Image = (Image) resources.GetObject("btnNextExtend.Image");
            this.btnNextExtend.ImageTransparentColor = Color.Magenta;
            this.btnNextExtend.Name = "btnNextExtend";
            this.btnNextExtend.Size = new Size(23, 22);
            this.btnNextExtend.Text = "toolStripButton5";
            this.cboMapScale.Name = "cboMapScale";
            this.cboMapScale.Size = new Size(121, 25);
            this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { this.mnuProperty });
            this.toolStripDropDownButton1.Image = (Image) resources.GetObject("toolStripDropDownButton1.Image");
            this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.mnuProperty.Name = "mnuProperty";
            this.mnuProperty.Size = new Size(94, 22);
            this.mnuProperty.Text = "属性";
            this.mnuProperty.Click += new EventHandler(this.mnuProperty_Click);
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 25);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(292, 248);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.MouseCaptureChanged += new EventHandler(this.axMapControl1_MouseCaptureChanged);
            this.axMapControl1.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 273);
            base.Controls.Add(this.axMapControl1);
            base.Controls.Add(this.toolStrip1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            base.Name = "frmMagnifierWindows";
            this.Text = "frmMagnifierWindows";
            base.TopMost = true;
            base.Load += new EventHandler(this.frmMagnifierWindows_Load);
            base.SizeChanged += new EventHandler(this.frmMagnifierWindows_SizeChanged);
            base.Shown += new EventHandler(this.frmMagnifierWindows_Shown);
            base.VisibleChanged += new EventHandler(this.frmMagnifierWindows_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.axMapControl1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private AxMapControl axMapControl1;
        private ToolStripButton btnFixedZoomIn;
        private ToolStripButton btnFixedZoomOut;
        private ToolStripButton btnFullExtend;
        private ToolStripButton btnLastExtend;
        private ToolStripButton btnNextExtend;
        private ToolStripComboBox cboMapScale;
        private ToolStripMenuItem mnuProperty;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
    }
}