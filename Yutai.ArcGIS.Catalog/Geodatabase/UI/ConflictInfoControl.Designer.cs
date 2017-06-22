using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ConflictInfoControl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.splitter1 = new Splitter();
            this.treeView1 = new TreeView();
            this.panel2 = new Panel();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.popupMenu1 = new PopupMenu();
            this.ReplaceConflictVersion = new BarButtonItem();
            this.ReplaceEditVersion = new BarButtonItem();
            this.RelpaceStartEditVersion = new BarButtonItem();
            this.ZoomToConflictVersion = new BarButtonItem();
            this.ZoomToEditVersion = new BarButtonItem();
            this.ZoomToStartEditVersion = new BarButtonItem();
            this.ShowSetup = new BarButtonItem();
            this.barManager_0 = new BarManager();
            this.barDockControl_0 = new BarDockControl();
            this.barDockControl_1 = new BarDockControl();
            this.barDockControl_2 = new BarDockControl();
            this.barDockControl_3 = new BarDockControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.popupMenu1.BeginInit();
            this.barManager_0.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(400, 138);
            this.panel1.TabIndex = 0;
            this.splitter1.Dock = DockStyle.Bottom;
            this.splitter1.Location = new Point(0, 136);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new Size(400, 2);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new SplitterEventHandler(this.splitter1_SplitterMoved);
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(400, 138);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseUp += new MouseEventHandler(this.treeView1_MouseUp);
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(400, 150);
            this.panel2.TabIndex = 1;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3 });
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.Location = new Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(400, 150);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "属性";
            this.columnHeader_0.Width = 70;
            this.columnHeader_1.Text = "冲突";
            this.columnHeader_1.Width = 90;
            this.columnHeader_2.Text = "编辑";
            this.columnHeader_2.Width = 93;
            this.columnHeader_3.Text = "编辑前";
            this.columnHeader_3.Width = 102;
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.ReplaceConflictVersion), new LinkPersistInfo(this.ReplaceEditVersion), new LinkPersistInfo(this.RelpaceStartEditVersion), new LinkPersistInfo(this.ZoomToConflictVersion, true), new LinkPersistInfo(this.ZoomToEditVersion), new LinkPersistInfo(this.ZoomToStartEditVersion), new LinkPersistInfo(this.ShowSetup, true) });
            this.popupMenu1.Manager = this.barManager_0;
            this.popupMenu1.Name = "popupMenu1";
            this.ReplaceConflictVersion.Caption = "替换为冲突版本";
            this.ReplaceConflictVersion.Id = 0;
            this.ReplaceConflictVersion.Name = "ReplaceConflictVersion";
            this.ReplaceConflictVersion.ItemClick += new ItemClickEventHandler(this.ReplaceConflictVersion_ItemClick);
            this.ReplaceEditVersion.Caption = "替换为编辑版本";
            this.ReplaceEditVersion.Id = 1;
            this.ReplaceEditVersion.Name = "ReplaceEditVersion";
            this.ReplaceEditVersion.ItemClick += new ItemClickEventHandler(this.ReplaceEditVersion_ItemClick);
            this.RelpaceStartEditVersion.Caption = "替换为编辑前版本";
            this.RelpaceStartEditVersion.Id = 2;
            this.RelpaceStartEditVersion.Name = "RelpaceStartEditVersion";
            this.RelpaceStartEditVersion.ItemClick += new ItemClickEventHandler(this.RelpaceStartEditVersion_ItemClick);
            this.ZoomToConflictVersion.Caption = "缩放到冲突版本";
            this.ZoomToConflictVersion.Id = 3;
            this.ZoomToConflictVersion.Name = "ZoomToConflictVersion";
            this.ZoomToConflictVersion.ItemClick += new ItemClickEventHandler(this.ZoomToConflictVersion_ItemClick);
            this.ZoomToEditVersion.Caption = "缩放到编辑版本";
            this.ZoomToEditVersion.Id = 4;
            this.ZoomToEditVersion.Name = "ZoomToEditVersion";
            this.ZoomToEditVersion.ItemClick += new ItemClickEventHandler(this.ZoomToEditVersion_ItemClick);
            this.ZoomToStartEditVersion.Caption = "缩放到编辑前版本";
            this.ZoomToStartEditVersion.Id = 5;
            this.ZoomToStartEditVersion.Name = "ZoomToStartEditVersion";
            this.ZoomToStartEditVersion.ItemClick += new ItemClickEventHandler(this.ZoomToStartEditVersion_ItemClick);
            this.ShowSetup.Caption = "显示...";
            this.ShowSetup.Id = 6;
            this.ShowSetup.Name = "ShowSetup";
            this.ShowSetup.ItemClick += new ItemClickEventHandler(this.ShowSetup_ItemClick);
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[] { this.ReplaceConflictVersion, this.ReplaceEditVersion, this.RelpaceStartEditVersion, this.ZoomToConflictVersion, this.ZoomToEditVersion, this.ZoomToStartEditVersion });
            this.barManager_0.MaxItemId = 7;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            base.Name = "ConflictInfoControl";
            base.Size = new Size(400, 288);
            base.Load += new EventHandler(this.ConflictInfoControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.popupMenu1.EndInit();
            this.barManager_0.EndInit();
            base.ResumeLayout(false);
        }

       
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private BarManager barManager_0;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private IFeatureWorkspace ifeatureWorkspace_0;
        private ListView listView1;
        private Panel panel1;
        private Panel panel2;
        private PopupMenu popupMenu1;
        private BarButtonItem RelpaceStartEditVersion;
        private BarButtonItem ReplaceConflictVersion;
        private BarButtonItem ReplaceEditVersion;
        private BarButtonItem ShowSetup;
        private Splitter splitter1;
        private TreeView treeView1;
        private BarButtonItem ZoomToConflictVersion;
        private BarButtonItem ZoomToEditVersion;
        private BarButtonItem ZoomToStartEditVersion;
    }
}