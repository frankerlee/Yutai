using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class VersionInfoControl
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
            this.VersionInfolist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.barManager_0 = new BarManager();
            this.barDockControl_0 = new BarDockControl();
            this.barDockControl_1 = new BarDockControl();
            this.barDockControl_2 = new BarDockControl();
            this.barDockControl_3 = new BarDockControl();
            this.popupMenu1 = new PopupMenu();
            this.NewVerion = new BarButtonItem();
            this.ReVersionName = new BarButtonItem();
            this.DeleteVersions = new BarButtonItem();
            this.RefreshVersion = new BarButtonItem();
            this.Property = new BarButtonItem();
            this.barManager_0.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.VersionInfolist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_4, this.columnHeader_3 });
            this.VersionInfolist.Dock = DockStyle.Fill;
            this.VersionInfolist.FullRowSelect = true;
            this.VersionInfolist.LabelEdit = true;
            this.VersionInfolist.Location = new Point(0, 0);
            this.VersionInfolist.Name = "VersionInfolist";
            this.VersionInfolist.Size = new Size(416, 200);
            this.VersionInfolist.TabIndex = 0;
            this.VersionInfolist.View = View.Details;
            this.VersionInfolist.MouseUp += new MouseEventHandler(this.VersionInfolist_MouseUp);
            this.VersionInfolist.AfterLabelEdit += new LabelEditEventHandler(this.VersionInfolist_AfterLabelEdit);
            this.VersionInfolist.SelectedIndexChanged += new EventHandler(this.VersionInfolist_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_1.Text = "所有者";
            this.columnHeader_1.Width = 70;
            this.columnHeader_2.Text = "访问方式";
            this.columnHeader_2.Width = 76;
            this.columnHeader_4.Text = "创建日期";
            this.columnHeader_4.Width = 85;
            this.columnHeader_3.Text = "最后修改日期";
            this.columnHeader_3.Width = 118;
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[] { this.NewVerion, this.ReVersionName, this.DeleteVersions, this.RefreshVersion, this.Property });
            this.barManager_0.MaxItemId = 5;
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.NewVerion), new LinkPersistInfo(this.ReVersionName), new LinkPersistInfo(this.DeleteVersions), new LinkPersistInfo(this.RefreshVersion, true), new LinkPersistInfo(this.Property, true) });
            this.popupMenu1.Manager = this.barManager_0;
            this.popupMenu1.Name = "popupMenu1";
            this.NewVerion.Caption = "新建";
            this.NewVerion.Id = 0;
            this.NewVerion.Name = "NewVerion";
            this.NewVerion.ItemClick += new ItemClickEventHandler(this.NewVerion_ItemClick);
            this.ReVersionName.Caption = "重命名";
            this.ReVersionName.Id = 1;
            this.ReVersionName.Name = "ReVersionName";
            this.ReVersionName.ItemClick += new ItemClickEventHandler(this.ReVersionName_ItemClick);
            this.DeleteVersions.Caption = "删除";
            this.DeleteVersions.Id = 2;
            this.DeleteVersions.Name = "DeleteVersions";
            this.DeleteVersions.ItemClick += new ItemClickEventHandler(this.DeleteVersions_ItemClick);
            this.RefreshVersion.Caption = "刷新";
            this.RefreshVersion.Id = 3;
            this.RefreshVersion.Name = "RefreshVersion";
            this.RefreshVersion.ItemClick += new ItemClickEventHandler(this.RefreshVersion_ItemClick);
            this.Property.Caption = "属性";
            this.Property.Id = 4;
            this.Property.Name = "Property";
            this.Property.ItemClick += new ItemClickEventHandler(this.Property_ItemClick);
            base.Controls.Add(this.VersionInfolist);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            base.Name = "VersionInfoControl";
            base.Size = new Size(416, 200);
            base.Load += new EventHandler(this.VersionInfoControl_Load);
            this.barManager_0.EndInit();
            this.popupMenu1.EndInit();
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
        private ColumnHeader columnHeader_4;
        private BarButtonItem DeleteVersions;
        private IVersionedWorkspace iversionedWorkspace_0;
        private BarButtonItem NewVerion;
        private PopupMenu popupMenu1;
        private BarButtonItem Property;
        private BarButtonItem RefreshVersion;
        private BarButtonItem ReVersionName;
        private ListView VersionInfolist;
    }
}