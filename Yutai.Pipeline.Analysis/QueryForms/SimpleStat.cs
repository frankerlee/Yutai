using System;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleStat : Form
    {
        public ColumnHeader ColumnHeader4;

        public IAppContext MContext;

        public IMapControl3 MapControl;

        public IGeometry MIpGeo;

        public IPipelineConfig PPipeCfg;

        public SimpleStat()
        {
            InitializeComponent();
        }

        public bool SelectGeometry
        {
            get { return GeometrySet.Checked; }
            set { GeometrySet.Checked = value; }
        }

        public void AutoFlash()
        {
            _layersCheckedListBox.Items.Clear();
            var layerCount = MContext.FocusMap.LayerCount;
            for (var i = 0; i < layerCount; i++)
            {
                var ipLay = MContext.FocusMap.Layer[i];
                AddLayer(ipLay);
            }
            if (_layersCheckedListBox.Items.Count > 0)
                _layersCheckedListBox.SelectedIndex = 0;
        }

        private IFeatureLayer GetLayer(string layername)
        {
            var layerCount = MContext.FocusMap.LayerCount;
            IFeatureLayer featureLayer = null;
            IFeatureLayer result;
            if (MapControl == null)
            {
                result = null;
            }
            else
            {
                for (var i = 0; i < layerCount; i++)
                {
                    var layer = MContext.FocusMap.Layer[i];
                    if (layer is IFeatureLayer)
                    {
                        var a = layer.Name;
                        if (a == layername)
                        {
                            featureLayer = (IFeatureLayer)MContext.FocusMap.Layer[i];
                            break;
                        }
                    }
                    else if (layer is IGroupLayer)
                    {
                        var compositeLayer = (ICompositeLayer)layer;
                        {
                            var count = compositeLayer.Count;
                            for (var j = 0; j < count; j++)
                            {
                                var layer2 = compositeLayer.Layer[j];
                                var a = layer2.Name;
                                if (a == layername)
                                {
                                    featureLayer = (IFeatureLayer)layer2;
                                    break;
                                }
                            }
                        }
                    }
                }
                result = featureLayer;
            }
            return result;
        }

        private bool ColumnEqual(object A, object B)
        {
            return ((A == DBNull.Value) && (B == DBNull.Value)) ||
                   ((A != DBNull.Value) && (B != DBNull.Value) && A.Equals(B));
        }

        private void AllBut_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < _layersCheckedListBox.Items.Count; i++)
                _layersCheckedListBox.SetItemChecked(i, true);
            _gjListBox.Items.Clear();
            var count = _layersCheckedListBox.CheckedItems.Count;
            if (count != 0)
                for (var j = 0; j < count; j++)
                {
                    var pPipeLayer =
                        ((LayerboxItem)_layersCheckedListBox.CheckedItems[j]).m_pPipeLayer;
                    FillFieldValuesToListBox(pPipeLayer, _gjListBox);
                }
        }

        private void NoneBut_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < _layersCheckedListBox.Items.Count; i++)
                _layersCheckedListBox.SetItemChecked(i, false);
            _gjListBox.Items.Clear();
        }

        private void RevBut_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < _layersCheckedListBox.Items.Count; i++)
                if (_layersCheckedListBox.GetItemChecked(i))
                    _layersCheckedListBox.SetItemChecked(i, false);
                else
                    _layersCheckedListBox.SetItemChecked(i, true);
            _gjListBox.Items.Clear();
            var count = _layersCheckedListBox.CheckedItems.Count;
            if (count != 0)
                for (var j = 0; j < count; j++)
                {
                    var pPipeLayer =
                        ((LayerboxItem)_layersCheckedListBox.CheckedItems[j]).m_pPipeLayer;
                    FillFieldValuesToListBox(pPipeLayer, _gjListBox);
                }
        }

        private void SimpleStat_Load(object sender, EventArgs e)
        {
            AutoFlash();
        }

        public void FillValue()
        {
            if (myfields != null)
            {
                var layerInfo = PPipeCfg.GetBasicLayerInfo(SelectLayer.FeatureClass);
                var lineTableFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);
                var num = myfields.FindField(lineTableFieldName);
                myfield = myfields.Field[num];
                var featureClass = SelectLayer.FeatureClass;
                IQueryFilter queryFilter = new QueryFilter();
                var featureCursor = featureClass.Search(queryFilter, false);
                var feature = featureCursor.NextFeature();
                _gjListBox.Items.Clear();
                while (feature != null)
                {
                    var obj = feature.Value[num];
                    string text;
                    if (obj is DBNull)
                    {
                        text = "NULL";
                    }
                    else
                    {
                        text = obj.ToString();
                        if (text.Length == 0)
                            text = "空字段值";
                    }
                    if (!_gjListBox.Items.Contains(text))
                        _gjListBox.Items.Add(text);
                    feature = featureCursor.NextFeature();
                }
            }
        }

        private void AddLayer(ILayer ipLay)
        {
            if (ipLay is IFeatureLayer)
                AddFeatureLayer((IFeatureLayer)ipLay);
            else if (ipLay is IGroupLayer)
                AddGroupLayer((IGroupLayer)ipLay);
        }

        private void AddGroupLayer(IGroupLayer iGLayer)
        {
            var compositeLayer = (ICompositeLayer)iGLayer;
            if (compositeLayer != null)
            {
                var count = compositeLayer.Count;
                for (var i = 0; i < count; i++)
                {
                    var ipLay = compositeLayer.Layer[i];
                    AddLayer(ipLay);
                }
            }
        }

        private void AddFeatureLayer(IFeatureLayer iFLayer)
        {
            if (iFLayer != null)
                if (PPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
                {
                    var layerboxItem = new LayerboxItem();
                    layerboxItem.m_pPipeLayer = iFLayer;
                    _layersCheckedListBox.Items.Add(layerboxItem);
                }
        }

        private void CalButton_Click_1(object sender, EventArgs e)
        {
            var count = _layersCheckedListBox.CheckedItems.Count;
            if (count == 0)
            {
                MessageBox.Show(@"请选定需要统计的管线");
            }
            else
            {
                ValidateField();
                var rowCount = dataGridView1.RowCount;
                if (rowCount <= 0)
                {
                    MessageBox.Show(@"请确定上下限的值，其值不能为空");
                }
                else if ((dataGridView1[0, 0].Value == null) && (dataGridView1[1, 0].Value == null))
                {
                    MessageBox.Show(@"没有确定管径的范围");
                }
                else
                {
                    var num2 = -1;
                    var dataTable = new DataTable();
                    dataTable.Columns.Clear();
                    if (!dataTable.Columns.Contains("层名"))
                        dataTable.Columns.Add("层名", typeof(string));
                    if (!dataTable.Columns.Contains("统计范围"))
                        dataTable.Columns.Add("统计范围", typeof(string));
                    if (!dataTable.Columns.Contains("条数"))
                        dataTable.Columns.Add("条数", typeof(int));
                    if (!dataTable.Columns.Contains("长度"))
                        dataTable.Columns.Add("长度", typeof(double));
                    var count2 = dataGridView1.Rows.Count;
                    for (var i = 0; i < count; i++)
                        for (var j = 0; j < count2; j++)
                        {
                            var text = (string)dataGridView1[0, j].Value;
                            var text2 = (string)dataGridView1[1, j].Value;
                            if ((text == null) || (text2 == null))
                            {
                                MessageBox.Show(@"请确定上下限的值,其值不能为空");
                                return;
                            }
                            double num3;
                            double num4;
                            try
                            {
                                num3 = Convert.ToDouble(text);
                                num4 = Convert.ToDouble(text2);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(@"请确定上下限的值");
                                return;
                            }
                            var pPipeLayer =
                                ((LayerboxItem)_layersCheckedListBox.CheckedItems[i]).m_pPipeLayer;
                            var fields = pPipeLayer.FeatureClass.Fields;
                            var layerInfo = PPipeCfg.GetBasicLayerInfo(SelectLayer.FeatureClass);
                            var lineTableFieldName = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);
                            // this.pPipeCfg.GetLineTableFieldName("管径");
                            var num = fields.FindField(lineTableFieldName);
                            for (var k = 0; k < fields.FieldCount; k++)
                            {
                                var field = fields.Field[k];
                                if (field.Type == (esriFieldType)7)
                                {
                                    num2 = k;
                                    break;
                                }
                            }
                            if (num >= 0)
                            {
                                var name = pPipeLayer.Name;
                                ISpatialFilter spatialFilter = new SpatialFilter();
                                var featureClass = pPipeLayer.FeatureClass;
                                if (GeometrySet.Checked)
                                {
                                    if (MIpGeo != null)
                                        spatialFilter.Geometry = MIpGeo;
                                    spatialFilter.SpatialRel = (esriSpatialRelEnum)1;
                                }
                                var selectionSet = featureClass.Select(spatialFilter, (esriSelectionType)3,
                                    (esriSelectionOption)1, null);
                                ITableSort tableSort = new TableSort();
                                tableSort.Fields = lineTableFieldName;
                                tableSort.SelectionSet = selectionSet;
                                tableSort.Sort(null);
                                var rows = tableSort.Rows;
                                var num5 = 0;
                                var row = rows.NextRow();
                                var num6 = 0.0;
                                while (row != null)
                                {
                                    var obj = row.Value[num];
                                    var num7 = 0.0;
                                    if (obj is DBNull)
                                    {
                                        row = rows.NextRow();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            num7 = Convert.ToDouble(obj);
                                        }
                                        catch (Exception)
                                        {
                                            // ignored
                                        }
                                        if ((num7 >= num3) && (num7 < num4))
                                        {
                                            var polyline = (IPolyline)row.Value[num2];
                                            var pointCollection = (IPointCollection)polyline;
                                            var num8 = 0.0;
                                            for (var l = 0; l < pointCollection.PointCount - 1; l++)
                                            {
                                                var point = pointCollection.Point[l];
                                                var point2 = pointCollection.Point[l + 1];
                                                if (double.IsNaN(point.Z))
                                                    num8 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0));
                                                else
                                                    num8 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0) + Math.Pow(point.Z - point2.Z, 2.0));
                                            }
                                            num6 += num8;
                                            num5++;
                                        }
                                        row = rows.NextRow();
                                    }
                                }
                                object obj2 = text + "-" + text2;
                                num6 = Math.Round(num6, 3);
                                dataTable.Rows.Add(name, obj2, num5, num6);
                            }
                        }
                    new ClassCollectResultForm
                    {
                        nType = 1,
                        ResultTable = dataTable
                    }.ShowDialog();
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _gjListBox.Items.Clear();
            var count = _layersCheckedListBox.CheckedItems.Count;
            if (count != 0)
                for (var i = 0; i < count; i++)
                {
                    var pPipeLayer = ((LayerboxItem)_layersCheckedListBox.CheckedItems[i]).m_pPipeLayer;
                    FillFieldValuesToListBox(pPipeLayer, _gjListBox);
                    var count2 = _gjListBox.Items.Count;
                    for (var j = 0; j < count2; j++)
                    {
                        var a = _gjListBox.Items[j].ToString();
                        if (a == "")
                            _gjListBox.Items.RemoveAt(j);
                    }
                }
        }

        private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, ListBox lbVal)
        {
            var featureClass = pFeaLay.FeatureClass;
            IQueryFilter queryFilter = new QueryFilter();
            var featureCursor = featureClass.Search(queryFilter, false);
            var feature = featureCursor.NextFeature();
            var layerInfo = PPipeCfg.GetBasicLayerInfo(featureClass);
            var num = featureClass.Fields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
            if (num != -1)
                while (feature != null)
                {
                    object obj = feature.Value[num].ToString();
                    if (Convert.IsDBNull(obj))
                    {
                        feature = featureCursor.NextFeature();
                    }
                    else
                    {
                        var text = obj.ToString();
                        text = text.Trim();
                        if (text == string.Empty)
                        {
                            feature = featureCursor.NextFeature();
                        }
                        else
                        {
                            if (!lbVal.Items.Contains(text))
                                lbVal.Items.Add(obj);
                            feature = featureCursor.NextFeature();
                        }
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _layersCheckedListBox.Items.Clear();
            _gjListBox.Items.Clear();
            dataGridView1.Rows.Clear();
            GeometrySet.Checked = false;
            Close();
        }

        private void ValidateField()
        {
            var selectedIndex = _layersCheckedListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                SelectLayer = null;
                if (MapControl != null)
                {
                    SelectLayer = ((LayerboxItem)_layersCheckedListBox.SelectedItem).m_pPipeLayer;
                    if (SelectLayer != null)
                    {
                        var layerInfo = PPipeCfg.GetBasicLayerInfo(SelectLayer.FeatureClass);
                        myfields = SelectLayer.FeatureClass.Fields;
                        strGJ = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                dataGridView1.Rows.Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_gjListBox.Items.Count == 0)
            {
                MessageBox.Show(@"请选定要统计的管线");
            }
            else if (_gjListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"请选定需要的管径");
            }
            else
            {
                var count = dataGridView1.Rows.Count;
                if (count < 1)
                {
                    MessageBox.Show(@"请用户添加行");
                }
                else
                {
                    var value = _gjListBox.SelectedItem.ToString();
                    var num = Convert.ToDouble(value);
                    var dataGridViewRow = dataGridView1.CurrentRow;
                    if (dataGridViewRow != null)
                    {
                        var index = dataGridViewRow.Index;
                        var num2 = Convert.ToDouble(dataGridView1[1, index].Value);
                        if ((num2 < num) && (Math.Abs(num2) > 0))
                            MessageBox.Show(@"下限值不应大于上限值");
                        else
                            dataGridViewRow.Cells[0].Value = value;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_gjListBox.Items.Count == 0)
            {
                MessageBox.Show(@"请选定要统计的管线");
            }
            else if (_gjListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"请选定需要的管径");
            }
            else
            {
                var count = dataGridView1.Rows.Count;
                if (count < 1)
                {
                    MessageBox.Show(@"请用户添加行");
                }
                else
                {
                    var value = _gjListBox.SelectedItem.ToString();
                    var num = Convert.ToDouble(value);
                    var dataGridViewRow = dataGridView1.CurrentRow;
                    if (dataGridViewRow != null)
                    {
                        var index = dataGridViewRow.Index;
                        var num2 = Convert.ToDouble(dataGridView1[0, index].Value);
                        if (num2 > num)
                            MessageBox.Show(@"下限值不应大于上限值");
                        else
                            dataGridViewRow.Cells[1].Value = value;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(@"行数已为空");
            }
            else
            {
                var dataGridViewRow = dataGridView1.CurrentRow;
                if (dataGridViewRow != null) dataGridView1.Rows.RemoveAt(dataGridViewRow.Index);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void InsertBut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Rows.Add();
            }
            else
            {
                var dataGridViewRow = dataGridView1.CurrentRow;
                if (dataGridViewRow != null)
                    dataGridView1.Rows.Insert(dataGridViewRow.Index);
            }
        }

        private void SimpleStat_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                var axMapControl = MContext.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw += AxMapControlOnOnAfterDraw;
            }
            else
            {
                var axMapControl = MContext.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            }
        }

        private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
        {
            if (viewDrawPhase == 32)
                DrawSelGeometry();
        }

        private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            var axMapControl = MContext.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            OnClosed(e);
        }

        public void DrawSelGeometry()
        {
            if (MIpGeo != null)
            {
                IRgbColor rgbColor = new RgbColor();
                var selectionCorlor = MContext.Config.SelectionEnvironment.DefaultColor;
                rgbColor.RGB = selectionCorlor.RGB;
                rgbColor.Transparency = selectionCorlor.Transparency;

                object obj = null;
                var selectionBufferInPixels = MContext.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
                switch ((int)MIpGeo.GeometryType)
                {
                    case 1:
                        {
                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                            symbol = (ISymbol)simpleMarkerSymbol;
                            symbol.ROP2 = (esriRasterOpCode)10;
                            simpleMarkerSymbol.Color = rgbColor;
                            simpleMarkerSymbol.Size =
                                selectionBufferInPixels + selectionBufferInPixels + selectionBufferInPixels;
                            break;
                        }
                    case 3:
                        {
                            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                            symbol = (ISymbol)simpleLineSymbol;
                            symbol.ROP2 = (esriRasterOpCode)10;
                            simpleLineSymbol.Color = rgbColor;
                            simpleLineSymbol.Color.Transparency = 1;
                            simpleLineSymbol.Width = selectionBufferInPixels;
                            break;
                        }
                    case 4:
                    case 5:
                        {
                            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                            symbol = (ISymbol)simpleFillSymbol;
                            symbol.ROP2 = (esriRasterOpCode)10;
                            simpleFillSymbol.Color = rgbColor;
                            simpleFillSymbol.Color.Transparency = 1;
                            break;
                        }
                }
                obj = symbol;
                MapControl.DrawShape(MIpGeo, ref obj);
            }
        }

        private void GeometrySet_Click(object sender, EventArgs e)
        {
            if (!GeometrySet.Checked)
            {
                MIpGeo = null;
                MContext.ActiveView.Refresh();
            }
        }

        private void SimpleStat_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            var url = Application.StartupPath + "\\帮助.chm";
            var parameter = "分段统计";
            var command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }

        private class LayerboxItem
        {
            public IFeatureLayer m_pPipeLayer;

            public override string ToString()
            {
                return m_pPipeLayer.Name;
            }
        }
    }
}