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
    internal partial class SeriesChart : UserControl
    {
        private string _Title = "";
        private ChartType _Type = ChartType.Line;
        private IList m_pList = new ArrayList();

        public SeriesChart()
        {
            this.InitializeComponent();
        }

        public void Add(object dataset, string name, string XValues, string YValues, string LabelField)
        {
            SerieObject obj2 = new SerieObject
            {
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

        public void InitChart()
        {
            this.tChart1.Series.Clear();
            for (int i = 0; i < this.m_pList.Count; i++)
            {
                SerieObject obj2 = (SerieObject) this.m_pList[i];
                if (obj2.dataset is DataSet)
                {
                    this.tChart1.Series.Add(this.CreateSeriesType((obj2.dataset as DataSet).Tables[0], obj2.Name,
                        obj2.XValuesField, obj2.YValuesField, obj2.LabelField));
                }
                else if (obj2.dataset is DataTable)
                {
                    this.tChart1.Series.Add(this.CreateSeriesType(obj2.dataset as DataTable, obj2.Name,
                        obj2.XValuesField, obj2.YValuesField, obj2.LabelField));
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
                if (((field.Type != esriFieldType.esriFieldTypeBlob) &&
                     (field.Type != esriFieldType.esriFieldTypeGeometry)) &&
                    (field.Type != esriFieldType.esriFieldTypeRaster))
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
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) &&
                         (field.Type != esriFieldType.esriFieldTypeGeometry)) &&
                        (field.Type != esriFieldType.esriFieldTypeRaster))
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
                this.Add(dataset, GraphicHelper.pGraphicHelper.FiledNames[index] as string,
                    GraphicHelper.pGraphicHelper.HorFieldName, GraphicHelper.pGraphicHelper.FiledNames[index] as string,
                    "");
            }
            return true;
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
            get { return this._Title; }
            set { this._Title = value; }
        }

        public ChartType Type
        {
            get { return this._Type; }
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

        protected partial class SerieObject
        {
            public object dataset = null;
            public string LabelField = "";
            public string Name = "";
            public string XValuesField = "";
            public string YValuesField = "";
        }
    }
}