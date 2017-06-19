namespace Yutai.Plugins.Editor.Views
{
    partial class GeometryInfoView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ExportGeometry = new DevExpress.XtraBars.BarButtonItem();
            this.listView1 = new Yutai.ArcGIS.Common.ControlExtend.EditListView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BarGeometryInfo = new DevExpress.XtraBars.BarStaticItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.ImportGeometryData = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.InsertPointBefore = new DevExpress.XtraBars.BarButtonItem();
            this.InsertPointAfter = new DevExpress.XtraBars.BarButtonItem();
            this.DeletePoints = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // ExportGeometry
            // 
            this.ExportGeometry.Caption = "导出几何数据";
            this.ExportGeometry.Id = 5;
            this.ExportGeometry.Name = "ExportGeometry";
            this.ExportGeometry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExportGeometry_ItemClick);
            // 
            // listView1
            // 
            this.listView1.ComboBoxBgColor = System.Drawing.Color.LightBlue;
            this.listView1.ComboBoxFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.EditBgColor = System.Drawing.Color.LightBlue;
            this.listView1.EditFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(104, 0);
            this.listView1.LockRowCount = 0;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(327, 409);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ValueChanged += new Yutai.ArcGIS.Common.ControlExtend.ValueChangedHandler(this.listView1_ValueChanged);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(104, 409);
            this.listBox1.TabIndex = 4;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.InsertPointBefore,
            this.InsertPointAfter,
            this.DeletePoints,
            this.BarGeometryInfo,
            this.ImportGeometryData,
            this.ExportGeometry});
            this.barManager1.MaxItemId = 6;
            this.barManager1.StatusBar = this.bar1;
            // 
            // bar1
            // 
            this.bar1.BarName = "状态栏";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BarGeometryInfo)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "状态栏";
            // 
            // BarGeometryInfo
            // 
            this.BarGeometryInfo.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.BarGeometryInfo.Id = 3;
            this.BarGeometryInfo.Name = "BarGeometryInfo";
            this.BarGeometryInfo.Size = new System.Drawing.Size(32, 0);
            this.BarGeometryInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            this.BarGeometryInfo.Width = 32;
            // 
            // bar2
            // 
            this.bar2.BarName = "工具";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Right;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Right;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ImportGeometryData),
            new DevExpress.XtraBars.LinkPersistInfo(this.ExportGeometry, true)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.Text = "工具";
            // 
            // ImportGeometryData
            // 
            this.ImportGeometryData.Caption = "导入几何数据";
            this.ImportGeometryData.Enabled = false;
            this.ImportGeometryData.Id = 4;
            this.ImportGeometryData.Name = "ImportGeometryData";
            this.ImportGeometryData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ImportGeometryData_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(460, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 409);
            this.barDockControl2.Size = new System.Drawing.Size(460, 27);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(0, 409);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(431, 0);
            this.barDockControl4.Size = new System.Drawing.Size(29, 409);
            // 
            // InsertPointBefore
            // 
            this.InsertPointBefore.Caption = "在之前插入点";
            this.InsertPointBefore.Id = 0;
            this.InsertPointBefore.Name = "InsertPointBefore";
            this.InsertPointBefore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.InsertPointBefore_ItemClick);
            // 
            // InsertPointAfter
            // 
            this.InsertPointAfter.Caption = "在之后插入点";
            this.InsertPointAfter.Id = 1;
            this.InsertPointAfter.Name = "InsertPointAfter";
            this.InsertPointAfter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.InsertPointAfter_ItemClick);
            // 
            // DeletePoints
            // 
            this.DeletePoints.Caption = "删除";
            this.DeletePoints.Id = 2;
            this.DeletePoints.Name = "DeletePoints";
            this.DeletePoints.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DeletePoints_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.InsertPointBefore),
            new DevExpress.XtraBars.LinkPersistInfo(this.InsertPointAfter),
            new DevExpress.XtraBars.LinkPersistInfo(this.DeletePoints, true)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // GeometryInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "GeometryInfoView";
            this.Size = new System.Drawing.Size(460, 436);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem ExportGeometry;
        private ArcGIS.Common.ControlExtend.EditListView listView1;
        private System.Windows.Forms.ListBox listBox1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarStaticItem BarGeometryInfo;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem ImportGeometryData;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarButtonItem InsertPointBefore;
        private DevExpress.XtraBars.BarButtonItem InsertPointAfter;
        private DevExpress.XtraBars.BarButtonItem DeletePoints;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
    }
}
