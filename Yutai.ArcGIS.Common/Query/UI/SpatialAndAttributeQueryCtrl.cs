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

namespace Yutai.ArcGIS.Common.Query.UI
{
    public partial class SpatialAndAttributeQueryCtrl : UserControl
    {
        private IContainer icontainer_0 = null;
        private IMap imap_0 = null;

        public SpatialAndAttributeQueryCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if ((this.cboSourceLayer.SelectedIndex != -1) && (this.comboBoxLayer.SelectedIndex != -1))
            {
                IFeatureLayer layer = null;
                layer = (this.cboSourceLayer.SelectedItem as LayerObjectWrap).Layer as IFeatureLayer;
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
                layer2 = (this.comboBoxLayer.SelectedItem as LayerObjectWrap).Layer as IFeatureLayer;
                this.method_3(layer2, cursor as IFeatureCursor, esriSpatialRelIntersects, esriSelectionResultNew,
                    this.memEditWhereCaluse.Text);
                (this.imap_0 as IActiveView).Refresh();
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
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
            Cursor = Cursors.Default;
        }

        private void btnCreateQuery_Click(object sender, EventArgs e)
        {
            if (this.comboBoxLayer.SelectedIndex != -1)
            {
                frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder();
                ILayer layer = (this.comboBoxLayer.SelectedItem as LayerObjectWrap).Layer;
                builder.CurrentLayer = layer;
                builder.WhereCaluse = this.memEditWhereCaluse.Text;
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    this.memEditWhereCaluse.Text = builder.WhereCaluse;
                }
            }
        }

        private void cboSourceLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSourceLayer.SelectedIndex > -1)
            {
                IFeatureLayer layer = (this.cboSourceLayer.SelectedItem as LayerObjectWrap).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count == 0)
                {
                    this.chkUseSelectFeature.Enabled = false;
                    this.chkUseSelectFeature.Checked = false;
                }
                else
                {
                    this.chkUseSelectFeature.Checked = true;
                    if (this.method_1(layer))
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

        private void chkUseBuffer_CheckedChanged(object sender, EventArgs e)
        {
            this.txtRadius.Enabled = this.chkUseBuffer.Checked;
            this.cboUnit.Enabled = this.chkUseBuffer.Checked;
        }

        private void chkUsetSelectedLayer_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxLayer.Properties.Items.Clear();
            this.method_2(this.comboBoxLayer, false);
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_2(this.cboSourceLayer, false);
            if (this.cboSourceLayer.Properties.Items.Count > 0)
            {
                this.cboSourceLayer.SelectedIndex = 0;
            }
        }

        private void method_0(ComboBoxEdit comboBoxEdit_0, ICompositeLayer icompositeLayer_0, bool bool_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (bool_0 && this.method_1(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else if (!bool_0)
                        {
                            if (this.chkUseBuffer.Checked)
                            {
                                if ((layer as IFeatureLayer).Selectable)
                                {
                                    comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                                }
                            }
                            else
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else
                        {
                            comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(comboBoxEdit_0, layer as ICompositeLayer, bool_0);
                }
            }
        }

        private bool method_1(ILayer ilayer_0)
        {
            return ((this.comboBoxLayer.SelectedIndex != -1) &&
                    ((this.comboBoxLayer.SelectedItem as LayerObjectWrap).Layer == ilayer_0));
        }

        private void method_2(ComboBoxEdit comboBoxEdit_0, bool bool_0)
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
                        if (bool_0 && this.method_1(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else if (!bool_0)
                        {
                            if (this.chkUseBuffer.Checked)
                            {
                                if ((layer as IFeatureLayer).Selectable)
                                {
                                    comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                                }
                            }
                            else
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else
                        {
                            comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(comboBoxEdit_0, layer as ICompositeLayer, bool_0);
                }
            }
        }

        private void method_3(IFeatureLayer ifeatureLayer_0, IFeatureCursor ifeatureCursor_0,
            esriSpatialRelEnum esriSpatialRelEnum_0, esriSelectionResultEnum esriSelectionResultEnum_0, string string_0)
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
                        ISpatialFilter filter = new SpatialFilter
                        {
                            SpatialRel = esriSpatialRelEnum_0,
                            WhereClause = string_0
                        };
                        if (selection.BufferDistance > 0.0)
                        {
                            filter.Geometry = (feature.Shape as ITopologicalOperator).Buffer(selection.BufferDistance);
                        }
                        else
                        {
                            filter.Geometry = feature.Shape;
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

        private void SpatialAndAttributeQueryCtrl_Load(object sender, EventArgs e)
        {
            this.method_2(this.comboBoxLayer, false);
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        public IMap Map
        {
            set { this.imap_0 = value; }
        }

        internal partial class LayerObjectWrap
        {
            private ILayer ilayer_0 = null;

            public LayerObjectWrap(ILayer ilayer_1)
            {
                this.ilayer_0 = ilayer_1;
            }

            public override string ToString()
            {
                if (this.ilayer_0 != null)
                {
                    return this.ilayer_0.Name;
                }
                return "";
            }

            public ILayer Layer
            {
                get { return this.ilayer_0; }
            }
        }
    }
}