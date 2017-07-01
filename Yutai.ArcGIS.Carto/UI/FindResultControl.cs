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
    internal partial class FindResultControl : UserControl
    {
        private Container container_0 = null;
        private IActiveView iactiveView_0 = null;
        private IArray iarray_0 = null;

        public FindResultControl()
        {
            this.InitializeComponent();
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
            frmInfo info = new frmInfo
            {
                FocusMap = this.iactiveView_0.FocusMap as IBasicMap
            };
            info.SetInfo(null, array, array.get_Element(0) as IFeature);
            info.ShowDialog();
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
            set { this.iactiveView_0 = value; }
        }

        public IArray FindResults
        {
            set { this.iarray_0 = value; }
        }
    }
}