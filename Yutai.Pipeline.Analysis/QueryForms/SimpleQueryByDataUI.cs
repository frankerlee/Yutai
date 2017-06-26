using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleQueryByDataUI : Form
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
            set
            {
                _plugin = value;
            }
        }
        public IGeometry m_ipGeo;
        public IAppContext m_context;
        public IMapControl3 MapControl;
        public IPipelineConfig pPipeCfg;
        public object mainform;
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

        public SimpleQueryByDataUI()
        {
            this.InitializeComponent();
        }

        private void SimpleQueryByDataUI_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        public void AutoFlash()
        {
            this.OperateBox.SelectedIndex = 0;
            this.FillLayerBox();
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
                if (this.radioButton1.Checked)
                {
                    if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Point))
                    {
                        SimpleQueryByDataUI.LayerboxItem layerboxItem = new SimpleQueryByDataUI.LayerboxItem();
                        layerboxItem.m_pPipeLayer = iFLayer;
                        this.LayerBox.Items.Add(layerboxItem);
                    }
                }
                else if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
                {
                    SimpleQueryByDataUI.LayerboxItem layerboxItem2 = new SimpleQueryByDataUI.LayerboxItem();
                    layerboxItem2.m_pPipeLayer = iFLayer;
                    this.LayerBox.Items.Add(layerboxItem2);
                }
            }
        }

        private void FillLayerBox()
        {
            this.LayerBox.Items.Clear();
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = m_context.FocusMap.get_Layer(i);
                this.AddLayer(ipLay);
            }
            if (this.LayerBox.Items.Count > 0)
            {
                this.LayerBox.SelectedIndex = 0;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.FillLayerBox();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.FillLayerBox();
        }

        private void OperateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.OperateBox.SelectedItem.ToString() == "介于")
            {
                this.label2.Visible = true;
                this.dateTimePicker2.Visible = true;
            }
            else
            {
                this.label2.Visible = false;
                this.dateTimePicker2.Visible = false;
            }
        }

        private void CloseBut_Click(object sender, EventArgs e)
        {
            if (this.SelectLayer != null)
            {
                IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
                featureSelection.Clear();
                featureSelection.SelectionSet.Refresh();
                IActiveView activeView = m_context.ActiveView;
                activeView.Refresh();
            }
            this.GeometrySet.Checked = false;
            base.Close();
        }

        private void QueryBut_Click(object sender, EventArgs e)
        {
            string text = "";
            int selectedIndex = this.LayerBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                this.SelectLayer = null;
                if (this.MapControl != null)
                {
                    this.SelectLayer = ((SimpleQueryByDataUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
                    if (this.SelectLayer != null)
                    {
                        this.myfields = this.SelectLayer.FeatureClass.Fields;
                        IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(this.SelectLayer.FeatureClass);
                        string text2;
                        if (this.radioButton1.Checked)
                        {

                            text2 = layerInfo.GetFieldName(PipeConfigWordHelper.PointWords.MSRQ); ;// this.pPipeCfg.GetPointTableFieldName("建设年代");
                        }
                        else
                        {

                            text2 = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.MSRQ);// this.pPipeCfg.GetLineTableFieldName("建设年代");
                        }
                        int num = this.myfields.FindField(text2);
                        if (num < 0)
                        {
                            MessageBox.Show("图层中日期字段没有找到！请检查配置文件！");
                        }
                        else
                        {
                            IField field = this.myfields.get_Field(num);
                            if (field.Type == (esriFieldType)4)
                            {
                                switch (this.OperateBox.SelectedIndex)
                                {
                                    case 0:
                                        text = text2;
                                        text += "<= '";
                                        text += this.dateTimePicker1.Value.ToShortDateString();
                                        text += "'";
                                        break;
                                    case 1:
                                        text = text2;
                                        text += ">= '";
                                        text += this.dateTimePicker1.Value.ToShortDateString();
                                        text += "'";
                                        break;
                                    case 2:
                                        text = text2;
                                        text += "= '";
                                        text += this.dateTimePicker1.Value.ToShortDateString();
                                        text += "'";
                                        break;
                                    case 3:
                                        text = text2;
                                        text += ">= '";
                                        text += this.dateTimePicker1.Value.ToShortDateString();
                                        text += "'and ";
                                        text += text2;
                                        text += "<= '";
                                        text += this.dateTimePicker2.Value.ToShortDateString();
                                        text += "'";
                                        break;
                                }
                            }
                            else if (field.Type == (esriFieldType)5)
                            {
                                if (this.SelectLayer.DataSourceType == "Personal Geodatabase Feature Class")
                                {
                                    switch (this.OperateBox.SelectedIndex)
                                    {
                                        case 0:
                                            text = text2;
                                            text += "<= #";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "#";
                                            break;
                                        case 1:
                                            text = text2;
                                            text += ">= #";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "#";
                                            break;
                                        case 2:
                                            text = text2;
                                            text += "= #";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "#";
                                            break;
                                        case 3:
                                            text = text2;
                                            text += ">= #";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "# and ";
                                            text += text2;
                                            text += "<= #";
                                            text += this.dateTimePicker2.Value.ToShortDateString();
                                            text += "#";
                                            break;
                                    }
                                }
                                if (this.SelectLayer.DataSourceType == "SDE Feature Class")
                                {
                                    switch (this.OperateBox.SelectedIndex)
                                    {
                                        case 0:
                                            text = text2;
                                            text += "<= TO_DATE('";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "','YYYY-MM-DD')";
                                            break;
                                        case 1:
                                            text = text2;
                                            text += ">= TO_DATE('";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "','YYYY-MM-DD')";
                                            break;
                                        case 2:
                                            text = text2;
                                            text += "= TO_DATE('";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "','YYYY-MM-DD')";
                                            break;
                                        case 3:
                                            text = text2;
                                            text += ">= TO_DATE('";
                                            text += this.dateTimePicker1.Value.ToShortDateString();
                                            text += "','YYYY-MM-DD') and ";
                                            text += text2;
                                            text += "<= TO_DATE('";
                                            text += this.dateTimePicker2.Value.ToShortDateString();
                                            text += "','YYYY-MM-DD')";
                                            break;
                                    }
                                }
                            }
                            IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                            ISpatialFilter spatialFilter = new SpatialFilter();
                            spatialFilter.WhereClause = text;
                            if (this.GeometrySet.Checked)
                            {
                                if (this.m_ipGeo != null)
                                {
                                    spatialFilter.Geometry = (this.m_ipGeo);
                                }
                                spatialFilter.SpatialRel = (esriSpatialRelEnum)(1);
                            }
                            IFeatureCursor pCursor = featureClass.Search(spatialFilter, false);
                            //修改为插件事件，因为结果显示窗体为插件拥有。
                            _plugin.FireQueryResultChanged(new QueryResultArgs(pCursor, (IFeatureSelection)this.SelectLayer));
                        }
                    }
                }
            }
        }

        private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SimpleQueryByDataUI_VisibleChanged(object sender, EventArgs e)
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

        private void SimpleQueryByDataUI_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "快速查询";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }
    }
}