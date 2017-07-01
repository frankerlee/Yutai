using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Identifer.Common;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class UcSelectByLocation : UserControl
    {
        private IMap _map = null;

        private bool _isBusy = true;

        public IMap Map
        {
            set { this._map = value; }
        }

        public UcSelectByLocation()
        {
            this.InitializeComponent();
            CloseButton = false;
        }

        public void Apply()
        {
            if (this.cboSourceLayer.SelectedIndex != -1)
            {
                IFeatureLayer layer = null;
                layer = (this.cboSourceLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                ICursor cursor = null;
                if (!this.chkUseSelectFeature.Checked)
                {
                    cursor = layer.Search(null, false) as ICursor;
                }
                else
                {
                    (layer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                }
                esriSpatialRelEnum _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelIntersects;
                string text = this.cboSpatialRelation.Text;
                if (text != null)
                {
                    if (text == "相交")
                    {
                        _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else if (text == "包围矩形相交")
                    {
                        _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                    }
                    else if (text == "相接")
                    {
                        _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelTouches;
                    }
                    else if (text == "重叠")
                    {
                        _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelOverlaps;
                    }
                    else if (text == "被包含")
                    {
                        _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelWithin;
                    }
                    else if (text == "包含")
                    {
                        _esriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                esriSelectionResultEnum _esriSelectionResultEnum = esriSelectionResultEnum.esriSelectionResultNew;
                switch (this.cboOperationType.SelectedIndex)
                {
                    case 0:
                    {
                        _esriSelectionResultEnum = esriSelectionResultEnum.esriSelectionResultNew;
                        break;
                    }
                    case 1:
                    {
                        _esriSelectionResultEnum = esriSelectionResultEnum.esriSelectionResultAdd;
                        break;
                    }
                    case 2:
                    {
                        _esriSelectionResultEnum = esriSelectionResultEnum.esriSelectionResultSubtract;
                        break;
                    }
                    case 3:
                    {
                        _esriSelectionResultEnum = esriSelectionResultEnum.esriSelectionResultAnd;
                        break;
                    }
                }
                IFeatureLayer featureLayer = null;
                for (int i = 0; i < this.checkedListBoxLayer.CheckedItems.Count; i++)
                {
                    featureLayer = (this.checkedListBoxLayer.CheckedItems[i] as LayerObject).Layer as IFeatureLayer;
                    this.method_6(featureLayer, cursor as IFeatureCursor, _esriSpatialRelEnum, _esriSelectionResultEnum);
                }
                (this._map as IActiveView).Refresh();
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.btnApply.Enabled = false;
            this.panel1.Visible = true;
            this._isBusy = false;
            try
            {
                this.Apply();
            }
            catch
            {
            }
            this._isBusy = true;
            this.btnApply.Enabled = true;
            this.panel1.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void cboSourceLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSourceLayer.SelectedIndex > -1)
            {
                IFeatureLayer layer = (this.cboSourceLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count != 0)
                {
                    this.chkUseSelectFeature.Checked = true;
                    if (!this.ValidateLayer(layer))
                    {
                        this.chkUseSelectFeature.Enabled = true;
                    }
                    else
                    {
                        this.chkUseSelectFeature.Enabled = false;
                    }
                }
                else
                {
                    this.chkUseSelectFeature.Enabled = false;
                    this.chkUseSelectFeature.Checked = false;
                }
            }
        }

        private void checkedListBoxLayer_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this._isBusy)
            {
                this._isBusy = false;
                this.checkedListBoxLayer.SetItemCheckState(e.Index, e.NewValue);
                this._isBusy = true;
                this.InitLayerList();
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
                    if (!((this.checkedListBoxLayer.Items[i] as LayerObject).Layer as IFeatureLayer).Selectable)
                    {
                        this.checkedListBoxLayer.Items.RemoveAt(i);
                    }
                }
            }
        }


        private void LoadGroupLayer(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if ((!this.chkUsetSelectedLayer.Checked || !(layer as IFeatureLayer).Selectable
                            ? !this.chkUsetSelectedLayer.Checked
                            : true))
                        {
                            this.checkedListBoxLayer.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.LoadGroupLayer(layer as ICompositeLayer);
                }
            }
        }

        private void InitControl()
        {
            this.checkedListBoxLayer.Items.Clear();
            for (int i = 0; i < this._map.LayerCount; i++)
            {
                ILayer layer = this._map.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if ((!this.chkUsetSelectedLayer.Checked || !(layer as IFeatureLayer).Selectable
                            ? !this.chkUsetSelectedLayer.Checked
                            : true))
                        {
                            if (layer is IFeatureLayerSelectionEvents_Event)
                            {
                                (layer as IFeatureLayerSelectionEvents_Event).FeatureLayerSelectionChanged +=
                                    new IFeatureLayerSelectionEvents_FeatureLayerSelectionChangedEventHandler(
                                        this.FeatureSelectionChanged);
                            }
                            this.checkedListBoxLayer.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.LoadGroupLayer(layer as ICompositeLayer);
                }
            }
        }

        private void InitCompLayer(ICompositeLayer compLayer)
        {
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (!this.ValidateLayer(layer))
                        {
                            this.cboSourceLayer.Items.Add(new LayerObject(layer));
                        }
                        else if ((layer as IFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                        {
                            this.cboSourceLayer.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.InitCompLayer(layer as ICompositeLayer);
                }
            }
        }

        private void InitLayerList()
        {
            this.cboSourceLayer.Items.Clear();
            this.cboSourceLayer.Text = "";
            for (int i = 0; i < this._map.LayerCount; i++)
            {
                ILayer layer = this._map.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (!this.ValidateLayer(layer))
                        {
                            this.cboSourceLayer.Items.Add(new LayerObject(layer));
                        }
                        else if ((layer as IFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                        {
                            this.cboSourceLayer.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.InitCompLayer(layer as ICompositeLayer);
                }
            }
        }

        private void FeatureSelectionChanged()
        {
            if (this._isBusy)
            {
                this.InitLayerList();
                this.cboSourceLayer_SelectedIndexChanged(null, null);
            }
        }

        private bool ValidateLayer(ILayer pLayer)
        {
            bool itemChecked;
            int num = 0;
            while (true)
            {
                if (num >= this.checkedListBoxLayer.Items.Count)
                {
                    itemChecked = false;
                    break;
                }
                else if ((this.checkedListBoxLayer.Items[num] as LayerObject).Layer == pLayer)
                {
                    itemChecked = this.checkedListBoxLayer.GetItemChecked(num);
                    break;
                }
                else
                {
                    num++;
                }
            }
            return itemChecked;
        }

        private void method_6(IFeatureLayer featureLayer, IFeatureCursor featureCursor,
            esriSpatialRelEnum pSpatialRelEnum, esriSelectionResultEnum pSelectionResultEnum)
        {
            IFeature feature = featureCursor.NextFeature();
            IFeatureSelection ifeatureLayer0 = featureLayer as IFeatureSelection;
            double bufferDistance = ifeatureLayer0.BufferDistance;
            if (this.chkUseBuffer.Checked)
            {
                try
                {
                    ifeatureLayer0.BufferDistance = double.Parse(this.txtRadius.Text);
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
                    feature = featureCursor.NextFeature();
                }
                else if (!feature.Shape.IsEmpty)
                {
                    try
                    {
                        ISpatialFilter spatialFilterClass = new SpatialFilter()
                        {
                            SpatialRel = pSpatialRelEnum
                        };
                        if (ifeatureLayer0.BufferDistance <= 0)
                        {
                            spatialFilterClass.Geometry = feature.Shape;
                        }
                        else
                        {
                            ITopologicalOperator shape = feature.Shape as ITopologicalOperator;
                            spatialFilterClass.Geometry = shape.Buffer(ifeatureLayer0.BufferDistance);
                        }
                        if (!flag)
                        {
                            ifeatureLayer0.SelectFeatures(spatialFilterClass,
                                esriSelectionResultEnum.esriSelectionResultAdd, false);
                        }
                        else
                        {
                            ifeatureLayer0.SelectFeatures(spatialFilterClass, pSelectionResultEnum, false);
                            flag = false;
                        }
                    }
                    catch
                    {
                    }
                    feature = featureCursor.NextFeature();
                }
                else
                {
                    feature = featureCursor.NextFeature();
                }
            }
            ifeatureLayer0.BufferDistance = bufferDistance;
        }

        private void UcSelectByLocation_Load(object sender, EventArgs e)
        {
            this.InitControl();
            this.InitLayerList();
            btnClose.Visible = CloseButton;
        }

        public bool CloseButton { get; set; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Cancel;
        }
    }
}