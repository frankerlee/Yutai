using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Infragistics.Win;
using Yutai.Pipeline.Analysis.QueryForms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Pipeline.Analysis.Views
{
    public partial class QueryResultView : DockPanelControlBase,IQueryResultView
    {
        private IEnumerable<ToolStripItemCollection> _toolStrips;
        private IEnumerable<Control> _buttons;
        private IAppContext _context;
        private IFeatureCursor _cursor;
        private IFeatureSelection _selection;
        private DataSet pDataSet = new DataSet("总表");
        private DataTable stable = new DataTable();
        private DataTable Geotable = new DataTable();
        private int nGeoType = -1;
        private bool bFitWidth = true;
        private bool bShowGeo = true;
        private IGeometry AllGeo;
        private bool bControlEvent;
        private int OidField;
       // private CustomColumnChooser customColumnChooserDialog;
        private string TopBandText;
        private FontData TopBandFont;
        private Color TopBandColor;
        private bool bHaveTop;

        public QueryResultView()
        {
            InitializeComponent();
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }


        public override Bitmap Image
        {
            get { return Properties.Resources.icon_excel; }
        }

        public override string Caption
        {
            get { return "查询结果"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Bottom; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "PipelineAnalysis_QueryResult";
        public IFeatureCursor Cursor
        {
            get
            {
                return this._cursor;
            }
            set
            {
                this._cursor = value;
            }
        }

        public void Initialize(IAppContext context)
        {
            _context = context;
        }

        public void SetResult(IFeatureCursor cursor, IFeatureSelection selection)
        {
            _cursor = cursor;
            _selection = selection;
            InitCombos();
        }

        private void InitCombos()
        {
            this.cmbStatWay.SelectedIndex = 0;
            this.bControlEvent = true;
            this.UpdateGrid();
            this.bControlEvent = false;
        }

        private void UpdateGrid()
        {
            this.gridControl1.DataSource = (null);
            Splash.Show();
            Splash.Status = "状态:正在查询,请稍候...";
            try
            {
                if (this.MakeData())
                {
                    this.pDataSet.Tables.Add(this.stable);
                    this.pDataSet.Tables.Add(this.Geotable);
                    DataRelation relation = new DataRelation("空间表", this.stable.Columns[0], this.Geotable.Columns[0]);
                    this.pDataSet.Relations.Add(relation);
                    this.gridControl1.DataSource = (this.pDataSet.Tables[0]);
                    for (int i = 0; i < this.mainGridView.Columns.Count; i++)
                    {
                        this.mainGridView.Columns[i].BestFit();
                        this.mainGridView.Columns[i].Width = ((int)((double)this.mainGridView.Columns[i].Width * 1.4));
                        string key = this.mainGridView.Columns[i].FieldName;
                        if (key == "管径" || key == "沟截面宽高")
                        {
                            this.mainGridView.Columns[i].Caption = (key + "[毫米]");
                        }
                        else if (key.ToUpper() == "X" || key.ToUpper() == "Y" || key == "地面高程" || key == "起点高程" || key == "终点高程" || key == "起点埋深" || key == "终点埋深")
                        {
                            this.mainGridView.Columns[i].Caption = (key + "[米]");
                        }
                        else if (key == "电压")
                        {
                            this.mainGridView.Columns[i].Caption = ("电压[千伏]");
                        }
                        else if (key == "压力")
                        {
                            this.mainGridView.Columns[i].Caption = ("压力[兆帕]");
                        }
                        Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
                        if (!regex.IsMatch(key))
                        {
                            this.mainGridView.Columns[i].Visible = (false);
                        }
                    }
                    this.FormatCurrencyColumns();
                }
                Splash.Close();
            }
            catch
            {
                Splash.Close();
            }
        }


        private void FormatCurrencyColumns()
        {
           
        }

        private bool MakeData()
        {
            Splash.Status = "状态:创建临时表,请稍候...";
            this.pDataSet = new DataSet("总表");
            this.stable = new DataTable("属性表");
            this.Geotable = new DataTable("空间");
            this.cmbStatField.Items.Clear();
            bool result;
            if (this._cursor == null)
            {
                result = false;
            }
            else
            {
                IFields fields = this._cursor.Fields;
                int num = 0;
                this.OidField = 0;
                this.stable.Columns.Add("OID", typeof(string));
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    string name = field.Name;
                    if (field.Type == (esriFieldType)7)
                    {
                        num = i;
                    }
                    if (field.Type == (esriFieldType)6)
                    {
                        this.OidField = i;
                    }
                    else
                    {
                        Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
                        if (regex.IsMatch(field.Name))
                        {
                            if (field.Type == (esriFieldType)3 || field.Type == (esriFieldType)1 || field.Type == (esriFieldType)2 || field.Type == 0)
                            {
                                this.cmbCalField.Items.Add(field.Name);
                            }
                            this.cmbStatField.Items.Add(fields.get_Field(i).Name);
                        }
                        if (!this.stable.Columns.Contains(fields.get_Field(i).Name))
                        {
                            switch ((int)field.Type)
                            {
                                case 1:
                                    this.stable.Columns.Add(name, typeof(int));
                                    continue;
                                case 2:
                                    this.stable.Columns.Add(name, typeof(float));
                                    continue;
                                case 3:
                                    this.stable.Columns.Add(name, typeof(double));
                                    continue;
                                case 6:
                                    this.stable.Columns.Add(name, typeof(string));
                                    continue;
                            }
                            this.stable.Columns.Add(name, typeof(string));
                        }
                    }
                   
                }
                if (this.cmbCalField.Items.Count > 0)
                {
                    this.cmbCalField.SelectedIndex = 0;
                }
                IFeature feature = this._cursor.NextFeature();
                bool flag = false;
                if (feature == null)
                {
                    result = false;
                }
                else
                {
                    if (feature.FeatureType == (esriFeatureType)10 || feature.FeatureType == (esriFeatureType)8)
                    {
                        this.cmbStatWay.Items.Add("统计长度");
                        if (!this.stable.Columns.Contains("GD管线长度"))
                        {
                            this.stable.Columns.Add("GD管线长度", typeof(double));
                        }
                        flag = true;
                        this.nGeoType = 1;
                    }
                    else if (feature.FeatureType == (esriFeatureType)7 || feature.FeatureType == (esriFeatureType)9)
                    {
                        this.nGeoType = 0;
                    }
                    else if (feature.FeatureType == (esriFeatureType)1)
                    {
                        if (feature.Shape.GeometryType == (esriGeometryType)1)
                        {
                            this.nGeoType = 0;
                        }
                        if (feature.Shape.GeometryType == (esriGeometryType)6 || feature.Shape.GeometryType == (esriGeometryType)3)
                        {
                            this.nGeoType = 3;
                        }
                        if (feature.Shape.GeometryType == (esriGeometryType)4)
                        {
                            this.nGeoType = 2;
                        }
                        if (feature.Shape.GeometryType == 0)
                        {
                            this.nGeoType = -1;
                        }
                    }
                    if (feature.FeatureType == (esriFeatureType)11)
                    {
                        this.nGeoType = 4;
                    }
                    if (this.nGeoType == -1)
                    {
                        result = false;
                    }
                    else
                    {
                        if (this.nGeoType > 0)
                        {
                            if (!this.Geotable.Columns.Contains("LINECODE"))
                            {
                                this.Geotable.Columns.Add("LINECODE", typeof(string));
                            }
                            if (!this.Geotable.Columns.Contains("序号"))
                            {
                                this.Geotable.Columns.Add("序号", typeof(int));
                            }
                            if (!this.Geotable.Columns.Contains("X"))
                            {
                                this.Geotable.Columns.Add("X", typeof(double));
                            }
                            if (!this.Geotable.Columns.Contains("Y"))
                            {
                                this.Geotable.Columns.Add("Y", typeof(double));
                            }
                            if (!this.Geotable.Columns.Contains("Z"))
                            {
                                this.Geotable.Columns.Add("Z", typeof(double));
                            }
                            if (!this.Geotable.Columns.Contains("M"))
                            {
                                this.Geotable.Columns.Add("M", typeof(double));
                            }
                        }
                        else
                        {
                            if (!this.Geotable.Columns.Contains("PONTCODE"))
                            {
                                this.Geotable.Columns.Add("PONTCODE", typeof(string));
                            }
                            if (!this.Geotable.Columns.Contains("X"))
                            {
                                this.Geotable.Columns.Add("X", typeof(double));
                            }
                            if (!this.Geotable.Columns.Contains("Y"))
                            {
                                this.Geotable.Columns.Add("Y", typeof(double));
                            }
                            if (!this.Geotable.Columns.Contains("Z"))
                            {
                                this.Geotable.Columns.Add("Z", typeof(double));
                            }
                        }
                        object[] array;
                        object[] array2;
                        if (this.nGeoType > 0)
                        {
                            if (this.nGeoType == 1)
                            {
                                array = new object[fields.FieldCount + 1];
                            }
                            else
                            {
                                array = new object[fields.FieldCount];
                            }
                            array2 = new object[6];
                        }
                        else
                        {
                            array = new object[fields.FieldCount];
                            array2 = new object[4];
                        }
                        string text = feature.FeatureType.ToString();
                        this._selection.Clear();
                        Splash.Status = "状态:填充表单,请稍候...";
                        while (feature != null)
                        {
                            double num2 = 0.0;
                            string text2 = feature.get_Value(this.OidField).ToString();
                            if (feature.Shape == null || feature.Shape.IsEmpty)
                            {
                                feature = this._cursor.NextFeature();
                            }
                            else
                            {
                                int j;
                                for (j = 0; j < fields.FieldCount; j++)
                                {
                                    if (j == this.OidField)
                                    {
                                        array[0] = text2;
                                    }
                                    if (j == num)
                                    {
                                        if (num > this.OidField)
                                        {
                                            array[j] = text;
                                        }
                                        else
                                        {
                                            array[j + 1] = text;
                                        }
                                        if (this.nGeoType == 1 || this.nGeoType == 3)
                                        {
                                            IPolyline polyline = (IPolyline)feature.Shape;
                                            IPointCollection pointCollection = (IPointCollection)polyline;
                                            IPoint point = null;
                                            IPoint point2 = null;
                                            for (int k = 0; k < pointCollection.PointCount - 1; k++)
                                            {
                                                point = pointCollection.get_Point(k);
                                                array2[0] = text2;
                                                array2[1] = k + 1;
                                                array2[2] = point.X;
                                                array2[3] = point.Y;
                                                if (this.nGeoType == 3)
                                                {
                                                    array2[4] = point.Z;
                                                    array2[5] = 0;
                                                }
                                                else
                                                {
                                                    array2[4] = ((float)(point.Z - point.M)).ToString("f3");
                                                    array2[5] = point.M.ToString("f3");
                                                }
                                                this.Geotable.Rows.Add(array2);
                                                point2 = pointCollection.get_Point(k + 1);
                                                num2 += Math.Sqrt(Math.Pow(point.X - point2.X, 2.0) + Math.Pow(point.Y - point2.Y, 2.0) + Math.Pow(point.Z - point.M - point2.Z + point2.M, 2.0));
                                            }
                                            array2[0] = text2;
                                            array2[1] = pointCollection.PointCount;
                                            array2[2] = point2.X;
                                            array2[3] = point2.Y;
                                            if (this.nGeoType == 3)
                                            {
                                                array2[4] = point2.Z;
                                                array2[5] = 0;
                                            }
                                            else
                                            {
                                                array2[4] = ((float)(point.Z - point.M)).ToString("f3");
                                                array2[5] = point2.M.ToString("f3");
                                            }
                                            this.Geotable.Rows.Add(array2);
                                        }
                                        else if (this.nGeoType == 0)
                                        {
                                            IPoint point3 = (IPoint)feature.Shape;
                                            array2[0] = text2;
                                            array2[1] = point3.X;
                                            array2[2] = point3.Y;
                                            array2[3] = point3.Z;
                                            this.Geotable.Rows.Add(array2);
                                        }
                                        else if (this.nGeoType == 2 || this.nGeoType == 4)
                                        {
                                            IPolygon polygon = (IPolygon)feature.Shape;
                                            IPointCollection pointCollection2 = (IPointCollection)polygon;
                                            IPoint point4 = null;
                                            for (int l = 0; l < pointCollection2.PointCount - 1; l++)
                                            {
                                                IPoint point5 = pointCollection2.get_Point(l);
                                                array2[0] = text2;
                                                array2[1] = l + 1;
                                                array2[2] = point5.X;
                                                array2[3] = point5.Y;
                                                array2[4] = point5.Z;
                                                array2[5] = point5.M;
                                                this.Geotable.Rows.Add(array2);
                                                point4 = pointCollection2.get_Point(l + 1);
                                                num2 += Math.Sqrt(Math.Pow(point5.X - point4.X, 2.0) + Math.Pow(point5.Y - point4.Y, 2.0) + Math.Pow(point5.Z - point5.M - point4.Z + point4.M, 2.0));
                                            }
                                            array2[0] = text2;
                                            array2[1] = pointCollection2.PointCount;
                                            array2[2] = point4.X;
                                            array2[3] = point4.Y;
                                            array2[4] = point4.Z;
                                            array2[5] = point4.M;
                                            this.Geotable.Rows.Add(array2);
                                        }
                                    }
                                    else
                                    {
                                        IField field2 = feature.Fields.get_Field(j);
                                        object obj = feature.get_Value(j);
                                        if (j < this.OidField)
                                        {
                                            if (field2.Type == (esriFieldType)3 || field2.Type == (esriFieldType)2)
                                            {
                                                if (obj != DBNull.Value)
                                                {
                                                    array[j + 1] = Math.Round(Convert.ToDouble(obj), 3);
                                                }
                                                else
                                                {
                                                    array[j + 1] = obj;
                                                }
                                                feature.get_Value(j).ToString();
                                            }
                                            else if (field2.Type == (esriFieldType)5)
                                            {
                                                if (obj != DBNull.Value)
                                                {
                                                    DateTime dateTime = Convert.ToDateTime(obj);
                                                    array[j + 1] = dateTime.ToShortDateString();
                                                }
                                                else
                                                {
                                                    array[j + 1] = "";
                                                }
                                            }
                                            else
                                            {
                                                array[j + 1] = obj;
                                            }
                                        }
                                        else if (field2.Type == (esriFieldType)3 || field2.Type == (esriFieldType)2)
                                        {
                                            if (obj != DBNull.Value)
                                            {
                                                array[j] = Math.Round(Convert.ToDouble(obj), 3);
                                            }
                                            else
                                            {
                                                array[j] = obj;
                                            }
                                        }
                                        else if (field2.Type == (esriFieldType)5)
                                        {
                                            if (obj != DBNull.Value)
                                            {
                                                array[j] = Convert.ToDateTime(obj).ToShortDateString();
                                            }
                                            else
                                            {
                                                array[j] = "";
                                            }
                                        }
                                        else
                                        {
                                            array[j] = obj;
                                        }
                                    }
                                }
                                if (flag)
                                {
                                    array[j] = num2.ToString();
                                }
                                this.stable.Rows.Add(array);
                                this._selection.Add(feature);
                                feature = this._cursor.NextFeature();
                            }
                        }
                        int count = this._selection.SelectionSet.Count;
                        //this.CountBox.Text = "查询的记录数为:" + count.ToString();
                        this._selection.SelectionSet.Refresh();
                        IActiveView activeView = _context.ActiveView;
                        activeView.Refresh();
                        result = true;
                    }
                }
            }
            return result;
        }

        private void mainGridView_MasterRowGetLevelDefaultView(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = this.detailGridView;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (this.stable.Rows.Count < 1)
            {
                MessageBox.Show("空数据,不转出EXCEL文件！");
            }
            else
            {
                this.saveFileDialog1.DefaultExt = "xls";
                this.saveFileDialog1.FileName = "Result.xls";
                this.saveFileDialog1.Filter = "Excel文件|*.xls";
                this.saveFileDialog1.OverwritePrompt = false;
                this.saveFileDialog1.Title = "保存";
                DialogResult dialogResult = this.saveFileDialog1.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    string fileName = this.saveFileDialog1.FileName;
                    if (!File.Exists(fileName))
                    {
                        DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                        gridControl1.ExportToXls(fileName);
                        DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("该文件已存在,返回!");
                    }
                }
            }
        }

        private void btnStatics_Click(object sender, EventArgs e)
        {
            if (this.stable.Rows.Count < 1)
            {
                MessageBox.Show("空数据,不进行统计！");
            }
            else if (this.cmbStatField.SelectedIndex < 0)
            {
                MessageBox.Show("请用户先指定分类项！");
            }
            else
            {
                new StatForm
                {
                    Owner = _context.MainView as Form,
                    Form_StatField = this.cmbStatField.SelectedItem.ToString(),
                    Form_StatWay = this.cmbStatWay.SelectedItem.ToString(),
                    Form_CalField = this.cmbCalField.SelectedItem.ToString(),
                    resultTable = this.stable
                }.ShowDialog();
            }
        }

        private void cmbStatWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bControlEvent)
            {
                if (this.cmbStatWay.SelectedItem.ToString() == "计数" || this.cmbStatWay.SelectedItem.ToString() == "统计长度" || this.cmbStatWay.SelectedItem.ToString() == "统计面积")
                {
                    this.cmbCalField.Enabled = false;
                }
                else
                {
                    this.cmbCalField.Enabled = true;
                }
            }
        }

        private void cmbStatField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bControlEvent && this.stable.Rows.Count > 0)
            {
                this.mainGridView.ClearSorting();
                this.mainGridView.Columns[this.cmbStatField.SelectedItem.ToString()].SortMode= ColumnSortMode.Default;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.stable.Rows.Count < 1)
            {
                MessageBox.Show("空数据,不进行打印！");
            }
            else
            {
                try
                {
                    this.gridControl1.PrintDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while printing.\n" + ex.Message, "Error printing", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }
}
