using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CHitAnalyse : CBase
    {
        private CHitAnalyse.enumHitAnalyseType enumHitAnalyseType_0 = CHitAnalyse.enumHitAnalyseType.emHit;

        private IPolyline baselineShape;

        private IPolyline baselineSimpleLine;

        private IFeature baseFeature;

        private int baselineOID;

        private double double_0;

        private double double_1;

        private int heightType;

        private IFeatureLayer baselineLayer;

        private IFeatureClass baselineFC;

        private double double_2 = 10;

        private List<CHitAnalyse.CItem> list_0 = new List<CHitAnalyse.CItem>();

        private ESRI.ArcGIS.Carto.IMap imap_0;

        //private string string_0 = "管线性质";

        //private string string_1 = "管径";

        //private string string_2 = "断面尺寸";

        private char[] char_0 = new char[] {'x', 'X', 'Х'};

        public IPipelineConfig m_config;

        public bool IsMUsing;

        public IPolyline BaseLine
        {
            set { this.BaseLine = value; }
        }

        public int BaseLine_OID
        {
            get { return this.baselineOID; }
            set
            {
                this.baselineOID = value;
                this.baselineShape = null;
                IFeature feature = this.baselineFC.GetFeature(this.baselineOID);
                IBasicLayerInfo lineConfig =
                    m_config.GetBasicLayerInfo(this.baselineFC.AliasName) as IBasicLayerInfo;
                if (feature != null)
                {
                    int gjIdx = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                    int dmccIdx = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                    if (feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ)) >= 0)
                    {
                        if ((gjIdx >= 0 ? false : dmccIdx < 0))
                        {
                            return;
                        }
                        this.baselineShape = (IPolyline) feature.Shape;
                        this.double_1 = this.GetPipeGJ(feature, gjIdx, dmccIdx, out this.double_0);
                        if (this.double_1 < 1)
                        {
                            this.double_1 = 10;
                        }
                        if (this.double_0 < 1)
                        {
                            this.double_0 = this.double_1;
                        }
                        this.heightType = (int) lineConfig.HeightType;
                        // this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(this.baselineFC.AliasName));
                    }
                    else
                    {
                        return;
                    }
                }
                this.baseFeature = feature;
            }
        }

        public double BufferDistance
        {
            set { this.double_2 = value; }
        }

        public ESRI.ArcGIS.Carto.IMap IMap
        {
            get { return this.imap_0; }
            set
            {
                this.imap_0 = value;
                this.m_listLayers.Clear();
                if (this.imap_0 != null)
                {
                    for (int i = 0; i < this.imap_0.LayerCount; i++)
                    {
                        base.AddLayer(this.imap_0.get_Layer(i), this.m_listLayers);
                    }
                }
                //this.string_0 = this.m_app.PipeConfig.get_Kind();
                //this.string_1 = this.m_app.PipeConfig.get_Diameter();
                //this.string_2 = this.m_app.PipeConfig.get_Section_Size();
            }
        }

        public List<CHitAnalyse.CItem> Items
        {
            get { return this.list_0; }
        }

        public IFeatureLayer PipeLayer
        {
            set
            {
                this.baselineFC = null;
                this.baselineLayer = value;
                if (this.baselineLayer != null)
                {
                    this.baselineFC = this.baselineLayer.FeatureClass;
                }
            }
        }

        public IFeatureClass PipeLayer_Class
        {
            get { return this.baselineFC; }
            set { this.baselineFC = value; }
        }

        public CHitAnalyse(IPipelineConfig config)
        {
            m_config = config;
        }

        public void Analyse_Hit()
        {
            this.enumHitAnalyseType_0 = CHitAnalyse.enumHitAnalyseType.emHit;
            this.method_1(new CHitAnalyse.Delegate1(this.Analyse_Hit));
        }

        public void Analyse_Hit(IFeatureCursor pFeaCursor)
        {
            double num;
            double double0 = this.double_0*0.0005;
            double double1 = this.double_1*0.0005;
            ITopologicalOperator ipolyline1 = (ITopologicalOperator) this.baselineSimpleLine;
            IProximityOperator proximityOperator = (IProximityOperator) this.baselineSimpleLine;
            IPointCollection ipolyline0 = (IPointCollection) this.baselineShape;
            int qdgcIdx = -1;
            int qdmsIdx = -1;
            int zdgcIdx = -1;
            int zdmsIdx = -1;

            double z = ipolyline0.get_Point(0).Z;
            double num1 = z;
            for (int i = 1; i < ipolyline0.PointCount; i++)
            {
                double z1 = ipolyline0.get_Point(i).Z;
                if (z > z1)
                {
                    z = z1;
                }
                if (num1 < z1)
                {
                    num1 = z1;
                }
            }
            if (this.heightType == 0)
            {
                z = z - double0;
                num1 = num1 - double0;
            }
            else if (2 == this.heightType)
            {
                z = z + double0;
                num1 = num1 + double0;
            }
            IFeature feature = pFeaCursor.NextFeature();
            if (feature == null) return;

            IBasicLayerInfo lineConfig = m_config.GetBasicLayerInfo(feature.Class.AliasName) as IBasicLayerInfo;
            int num2 = -1;
            int num3 = -1;
            int num4 = -1;
            if (feature != null)
            {
                num2 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                num3 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                num4 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ));
                if (!IsMUsing)
                {
                    qdgcIdx = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.QDGC));
                    qdmsIdx = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                    zdgcIdx = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC));
                    zdmsIdx = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
                }
            }
            if (num4 >= 0)
            {
                if ((num2 >= 0 ? true : num3 >= 0))
                {
                    //!+ 要判断lineConfigHeightFlag 和 enumPipelineHeightType的对应关系
                    int lineConfigHeightFlag = (int) lineConfig.HeightType;
                    //this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(feature.Class.AliasName));
                    while (feature != null)
                    {
                        IPolyline shape = feature.Shape as IPolyline;
                        IPolyline polyline = this.SimplifyPolyline(shape);
                        double num5 = 0;
                        double num6 = this.GetPipeGJ(feature, num2, num3, out num5);
                        if (num6 < 10)
                        {
                            num6 = 10;
                        }
                        num6 = num6*0.0005;
                        if (num5 < 10)
                        {
                            num5 = 10;
                        }
                        num5 = num5*0.0005;
                        IPoint point = null;
                        IGeometry geometry = ipolyline1.Intersect(polyline, (esriGeometryDimension) 1);
                        if (geometry != null)
                        {
                            if (geometry is IPoint)
                            {
                                point = (IPoint) geometry;
                            }
                            else if (geometry is IMultipoint)
                            {
                                IPointCollection pointCollection = (IPointCollection) geometry;
                                if (pointCollection.PointCount > 0)
                                {
                                    point = pointCollection.get_Point(0);
                                }
                            }
                        }
                        if (point == null)
                        {
                            CHitAnalyse.CItem cItem = new CHitAnalyse.CItem();
                            string str = feature.get_Value(num4).ToString();
                            cItem._OID = feature.OID;
                            cItem._sKind = str;
                            cItem._pClass = (IFeatureClass) feature.Class;
                            num = proximityOperator.ReturnDistance(polyline);
                            num = num - double1 - num6;
                            if (num >= 0.001)
                            {
                                cItem._dHorDistance = num;
                                cItem._dHorBase = 0;
                                IPointCollection pointCollection1 = (IPointCollection) shape;
                                double z2;
                                if (IsMUsing)
                                    z2 = pointCollection1.get_Point(0).Z - pointCollection1.get_Point(0).M;
                                else
                                {
                                    double pHeight = 0;
                                    z2 = GetDoubleValue(feature, qdgcIdx, out pHeight) -
                                         GetDoubleValue(feature, qdmsIdx, out pHeight);
                                }
                                double num7 = z2;
                                if (IsMUsing)
                                {
                                    for (int j = 1; j < pointCollection1.PointCount; j++)
                                    {
                                        double z3 = pointCollection1.get_Point(j).Z - pointCollection1.get_Point(j).M;
                                        if (z2 > z3)
                                        {
                                            z2 = z3;
                                        }
                                        if (num7 < z3)
                                        {
                                            num7 = z3;
                                        }
                                    }
                                }
                                else
                                {
                                    double pHeight = 0;
                                    double z3 = GetDoubleValue(feature, zdgcIdx, out pHeight) -
                                                GetDoubleValue(feature, zdmsIdx, out pHeight);
                                    if (z2 > z3)
                                    {
                                        z2 = z3;
                                    }
                                    if (num7 < z3)
                                    {
                                        num7 = z3;
                                    }
                                }
                                if (lineConfigHeightFlag == 0)
                                {
                                    z2 = z2 - num5;
                                    num7 = num7 - num5;
                                }
                                else if (2 == lineConfigHeightFlag)
                                {
                                    z2 = z2 + num5;
                                    num7 = num7 + num5;
                                }
                                if (z > num7)
                                {
                                    num = z - num7;
                                    num = num - double0 - num5;
                                    if (num < 0.001)
                                    {
                                        num = 0;
                                    }
                                }
                                else if (z2 <= num1)
                                {
                                    num = 0;
                                }
                                else
                                {
                                    num = z2 - num1;
                                    num = num - double0 - num5;
                                    if (num < 0.001)
                                    {
                                        num = 0;
                                    }
                                }
                                cItem._dVerDistance = num;
                                cItem._dVerBase = 0;
                                cItem._dHorBase =
                                    (double)
                                    CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(m_config,
                                        CommonUtils.GetSmpClassName(this.baselineFC.AliasName),
                                        CommonUtils.GetSmpClassName(feature.Class.AliasName), this.baseFeature, feature);
                                string str1 = this.GetPipeMSFS(feature);
                                string str2 = this.GetPipeMSFS(this.baseFeature);
                                cItem._dVerBase =
                                    (double)
                                    CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(m_config,
                                        CommonUtils.GetSmpClassName(this.baselineFC.AliasName),
                                        CommonUtils.GetSmpClassName(feature.Class.AliasName), str2, str1);
                                this.list_0.Add(cItem);
                            }
                            else
                            {
                                feature = pFeaCursor.NextFeature();
                                continue;
                            }
                        }
                        else
                        {
                            int num8 = this.method_3(this.baselineSimpleLine, point);
                            double num9 = 0;
                            if (IsMUsing)
                                num9 = this.CalculateZ(this.baselineShape, point, num8);
                            else
                            {
                                num9 = this.CalculateZ(this.baselineShape, point, num8, feature, qdgcIdx, zdgcIdx);
                            }

                            if (this.heightType == 0)
                            {
                                num9 = num9 - double0;
                            }
                            else if (2 == this.heightType)
                            {
                                num9 = num9 + double0;
                            }
                            int num10 = this.method_3(polyline, point);
                            double num11 = 0;
                            if (IsMUsing)
                                num11 = this.CalculateZAlongLine(shape, point, num10);
                            else
                            {
                                num11 = this.CalculateZAlongLine(shape, point, num10, feature, qdgcIdx, qdmsIdx, zdgcIdx,
                                    zdmsIdx);
                            }

                            if (lineConfigHeightFlag == 0)
                            {
                                num11 = num11 - num5;
                            }
                            else if (2 == lineConfigHeightFlag)
                            {
                                num11 = num11 + num5;
                            }
                            num = Math.Abs(num9 - num11);
                            if (num >= 0.001)
                            {
                                string str3 = feature.get_Value(num4).ToString();
                                CHitAnalyse.CItem pipeLineAlarmHrzDistByFeatureClassName2 = new CHitAnalyse.CItem()
                                {
                                    _OID = feature.OID,
                                    _sKind = str3,
                                    _dVerDistance = num,
                                    _dVerBase = 0,
                                    _dHorDistance = 0,
                                    _dHorBase = 0,
                                    _pClass = (IFeatureClass) feature.Class
                                };
                                pipeLineAlarmHrzDistByFeatureClassName2._dHorBase =
                                    (double)
                                    CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(m_config,
                                        CommonUtils.GetSmpClassName(this.baselineFC.AliasName),
                                        CommonUtils.GetSmpClassName(feature.Class.AliasName), this.baseFeature, feature);
                                string str4 = this.GetPipeMSFS(feature);
                                string str5 = this.GetPipeMSFS(this.baseFeature);
                                pipeLineAlarmHrzDistByFeatureClassName2._dVerBase =
                                    (double)
                                    CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(m_config,
                                        CommonUtils.GetSmpClassName(this.baselineFC.AliasName),
                                        CommonUtils.GetSmpClassName(feature.Class.AliasName), str5, str4);
                                this.list_0.Add(pipeLineAlarmHrzDistByFeatureClassName2);
                            }
                            else
                            {
                                feature = pFeaCursor.NextFeature();
                                continue;
                            }
                        }
                        feature = pFeaCursor.NextFeature();
                    }
                }
            }
        }

        public void Analyse_Horizontal()
        {
            this.enumHitAnalyseType_0 = CHitAnalyse.enumHitAnalyseType.emHorizontal;
            this.method_1(new CHitAnalyse.Delegate1(this.Analyse_Horizontal));
        }

        public void Analyse_Horizontal(IFeatureCursor pFeaCursor)
        {
            double double1 = this.double_1*0.0005;
            IProximityOperator ipolyline1 = (IProximityOperator) this.baselineSimpleLine;
            IFeature feature = pFeaCursor.NextFeature();
            if (feature == null) return;
            int num = -1;
            int num1 = -1;
            int num2 = -1;


            IBasicLayerInfo lineConfig = m_config.GetBasicLayerInfo(feature.Class.AliasName) as IBasicLayerInfo;
            IPipelineLayer pipelineLayer = m_config.GetPipelineLayer(feature.Class as IFeatureClass);
            IPipelineLayer pipelineLayer0 = m_config.GetPipelineLayer(baselineFC);
            num = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
            num1 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
            num2 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ));

            if (num2 >= 0)
            {
                if ((num >= 0 ? true : num1 >= 0))
                {
                    while (feature != null)
                    {
                        IPolyline polyline = this.SimplifyPolyline(feature.Shape as IPolyline);
                        double num3 = 0;
                        double num4 = this.GetPipeGJ(feature, num, num1, out num3);
                        if (num4 < 10)
                        {
                            num4 = 10;
                        }
                        num4 = num4*0.0005;
                        double num5 = ipolyline1.ReturnDistance(polyline);
                        num5 = num5 - double1 - num4;
                        if (num5 >= 0.001)
                        {
                            string str = feature.get_Value(num2).ToString();
                            CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
                            {
                                _OID = feature.OID,
                                _sKind = str,
                                _dHorDistance = num5,
                                _dHorBase = 0,
                                _pClass = (IFeatureClass) feature.Class
                            };
                            cItem._dHorBase =
                                (double)
                                CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(m_config,
                                    baselineFC.AliasName, feature.Class.AliasName, this.baseFeature, feature);
                            this.list_0.Add(cItem);
                            feature = pFeaCursor.NextFeature();
                        }
                        else
                        {
                            feature = pFeaCursor.NextFeature();
                        }
                    }
                }
            }
        }

        public void Analyse_Vertical()
        {
            this.BufferDistance = 1;
            this.enumHitAnalyseType_0 = CHitAnalyse.enumHitAnalyseType.emVertical;
            this.method_1(new CHitAnalyse.Delegate1(this.Analyse_Vertical));
        }

        public void Analyse_Vertical(IFeatureCursor pFeaCursor)
        {
            double double0 = this.double_0*0.0005;
            ITopologicalOperator ipolyline1 = (ITopologicalOperator) this.baselineSimpleLine;
            IFeature feature = pFeaCursor.NextFeature();
            if (feature == null) return;
            int num = -1;
            int num1 = -1;
            int num2 = -1;

            IBasicLayerInfo lineConfig = m_config.GetBasicLayerInfo(feature.Class.AliasName) as IBasicLayerInfo;
            IPipelineLayer pipelineLayer = m_config.GetPipelineLayer(feature.Class as IFeatureClass);
            IPipelineLayer pipelineLayer0 = m_config.GetPipelineLayer(baselineFC);

            if (feature != null)
            {
                num = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                num1 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                num2 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ));
            }
            if (num2 >= 0)
            {
                if ((num >= 0 ? true : num1 >= 0))
                {
                    int lineConfigHeightFlag = (int) lineConfig.HeightType;
                    // this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(feature.Class.AliasName));
                    while (feature != null)
                    {
                        IPolyline shape = feature.Shape as IPolyline;
                        IPolyline polyline = this.SimplifyPolyline(shape);
                        double num3 = 0;
                        this.GetPipeGJ(feature, num, num1, out num3);
                        if (num3 < 10)
                        {
                            num3 = 10;
                        }
                        num3 = num3*0.0005;
                        IGeometry geometry = ipolyline1.Intersect(polyline, (esriGeometryDimension) 1);
                        if (geometry != null)
                        {
                            IPoint point = null;
                            if (geometry is IPoint)
                            {
                                point = (IPoint) geometry;
                            }
                            else if (geometry is IMultipoint)
                            {
                                IPointCollection pointCollection = (IPointCollection) geometry;
                                if (pointCollection.PointCount > 0)
                                {
                                    point = pointCollection.get_Point(0);
                                }
                            }
                            if (point != null)
                            {
                                int num4 = this.method_3(this.baselineSimpleLine, point);
                                double num5 = this.CalculateZAlongLine(this.baselineShape, point, num4);
                                if (this.heightType == 0)
                                {
                                    num5 = num5 - double0;
                                }
                                else if (2 == this.heightType)
                                {
                                    num5 = num5 + double0;
                                }
                                int num6 = this.method_3(polyline, point);
                                double num7 = this.CalculateZAlongLine(shape, point, num6);
                                if (lineConfigHeightFlag == 0)
                                {
                                    num7 = num7 - num3;
                                }
                                else if (2 == lineConfigHeightFlag)
                                {
                                    num7 = num7 + num3;
                                }
                                double num8 = Math.Abs(num5 - num7);
                                if (num8 >= 0.001)
                                {
                                    string str = feature.get_Value(num2).ToString();
                                    CHitAnalyse.CItem cItem = new CHitAnalyse.CItem()
                                    {
                                        _OID = feature.OID,
                                        _sKind = str,
                                        _dVerDistance = num8,
                                        _dVerBase = 0,
                                        _pClass = (IFeatureClass) feature.Class
                                    };
                                    string str1 = this.GetPipeMSFS(feature);
                                    string str2 = this.GetPipeMSFS(this.baseFeature);
                                    cItem._dVerBase =
                                        (double)
                                        CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(m_config,
                                            pipelineLayer0.ClassCode, pipelineLayer.ClassCode, str2, str1);
                                    this.list_0.Add(cItem);
                                }
                                else
                                {
                                    feature = pFeaCursor.NextFeature();
                                    continue;
                                }
                            }
                        }
                        feature = pFeaCursor.NextFeature();
                    }
                }
            }
        }

        private IPolyline SimplifyPolyline(IPolyline polyline)
        {
            object missing = Type.Missing;
            IPolyline polylineClass = new Polyline() as IPolyline;
            IPointCollection pointCollection = (IPointCollection) polylineClass;
            IPointCollection pointCollection1 = (IPointCollection) polyline;
            for (int i = 0; i <= pointCollection1.PointCount - 1; i++)
            {
                IPoint point = pointCollection1.get_Point(i);
                IPoint pointClass = new Point();
                pointClass.X = (point.X);
                pointClass.Y = (point.Y);
                pointClass.Z = (0);
                pointCollection.AddPoint(pointClass, ref missing, ref missing);
            }
            return polylineClass;
        }

        private void method_1(CHitAnalyse.Delegate1 delegate1)
        {
            CommonUtils.AppContext = this.m_app;
            this.list_0.Clear();
            this.baselineSimpleLine = this.SimplifyPolyline(this.baselineShape);
            IGeometry geometry = ((ITopologicalOperator) this.baselineShape).Buffer(this.double_2);
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            spatialFilterClass.Geometry = (geometry);
            spatialFilterClass.SpatialRel = (esriSpatialRelEnum) (1);
            if (this.m_listLayers.Count >= 1)
            {
                foreach (IFeatureLayer mListLayer in this.m_listLayers)
                {
                    if (!mListLayer.Visible)
                    {
                        continue;
                    }
                    IFeatureClass featureClass = mListLayer.FeatureClass;
                    if ((featureClass.AliasName == this.baselineFC.AliasName
                        ? true
                        : featureClass.ShapeType != esriGeometryType.esriGeometryPolyline))
                    {
                        continue;
                    }
                    spatialFilterClass.GeometryField = featureClass.ShapeFieldName;
                    delegate1(featureClass.Search(spatialFilterClass, false));
                }
            }
        }

        private string GetPipeMSFS(IFeature feature)
        {
            string str;
            string str1;
            if (feature != null)
            {
                IBasicLayerInfo layerInfo = m_config.GetBasicLayerInfo(feature.Class as IFeatureClass);
                if (layerInfo == null) return "";
                int num = feature.Fields.FindField(layerInfo.GetFieldName(PipeConfigWordHelper.LineWords.MSFS));
                str1 = (num == -1 ? "" : CommonUtils.ObjToString(feature.get_Value(num)));
                str = str1;
            }
            else
            {
                str = "";
            }
            return str;
        }

        private int method_3(IPolyline polyline, IPoint point)
        {
            int num;
            int num1 = -1;
            IPointCollection pointCollection = (IPointCollection) polyline;
            if (pointCollection.PointCount != 2)
            {
                ISegmentCollection segmentCollection = (ISegmentCollection) polyline;
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
                        if (flagArray1[0] != flagArray1[1] &&
                            Math.Abs(((IProximityOperator) segmentCollection.get_Segment(j)).ReturnDistance(point)) <
                            0.001)
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

        private double CalculateZ(IPolyline polyline, IPoint point, int num)
        {
            double z;
            IPointCollection pointCollection = (IPointCollection) polyline;
            IPoint point1 = pointCollection.get_Point(num);
            IPoint point2 = pointCollection.get_Point(num + 1);
            double x = point2.X - point1.X;
            double y = point2.Y - point1.Y;
            double z1 = point2.Z - point1.Z;
            double x1 = point.X - point1.X;
            double y1 = point.Y - point1.Y;
            double num1 = Math.Sqrt(x*x + y*y);
            double num2 = Math.Sqrt(x1*x1 + y1*y1);
            if (num1 >= 0.001)
            {
                double num3 = num2/num1;
                z = num3*z1 + point1.Z;
            }
            else
            {
                z = point1.Z;
            }
            return z;
        }

        private double CalculateZ(IPolyline polyline, IPoint point, int num, IFeature pFeature, int qdgcIdx, int zdgcIdx)
        {
            double height;
            double qdgc = GetDoubleValue(pFeature, qdgcIdx, out height);

            double zdgc = GetDoubleValue(pFeature, zdgcIdx, out height);

            double z;
            IPointCollection pointCollection = (IPointCollection) polyline;
            IPoint point1 = pointCollection.get_Point(num);
            IPoint point2 = pointCollection.get_Point(num + 1);
            double x = point2.X - point1.X;
            double y = point2.Y - point1.Y;
            double z1 = zdgc - qdgc;
            double x1 = point.X - point1.X;
            double y1 = point.Y - point1.Y;
            double num1 = Math.Sqrt(x*x + y*y);
            double num2 = Math.Sqrt(x1*x1 + y1*y1);
            if (num1 >= 0.001)
            {
                double num3 = num2/num1;
                z = num3*z1 + qdgc;
            }
            else
            {
                z = qdgc;
            }
            return z;
        }

        private double CalculateZAlongLine(IPolyline polyline, IPoint point, int num)
        {
            double z;
            IPointCollection pointCollection = (IPointCollection) polyline;
            IPoint point1 = pointCollection.get_Point(num);
            IPoint point2 = pointCollection.get_Point(num + 1);
            double x = point2.X - point1.X;
            double y = point2.Y - point1.Y;
            double z1 = point2.Z - point2.M - point1.Z + point1.M;
            double x1 = point.X - point1.X;
            double y1 = point.Y - point1.Y;
            double num1 = Math.Sqrt(x*x + y*y);
            double num2 = Math.Sqrt(x1*x1 + y1*y1);
            if (num1 >= 0.001)
            {
                double num3 = num2/num1;
                z = num3*z1 + point1.Z - point1.M;
            }
            else
            {
                z = point1.Z - point1.M;
            }
            return z;
        }

        private double CalculateZAlongLine(IPolyline polyline, IPoint point, int num, IFeature pFeature, int qdgcIdx,
            int qdmsIdx, int zdgcIdx, int zdmsIdx)
        {
            double z;
            double height;
            double qdgc = GetDoubleValue(pFeature, qdgcIdx, out height);
            double qdms = GetDoubleValue(pFeature, qdmsIdx, out height);
            double zdgc = GetDoubleValue(pFeature, zdgcIdx, out height);
            double zdms = GetDoubleValue(pFeature, zdmsIdx, out height);
            IPointCollection pointCollection = (IPointCollection) polyline;
            IPoint point1 = pointCollection.get_Point(num);
            IPoint point2 = pointCollection.get_Point(num + 1);
            double x = point2.X - point1.X;
            double y = point2.Y - point1.Y;
            double z1 = zdgc - zdms - qdgc + qdms;
            double x1 = point.X - point1.X;
            double y1 = point.Y - point1.Y;
            double num1 = Math.Sqrt(x*x + y*y);
            double num2 = Math.Sqrt(x1*x1 + y1*y1);
            if (num1 >= 0.001)
            {
                double num3 = num2/num1;
                z = num3*z1 + qdgc - qdms;
            }
            else
            {
                z = qdgc - qdms;
            }
            return z;
        }

        private double GetPipeGJ(IFeature feature, int gjIdx, int dmccIdx, out double double_3)
        {
            double_3 = 0.0;
            double num3 = 0.0;
            try
            {
                if (gjIdx > 0)
                {
                    object value = feature.get_Value(gjIdx);
                    if (Convert.IsDBNull(value))
                    {
                        num3 = 0.0;
                    }
                    else
                    {
                        num3 = Convert.ToDouble(value);
                    }
                }
                if (num3 < 1.0)
                {
                    num3 = this.GetDoubleValue(feature, dmccIdx, out double_3);
                }
                else
                {
                    double_3 = num3;
                }
                return num3;
            }
            catch (Exception ex)
            {
                return 0.0;
            }
        }


        private double GetDoubleValue(IFeature feature, int fldIdx, out double height)
        {
            double width = 0;
            height = 0;
            string str = "";
            try
            {
                if (fldIdx > 0)
                {
                    object value = feature.get_Value(fldIdx);
                    if (!Convert.IsDBNull(value))
                    {
                        str = Convert.ToString(value);
                    }
                    if ((str == null ? false : str.Length >= 1))
                    {
                        string[] strArrays = str.Split(this.char_0);
                        if (strArrays[0].ToString().Trim() != "")
                        {
                            width = Convert.ToDouble(strArrays[0]);
                            if (strArrays.Length > 1)
                                height = Convert.ToDouble(strArrays[1]);
                        }
                        else
                        {
                            width = 0;
                            height = 0;
                        }
                    }
                }
                return width;
            }
            catch (Exception ex)
            {
                return 0.0;
            }
        }

        public void RegexPipeSize(object obj, out double dw, out double dh)
        {
            double num = 0;
            double num1 = num;
            dh = num;
            dw = num1;
            TypeCode typeCode = Convert.GetTypeCode(obj);
            if (!(typeCode < TypeCode.Int16 ? true : typeCode > TypeCode.Decimal))
            {
                dw = Convert.ToDouble(obj);
                dh = dw;
            }
            else if (!Convert.IsDBNull(obj) && typeCode == TypeCode.String)
            {
                this.RegexPipeSize(Convert.ToString(obj), out dw, out dh);
            }
        }

        public void RegexPipeSize(string sSize, out double dw, out double dh)
        {
            double num = 0;
            double num1 = num;
            dh = num;
            dw = num1;
            if (sSize != null && sSize.Length >= 1)
            {
                string[] strArrays = sSize.Split(this.char_0);
                dw = Convert.ToDouble(strArrays[0]);
                if ((int) strArrays.Length <= 1)
                {
                    dh = dw;
                }
                else
                {
                    dh = Convert.ToDouble(strArrays[1]);
                }
            }
        }

        public void SetData(string sSize, IPolyline pLine, IFeatureClass pClass)
        {
            this.baseFeature = null;
            this.baselineFC = pClass;
            this.RegexPipeSize(sSize, out this.double_1, out this.double_0);
            if (this.double_1 < 10)
            {
                this.double_1 = 10;
            }
            if (this.double_0 < 10)
            {
                this.double_0 = 10;
            }
            this.baselineShape = pLine;
        }

        public void SetData(string sSize, IPolyline pLine)
        {
            this.RegexPipeSize(sSize, out this.double_1, out this.double_0);
            if (this.double_1 < 10)
            {
                this.double_1 = 10;
            }
            if (this.double_0 < 10)
            {
                this.double_0 = 10;
            }
            this.baselineShape = pLine;
        }

        public string toHtml()
        {
            string str;
            string str1;
            if (this.list_0.Count >= 1)
            {
                string str2 = "";
                string str3 = "<tr > {0} </tr>";
                switch (this.enumHitAnalyseType_0)
                {
                    case CHitAnalyse.enumHitAnalyseType.emHorizontal:
                    {
                        for (int i = 0; i < this.list_0.Count; i++)
                        {
                            str1 = this.list_0[i].toHtml_Hor(i + 1);
                            str1 = string.Format(str3, str1);
                            str2 = string.Concat(str2, str1);
                        }
                        break;
                    }
                    case CHitAnalyse.enumHitAnalyseType.emVertical:
                    {
                        for (int j = 0; j < this.list_0.Count; j++)
                        {
                            str1 = this.list_0[j].toHtml_Ver(j + 1);
                            str1 = string.Format(str3, str1);
                            str2 = string.Concat(str2, str1);
                        }
                        break;
                    }
                    case CHitAnalyse.enumHitAnalyseType.emHit:
                    {
                        for (int k = 0; k < this.list_0.Count; k++)
                        {
                            str1 = this.list_0[k].toHtml_Hit(k + 1);
                            str1 = string.Format(str3, str1);
                            str2 = string.Concat(str2, str1);
                        }
                        break;
                    }
                }
                str = str2;
            }
            else
            {
                str = "";
            }
            return str;
        }

        public class CItem
        {
            public IFeatureClass _pClass;

            public int _OID;

            public string _sKind;

            public double _dHorDistance;

            public double _dHorBase;

            public double _dVerDistance;

            public double _dVerBase;

            public CItem()
            {
            }

            public string toHtml_Hit(int idx)
            {
                string str = "";
                string str1 = "<td style=\"height:15px\"> {0} </td> ";
                str = string.Concat(str, string.Format(str1, idx));
                str = string.Concat(str, string.Format(str1, this._OID.ToString()));
                str = string.Concat(str, string.Format(str1, this._sKind));
                str = string.Concat(str, string.Format(str1, this._dHorDistance.ToString("0.000")));
                str = string.Concat(str, string.Format(str1, this._dHorBase.ToString("0.000")));
                str = string.Concat(str, string.Format(str1, this._dVerDistance.ToString("0.000")));
                string str2 = string.Concat(str, string.Format(str1, this._dVerBase.ToString("0.000")));
                return str2;
            }

            public string toHtml_Hor(int idx)
            {
                string str = "";
                string str1 = "<td style=\"height:15px\"> {0} </td> ";
                str = string.Concat(str, string.Format(str1, idx));
                str = string.Concat(str, string.Format(str1, this._OID.ToString()));
                str = string.Concat(str, string.Format(str1, this._sKind));
                str = string.Concat(str, string.Format(str1, this._dHorDistance.ToString("0.000")));
                string str2 = string.Concat(str, string.Format(str1, this._dHorBase.ToString("0.000")));
                return str2;
            }

            public string toHtml_Ver(int idx)
            {
                string str = "";
                string str1 = "<td style=\"height:15px\"> {0} </td> ";
                str = string.Concat(str, string.Format(str1, idx));
                str = string.Concat(str, string.Format(str1, this._OID.ToString()));
                str = string.Concat(str, string.Format(str1, this._sKind));
                str = string.Concat(str, string.Format(str1, this._dVerDistance.ToString("0.000")));
                string str2 = string.Concat(str, string.Format(str1, this._dVerBase.ToString("0.000")));
                return str2;
            }
        }

        private delegate void Delegate1(IFeatureCursor pFeaCursor);

        public enum enumHitAnalyseType
        {
            emHorizontal,
            emVertical,
            emHit
        }
    }
}