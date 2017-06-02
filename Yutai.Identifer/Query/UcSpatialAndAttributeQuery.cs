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
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class UcSpatialAndAttributeQuery : UserControl
    {

        private IMap _pMap = null;

        public IMap Map
        {
            set
            {
                this._pMap = value;
            }
        }

        public UcSpatialAndAttributeQuery()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if ((this.cboSourceLayer.SelectedIndex == -1 ? false : this.comboBoxLayer.SelectedIndex != -1))
            {
                IFeatureLayer layer = null;
                layer = (this.cboSourceLayer.SelectedItem as UcSpatialAndAttributeQuery.LayerObjectWrap).Layer as IFeatureLayer;
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
                featureLayer = (this.comboBoxLayer.SelectedItem as UcSpatialAndAttributeQuery.LayerObjectWrap).Layer as IFeatureLayer;
                this.StartQuery(featureLayer, cursor as IFeatureCursor, _esriSpatialRelEnum, _esriSelectionResultEnum, this.memEditWhereCaluse.Text);
                (this._pMap as IActiveView).Refresh();
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.btnApply.Enabled = false;
            this.panel1.Visible = true;
            try
            {
                this.Apply();
            }
            catch
            {
            }
            this.btnApply.Enabled = true;
            this.panel1.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void btnCreateQuery_Click(object sender, EventArgs e)
        {
            if (this.comboBoxLayer.SelectedIndex != -1)
            {
                frmSimpleAttributeQueryBuilder _builder = new frmSimpleAttributeQueryBuilder()
                {
                    CurrentLayer = (this.comboBoxLayer.SelectedItem as UcSpatialAndAttributeQuery.LayerObjectWrap).Layer,
                    WhereCaluse = this.memEditWhereCaluse.Text
                };
                if (_builder.ShowDialog() == DialogResult.OK)
                {
                    this.memEditWhereCaluse.Text = _builder.WhereCaluse;
                }
            }
        }

        private void cboSourceLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSourceLayer.SelectedIndex > -1)
            {
                IFeatureLayer layer = (this.cboSourceLayer.SelectedItem as UcSpatialAndAttributeQuery.LayerObjectWrap).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count != 0)
                {
                    this.chkUseSelectFeature.Checked = true;
                    if (!this.CompareSelectedLayer(layer))
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

        private void chkUseBuffer_CheckedChanged(object sender, EventArgs e)
        {
            this.txtRadius.Enabled = this.chkUseBuffer.Checked;
            this.cboUnit.Enabled = this.chkUseBuffer.Checked;
        }

        private void chkUsetSelectedLayer_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxLayer.Items.Clear();
            this.InitLayers(this.comboBoxLayer, false);
            if (this.comboBoxLayer.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitLayers(this.cboSourceLayer, false);
            if (this.cboSourceLayer.Items.Count > 0)
            {
                this.cboSourceLayer.SelectedIndex = 0;
            }
        }

   

       

        private void LoadGroupLayer(ComboBox comboBoxEdit_0, ICompositeLayer compLayer, bool bool_0)
        {
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (!(!bool_0 ? true : !this.CompareSelectedLayer(layer)))
                        {
                            if ((layer as IFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                comboBoxEdit_0.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                            }
                        }
                        else if (bool_0)
                        {
                            comboBoxEdit_0.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                        }
                        else if (!this.chkUseBuffer.Checked)
                        {
                            comboBoxEdit_0.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                        }
                        else if ((layer as IFeatureLayer).Selectable)
                        {
                            comboBoxEdit_0.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.LoadGroupLayer(comboBoxEdit_0, layer as ICompositeLayer, bool_0);
                }
            }
        }

        private bool CompareSelectedLayer(ILayer pLayer)
        {
            bool flag;
            flag = (this.comboBoxLayer.SelectedIndex == -1 || (this.comboBoxLayer.SelectedItem as UcSpatialAndAttributeQuery.LayerObjectWrap).Layer != pLayer ? false : true);
            return flag;
        }

        private void InitLayers(ComboBox cmbControl, bool bool_0)
        {
            this.cboSourceLayer.Items.Clear();
            this.cboSourceLayer.Text = "";
            for (int i = 0; i < this._pMap.LayerCount; i++)
            {
                ILayer layer = this._pMap.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (!(!bool_0 ? true : ! this.CompareSelectedLayer(layer)))
                        {
                            if ((layer as IFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                cmbControl.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                            }
                        }
                        else if (bool_0)
                        {
                            cmbControl.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                        }
                        else if (!this.chkUseBuffer.Checked)
                        {
                            cmbControl.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                        }
                        else if ((layer as IFeatureLayer).Selectable)
                        {
                            cmbControl.Items.Add(new UcSpatialAndAttributeQuery.LayerObjectWrap(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.LoadGroupLayer(cmbControl, layer as ICompositeLayer, bool_0);
                }
            }
        }

        private void StartQuery(IFeatureLayer fLayer, IFeatureCursor fCursor, esriSpatialRelEnum pSpatialRelEnum, esriSelectionResultEnum pSelectionResultEnum, string whereClause)
        {
            IFeature feature = fCursor.NextFeature();
            IFeatureSelection fSelection = fLayer as IFeatureSelection;
            double bufferDistance = fSelection.BufferDistance;
            if (this.chkUseBuffer.Checked)
            {
                try
                {
                    fSelection.BufferDistance = double.Parse(this.txtRadius.Text);
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
                    feature = fCursor.NextFeature();
                }
                else if (!feature.Shape.IsEmpty)
                {
                    try
                    {
                        ISpatialFilter spatialFilterClass = new SpatialFilter()
                        {
                            SpatialRel = pSpatialRelEnum,
                            WhereClause = whereClause
                        };
                        if (fSelection.BufferDistance <= 0)
                        {
                            spatialFilterClass.Geometry = feature.Shape;
                        }
                        else
                        {
                            ITopologicalOperator shape = feature.Shape as ITopologicalOperator;
                            spatialFilterClass.Geometry = shape.Buffer(fSelection.BufferDistance);
                        }
                        if (!flag)
                        {
                            fSelection.SelectFeatures(spatialFilterClass, esriSelectionResultEnum.esriSelectionResultAdd, false);
                        }
                        else
                        {
                            fSelection.SelectFeatures(spatialFilterClass, pSelectionResultEnum, false);
                            flag = false;
                        }
                    }
                    catch
                    {
                    }
                    feature = fCursor.NextFeature();
                }
                else
                {
                    feature = fCursor.NextFeature();
                }
            }
            fSelection.BufferDistance = bufferDistance;
        }

        private void UcSpatialAndAttributeQuery_Load(object sender, EventArgs e)
        {
            this.InitLayers(this.comboBoxLayer, false);
            if (this.comboBoxLayer.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        internal class LayerObjectWrap
        {
            private ILayer _layer = null;

            public ILayer Layer
            {
                get
                {
                    return this._layer;
                }
            }

            public LayerObjectWrap(ILayer pLayer)
            {
                this._layer = pLayer;
            }

            public override string ToString()
            {
                string str;
                str = (this._layer == null ? "" : this._layer.Name);
                return str;
            }
        }
    }
}