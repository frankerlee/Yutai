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
    internal class ProportionalSymbolRendererCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private StyleButton btnBackground;
        private SimpleButton btnDataExclusion;
        private StyleButton btnLineMinSymbol;
        private StyleButton btnMinSymbol;
        private ComboBoxEdit cboLegendCount;
        private ComboBoxEdit cboNormFields;
        private ComboBoxEdit cboValueFields;
        private ComboBoxEdit cboValueUnit;
        private CheckEdit chkFlannery;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBoxBackgroundSymbol;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IProportionalSymbolRenderer iproportionalSymbolRenderer_0;
        private IStyleGallery istyleGallery_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private GroupBox SymbolgroupBox1;
        private GroupBox SymbolgroupBox2;
        private SymbolItem symbolItem1;
        private SymbolItem symbolItem2;

        public ProportionalSymbolRendererCtrl()
        {
            this.InitializeComponent();
            this.symbolItem1.HasDrawLine = false;
            this.symbolItem2.HasDrawLine = false;
        }

        public void Apply()
        {
            IObjectCopy copy = new ObjectCopyClass();
            IProportionalSymbolRenderer renderer = copy.Copy(this.iproportionalSymbolRenderer_0) as IProportionalSymbolRenderer;
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
            frmDataExclusion exclusion = new frmDataExclusion {
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
                    (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByNothing;
                }
                else if (this.cboNormFields.SelectedIndex == 1)
                {
                    this.iproportionalSymbolRenderer_0.NormField = "";
                    (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByLog;
                }
                else if (this.cboNormFields.SelectedIndex > 1)
                {
                    (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByField;
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
                    else if ((this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) || (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public void GetMinMaxValue()
        {
            try
            {
                if (this.cboValueFields.SelectedIndex > 0)
                {
                    IQueryFilter filter = new QueryFilterClass {
                        SubFields = this.cboValueFields.Text
                    };
                    ITableHistogram histogram = new BasicTableHistogramClass {
                        Table = this.method_0(),
                        Field = (this.cboValueFields.SelectedItem as FieldWrap).Name
                    };
                    if (this.cboNormFields.SelectedIndex == 0)
                    {
                        (histogram as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByNothing;
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
                        double num3 = maximum / minimum;
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
                    if ((this.cboValueFields.Properties.Items[index] as FieldWrap).Name == this.iproportionalSymbolRenderer_0.Field)
                    {
                        this.cboValueFields.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
            if ((this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType == esriDataNormalization.esriNormalizeByLog)
            {
                this.cboNormFields.Text = "<LOG>";
            }
            else if ((this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType == esriDataNormalization.esriNormalizeByNothing)
            {
                this.cboNormFields.Text = "<无>";
            }
            else
            {
                for (index = 2; index < this.cboNormFields.Properties.Items.Count; index++)
                {
                    if ((this.cboNormFields.Properties.Items[index] as FieldWrap).Name == this.iproportionalSymbolRenderer_0.NormField)
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
                else if ((this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) || (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
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

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.cboNormFields = new ComboBoxEdit();
            this.cboValueFields = new ComboBoxEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.label5 = new Label();
            this.SymbolgroupBox1 = new GroupBox();
            this.chkFlannery = new CheckEdit();
            this.groupBox5 = new GroupBox();
            this.symbolItem1 = new SymbolItem();
            this.groupBox4 = new GroupBox();
            this.btnMinSymbol = new StyleButton();
            this.groupBoxBackgroundSymbol = new GroupBox();
            this.btnBackground = new StyleButton();
            this.SymbolgroupBox2 = new GroupBox();
            this.groupBox6 = new GroupBox();
            this.symbolItem2 = new SymbolItem();
            this.groupBox7 = new GroupBox();
            this.btnLineMinSymbol = new StyleButton();
            this.label3 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnDataExclusion = new SimpleButton();
            this.cboLegendCount = new ComboBoxEdit();
            this.cboValueUnit = new ComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.cboNormFields.Properties.BeginInit();
            this.cboValueFields.Properties.BeginInit();
            this.SymbolgroupBox1.SuspendLayout();
            this.chkFlannery.Properties.BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxBackgroundSymbol.SuspendLayout();
            this.SymbolgroupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cboLegendCount.Properties.BeginInit();
            this.cboValueUnit.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboNormFields);
            this.groupBox1.Controls.Add(this.cboValueFields);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xf8, 80);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            this.cboNormFields.EditValue = "";
            this.cboNormFields.Location = new System.Drawing.Point(0x58, 0x2d);
            this.cboNormFields.Name = "cboNormFields";
            this.cboNormFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboNormFields.Size = new Size(0x98, 0x17);
            this.cboNormFields.TabIndex = 0x30;
            this.cboNormFields.SelectedIndexChanged += new EventHandler(this.cboNormFields_SelectedIndexChanged);
            this.cboValueFields.EditValue = "";
            this.cboValueFields.Location = new System.Drawing.Point(0x58, 13);
            this.cboValueFields.Name = "cboValueFields";
            this.cboValueFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboValueFields.Size = new Size(0x98, 0x17);
            this.cboValueFields.TabIndex = 0x2f;
            this.cboValueFields.SelectedIndexChanged += new EventHandler(this.cboValueFields_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 0x11);
            this.label2.TabIndex = 0x27;
            this.label2.Text = "正则化字段";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 0x11);
            this.label1.TabIndex = 0x26;
            this.label1.Text = "值字段";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 0x60);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2a, 0x11);
            this.label5.TabIndex = 0x2b;
            this.label5.Text = "值单位";
            this.SymbolgroupBox1.Controls.Add(this.chkFlannery);
            this.SymbolgroupBox1.Controls.Add(this.groupBox5);
            this.SymbolgroupBox1.Controls.Add(this.groupBox4);
            this.SymbolgroupBox1.Controls.Add(this.groupBoxBackgroundSymbol);
            this.SymbolgroupBox1.Location = new System.Drawing.Point(8, 120);
            this.SymbolgroupBox1.Name = "SymbolgroupBox1";
            this.SymbolgroupBox1.Size = new Size(0x120, 0x70);
            this.SymbolgroupBox1.TabIndex = 0x2c;
            this.SymbolgroupBox1.TabStop = false;
            this.SymbolgroupBox1.Text = "符号";
            this.SymbolgroupBox1.Visible = false;
            this.chkFlannery.Location = new System.Drawing.Point(8, 80);
            this.chkFlannery.Name = "chkFlannery";
            this.chkFlannery.Properties.Caption = "外观补偿";
            this.chkFlannery.Size = new Size(80, 0x13);
            this.chkFlannery.TabIndex = 40;
            this.chkFlannery.CheckedChanged += new EventHandler(this.chkFlannery_CheckedChanged);
            this.groupBox5.Controls.Add(this.symbolItem1);
            this.groupBox5.Location = new System.Drawing.Point(0xb8, 0x13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x60, 0x55);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "最大值";
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new System.Drawing.Point(9, 0x12);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(80, 0x38);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 0;
            this.groupBox4.Controls.Add(this.btnMinSymbol);
            this.groupBox4.Location = new System.Drawing.Point(0x60, 0x13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(80, 0x35);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "最小值";
            this.btnMinSymbol.Location = new System.Drawing.Point(8, 20);
            this.btnMinSymbol.Name = "btnMinSymbol";
            this.btnMinSymbol.Size = new Size(0x40, 0x18);
            this.btnMinSymbol.Style = null;
            this.btnMinSymbol.TabIndex = 0x2a;
            this.btnMinSymbol.Click += new EventHandler(this.btnMinSymbol_Click);
            this.groupBoxBackgroundSymbol.Controls.Add(this.btnBackground);
            this.groupBoxBackgroundSymbol.Location = new System.Drawing.Point(8, 0x13);
            this.groupBoxBackgroundSymbol.Name = "groupBoxBackgroundSymbol";
            this.groupBoxBackgroundSymbol.Size = new Size(80, 0x35);
            this.groupBoxBackgroundSymbol.TabIndex = 0;
            this.groupBoxBackgroundSymbol.TabStop = false;
            this.groupBoxBackgroundSymbol.Text = "背景";
            this.btnBackground.Location = new System.Drawing.Point(8, 20);
            this.btnBackground.Name = "btnBackground";
            this.btnBackground.Size = new Size(0x40, 0x18);
            this.btnBackground.Style = null;
            this.btnBackground.TabIndex = 0x2a;
            this.btnBackground.Click += new EventHandler(this.btnBackground_Click);
            this.SymbolgroupBox2.Controls.Add(this.groupBox6);
            this.SymbolgroupBox2.Controls.Add(this.groupBox7);
            this.SymbolgroupBox2.Location = new System.Drawing.Point(8, 120);
            this.SymbolgroupBox2.Name = "SymbolgroupBox2";
            this.SymbolgroupBox2.Size = new Size(0xd0, 0x70);
            this.SymbolgroupBox2.TabIndex = 0x2d;
            this.SymbolgroupBox2.TabStop = false;
            this.SymbolgroupBox2.Text = "符号";
            this.SymbolgroupBox2.Visible = false;
            this.groupBox6.Controls.Add(this.symbolItem2);
            this.groupBox6.Location = new System.Drawing.Point(0x60, 0x10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x60, 0x55);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "最大值";
            this.symbolItem2.BackColor = SystemColors.ControlLight;
            this.symbolItem2.Location = new System.Drawing.Point(8, 0x13);
            this.symbolItem2.Name = "symbolItem2";
            this.symbolItem2.Size = new Size(80, 0x38);
            this.symbolItem2.Symbol = null;
            this.symbolItem2.TabIndex = 1;
            this.groupBox7.Controls.Add(this.btnLineMinSymbol);
            this.groupBox7.Location = new System.Drawing.Point(8, 0x10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(80, 0x35);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "最小值";
            this.btnLineMinSymbol.Location = new System.Drawing.Point(8, 20);
            this.btnLineMinSymbol.Name = "btnLineMinSymbol";
            this.btnLineMinSymbol.Size = new Size(0x40, 0x18);
            this.btnLineMinSymbol.Style = null;
            this.btnLineMinSymbol.TabIndex = 0x2a;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 240);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x74, 0x11);
            this.label3.TabIndex = 0x2f;
            this.label3.Text = "图例中显示的符号数";
            this.groupBox2.Controls.Add(this.btnDataExclusion);
            this.groupBox2.Location = new System.Drawing.Point(0x110, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x80, 80);
            this.groupBox2.TabIndex = 0x30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据";
            this.btnDataExclusion.Location = new System.Drawing.Point(0x20, 0x20);
            this.btnDataExclusion.Name = "btnDataExclusion";
            this.btnDataExclusion.Size = new Size(0x40, 0x20);
            this.btnDataExclusion.TabIndex = 0;
            this.btnDataExclusion.Text = "排除...";
            this.btnDataExclusion.Click += new EventHandler(this.btnDataExclusion_Click);
            this.cboLegendCount.EditValue = "";
            this.cboLegendCount.Location = new System.Drawing.Point(0x80, 0xed);
            this.cboLegendCount.Name = "cboLegendCount";
            this.cboLegendCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLegendCount.Properties.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            this.cboLegendCount.Size = new Size(0x98, 0x17);
            this.cboLegendCount.TabIndex = 50;
            this.cboLegendCount.SelectedIndexChanged += new EventHandler(this.cboLegendCount_SelectedIndexChanged);
            this.cboValueUnit.EditValue = "";
            this.cboValueUnit.Location = new System.Drawing.Point(70, 0x5c);
            this.cboValueUnit.Name = "cboValueUnit";
            this.cboValueUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboValueUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米" });
            this.cboValueUnit.Size = new Size(0x98, 0x17);
            this.cboValueUnit.TabIndex = 0x31;
            this.cboValueUnit.SelectedIndexChanged += new EventHandler(this.cboValueUnit_SelectedIndexChanged);
            base.Controls.Add(this.cboLegendCount);
            base.Controls.Add(this.cboValueUnit);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.SymbolgroupBox1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.SymbolgroupBox2);
            base.Name = "ProportionalSymbolRendererCtrl";
            base.Size = new Size(0x1a0, 0x108);
            base.Load += new EventHandler(this.ProportionalSymbolRendererCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboNormFields.Properties.EndInit();
            this.cboValueFields.Properties.EndInit();
            this.SymbolgroupBox1.ResumeLayout(false);
            this.chkFlannery.Properties.EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBoxBackgroundSymbol.ResumeLayout(false);
            this.SymbolgroupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.cboLegendCount.Properties.EndInit();
            this.cboValueUnit.Properties.EndInit();
            base.ResumeLayout(false);
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
                        this.iproportionalSymbolRenderer_0.ValueRepresentation = esriValueRepresentations.esriValueRepUnknown;
                        this.iproportionalSymbolRenderer_0.Field = "";
                        (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationField = "";
                        (this.iproportionalSymbolRenderer_0 as IDataNormalization).NormalizationType = esriDataNormalization.esriNormalizeByNothing;
                        if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            IFillSymbol symbol = new SimpleFillSymbolClass {
                                Color = this.method_3(0x17)
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
            IRandomColorRamp ramp = new RandomColorRampClass {
                StartHue = 40,
                EndHue = 120,
                MinValue = 0x41,
                MaxValue = 90,
                MinSaturation = 0x19,
                MaxSaturation = 0x2d,
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
            return new RgbColorClass { Red = (int) (255.0 * random.NextDouble()), Green = (int) (255.0 * random.NextDouble()), Blue = (int) (255.0 * random.NextDouble()) };
        }

        private ISymbol method_5()
        {
            if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                ILineSymbol symbol = new SimpleLineSymbolClass {
                    Width = 1.0
                };
                (symbol as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSSolid;
                symbol.Color = this.method_4();
                return (symbol as ISymbol);
            }
            IMarkerSymbol symbol3 = new SimpleMarkerSymbolClass {
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
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public IProportionalSymbolRenderer ProportionalSymbolRenderer
        {
            get
            {
                return this.iproportionalSymbolRenderer_0;
            }
            set
            {
                this.iproportionalSymbolRenderer_0 = value;
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

