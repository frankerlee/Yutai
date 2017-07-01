using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleQueryByMaterialUI : Form
    {
        private partial class LayerboxItem
        {
            public IFeatureLayer m_pPipeLayer;

            public override string ToString()
            {
                return this.m_pPipeLayer.Name;
            }
        }

        private PipelineAnalysisPlugin _plugin;

        public PipelineAnalysisPlugin Plugin
        {
            set { _plugin = value; }
        }

        public IGeometry m_ipGeo;
        public IAppContext m_context;
        public IMapControl3 MapControl;
        public IPipelineConfig pPipeCfg;
        public object mainform;
        private List<string> MTArray = new List<string>();

        public bool SelectGeometry
        {
            get { return this.GeometrySet.Checked; }
            set { this.GeometrySet.Checked = value; }
        }

        public SimpleQueryByMaterialUI()
        {
            this.InitializeComponent();
        }

        private void AddLayer(ILayer ipLay)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer) ipLay);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer) ipLay);
            }
        }

        private void AddGroupLayer(IGroupLayer iGLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer) iGLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    ILayer ipLay = compositeLayer.Layer[i];
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
                    SimpleQueryByMaterialUI.LayerboxItem layerboxItem = new SimpleQueryByMaterialUI.LayerboxItem();
                    layerboxItem.m_pPipeLayer = iFLayer;
                    this.LayerBox.Items.Add(layerboxItem);
                }
            }
        }

        private void FillLayerBox()
        {
            this.LayerBox.Items.Clear();
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = m_context.FocusMap.Layer[i];
                this.AddLayer(ipLay);
            }
            if (this.LayerBox.Items.Count > 0)
            {
                this.LayerBox.SelectedIndex = 0;
            }
        }

        private bool ValidateField()
        {
            int selectedIndex = this.LayerBox.SelectedIndex;
            bool result;
            if (selectedIndex < 0)
            {
                result = false;
            }
            else
            {
                this.SelectLayer = null;
                if (this.MapControl == null)
                {
                    result = false;
                }
                else
                {
                    this.SelectLayer = ((SimpleQueryByMaterialUI.LayerboxItem) this.LayerBox.SelectedItem).m_pPipeLayer;
                    if (this.SelectLayer == null)
                    {
                        result = false;
                    }
                    else
                    {
                        IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(this.SelectLayer.FeatureClass);
                        this.myfields = this.SelectLayer.FeatureClass.Fields;
                        this.strMT = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GXCZ);
                            //this.pPipeCfg.GetLineTableFieldName("材质");
                        if (this.myfields.FindField(this.strMT) < 0)
                        {
                            this.QueryBut.Enabled = false;
                            result = false;
                        }
                        else
                        {
                            this.QueryBut.Enabled = true;
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        private void FillValueBox()
        {
            if (this.myfields != null)
            {
                int num = this.myfields.FindField(this.strMT);
                if (num >= 0)
                {
                    this.myfieldDX = this.myfields.Field[num];
                    IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                    this.ValueBox.Items.Clear();
                    List<string> values = new List<string>();
                    CommonHelper.GetUniqueValues((ITable) featureClass, strMT, values);
                    this.ValueBox.Items.AddRange(values.ToArray());
                }
            }
        }

        private void SimpleQueryByMaterialUI_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        public void AutoFlash()
        {
            this.FillLayerBox();
        }

        private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.ValueBox.SelectedIndex;
            string text = "";
            string item = this.ValueBox.SelectedItem.ToString();
            if (this.ValueBox.GetItemChecked(selectedIndex))
            {
                if (!this.MTArray.Contains(item))
                {
                    this.MTArray.Add(item);
                }
            }
            else if (this.MTArray.Contains(item))
            {
                this.MTArray.Remove(item);
            }
            foreach (string current in this.MTArray)
            {
                text += current;
                text += " ";
            }
            this.SqlBox.Text = text;
        }

        private void AllBut_Click(object sender, EventArgs e)
        {
            this.MTArray.Clear();
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                this.MTArray.Add(this.ValueBox.Items[i].ToString());
                this.ValueBox.SetItemChecked(i, true);
            }
            string text = "";
            foreach (string current in this.MTArray)
            {
                text += current;
                text += " ";
            }
            this.SqlBox.Text = text;
        }

        private void NoneBut_Click(object sender, EventArgs e)
        {
            this.MTArray.Clear();
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                this.ValueBox.SetItemChecked(i, false);
            }
            this.SqlBox.Text = "";
        }

        private void RevBut_Click(object sender, EventArgs e)
        {
            this.MTArray.Clear();
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                if (this.ValueBox.GetItemChecked(i))
                {
                    this.ValueBox.SetItemChecked(i, false);
                }
                else
                {
                    this.MTArray.Add(this.ValueBox.Items[i].ToString());
                    this.ValueBox.SetItemChecked(i, true);
                }
            }
            string text = "";
            foreach (string current in this.MTArray)
            {
                text += current;
                text += " ";
            }
            this.SqlBox.Text = text;
        }

        private void QueryBut_Click(object sender, EventArgs e)
        {
            IFeatureClass featureClass = this.SelectLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilter();
            string text = "";
            int count = this.MTArray.Count;
            int num = 1;
            foreach (string current in this.MTArray)
            {
                text += this.strMT;
                text += " = '";
                text += current;
                text += "'";
                if (num < count)
                {
                    text += " OR ";
                }
                num++;
            }
            spatialFilter.WhereClause = text;
            if (this.GeometrySet.Checked)
            {
                if (this.m_ipGeo != null)
                {
                    spatialFilter.Geometry = (this.m_ipGeo);
                }
                spatialFilter.SpatialRel = (esriSpatialRelEnum) (1);
            }
            IFeatureCursor pCursor = featureClass.Search(spatialFilter, false);
            //修改为插件事件，因为结果显示窗体为插件拥有。
            _plugin.FireQueryResultChanged(new QueryResultArgs(pCursor, (IFeatureSelection) this.SelectLayer));
        }

        private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MTArray.Clear();
            this.SqlBox.Text = "";
        }

        private void CloseBut_Click(object sender, EventArgs e)
        {
            if (this.SelectLayer != null)
            {
                IFeatureSelection featureSelection = (IFeatureSelection) this.SelectLayer;
                featureSelection.Clear();
                featureSelection.SelectionSet.Refresh();
                IActiveView activeView = m_context.ActiveView;
                activeView.Refresh();
            }
            this.GeometrySet.Checked = false;
            base.Close();
        }

        private void SimpleQueryByMaterialUI_VisibleChanged(object sender, EventArgs e)
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
            base.OnClosed(e);
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
                switch ((int) m_ipGeo.GeometryType)
                {
                    case 1:
                    {
                        ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                        symbol = (ISymbol) simpleMarkerSymbol;
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                        simpleMarkerSymbol.Color = rgbColor;
                        simpleMarkerSymbol.Size = selectionBufferInPixels + selectionBufferInPixels +
                                                  selectionBufferInPixels;
                        break;
                    }
                    case 3:
                    {
                        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                        symbol = (ISymbol) simpleLineSymbol;
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                        simpleLineSymbol.Color = rgbColor;
                        simpleLineSymbol.Color.Transparency = 1;
                        simpleLineSymbol.Width = selectionBufferInPixels;
                        break;
                    }
                    case 4:
                    case 5:
                    {
                        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                        symbol = (ISymbol) simpleFillSymbol;
                        symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                        simpleFillSymbol.Color = rgbColor;
                        simpleFillSymbol.Color.Transparency = 1;
                        break;
                    }
                }
                obj = symbol;
                this.MapControl.DrawShape(this.m_ipGeo, ref obj);
            }
        }

        private void SimpleQueryByMaterialUI_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "快速查询";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }

        private void BtnGetUniqueValue_Click(object sender, EventArgs e)
        {
            if (!this.ValidateField())
            {
                MessageBox.Show(@"配置文件有误，请检查！");
            }
            else
            {
                this.FillValueBox();
            }
        }
    }
}