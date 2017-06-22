using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class DotDensityRendererCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IColorRamp icolorRamp_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public DotDensityRendererCtrl()
        {
            this.InitializeComponent();
            this.SelectFieldslistView.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_4);
        }

        public void Apply()
        {
            IObjectCopy copy = new ObjectCopyClass();
            IDotDensityRenderer renderer = copy.Copy(this.idotDensityRenderer_0) as IDotDensityRenderer;
            renderer.CreateLegend();
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
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
            ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
            dotDensitySymbol.MoveSymbol(dotDensitySymbol.get_Symbol(index), index + 1);
            IRendererFields fields = this.idotDensityRenderer_0 as IRendererFields;
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
            ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
            dotDensitySymbol.MoveSymbol(dotDensitySymbol.get_Symbol(index), index - 1);
            IRendererFields fields = this.idotDensityRenderer_0 as IRendererFields;
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
                int num3;
                double num = 0.0;
                double num2 = double.Parse(this.txtSize.Text);
                ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
                string[] strArray = new string[2];
                for (num3 = this.FieldsListBoxCtrl.SelectedIndices.Count - 1; num3 >= 0; num3--)
                {
                    int index = this.FieldsListBoxCtrl.SelectedIndices[num3];
                    (this.idotDensityRenderer_0 as IRendererFields).AddField((this.FieldsListBoxCtrl.Items[index] as FieldWrap).Name, this.FieldsListBoxCtrl.Items[index].ToString());
                    IColor color = this.ienumColors_0.Next();
                    if (color == null)
                    {
                        this.ienumColors_0.Reset();
                        color = this.ienumColors_0.Next();
                    }
                    ISymbol symbol = this.CreateMarkerSymbol(color, esriSimpleMarkerStyle.esriSMSCircle, num2) as ISymbol;
                    strArray[0] = "";
                    strArray[1] = this.FieldsListBoxCtrl.Items[index].ToString();
                    ListViewItemEx ex = new ListViewItemEx(strArray) {
                        Style = symbol,
                        Tag = this.FieldsListBoxCtrl.Items[index]
                    };
                    this.SelectFieldslistView.Add(ex);
                    dotDensitySymbol.AddSymbol(symbol);
                    this.FieldsListBoxCtrl.Items.RemoveAt(index);
                }
                IDotDensityFillSymbol symbol2 = (dotDensitySymbol as IClone).Clone() as IDotDensityFillSymbol;
                IDotDensityFillSymbol symbol3 = (dotDensitySymbol as IClone).Clone() as IDotDensityFillSymbol;
                IDotDensityFillSymbol symbol4 = (dotDensitySymbol as IClone).Clone() as IDotDensityFillSymbol;
                this.MinsymbolItem.Symbol = symbol2;
                this.MeansymbolItem.Symbol = symbol3;
                this.MaxsymbolItem.Symbol = symbol4;
                double[] numArray = new double[dotDensitySymbol.SymbolCount];
                double[] numArray2 = new double[dotDensitySymbol.SymbolCount];
                double[] numArray3 = new double[dotDensitySymbol.SymbolCount];
                for (num3 = 0; num3 < dotDensitySymbol.SymbolCount; num3++)
                {
                    double num5;
                    double num6;
                    double num7;
                    this.GetStaticsValue((this.idotDensityRenderer_0 as IRendererFields).get_Field(num3), out num5, out num6, out num7);
                    numArray[num3] = num5;
                    numArray3[num3] = num6;
                    numArray2[num3] = num7;
                    if (num5 > 0.0)
                    {
                        if (num == 0.0)
                        {
                            num = num5;
                        }
                        else
                        {
                            num = (num < num5) ? num : num5;
                        }
                    }
                    num = (num > this.idotDensityRenderer_0.DotValue) ? this.idotDensityRenderer_0.DotValue : num;
                    if (num == 0.0)
                    {
                        num = 500.0;
                    }
                    this.txtPointValue.Text = num.ToString();
                    this.idotDensityRenderer_0.DotValue = num;
                }
                for (num3 = 0; num3 < numArray.Length; num3++)
                {
                    symbol2.set_DotCount(num3, (int) (numArray[num3] / num));
                    symbol3.set_DotCount(num3, (int) (numArray3[num3] / num));
                    symbol4.set_DotCount(num3, (int) (numArray2[num3] / num));
                }
                this.MinsymbolItem.Invalidate();
                this.MeansymbolItem.Invalidate();
                this.MaxsymbolItem.Invalidate();
            }
            catch
            {
            }
            this.btnUnSelectAll.Enabled = true;
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            int index = this.SelectFieldslistView.SelectedIndices[0];
            ListViewItem item = this.SelectFieldslistView.SelectedItems[0];
            this.FieldsListBoxCtrl.Items.Add(item.Tag);
            ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
            dotDensitySymbol.DeleteSymbol(dotDensitySymbol.get_Symbol(index));
            (this.idotDensityRenderer_0 as IRendererFields).DeleteField(item.Text);
            this.SelectFieldslistView.Items.RemoveAt(index);
            if (this.SelectFieldslistView.Items.Count == 0)
            {
                this.btnUnSelectAll.Enabled = false;
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.SelectFieldslistView.Items.Count; i++)
            {
                ListViewItemEx ex = this.SelectFieldslistView.Items[i] as ListViewItemEx;
                this.FieldsListBoxCtrl.Items.Add(ex.Tag);
            }
            this.SelectFieldslistView.Items.Clear();
            ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
            (this.idotDensityRenderer_0 as IRendererFields).ClearFields();
            dotDensitySymbol.ClearSymbols();
        }

        private void colorRampComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                this.ienumColors_0 = this.method_1(this.colorRampComboBox1.GetSelectColorRamp(), fields.FieldCount);
                this.ienumColors_0.Reset();
                ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
                this.idotDensityRenderer_0.ColorScheme = this.colorRampComboBox1.Text;
                for (int i = 0; i < dotDensitySymbol.SymbolCount; i++)
                {
                    IMarkerSymbol symbol = dotDensitySymbol.get_Symbol(i) as IMarkerSymbol;
                    symbol.Color = this.ienumColors_0.Next();
                    dotDensitySymbol.set_Symbol(i, symbol as ISymbol);
                }
                this.SelectFieldslistView.Invalidate();
            }
        }

        public IDotDensityFillSymbol CreateDotDensityFillSymbol()
        {
            IDotDensityFillSymbol symbol = new DotDensityFillSymbolClass {
                DotSize = 2.0
            };
            IColor color = new RgbColorClass {
                NullColor = true
            };
            symbol.BackgroundColor = color;
            return symbol;
        }

        public ISimpleMarkerSymbol CreateMarkerSymbol(IColor icolor_0, esriSimpleMarkerStyle esriSimpleMarkerStyle_0, double double_0)
        {
            return new SimpleMarkerSymbolClass { Style = esriSimpleMarkerStyle_0, Size = double_0, Color = icolor_0 };
        }

 private void DotDensityRendererCtrl_Load(object sender, EventArgs e)
        {
            this.method_2();
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
                this.colorRampComboBox1.Text = this.idotDensityRenderer_0.ColorScheme;
                if (this.colorRampComboBox1.SelectedIndex == -1)
                {
                    this.colorRampComboBox1.SelectedIndex = 0;
                    this.idotDensityRenderer_0.ColorScheme = this.colorRampComboBox1.Text;
                }
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                this.ienumColors_0 = this.method_1(this.colorRampComboBox1.GetSelectColorRamp(), fields.FieldCount);
                this.ienumColors_0.Reset();
            }
            if (this.igeoFeatureLayer_0 != null)
            {
                this.method_0();
            }
            this.bool_0 = true;
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

        public void GetStaticsValue(string string_0, out double double_0, out double double_1, out double double_2)
        {
            double_0 = 0.0;
            double_1 = 0.0;
            double_2 = 0.0;
            try
            {
                ITableHistogram histogram = new BasicTableHistogramClass {
                    Field = string_0,
                    Table = this.method_3()
                };
                (histogram as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByNothing;
                (histogram as IBasicHistogram).Invalidate();
                double_2 = (histogram as IStatisticsResults).Maximum;
                double_0 = (histogram as IStatisticsResults).Minimum;
                double_1 = (histogram as IStatisticsResults).Mean;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

 private void method_0()
        {
            IFields fields2;
            int num;
            this.FieldsListBoxCtrl.Items.Clear();
            this.SelectFieldslistView.Items.Clear();
            IRendererFields fields = this.idotDensityRenderer_0 as IRendererFields;
            if (this.igeoFeatureLayer_0 is IDisplayTable)
            {
                fields2 = (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable.Fields;
            }
            else
            {
                fields2 = this.igeoFeatureLayer_0.FeatureClass.Fields;
            }
            this.colorRampComboBox1.Text = this.idotDensityRenderer_0.ColorScheme;
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
                if ((((type != esriFieldType.esriFieldTypeDouble) && (type != esriFieldType.esriFieldTypeInteger)) && (type != esriFieldType.esriFieldTypeSingle)) && (type != esriFieldType.esriFieldTypeSmallInteger))
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
            double[] numArray = new double[fields.FieldCount];
            double[] numArray2 = new double[fields.FieldCount];
            double[] numArray3 = new double[fields.FieldCount];
            string[] strArray = new string[2];
            ISymbolArray dotDensitySymbol = this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray;
            for (num = 0; num < fields.FieldCount; num++)
            {
                double num3;
                double num4;
                double num5;
                strArray[0] = "";
                strArray[1] = fields.get_FieldAlias(num);
                ListViewItemEx ex = new ListViewItemEx(strArray) {
                    Style = dotDensitySymbol.get_Symbol(num),
                    Tag = new FieldWrap(fields2.get_Field(fields2.FindField(fields.get_Field(num))))
                };
                this.SelectFieldslistView.Add(ex);
                this.GetStaticsValue(fields.get_Field(num), out num3, out num4, out num5);
                numArray[num] = num3;
                numArray3[num] = num4;
                numArray2[num] = num5;
            }
            if (fields.FieldCount > 0)
            {
                this.btnUnSelectAll.Enabled = true;
            }
            this.txtSize.Text = this.idotDensityRenderer_0.DotDensitySymbol.DotSize.ToString();
            this.txtPointValue.Text = this.idotDensityRenderer_0.DotValue.ToString();
            double num7 = double.Parse(this.txtPointValue.Text);
            IDotDensityFillSymbol symbol = (this.idotDensityRenderer_0.DotDensitySymbol as IClone).Clone() as IDotDensityFillSymbol;
            IDotDensityFillSymbol symbol2 = (this.idotDensityRenderer_0.DotDensitySymbol as IClone).Clone() as IDotDensityFillSymbol;
            IDotDensityFillSymbol symbol3 = (this.idotDensityRenderer_0.DotDensitySymbol as IClone).Clone() as IDotDensityFillSymbol;
            this.MinsymbolItem.Symbol = symbol;
            this.MeansymbolItem.Symbol = symbol2;
            this.MaxsymbolItem.Symbol = symbol3;
            for (num = 0; num < (symbol as ISymbolArray).SymbolCount; num++)
            {
                symbol.set_DotCount(num, (int) (numArray[num] / num7));
                symbol2.set_DotCount(num, (int) (numArray3[num] / num7));
                symbol3.set_DotCount(num, (int) (numArray2[num] / num7));
            }
            this.MinsymbolItem.Invalidate();
            this.MeansymbolItem.Invalidate();
            this.MaxsymbolItem.Invalidate();
        }

        private IEnumColors method_1(IColorRamp icolorRamp_1, int int_0)
        {
            if (icolorRamp_1 == null)
            {
                IAlgorithmicColorRamp ramp = new AlgorithmicColorRampClass {
                    FromColor = ColorManage.CreatColor(160, 0, 0),
                    ToColor = ColorManage.CreatColor(255, 200, 200),
                    Algorithm = esriColorRampAlgorithm.esriLabLChAlgorithm
                };
                icolorRamp_1 = ramp;
            }
            return ColorManage.CreateColors(icolorRamp_1, int_0);
        }

        private void method_2()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                this.idotDensityRenderer_0 = null;
            }
            else
            {
                IDotDensityRenderer pInObject = this.igeoFeatureLayer_0.Renderer as IDotDensityRenderer;
                if (pInObject == null)
                {
                    if (this.idotDensityRenderer_0 == null)
                    {
                        this.idotDensityRenderer_0 = new DotDensityRendererClass();
                        this.idotDensityRenderer_0.DotDensitySymbol = this.CreateDotDensityFillSymbol();
                        this.idotDensityRenderer_0.DotValue = 0.0;
                    }
                }
                else
                {
                    IObjectCopy copy = new ObjectCopyClass();
                    this.idotDensityRenderer_0 = copy.Copy(pInObject) as IDotDensityRenderer;
                }
            }
        }

        private ITable method_3()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                return null;
            }
            if (this.igeoFeatureLayer_0 is IDisplayTable)
            {
                return (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.igeoFeatureLayer_0 is IAttributeTable)
            {
                return (this.igeoFeatureLayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.igeoFeatureLayer_0 != null)
            {
                return (this.igeoFeatureLayer_0.FeatureClass as ITable);
            }
            return (this.igeoFeatureLayer_0 as ITable);
        }

        private void method_4(int int_0, object object_0)
        {
            if (object_0 is ISymbol)
            {
                (this.idotDensityRenderer_0.DotDensitySymbol as ISymbolArray).set_Symbol(int_0, object_0 as ISymbol);
            }
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

        private void txtPointValue_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtPointValue.Text);
                    double num2 = this.idotDensityRenderer_0.DotValue / num;
                    this.idotDensityRenderer_0.DotValue = num;
                    IDotDensityFillSymbol symbol = this.MinsymbolItem.Symbol as IDotDensityFillSymbol;
                    IDotDensityFillSymbol symbol2 = this.MeansymbolItem.Symbol as IDotDensityFillSymbol;
                    IDotDensityFillSymbol symbol3 = this.MaxsymbolItem.Symbol as IDotDensityFillSymbol;
                    for (int i = 0; i < (symbol as ISymbolArray).SymbolCount; i++)
                    {
                        symbol.set_DotCount(i, (int) (symbol.get_DotCount(i) * num2));
                        symbol2.set_DotCount(i, (int) (symbol2.get_DotCount(i) * num2));
                        symbol3.set_DotCount(i, (int) (symbol3.get_DotCount(i) * num2));
                    }
                    this.MinsymbolItem.Invalidate();
                    this.MeansymbolItem.Invalidate();
                    this.MaxsymbolItem.Invalidate();
                }
                catch
                {
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtSize.Text);
                    this.idotDensityRenderer_0.DotDensitySymbol.DotSize = num;
                }
                catch
                {
                }
            }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.bool_0)
                {
                    this.method_2();
                    this.bool_0 = false;
                    this.method_0();
                    this.bool_0 = true;
                }
            }
        }

        bool IUserControl.Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }
    }
}

