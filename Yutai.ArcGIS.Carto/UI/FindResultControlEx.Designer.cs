using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.Shared;


namespace Yutai.ArcGIS.Carto.UI
{
    partial class FindResultControlEx
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.panel2 = new Panel();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.barManager_0 = new BarManager();
            this.barDockControl_0 = new BarDockControl();
            this.barDockControl_1 = new BarDockControl();
            this.barDockControl_2 = new BarDockControl();
            this.barDockControl_3 = new BarDockControl();
            this.FlashFeature = new BarButtonItem();
            this.ZoomToFeature = new BarButtonItem();
            this.SelectFeature = new BarButtonItem();
            this.UnSelectFeature = new BarButtonItem();
            this.Identify = new BarButtonItem();
            this.popupMenu1 = new PopupMenu();
            this.barAndDockingController_0 = new BarAndDockingController(this.icontainer_0);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.barManager_0.BeginInit();
            this.popupMenu1.BeginInit();
            this.barAndDockingController_0.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(472, 26);
            this.panel1.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 17);
            this.label1.TabIndex = 0;
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(472, 126);
            this.panel2.TabIndex = 1;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(472, 126);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp);
            this.columnHeader_0.Text = "值";
            this.columnHeader_0.Width = 123;
            this.columnHeader_1.Text = "图层";
            this.columnHeader_1.Width = 113;
            this.columnHeader_2.Text = "字段";
            this.columnHeader_2.Width = 188;
            this.barManager_0.Controller = this.barAndDockingController_0;
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[] { this.FlashFeature, this.ZoomToFeature, this.SelectFeature, this.UnSelectFeature, this.Identify });
            this.barManager_0.MaxItemId = 5;
            this.FlashFeature.Caption = "闪烁要素";
            this.FlashFeature.Id = 0;
            this.FlashFeature.Name = "FlashFeature";
            this.FlashFeature.ItemClick += new ItemClickEventHandler(this.FlashFeature_ItemClick);
            this.ZoomToFeature.Caption = "缩放到要素";
            this.ZoomToFeature.Id = 1;
            this.ZoomToFeature.Name = "ZoomToFeature";
            this.ZoomToFeature.ItemClick += new ItemClickEventHandler(this.ZoomToFeature_ItemClick);
            this.SelectFeature.Caption = "选择要素";
            this.SelectFeature.Id = 2;
            this.SelectFeature.Name = "SelectFeature";
            this.SelectFeature.ItemClick += new ItemClickEventHandler(this.SelectFeature_ItemClick);
            this.UnSelectFeature.Caption = "取消选择要素";
            this.UnSelectFeature.Id = 3;
            this.UnSelectFeature.Name = "UnSelectFeature";
            this.UnSelectFeature.ItemClick += new ItemClickEventHandler(this.UnSelectFeature_ItemClick);
            this.Identify.Caption = "查看要素";
            this.Identify.Id = 4;
            this.Identify.Name = "Identify";
            this.Identify.ItemClick += new ItemClickEventHandler(this.Identify_ItemClick);
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.FlashFeature), new LinkPersistInfo(this.ZoomToFeature), new LinkPersistInfo(this.SelectFeature), new LinkPersistInfo(this.UnSelectFeature), new LinkPersistInfo(this.Identify) });
            this.popupMenu1.Manager = this.barManager_0;
            this.popupMenu1.Name = "popupMenu1";
            this.barAndDockingController_0.PaintStyleName = "Office2003";
            this.barAndDockingController_0.PropertiesBar.AllowLinkLighting = false;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            base.Name = "FindResultControlEx";
            base.Size = new Size(472, 152);
            base.Load += new EventHandler(this.FindResultControlEx_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.barManager_0.EndInit();
            this.popupMenu1.EndInit();
            this.barAndDockingController_0.EndInit();
            base.ResumeLayout(false);
        }

       
        private BarAndDockingController barAndDockingController_0;
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private BarManager barManager_0;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private BarButtonItem FlashFeature;
        private IContainer icontainer_0;
        private BarButtonItem Identify;
        private Label label1;
        private ListView listView1;
        private Panel panel1;
        private Panel panel2;
        private PopupMenu popupMenu1;
        private BarButtonItem SelectFeature;
        private BarButtonItem UnSelectFeature;
        private BarButtonItem ZoomToFeature;
    }
}