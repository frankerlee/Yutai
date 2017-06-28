using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Resources;
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
    public partial class HitAnalyseDlg : XtraForm
    {
        private IContainer icontainer_0 = null;

        public IPipelineConfig m_PipeConfig;
        private CHitAnalyse chitAnalyse_0; //= new CHitAnalyse();

        public IAppContext m_app;

        public CommonDistAnalyse m_commonDistAls;

        public int m_nTimerCount;

        public IGeometry m_pFlashGeo;

        private IBasicLayerInfo _baseLayerInfo;
       


        public HitAnalyseDlg.HitAnalyseType HitType
        {
            get { return this.hitAnalyseType_0; }
            set { this.hitAnalyseType_0 = value; }
        }

        public HitAnalyseDlg(IAppContext pApp, IPipelineConfig config)
        {
            this.InitializeComponent();
            cmbDepthType.SelectedIndex = 0;
            this.m_commonDistAls = new CommonDistAnalyse()
            {
                m_nAnalyseType = DistAnalyseType.emHitDist
            };
            chitAnalyse_0 = new CHitAnalyse(config);

            this.m_app = pApp;
            m_PipeConfig = config;
            this.m_commonDistAls.PipeConfig = config;
            this.m_nTimerCount = 0;
            this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect;
        }

        private void btAnalyse_Click(object obj, EventArgs eventArg)
        {
            double num = Convert.ToDouble(this.tbBufferRadius.Text);
            this.btAnalyse.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            IPolyline polyline = this.BuildAnalysisLine();
            string text = this.tbPipeWidthOrHeight.Text;
            if (!this.radioButton2.Checked)
            {
                this.chitAnalyse_0.SetData(text, polyline);
            }
            else
            {
                if (this.comboBoxGX.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择管线层!");
                    this.Cursor = Cursors.Default;
                    this.btAnalyse.Enabled = true;
                    return;
                }
                LayerInfo selectedItem = (LayerInfo) this.comboBoxGX.SelectedItem;
                this.chitAnalyse_0.SetData(text, polyline, selectedItem.FeatureLauer.FeatureClass);
            }
            try
            {
                this.chitAnalyse_0.IsMUsing = cmbDepthType.SelectedIndex != 0;
                this.chitAnalyse_0.BufferDistance = num;
                this.chitAnalyse_0.Analyse_Hit();
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
                        str.Cells[0].Value = i + 1;
                        str.Cells[1].Value = item._sKind;
                        str.Cells[2].Value = item._OID;
                        str.Cells[3].Value = item._dHorDistance.ToString("0.000");
                        if (item._dHorBase > 0.001)
                        {
                            str.Cells[4].Value = item._dHorBase.ToString("0.000");
                        }
                        str.Cells[5].Value = item._dVerDistance.ToString("0.000");
                        if (item._dVerBase > 0.001)
                        {
                            str.Cells[6].Value = item._dVerBase.ToString("0.000");
                        }
                        if (item._dHorDistance < item._dHorBase)
                        {
                            str.Cells[3].Style = dataGridViewCellStyle;
                        }
                        if (item._dVerDistance < item._dVerBase)
                        {
                            str.Cells[5].Style = dataGridViewCellStyle;
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

        private void btSaveResult_Click(object obj, EventArgs eventArg)
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
                    odbcCommand.CommandText = string.Concat("CREATE TABLE HitAnalyseResult ",
                        "(序号 varchar(10), 管线性质 varchar(40), 编号 varchar(40), 水平间距 varchar(40), 水平标准 varchar(40),  垂直间距 varchar(40), 垂直标准 varchar(40))");
                    odbcCommand.ExecuteNonQuery();
                    int count = this.dataGridView1.Rows.Count;
                    int columnCount = this.dataGridView1.ColumnCount;
                    for (int i = 0; i < count; i++)
                    {
                        OdbcCommand odbcCommand1 = new OdbcCommand()
                        {
                            Connection = odbcConnection,
                            CommandType = CommandType.Text
                        };
                        string str = "INSERT INTO HitAnalyseResult ";
                        string str1 = " VALUES(";
                        for (int j = 0; j < columnCount; j++)
                        {
                            str1 = (j == columnCount - 1
                                ? string.Concat(str1, "'", this.dataGridView1[j, i].Value.ToString(), "')")
                                : string.Concat(str1, "'", this.dataGridView1[j, i].Value.ToString(), "', "));
                        }
                        str = string.Concat(str, str1);
                        odbcCommand1.CommandText = str;
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
                IActiveView activeView = this.m_app.ActiveView;
                activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
                this.timer_0.Start();
                this.timer_0.Interval = 300;
                this.m_nTimerCount = 0;
                this.FlashDstItem();
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object obj,
            DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArg)
        {
            this.dataGridView1.Sort(this.dataGridView1.Columns[dataGridViewCellMouseEventArg.ColumnIndex],
                ListSortDirection.Ascending);
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

        private void HitAnalyseDlg_Load(object obj, EventArgs eventArg)
        {
            this.chitAnalyse_0.m_app = this.m_app;
            this.chitAnalyse_0.IMap = this.m_app.FocusMap;
            CBase cBase = new CBase();
            int layerCount = this.m_app.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                cBase.AddLayer(this.m_app.FocusMap.get_Layer(i), cBase.m_listLayers);
            }
            foreach (IFeatureLayer mListLayer in cBase.m_listLayers)
            {
                LayerInfo layerInfo = new LayerInfo(mListLayer);
                this.comboBoxGX.Items.Add(layerInfo);
            }
            this.comboBoxGX.Enabled = this.radioButton2.Checked;
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
                this.tbPipeWidthOrHeight.Text = "";
                return;
            }

            //CommonUtils.GetSmpClassName(feature.Class.AliasName);
            IFeatureLayer pLayer =
                CommonUtils.GetLayerByFeatureClassName(m_app.FocusMap, ((IDataset) feature.Class).Name) as IFeatureLayer;
            IPipelineLayer pipeLayer = m_PipeConfig.GetPipelineLayer(feature.Class as IFeatureClass);
            IBasicLayerInfo pipeLine = m_PipeConfig.GetBasicLayerInfo(feature.Class as IFeatureClass);
            if (pipeLine == null)
            {
                this.m_commonDistAls.m_pBaseLine = null;
                this.btAnalyse.Enabled = false;
                this.m_app.FocusMap.ClearSelection();
                this.m_app.ActiveView.Refresh();
                this.tbPipeWidthOrHeight.Text = "";
                return;
            }

            List<IBasicLayerInfo> basicInfos = pipeLayer.GetLayers(enumPipelineDataType.Junction);
            IFeatureClass junFeatureClass = basicInfos.Count > 0 ? basicInfos[0].FeatureClass : null;
            //需要重新获取边信息
            IGeometricNetwork geometricNetwork = ((INetworkClass)junFeatureClass).GeometricNetwork;
            IFeatureClassContainer featureDataset = geometricNetwork.FeatureDataset as IFeatureClassContainer;
            IPointToEID pointToEIDClass = new PointToEID();
            pointToEIDClass.SourceMap = (m_app.FocusMap);
            pointToEIDClass.GeometricNetwork = (geometricNetwork);
            pointToEIDClass.SnapTolerance = (m_app.ActiveView.Extent.Width / 200.0);
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
                this.m_commonDistAls.m_pFeature = lineFeature;
                this.m_commonDistAls.m_pBaseLine = (IPolyline) shape;
                this.m_commonDistAls.m_strLayerName = pLayer.Name;
                //int num = feature.Fields.FindField("埋设方式");
                int num = lineFeature.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.MSFS));
                str = (num == -1 ? "" : this.GetDBObjectValue(lineFeature.get_Value(num)));
                this.m_commonDistAls.m_strBuryKind = str;
                int num1 = lineFeature.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                str1 = (num1 == -1 ? "" : this.GetDBObjectValue(lineFeature.get_Value(num1)));
                num1 = lineFeature.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                str2 = (num1 == -1 ? "" : lineFeature.get_Value(num1).ToString());
                if (str1 != "")
                {
                    this.tbPipeWidthOrHeight.Text = str1;
                }
                if (str2 != "")
                {
                    this.tbPipeWidthOrHeight.Text = str2;
                }
                this.m_commonDistAls.m_dDiameter =
                    this.m_commonDistAls.GetDiameterFromString(this.tbPipeWidthOrHeight.Text.Trim());
             

                IEdgeFeature edgeFeature = (IEdgeFeature)lineFeature;
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
                this.tbPipeWidthOrHeight.Text = "";
            }
        }

        public void GetDrawLine()
        {
            IGeometry geometry = this.m_app.MapControl.TrackLine();
            CommonUtils.NewLineElement(this.m_app.FocusMap, geometry as IPolyline);
            if (geometry != null && geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                this.m_commonDistAls.m_pBaseLine = (IPolyline) geometry;
                this.m_commonDistAls.m_strLayerName = "";
                this.m_commonDistAls.m_nBaseLineFromID = -1;
                this.m_commonDistAls.m_nBaseLineToID = -1;
                this.btAnalyse.Enabled = this.m_commonDistAls.m_pBaseLine != null;
            }
        }

        private void HitAnalyseDlg_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
        {
            base.Visible = false;
        }

        private void HitAnalyseDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
        {
            CommonUtils.DeleteAllElements(this.m_app.FocusMap);
            base.Visible = false;
            formClosingEventArg.Cancel = true;
        }

        public void InitDistAnalyseDlg()
        {
            this.m_app.FocusMap.ClearSelection();
            this.m_app.ActiveView.Refresh();
            this.dataGridViewBase.Rows.Clear();
            this.dataGridView1.Rows.Clear();
            this.btAnalyse.Enabled = false;
            this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect;
            this.radioButton1.Checked = true;
        }

        private string GetDBObjectValue(object obj)
        {
            return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
        }

        private void method_1(object obj, EventArgs eventArg)
        {
        }

        private void method_2()
        {
            IPointCollection polylineClass = new Polyline();
            int rowCount = this.dataGridViewBase.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.X = (Convert.ToDouble(this.dataGridViewBase[1, i].Value.ToString()));
                pointClass.Y = (Convert.ToDouble(this.dataGridViewBase[2, i].Value.ToString()));
                pointClass.Z = (Convert.ToDouble(this.dataGridViewBase[3, i].Value.ToString()));
                object missing = Type.Missing;
                polylineClass.AddPoint(pointClass, ref missing, ref missing);
            }
            this.m_commonDistAls.m_pBaseLine = (IPolyline) polylineClass;
            this.method_4(this.m_commonDistAls.m_pBaseLine);
            this.m_commonDistAls.m_dDiameter =
                this.m_commonDistAls.GetDiameterFromString(this.tbPipeWidthOrHeight.Text.Trim());
        }

        private IPolyline BuildAnalysisLine()
        {
            IPointCollection polylineClass = new Polyline();
            int rowCount = this.dataGridViewBase.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.X = (Convert.ToDouble(this.dataGridViewBase[1, i].Value.ToString()));
                pointClass.Y = (Convert.ToDouble(this.dataGridViewBase[2, i].Value.ToString()));
                pointClass.Z = (Convert.ToDouble(this.dataGridViewBase[3, i].Value.ToString()));
                object missing = Type.Missing;
                polylineClass.AddPoint(pointClass, ref missing, ref missing);
            }
            return polylineClass as IPolyline;
        }

        private void method_4(IGeometry geometry)
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

        private void method_5()
        {
            this.dataGridViewBase.Rows.Clear();
            this.dataGridView1.Rows.Clear();
            this.m_nTimerCount = 0;
            this.timer_0.Stop();
            this.m_app.ActiveView.Refresh();
        }

        private void method_6(object obj, HelpEventArgs helpEventArg)
        {
            string str = string.Concat(Application.StartupPath, "\\帮助.chm");
            Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "碰撞分析");
        }

        private void radioButton1_CheckedChanged(object obj, EventArgs eventArg)
        {
            this.dataGridViewBase.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseSelect;
            this.method_5();
            this.btAnalyse.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object obj, EventArgs eventArg)
        {
            this.dataGridViewBase.EditMode = DataGridViewEditMode.EditOnEnter;
            this.hitAnalyseType_0 = HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw;
            this.m_app.FocusMap.ClearSelection();
            this.comboBoxGX.Enabled = this.radioButton2.Checked;
            this.method_5();
            this.btAnalyse.Enabled = false;
        }

        public void RefreshBaseLineGrid()
        {
            if (this.m_commonDistAls.m_pBaseLine == null)
            {
                this.dataGridViewBase.Rows.Clear();
                return;
            }
            IPointCollection mPBaseLine = (IPointCollection) this.m_commonDistAls.m_pBaseLine;
                IFeature pFeature=null;
                int qdgcIdx = -1;
                int qdmsIdx = -1;
                int zdgcIdx = -1;
                int zdmsIdx = -1;
                if (cmbDepthType.SelectedIndex == 0)
                {
                    pFeature = this.chitAnalyse_0.PipeLayer_Class.GetFeature(this.chitAnalyse_0.BaseLine_OID);
                    qdgcIdx = pFeature.Fields.FindField(_baseLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDGC));
                    qdmsIdx = pFeature.Fields.FindField(_baseLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                    zdgcIdx = pFeature.Fields.FindField(_baseLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC));
                    zdmsIdx = pFeature.Fields.FindField(_baseLayerInfo.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));

                }
                int pointCount = mPBaseLine.PointCount;
                if (pointCount != 0)
                {
                    this.dataGridViewBase.Rows.Clear();
                    for (int i = 0; i < pointCount; i++)
                    {
                        IPoint point = mPBaseLine.get_Point(i);
                        double x = point.X;
                        double y = point.Y;
                        double z = 0;

                        if (cmbDepthType.SelectedIndex == 0 )
                        {
                            if (i == 0)
                            {
                                double z1 = Convert.ToDouble(pFeature.Value[qdgcIdx]);
                                double z2 = Convert.ToDouble(pFeature.Value[qdmsIdx]);
                                z = z1 - z2;
                            }
                            else if (i == pointCount - 1)
                            {
                            double z1 = Convert.ToDouble(pFeature.Value[zdgcIdx]);
                            double z2 = Convert.ToDouble(pFeature.Value[zdmsIdx]);
                            z = z1 - z2;
                            }
                        }
                        else
                        {
                             z= point.Z - point.M;
                        }
                        this.dataGridViewBase.Rows.Add(new object[] {""});
                        this.dataGridViewBase[0, i].Value = i + 1;
                        this.dataGridViewBase[1, i].Value = x.ToString("f3");
                        this.dataGridViewBase[2, i].Value = y.ToString("f3");
                        this.dataGridViewBase[3, i].Value = z.ToString("f3");
                        if (this.hitAnalyseType_0 == HitAnalyseDlg.HitAnalyseType.emHitAnalyseDraw)
                        {
                            this.dataGridViewBase[3, i].Value = "0.000";
                        }
                    }
                
            }
           
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

        private void tbPipeWidthOrHeight_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
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
            HitAnalyseDlg mNTimerCount = this;
            mNTimerCount.m_nTimerCount = mNTimerCount.m_nTimerCount + 1;
        }

        public enum HitAnalyseType
        {
            emHitAnalyseSelect,
            emHitAnalyseDraw
        }
    }
}