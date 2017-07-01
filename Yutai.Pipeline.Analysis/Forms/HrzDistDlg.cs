using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.ArcGIS.Common;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class HrzDistDlg : XtraForm
    {
        private CHitAnalyse chitAnalyse_0; //= new CHitAnalyse();

        public IAppContext m_app;

        public CommonDistAnalyse m_commonDistAls;

        public int m_nTimerCount;

        public IGeometry m_pFlashGeo;


        private IContainer icontainer_0 = null;

        public IPipelineConfig m_config;
        private IBasicLayerInfo _baseLayerInfo;


        public HrzDistDlg(IAppContext pApp, IPipelineConfig pipeConfig)
        {
            this.InitializeComponent();
            this.m_commonDistAls = new CommonDistAnalyse()
            {
                m_nAnalyseType = DistAnalyseType.emHrzDist
            };
            this.m_app = pApp;
            chitAnalyse_0 = new CHitAnalyse(pipeConfig);

            this.m_commonDistAls.PipeConfig = pipeConfig;
            m_config = pipeConfig;
            this.m_nTimerCount = 0;
        }

        private void btAnalyse_Click(object obj, EventArgs eventArg)
        {
            double num = Convert.ToDouble(this.tbBufferRadius.Text);
            this.btAnalyse.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.chitAnalyse_0.IsMUsing = cmbDepthType.SelectedIndex != 0;
                this.chitAnalyse_0.BufferDistance = num;
                this.chitAnalyse_0.Analyse_Horizontal();
                List<CHitAnalyse.CItem> items = this.chitAnalyse_0.Items;
                this.dataGridView1.Rows.Clear();
                DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.Red
                };
                if (items.Count > 0)
                {
                    this.dataGridView1.Rows.Add(items.Count);
                    for (int i = 0; i < items.Count; i++)
                    {
                        CHitAnalyse.CItem item = items[i];
                        DataGridViewRow str = this.dataGridView1.Rows[i];
                        str.Tag = item._pClass;
                        int num1 = i + 1;
                        str.Cells[0].Value = num1.ToString();
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
            catch (Exception exception)
            {
            }
            this.Cursor = Cursors.Default;
            this.btAnalyse.Enabled = true;
        }

        private void btClose_Click(object obj, EventArgs eventArg)
        {
            base.Close();
        }

        private void dataGridView1_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArg)
        {
            int rowIndex = dataGridViewCellEventArg.RowIndex;
            if (rowIndex >= 0)
            {
                int num = Convert.ToInt32(this.dataGridView1[2, rowIndex].Value);
                DataGridViewRow item = this.dataGridView1.Rows[dataGridViewCellEventArg.RowIndex];
                IFeatureClass tag = (IFeatureClass) item.Tag;
                if (tag != null)
                {
                    IFeature feature = tag.GetFeature(num);
                    if (feature != null)
                    {
                        this.m_pFlashGeo = feature.Shape;
                    }
                }
                CommonUtils.ScaleToTwoGeo(this.m_app.FocusMap, this.m_commonDistAls.m_pBaseLine, this.m_pFlashGeo);
                this.timer_0.Start();
                this.timer_0.Interval = 300;
                this.m_app.ActiveView.Refresh();
                this.m_nTimerCount = 0;
                this.FlashDstItem();
            }
        }

        public void FlashDstItem()
        {
            IMapControl3 mapControl = this.m_app.MapControl as IMapControl3;
            Color randColor = (new CRandomColor()).GetRandColor();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red = ((int) randColor.R);
            rgbColorClass.Green = ((int) randColor.G);
            rgbColorClass.Blue = ((int) randColor.B);
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
                mapControl.DrawShape(this.m_commonDistAls.m_pBaseLine, ref obj1);
            }
            catch
            {
            }
        }

        public void FlashDstItem22()
        {
            IMapControl3 mapControl = this.m_app.MapControl as IMapControl3;
            Color randColor = (new CRandomColor()).GetRandColor();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red = ((int) randColor.R);
            rgbColorClass.Green = ((int) randColor.G);
            rgbColorClass.Blue = ((int) randColor.B);
            simpleLineSymbolClass.Color = (rgbColorClass);
            simpleLineSymbolClass.Width = (5);
            mapControl.FlashShape(this.m_pFlashGeo, 30, 200, simpleLineSymbolClass);
        }

        public void GetBaseLine(IPoint point)
        {
            string str;
            string str1;
            string str2;
            this.timer_0.Stop();
            this.dataGridView1.Rows.Clear();
            IMap map = this.m_app.FocusMap;
            IFeature feature = ((IEnumFeature) map.FeatureSelection).Next();
            if (feature == null ? true : feature.FeatureType != esriFeatureType.esriFTSimpleEdge)
            {
                this.m_commonDistAls.m_pBaseLine = null;
                this.btAnalyse.Enabled = false;
                this.m_app.FocusMap.ClearSelection();
                this.m_app.ActiveView.Refresh();
                //this.tbPipeWidthOrHeight.Text = "";
                return;
            }
            IFeatureLayer pLayer =
                CommonUtils.GetLayerByFeatureClassName(m_app.FocusMap, ((IDataset) feature.Class).Name) as IFeatureLayer;
            IPipelineLayer pipeLayer = m_config.GetPipelineLayer(feature.Class as IFeatureClass);
            IBasicLayerInfo pipeLine = m_config.GetBasicLayerInfo(feature.Class as IFeatureClass);
            if (pipeLine == null)
            {
                this.m_commonDistAls.m_pBaseLine = null;
                this.btAnalyse.Enabled = false;
                this.m_app.FocusMap.ClearSelection();
                this.m_app.ActiveView.Refresh();
                return;
            }
            List<IBasicLayerInfo> basicInfos = pipeLayer.GetLayers(enumPipelineDataType.Junction);
            IFeatureClass junFeatureClass = basicInfos.Count > 0 ? basicInfos[0].FeatureClass : null;
            //需要重新获取边信息
            IGeometricNetwork geometricNetwork = ((INetworkClass) junFeatureClass).GeometricNetwork;
            IFeatureClassContainer featureDataset = geometricNetwork.FeatureDataset as IFeatureClassContainer;
            IPointToEID pointToEIDClass = new PointToEID();
            pointToEIDClass.SourceMap = (m_app.FocusMap);
            pointToEIDClass.GeometricNetwork = (geometricNetwork);
            pointToEIDClass.SnapTolerance = (m_app.ActiveView.Extent.Width/200.0);
            int edgeID = 0;
            IPoint location = null;
            double percent = 0;
            pointToEIDClass.GetNearestEdge(point, out edgeID, out location, out percent);
            if (edgeID == 0)
            {
                return;
            }

            int userClassID;
            int userID;
            int userSubID;
            INetElements network = geometricNetwork.Network as INetElements;
            network.QueryIDs(edgeID, esriElementType.esriETEdge, out userClassID, out userID, out userSubID);
            IFeatureClass lineClass = featureDataset.ClassByID[userClassID] as IFeatureClass;
            IFeature lineFeature = lineClass.GetFeature(userID);
            IGeometry shape = lineFeature.Shape;

            if (shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                this.ipolyline_0 = CommonUtils.GetPolylineDeepCopy((IPolyline) shape);
                this.m_commonDistAls.m_pFeature = feature;
                this.m_commonDistAls.m_pBaseLine = this.ipolyline_0;
                this.m_commonDistAls.m_strLayerName = feature.Class.AliasName;
                //int num = feature.Fields.FindField("埋设方式");
                int num = lineFeature.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.MSFS));
                str = (num == -1 ? "" : this.GetDBObjectValue(lineFeature.get_Value(num)));
                this.m_commonDistAls.m_strBuryKind = str;
                int num1 = lineFeature.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                str1 = (num1 == -1 ? "" : this.GetDBObjectValue(lineFeature.get_Value(num1)));
                num1 = feature.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                str2 = (num1 == -1 ? "" : lineFeature.get_Value(num1).ToString());
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
                IEdgeFeature edgeFeature = (IEdgeFeature) lineFeature;
                this.m_commonDistAls.m_nBaseLineFromID = edgeFeature.FromJunctionEID;
                this.m_commonDistAls.m_nBaseLineToID = edgeFeature.ToJunctionEID;
                this.btAnalyse.Enabled = this.m_commonDistAls.m_pBaseLine != null;
                this.chitAnalyse_0.PipeLayer_Class = lineFeature.Class as IFeatureClass;
                this.chitAnalyse_0.BaseLine_OID = lineFeature.OID;
                _baseLayerInfo = pipeLine;
            }
            else
            {
                MessageBox.Show("所选择的管线多于一条，或者不是管线！");
            }
        }

        private void HrzDistDlg_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
        {
        }

        private void HrzDistDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
        {
            base.Visible = false;
            formClosingEventArg.Cancel = true;
        }

        private void HrzDistDlg_Load(object obj, EventArgs eventArg)
        {
            this.chitAnalyse_0.m_app = this.m_app;
            this.chitAnalyse_0.IMap = this.m_app.FocusMap;
        }

        public void InitDistAnalyseDlg()
        {
            this.m_app.FocusMap.ClearSelection();
            this.m_app.ActiveView.Refresh();
            this.dataGridView1.Rows.Clear();
            this.btAnalyse.Enabled = false;
        }

        private string GetDBObjectValue(object obj)
        {
            return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
        }

        private void method_1(object obj, EventArgs eventArg)
        {
        }

        private void method_2(IGeometry geometry)
        {
            IPointCollection pointCollection = (IPointCollection) geometry;
            int pointCount = pointCollection.PointCount;
            if (pointCount != 0)
            {
                for (int i = 0; i < pointCount; i++)
                {
                    pointCollection.get_Point(i);
                }
            }
        }

        private void method_3(object obj, HelpEventArgs helpEventArg)
        {
            string str = string.Concat(Application.StartupPath, "\\帮助.chm");
            Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "水平净距分析");
        }

        private void tbBufferRadius_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
        {
            char keyChar = keyPressEventArg.KeyChar;
            if (keyChar != '\b')
            {
                switch (keyChar)
                {
                    case '.':
                    {
                        if ((this.tbBufferRadius.Text.IndexOf('.') != -1
                            ? false
                            : this.tbBufferRadius.SelectionStart != 0))
                        {
                            break;
                        }
                        keyPressEventArg.KeyChar = '\0';
                        break;
                    }
                    case '/':
                    {
                        keyPressEventArg.KeyChar = '\0';
                        break;
                    }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    {
                        break;
                    }
                    default:
                    {
                        goto case '/';
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
                activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
            }
            this.FlashDstItem();
            HrzDistDlg mNTimerCount = this;
            mNTimerCount.m_nTimerCount = mNTimerCount.m_nTimerCount + 1;
        }
    }
}