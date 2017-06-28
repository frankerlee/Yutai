
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class ComplexQueryUI : Form
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

        public IMapControl3 MapControl;
        public IPipelineConfig pPipeCfg;
        public IGeometry newGeometry;
        private bool bFirst = true;
        public int DrawType = 1;
        public IGeometry m_ipGeo;
        public IAppContext m_context;
        public object mainform;
        public IGeometry m_OriginGeo;

        public bool SelectGeometry
        {
            get
            {
                return this.bGeo.Checked;
            }
        }

        public double GlacisNum
        {
            get
            {
                return (double)this.GlacisUpDown.Value;
            }
            set
            {
                this.GlacisUpDown.Value = (decimal)value;
            }
        }

        private void MakeSelectionSetForSearch(IFeatureLayer pFeaLay)
        {
            if (this.chkBoxTwice.Checked && this.m_pSelectionSetForSearch != null)
            {
                IFeatureSelection featureSelection = (IFeatureSelection)pFeaLay;
                this.m_pSelectionSetForSearch = featureSelection.SelectionSet;
            }
            else if (pFeaLay == null)
            {
                MessageBox.Show("FeatureClass为空");
            }
            else
            {
                IFeatureClass featureClass = pFeaLay.FeatureClass;
                IQueryFilter queryFilter = new QueryFilter();
                ISelectionSet pSelectionSetForSearch = featureClass.Select(queryFilter, (esriSelectionType)1, (esriSelectionOption)1, null);
                this.m_pSelectionSetForSearch = pSelectionSetForSearch;
            }
        }

        public ComplexQueryUI()
        {
            this.InitializeComponent();
        }

        ~ComplexQueryUI()
        {
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
                iFLayer.Name.ToString();
                ComplexQueryUI.LayerboxItem layerboxItem = new ComplexQueryUI.LayerboxItem();
                layerboxItem.m_pPipeLayer = iFLayer;
                this.LayerBox.Items.Add(layerboxItem);
            }
        }

        private void Fill()
        {
            this.LayerBox.Items.Clear();
            int layerCount = this.MapControl.LayerCount;
            if (layerCount >= 1)
            {
                for (int i = 0; i < layerCount; i++)
                {
                    ILayer ipLay = m_context.FocusMap.get_Layer(i);
                    this.AddLayer(ipLay);
                }
                this.LayerBox.SelectedIndex = 0;
            }
        }

        private void FillField()
        {
            this.LayerBox.SelectedItem.ToString();
            if (this.MapControl != null)
            {
                this.SelectLayer = ((ComplexQueryUI.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
                if (this.SelectLayer != null)
                {
                    this.myfields = this.SelectLayer.FeatureClass.Fields;
                    this.FieldBox.Items.Clear();
                    for (int i = 0; i < this.myfields.FieldCount; i++)
                    {
                        IField field = this.myfields.get_Field(i);
                        string name = field.Name;
                        if (field.Type != (esriFieldType)6 && field.Type != (esriFieldType)7 && !(name.ToUpper() == "ENABLED") && !(name.ToUpper() == "SHAPE.LEN") && !(name.ToUpper() == "SHAPE.AREA"))
                        {
                            this.FieldBox.Items.Add(name);
                        }
                    }
                    if (this.FieldBox.Items.Count > 0)
                    {
                        this.FieldBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ValueEdit.Text = "";
                        this.ValueBox.Items.Clear();
                    }
                }
            }
        }

        private void FillValue()
        {
            if (this.myfields != null)
            {
                if (this.FieldBox.Items.Count >= 1)
                {
                    int num = this.myfields.FindField(this.FieldBox.SelectedItem.ToString());
                    this.myfield = this.myfields.get_Field(num);
                    if (this.myfield.Type == (esriFieldType)4)
                    {
                        this.BigeRadio.Enabled = false;
                        this.BigRadio.Enabled = false;
                        this.SmallRadio.Enabled = false;
                        this.SmalleRadio.Enabled = false;
                        this.Likeradio.Enabled = true;
                    }
                    else
                    {
                        this.BigeRadio.Enabled = true;
                        this.BigRadio.Enabled = true;
                        this.SmalleRadio.Enabled = true;
                        this.SmallRadio.Enabled = true;
                        this.Likeradio.Enabled = false;
                    }
                    this.Equradio.Checked = true;
                    IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                    IQueryFilter queryFilter = new QueryFilter();
                    IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                    IFeature feature = featureCursor.NextFeature();
                    this.ValueBox.Items.Clear();
                    while (feature != null)
                    {
                        object obj = feature.get_Value(num);
                        string text;
                        if (obj is DBNull)
                        {
                            text = "NULL";
                        }
                        else if (this.myfield.Type == (esriFieldType)5)
                        {
                            text = Convert.ToDateTime(obj).ToShortDateString();
                        }
                        else
                        {
                            text = obj.ToString();
                            if (text.Length == 0)
                            {
                                text = "空字段值";
                            }
                        }
                        if (!this.ValueBox.Items.Contains(text))
                        {
                            this.ValueBox.Items.Add(text);
                        }
                        if (this.ValueBox.Items.Count > 100)
                        {
                            break;
                        }
                        feature = featureCursor.NextFeature();
                    }
                }
            }
        }

        private void ComplexQueryUI_Load(object sender, EventArgs e)
        {
            this.Fill();
            this.FillField();
            this.FillValue();
        }

        public void AutoFlash()
        {
            this.Fill();
            this.FillField();
            this.FillValue();
        }

        private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillField();
            this.SelectText.Text = "";
            this.SqlBox.Text = "";
            this.bFirst = true;
        }

        private void FieldBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillValue();
            this.ValueEdit.Text = "";
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (this.SelectLayer != null)
            {
                IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
                featureSelection.Clear();
                featureSelection.SelectionSet.Refresh();
                IActiveView activeView = m_context.ActiveView;
                this.SqlBox.Text = "";
                this.SelectText.Text = "";
                this.bFirst = true;
                this.m_ipGeo = null;
                this.m_OriginGeo = null;
                this.GlacisUpDown.Value = 0m;
                activeView.Refresh();
            }
            this.bGeo.Checked = false;
            base.Close();
        }

        private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValueEdit.Text = this.ValueBox.SelectedItem.ToString();
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            string text = "*";
            IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
            if (dataset.Workspace.Type == (esriWorkspaceType)2)
            {
                text = "%";
            }
            string text2 = this.SqlBox.Text;
            if (this.bFirst)
            {
                this.bFirst = false;
                this.SelectText.Text = "select * from ";
                TextBox selectText = this.SelectText;
                TextBox expr_73 = selectText;
                expr_73.Text += this.LayerBox.SelectedItem.ToString();
                TextBox selectText2 = this.SelectText;
                TextBox expr_9E = selectText2;
                expr_9E.Text += " where ";
            }
            else if (this.AndRaio.Checked)
            {
                text2 += " and ";
            }
            else
            {
                text2 += " or ";
            }
            string text3 = this.FieldBox.SelectedItem.ToString();
            string text4 = this.ValueEdit.Text;
            if (text4 == "NULL")
            {
                if (this.Equradio.Checked)
                {
                    text2 += text3;
                    text2 += " IS NULL";
                }
                if (this.Noequradio.Checked)
                {
                    text2 = text2 + "NOT(" + text3 + " IS NULL)";
                }
                if (this.Likeradio.Checked)
                {
                    text2 += text3;
                    text2 += " LIKE NULL";
                }
            }
            else
            {
                text2 += text3;
                if (this.Equradio.Checked)
                {
                    text2 += "=";
                }
                if (this.Noequradio.Checked)
                {
                    text2 += "<>";
                }
                if (this.SmallRadio.Checked)
                {
                    text2 += "<";
                }
                if (this.SmalleRadio.Checked)
                {
                    text2 += "<=";
                }
                if (this.BigRadio.Checked)
                {
                    text2 += ">";
                }
                if (this.BigeRadio.Checked)
                {
                    text2 += ">=";
                }
                if (this.Likeradio.Checked)
                {
                    text2 += " like ";
                }
                if (text4 == "空字段值")
                {
                    text4 = "";
                }
                if (this.myfield.Type == (esriFieldType)4)
                {
                    if (this.Likeradio.Checked)
                    {
                        text2 = text2 + "'" + text;
                        text2 += text4;
                        text2 = text2 + text + "'";
                    }
                    else
                    {
                        text2 += "'";
                        text2 += text4;
                        text2 += "'";
                    }
                }
                else if (this.myfield.Type == (esriFieldType)5)
                {
                    if (this.SelectLayer.DataSourceType == "SDE Feature Class")
                    {
                        text2 += "TO_DATE('";
                        text2 += text4;
                        text2 += "','YYYY-MM-DD')";
                    }
                    if (this.SelectLayer.DataSourceType == "Personal Geodatabase Feature Class")
                    {
                        text2 += "#";
                        text2 += text4;
                        text2 += "#";
                    }
                }
                else
                {
                    text2 += text4;
                }
            }
            this.SqlBox.Text = text2;
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            this.SqlBox.Clear();
            this.SelectText.Clear();
            this.bFirst = true;
            this.bGeo.Checked = false;
            this.m_ipGeo = null;
        }

        private void ComplexQueryUI_VisibleChanged(object sender, EventArgs e)
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

        private void QueryButton_Click(object sender, EventArgs e)
        {
            ISpatialFilter spatialFilter = new SpatialFilter();
            IFeatureCursor featureCursor = null;
            if (!(this.SqlBox.Text == "") || MessageBox.Show("末指定属性条件,是否查询?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                spatialFilter.WhereClause = this.SqlBox.Text;
                if (this.bGeo.Checked && this.m_ipGeo != null)
                {
                    if (this.GlacisUpDown.Value > 0m)
                    {
                        ITopologicalOperator topologicalOperator = (ITopologicalOperator)this.m_OriginGeo;
                        IGeometry ipGeo = topologicalOperator.Buffer((double)this.GlacisUpDown.Value);
                        this.m_ipGeo = ipGeo;
                        spatialFilter.Geometry = (this.m_ipGeo);
                    }
                    else
                    {
                        spatialFilter.Geometry = (this.m_OriginGeo);
                    }
                }
                if (this.SelectType == 0)
                {
                    spatialFilter.SpatialRel = (esriSpatialRelEnum)(1);
                }
                if (this.SelectType == 1)
                {
                    spatialFilter.SpatialRel = (esriSpatialRelEnum)(7);
                }
                try
                {
                    this.MakeSelectionSetForSearch(this.SelectLayer);
                    ICursor cursor = featureCursor as ICursor;
                    this.m_pSelectionSetForSearch.Search(spatialFilter, false, out cursor);
                    if (cursor is IFeatureCursor)
                    {
                        featureCursor = (cursor as IFeatureCursor);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("查询值有误,请检查!");
                    return;
                }
                _plugin.FireQueryResultChanged(new QueryResultArgs(featureCursor, (IFeatureSelection)this.SelectLayer));
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                IMapControlEvents2_Event axMapControl = this.m_context.MapControl as IMapControlEvents2_Event;
                axMapControl.OnAfterDraw -= AxMapControlOnOnAfterDraw;
                IFeatureSelection featureSelection = (IFeatureSelection)this.SelectLayer;
                featureSelection.Clear();
                featureSelection.SelectionSet.Refresh();
                IActiveView activeView = m_context.ActiveView;
                activeView.Refresh();
                this.SqlBox.Text = "";
                this.SelectText.Text = "";
                this.bGeo.Checked = false;
                this.bFirst = true;
                this.m_ipGeo = null;
                base.OnClosed(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ValueBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.AddItem_Click(null, null);
        }

        private void GlacisUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.bGeo.Checked && this.m_OriginGeo != null)
            {
                if (this.GlacisUpDown.Value > 0m)
                {
                    try
                    {
                        ITopologicalOperator topologicalOperator = (ITopologicalOperator)this.m_OriginGeo;
                        IGeometry geometry = topologicalOperator.Buffer((double)this.GlacisUpDown.Value);
                        if (geometry != null)
                        {
                            this.m_ipGeo = geometry;
                        }
                        else
                        {
                            MessageBox.Show("复杂多边形,创建缓冲区失败!");
                            this.m_ipGeo = this.m_OriginGeo;
                        }
                        goto IL_CF;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("复杂多边形,创建缓冲区失败!");
                        this.GlacisUpDown.Value = 0m;
                        this.m_ipGeo = this.m_OriginGeo;
                        goto IL_CF;
                    }
                }
                this.m_ipGeo = this.m_OriginGeo;
                IL_CF:
                IActiveView activeView = m_context.ActiveView;
                activeView.Refresh();
            }
        }

        private void bGeo_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.bGeo.Checked)
            {
                this.m_ipGeo = null;
                this.m_OriginGeo = null;
                this.MapControl.Refresh((esriViewDrawPhase)32, null, null);
            }
        }

        private void ValueEdit_TextChanged(object sender, EventArgs e)
        {
        }

        private void ComplexQueryUI_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "复合查询";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }
    }
}
