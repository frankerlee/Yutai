using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class FindResultControl : UserControl
    {
        private BarDockControl barDockControl_0;
        private BarDockControl barDockControl_1;
        private BarDockControl barDockControl_2;
        private BarDockControl barDockControl_3;
        private BarManager barManager_0;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Container container_0 = null;
        private BarButtonItem FlashFeature;
        private IActiveView iactiveView_0 = null;
        private IArray iarray_0 = null;
        private BarButtonItem Identify;
        private Label label1;
        private ListView listView1;
        private Panel panel1;
        private Panel panel2;
        private PopupMenu popupMenu1;
        private BarButtonItem SelectFeature;
        private BarButtonItem UnSelectFeature;
        private BarButtonItem ZoomToFeature;

        public FindResultControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void FindResultControl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void FlashFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            IFeatureFindData2 tag = null;
            int num;
            new ArrayClass();
            IEnvelope other = null;
            for (num = 0; num < this.listView1.SelectedItems.Count; num++)
            {
                tag = this.listView1.SelectedItems[num].Tag as IFeatureFindData2;
                if (num == 0)
                {
                    other = tag.Feature.Shape.Envelope;
                }
                else
                {
                    other.Union(tag.Feature.Shape.Envelope);
                }
            }
            IRelationalOperator extent = this.iactiveView_0.Extent as IRelationalOperator;
            if (!extent.Contains(other))
            {
                this.iactiveView_0.Extent = other;
                this.iactiveView_0.Refresh();
                this.iactiveView_0.ScreenDisplay.UpdateWindow();
            }
            for (num = 0; num < this.listView1.SelectedItems.Count; num++)
            {
                tag = this.listView1.SelectedItems[num].Tag as IFeatureFindData2;
                Flash.FlashFeature(this.iactiveView_0.ScreenDisplay, tag.Feature);
            }
        }

        private void Identify_ItemClick(object sender, ItemClickEventArgs e)
        {
            IFeatureFindData2 tag = null;
            IArray array = new ArrayClass();
            for (int i = 0; i < this.listView1.SelectedItems.Count; i++)
            {
                tag = this.listView1.SelectedItems[i].Tag as IFeatureFindData2;
                array.Add(tag.Feature);
            }
            frmInfo info = new frmInfo {
                FocusMap = this.iactiveView_0.FocusMap as IBasicMap
            };
            info.SetInfo(null, array, array.get_Element(0) as IFeature);
            info.ShowDialog();
        }

        private void InitializeComponent()
        {
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
            this.popupMenu1 = new PopupMenu();
            this.FlashFeature = new BarButtonItem();
            this.ZoomToFeature = new BarButtonItem();
            this.Identify = new BarButtonItem();
            this.SelectFeature = new BarButtonItem();
            this.UnSelectFeature = new BarButtonItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.barManager_0.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1d8, 0x1a);
            this.panel1.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 0x11);
            this.label1.TabIndex = 0;
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0x1a);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1d8, 0x7e);
            this.panel2.TabIndex = 1;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x1d8, 0x7e);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp);
            this.columnHeader_0.Text = "值";
            this.columnHeader_0.Width = 0x7b;
            this.columnHeader_1.Text = "图层";
            this.columnHeader_1.Width = 0x71;
            this.columnHeader_2.Text = "字段";
            this.columnHeader_2.Width = 0xbc;
            this.barManager_0.DockControls.Add(this.barDockControl_0);
            this.barManager_0.DockControls.Add(this.barDockControl_1);
            this.barManager_0.DockControls.Add(this.barDockControl_2);
            this.barManager_0.DockControls.Add(this.barDockControl_3);
            this.barManager_0.Form = this;
            this.barManager_0.Items.AddRange(new BarItem[] { this.FlashFeature, this.ZoomToFeature, this.Identify, this.SelectFeature, this.UnSelectFeature });
            this.barManager_0.MaxItemId = 5;
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.FlashFeature), new LinkPersistInfo(this.ZoomToFeature), new LinkPersistInfo(this.Identify), new LinkPersistInfo(this.SelectFeature), new LinkPersistInfo(this.UnSelectFeature) });
            this.popupMenu1.Manager = this.barManager_0;
            this.popupMenu1.Name = "popupMenu1";
            this.FlashFeature.Caption = "闪烁要素";
            this.FlashFeature.Id = 0;
            this.FlashFeature.Name = "FlashFeature";
            this.FlashFeature.ItemClick += new ItemClickEventHandler(this.FlashFeature_ItemClick);
            this.ZoomToFeature.Caption = "缩放到要素";
            this.ZoomToFeature.Id = 1;
            this.ZoomToFeature.Name = "ZoomToFeature";
            this.ZoomToFeature.ItemClick += new ItemClickEventHandler(this.ZoomToFeature_ItemClick);
            this.Identify.Caption = "查看要素";
            this.Identify.Id = 2;
            this.Identify.Name = "Identify";
            this.Identify.ItemClick += new ItemClickEventHandler(this.Identify_ItemClick);
            this.SelectFeature.Caption = "选择要素";
            this.SelectFeature.Id = 3;
            this.SelectFeature.Name = "SelectFeature";
            this.SelectFeature.ItemClick += new ItemClickEventHandler(this.SelectFeature_ItemClick);
            this.UnSelectFeature.Caption = "取消选择要素";
            this.UnSelectFeature.Id = 4;
            this.UnSelectFeature.Name = "UnSelectFeature";
            this.UnSelectFeature.ItemClick += new ItemClickEventHandler(this.UnSelectFeature_ItemClick);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.barDockControl_2);
            base.Controls.Add(this.barDockControl_3);
            base.Controls.Add(this.barDockControl_1);
            base.Controls.Add(this.barDockControl_0);
            base.Name = "FindResultControl";
            base.Size = new Size(0x1d8, 0x98);
            base.Load += new EventHandler(this.FindResultControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.barManager_0.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (this.listView1.Items.Count > 0))
            {
                System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
                p = base.PointToScreen(p);
                this.popupMenu1.ShowPopup(p);
            }
        }

        private void method_0()
        {
            this.listView1.Items.Clear();
            if (this.iarray_0 == null)
            {
                this.label1.Text = "没找到满足条件的要素";
            }
            else
            {
                string[] items = new string[3];
                IArray array = null;
                int num = 0;
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    array = this.iarray_0.get_Element(i) as IArray;
                    int index = 0;
                    while (index < array.Count)
                    {
                        IFindObj obj2 = array.get_Element(index) as IFindObj;
                        items[0] = obj2.Value;
                        items[1] = obj2.LayerName;
                        items[2] = obj2.FieldName;
                        this.listView1.Items.Add(new ListViewItem(items)).Tag = obj2;
                        index++;
                    }
                    num += index;
                }
                this.label1.Text = "共找到 " + num.ToString() + " 个要素";
                if (this.listView1.Items.Count > 0)
                {
                    this.listView1.Items[0].Selected = true;
                }
            }
        }

        private void SelectFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            IFeatureFindData2 tag = null;
            IArray array = new ArrayClass();
            try
            {
                for (int i = 0; i < this.listView1.SelectedItems.Count; i++)
                {
                    tag = this.listView1.SelectedItems[i].Tag as IFeatureFindData2;
                    this.iactiveView_0.FocusMap.SelectFeature(tag.Layer, tag.Feature);
                    array.Add(tag.Feature);
                }
                IEnumFeature featureSelection = this.iactiveView_0.FocusMap.FeatureSelection as IEnumFeature;
                featureSelection.Reset();
                IFeature feature2 = featureSelection.Next();
                IEnvelope other = null;
                while (feature2 != null)
                {
                    if (other == null)
                    {
                        other = feature2.Extent;
                    }
                    else
                    {
                        other.Union(feature2.Extent);
                    }
                    feature2 = featureSelection.Next();
                }
                if (other != null)
                {
                    IRelationalOperator extent = this.iactiveView_0.Extent as IRelationalOperator;
                    if (!extent.Contains(other))
                    {
                        this.iactiveView_0.Extent = other;
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            this.iactiveView_0.Refresh();
        }

        private void UnSelectFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            IFeatureFindData2 tag = null;
            IFeatureSelection layer = null;
            Exception exception;
            int[] numArray = new int[1];
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            try
            {
                for (int i = 0; i < this.listView1.SelectedItems.Count; i++)
                {
                    tag = this.listView1.SelectedItems[i].Tag as IFeatureFindData2;
                    layer = tag.Layer as IFeatureSelection;
                    if (layer != null)
                    {
                        numArray[0] = tag.Feature.OID;
                        try
                        {
                            layer.SelectionSet.RemoveList(1, ref numArray[0]);
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            Logger.Current.Error("", exception, "");
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Logger.Current.Error("", exception, "");
            }
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        private void ZoomToFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            IFeatureFindData2 tag = null;
            int num;
            IArray array = new ArrayClass();
            for (num = 0; num < this.listView1.SelectedItems.Count; num++)
            {
                tag = this.listView1.SelectedItems[num].Tag as IFeatureFindData2;
                array.Add(tag.Feature);
            }
            CommonHelper.Zoom2Features(this.iactiveView_0, array);
            this.iactiveView_0.ScreenDisplay.UpdateWindow();
            for (num = 0; num < this.listView1.SelectedItems.Count; num++)
            {
                tag = this.listView1.SelectedItems[num].Tag as IFeatureFindData2;
                Flash.FlashFeature(this.iactiveView_0.ScreenDisplay, tag.Feature);
            }
        }

        public IActiveView ActiveView
        {
            set
            {
                this.iactiveView_0 = value;
            }
        }

        public IArray FindResults
        {
            set
            {
                this.iarray_0 = value;
            }
        }
    }
}

