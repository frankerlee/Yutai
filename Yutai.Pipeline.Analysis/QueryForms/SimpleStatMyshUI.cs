
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleStatMyshUI : Form
    {
        private partial class LayerboxItem
        {
            public IFeatureLayer m_pPipeLayer;

            public override string ToString()
            {
                return this.m_pPipeLayer.Name;
            }
        }

        public IGeometry m_ipGeo;

        public IAppContext m_context;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;

        private IList<IPoint> DXArray = new List<IPoint>();

        private DataTable Sumtable = new DataTable();

        public bool SelectGeometry
        {
            get
            {
                return this.GeometrySet.Checked;
            }
            set
            {
                this.GeometrySet.Checked = value;
            }
        }

        public SimpleStatMyshUI()
        {
            this.InitializeComponent();
        }

        private void SimpleStatMyshUI_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        public void AutoFlash()
        {
            this.checkedListBox1.Items.Clear();
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = m_context.FocusMap.get_Layer(i);
                this.AddLayer(ipLay);
            }
            if (this.checkedListBox1.Items.Count > 0)
            {
                this.checkedListBox1.SelectedIndex = 0;
            }
        }

        public void FillValue()
        {
            if (this.myfields != null)
            {
                IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(featureClass);
                int num = this.myfields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                this.myfield = this.myfields.get_Field(num);
                IQueryFilter queryFilter = new QueryFilter();
                IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                IFeature feature = featureCursor.NextFeature();
                this.listBox1.Items.Clear();
                while (feature != null)
                {
                    object obj = feature.get_Value(num);
                    string text;
                    if (obj is DBNull)
                    {
                        text = "NULL";
                    }
                    else
                    {
                        text = obj.ToString();
                        if (text.Length == 0)
                        {
                            text = "空字段值";
                        }
                    }
                    if (!this.listBox1.Items.Contains(text))
                    {
                        this.listBox1.Items.Add(text);
                    }
                    feature = featureCursor.NextFeature();
                }
            }
        }

        private void AddLayer(ILayer ipLay)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer)ipLay);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer)ipLay);
            }
        }

        private void AddGroupLayer(IGroupLayer iGLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    ILayer ipLay = compositeLayer.get_Layer(i);
                    this.AddLayer(ipLay);
                }
            }
        }

        private void AddFeatureLayer(IFeatureLayer iFLayer)
        {
            if (iFLayer != null)
            {
                string aliasName = iFLayer.FeatureClass.AliasName;
                if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
                {
                    SimpleStatMyshUI.LayerboxItem layerboxItem = new SimpleStatMyshUI.LayerboxItem();
                    layerboxItem.m_pPipeLayer = iFLayer;
                    this.checkedListBox1.Items.Add(layerboxItem);
                }
            }
        }

        private bool ColumnEqual(object A, object B)
        {
            return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
        }

        private void CalButton_Click(object sender, EventArgs e)
        {
            int count = this.checkedListBox1.CheckedItems.Count;
            if (count == 0)
            {
                MessageBox.Show("请选定需要统计的管线");
            }
            else
            {
                int rowCount = this.dataGridView1.RowCount;
                if (rowCount <= 0)
                {
                    MessageBox.Show("请确定上下限的值，其值不能为空");
                }
                else if (this.dataGridView1[0, 0].Value == null && this.dataGridView1[1, 0].Value == null)
                {
                    MessageBox.Show("没有确定埋深的范围");
                }
                else
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Clear();
                    if (!dataTable.Columns.Contains("层名"))
                    {
                        dataTable.Columns.Add("层名", typeof(string));
                    }
                    if (!dataTable.Columns.Contains("统计范围"))
                    {
                        dataTable.Columns.Add("统计范围", typeof(string));
                    }
                    if (!dataTable.Columns.Contains("个数"))
                    {
                        dataTable.Columns.Add("个数", typeof(int));
                    }
                    int count2 = this.dataGridView1.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        for (int j = 0; j < count2; j++)
                        {
                            this.DXArray.Clear();
                            string text = (string)this.dataGridView1[0, j].Value;
                            string text2 = (string)this.dataGridView1[1, j].Value;
                            if (text == null || text2 == null)
                            {
                                MessageBox.Show("请确定上下限的值，其值不能为空");
                                return;
                            }
                            double num = 0.0;
                            double num2 = 0.0;
                            try
                            {
                                num = Convert.ToDouble(text);
                                num2 = Convert.ToDouble(text2);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("请确定上下限的值是否输入有误");
                                return;
                            }
                            int num3 = 0;
                            int num4 = 0;
                            IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[i]).m_pPipeLayer;
                            ISpatialFilter spatialFilter = new SpatialFilter();
                            if (this.GeometrySet.Checked)
                            {
                                if (this.m_ipGeo != null)
                                {
                                    spatialFilter.Geometry = (this.m_ipGeo);
                                }
                                spatialFilter.SpatialRel = (esriSpatialRelEnum)(1);
                            }
                            IFeatureCursor featureCursor = pPipeLayer.FeatureClass.Search(spatialFilter, false);
                            IFeature feature = featureCursor.NextFeature();
                            string name = pPipeLayer.Name;
                            while (feature != null)
                            {
                                if (feature.Shape == null)
                                {
                                    feature = featureCursor.NextFeature();
                                }
                                else
                                {
                                    IPolyline polyline = (IPolyline)feature.Shape;
                                    IPointCollection pointCollection = (IPointCollection)polyline;
                                    IPoint point = pointCollection.get_Point(0);
                                    IPoint point2 = pointCollection.get_Point(1);
                                    double m = point.M;
                                    double m2 = point2.M;
                                    if (m >= num && m < num2 && !this.DXArray.Contains(point))
                                    {
                                        this.DXArray.Add(point);
                                        num3++;
                                    }
                                    if (m2 >= num && m2 < num2 && !this.DXArray.Contains(point2))
                                    {
                                        this.DXArray.Add(point2);
                                        num4++;
                                    }
                                    feature = featureCursor.NextFeature();
                                }
                            }
                            int num5 = num3 + num4;
                            object obj = text + "-" + text2;
                            dataTable.Rows.Add(new object[]
                            {
                                name,
                                obj,
                                num5
                            });
                        }
                    }
                    new ClassCollectResultForm
                    {
                        nType = 0,
                        ResultTable = dataTable
                    }.ShowDialog();
                }
            }
        }

        private void AllBut_Click(object sender, EventArgs e)
        {
            this.minNum = 1000.0;
            this.maxNum = -1000.0;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }
            this.listBox1.Items.Clear();
            int count = this.checkedListBox1.CheckedItems.Count;
            if (count != 0)
            {
                for (int j = 0; j < count; j++)
                {
                    IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
                    this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
                }
            }
            this.all();
        }

        public void all()
        {
            this.listBox1.Items.Clear();
            double num = Convert.ToDouble(this.numericUpDown1.Value);
            if (this.maxNum - this.minNum < 0.0001)
            {
                this.listBox1.Items.Add(this.maxNum);
            }
            else
            {
                double num2 = (this.maxNum - this.minNum) / num;
                int num3 = 0;
                while ((double)num3 < num + 1.0)
                {
                    string text = (this.minNum + (double)num3 * num2).ToString("f2");
                    if (!this.listBox1.Items.Contains(text))
                    {
                        this.listBox1.Items.Add(text);
                    }
                    num3++;
                }
            }
        }

        private void FillFieldValuesToListBox(IFeatureLayer pFeaLay, ListBox lbVal)
        {
            IFeatureClass featureClass = pFeaLay.FeatureClass;
            IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(featureClass);
            IQueryFilter queryFilter = new QueryFilter();
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
            IFeature feature = featureCursor.NextFeature();
            int num = featureClass.Fields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
            int num2 = featureClass.Fields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
            if (num != -1 && num2 != -1)
            {
                double num3 = 2147483647.0;
                double num4 = -2147483648.0;
                this.minNum = 2147483647.0;
                this.maxNum = -2147483648.0;
                while (feature != null)
                {
                    try
                    {
                        object obj = feature.get_Value(num).ToString();
                        object obj2 = feature.get_Value(num2).ToString();
                        if (obj == null || Convert.IsDBNull(obj))
                        {
                            continue;
                        }
                        if (obj2 == null || Convert.IsDBNull(obj2))
                        {
                            continue;
                        }
                        double num5 = Convert.ToDouble(obj);
                        double num6 = Convert.ToDouble(obj2);
                        num3 = ((num3 < num5) ? num3 : num5);
                        num3 = ((num3 < num6) ? num3 : num6);
                        num4 = ((num4 > num5) ? num4 : num5);
                        num4 = ((num4 > num6) ? num4 : num6);
                        feature = featureCursor.NextFeature();
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    if (num3 < this.minNum)
                    {
                        this.minNum = num3;
                    }
                    if (num4 > this.maxNum)
                    {
                        this.maxNum = num4;
                    }
                }
                this.all();
            }
        }

        private void NoneBut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
            this.listBox1.Items.Clear();
        }

        private void RevBut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
                else
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            this.listBox1.Items.Clear();
            int count = this.checkedListBox1.CheckedItems.Count;
            if (count != 0)
            {
                for (int j = 0; j < count; j++)
                {
                    IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[j]).m_pPipeLayer;
                    this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("行数已为空");
            }
            else
            {
                this.dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show("请选定要统计的管线");
            }
            else if (this.listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选定需要的埋深");
            }
            else
            {
                int count = this.dataGridView1.Rows.Count;
                if (count < 1)
                {
                    MessageBox.Show("请用户添加行");
                }
                else
                {
                    string value = this.listBox1.SelectedItem.ToString();
                    double num = Convert.ToDouble(value);
                    int index = this.dataGridView1.CurrentRow.Index;
                    double num2 = Convert.ToDouble(this.dataGridView1[0, index].Value);
                    if (num2 > num)
                    {
                        MessageBox.Show("下限值不应大于上限值");
                    }
                    else
                    {
                        this.dataGridView1.CurrentRow.Cells[1].Value = value;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show("请选定要统计的管线");
            }
            else if (this.listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选定需要的埋深");
            }
            else
            {
                int count = this.dataGridView1.Rows.Count;
                if (count < 1)
                {
                    MessageBox.Show("请用户添加行");
                }
                else
                {
                    string value = this.listBox1.SelectedItem.ToString();
                    double num = Convert.ToDouble(value);
                    int index = this.dataGridView1.CurrentRow.Index;
                    double num2 = Convert.ToDouble(this.dataGridView1[1, index].Value);
                    if (num2 < num && num2 != 0.0)
                    {
                        MessageBox.Show("下限值不应大于上限值");
                    }
                    else
                    {
                        this.dataGridView1.CurrentRow.Cells[0].Value = value;
                    }
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            int count = this.checkedListBox1.CheckedItems.Count;
            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    IFeatureLayer pPipeLayer = ((SimpleStatMyshUI.LayerboxItem)this.checkedListBox1.CheckedItems[i]).m_pPipeLayer;
                    this.FillFieldValuesToListBox(pPipeLayer, this.listBox1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.GeometrySet.Checked = false;
            base.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                this.dataGridView1.Rows.Add();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.all();
        }

        private void InsertBut_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                this.dataGridView1.Rows.Add();
            }
            else
            {
                this.dataGridView1.Rows.Insert(this.dataGridView1.CurrentRow.Index, new object[0]);
            }
        }

        private void SimpleStatMyshUI_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw += AxMapControlOnOnAfterDraw;
            }
            else
            {

                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            }
        }

        private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
        {

            if (viewDrawPhase == 32)
            {
                this.DrawSelGeometry();
            }
        }

        private void SimpleQueryByDiaUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
            axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
            this.OnClosed(e);
        }

        public void DrawSelGeometry()
        {
            if (this.m_ipGeo != null)
            {
                IRgbColor rgbColor = new RgbColor();
                IColor selectionCorlor = this.m_context.Config.SelectionEnvironment.DefaultColor;
                rgbColor.RGB = selectionCorlor.RGB;
                rgbColor.Transparency = selectionCorlor.Transparency;

                object obj = null;
                int selectionBufferInPixels = this.m_context.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
                switch ((int)this.m_ipGeo.GeometryType)
                {
                    case 1:
                        {
                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                            symbol = (ISymbol)simpleMarkerSymbol;
                            symbol.ROP2 = (esriRasterOpCode)(10);
                            simpleMarkerSymbol.Color = (rgbColor);
                            simpleMarkerSymbol.Size = ((double)(selectionBufferInPixels + selectionBufferInPixels + selectionBufferInPixels));
                            break;
                        }
                    case 3:
                        {
                            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                            symbol = (ISymbol)simpleLineSymbol;
                            symbol.ROP2 = (esriRasterOpCode)(10);
                            simpleLineSymbol.Color = (rgbColor);
                            simpleLineSymbol.Color.Transparency = (1);
                            simpleLineSymbol.Width = ((double)selectionBufferInPixels);
                            break;
                        }
                    case 4:
                    case 5:
                        {
                            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                            symbol = (ISymbol)simpleFillSymbol;
                            symbol.ROP2 = (esriRasterOpCode)(10);
                            simpleFillSymbol.Color = (rgbColor);
                            simpleFillSymbol.Color.Transparency = (1);
                            break;
                        }
                }
                obj = symbol;
                this.MapControl.DrawShape(this.m_ipGeo, ref obj);
            }
        }

        private void GeometrySet_Click(object sender, EventArgs e)
        {
            if (!this.GeometrySet.Checked)
            {
                this.m_ipGeo = null;
                m_context.ActiveView.Refresh();
            }
        }

        private void SimpleStatMyshUI_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "分段统计";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }
    }
}
