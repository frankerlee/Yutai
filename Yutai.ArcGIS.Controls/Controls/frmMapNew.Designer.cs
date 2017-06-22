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
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmMapNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapNew));
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
            this.toolStrip1.Size = new Size(292, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new Point(0, 24);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(292, 249);
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
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.ClientSize = new Size(292, 273);
            base.Controls.Add(this.axMapControl1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Controls.Add(this.toolStrip1);
            base.Name = "frmMap";
            this.Text = "地图视图";
            base.Load += new EventHandler(this.frmMap_Load);
            base.ResizeBegin += new EventHandler(this.frmMap_ResizeBegin);
            base.ResizeEnd += new EventHandler(this.frmMap_ResizeEnd);
            this.axMapControl1.EndInit();
            this.barManager1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private Bar bar2;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private esriControlsDragDropEffect m_Effect;
        private ToolStrip toolStrip1;
    }
}