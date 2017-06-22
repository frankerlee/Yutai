using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Overview;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class OverviewWindowsEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewWindowsEx));
            this.axMapControl1 = new AxMapControl();
            this.barManager1 = new BarManager(this.components);
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.UpdateView = new BarButtonItem();
            this.OverviewProperty = new BarButtonItem();
            this.popupMenu1 = new PopupMenu(this.components);
            this.axMapControl1.BeginInit();
            this.barManager1.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(150, 150);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnAfterDraw += new IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.axMapControl1_OnAfterDraw);
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.UpdateView, this.OverviewProperty });
            this.barManager1.MaxItemId = 2;
            this.UpdateView.Caption = "更新显示";
            this.UpdateView.Id = 0;
            this.UpdateView.Name = "UpdateView";
            this.OverviewProperty.Caption = "属性";
            this.OverviewProperty.Id = 1;
            this.OverviewProperty.Name = "OverviewProperty";
            this.OverviewProperty.ItemClick += new ItemClickEventHandler(this.OverviewProperty_ItemClick);
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.OverviewProperty) });
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.axMapControl1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "OverviewWindowsEx";
            base.Load += new EventHandler(this.OverviewWindowsEx_Load);
            base.SizeChanged += new EventHandler(this.OverviewWindowsEx_SizeChanged);
            this.axMapControl1.EndInit();
            this.barManager1.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private AxMapControl axMapControl1;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private IFillSymbol m_pFillSymbol;
        private BarButtonItem OverviewProperty;
        private PopupMenu popupMenu1;
        private BarButtonItem UpdateView;
    }
}