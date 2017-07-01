using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class ChartRendererCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IChartRenderer ichartRenderer_0 = null;
        private IColorRamp icolorRamp_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private int int_0 = 0;
        private IStyleGallery istyleGallery_0 = null;

        public ChartRendererCtrl()
        {
            this.InitializeComponent();
            this.SelectFieldslistView.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_9);
        }

        public void Apply()
        {
            IRendererFields fields = this.ichartRenderer_0 as IRendererFields;
            if (fields.FieldCount != 0)
            {
                this.ichartRenderer_0.ChartSymbol.MaxValue = this.GetMaxValue();
                IObjectCopy copy = new ObjectCopyClass();
                IChartRenderer renderer = copy.Copy(this.ichartRenderer_0) as IChartRenderer;
                renderer.CreateLegend();
                this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = this.SelectFieldslistView.SelectedIndices[0];
            ListViewItemEx ex = this.SelectFieldslistView.Items[index] as ListViewItemEx;
            this.SelectFieldslistView.Items.RemoveAt(index);
            if ((index + 1) == this.SelectFieldslistView.Items.Count)
            {
                this.SelectFieldslistView.Items.Add(ex);
            }
            else
            {
                this.SelectFieldslistView.Items.Insert(index + 1, ex);
            }
            ISymbolArray chartSymbol = this.ichartRenderer_0.ChartSymbol as ISymbolArray;
            chartSymbol.MoveSymbol(chartSymbol.get_Symbol(index), index + 1);
            IRendererFields fields = this.ichartRenderer_0 as IRendererFields;
            fields.ClearFields();
            for (int i = 0; i < this.SelectFieldslistView.Items.Count; i++)
            {
                ex = this.SelectFieldslistView.Items[i] as ListViewItemEx;
                FieldWrap tag = ex.Tag as FieldWrap;
                fields.AddField(tag.Name, tag.ToString());
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = this.SelectFieldslistView.SelectedIndices[0];
            ListViewItemEx item = this.SelectFieldslistView.Items[index] as ListViewItemEx;
            this.SelectFieldslistView.Items.RemoveAt(index);
            this.SelectFieldslistView.Items.Insert(index - 1, item);
            ISymbolArray chartSymbol = this.ichartRenderer_0.ChartSymbol as ISymbolArray;
            chartSymbol.MoveSymbol(chartSymbol.get_Symbol(index), index - 1);
            IRendererFields fields = this.ichartRenderer_0 as IRendererFields;
            fields.ClearFields();
            for (int i = 0; i < this.SelectFieldslistView.Items.Count; i++)
            {
                item = this.SelectFieldslistView.Items[i] as ListViewItemEx;
                FieldWrap tag = item.Tag as FieldWrap;
                fields.AddField(tag.Name, tag.ToString());
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                ISymbolArray chartSymbol = this.ichartRenderer_0.ChartSymbol as ISymbolArray;
                string[] strArray = new string[2];
                for (int i = this.FieldsListBoxCtrl.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    int index = this.FieldsListBoxCtrl.SelectedIndices[i];
                    FieldWrap wrap = this.FieldsListBoxCtrl.Items[index] as FieldWrap;
                    (this.ichartRenderer_0 as IRendererFields).AddField(wrap.Name, wrap.ToString());
                    IColor color = this.ienumColors_0.Next();
                    if (color == null)
                    {
                        this.ienumColors_0.Reset();
                        color = this.ienumColors_0.Next();
                    }
                    ISymbol symbol = this.method_5(color);
                    strArray[0] = "";
                    strArray[1] = wrap.ToString();
                    ListViewItemEx ex = new ListViewItemEx(strArray)
                    {
                        Style = symbol,
                        Tag = wrap
                    };
                    this.SelectFieldslistView.Add(ex);
                    chartSymbol.AddSymbol(symbol);
                    this.FieldsListBoxCtrl.Items.RemoveAt(index);
                }
            }
            catch
            {
            }
            this.btnUnSelectAll.Enabled = true;
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnStyle.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnStyle.Style = selector.GetSymbol();
                        this.ichartRenderer_0.BaseSymbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.SelectFieldslistView.SelectedIndices[0];
                ListViewItem item = this.SelectFieldslistView.SelectedItems[0];
                this.FieldsListBoxCtrl.Items.Add(item.Tag);
                (this.ichartRenderer_0 as IRendererFields).DeleteField(item.SubItems[1].Text);
                ISymbolArray chartSymbol = this.ichartRenderer_0.ChartSymbol as ISymbolArray;
                chartSymbol.DeleteSymbol(chartSymbol.get_Symbol(index));
                this.SelectFieldslistView.Items.RemoveAt(index);
                if (this.SelectFieldslistView.Items.Count == 0)
                {
                    this.btnUnSelectAll.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.SelectFieldslistView.Items.Count; i++)
            {
                ListViewItem item = this.SelectFieldslistView.Items[i];
                this.FieldsListBoxCtrl.Items.Add(item.Tag);
            }
            this.SelectFieldslistView.Items.Clear();
            (this.ichartRenderer_0 as IRendererFields).ClearFields();
            (this.ichartRenderer_0.ChartSymbol as ISymbolArray).ClearSymbols();
        }

        private void ChartRendererCtrl_Load(object sender, EventArgs e)
        {
            this.method_4();
            if (this.istyleGallery_0 != null)
            {
                IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                item.Reset();
                for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                {
                    this.colorRampComboBox1.Add(item2);
                }
                item = null;
                GC.Collect();
            }
            if (this.colorRampComboBox1.Items.Count == 0)
            {
                this.colorRampComboBox1.Enabled = false;
            }
            else
            {
                this.colorRampComboBox1.Text = this.ichartRenderer_0.ColorScheme;
                if (this.colorRampComboBox1.SelectedIndex == -1)
                {
                    this.colorRampComboBox1.SelectedIndex = 0;
                    this.ichartRenderer_0.ColorScheme = this.colorRampComboBox1.Text;
                }
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                this.ienumColors_0 = this.method_1(this.colorRampComboBox1.GetSelectColorRamp(), fields.FieldCount);
                this.ienumColors_0.Reset();
            }
            if (this.igeoFeatureLayer_0 != null)
            {
                this.method_2();
            }
            this.bool_0 = true;
        }

        private void chkOverposter_CheckedChanged(object sender, EventArgs e)
        {
            this.ichartRenderer_0.UseOverposter = this.chkOverposter.Checked;
        }

        private void colorRampComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                this.ienumColors_0 = this.method_1(this.colorRampComboBox1.GetSelectColorRamp(), fields.FieldCount);
                this.ienumColors_0.Reset();
                this.ichartRenderer_0.ColorScheme = this.colorRampComboBox1.Text;
                ISymbolArray chartSymbol = this.ichartRenderer_0.ChartSymbol as ISymbolArray;
                for (int i = 0; i < chartSymbol.SymbolCount; i++)
                {
                    IFillSymbol symbol = chartSymbol.get_Symbol(i) as IFillSymbol;
                    symbol.Color = this.ienumColors_0.Next();
                }
                this.SelectFieldslistView.Invalidate();
            }
        }

        private void FieldsListBoxCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FieldsListBoxCtrl.SelectedIndices.Count > 0)
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false;
            }
        }

        public double GetMaxValue()
        {
            double num = 0.0;
            try
            {
                IRendererFields fields = this.ichartRenderer_0 as IRendererFields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    if (i == 0)
                    {
                        num = this.method_6(fields.get_Field(i));
                    }
                    else
                    {
                        double num3 = this.method_6(fields.get_Field(i));
                        num = (num < num3) ? num3 : num;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return num;
        }

        public IColorRamp MakeNewRamp()
        {
            return new AlgorithmicColorRampClass
            {
                FromColor = this.RandomColor(),
                ToColor = this.RandomColor(),
                Algorithm = esriColorRampAlgorithm.esriLabLChAlgorithm
            };
        }

        private IColor method_0(int int_1, int int_2, int int_3)
        {
            return new RgbColorClass {Red = int_1, Green = int_2, Blue = int_3};
        }

        private IEnumColors method_1(IColorRamp icolorRamp_1, int int_1)
        {
            if (icolorRamp_1 == null)
            {
                icolorRamp_1 = this.MakeNewRamp();
            }
            return ColorManage.CreateColors(icolorRamp_1, int_1);
        }

        private void method_2()
        {
            IFields fields2;
            int num;
            this.FieldsListBoxCtrl.Items.Clear();
            this.SelectFieldslistView.Items.Clear();
            IRendererFields fields = this.ichartRenderer_0 as IRendererFields;
            if (this.igeoFeatureLayer_0 is IDisplayTable)
            {
                fields2 = (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable.Fields;
            }
            else
            {
                fields2 = this.igeoFeatureLayer_0.FeatureClass.Fields;
            }
            this.colorRampComboBox1.Text = this.ichartRenderer_0.ColorScheme;
            if (this.colorRampComboBox1.Items.Count == 0)
            {
                this.ienumColors_0 = this.method_1(null, fields2.FieldCount);
            }
            else
            {
                this.ienumColors_0 = this.method_1(this.colorRampComboBox1.GetSelectColorRamp(), fields2.FieldCount);
            }
            bool flag = false;
            for (num = 0; num < fields2.FieldCount; num++)
            {
                IField field = fields2.get_Field(num);
                esriFieldType type = field.Type;
                if ((((type != esriFieldType.esriFieldTypeDouble) && (type != esriFieldType.esriFieldTypeInteger)) &&
                     (type != esriFieldType.esriFieldTypeSingle)) && (type != esriFieldType.esriFieldTypeSmallInteger))
                {
                    continue;
                }
                flag = true;
                string str = field.Name.ToUpper();
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    if (str == fields.get_Field(i).ToUpper())
                    {
                        goto Label_0145;
                    }
                }
                goto Label_0147;
                Label_0145:
                flag = false;
                Label_0147:
                if (flag)
                {
                    this.FieldsListBoxCtrl.Items.Add(new FieldWrap(field));
                }
            }
            string[] strArray = new string[2];
            ISymbolArray chartSymbol = this.ichartRenderer_0.ChartSymbol as ISymbolArray;
            for (num = 0; num < fields.FieldCount; num++)
            {
                strArray[0] = "";
                strArray[1] = fields.get_FieldAlias(num);
                ListViewItemEx ex = new ListViewItemEx(strArray)
                {
                    Style = chartSymbol.get_Symbol(num),
                    Tag = new FieldWrap(fields2.get_Field(fields2.FindField(fields.get_Field(num))))
                };
                this.SelectFieldslistView.Add(ex);
            }
            if ((this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) ||
                (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
            {
                this.lblBackground.Visible = false;
                this.btnStyle.Visible = false;
            }
            else
            {
                this.lblBackground.Visible = true;
                this.btnStyle.Visible = true;
            }
            if (this.ichartRenderer_0.BaseSymbol == null)
            {
                this.ichartRenderer_0.BaseSymbol = this.method_8(this.igeoFeatureLayer_0.FeatureClass.ShapeType);
            }
            this.btnStyle.Style = this.ichartRenderer_0.BaseSymbol;
            this.chkOverposter.Checked = this.ichartRenderer_0.UseOverposter;
        }

        private string method_3(string string_0)
        {
            IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
            int index = fields.FindFieldByAliasName(string_0);
            return fields.get_Field(index).Name;
        }

        private void method_4()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                this.ichartRenderer_0 = null;
            }
            else
            {
                IChartRenderer pInObject = this.igeoFeatureLayer_0.Renderer as IChartRenderer;
                if (pInObject == null)
                {
                    if (this.ichartRenderer_0 == null)
                    {
                        this.ichartRenderer_0 = new ChartRendererClass();
                        if (this.int_0 == 0)
                        {
                            this.ichartRenderer_0.ChartSymbol = new PieChartSymbolClass();
                        }
                        else if (this.int_0 == 1)
                        {
                            this.ichartRenderer_0.ChartSymbol = new BarChartSymbolClass();
                        }
                        else
                        {
                            this.ichartRenderer_0.ChartSymbol = new StackedChartSymbolClass();
                        }
                        (this.ichartRenderer_0.ChartSymbol as IMarkerSymbol).Size = 32.0;
                    }
                }
                else
                {
                    IObjectCopy copy = new ObjectCopyClass();
                    this.ichartRenderer_0 = copy.Copy(pInObject) as IChartRenderer;
                    IChartSymbol chartSymbol = this.ichartRenderer_0.ChartSymbol;
                    IChartSymbol symbol2 = null;
                    if (this.int_0 == 0)
                    {
                        if (!(chartSymbol is IPieChartSymbol))
                        {
                            symbol2 = new PieChartSymbolClass();
                        }
                    }
                    else if (this.int_0 == 1)
                    {
                        if (!(chartSymbol is IBarChartSymbol))
                        {
                            symbol2 = new BarChartSymbolClass();
                        }
                    }
                    else if ((this.int_0 == 2) && !(chartSymbol is IStackedChartSymbol))
                    {
                        symbol2 = new StackedChartSymbolClass();
                    }
                    if (symbol2 != null)
                    {
                        ISymbolArray array = chartSymbol as ISymbolArray;
                        for (int i = 0; i < array.SymbolCount; i++)
                        {
                            (symbol2 as ISymbolArray).AddSymbol(array.get_Symbol(i));
                        }
                        this.ichartRenderer_0.ChartSymbol = symbol2;
                    }
                }
            }
        }

        private ISymbol method_5(IColor icolor_0)
        {
            IFillSymbol symbol = new SimpleFillSymbolClass
            {
                Color = icolor_0
            };
            return (symbol as ISymbol);
        }

        private double method_6(string string_0)
        {
            ITableHistogram histogram = new BasicTableHistogramClass
            {
                Exclusion = this.ichartRenderer_0 as IDataExclusion,
                Field = string_0,
                Table = this.igeoFeatureLayer_0.FeatureClass as ITable
            };
            return (histogram as IStatisticsResults).Maximum;
        }

        private IColor method_7()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return new RgbColorClass
            {
                Red = (int) (255.0*random.NextDouble()),
                Green = (int) (255.0*random.NextDouble()),
                Blue = (int) (255.0*random.NextDouble())
            };
        }

        private ISymbol method_8(esriGeometryType esriGeometryType_0)
        {
            ISymbol symbol = null;
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    symbol = new SimpleMarkerSymbolClass();
                    (symbol as IMarkerSymbol).Color = this.method_7();
                    return symbol;

                case esriGeometryType.esriGeometryPolyline:
                    symbol = new SimpleLineSymbolClass();
                    (symbol as ILineSymbol).Color = this.method_7();
                    return symbol;

                case esriGeometryType.esriGeometryPolygon:
                    symbol = new SimpleFillSymbolClass();
                    (symbol as IFillSymbol).Color = this.method_7();
                    return symbol;
            }
            return symbol;
        }

        private void method_9(int int_1, object object_0)
        {
            if (object_0 is ISymbol)
            {
                (this.ichartRenderer_0.ChartSymbol as ISymbolArray).set_Symbol(int_1, object_0 as ISymbol);
            }
        }

        public IColor RandomColor()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return new RgbColorClass {RGB = (int) ((16777214.0*random.NextDouble()) + 1.0)};
        }

        private void SelectFieldslistView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectFieldslistView.SelectedItems.Count == 1)
            {
                this.btnUnSelect.Enabled = true;
                if (this.SelectFieldslistView.SelectedIndices[0] == 0)
                {
                    this.btnMoveUp.Enabled = false;
                    if (this.SelectFieldslistView.SelectedIndices[0] == (this.SelectFieldslistView.Items.Count - 1))
                    {
                        this.btnMoveDown.Enabled = false;
                    }
                    else
                    {
                        this.btnMoveDown.Enabled = true;
                    }
                }
                else if (this.SelectFieldslistView.SelectedIndices[0] == (this.SelectFieldslistView.Items.Count - 1))
                {
                    this.btnMoveUp.Enabled = true;
                    this.btnMoveDown.Enabled = false;
                }
                else
                {
                    this.btnMoveUp.Enabled = true;
                    this.btnMoveDown.Enabled = true;
                }
            }
            else if (this.SelectFieldslistView.SelectedItems.Count > 1)
            {
                this.btnUnSelect.Enabled = true;
                this.btnMoveUp.Enabled = false;
                this.btnMoveDown.Enabled = false;
            }
            else
            {
                this.btnUnSelect.Enabled = false;
                this.btnMoveUp.Enabled = false;
                this.btnMoveDown.Enabled = false;
            }
        }

        public int ChartRenderType
        {
            set { this.int_0 = value; }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.bool_0)
                {
                    this.method_4();
                    this.bool_0 = false;
                    this.method_2();
                    this.bool_0 = true;
                }
            }
        }

        bool IUserControl.Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
                this.SelectFieldslistView.StyleGallery = this.istyleGallery_0;
            }
        }
    }
}