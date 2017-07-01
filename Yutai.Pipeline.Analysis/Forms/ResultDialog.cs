using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class ResultDialog : XtraForm
    {
        private IContainer icontainer_0 = null;

        public string m_strBuildDate = "建设时间";

        public IAppContext m_iApp;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;

        public string m_strLayerName = "";

        public int m_nExpireTime;

        public int m_nTimerCounter;

        public int m_nCurRowIndex;

        public IGeometry m_pBufferGeo;

        public ArrayList m_alLayers;

        public int m_nRow;


        public IFeatureLayer m_pCurLayer;

        public IAppContext App
        {
            set
            {
                this.m_iApp = value;
                this.MapControl = (IMapControl3) this.m_iApp.MapControl;
            }
        }

        public ResultDialog(IAppContext context, IPipelineConfig config)
        {
            this.InitializeComponent();
            m_iApp = context;
            pPipeCfg = config;
        }

        private void dataGridView3_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            if (dataGridViewCellEventArg.RowIndex >= 0 && dataGridViewCellEventArg.ColumnIndex >= 0)
            {
                string str = this.dataGridView3[0, dataGridViewCellEventArg.RowIndex].Value.ToString();
                IFeatureLayer featureLayer = null;
                for (int i = 0; i < this.m_alLayers.Count; i++)
                {
                    IFeatureLayer item = this.m_alLayers[i] as IFeatureLayer;
                    if (str == item.Name)
                    {
                        featureLayer = item;
                    }
                }
                if (featureLayer != null)
                {
                    try
                    {
                        IFeatureClass featureClass = featureLayer.FeatureClass;
                        int num = featureClass.Fields.FindField(featureClass.OIDFieldName);
                        if (num != -1)
                        {
                            string str1 =
                                this.dataGridView3[num + 1, dataGridViewCellEventArg.RowIndex].Value.ToString();
                            IFeature feature = featureClass.GetFeature(Convert.ToInt32(str1));
                            if (feature != null)
                            {
                                this.igeometry_0 = (IGeometry) ((IClone) feature.Shape).Clone();
                                this.ScaleToGeo(this.m_iApp.MapControl as IMapControl3, this.igeometry_0);
                                this.m_nCurRowIndex = dataGridViewCellEventArg.RowIndex;
                                this.timer_0.Start();
                                this.m_nTimerCounter = 0;
                                this.m_iApp.ActiveView.Refresh();
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }

        public void FlashDstItem()
        {
            IMapControl3 mapControl = this.m_iApp.MapControl as IMapControl3;
            Color randColor = (new CRandomColor()).GetRandColor();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red = ((int) randColor.R);
            rgbColorClass.Green = ((int) randColor.G);
            rgbColorClass.Blue = ((int) randColor.B);
            simpleLineSymbolClass.Color = (rgbColorClass);
            simpleLineSymbolClass.Width = (5);
            object obj = simpleLineSymbolClass;
            ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
            simpleMarkerSymbolClass.Color = (rgbColorClass);
            simpleMarkerSymbolClass.Size = (10);
            simpleMarkerSymbolClass.Style = (0);
            object obj1 = simpleMarkerSymbolClass;
            ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
            simpleFillSymbolClass.Style = 0;
            simpleFillSymbolClass.Outline.Width = (6);
            simpleFillSymbolClass.Color = (rgbColorClass);
            object obj2 = simpleFillSymbolClass;
            try
            {
                if (this.igeometry_0.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    mapControl.DrawShape(this.igeometry_0, ref obj1);
                }
                if (this.igeometry_0.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    mapControl.DrawShape(this.igeometry_0, ref obj);
                }
                if (this.igeometry_0.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    mapControl.DrawShape(this.igeometry_0, ref obj2);
                }
            }
            catch
            {
            }
        }

        private void method_0(IFeatureLayer featureLayer)
        {
            ILayerFields layerField = (ILayerFields) featureLayer;
            int fieldCount = featureLayer.FeatureClass.Fields.FieldCount;
            Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
            for (int i = 0; i < fieldCount; i++)
            {
                string name = layerField.get_Field(i).Name;
                if (!this.dataGridView3.Columns.Contains(name))
                {
                    this.dataGridView3.Columns.Add(name, name);
                    if (name == "起点管顶高程")
                    {
                        this.dataGridView3.Columns.Add("起点管底高程", "起点管底高程");
                    }
                    if (name == "终点管顶高程")
                    {
                        this.dataGridView3.Columns.Add("终点管底高程", "终点管底高程");
                    }
                }
            }
        }

        private void method_1(IFeatureLayer featureLayer)
        {
            IFeatureClass featureClass = featureLayer.FeatureClass;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            spatialFilterClass.Geometry = (this.m_pBufferGeo);
            spatialFilterClass.SpatialRel = (esriSpatialRelEnum) (1);
            IFeatureCursor featureCursor = featureClass.Search(spatialFilterClass, false);
            IFeature feature = featureCursor.NextFeature();
            int count = this.dataGridView3.Columns.Count;
            while (feature != null)
            {
                int length = "esriGeometry".Length;
                featureClass.Fields.FindField(featureLayer.FeatureClass.ShapeFieldName);
                string str = featureLayer.FeatureClass.ShapeType.ToString();
                string str1 = str.Remove(0, length);
                bool flag = false;
                this.dataGridView3.Rows.Add(new object[] {""});
                this.dataGridView3[0, this.m_nRow].Value = featureLayer.Name;
                for (int i = 1; i < count; i++)
                {
                    if (this.dataGridView3.Columns[i].Name != featureLayer.FeatureClass.ShapeFieldName)
                    {
                        int num = featureClass.Fields.FindField(this.dataGridView3.Columns[i].Name);
                        if (num != -1)
                        {
                            string name = feature.Fields.get_Field(num).Name;
                            if (!(name != "起点管顶高程" ? true : !flag))
                            {
                                this.dataGridView3.Rows[this.m_nRow].Cells[name].Value = "";
                                this.dataGridView3.Rows[this.m_nRow].Cells["起点管底高程"].Value =
                                    feature.get_Value(num).ToString();
                            }
                            else if ((name != "终点管顶高程" ? true : !flag))
                            {
                                this.dataGridView3.Rows[this.m_nRow].Cells[name].Value =
                                    feature.get_Value(num).ToString();
                            }
                            else
                            {
                                this.dataGridView3.Rows[this.m_nRow].Cells[name].Value = "";
                                this.dataGridView3.Rows[this.m_nRow].Cells["终点管底高程"].Value =
                                    feature.get_Value(num).ToString();
                            }
                        }
                    }
                    else
                    {
                        this.dataGridView3[i, this.m_nRow].Value = str1;
                    }
                }
                ResultDialog mNRow = this;
                mNRow.m_nRow = mNRow.m_nRow + 1;
                feature = featureCursor.NextFeature();
            }
        }

        private void OutBut_Click(object obj, EventArgs eventArg)
        {
            if (this.saveFileDialog_0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = this.saveFileDialog_0.FileName;
                if (!File.Exists(fileName))
                {
                    OdbcCommand odbcCommand = new OdbcCommand();
                    OdbcConnection odbcConnection = new OdbcConnection()
                    {
                        ConnectionString =
                            string.Concat(
                                "DRIVER=MICROSOFT EXCEL DRIVER (*.xls);FIRSTROWHASNAMES=1;READONLY=FALSE;CREATE_DB=\"",
                                fileName, "\\;DBQ=", fileName)
                    };
                    odbcConnection.Open();
                    odbcCommand.Connection = odbcConnection;
                    odbcCommand.CommandType = CommandType.Text;
                    string str = "(";
                    for (int i = 0; i < this.dataGridView3.ColumnCount - 1; i++)
                    {
                        str = string.Concat(str, this.dataGridView3.Columns[i].Name);
                        str = string.Concat(str, " varchar(40)");
                        if (i < this.dataGridView3.ColumnCount - 2)
                        {
                            str = string.Concat(str, ",");
                        }
                    }
                    str = string.Concat(str, ")");
                    odbcCommand.CommandText = string.Concat("CREATE TABLE HitAnalyseResult ", str);
                    odbcCommand.ExecuteNonQuery();
                    int count = this.dataGridView3.Rows.Count;
                    int columnCount = this.dataGridView3.ColumnCount;
                    str = "(";
                    for (int j = 0; j < this.dataGridView3.ColumnCount - 1; j++)
                    {
                        str = string.Concat(str, this.dataGridView3.Columns[j].Name);
                        if (j < this.dataGridView3.ColumnCount - 2)
                        {
                            str = string.Concat(str, ",");
                        }
                    }
                    str = string.Concat(str, ")");
                    for (int k = 0; k < count; k++)
                    {
                        OdbcCommand odbcCommand1 = new OdbcCommand()
                        {
                            Connection = odbcConnection,
                            CommandType = CommandType.Text
                        };
                        string str1 = "INSERT INTO HitAnalyseResult ";
                        string str2 = " VALUES(";
                        for (int l = 0; l < columnCount - 1; l++)
                        {
                            object value = this.dataGridView3[l, k].Value;
                            if (value == null)
                            {
                                value = "";
                            }
                            str2 = (l == columnCount - 2
                                ? string.Concat(str2, "'", value.ToString(), "')")
                                : string.Concat(str2, "'", value.ToString(), "', "));
                        }
                        str1 = string.Concat(str1, str2);
                        odbcCommand1.CommandText = str1;
                        odbcCommand1.ExecuteNonQuery();
                    }
                    odbcConnection.Close();
                }
                else
                {
                    MessageBox.Show("该文件已经存在,请换名保存!");
                }
            }
        }

        private void ResultDialog_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
        {
            base.Visible = false;
            formClosingEventArg.Cancel = true;
        }

        private void ResultDialog_Load(object obj, EventArgs eventArg)
        {
            this.ThrougAllLayer();
        }

        public void ScaleToGeo(IMapControl3 pMapCtrl, IGeometry pGeo)
        {
            IEnvelope envelope = pGeo.Envelope;
            if ((pMapCtrl.Extent.LowerLeft.X > envelope.LowerLeft.X ||
                 pMapCtrl.Extent.LowerLeft.Y > envelope.LowerLeft.Y ||
                 pMapCtrl.Extent.UpperRight.X < envelope.UpperRight.X
                ? true
                : pMapCtrl.Extent.UpperRight.Y < envelope.UpperRight.Y))
            {
                if (pGeo.GeometryType != esriGeometryType.esriGeometryPoint)
                {
                    IEnvelope envelope1 = pGeo.Envelope;
                    envelope1.Expand(3, 3, true);
                    pMapCtrl.Extent = (envelope1);
                }
                else
                {
                    IEnvelope envelope2 = pGeo.Envelope;
                    IEnvelope extent = pMapCtrl.Extent;
                    double width = extent.Width;
                    double height = extent.Height;
                    envelope2.Expand(width/2, height/2, false);
                    pMapCtrl.Extent = (envelope2);
                }
            }
        }

        public void ThrougAllLayer()
        {
            int count = this.m_alLayers.Count;
            this.m_nRow = 0;
            if (count > 0)
            {
                IFeatureLayer item = this.m_alLayers[0] as IFeatureLayer;
                this.dataGridView3.Rows.Clear();
                this.dataGridView3.Columns.Clear();
                DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
                this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
                this.dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName("Control");
                this.dataGridView3.Columns.Clear();
                this.dataGridView3.ColumnCount = 1;
                this.dataGridView3.Columns[0].Name = "管线名称";
                this.dataGridView3.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (int i = 0; i < count; i++)
                {
                    this.method_0(item);
                }
                try
                {
                    for (int j = 0; j < count; j++)
                    {
                        this.method_1(this.m_alLayers[j] as IFeatureLayer);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                int rowCount = this.dataGridView3.RowCount;
                this.Text = string.Concat("缓冲分析明细 - 记录数:", rowCount.ToString());
            }
        }

        private void timer_0_Tick(object obj, EventArgs eventArg)
        {
            ResultDialog mNTimerCounter = this;
            mNTimerCounter.m_nTimerCounter = mNTimerCounter.m_nTimerCounter + 1;
            if (this.m_nTimerCounter <= 20)
            {
                this.FlashDstItem();
            }
            else
            {
                this.timer_0.Stop();
                this.m_nTimerCounter = 0;
            }
        }
    }
}