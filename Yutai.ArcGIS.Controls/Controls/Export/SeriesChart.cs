using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class SeriesChart : UserControl
    {
        private string _Title = "";
        private ChartType _Type = ChartType.Line;
        private Button btnExport;
        private Button btnPrint;
        private Button btnPrintPreview;
        private Button button1;
        private ComboBox comboBox1;
        private Container components = null;
        private Label label1;
        private IList m_pList = new ArrayList();
        private TChart tChart1;

        public SeriesChart()
        {
            this.InitializeComponent();
        }

        public void Add(object dataset, string name, string XValues, string YValues, string LabelField)
        {
            SerieObject obj2 = new SerieObject {
                dataset = dataset,
                YValuesField = YValues,
                Name = name,
                LabelField = XValues
            };
            this.m_pList.Add(obj2);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.tChart1.Export.ShowExportDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.tChart1.Printer.Print();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            this.tChart1.Printer.Preview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tChart1.ShowEditor();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                this._Type = (ChartType) this.comboBox1.SelectedIndex;
                this.InitChart();
            }
        }

        private Series CreateSeriesType(DataTable dt, string Name, string XValues, string YValues, string Label)
        {
            Series series;
            switch (this._Type)
            {
                case ChartType.Bar:
                    series = new Bar();
                    break;

                case ChartType.Pie:
                    series = new Pie();
                    break;

                case ChartType.Area:
                    series = new Area();
                    break;

                default:
                    series = new Line();
                    break;
            }
            try
            {
                series.Title = Name;
                series.DataSource = dt;
                series.LabelMember = Label;
                series.YValues.DataMember = YValues;
                series.YValues.Order = ValueListOrder.None;
            }
            catch (Exception)
            {
            }
            return series;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void InitChart()
        {
            this.tChart1.Series.Clear();
            for (int i = 0; i < this.m_pList.Count; i++)
            {
                SerieObject obj2 = (SerieObject) this.m_pList[i];
                if (obj2.dataset is DataSet)
                {
                    this.tChart1.Series.Add(this.CreateSeriesType((obj2.dataset as DataSet).Tables[0], obj2.Name, obj2.XValuesField, obj2.YValuesField, obj2.LabelField));
                }
                else if (obj2.dataset is DataTable)
                {
                    this.tChart1.Series.Add(this.CreateSeriesType(obj2.dataset as DataTable, obj2.Name, obj2.XValuesField, obj2.YValuesField, obj2.LabelField));
                }
            }
            this.tChart1.Text = this._Title;
        }

        public bool InitGraphic()
        {
            IField field;
            if (GraphicHelper.pGraphicHelper.DataSource == null)
            {
                return false;
            }
            this.m_pList.Clear();
            ICursor o = GraphicHelper.pGraphicHelper.Cursor;
            DataTable dataset = new DataTable();
            this._Title = GraphicHelper.pGraphicHelper.Title;
            IFields fields = o.Fields;
            int index = 0;
            while (index < fields.FieldCount)
            {
                field = fields.get_Field(index);
                if (((field.Type != esriFieldType.esriFieldTypeBlob) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                {
                    dataset.Columns.Add(field.AliasName);
                }
                index++;
            }
            object[] values = new object[dataset.Columns.Count];
            int num2 = 0;
            for (IRow row = o.NextRow(); row != null; row = o.NextRow())
            {
                num2 = 0;
                index = 0;
                while (index < fields.FieldCount)
                {
                    field = fields.get_Field(index);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                    {
                        values[num2++] = row.get_Value(index);
                    }
                    index++;
                }
                dataset.Rows.Add(values);
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
            for (index = 0; index < GraphicHelper.pGraphicHelper.FiledNames.Count; index++)
            {
                this.Add(dataset, GraphicHelper.pGraphicHelper.FiledNames[index] as string, GraphicHelper.pGraphicHelper.HorFieldName, GraphicHelper.pGraphicHelper.FiledNames[index] as string, "");
            }
            return true;
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.comboBox1 = new ComboBox();
            this.label1 = new Label();
            this.tChart1 = new TChart();
            this.btnExport = new Button();
            this.btnPrint = new Button();
            this.btnPrintPreview = new Button();
            base.SuspendLayout();
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.Location = new Point(360, 8);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x40, 0x18);
            this.button1.TabIndex = 6;
            this.button1.Text = "编辑...";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.comboBox1.Items.AddRange(new object[] { "线状图表", "柱状图表", "饼状图表", "面积图表" });
            this.comboBox1.Location = new Point(0x60, 0xe8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x90, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0xe8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "选择图表类型";
            this.tChart1.Aspect.SmoothingMode = SmoothingMode.HighSpeed;
            this.tChart1.Aspect.TextRenderingHint = TextRenderingHint.SystemDefault;
            this.tChart1.Axes.Bottom.AxisPen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Grid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Grid.Style = DashStyle.Dot;
            this.tChart1.Axes.Bottom.Labels.Align = AxisLabelAlign.Default;
            this.tChart1.Axes.Bottom.Labels.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Bottom.Labels.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Labels.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Labels.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Labels.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Labels.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Labels.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Labels.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Labels.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Labels.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Labels.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Labels.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Bottom.Labels.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Labels.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Labels.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Labels.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Labels.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Bottom.Labels.Style = AxisLabelStyle.Auto;
            this.tChart1.Axes.Bottom.MinorGrid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.MinorTicks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.PositionUnits = PositionUnits.Percent;
            this.tChart1.Axes.Bottom.Ticks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.TicksInner.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Title.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Bottom.Title.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Title.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Title.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Title.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Title.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Title.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Title.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Title.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Title.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Title.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Title.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Title.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Title.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Title.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Bottom.Title.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Bottom.Title.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Bottom.Title.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Bottom.Title.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Bottom.Title.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Depth.AxisPen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Grid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Grid.Style = DashStyle.Dot;
            this.tChart1.Axes.Depth.Labels.Align = AxisLabelAlign.Default;
            this.tChart1.Axes.Depth.Labels.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Depth.Labels.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Labels.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Labels.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Labels.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Labels.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Labels.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Labels.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Labels.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Labels.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Labels.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Labels.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Labels.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Labels.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Labels.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Depth.Labels.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Labels.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Labels.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Labels.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Labels.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Depth.Labels.Style = AxisLabelStyle.Auto;
            this.tChart1.Axes.Depth.MinorGrid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.MinorTicks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.PositionUnits = PositionUnits.Percent;
            this.tChart1.Axes.Depth.Ticks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.TicksInner.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Title.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Depth.Title.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Title.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Title.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Title.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Title.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Title.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Title.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Title.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Title.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Title.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Title.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Title.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Title.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Title.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Depth.Title.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Depth.Title.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Depth.Title.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Depth.Title.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Depth.Title.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.DepthTop.AxisPen.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Grid.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Grid.Style = DashStyle.Dot;
            this.tChart1.Axes.DepthTop.Labels.Align = AxisLabelAlign.Default;
            this.tChart1.Axes.DepthTop.Labels.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.DepthTop.Labels.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Labels.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Labels.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Labels.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Labels.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Labels.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Labels.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Labels.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.DepthTop.Labels.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Labels.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Labels.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Labels.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Labels.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.DepthTop.Labels.Style = AxisLabelStyle.Auto;
            this.tChart1.Axes.DepthTop.MinorGrid.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.MinorTicks.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.PositionUnits = PositionUnits.Percent;
            this.tChart1.Axes.DepthTop.Ticks.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.TicksInner.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Title.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.DepthTop.Title.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Title.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Title.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Title.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Title.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Title.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Title.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Title.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Title.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Title.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Title.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.DepthTop.Title.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.DepthTop.Title.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.DepthTop.Title.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.DepthTop.Title.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.DepthTop.Title.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Left.AxisPen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Grid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Grid.Style = DashStyle.Dot;
            this.tChart1.Axes.Left.Labels.Align = AxisLabelAlign.Default;
            this.tChart1.Axes.Left.Labels.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Left.Labels.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Labels.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Labels.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Labels.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Labels.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Labels.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Labels.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Labels.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Labels.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Labels.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Labels.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Labels.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Labels.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Labels.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Left.Labels.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Labels.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Labels.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Labels.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Labels.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Left.Labels.Style = AxisLabelStyle.Auto;
            this.tChart1.Axes.Left.MinorGrid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.MinorTicks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.PositionUnits = PositionUnits.Percent;
            this.tChart1.Axes.Left.Ticks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.TicksInner.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Title.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Left.Title.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Title.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Title.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Title.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Title.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Title.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Title.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Title.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Title.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Title.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Title.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Title.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Title.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Title.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Left.Title.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Left.Title.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Left.Title.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Left.Title.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Left.Title.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Right.AxisPen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Grid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Grid.Style = DashStyle.Dot;
            this.tChart1.Axes.Right.Labels.Align = AxisLabelAlign.Default;
            this.tChart1.Axes.Right.Labels.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Right.Labels.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Labels.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Labels.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Labels.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Labels.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Labels.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Labels.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Labels.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Labels.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Labels.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Labels.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Labels.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Labels.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Labels.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Right.Labels.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Labels.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Labels.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Labels.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Labels.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Right.Labels.Style = AxisLabelStyle.Auto;
            this.tChart1.Axes.Right.MinorGrid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.MinorTicks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.PositionUnits = PositionUnits.Percent;
            this.tChart1.Axes.Right.Ticks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.TicksInner.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Title.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Right.Title.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Title.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Title.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Title.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Title.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Title.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Title.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Title.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Title.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Title.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Title.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Title.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Title.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Title.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Right.Title.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Right.Title.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Right.Title.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Right.Title.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Right.Title.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Top.AxisPen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Grid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Grid.Style = DashStyle.Dot;
            this.tChart1.Axes.Top.Labels.Align = AxisLabelAlign.Default;
            this.tChart1.Axes.Top.Labels.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Top.Labels.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Labels.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Labels.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Labels.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Labels.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Labels.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Labels.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Labels.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Labels.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Labels.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Labels.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Labels.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Labels.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Labels.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Top.Labels.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Labels.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Labels.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Labels.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Labels.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Axes.Top.Labels.Style = AxisLabelStyle.Auto;
            this.tChart1.Axes.Top.MinorGrid.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.MinorTicks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.PositionUnits = PositionUnits.Percent;
            this.tChart1.Axes.Top.Ticks.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.TicksInner.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Title.Bevel.Inner = BevelStyles.None;
            this.tChart1.Axes.Top.Title.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Title.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Title.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Title.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Title.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Title.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Title.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Title.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Title.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Title.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Title.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Title.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Title.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Title.ImageMode = ImageMode.Stretch;
            this.tChart1.Axes.Top.Title.Pen.DashCap = DashCap.Flat;
            this.tChart1.Axes.Top.Title.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Axes.Top.Title.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Axes.Top.Title.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Axes.Top.Title.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Footer.Alignment = StringAlignment.Center;
            this.tChart1.Footer.Bevel.Inner = BevelStyles.None;
            this.tChart1.Footer.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Footer.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Footer.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Footer.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Footer.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Footer.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Footer.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Footer.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Footer.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Footer.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Footer.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Footer.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Footer.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Footer.ImageMode = ImageMode.Stretch;
            this.tChart1.Footer.Pen.DashCap = DashCap.Flat;
            this.tChart1.Footer.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Footer.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Footer.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Footer.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Header.Alignment = StringAlignment.Center;
            this.tChart1.Header.Bevel.Inner = BevelStyles.None;
            this.tChart1.Header.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Header.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Header.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Header.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Header.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Header.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Header.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Header.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Header.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Header.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Header.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Header.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Header.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Header.ImageMode = ImageMode.Stretch;
            this.tChart1.Header.Lines = new string[] { "TeeChart" };
            this.tChart1.Header.Pen.DashCap = DashCap.Flat;
            this.tChart1.Header.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Header.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Header.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Header.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Legend.Alignment = LegendAlignments.Right;
            this.tChart1.Legend.Bevel.Inner = BevelStyles.None;
            this.tChart1.Legend.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Legend.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Legend.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Legend.DividingLines.DashCap = DashCap.Flat;
            this.tChart1.Legend.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Legend.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Legend.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Legend.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Legend.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Legend.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Legend.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Legend.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Legend.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Legend.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Legend.ImageMode = ImageMode.Stretch;
            this.tChart1.Legend.LegendStyle = LegendStyles.Auto;
            this.tChart1.Legend.Pen.DashCap = DashCap.Flat;
            this.tChart1.Legend.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Legend.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Legend.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Legend.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.Legend.Symbol.Pen.DashCap = DashCap.Flat;
            this.tChart1.Legend.Symbol.Position = LegendSymbolPosition.Left;
            this.tChart1.Legend.Symbol.WidthUnits = LegendSymbolSize.Percent;
            this.tChart1.Legend.TextStyle = LegendTextStyles.LeftValue;
            this.tChart1.Location = new Point(0, 0);
            this.tChart1.Name = "tChart1";
            this.tChart1.Panel.Bevel.Inner = BevelStyles.None;
            this.tChart1.Panel.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Panel.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Panel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Panel.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Panel.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Panel.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Panel.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Panel.ImageMode = ImageMode.Stretch;
            this.tChart1.Panel.MarginUnits = PanelMarginUnits.Percent;
            this.tChart1.Panel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Panel.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Panel.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Panel.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Panning.Allow = ScrollModes.Both;
            this.tChart1.Panning.MouseButton = MouseButtons.Right;
            this.tChart1.Printer.MarginUnits = PrintMarginUnits.HundredthsInch;
            this.tChart1.Size = new Size(0x160, 0xe0);
            this.tChart1.SubFooter.Alignment = StringAlignment.Center;
            this.tChart1.SubFooter.Bevel.Inner = BevelStyles.None;
            this.tChart1.SubFooter.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubFooter.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubFooter.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubFooter.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubFooter.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubFooter.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubFooter.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubFooter.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubFooter.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubFooter.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubFooter.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubFooter.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubFooter.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.SubFooter.ImageMode = ImageMode.Stretch;
            this.tChart1.SubFooter.Pen.DashCap = DashCap.Flat;
            this.tChart1.SubFooter.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubFooter.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubFooter.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubFooter.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.SubHeader.Alignment = StringAlignment.Center;
            this.tChart1.SubHeader.Bevel.Inner = BevelStyles.None;
            this.tChart1.SubHeader.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubHeader.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubHeader.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubHeader.Font.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubHeader.Font.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubHeader.Font.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubHeader.Font.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubHeader.Font.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubHeader.Font.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubHeader.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubHeader.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubHeader.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubHeader.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.SubHeader.ImageMode = ImageMode.Stretch;
            this.tChart1.SubHeader.Pen.DashCap = DashCap.Flat;
            this.tChart1.SubHeader.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.SubHeader.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.SubHeader.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.SubHeader.ShapeStyle = TextShapeStyle.Rectangle;
            this.tChart1.TabIndex = 9;
            this.tChart1.Walls.Back.Bevel.Inner = BevelStyles.None;
            this.tChart1.Walls.Back.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Back.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Back.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Back.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Back.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Back.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Back.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Back.ImageMode = ImageMode.Stretch;
            this.tChart1.Walls.Back.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Back.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Back.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Back.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Bottom.Bevel.Inner = BevelStyles.None;
            this.tChart1.Walls.Bottom.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Bottom.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Bottom.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Bottom.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Bottom.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Bottom.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Bottom.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Bottom.ImageMode = ImageMode.Stretch;
            this.tChart1.Walls.Bottom.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Bottom.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Bottom.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Bottom.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Left.Bevel.Inner = BevelStyles.None;
            this.tChart1.Walls.Left.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Left.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Left.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Left.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Left.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Left.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Left.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Left.ImageMode = ImageMode.Stretch;
            this.tChart1.Walls.Left.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Left.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Left.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Left.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Right.Bevel.Inner = BevelStyles.None;
            this.tChart1.Walls.Right.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Right.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Right.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Right.ImageBevel.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Right.ImageBevel.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Right.ImageBevel.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Right.ImageBevel.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Right.ImageMode = ImageMode.Stretch;
            this.tChart1.Walls.Right.Pen.DashCap = DashCap.Flat;
            this.tChart1.Walls.Right.Shadow.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Walls.Right.Shadow.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Walls.Right.Shadow.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Walls.Right.Visible = false;
            this.tChart1.Zoom.Brush.Gradient.Direction = LinearGradientMode.Vertical;
            this.tChart1.Zoom.Brush.Gradient.WrapMode = WrapMode.Clamp;
            this.tChart1.Zoom.Brush.Style = HatchStyle.BackwardDiagonal;
            this.tChart1.Zoom.Direction = ZoomDirections.Both;
            this.tChart1.Zoom.KeyShift = Keys.None;
            this.tChart1.Zoom.MouseButton = MouseButtons.Left;
            this.tChart1.Zoom.Pen.DashCap = DashCap.Flat;
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.Location = new Point(360, 40);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(0x40, 0x18);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "导出...";
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.Location = new Point(360, 0x48);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(0x40, 0x18);
            this.btnPrint.TabIndex = 11;
            this.btnPrint.Text = "打印...";
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.btnPrintPreview.FlatStyle = FlatStyle.Flat;
            this.btnPrintPreview.Location = new Point(360, 0x68);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new Size(0x40, 0x18);
            this.btnPrintPreview.TabIndex = 12;
            this.btnPrintPreview.Text = "打印预览";
            this.btnPrintPreview.Click += new EventHandler(this.btnPrintPreview_Click);
            base.Controls.Add(this.btnPrintPreview);
            base.Controls.Add(this.btnPrint);
            base.Controls.Add(this.btnExport);
            base.Controls.Add(this.tChart1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.button1);
            base.Name = "SeriesChart";
            base.Size = new Size(0x1b3, 0x110);
            base.Load += new EventHandler(this.SeriesChart_Load);
            base.VisibleChanged += new EventHandler(this.SeriesChart_VisibleChanged);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void SeriesChart_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = (int) this._Type;
            this.InitChart();
        }

        private void SeriesChart_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.InitGraphic();
                this.InitChart();
            }
        }

        public string ChartTitle
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public ChartType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
                this.comboBox1.SelectedIndex = (int) this._Type;
            }
        }

        public enum ChartType
        {
            Line,
            Bar,
            Pie,
            Area
        }

        protected class SerieObject
        {
            public object dataset = null;
            public string LabelField = "";
            public string Name = "";
            public string XValuesField = "";
            public string YValuesField = "";
        }
    }
}

