
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
    public partial class SimpleQueryByAddressUI1 : Form
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
        private string FindField = "";
        private string FindField1 = "";
        public object mainform;

        private bool bUnWipe = true;

        public SimpleQueryByAddressUI1()
        {
            this.InitializeComponent();
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
                        SimpleQueryByAddressUI1.LayerboxItem layerboxItem = new SimpleQueryByAddressUI1.LayerboxItem();
                        layerboxItem.m_pPipeLayer = iFLayer;
                        this.LayerBox.Items.Add(layerboxItem);
                    }
                }
                else if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
                {
                    SimpleQueryByAddressUI1.LayerboxItem layerboxItem2 = new SimpleQueryByAddressUI1.LayerboxItem();
                    layerboxItem2.m_pPipeLayer = iFLayer;
                    this.LayerBox.Items.Add(layerboxItem2);
                }
            }
        }

        private void FillLayerBox()
        {
            this.LayerBox.Items.Clear();
            this.FieldValueBox.Items.Clear();
            this.ValueBox.Items.Clear();
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer ipLay = m_context.FocusMap.get_Layer(i);
                this.AddLayer(ipLay);
                if (this.LayerBox.Items.Count > 0)
                {
                    this.LayerBox.SelectedIndex = 0;
                }
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
                    this.SelectLayer = ((SimpleQueryByAddressUI1.LayerboxItem)this.LayerBox.SelectedItem).m_pPipeLayer;
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

                            this.FieldBox.Text = layerInfo.GetFieldName(PipeConfigWordHelper.PointWords.SZDL);// this.pPipeCfg.GetPointTableFieldName("所在道路");
                            this.FindField = layerInfo.GetFieldName(PipeConfigWordHelper.PointWords.TZW);// this.pPipeCfg.GetPointTableFieldName("点性");
                        }
                        else
                        {

                            this.FieldBox.Text = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.SZDL);// this.pPipeCfg.GetLineTableFieldName("所在道路");
                            this.FindField = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.GJ);// this.pPipeCfg.GetLineTableFieldName("管径");
                            this.FindField1 = layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.DMCC);// this.pPipeCfg.GetLineTableFieldName("断面尺寸");
                        }
                        if (this.myfields.FindField(this.FindField) < 0 && this.myfields.FindField(this.FindField1) < 0)
                        {
                            this.ValueBox.Enabled = false;
                            this.AllBut.Enabled = false;
                            this.NoneBut.Enabled = false;
                            this.RevBut.Enabled = false;
                        }
                        else
                        {
                            this.ValueBox.Enabled = true;
                            this.AllBut.Enabled = true;
                            this.NoneBut.Enabled = true;
                            this.RevBut.Enabled = true;
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
                int num = -1;
                this.bUnWipe = true;
                int num2 = this.myfields.FindField(this.FieldBox.Text);
                if (num2 >= 0)
                {
                    IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                    IQueryFilter queryFilter = new QueryFilter();
                    IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                    IFeature feature = featureCursor.NextFeature();
                    this.FieldValueBox.Items.Clear();
                    this.ValueBox.Items.Clear();
                    int num3 = this.myfields.FindField(this.FindField);
                    if (!this.radioButton1.Checked)
                    {
                        num = this.myfields.FindField(this.FindField1);
                    }
                    while (feature != null)
                    {
                        object obj = feature.get_Value(num2);
                        if (num3 > 0)
                        {
                            object obj2 = feature.get_Value(num3);
                            if (!(obj2 is DBNull))
                            {
                                string text = obj2.ToString();
                                if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
                                {
                                    this.ValueBox.Items.Add(text);
                                }
                            }
                        }
                        if (num > 0)
                        {
                            object obj2 = feature.get_Value(num);
                            if (!(obj2 is DBNull))
                            {
                                string text = obj2.ToString();
                                if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
                                {
                                    this.ValueBox.Items.Add(text);
                                }
                            }
                        }
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
                                if (!this.FieldValueBox.Items.Contains(text))
                                {
                                    this.FieldValueBox.Items.Add(text);
                                }
                                feature = featureCursor.NextFeature();
                            }
                        }
                    }
                    if (this.FieldValueBox.Items.Count > 0)
                    {
                        this.FieldValueBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void SimpleQueryByAddressUI1_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        public void AutoFlash()
        {
            this.FillLayerBox();
            if (this.ValidateField())
            {
                this.FillValueBox();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Text = "选择查询对象：点性";
            this.FieldValueBox.Text = "";
            this.FillLayerBox();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Text = "选择查询对象：管径/沟截面宽高";
            this.FieldValueBox.Text = "";
            this.FillLayerBox();
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
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                this.ValueBox.SetItemChecked(i, true);
            }
        }

        private void NoneBut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                this.ValueBox.SetItemChecked(i, false);
            }
        }

        private void RevBut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ValueBox.Items.Count; i++)
            {
                if (this.ValueBox.GetItemChecked(i))
                {
                    this.ValueBox.SetItemChecked(i, false);
                }
                else
                {
                    this.ValueBox.SetItemChecked(i, true);
                }
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
            base.Close();
        }

        private void QueryBut_Click(object sender, EventArgs e)
        {
            IFeatureClass featureClass = this.SelectLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilter();
            IFeatureCursor pCursor = null;
            string text = "";
            if (this.FieldValueBox.Text == string.Empty)
            {
                MessageBox.Show("请确定所在位置!");
            }
            else
            {
                if (!this.BlurCheck.Checked)
                {
                    text = this.FieldBox.Text;
                    text += "='";
                    text += this.FieldValueBox.Text;
                    text += "'";
                }
                else
                {
                    IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
                    if (dataset.Workspace.Type == (esriWorkspaceType)2)
                    {
                        text = this.FieldBox.Text;
                        text += " LIKE '%";
                        text += this.FieldValueBox.Text;
                        text += "%'";
                    }
                    else
                    {
                        text = this.FieldBox.Text;
                        text += " LIKE '*";
                        text += this.FieldValueBox.Text;
                        text += "*'";
                    }
                }
                List<string> list = new List<string>();
                list.Clear();
                for (int i = 0; i < this.ValueBox.Items.Count; i++)
                {
                    if (this.ValueBox.GetItemChecked(i))
                    {
                        string item = this.ValueBox.Items[i].ToString();
                        list.Add(item);
                    }
                }
                if (this.radioButton1.Checked)
                {
                    if (list.Count > 0)
                    {
                        text += " and ( ";
                        int num = 1;
                        int count = list.Count;
                        foreach (string current in list)
                        {
                            text += this.FindField;
                            text += " = '";
                            text += current;
                            text += "'";
                            if (num < count)
                            {
                                text += " OR ";
                            }
                            num++;
                        }
                        text += ")";
                    }
                }
                else if (list.Count > 0)
                {
                    text += " and ( ";
                    int num2 = 1;
                    int count2 = list.Count;
                    foreach (string current2 in list)
                    {
                        if (current2.Contains("x") || current2.Contains("X") || current2.Contains("*"))
                        {
                            text += this.FindField1;
                            text += " = '";
                            text += current2;
                            text += "'";
                        }
                        else
                        {
                            text += this.FindField;
                            text += " = ";
                            text += current2;
                        }
                        if (num2 < count2)
                        {
                            text += " OR ";
                        }
                        num2++;
                    }
                    text += ")";
                }
                spatialFilter.WhereClause = text;
                try
                {
                    pCursor = featureClass.Search(spatialFilter, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询条件过于复杂,请减少条件项。" + ex.Message);
                }
                //修改为插件事件，因为结果显示窗体为插件拥有。
                _plugin.FireQueryResultChanged(new QueryResultArgs(pCursor, (IFeatureSelection)this.SelectLayer));
            }
        }

        private void WipeValueBox()
        {
            this.bUnWipe = false;
            if (this.myfields != null)
            {
                int num = -1;
                IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                IQueryFilter queryFilter = new QueryFilter();
                string text;
                if (!this.BlurCheck.Checked)
                {
                    text = this.FieldBox.Text;
                    text += "='";
                    text += this.FieldValueBox.Text;
                    text += "'";
                }
                else
                {
                    text = this.FieldBox.Text;
                    text += " LIKE '*";
                    text += this.FieldValueBox.Text;
                    text += "*'";
                }
                queryFilter.WhereClause = text;
                IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                IFeature feature = featureCursor.NextFeature();
                this.ValueBox.Items.Clear();
                int num2 = this.myfields.FindField(this.FindField);
                if (!this.radioButton1.Checked)
                {
                    num = this.myfields.FindField(this.FindField1);
                }
                while (feature != null)
                {
                    if (num2 > 0)
                    {
                        object obj = feature.get_Value(num2);
                        if (!(obj is DBNull))
                        {
                            string text2 = obj.ToString();
                            if (text2.Length > 0 && !this.ValueBox.Items.Contains(text2))
                            {
                                this.ValueBox.Items.Add(text2);
                            }
                        }
                    }
                    if (num > 0)
                    {
                        object obj = feature.get_Value(num);
                        if (!(obj is DBNull))
                        {
                            string text2 = obj.ToString();
                            if (text2.Length > 0 && !this.ValueBox.Items.Contains(text2))
                            {
                                this.ValueBox.Items.Add(text2);
                            }
                        }
                    }
                    feature = featureCursor.NextFeature();
                }
            }
        }

        private void WipeBut_Click(object sender, EventArgs e)
        {
            this.WipeValueBox();
        }

        private void UnWipeValuebox()
        {
            if (this.myfields != null)
            {
                int num = -1;
                if (!this.bUnWipe)
                {
                    this.bUnWipe = true;
                    IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                    IQueryFilter queryFilter = new QueryFilter();
                    IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                    IFeature feature = featureCursor.NextFeature();
                    this.ValueBox.Items.Clear();
                    int num2 = this.myfields.FindField(this.FindField);
                    if (!this.radioButton1.Checked)
                    {
                        num = this.myfields.FindField(this.FindField1);
                    }
                    while (feature != null)
                    {
                        if (num2 > 0)
                        {
                            object obj = feature.get_Value(num2);
                            if (!(obj is DBNull))
                            {
                                string text = obj.ToString();
                                if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
                                {
                                    this.ValueBox.Items.Add(text);
                                }
                            }
                        }
                        if (num > 0)
                        {
                            object obj = feature.get_Value(num);
                            if (!(obj is DBNull))
                            {
                                string text = obj.ToString();
                                if (text.Length > 0 && !this.ValueBox.Items.Contains(text))
                                {
                                    this.ValueBox.Items.Add(text);
                                }
                            }
                        }
                        feature = featureCursor.NextFeature();
                    }
                }
            }
        }

        private void FieldValueBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bUnWipe)
            {
                this.UnWipeValuebox();
            }
        }

        private void FieldValueBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void BlurCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.bUnWipe)
            {
                this.UnWipeValuebox();
            }
        }

        private void FieldValueBox_TextUpdate(object sender, EventArgs e)
        {
            if (!this.bUnWipe)
            {
                this.UnWipeValuebox();
            }
        }

        private void FillAllBut_Click(object sender, EventArgs e)
        {
            if (!this.bUnWipe)
            {
                this.UnWipeValuebox();
            }
        }

        private void SimpleQueryByAddressUI1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string url = Application.StartupPath + "\\帮助.chm";
            string parameter = "快速查询";
            HelpNavigator command = HelpNavigator.KeywordIndex;
            Help.ShowHelp(this, url, command, parameter);
        }
    }
}
