using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;
using ItemCheckEventArgs = System.Windows.Forms.ItemCheckEventArgs;
using ItemCheckEventHandler = System.Windows.Forms.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public partial class SelectByLocationCtrl : UserControl
    {
        private bool bool_0 = true;
        private Container container_0 = null;
        private IMap imap_0 = null;

        public SelectByLocationCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.cboSourceLayer.SelectedIndex != -1)
            {
                IFeatureLayer layer = null;
                layer = (this.cboSourceLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                ICursor cursor = null;
                if (this.chkUseSelectFeature.Checked)
                {
                    (layer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                }
                else
                {
                    cursor = layer.Search(null, false) as ICursor;
                }
                esriSpatialRelEnum esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelIntersects;
                string text = this.cboSpatialRelation.Text;
                switch (text)
                {
                    case null:
                        break;

                    case "相交":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelIntersects;
                        break;

                    case "包围矩形相交":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                        break;

                    case "相接":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelTouches;
                        break;

                    case "重叠":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelOverlaps;
                        break;

                    default:
                        if (!(text == "被包含"))
                        {
                            if (text == "包含")
                            {
                                esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelContains;
                            }
                        }
                        else
                        {
                            esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelWithin;
                        }
                        break;
                }
                esriSelectionResultEnum esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultNew;
                switch (this.cboOperationType.SelectedIndex)
                {
                    case 0:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultNew;
                        break;

                    case 1:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultAdd;
                        break;

                    case 2:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultSubtract;
                        break;

                    case 3:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultAnd;
                        break;
                }
                IFeatureLayer layer2 = null;
                for (int i = 0; i < this.checkedListBoxLayer.CheckedItems.Count; i++)
                {
                    layer2 = (this.checkedListBoxLayer.CheckedItems[i] as LayerObject).Layer as IFeatureLayer;
                    this.method_6(layer2, cursor as IFeatureCursor, esriSpatialRelIntersects, esriSelectionResultNew);
                }
                (this.imap_0 as IActiveView).Refresh();
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.btnApply.Enabled = false;
            this.panel1.Visible = true;
            this.bool_0 = false;
            try
            {
                this.Apply();
            }
            catch
            {
            }
            this.bool_0 = true;
            this.btnApply.Enabled = true;
            this.panel1.Visible = false;
            Cursor = Cursors.Default;
        }

        private void cboSourceLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSourceLayer.SelectedIndex > -1)
            {
                IFeatureLayer layer = (this.cboSourceLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count == 0)
                {
                    this.chkUseSelectFeature.Enabled = false;
                    this.chkUseSelectFeature.Checked = false;
                }
                else
                {
                    this.chkUseSelectFeature.Checked = true;
                    if (this.method_5(layer))
                    {
                        this.chkUseSelectFeature.Enabled = false;
                    }
                    else
                    {
                        this.chkUseSelectFeature.Enabled = true;
                    }
                }
            }
        }

        private void checkedListBoxLayer_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.checkedListBoxLayer.SetItemCheckState(e.Index, e.NewValue);
                this.bool_0 = true;
                this.method_3();
            }
        }

        private void chkUseBuffer_CheckedChanged(object sender, EventArgs e)
        {
            this.txtRadius.Enabled = this.chkUseBuffer.Checked;
            this.cboUnit.Enabled = this.chkUseBuffer.Checked;
        }

        private void chkUsetSelectedLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUsetSelectedLayer.Checked)
            {
                for (int i = this.checkedListBoxLayer.Items.Count - 1; i >= 0; i--)
                {
                    LayerObject obj2 = this.checkedListBoxLayer.Items[i] as LayerObject;
                    if (!(obj2.Layer as IFeatureLayer).Selectable)
                    {
                        this.checkedListBoxLayer.Items.RemoveAt(i);
                    }
                }
            }
        }

 private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if (((layer as IFeatureLayer).FeatureClass != null) && !((!this.chkUsetSelectedLayer.Checked || !(layer as IFeatureLayer).Selectable) ? this.chkUsetSelectedLayer.Checked : false))
                    {
                        this.checkedListBoxLayer.Items.Add(new LayerObject(layer));
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
            }
        }

        private void method_1()
        {
            this.checkedListBoxLayer.Items.Clear();
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if (((layer as IFeatureLayer).FeatureClass != null) && !((!this.chkUsetSelectedLayer.Checked || !(layer as IFeatureLayer).Selectable) ? this.chkUsetSelectedLayer.Checked : false))
                    {
                        if (layer is IFeatureLayerSelectionEvents_Event)
                        {
                            (layer as IFeatureLayerSelectionEvents_Event).FeatureLayerSelectionChanged+=(new IFeatureLayerSelectionEvents_FeatureLayerSelectionChangedEventHandler(this.method_4));
                        }
                        this.checkedListBoxLayer.Items.Add(new LayerObject(layer));
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
            }
        }

        private void method_2(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (this.method_5(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                            }
                        }
                        else
                        {
                            this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_2(layer as ICompositeLayer);
                }
            }
        }

        private void method_3()
        {
            this.cboSourceLayer.Properties.Items.Clear();
            this.cboSourceLayer.Text = "";
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (this.method_5(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                            }
                        }
                        else
                        {
                            this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_2(layer as ICompositeLayer);
                }
            }
        }

        private void method_4()
        {
            if (this.bool_0)
            {
                this.method_3();
                this.cboSourceLayer_SelectedIndexChanged(null, null);
            }
        }

        private bool method_5(ILayer ilayer_0)
        {
            for (int i = 0; i < this.checkedListBoxLayer.Items.Count; i++)
            {
                if ((this.checkedListBoxLayer.Items[i] as LayerObject).Layer == ilayer_0)
                {
                    return this.checkedListBoxLayer.GetItemChecked(i);
                }
            }
            return false;
        }

        private void method_6(IFeatureLayer ifeatureLayer_0, IFeatureCursor ifeatureCursor_0, esriSpatialRelEnum esriSpatialRelEnum_0, esriSelectionResultEnum esriSelectionResultEnum_0)
        {
            IFeature feature = ifeatureCursor_0.NextFeature();
            IFeatureSelection selection = ifeatureLayer_0 as IFeatureSelection;
            double bufferDistance = selection.BufferDistance;
            if (this.chkUseBuffer.Checked)
            {
                try
                {
                    selection.BufferDistance = double.Parse(this.txtRadius.Text);
                }
                catch
                {
                }
            }
            bool flag = true;
            while (feature != null)
            {
                if (feature.Shape == null)
                {
                    feature = ifeatureCursor_0.NextFeature();
                }
                else if (feature.Shape.IsEmpty)
                {
                    feature = ifeatureCursor_0.NextFeature();
                }
                else
                {
                    try
                    {
                        ISpatialFilter filter = new SpatialFilter {
                            SpatialRel = esriSpatialRelEnum_0
                        };
                        if (selection.BufferDistance > 0.0)
                        {
                            filter.Geometry = (feature.Shape as ITopologicalOperator).Buffer(selection.BufferDistance);
                        }
                        else
                        {
                            IGeometry shape = feature.Shape;
                            filter.Geometry = shape;
                        }
                        if (flag)
                        {
                            selection.SelectFeatures(filter, esriSelectionResultEnum_0, false);
                            flag = false;
                        }
                        else
                        {
                            selection.SelectFeatures(filter, esriSelectionResultEnum.esriSelectionResultAdd, false);
                        }
                    }
                    catch
                    {
                    }
                    feature = ifeatureCursor_0.NextFeature();
                }
            }
            selection.BufferDistance = bufferDistance;
        }

        private void SelectByLocationCtrl_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.method_3();
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

