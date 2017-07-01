using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class ProportionalSymbolRendererCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public ProportionalSymbolRendererCtrl()
        {
            this.InitializeComponent();
            this.symbolItem1.HasDrawLine = false;
            this.symbolItem2.HasDrawLine = false;
        }

        public void Apply()
        {
            IObjectCopy copy = new ObjectCopyClass();
            IProportionalSymbolRenderer renderer =
                copy.Copy(this.iproportionalSymbolRenderer_0) as IProportionalSymbolRenderer;
            renderer.CreateLegendSymbols();
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
        }

        private void btnBackground_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnBackground.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnBackground.Style = selector.GetSymbol();
                        this.iproportionalSymbolRenderer_0.BackgroundSymbol = this.btnBackground.Style as IFillSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnDataExclusion_Click(object sender, EventArgs e)
        {
            frmDataExclusion exclusion = new frmDataExclusion
            {
                FeatureLayer = this.igeoFeatureLayer_0,
                DataExclusion = this.iproportionalSymbolRenderer_0 as IDataExclusion
            };
            if (exclusion.ShowDialog() == DialogResult.OK)
            {
                this.GetMinMaxValue();
            }
        }

        private void btnMinSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnMinSymbol.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnMinSymbol.Style = selector.GetSymbol();
                        this.iproportionalSymbolRenderer_0.MinSymbol = this.btnMinSymbol.Style as ISymbol;
                        this.GetMinMaxValue();
                    }
                }
            }
            catch
            {
            }
        }

        private void cboLegendCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iproportionalSymbolRenderer_0.LegendSymbolCount = this.cboLegendCount.SelectedIndex;
        }

        private void cboNormFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboNormFields.SelectedIndex == 0)
                {
                    this.iproportionalSymbolRenderer_0.NormField = "";
                    (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType =
                        esriDataNormalization.esriNormalizeByNothing;
                }
                else if (this.cboNormFields.SelectedIndex == 1)
                {
                    this.iproportionalSymbolRenderer_0.NormField = "";
                    (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType =
                        esriDataNormalization.esriNormalizeByLog;
                }
                else if (this.cboNormFields.SelectedIndex > 1)
                {
                    (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType =
                        esriDataNormalization.esriNormalizeByField;
                    this.iproportionalSymbolRenderer_0.NormField = (this.cboNormFields.SelectedItem as FieldWrap).Name;
                }
                this.GetMinMaxValue();
            }
        }

        private void cboValueFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboValueFields.SelectedIndex > 0)
                {
                    this.iproportionalSymbolRenderer_0.Field = (this.cboValueFields.SelectedItem as FieldWrap).Name;
                    if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        this.SymbolgroupBox1.Visible = true;
                        this.SymbolgroupBox2.Visible = false;
                        this.groupBoxBackgroundSymbol.Visible = true;
                        this.btnBackground.Style = this.iproportionalSymbolRenderer_0.BackgroundSymbol;
                        this.btnMinSymbol.Style = this.iproportionalSymbolRenderer_0.MinSymbol;
                        this.btnBackground.Invalidate();
                        this.btnMinSymbol.Invalidate();
                    }
                    else if ((this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) ||
                             (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
                    {
                        this.SymbolgroupBox1.Visible = true;
                        this.SymbolgroupBox2.Visible = false;
                        this.groupBoxBackgroundSymbol.Visible = false;
                        this.btnMinSymbol.Style = this.iproportionalSymbolRenderer_0.MinSymbol;
                        this.btnMinSymbol.Invalidate();
                    }
                    else if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        this.SymbolgroupBox1.Visible = false;
                        this.SymbolgroupBox2.Visible = true;
                        this.btnLineMinSymbol.Style = this.iproportionalSymbolRenderer_0.MinSymbol;
                        this.btnLineMinSymbol.Invalidate();
                    }
                }
                else
                {
                    this.iproportionalSymbolRenderer_0.Field = "";
                    this.SymbolgroupBox1.Visible = false;
                    this.SymbolgroupBox2.Visible = false;
                }
                this.GetMinMaxValue();
            }
        }

        private void cboValueUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboValueUnit.SelectedIndex != 0)
            {
            }
        }

        private void chkFlannery_CheckedChanged(object sender, EventArgs e)
        {
            this.iproportionalSymbolRenderer_0.FlanneryCompensation = this.chkFlannery.Checked;
        }

        public void GetMinMaxValue()
        {
            try
            {
                if (this.cboValueFields.SelectedIndex > 0)
                {
                    IQueryFilter filter = new QueryFilterClass
                    {
                        SubFields = this.cboValueFields.Text
                    };
                    ITableHistogram histogram = new BasicTableHistogramClass
                    {
                        Table = this.method_0(),
                        Field = (this.cboValueFields.SelectedItem as FieldWrap).Name
                    };
                    if (this.cboNormFields.SelectedIndex == 0)
                    {
                        (histogram as IDataNormalization).NormalizationType =
                            esriDataNormalization.esriNormalizeByNothing;
                    }
                    else if (this.cboNormFields.SelectedIndex == 1)
                    {
                        (histogram as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByLog;
                    }
                    else if (this.cboNormFields.SelectedIndex > 1)
                    {
                        histogram.NormField = (this.cboNormFields.SelectedItem as FieldWrap).Name;
                        (histogram as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByField;
                    }
                    histogram.Exclusion = this.iproportionalSymbolRenderer_0 as IDataExclusion;
                    (histogram as IBasicHistogram).Invalidate();
                    double maximum = (histogram as IStatisticsResults).Maximum;
                    double minimum = (histogram as IStatisticsResults).Minimum;
                    if ((minimum <= 0.0) || (maximum <= 0.0))
                    {
                        MessageBox.Show("最小值或最大值<=0,请先排除该值");
                    }
                    else
                    {
                        this.iproportionalSymbolRenderer_0.MinDataValue = minimum;
                        this.iproportionalSymbolRenderer_0.MaxDataValue = maximum;
                        ISymbol symbol = (this.iproportionalSymbolRenderer_0.MinSymbol as IClone).Clone() as ISymbol;
                        double num3 = maximum/minimum;
                        if (num3 > 50.0)
                        {
                            num3 = 50.0;
                        }
                        if (symbol is ILineSymbol)
                        {
                            (symbol as ILineSymbol).Width *= num3;
                            this.symbolItem2.Symbol = symbol;
                            this.symbolItem2.Invalidate();
                        }
                        else if (symbol is IMarkerSymbol)
                        {
                            (symbol as IMarkerSymbol).Size *= num3;
                            this.symbolItem1.Symbol = symbol;
                            this.symbolItem1.Invalidate();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        public void Init()
        {
            IFields fields;
            this.cboValueFields.Properties.Items.Clear();
            this.cboValueFields.Properties.Items.Add("<无>");
            this.cboNormFields.Properties.Items.Clear();
            this.cboNormFields.Properties.Items.Add("<无>");
            this.cboNormFields.Properties.Items.Add("<LOG>");
            if (this.igeoFeatureLayer_0 is IDisplayTable)
            {
                fields = (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable.Fields;
            }
            else
            {
                fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
            }
            int index = 0;
            while (index < fields.FieldCount)
            {
                IField field = fields.get_Field(index);
                switch (field.Type)
                {
                    case esriFieldType.esriFieldTypeDouble:
                    case esriFieldType.esriFieldTypeInteger:
                    case esriFieldType.esriFieldTypeSingle:
                    case esriFieldType.esriFieldTypeSmallInteger:
                        this.cboValueFields.Properties.Items.Add(new FieldWrap(field));
                        this.cboNormFields.Properties.Items.Add(new FieldWrap(field));
                        break;
                }
                index++;
            }
            if (this.iproportionalSymbolRenderer_0.Field == "")
            {
                this.cboValueFields.Text = "<无>";
            }
            else
            {
                index = 1;
                while (index < this.cboValueFields.Properties.Items.Count)
                {
                    if ((this.cboValueFields.Properties.Items[index] as FieldWrap).Name ==
                        this.iproportionalSymbolRenderer_0.Field)
                    {
                        this.cboValueFields.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
            if ((this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType ==
                esriDataNormalization.esriNormalizeByLog)
            {
                this.cboNormFields.Text = "<LOG>";
            }
            else if ((this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType ==
                     esriDataNormalization.esriNormalizeByNothing)
            {
                this.cboNormFields.Text = "<无>";
            }
            else
            {
                for (index = 2; index < this.cboNormFields.Properties.Items.Count; index++)
                {
                    if ((this.cboNormFields.Properties.Items[index] as FieldWrap).Name ==
                        this.iproportionalSymbolRenderer_0.NormField)
                    {
                        this.cboNormFields.SelectedIndex = index;
                        break;
                    }
                }
            }
            this.cboValueUnit.SelectedIndex = 0;
            if (this.cboValueFields.SelectedIndex > 0)
            {
                if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    this.SymbolgroupBox1.Visible = true;
                    this.SymbolgroupBox2.Visible = false;
                    this.groupBoxBackgroundSymbol.Visible = true;
                    this.btnBackground.Style = this.iproportionalSymbolRenderer_0.BackgroundSymbol;
                    this.btnMinSymbol.Style = this.iproportionalSymbolRenderer_0.MinSymbol;
                    this.btnBackground.Invalidate();
                    this.btnMinSymbol.Invalidate();
                }
                else if ((this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) ||
                         (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
                {
                    this.SymbolgroupBox1.Visible = true;
                    this.SymbolgroupBox2.Visible = false;
                    this.groupBoxBackgroundSymbol.Visible = false;
                    this.btnMinSymbol.Style = this.iproportionalSymbolRenderer_0.MinSymbol;
                    this.btnMinSymbol.Invalidate();
                }
                else if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    this.SymbolgroupBox1.Visible = false;
                    this.SymbolgroupBox2.Visible = true;
                    this.btnLineMinSymbol.Style = this.iproportionalSymbolRenderer_0.MinSymbol;
                    this.btnLineMinSymbol.Invalidate();
                }
                this.GetMinMaxValue();
            }
            this.chkFlannery.Checked = this.iproportionalSymbolRenderer_0.FlanneryCompensation;
            this.cboLegendCount.Text = this.iproportionalSymbolRenderer_0.LegendSymbolCount.ToString();
        }

        private ITable method_0()
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

        private void method_1()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                this.iproportionalSymbolRenderer_0 = null;
            }
            else
            {
                IProportionalSymbolRenderer pInObject = this.igeoFeatureLayer_0.Renderer as IProportionalSymbolRenderer;
                if (pInObject == null)
                {
                    if (this.iproportionalSymbolRenderer_0 == null)
                    {
                        this.iproportionalSymbolRenderer_0 = new ProportionalSymbolRendererClass();
                        this.iproportionalSymbolRenderer_0.MinSymbol = this.method_5();
                        this.iproportionalSymbolRenderer_0.LegendSymbolCount = 3;
                        this.iproportionalSymbolRenderer_0.FlanneryCompensation = false;
                        this.iproportionalSymbolRenderer_0.ValueUnit = esriUnits.esriUnknownUnits;
                        this.iproportionalSymbolRenderer_0.ValueRepresentation =
                            esriValueRepresentations.esriValueRepUnknown;
                        this.iproportionalSymbolRenderer_0.Field = "";
                        (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationField = "";
                        (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType =
                            esriDataNormalization.esriNormalizeByNothing;
                        if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            IFillSymbol symbol = new SimpleFillSymbolClass
                            {
                                Color = this.method_3(23)
                            };
                            this.iproportionalSymbolRenderer_0.BackgroundSymbol = symbol;
                        }
                    }
                }
                else
                {
                    IObjectCopy copy = new ObjectCopyClass();
                    this.iproportionalSymbolRenderer_0 = copy.Copy(pInObject) as IProportionalSymbolRenderer;
                }
            }
        }

        private IFillSymbol method_2(IGeoFeatureLayer igeoFeatureLayer_1)
        {
            if (igeoFeatureLayer_1.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
            }
            return null;
        }

        private IColor method_3(int int_0)
        {
            bool flag;
            IRandomColorRamp ramp = new RandomColorRampClass
            {
                StartHue = 40,
                EndHue = 120,
                MinValue = 65,
                MaxValue = 90,
                MinSaturation = 25,
                MaxSaturation = 45,
                Size = 5,
                Seed = int_0
            };
            ramp.CreateRamp(out flag);
            IEnumColors colors = ramp.Colors;
            colors.Reset();
            return colors.Next();
        }

        private IColor method_4()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return new RgbColorClass
            {
                Red = (int) (255.0*random.NextDouble()),
                Green = (int) (255.0*random.NextDouble()),
                Blue = (int) (255.0*random.NextDouble())
            };
        }

        private ISymbol method_5()
        {
            if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                ILineSymbol symbol = new SimpleLineSymbolClass
                {
                    Width = 1.0
                };
                (symbol as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSSolid;
                symbol.Color = this.method_4();
                return (symbol as ISymbol);
            }
            IMarkerSymbol symbol3 = new SimpleMarkerSymbolClass
            {
                Size = 2.0,
                Color = this.method_4()
            };
            (symbol3 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCircle;
            return (symbol3 as ISymbol);
        }

        private void ProportionalSymbolRendererCtrl_Load(object sender, EventArgs e)
        {
            this.method_1();
            if (this.igeoFeatureLayer_0 != null)
            {
                this.Init();
            }
            this.bool_0 = true;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.bool_0)
                {
                    this.method_1();
                    this.bool_0 = false;
                    this.Init();
                    this.bool_0 = true;
                }
            }
        }

        bool IUserControl.Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public IProportionalSymbolRenderer ProportionalSymbolRenderer
        {
            get { return this.iproportionalSymbolRenderer_0; }
            set { this.iproportionalSymbolRenderer_0 = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
        }
    }
}