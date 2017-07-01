using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class SimpleQueryByAddressUI : Form
    {
        public IGeometry m_ipGeo;

        public IAppContext m_context;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;


        private PipelineAnalysisPlugin _plugin;

        public PipelineAnalysisPlugin Plugin
        {
            set { _plugin = value; }
        }


        private List<string> ADArray = new List<string>();


        public SimpleQueryByAddressUI()
        {
            this.InitializeComponent();
        }

        private void FillLayerBox()
        {
            this.LayerBox.Items.Clear();
            this.ADArray.Clear();
            this.SqlBox.Text = "";
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer layer = m_context.FocusMap.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    ICompositeLayer compositeLayer = (ICompositeLayer) layer;
                    if (compositeLayer == null)
                    {
                        return;
                    }
                    int count = compositeLayer.Count;
                    for (int j = 0; j < count; j++)
                    {
                        ILayer layer2 = compositeLayer.get_Layer(j);
                        string text = layer2.Name.ToString();
                        if (this.radioButton1.Checked)
                        {
                            if (this.pPipeCfg.IsPipelineLayer(layer2.Name, enumPipelineDataType.Point))
                            {
                                this.LayerBox.Items.Add(text);
                            }
                        }
                        else if (this.pPipeCfg.IsPipelineLayer(text, enumPipelineDataType.Line))
                        {
                            this.LayerBox.Items.Add(text);
                        }
                    }
                }
                else if (layer is IFeatureLayer)
                {
                    string text = layer.Name.ToString();
                    if (this.radioButton1.Checked)
                    {
                        if (this.pPipeCfg.GetBasicLayerInfo(((IFeatureLayer) layer).FeatureClass) != null)
                        {
                            this.LayerBox.Items.Add(text);
                        }
                    }
                    else if (this.pPipeCfg.GetBasicLayerInfo(((IFeatureLayer) layer).FeatureClass) != null)
                    {
                        this.LayerBox.Items.Add(text);
                    }
                }
            }
            if (this.LayerBox.Items.Count > 0)
            {
                this.LayerBox.SelectedIndex = 0;
                return;
            }
        }

        private bool ValidateField()
        {
            int layerCount = m_context.FocusMap.LayerCount;
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
                    string b = this.LayerBox.SelectedItem.ToString();
                    for (int i = 0; i < layerCount; i++)
                    {
                        ILayer layer = m_context.FocusMap.get_Layer(i);
                        if (layer is IFeatureLayer)
                        {
                            string a = layer.Name.ToString();
                            if (a == b)
                            {
                                this.SelectLayer = (IFeatureLayer) m_context.FocusMap.get_Layer(i);
                                break;
                            }
                        }
                        else if (layer is IGroupLayer)
                        {
                            ICompositeLayer compositeLayer = (ICompositeLayer) layer;
                            if (compositeLayer != null)
                            {
                                int count = compositeLayer.Count;
                                for (int j = 0; j < count; j++)
                                {
                                    ILayer layer2 = compositeLayer.get_Layer(j);
                                    string a = layer2.Name.ToString();
                                    if (a == b)
                                    {
                                        this.SelectLayer = (IFeatureLayer) layer2;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (this.SelectLayer == null)
                    {
                        result = false;
                    }
                    else
                    {
                        this.myfields = this.SelectLayer.FeatureClass.Fields;
                        IBasicLayerInfo layerInfo = pPipeCfg.GetBasicLayerInfo(this.SelectLayer.FeatureClass);
                        if (this.radioButton1.Checked)
                        {
                            this.FieldBox.Text = layerInfo.GetFieldName(PipeConfigWordHelper.PointWords.QSDW);
                            // this.FieldBox.Text = this.pPipeCfg.GetPointTableFieldName("所属");
                        }
                        else
                        {
                            this.FieldBox.Text = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.SZDL);
                        }
                        if (this.myfields.FindField(this.FieldBox.Text) < 0)
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
                int num = this.myfields.FindField(this.FieldBox.Text);
                if (num >= 0)
                {
                    IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                    IQueryFilter queryFilter = new QueryFilter();
                    IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                    IFeature feature = featureCursor.NextFeature();
                    this.ValueBox.Items.Clear();
                    while (feature != null)
                    {
                        object obj = feature.get_Value(num);
                        if (obj is DBNull)
                        {
                            feature = featureCursor.NextFeature();
                        }
                        else
                        {
                            string text = obj.ToString();
                            if (text.Length == 0)
                            {
                                feature = featureCursor.NextFeature();
                            }
                            else
                            {
                                if (!this.ValueBox.Items.Contains(text))
                                {
                                    this.ValueBox.Items.Add(text);
                                }
                                feature = featureCursor.NextFeature();
                            }
                        }
                    }
                }
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

        private void SimpleQueryByAddressUI_Load(object sender, EventArgs e)
        {
            this.FillLayerBox();
            if (this.ValidateField())
            {
                this.FillValueBox();
            }
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
            base.Close();
        }

        private void LayerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ValidateField())
            {
                this.FillValueBox();
            }
        }

        private void AllBut_Click(object sender, EventArgs e)
        {
            this.ADArray.Clear();
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                this.ADArray.Add(this.ValueBox.Items[i].ToString());
                this.ValueBox.SetItemChecked(i, true);
            }
            string text = "";
            foreach (string current in this.ADArray)
            {
                text += current;
                text += " ";
            }
            this.SqlBox.Text = text;
        }

        private void NoneBut_Click(object sender, EventArgs e)
        {
            this.ADArray.Clear();
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                this.ValueBox.SetItemChecked(i, false);
            }
            this.SqlBox.Text = "";
        }

        private void RevBut_Click(object sender, EventArgs e)
        {
            this.ADArray.Clear();
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                if (this.ValueBox.GetItemChecked(i))
                {
                    this.ValueBox.SetItemChecked(i, false);
                }
                else
                {
                    this.ADArray.Add(this.ValueBox.Items[i].ToString());
                    this.ValueBox.SetItemChecked(i, true);
                }
            }
            string text = "";
            foreach (string current in this.ADArray)
            {
                text += current;
                text += " ";
            }
            this.SqlBox.Text = text;
        }

        private void ValueBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.ValueBox.SelectedIndex;
            string text = "";
            string item = this.ValueBox.SelectedItem.ToString();
            if (this.ValueBox.GetItemChecked(selectedIndex))
            {
                if (!this.ADArray.Contains(item))
                {
                    this.ADArray.Add(item);
                }
            }
            else if (this.ADArray.Contains(item))
            {
                this.ADArray.Remove(item);
            }
            foreach (string current in this.ADArray)
            {
                text += current;
                text += " ";
            }
            this.SqlBox.Text = text;
        }

        private void BlurCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BlurCheck.Checked)
            {
                this.SqlBox.ReadOnly = false;
                this.SqlBox.Text = "";
                this.ValueBox.Enabled = false;
                this.AllBut.Enabled = false;
                this.NoneBut.Enabled = false;
                this.RevBut.Enabled = false;
                this.NoneBut_Click(null, null);
            }
            else
            {
                this.SqlBox.ReadOnly = true;
                this.SqlBox.Text = "";
                this.ValueBox.Enabled = true;
                this.AllBut.Enabled = true;
                this.NoneBut.Enabled = true;
                this.RevBut.Enabled = true;
            }
        }

        private void QueryBut_Click(object sender, EventArgs e)
        {
            IFeatureClass featureClass = this.SelectLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilter();
            string text = "";
            if (!this.BlurCheck.Checked)
            {
                int count = this.ADArray.Count;
                int num = 1;
                using (List<string>.Enumerator enumerator = this.ADArray.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        text += this.FieldBox.Text;
                        text += " = \"";
                        text += current;
                        text += "\"";
                        if (num < count)
                        {
                            text += " OR ";
                        }
                        num++;
                    }
                    goto IL_101;
                }
            }
            text = this.FieldBox.Text;
            text += " LIKE \"*";
            text += this.SqlBox.Text;
            text += "*\"";
            IL_101:
            spatialFilter.WhereClause = text;
            IFeatureCursor pFeatureCursor = featureClass.Search(spatialFilter, false);
            _plugin.FireQueryResultChanged(new QueryResultArgs(pFeatureCursor, this.SelectLayer as IFeatureSelection));
        }

        private void SimpleQueryByAddressUI_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "快速查询";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }
    }
}