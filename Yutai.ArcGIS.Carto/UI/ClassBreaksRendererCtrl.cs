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
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class ClassBreaksRendererCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IClassBreaksRenderer iclassBreaksRenderer_0 = null;
        private IColorRamp icolorRamp_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public ClassBreaksRendererCtrl()
        {
            this.InitializeComponent();
            this.listView1.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_6);
            this.listView1.SetColumnEditable(2, true);
        }

        public void Apply()
        {
            IObjectCopy copy = new ObjectCopyClass();
            IClassBreaksRenderer renderer = copy.Copy(this.iclassBreaksRenderer_0) as IClassBreaksRenderer;
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
        }

        private void cboClassifyMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            IClassify classify;
            if (this.bool_0)
            {
                classify = null;
                switch (this.cboClassifyMethod.SelectedIndex)
                {
                    case 0:
                        classify = new EqualIntervalClass();
                        goto Label_0043;

                    case 1:
                        classify = new QuantileClass();
                        goto Label_0043;

                    case 2:
                        classify = new NaturalBreaksClass();
                        goto Label_0043;
                }
            }
            return;
        Label_0043:
            (this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).Method = classify.ClassID;
            if (this.cboClassifyNum.SelectedIndex >= 0)
            {
                this.method_3();
            }
            else
            {
                this.cboClassifyNum.SelectedIndex = 4;
            }
        }

        private void cboClassifyNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_3();
            }
        }

        private void cboNormFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboNormFields.SelectedIndex == 0)
                {
                    this.iclassBreaksRenderer_0.NormField = "";
                    (this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByNothing;
                }
                else if (this.cboNormFields.SelectedIndex == 1)
                {
                    this.iclassBreaksRenderer_0.NormField = "";
                    (this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByLog;
                }
                else if (this.cboNormFields.SelectedIndex == 2)
                {
                    this.iclassBreaksRenderer_0.NormField = "";
                    (this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByPercentOfTotal;
                }
                else if (this.cboNormFields.SelectedIndex > 2)
                {
                    (this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByField;
                    this.iclassBreaksRenderer_0.NormField = (this.cboNormFields.SelectedItem as FieldWrap).Name;
                }
                if (this.cboValueFields.SelectedIndex > 0)
                {
                    this.method_3();
                }
            }
        }

        private void cboValueFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboValueFields.SelectedIndex > 0)
                {
                    this.iclassBreaksRenderer_0.Field = (this.cboValueFields.SelectedItem as FieldWrap).Name;
                    this.Classifygroup.Enabled = true;
                    if (this.cboClassifyMethod.SelectedIndex != 2)
                    {
                        this.cboClassifyMethod.SelectedIndex = 2;
                    }
                    else if (this.cboClassifyNum.SelectedIndex == -1)
                    {
                        this.cboClassifyNum.SelectedIndex = 4;
                    }
                    else
                    {
                        this.method_3();
                    }
                }
                else
                {
                    this.iclassBreaksRenderer_0.Field = "";
                    this.Classifygroup.Enabled = false;
                    this.listView1.Items.Clear();
                    this.iclassBreaksRenderer_0.BreakCount = 0;
                }
            }
        }

        private void ClassBreaksRendererCtrl_Load(object sender, EventArgs e)
        {
            this.method_1();
            int num = -1;
            string colorRamp = (this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).ColorRamp;
            if (this.istyleGallery_0 != null)
            {
                IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                item.Reset();
                for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                {
                    this.colorRampComboBox1.Add(item2);
                    if (item2.Name == colorRamp)
                    {
                        num = this.colorRampComboBox1.Items.Count - 1;
                    }
                }
                item = null;
                GC.Collect();
            }
            if (this.colorRampComboBox1.Items.Count == 0)
            {
                this.colorRampComboBox1.Enabled = false;
                IRandomColorRamp ramp = new RandomColorRampClass {
                    StartHue = 40,
                    EndHue = 120,
                    MinValue = 65,
                    MaxValue = 90,
                    MinSaturation = 25,
                    MaxSaturation = 45,
                    Size = 5,
                    Seed = 23
                };
                this.icolorRamp_0 = ramp;
            }
            else
            {
                this.colorRampComboBox1.SelectedIndex = num;
                if (this.colorRampComboBox1.SelectedIndex == -1)
                {
                    this.colorRampComboBox1.SelectedIndex = 0;
                }
                this.icolorRamp_0 = this.colorRampComboBox1.GetSelectColorRamp();
                (this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).ColorRamp = this.colorRampComboBox1.Text;
            }
            if (this.igeoFeatureLayer_0 != null)
            {
                this.method_0();
            }
            this.bool_0 = true;
        }

        private void colorRampComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.icolorRamp_0 = this.colorRampComboBox1.GetSelectColorRamp();
                (this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).ColorRamp = this.colorRampComboBox1.Text;
                if (this.iclassBreaksRenderer_0.BreakCount > 0)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.iclassBreaksRenderer_0.BreakCount;
                    this.icolorRamp_0.CreateRamp(out flag);
                    IEnumColors colors = this.icolorRamp_0.Colors;
                    colors.Reset();
                    this.listView1.BeginUpdate();
                    for (int i = 0; i < this.iclassBreaksRenderer_0.BreakCount; i++)
                    {
                        ISymbol sym = this.iclassBreaksRenderer_0.get_Symbol(i);
                        if (sym is IMarkerSymbol)
                        {
                            (sym as IMarkerSymbol).Color = colors.Next();
                        }
                        else if (sym is ILineSymbol)
                        {
                            (sym as ILineSymbol).Color = colors.Next();
                        }
                        else if (sym is IFillSymbol)
                        {
                            (sym as IFillSymbol).Color = colors.Next();
                        }
                        this.iclassBreaksRenderer_0.set_Symbol(i, sym);
                        if (this.listView1.Items[i] is ListViewItemEx)
                        {
                            (this.listView1.Items[i] as ListViewItemEx).Style = sym;
                        }
                        this.listView1.Items[i].Tag = sym;
                    }
                    this.listView1.EndUpdate();
                    this.listView1.Invalidate();
                }
            }
        }

 private void method_0()
        {
            IFields fields;
            this.cboValueFields.Properties.Items.Clear();
            this.cboValueFields.Properties.Items.Add("<无>");
            this.cboNormFields.Properties.Items.Clear();
            this.cboNormFields.Properties.Items.Add("<无>");
            this.cboNormFields.Properties.Items.Add("<百分比>");
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
            if (this.iclassBreaksRenderer_0.Field == "")
            {
                this.cboValueFields.SelectedIndex = 0;
            }
            else
            {
                index = 1;
                while (index < this.cboValueFields.Properties.Items.Count)
                {
                    if ((this.cboValueFields.Properties.Items[index] as FieldWrap).Name == this.iclassBreaksRenderer_0.Field)
                    {
                        this.cboValueFields.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
            if ((this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType == esriDataNormalization.esriNormalizeByLog)
            {
                this.cboNormFields.Text = "<LOG>";
            }
            else if ((this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType == esriDataNormalization.esriNormalizeByNothing)
            {
                this.cboNormFields.Text = "<无>";
            }
            else if ((this.iclassBreaksRenderer_0 as IDataNormalization).NormalizationType == esriDataNormalization.esriNormalizeByPercentOfTotal)
            {
                this.cboNormFields.Text = "<百分比>";
            }
            else
            {
                index = 3;
                while (index < this.cboNormFields.Properties.Items.Count)
                {
                    if ((this.cboNormFields.Properties.Items[index] as FieldWrap).Name == this.iclassBreaksRenderer_0.NormField)
                    {
                        this.cboNormFields.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
            object[] objArray = new object[3];
            this.listView1.Items.Clear();
            for (index = 0; index < this.iclassBreaksRenderer_0.BreakCount; index++)
            {
                objArray[0] = this.iclassBreaksRenderer_0.get_Symbol(index);
                objArray[1] = (this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).get_LowBreak(index).ToString() + " - " + this.iclassBreaksRenderer_0.get_Break(index).ToString();
                objArray[2] = this.iclassBreaksRenderer_0.get_Label(index);
                this.listView1.Add(objArray);
            }
            this.cboClassifyNum.SelectedIndex = this.iclassBreaksRenderer_0.BreakCount - 1;
            IClassify classify = this.method_2((this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).Method) as IClassify;
            if (classify != null)
            {
                this.Classifygroup.Enabled = true;
                switch (classify.MethodName)
                {
                    case "Equal Interval":
                    case "等间隔":
                        this.cboClassifyMethod.SelectedIndex = 0;
                        break;

                    case "Quantile":
                    case "分位数":
                        this.cboClassifyMethod.SelectedIndex = 1;
                        break;

                    case "Natural Breaks (Jenks)":
                    case "自然间隔(Jenks)":
                        this.cboClassifyMethod.SelectedIndex = 2;
                        break;
                }
            }
            else
            {
                this.Classifygroup.Enabled = false;
            }
        }

        private void method_1()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                this.iclassBreaksRenderer_0 = null;
            }
            else
            {
                IClassBreaksRenderer pInObject = this.igeoFeatureLayer_0.Renderer as IClassBreaksRenderer;
                if (pInObject == null)
                {
                    if (this.iclassBreaksRenderer_0 == null)
                    {
                        this.iclassBreaksRenderer_0 = new ClassBreaksRendererClass();
                    }
                }
                else
                {
                    IObjectCopy copy = new ObjectCopyClass();
                    this.iclassBreaksRenderer_0 = copy.Copy(pInObject) as IClassBreaksRenderer;
                }
            }
        }

        private object method_2(UID uid_0)
        {
            if (uid_0 == null)
            {
                return null;
            }
            try
            {
                Guid clsid = new Guid(uid_0.Value.ToString());
                return Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid));
            }
            catch
            {
                return null;
            }
        }

        private void method_3()
        {
            string name = "";
            esriDataNormalization esriNormalizeByNothing = esriDataNormalization.esriNormalizeByNothing;
            if (this.cboNormFields.SelectedIndex == 0)
            {
                esriNormalizeByNothing = esriDataNormalization.esriNormalizeByNothing;
            }
            else if (this.cboNormFields.SelectedIndex == 1)
            {
                esriNormalizeByNothing = esriDataNormalization.esriNormalizeByLog;
            }
            else if (this.cboNormFields.SelectedIndex == 2)
            {
                esriNormalizeByNothing = esriDataNormalization.esriNormalizeByPercentOfTotal;
            }
            else if (this.cboNormFields.SelectedIndex > 2)
            {
                name = (this.cboNormFields.SelectedItem as FieldWrap).Name;
                esriNormalizeByNothing = esriDataNormalization.esriNormalizeByField;
            }
            this.method_5(this.icolorRamp_0, this.iclassBreaksRenderer_0, this.cboClassifyNum.SelectedIndex + 1, this.method_4(), (this.cboValueFields.SelectedItem as FieldWrap).Name, name, esriNormalizeByNothing);
            this.listView1.Items.Clear();
            string[] strArray = new string[3];
            int index = 0;
            while (true)
            {
                if (index >= this.iclassBreaksRenderer_0.BreakCount)
                {
                    break;
                }
                strArray[0] = "";
                strArray[1] = (this.iclassBreaksRenderer_0 as IClassBreaksUIProperties).get_LowBreak(index).ToString() + " - " + this.iclassBreaksRenderer_0.get_Break(index).ToString();
                strArray[2] = this.iclassBreaksRenderer_0.get_Label(index);
                ListViewItemEx ex = new ListViewItemEx(strArray);
                try
                {
                    ex.Style = this.iclassBreaksRenderer_0.get_Symbol(index);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
                this.listView1.Add(ex);
                index++;
            }
            this.cboClassifyNum.SelectedIndex = this.iclassBreaksRenderer_0.BreakCount - 1;
        }

        private ITable method_4()
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

        private void method_5(IColorRamp icolorRamp_1, IClassBreaksRenderer iclassBreaksRenderer_1, int int_0, ITable itable_0, string string_0, string string_1, esriDataNormalization esriDataNormalization_0)
        {
            try
            {
                esriGeometryType esriGeometryAny = esriGeometryType.esriGeometryAny;
                if (this.igeoFeatureLayer_0.FeatureClass != null)
                {
                    esriGeometryAny = this.igeoFeatureLayer_0.FeatureClass.ShapeType;
                }
                IClassify classify = this.method_2((iclassBreaksRenderer_1 as IClassBreaksUIProperties).Method) as IClassify;
                if (classify != null)
                {
                    object obj2;
                    object obj3;
                    bool flag;
                    ITableHistogram histogram = new BasicTableHistogramClass {
                        Field = string_0,
                        Table = itable_0,
                        NormField = string_1
                    };
                    (histogram as IDataNormalization).NormalizationType = esriDataNormalization_0;
                    (histogram as IBasicHistogram).GetHistogram(out obj2, out obj3);
                    if (classify is IClassifyMinMax2)
                    {
                        double minimum = (histogram as IStatisticsResults).Minimum;
                        double maximum = (histogram as IStatisticsResults).Maximum;
                        (classify as IClassifyMinMax2).ClassifyMinMax(minimum, maximum, ref int_0);
                    }
                    else if (classify is IDeviationInterval)
                    {
                        (classify as IDeviationInterval).Mean = (histogram as IStatisticsResults).Mean;
                        (classify as IDeviationInterval).StandardDev = (histogram as IStatisticsResults).StandardDeviation;
                        (classify as IDeviationInterval).DeviationInterval = 0.5;
                        classify.Classify(ref int_0);
                    }
                    else
                    {
                        (classify as IClassifyGEN).Classify(obj2, obj3, ref int_0);
                    }
                    iclassBreaksRenderer_1.BreakCount = int_0;
                    this.bool_0 = false;
                    this.cboClassifyNum.SelectedIndex = int_0 - 1;
                    this.bool_0 = true;
                    double[] classBreaks = (double[]) classify.ClassBreaks;
                    if (classBreaks.Length == 0)
                    {
                        icolorRamp_1.Size = 5;
                    }
                    else
                    {
                        icolorRamp_1.Size = classBreaks.Length;
                    }
                    icolorRamp_1.CreateRamp(out flag);
                    IEnumColors colors = icolorRamp_1.Colors;
                    ISymbol sym = null;
                    for (int i = 0; i < (classBreaks.Length - 1); i++)
                    {
                        IColor color = colors.Next();
                        switch (esriGeometryAny)
                        {
                            case esriGeometryType.esriGeometryLine:
                            case esriGeometryType.esriGeometryPolyline:
                            {
                                ILineSymbol symbol2 = new SimpleLineSymbolClass {
                                    Color = color
                                };
                                sym = symbol2 as ISymbol;
                                break;
                            }
                            case esriGeometryType.esriGeometryPolygon:
                            {
                                ISimpleFillSymbol symbol3 = new SimpleFillSymbolClass {
                                    Color = color,
                                    Style = esriSimpleFillStyle.esriSFSSolid
                                };
                                sym = symbol3 as ISymbol;
                                break;
                            }
                            case esriGeometryType.esriGeometryPoint:
                            case esriGeometryType.esriGeometryMultipoint:
                            {
                                IMarkerSymbol symbol4 = new SimpleMarkerSymbolClass {
                                    Color = color
                                };
                                sym = symbol4 as ISymbol;
                                break;
                            }
                        }
                        iclassBreaksRenderer_1.set_Symbol(i, sym);
                        (iclassBreaksRenderer_1 as IClassBreaksUIProperties).set_LowBreak(i, classBreaks[i]);
                        iclassBreaksRenderer_1.set_Break(i, classBreaks[i + 1]);
                        string label = classBreaks[i].ToString() + " - " + classBreaks[i + 1].ToString();
                        iclassBreaksRenderer_1.set_Label(i, label);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void method_6(int int_0, object object_0)
        {
            if (object_0 is ISymbol)
            {
                this.iclassBreaksRenderer_0.set_Symbol(int_0, object_0 as ISymbol);
            }
            else if (object_0 is string)
            {
                this.iclassBreaksRenderer_0.set_Label(int_0, object_0.ToString());
            }
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
                this.listView1.StyleGallery = this.istyleGallery_0;
            }
        }
    }
}

