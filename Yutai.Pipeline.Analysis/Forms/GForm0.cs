
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesFile;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class GForm0 : XtraForm
    {

        public IAppContext m_app;

        private List<IFeatureLayer> list_0 = new List<IFeatureLayer>();

        private List<IFeatureLayer> list_1 = new List<IFeatureLayer>();

        private CHitAnalyse chitAnalyse_0 = new CHitAnalyse();

        public CommonDistAnalyse m_commonDistAls;


        public int m_nTimerCount;

        public IGeometry m_pFlashGeo;

        private List<IFeatureLayer> list_2 = new List<IFeatureLayer>();

        private string string_0 = "管线性质";

        private string string_1 = "管径";

        private string string_2 = "沟截面宽高";



        private List<CHitAnalyse.CItem> list_3 = new List<CHitAnalyse.CItem>();

        private List<CHitAnalyse.CItem> list_4 = new List<CHitAnalyse.CItem>();

        private List<CHitAnalyse.CItem> list_5 = new List<CHitAnalyse.CItem>();


        private Dictionary<string, string> dictionary_0 = new Dictionary<string, string>();



        private IContainer icontainer_0 = null;


        public GForm0(IAppContext pApp)
        {
            this.InitializeComponent();
            this.m_commonDistAls = new CommonDistAnalyse()
            {
                m_nAnalyseType = DistAnalyseType.emVerDist
            };
            this.m_app = pApp;
            this.m_commonDistAls.PipeConfig = pApp.PipeConfig;
            this.m_nTimerCount = 0;
            this.dataGridViewSelectItem.Columns[0].ReadOnly = true;
            this.dataGridViewSelectItem.Columns[1].ReadOnly = true;
            this.dataGridViewSelectItem.Columns[2].ReadOnly = true;
            this.method_16();
        }

        public void AddFeatureLayer(IFeatureLayer iFLayer, List<IFeatureLayer> pListLayers)
        {
            try
            {
                if (iFLayer != null)
                {
                    IFeatureClass featureClass = iFLayer.FeatureClass;
                    if ((featureClass.ShapeType == (esriGeometryType)13 ? true : featureClass.ShapeType == (esriGeometryType)3))
                    {
                        if ((featureClass.AliasName == "SY_ZX_L" ? true : featureClass.AliasName == "SDE.SY_ZX_L"))
                        {
                            this.ifeatureClass_1 = featureClass;
                        }
                        INetworkClass networkClass = featureClass as INetworkClass;
                        if ((networkClass == null ? false : networkClass.GeometricNetwork != null))
                        {
                            pListLayers.Add(iFLayer);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public void AddGroupLayer(IGroupLayer iGLayer, List<IFeatureLayer> pListLayers)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    this.AddLayer(compositeLayer.get_Layer(i), pListLayers);
                }
            }
        }

        public void AddLayer(ILayer ipLay, List<IFeatureLayer> pListLayers)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer)ipLay, pListLayers);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer)ipLay, pListLayers);
            }
        }

        public void AddName(ILayer pLayer)
        {
            try
            {
                if (pLayer != null)
                {
                    IFeatureLayer featureLayer = pLayer as IFeatureLayer;
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    if (featureLayer.FeatureClass.FeatureType != (esriFeatureType)11 && this.m_app.PipeConfig.IsPipeLine(featureClass.AliasName))
                    {
                        this.dictionary_0.Add(featureLayer.Name, featureClass.AliasName);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void btAnalyse_Click(object obj, EventArgs eventArg)
        {
            try
            {
                IPoint fromPoint = this.ipolyline_0.FromPoint;
                IPoint toPoint = this.ipolyline_0.ToPoint;
                fromPoint.Z = ((double)this.int_1);
                toPoint.Z = ((double)this.int_2);
                this.ipolyline_0.FromPoint = (fromPoint);
                this.ipolyline_0.ToPoint = (toPoint);
                this.method_0();
                MessageBox.Show("分析完成！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btClose_Click(object obj, EventArgs eventArg)
        {
            base.Close();
        }

        private void btnClear_Click(object obj, EventArgs eventArg)
        {
            this.method_10();
            this.InitDistAnalyseDlg();
        }

        private void dataGridViewCenterLine_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            int rowIndex = dataGridViewCellEventArg.RowIndex;
            if (rowIndex >= 0)
            {
                try
                {
                    int num = Convert.ToInt32(this.dataGridViewCenterLine[2, rowIndex].Value);
                    DataGridViewRow item = this.dataGridViewCenterLine.Rows[dataGridViewCellEventArg.RowIndex];
                    IFeatureClass tag = (IFeatureClass)item.Tag;
                    if (tag != null)
                    {
                        IFeature feature = tag.GetFeature(num);
                        if (feature != null)
                        {
                            this.m_pFlashGeo = feature.Shape;
                        }
                    }
                    this.timer_0.Start();
                    this.timer_0.Interval = 300;
                    this.m_app.ActiveView.Refresh();
                    this.m_nTimerCount = 0;
                    this.FlashDstItem();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void dataGridViewHor_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            int rowIndex = dataGridViewCellEventArg.RowIndex;
            if (rowIndex >= 0)
            {
                try
                {
                    int num = Convert.ToInt32(this.dataGridViewHor[2, rowIndex].Value);
                    DataGridViewRow item = this.dataGridViewHor.Rows[dataGridViewCellEventArg.RowIndex];
                    IFeatureClass tag = (IFeatureClass)item.Tag;
                    if (tag != null)
                    {
                        IFeature feature = tag.GetFeature(num);
                        if (feature != null)
                        {
                            this.m_pFlashGeo = feature.Shape;
                        }
                    }
                    this.timer_0.Start();
                    this.timer_0.Interval = 300;
                    this.m_app.ActiveView.Refresh();
                    this.m_nTimerCount = 0;
                    this.FlashDstItem();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void dataGridViewSelectItem_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            int rowIndex = dataGridViewCellEventArg.RowIndex;
            if (rowIndex >= 0)
            {
                try
                {
                    int num = Convert.ToInt32(this.dataGridViewSelectItem[2, rowIndex].Value);
                    DataGridViewRow item = this.dataGridViewSelectItem.Rows[dataGridViewCellEventArg.RowIndex];
                    IFeatureLayer tag = (IFeatureLayer)item.Tag;
                    IFeatureClass featureClass = tag.FeatureClass;
                    if (featureClass != null)
                    {
                        IFeature feature = featureClass.GetFeature(num);
                        if (feature != null)
                        {
                            this.m_pFlashGeo = feature.Shape;
                            IMap map = this.m_app.FocusMap;
                            map.ClearSelection();
                            map.SelectFeature(tag, feature);
                            this.GetBaseLine();
                            ((IActiveView)map).PartialRefresh((esriViewDrawPhase)4, null, null);
                            CommonUtils.ScaleToTwoGeo(this.m_app.FocusMap, this.ipolyline_0, this.m_pFlashGeo);
                        }
                    }
                    this.m_app.ActiveView.Refresh();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void dataGridViewSelectItem_CellEndEdit(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            DataGridViewRow item = this.dataGridViewSelectItem.Rows[dataGridViewCellEventArg.RowIndex];
            this.dataGridViewComboBoxCell_0 = (DataGridViewComboBoxCell)item.Cells["comboLineType"];
            this.int_1 = Convert.ToInt16(item.Cells["editHeightStartPoint"].Value);
            this.int_2 = Convert.ToInt16(item.Cells["editHeightEndPoint"].Value);
        }

        private void dataGridViewVer_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            int rowIndex = dataGridViewCellEventArg.RowIndex;
            if (rowIndex >= 0)
            {
                try
                {
                    int num = Convert.ToInt32(this.dataGridViewVer[2, rowIndex].Value);
                    DataGridViewRow item = this.dataGridViewVer.Rows[dataGridViewCellEventArg.RowIndex];
                    IFeatureClass tag = (IFeatureClass)item.Tag;
                    if (tag != null)
                    {
                        IFeature feature = tag.GetFeature(num);
                        if (feature != null)
                        {
                            this.m_pFlashGeo = feature.Shape;
                        }
                    }
                    this.timer_0.Start();
                    this.timer_0.Interval = 300;
                    this.m_app.ActiveView.Refresh();
                    this.m_nTimerCount = 0;
                    this.FlashDstItem();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void FlashDstItem()
        {
            IMapControl3 mapControl = this.m_app.MapControl as IMapControl3;
            Color randColor = (new CRandomColor()).GetRandColor();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red = ((int)randColor.R);
            rgbColorClass.Green = ((int)randColor.G);
            rgbColorClass.Blue = ((int)randColor.B);
            simpleLineSymbolClass.Color = (rgbColorClass);
            simpleLineSymbolClass.Width = (5);
            object obj = simpleLineSymbolClass;
            ISimpleLineSymbol simpleLineSymbolClass1 = new SimpleLineSymbol();
            IRgbColor rgbColorClass1 = new RgbColor();
            rgbColorClass1.Red = (255);
            rgbColorClass1.Green = (0);
            rgbColorClass1.Blue = (0);
            simpleLineSymbolClass1.Color = (rgbColorClass1);
            simpleLineSymbolClass1.Width = (5);
            object obj1 = simpleLineSymbolClass1;

            try
            {
                mapControl.DrawShape(this.m_pFlashGeo, ref obj);
                if (this.m_commonDistAls.m_pBaseLine != null)
                {
                    mapControl.DrawShape(this.ipolyline_0, ref obj1);
                }
            }
            catch
            {
            }
        }

        public void GetBaseLine()
        {
            string str;
            string str1;
            string str2;
            this.timer_0.Stop();
            IMap map = this.m_app.FocusMap;
            IFeature feature = ((IEnumFeature)map.FeatureSelection).Next();
            if (feature != null)
            {
                CommonUtils.GetSmpClassName(feature.Class.AliasName);
                if ((this.m_app.PipeConfig.IsPipeLine(feature.Class.AliasName) ? true : !(feature.Class.AliasName != "Polyline")))
                {
                    IGeometry shape = feature.Shape;
                    if (shape.GeometryType == (esriGeometryType)3)
                    {
                        this.ipolyline_0 = CommonUtils.GetPolylineDeepCopy((IPolyline)shape);
                        this.m_commonDistAls.m_pFeature = feature;
                        this.m_commonDistAls.m_pBaseLine = this.ipolyline_0;
                        this.m_commonDistAls.m_strLayerName = feature.Class.AliasName;
                        int num = feature.Fields.FindField("埋设方式");
                        str = (num == -1 ? "" : this.method_11(feature.get_Value(num)));
                        this.m_commonDistAls.m_strBuryKind = str;
                        int num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Diameter());
                        str1 = (num1 == -1 ? "" : this.method_11(feature.get_Value(num1)));
                        num1 = feature.Fields.FindField(this.m_app.PipeConfig.get_Section_Size());
                        str2 = (num1 == -1 ? "" : this.method_11(feature.get_Value(num1)));
                        string str3 = "";
                        if (str1 != "")
                        {
                            str3 = str1;
                        }
                        if (str2 != "")
                        {
                            str3 = str2;
                        }
                        this.m_commonDistAls.m_dDiameter = this.m_commonDistAls.GetDiameterFromString(str3.Trim());
                        this.m_commonDistAls.m_nBaseLineFromID = -1;
                        this.m_commonDistAls.m_nBaseLineToID = -1;
                        this.btAnalyse.Enabled = this.m_commonDistAls.m_pBaseLine != null;
                        this.ifeature_0 = feature;
                        this.ifeatureClass_0 = feature.Class as IFeatureClass;
                    }
                    else
                    {
                        MessageBox.Show("所选择的管线多于一条，或者不是管线！");
                    }
                }
                else
                {
                    this.m_commonDistAls.m_pBaseLine = null;
                    this.btAnalyse.Enabled = false;
                    this.m_app.FocusMap.ClearSelection();
                    this.m_app.ActiveView.Refresh();
                }
            }
            else
            {
                this.m_commonDistAls.m_pBaseLine = null;
                this.btAnalyse.Enabled = false;
                this.m_app.FocusMap.ClearSelection();
                this.m_app.ActiveView.Refresh();
            }
        }

        private void GForm0_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
        {
        }

        private void GForm0_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
        {
            base.Visible = false;
            formClosingEventArg.Cancel = true;
            this.method_10();
            this.InitDistAnalyseDlg();
        }

        private void GForm0_HelpRequested(object obj, HelpEventArgs helpEventArg)
        {
            string str = string.Concat(Application.StartupPath, "\\帮助.chm");
            Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "审批辅助分析");
        }

        private void GForm0_Load(object obj, EventArgs eventArg)
        {
            this.LoadMapLayer();
        }

        public void InitDistAnalyseDlg()
        {
            this.m_app.FocusMap.ClearSelection();
            this.m_app.ActiveView.Refresh();
            this.dataGridViewVer.Rows.Clear();
            this.dataGridViewHor.Rows.Clear();
            this.dataGridViewSelectItem.Rows.Clear();
            this.dataGridViewCenterLine.Rows.Clear();
            this.btAnalyse.Enabled = false;
            this.list_0.Clear();
            this.list_1.Clear();
            this.textBoxLoadCADName.Text = "";
        }

        public void LoadMapLayer()
        {
            this.list_2.Clear();
            this.ifeatureClass_1 = null;
            if ((this.m_app == null || this.m_app.FocusMap == null ? false : this.m_app.FocusMap != null))
            {
                for (int i = 0; i < this.m_app.FocusMap.LayerCount; i++)
                {
                    ILayer layer = this.m_app.FocusMap.get_Layer(i);
                    this.AddLayer(layer, this.list_2);
                }
            }
            this.string_0 = this.m_app.PipeConfig.get_Kind();
            this.string_1 = this.m_app.PipeConfig.get_Diameter();
            this.string_2 = this.m_app.PipeConfig.get_Section_Size();
            this.method_12();
        }

        private void method_0()
        {
            CommonUtils.AppContext = this.m_app;
            this.ipolyline_1 = this.method_13(this.ipolyline_0);
            ITopologicalOperator ipolyline0 = (ITopologicalOperator)this.ipolyline_0;
            double num = Convert.ToDouble(this.tbBufferRadius.Text);
            if (num > 0)
            {
                IGeometry geometry = ipolyline0.Buffer(num);
                ISpatialFilter spatialFilterClass = new SpatialFilter();
                spatialFilterClass.Geometry = (geometry);
                spatialFilterClass.SpatialRel = (esriSpatialRelEnum)(1);
                if (this.list_2.Count >= 1)
                {
                    this.list_4.Clear();
                    this.list_3.Clear();
                    this.list_5.Clear();
                    foreach (IFeatureLayer list2 in this.list_2)
                    {
                        if (!list2.Visible)
                        {
                            continue;
                        }
                        IFeatureClass featureClass = list2.FeatureClass;
                        if ((featureClass.AliasName == this.ifeatureClass_0.AliasName ? true : featureClass.ShapeType != (esriGeometryType)3))
                        {
                            continue;
                        }
                        spatialFilterClass.GeometryField = (featureClass.ShapeFieldName);
                        this.method_1(featureClass.Search(spatialFilterClass, false));
                        this.method_2();
                        this.method_3(featureClass.Search(spatialFilterClass, false));
                        this.method_4();
                    }
                    if (this.ifeatureClass_1 != null)
                    {
                        spatialFilterClass.GeometryField = (this.ifeatureClass_1.ShapeFieldName);
                        this.method_8(this.ifeatureClass_1.Search(spatialFilterClass, false));
                        this.method_9();
                    }
                }
                else
                {
                    MessageBox.Show("当前地图不可用!");
                }
            }
            else
            {
                MessageBox.Show("分析半径不能为空!");
            }
        }

        private void method_1(IFeatureCursor featureCursor)
        {
            double double1 = this.double_1 * 0.0005;
            IProximityOperator ipolyline1 = (IProximityOperator)this.ipolyline_1;
            IFeature feature = featureCursor.NextFeature();
            int num = -1;
            int num1 = -1;
            int num2 = -1;
            if (feature != null)
            {
                num = feature.Fields.FindField(this.string_1);
                num1 = feature.Fields.FindField(this.string_2);
                num2 = feature.Fields.FindField(this.string_0);
            }
            if (num2 >= 0)
            {
                if ((num >= 0 ? true : num1 >= 0))
                {
                    while (feature != null)
                    {
                        IPolyline polyline = this.method_13(feature.Shape as IPolyline);
                        double num3 = 0;
                        double num4 = this.method_14(feature, num, num1, out num3);
                        if (num4 < 10)
                        {
                            num4 = 10;
                        }
                        num4 = num4 * 0.0005;
                        double num5 = ipolyline1.ReturnDistance(polyline);
                        if (num5 >= 0.001)
                        {
                            num5 = num5 - double1 - num4;
                            if (num5 < 0.001)
                            {
                                num5 = 0;
                            }
                        }
                        else
                        {
                            num5 = 0;
                        }
                        string str = feature.get_Value(num2).ToString();
                        CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
                        {
                            _OID = feature.OID,
                            _sKind = str,
                            _dHorDistance = num5,
                            _dHorBase = 0,
                            _pClass = (IFeatureClass)feature.Class
                        };
                        string value = this.dataGridViewComboBoxCell_0.Value as string;
                        if (this.dictionary_0.ContainsKey(value))
                        {
                            value = this.dictionary_0[value].ToString();
                        }
                        cItem._dHorBase = (double)CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(CommonUtils.GetSmpClassName(value), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.ifeature_0, feature);
                        this.list_3.Add(cItem);
                        feature = featureCursor.NextFeature();
                    }
                }
            }
        }

        private void method_10()
        {
            if ((this.list_1 == null ? false : this.list_1.Count > 0))
            {
                for (int i = 0; i < this.list_1.Count; i++)
                {
                    this.m_app.FocusMap.DeleteLayer(this.list_1[i]);
                }
            }
        }

        private string method_11(object obj)
        {
            return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
        }

        private void method_12()
        {
            this.ipolyline_0 = null;
            if (this.ifeature_0 != null)
            {
                int num = this.ifeature_0.Fields.FindField(this.string_1);
                int num1 = this.ifeature_0.Fields.FindField(this.string_2);
                if (this.ifeature_0.Fields.FindField(this.string_0) >= 0)
                {
                    if ((num >= 0 ? true : num1 >= 0))
                    {
                        this.double_1 = this.method_14(this.ifeature_0, num, num1, out this.double_0);
                        if (this.double_1 < 1)
                        {
                            this.double_1 = 10;
                        }
                        if (this.double_0 < 1)
                        {
                            this.double_0 = this.double_1;
                        }
                        this.int_0 = this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName));
                    }
                }
            }
        }

        private IPolyline method_13(IPolyline polyline)
        {
            object missing = Type.Missing;
            IPolyline polylineClass = new Polyline() as IPolyline;
            IPointCollection pointCollection = (IPointCollection)polylineClass;
            IPointCollection pointCollection1 = (IPointCollection)polyline;
            for (int i = 0; i <= pointCollection1.PointCount - 1; i++)
            {
                IPoint point = pointCollection1.get_Point(i);
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.X = (point.X);
                pointClass.Y = (point.Y);
                pointClass.Z = (0);
                pointCollection.AddPoint(pointClass, ref missing, ref missing);
            }
            return polylineClass;
        }

        private double method_14(IFeature feature, int num, int num2, out double double_2)
        {
            double_2 = 0.0;
            double num3 = 0.0;
            object obj = null;
            if (num > 0)
            {
                obj = feature.get_Value(num);
                if (obj == null || Convert.IsDBNull(obj) || !Regex.IsMatch(obj.ToString(), "^\\d+$"))
                {
                    num3 = 0.0;
                }
                else
                {
                    num3 = Convert.ToDouble(obj);
                }
            }
            if (num3 < 1.0)
            {
                if (Convert.IsDBNull(obj))
                {
                    num3 = this.method_15(feature, num2, out double_2);
                }
                else
                {
                    num3 = this.method_15(feature, num, out double_2);
                }
            }
            else
            {
                double_2 = num3;
            }
            return num3;
        }

        private double method_15(IFeature feature, int num, out double double_2)
        {
            double num1 = 0;
            double_2 = 0;
            string str = "";
            if (num > 0)
            {
                object value = feature.get_Value(num);
                if (!Convert.IsDBNull(value))
                {
                    str = Convert.ToString(value);
                }
                if ((str == null ? false : str.Length >= 1))
                {
                    string[] strArrays = str.Split(new char[] { 'x', 'X', 'Х', '×' });
                    num1 = Convert.ToDouble(strArrays[0]);
                    double_2 = Convert.ToDouble(strArrays[1]);
                }
            }
            return num1;
        }

        private void method_16()
        {
            this.dictionary_0.Clear();
            CommonUtils.ThrougAllLayer(this.m_app.FocusMap, new CommonUtils.DealLayer(this.AddName));
        }

        private void method_2()
        {
            List<CHitAnalyse.CItem> list3 = this.list_3;
            this.dataGridViewHor.Rows.Clear();
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.Red
            };
            if (list3.Count > 0)
            {
                this.dataGridViewHor.Rows.Add(list3.Count);
                for (int i = 0; i < list3.Count; i++)
                {
                    CHitAnalyse.CItem item = list3[i];
                    DataGridViewRow str = this.dataGridViewHor.Rows[i];
                    str.Tag = item._pClass;
                    int num = i + 1;
                    str.Cells[0].Value = num.ToString();
                    str.Cells[1].Value = item._sKind;
                    str.Cells[2].Value = item._OID.ToString();
                    str.Cells[3].Value = item._dHorDistance.ToString("0.000");
                    if (item._dHorBase > 0.001)
                    {
                        str.Cells[4].Value = item._dHorBase.ToString("0.000");
                    }
                    if (item._dHorDistance < item._dHorBase)
                    {
                        str.Cells[3].Style = dataGridViewCellStyle;
                    }
                }
            }
        }

        private void method_3(IFeatureCursor featureCursor)
        {
            double double0 = this.double_0 * 0.0005;
            ITopologicalOperator ipolyline1 = (ITopologicalOperator)this.ipolyline_1;
            IFeature feature = featureCursor.NextFeature();
            int num = -1;
            int num1 = -1;
            int num2 = -1;
            if (feature != null)
            {
                num = feature.Fields.FindField(this.string_1);
                num1 = feature.Fields.FindField(this.string_2);
                num2 = feature.Fields.FindField(this.string_0);
            }
            if (num2 >= 0)
            {
                if ((num >= 0 ? true : num1 >= 0))
                {
                    int lineConfigHeightFlag = this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(feature.Class.AliasName));
                    while (feature != null)
                    {
                        IPolyline shape = feature.Shape as IPolyline;
                        IPolyline polyline = this.method_13(shape);
                        double num3 = 0;
                        this.method_14(feature, num, num1, out num3);
                        if (num3 < 10)
                        {
                            num3 = 10;
                        }
                        num3 = num3 * 0.0005;
                        IGeometry geometry = ipolyline1.Intersect(polyline, (esriGeometryDimension)1);
                        if (geometry != null)
                        {
                            IPoint point = null;
                            if (geometry is IPoint)
                            {
                                point = (IPoint)geometry;
                            }
                            else if (geometry is IMultipoint)
                            {
                                IPointCollection pointCollection = (IPointCollection)geometry;
                                if (pointCollection.PointCount > 0)
                                {
                                    point = pointCollection.get_Point(0);
                                }
                            }
                            if (point != null)
                            {
                                int num4 = this.method_5(this.ipolyline_1, point);
                                double num5 = this.method_6(this.ipolyline_0, point, num4);
                                if (this.int_0 == 0)
                                {
                                    num5 = num5 - double0;
                                }
                                else if (2 == this.int_0)
                                {
                                    num5 = num5 + double0;
                                }
                                int num6 = this.method_5(polyline, point);
                                double num7 = this.method_6(shape, point, num6);
                                if (lineConfigHeightFlag == 0)
                                {
                                    num7 = num7 - num3;
                                }
                                else if (2 == lineConfigHeightFlag)
                                {
                                    num7 = num7 + num3;
                                }
                                double num8 = Math.Abs(num5 - num7);
                                num8 = num8 - double0 - num3;
                                if (num8 < 0.001)
                                {
                                    num8 = 0;
                                }
                                string str = feature.get_Value(num2).ToString();
                                CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
                                {
                                    _OID = feature.OID,
                                    _sKind = str,
                                    _dVerDistance = num8,
                                    _dVerBase = 0,
                                    _pClass = (IFeatureClass)feature.Class
                                };
                                string str1 = this.method_7(feature);
                                string str2 = this.method_7(this.ifeature_0);
                                string value = this.dataGridViewComboBoxCell_0.Value as string;
                                if (this.dictionary_0.ContainsKey(value))
                                {
                                    value = this.dictionary_0[value].ToString();
                                }
                                cItem._dVerBase = (double)CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(CommonUtils.GetSmpClassName(value), CommonUtils.GetSmpClassName(feature.Class.AliasName), str2, str1);
                                this.list_4.Add(cItem);
                            }
                        }
                        feature = featureCursor.NextFeature();
                    }
                }
            }
        }

        private void method_4()
        {
            List<CHitAnalyse.CItem> list4 = this.list_4;
            this.dataGridViewVer.Rows.Clear();
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.Red
            };
            if (list4.Count > 0)
            {
                this.dataGridViewVer.Rows.Add(list4.Count);
                for (int i = 0; i < list4.Count; i++)
                {
                    CHitAnalyse.CItem item = list4[i];
                    DataGridViewRow str = this.dataGridViewVer.Rows[i];
                    str.Tag = item._pClass;
                    int num = i + 1;
                    str.Cells[0].Value = num.ToString();
                    str.Cells[1].Value = item._sKind;
                    str.Cells[2].Value = item._OID.ToString();
                    str.Cells[3].Value = item._dVerDistance.ToString("0.000");
                    if (item._dVerBase > 0.001)
                    {
                        str.Cells[4].Value = item._dVerBase.ToString("0.000");
                    }
                    if (item._dVerDistance < item._dVerBase)
                    {
                        str.Cells[3].Style = dataGridViewCellStyle;
                    }
                }
            }
        }

        private int method_5(IPolyline polyline, IPoint point)
        {
            int num;
            int num1 = -1;
            IPointCollection pointCollection = (IPointCollection)polyline;
            if (pointCollection.PointCount != 2)
            {
                ISegmentCollection segmentCollection = (ISegmentCollection)polyline;
                int pointCount = pointCollection.PointCount;
                bool[] flagArray = new bool[pointCount];
                bool[] flagArray1 = new bool[2];
                for (int i = 0; i < pointCount; i++)
                {
                    IPoint point1 = pointCollection.get_Point(i);
                    if (point.X < point1.X)
                    {
                        flagArray[i] = false;
                    }
                    else if ((point.X != point1.X ? false : point.Y == point1.Y))
                    {
                        num = i;
                        return num;
                    }
                    else
                    {
                        flagArray[i] = true;
                    }
                }
                for (int j = 0; j < pointCount - 1; j++)
                {
                    if (flagArray[j] != flagArray[j + 1])
                    {
                        IPoint point2 = pointCollection.get_Point(j);
                        IPoint point3 = pointCollection.get_Point(j + 1);
                        if (point.Y < point2.Y)
                        {
                            flagArray1[0] = false;
                        }
                        else if ((point.Y != point2.Y ? false : point.Y == point3.Y))
                        {
                            num = j;
                            return num;
                        }
                        else
                        {
                            flagArray1[0] = true;
                        }
                        if (point.Y >= point3.Y)
                        {
                            flagArray1[1] = true;
                        }
                        else
                        {
                            flagArray1[1] = false;
                        }
                        if (flagArray1[0] != flagArray1[1] && Math.Abs(((IProximityOperator)segmentCollection.get_Segment(j)).ReturnDistance(point)) < 0.001)
                        {
                            num = j;
                            return num;
                        }
                    }
                }
                if (num1 < 0)
                {
                    for (int k = 0; k < pointCount - 1; k++)
                    {
                        if (flagArray[k] == flagArray[k + 1])
                        {
                            IPoint point4 = pointCollection.get_Point(k);
                            IPoint point5 = pointCollection.get_Point(k + 1);
                            if ((point.X != point4.X ? false : point.X == point5.X))
                            {
                                num = k;
                                return num;
                            }
                        }
                    }
                }
                num = num1;
            }
            else
            {
                num = 0;
            }
            return num;
        }

        private double method_6(IPolyline polyline, IPoint point, int num)
        {
            double z;
            IPointCollection pointCollection = (IPointCollection)polyline;
            IPoint point1 = pointCollection.get_Point(num);
            IPoint point2 = pointCollection.get_Point(num + 1);
            double x = point2.X - point1.X;
            double y = point2.Y - point1.Y;
            double z1 = point2.Z - point1.Z;
            double x1 = point.X - point1.X;
            double y1 = point.Y - point1.Y;
            double num1 = Math.Sqrt(x * x + y * y);
            double num2 = Math.Sqrt(x1 * x1 + y1 * y1);
            if (num1 >= 0.001)
            {
                double num3 = num2 / num1;
                z = num3 * z1 + point1.Z;
            }
            else
            {
                z = point1.Z;
            }
            return z;
        }

        private string method_7(IFeature feature)
        {
            string str;
            string str1;
            if (feature != null)
            {
                int num = feature.Fields.FindField("埋设方式");
                str1 = (num == -1 ? "" : CommonUtils.ObjToString(feature.get_Value(num)));
                str = str1;
            }
            else
            {
                str = "";
            }
            return str;
        }

        private void method_8(IFeatureCursor featureCursor)
        {
            double double1 = this.double_1 * 0.0005;
            IProximityOperator ipolyline1 = (IProximityOperator)this.ipolyline_1;
            IFeature feature = featureCursor.NextFeature();
            int num = -1;
            if (feature != null)
            {
                num = feature.Fields.FindField("名称");
            }
            if (num >= 0)
            {
                while (feature != null)
                {
                    IPolyline shape = feature.Shape as IPolyline;
                    double num1 = ipolyline1.ReturnDistance(this.method_13(shape));
                    if (num1 >= 0.001)
                    {
                        num1 = num1 - double1;
                        if (num1 < 0.001)
                        {
                            num1 = 0;
                        }
                    }
                    else
                    {
                        num1 = 0;
                    }
                    string str = feature.get_Value(num).ToString();
                    CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
                    {
                        _OID = feature.OID,
                        _sKind = str,
                        _dHorDistance = num1,
                        _dHorBase = 0,
                        _pClass = (IFeatureClass)feature.Class
                    };
                    this.list_5.Add(cItem);
                    feature = featureCursor.NextFeature();
                }
            }
        }

        private void method_9()
        {
            List<CHitAnalyse.CItem> list5 = this.list_5;
            this.dataGridViewCenterLine.Rows.Clear();
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.Red
            };
            if (list5.Count > 0)
            {
                this.dataGridViewCenterLine.Rows.Add(list5.Count);
                for (int i = 0; i < list5.Count; i++)
                {
                    CHitAnalyse.CItem item = list5[i];
                    DataGridViewRow str = this.dataGridViewCenterLine.Rows[i];
                    str.Tag = item._pClass;
                    int num = i + 1;
                    str.Cells[0].Value = num.ToString();
                    str.Cells[1].Value = item._sKind;
                    str.Cells[2].Value = item._OID.ToString();
                    str.Cells[3].Value = item._dHorDistance.ToString("0.000");
                    if (item._dVerDistance < 0.001)
                    {
                        str.Cells[3].Style = dataGridViewCellStyle;
                    }
                }
            }
        }

        private void qsTayfWbAn(object obj, EventArgs eventArg)
        {
            IFeatureLayer cadFeatureLayerClass;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "CAD文件(*.dwg;*.dxf)|*.dwg;*.dxf"
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IWorkspaceFactory cadWorkspaceFactoryClass = new CadWorkspaceFactory();
                string fileName = openFileDialog.FileName;
                int num = fileName.LastIndexOf('\\');
                fileName = fileName.Substring(0, num);
                IWorkspace workspace = cadWorkspaceFactoryClass.OpenFromFile(fileName, this.m_app.MapControl.hWnd);
                if (workspace != null)
                {
                    IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
                    string str = openFileDialog.FileName.Substring(num + 1);
                    IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(str);
                    if (featureDataset != null)
                    {
                        IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
                        long classCount = (long)featureClassContainer.ClassCount;
                        this.textBoxLoadCADName.Text = openFileDialog.FileName;
                        this.list_0.Clear();
                        for (int i = 0; (long)i < classCount; i++)
                        {
                            IFeatureClass pclass = featureClassContainer.get_Class(i);
                            if (pclass != null)
                            {
                                if (pclass.FeatureType != (esriFeatureType)12)
                                {
                                    cadFeatureLayerClass = new CadFeatureLayer() as IFeatureLayer;
                                }
                                else
                                {
                                    cadFeatureLayerClass = new CadAnnotationLayer() as IFeatureLayer;
                                }
                                cadFeatureLayerClass.Name = (pclass.AliasName);
                                cadFeatureLayerClass.FeatureClass = (pclass);
                                cadFeatureLayerClass.Selectable = (true);

                                this.m_app.FocusMap.AddLayer(cadFeatureLayerClass);
                                if (cadFeatureLayerClass.FeatureClass.ShapeType == (esriGeometryType)3)

                                {
                                    this.list_0.Add(cadFeatureLayerClass);
                                }
                                this.list_1.Add(cadFeatureLayerClass);
                            }
                        }
                        int num1 = 0;

                        this.dataGridViewSelectItem.Rows.Clear();
                        for (int j = 0; j < this.list_0.Count; j++)

                        {
                            IFeatureLayer item = this.list_0[j];
                            IFeatureClass featureClass = item.FeatureClass;
                            int num2 = featureClass.FeatureCount(null);
                            if (num2 > 0)
                            {
                                IFeatureCursor featureCursor = featureClass.Search(null, false);
                                this.dataGridViewSelectItem.Rows.Add(num2);
                                for (int k = 0; k < num2; k++)
                                {
                                    IFeature feature = featureCursor.NextFeature();
                                    DataGridViewRow name = this.dataGridViewSelectItem.Rows[k + num1];
                                    name.Tag = item;
                                    name.Cells[0].Value = k + num1 + 1;
                                    name.Cells[1].Value = item.Name;
                                    name.Cells[2].Value = feature.OID;
                                    name.Cells[3].Value = 0;
                                    name.Cells[4].Value = 0;
                                    DataGridViewComboBoxCell dataGridViewComboBoxCell = (DataGridViewComboBoxCell)name.Cells[5];
                                    IEnumerator enumerator = this.dictionary_0.Keys.GetEnumerator();
                                    enumerator.Reset();
                                    for (int l = 0; l < this.dictionary_0.Keys.Count; l++)
                                    {
                                        enumerator.MoveNext();
                                        string str1 = enumerator.Current.ToString();
                                        dataGridViewComboBoxCell.Items.Add(str1);
                                    }
                                }
                                num1 = num1 + num2;
                            }
                        }
                        this.m_app.ActiveView.Refresh();
                    }
                }
            }
        }

        private void timer_0_Tick(object obj, EventArgs eventArg)
        {
            if ((!base.Visible ? true : this.m_nTimerCount > 20))
            {
                this.m_nTimerCount = 0;
                this.timer_0.Stop();
                IActiveView activeView = this.m_app.ActiveView;
                activeView.PartialRefresh((esriViewDrawPhase)8, null, null);
            }
            this.FlashDstItem();
            GForm0 mNTimerCount = this;
            mNTimerCount.m_nTimerCount = mNTimerCount.m_nTimerCount + 1;
        }
    }
}