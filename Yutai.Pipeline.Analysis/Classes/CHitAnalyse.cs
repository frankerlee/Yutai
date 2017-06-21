using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Helpers;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CHitAnalyse : CBase
    {
        private CHitAnalyse.enumHitAnalyseType enumHitAnalyseType_0 = CHitAnalyse.enumHitAnalyseType.emHit;

        private IPolyline ipolyline_0;

        private IPolyline ipolyline_1;

        private IFeature ifeature_0;

        private int int_0;

        private double double_0;

        private double double_1;

        private int int_1;

        private IFeatureLayer ifeatureLayer_0;

        private IFeatureClass ifeatureClass_0;

        private double double_2 = 10;

        private List<CHitAnalyse.CItem> list_0 = new List<CHitAnalyse.CItem>();

        private ESRI.ArcGIS.Carto.IMap imap_0;

        private string string_0 = "管线性质";

        private string string_1 = "管径";

        private string string_2 = "断面尺寸";

        private char[] char_0 = new char[] { 'x', 'X', 'Х' };

        public IPolyline BaseLine
        {
            set
            {
                this.BaseLine = value;
            }
        }

        public int BaseLine_OID
        {
            set
            {
                this.int_0 = value;
                this.ipolyline_0 = null;
                IFeature feature = this.ifeatureClass_0.GetFeature(this.int_0);
                if (feature != null)
                {
                    int num = feature.Fields.FindField(this.string_1);
                    int num1 = feature.Fields.FindField(this.string_2);
                    if (feature.Fields.FindField(this.string_0) >= 0)
                    {
                        if ((num >= 0 ? false : num1 < 0))
                        {
                            return;
                        }
                        this.ipolyline_0 = (IPolyline)feature.Shape;
                        this.double_1 = this.method_6(feature, num, num1, out this.double_0);
                        if (this.double_1 < 1)
                        {
                            this.double_1 = 10;
                        }
                        if (this.double_0 < 1)
                        {
                            this.double_0 = this.double_1;
                        }
                        this.int_1 = this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName));
                    }
                    else
                    {
                        return;
                    }
                }
                this.ifeature_0 = feature;
            }
        }

        public double BufferDistance
        {
            set
            {
                this.double_2 = value;
            }
        }

        public ESRI.ArcGIS.Carto.IMap IMap
        {
            get
            {
                return this.imap_0;
            }
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
                this.string_0 = this.m_app.PipeConfig.get_Kind();
                this.string_1 = this.m_app.PipeConfig.get_Diameter();
                this.string_2 = this.m_app.PipeConfig.get_Section_Size();
            }
        }

        public List<CHitAnalyse.CItem> Items
        {
            get
            {
                return this.list_0;
            }
        }

        public IFeatureLayer PipeLayer
        {
            set
            {
                this.ifeatureClass_0 = null;
                this.ifeatureLayer_0 = value;
                if (this.ifeatureLayer_0 != null)
                {
                    this.ifeatureClass_0 = this.ifeatureLayer_0.FeatureClass;
                }
            }
        }

        public IFeatureClass PipeLayer_Class
        {
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public CHitAnalyse()
        {
        }

        public void Analyse_Hit()
        {
            this.enumHitAnalyseType_0 = CHitAnalyse.enumHitAnalyseType.emHit;
            this.method_1(new CHitAnalyse.Delegate1(this.Analyse_Hit));
        }

        public void Analyse_Hit(IFeatureCursor pFeaCursor)
        {
            double num;
            double double0 = this.double_0 * 0.0005;
            double double1 = this.double_1 * 0.0005;
            ITopologicalOperator ipolyline1 = (ITopologicalOperator)this.ipolyline_1;
            IProximityOperator proximityOperator = (IProximityOperator)this.ipolyline_1;
            IPointCollection ipolyline0 = (IPointCollection)this.ipolyline_0;
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
            if (this.int_1 == 0)
            {
                z = z - double0;
                num1 = num1 - double0;
            }
            else if (2 == this.int_1)
            {
                z = z + double0;
                num1 = num1 + double0;
            }
            IFeature feature = pFeaCursor.NextFeature();
            int num2 = -1;
            int num3 = -1;
            int num4 = -1;
            if (feature != null)
            {
                num2 = feature.Fields.FindField(this.string_1);
                num3 = feature.Fields.FindField(this.string_2);
                num4 = feature.Fields.FindField(this.string_0);
            }
            if (num4 >= 0)
            {
                if ((num2 >= 0 ? true : num3 >= 0))
                {
                    int lineConfigHeightFlag = this.m_app.PipeConfig.getLineConfig_HeightFlag(CommonUtils.GetSmpClassName(feature.Class.AliasName));
                    while (feature != null)
                    {
                        IPolyline shape = feature.Shape as IPolyline;
                        IPolyline polyline = this.method_0(shape);
                        double num5 = 0;
                        double num6 = this.method_6(feature, num2, num3, out num5);
                        if (num6 < 10)
                        {
                            num6 = 10;
                        }
                        num6 = num6 * 0.0005;
                        if (num5 < 10)
                        {
                            num5 = 10;
                        }
                        num5 = num5 * 0.0005;
                        IPoint point = null;
                        IGeometry geometry = ipolyline1.Intersect(polyline, 1);
                        if (geometry != null)
                        {
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
                        }
                        if (point == null)
                        {
                            CHitAnalyse.CItem cItem = new CHitAnalyse.CItem();
                            string str = feature.get_Value(num4).ToString();
                            cItem._OID = feature.OID;
                            cItem._sKind = str;
                            cItem._pClass = (IFeatureClass)feature.Class;
                            num = proximityOperator.ReturnDistance(polyline);
                            num = num - double1 - num6;
                            if (num >= 0.001)
                            {
                                cItem._dHorDistance = num;
                                cItem._dHorBase = 0;
                                IPointCollection pointCollection1 = (IPointCollection)shape;
                                double z2 = pointCollection1.get_Point(0).Z - pointCollection1.get_Point(0).M;
                                double num7 = z2;
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
                                cItem._dHorBase = (double)CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.ifeature_0, feature);
                                string str1 = this.method_2(feature);
                                string str2 = this.method_2(this.ifeature_0);
                                cItem._dVerBase = (double)CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName), CommonUtils.GetSmpClassName(feature.Class.AliasName), str2, str1);
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
                            int num8 = this.method_3(this.ipolyline_1, point);
                            double num9 = this.method_4(this.ipolyline_0, point, num8);
                            if (this.int_1 == 0)
                            {
                                num9 = num9 - double0;
                            }
                            else if (2 == this.int_1)
                            {
                                num9 = num9 + double0;
                            }
                            int num10 = this.method_3(polyline, point);
                            double num11 = this.method_5(shape, point, num10);
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
                                    _pClass = (IFeatureClass)feature.Class
                                };
                                pipeLineAlarmHrzDistByFeatureClassName2._dHorBase = (double)CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.ifeature_0, feature);
                                string str4 = this.method_2(feature);
                                string str5 = this.method_2(this.ifeature_0);
                                pipeLineAlarmHrzDistByFeatureClassName2._dVerBase = (double)CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName), CommonUtils.GetSmpClassName(feature.Class.AliasName), str5, str4);
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
            double double1 = this.double_1 * 0.0005;
            IProximityOperator ipolyline1 = (IProximityOperator)this.ipolyline_1;
            IFeature feature = pFeaCursor.NextFeature();
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
                        IPolyline polyline = this.method_0(feature.Shape as IPolyline);
                        double num3 = 0;
                        double num4 = this.method_6(feature, num, num1, out num3);
                        if (num4 < 10)
                        {
                            num4 = 10;
                        }
                        num4 = num4 * 0.0005;
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
                                _pClass = (IFeatureClass)feature.Class
                            };
                            cItem._dHorBase = (double)CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.ifeature_0, feature);
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
            double double0 = this.double_0 * 0.0005;
            ITopologicalOperator ipolyline1 = (ITopologicalOperator)this.ipolyline_1;
            IFeature feature = pFeaCursor.NextFeature();
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
                        IPolyline polyline = this.method_0(shape);
                        double num3 = 0;
                        this.method_6(feature, num, num1, out num3);
                        if (num3 < 10)
                        {
                            num3 = 10;
                        }
                        num3 = num3 * 0.0005;
                        IGeometry geometry = ipolyline1.Intersect(polyline, 1);
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
                                int num4 = this.method_3(this.ipolyline_1, point);
                                double num5 = this.method_5(this.ipolyline_0, point, num4);
                                if (this.int_1 == 0)
                                {
                                    num5 = num5 - double0;
                                }
                                else if (2 == this.int_1)
                                {
                                    num5 = num5 + double0;
                                }
                                int num6 = this.method_3(polyline, point);
                                double num7 = this.method_5(shape, point, num6);
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
                                        _pClass = (IFeatureClass)feature.Class
                                    };
                                    string str1 = this.method_2(feature);
                                    string str2 = this.method_2(this.ifeature_0);
                                    cItem._dVerBase = (double)CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(CommonUtils.GetSmpClassName(this.ifeatureClass_0.AliasName), CommonUtils.GetSmpClassName(feature.Class.AliasName), str2, str1);
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

        private IPolyline method_0(IPolyline polyline)
        {
            object missing = Type.Missing;
            IPolyline polylineClass = new Polyline() as IPolyline;
            IPointCollection pointCollection = (IPointCollection)polylineClass;
            IPointCollection pointCollection1 = (IPointCollection)polyline;
            for (int i = 0; i <= pointCollection1.PointCount - 1; i++)
            {
                IPoint point = pointCollection1.get_Point(i);
                IPoint pointClass = new Point();
                pointClass.X=(point.X);
                pointClass.Y=(point.Y);
                pointClass.Z=(0);
                pointCollection.AddPoint(pointClass, ref missing, ref missing);
            }
            return polylineClass;
        }

        private void method_1(CHitAnalyse.Delegate1 delegate1)
        {
            CommonUtils.AppContext = this.m_app;
            this.list_0.Clear();
            this.ipolyline_1 = this.method_0(this.ipolyline_0);
            IGeometry geometry = ((ITopologicalOperator)this.ipolyline_0).Buffer(this.double_2);
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            spatialFilterClass.Geometry=(geometry);
            spatialFilterClass.SpatialRel=(esriSpatialRelEnum) (1);
            if (this.m_listLayers.Count >= 1)
            {
                foreach (IFeatureLayer mListLayer in this.m_listLayers)
                {
                    if (!mListLayer.Visible)
                    {
                        continue;
                    }
                    IFeatureClass featureClass = mListLayer.FeatureClass;
                    if ((featureClass.AliasName == this.ifeatureClass_0.AliasName ? true : featureClass.ShapeType!= (esriGeometryType) 3))
                    {
                        continue;
                    }
                    spatialFilterClass.GeometryField=featureClass.ShapeFieldName;
                    delegate1(featureClass.Search(spatialFilterClass, false));
                }
            }
        }

        private string method_2(IFeature feature)
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

        private int method_3(IPolyline polyline, IPoint point)
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

        private double method_4(IPolyline polyline, IPoint point, int num)
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

        private double method_5(IPolyline polyline, IPoint point, int num)
        {
            double z;
            IPointCollection pointCollection = (IPointCollection)polyline;
            IPoint point1 = pointCollection.get_Point(num);
            IPoint point2 = pointCollection.get_Point(num + 1);
            double x = point2.X - point1.X;
            double y = point2.Y - point1.Y;
            double z1 = point2.Z - point2.M - point1.Z + point1.M;
            double x1 = point.X - point1.X;
            double y1 = point.Y - point1.Y;
            double num1 = Math.Sqrt(x * x + y * y);
            double num2 = Math.Sqrt(x1 * x1 + y1 * y1);
            if (num1 >= 0.001)
            {
                double num3 = num2 / num1;
                z = num3 * z1 + point1.Z - point1.M;
            }
            else
            {
                z = point1.Z - point1.M;
            }
            return z;
        }

        private double method_6(IFeature feature, int num, int num2, out double double_3)
        {
            double_3 = 0.0;
            double num3 = 0.0;
            if (num > 0)
            {
                object value = feature.get_Value(num);
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
                num3 = this.method_7(feature, num2, out double_3);
            }
            else
            {
                double_3 = num3;
            }
            return num3;
        }


        private double method_7(IFeature feature, int num, out double double_3)
        {
            double num1 = 0;
            double_3 = 0;
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
                    string[] strArrays = str.Split(this.char_0);
                    if (strArrays[0].ToString().Trim() != "")
                    {
                        num1 = Convert.ToDouble(strArrays[0]);
                        double_3 = Convert.ToDouble(strArrays[1]);
                    }
                    else
                    {
                        num1 = 0;
                        double_3 = 0;
                    }
                }
            }
            return num1;
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
                if ((int)strArrays.Length <= 1)
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
            this.ifeature_0 = null;
            this.ifeatureClass_0 = pClass;
            this.RegexPipeSize(sSize, out this.double_1, out this.double_0);
            if (this.double_1 < 10)
            {
                this.double_1 = 10;
            }
            if (this.double_0 < 10)
            {
                this.double_0 = 10;
            }
            this.ipolyline_0 = pLine;
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
            this.ipolyline_0 = pLine;
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