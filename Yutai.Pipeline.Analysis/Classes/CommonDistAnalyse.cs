using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CommonDistAnalyse
    {
        private IPipelineConfig ipipeConfig_0;

        public IFeature m_pFeature;

        public DistAnalyseType m_nAnalyseType;

        public IPolyline m_pBaseLine;

        public double m_dBufferRadius;

        public int m_nBaseLineFromID;

        public int m_nBaseLineToID;

        public int m_dDiameter;

        public int m_nHeightFlagBase;

        public int m_dDiameterDst;

        public double m_dPipeWidth;

        public double m_dPipeHeight;

        public int m_nHeightFlagDst;

        public string m_strLayerName;

        public string m_strBuryKind;

        public bool m_bIsSelectLine;

        public ArrayList m_arrDstPipeline;

        public int Diameter
        {
            get
            {
                return this.m_dDiameter;
            }
            set
            {
                this.m_dDiameter = value;
            }
        }

        public IPipelineConfig PipeConfig
        {
            get
            {
                return this.ipipeConfig_0;
            }
            set
            {
                this.ipipeConfig_0 = value;
            }
        }

        public CommonDistAnalyse()
        {
        }

        public void GetBufferLines(double dRadius, IMap pMap)
        {
            this.m_dBufferRadius = dRadius;
            this.method_4(pMap);
        }

        public int GetDiameterFromString(string strVal)
        {
            int num;
            if (strVal == "")
            {
                strVal = "0";
            }
            if ((strVal.IndexOf('x') != -1 ? true : strVal.IndexOf('X') != -1))
            {
                int num1 = strVal.IndexOf('x') + strVal.IndexOf('X') + 1;
                string str = strVal.Substring(0, num1);
                string str1 = strVal.Substring(num1 + 1, strVal.Length - num1 - 1);
                int num2 = Convert.ToInt16(str);
                num = Math.Max(num2, Convert.ToInt16(str1));
            }
            else
            {
                num = Convert.ToInt16(strVal);
            }
            return num;
        }

        public IPolyline GetFlashDstItem(int nFID)
        {
            IPolyline polyline;
            if (this.m_arrDstPipeline.Count != 0)
            {
                int count = this.m_arrDstPipeline.Count;
                IPolyline mPPolyline = null;
                int num = 0;
                while (true)
                {
                    if (num < count)
                    {
                        DstLineItem item = (DstLineItem)this.m_arrDstPipeline[num];
                        if (Convert.ToInt32(item.m_nFID) == nFID)
                        {
                            mPPolyline = item.m_pPolyline;
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                polyline = mPPolyline;
            }
            else
            {
                polyline = null;
            }
            return polyline;
        }

        public bool HighOpAnother(IPolyline pBaseLine, IPolyline pDstLine)
        {
            bool flag;
            IPoint fromPoint = pBaseLine.FromPoint;
            IPoint toPoint = pBaseLine.ToPoint;
            IPoint point = pDstLine.FromPoint;
            IPoint toPoint1 = pDstLine.ToPoint;
            double num = (fromPoint.Z < toPoint.Z ? fromPoint.Z : toPoint.Z);
            double num1 = (fromPoint.Z > toPoint.Z ? fromPoint.Z : toPoint.Z);
            double num2 = (point.Z < toPoint1.Z ? point.Z : toPoint1.Z);
            double num3 = (point.Z > toPoint1.Z ? point.Z : toPoint1.Z);
            if (num > num3)
            {
                flag = true;
            }
            else if (num1 < num2)
            {
                flag = false;
            }
            else if (num1 > num3)
            {
                flag = true;
            }
            else
            {
                flag = (num < num2 ? false : num1 + num > num3 + num2);
            }
            return flag;
        }

        private bool method_0(IEdgeFeature edgeFeature, IEdgeFeature edgeFeature2)
        {
            return (edgeFeature.FromJunctionEID == edgeFeature2.FromJunctionEID || edgeFeature.FromJunctionEID == edgeFeature2.ToJunctionEID || edgeFeature.ToJunctionEID == edgeFeature2.FromJunctionEID ? false : edgeFeature.ToJunctionEID != edgeFeature2.ToJunctionEID);
        }

        private bool method_1(int num, int num2, int num3, int num4)
        {
            return num == -1 || num4 == -1 || (num != num3 && num != num4 && num2 != num3 && num2 != num4);
        }


        private double method_2(IPolyline polyline, IPolyline polyline2)
        {
            IPointCollection pointCollection = (IPointCollection)polyline;
            int pointCount = pointCollection.PointCount;
            double result;
            if (pointCount == 0)
            {
                result = 0.0;
            }
            else
            {
                GPolyLine gPolyLine = new GPolyLine();
                gPolyLine.Clear();
                for (int i = 0; i < pointCount; i++)
                {
                    IPoint point = pointCollection.get_Point(i);
                    double x = point.X;
                    double y = point.Y;
                    double z = point.Z;
                    double m = point.M;
                    gPolyLine.PushBack(new GPoint
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        M = m
                    });
                }
                pointCollection = (IPointCollection)polyline2;
                pointCount = pointCollection.PointCount;
                if (pointCount == 0)
                {
                    result = 0.0;
                }
                else
                {
                    GPolyLine gPolyLine2 = new GPolyLine();
                    gPolyLine2.Clear();
                    for (int j = 0; j < pointCount; j++)
                    {
                        IPoint point2 = pointCollection.get_Point(j);
                        double x2 = point2.X;
                        double y2 = point2.Y;
                        double z2 = point2.Z;
                        double m2 = point2.M;
                        gPolyLine2.PushBack(new GPoint
                        {
                            X = x2,
                            Y = y2,
                            Z = z2,
                            M = m2
                        });
                    }
                    GPoints gPoints = new GPoints();
                    gPoints = gPolyLine.GetInterPtsToPolyLineWithHeight(gPolyLine2);
                    long num = (long)gPoints.Size();
                    double num2 = -1.0;
                    int num3 = 0;
                    while ((long)num3 < num)
                    {
                        GPoint gPoint = gPoints[num3];
                        double num4 = this.method_5(gPoint.M, gPoint.Z);
                        if (num3 == 0)
                        {
                            num2 = num4;
                        }
                        else if (num2 > num4)
                        {
                            num2 = num4;
                        }
                        num3++;
                    }
                    result = num2;
                }
            }
            return result;
        }

        private double method_3(IPolyline polyline, IPolyline polyline2)
        {
            IPointCollection pointCollection = (IPointCollection)polyline;
            int pointCount = pointCollection.PointCount;
            double result;
            if (pointCount == 0)
            {
                result = -1.0;
            }
            else
            {
                double num = 0.0;
                double num2 = 0.0;
                for (int i = 0; i < pointCount; i++)
                {
                    IPoint point = pointCollection.get_Point(i);
                    double z = point.Z;
                    if (i == 0)
                    {
                        num = (num2 = z);
                    }
                    else
                    {
                        num = ((z > num) ? z : num);
                        num2 = ((z < num2) ? z : num2);
                    }
                }
                pointCollection = (IPointCollection)polyline2;
                pointCount = pointCollection.PointCount;
                if (pointCount == 0)
                {
                    result = -1.0;
                }
                else
                {
                    double num3 = 0.0;
                    double num4 = 0.0;
                    for (int j = 0; j < pointCount; j++)
                    {
                        IPoint point2 = pointCollection.get_Point(j);
                        double z2 = point2.Z;
                        if (j == 0)
                        {
                            num3 = (num4 = z2);
                        }
                        else
                        {
                            num3 = ((z2 > num3) ? z2 : num3);
                            num4 = ((z2 < num4) ? z2 : num4);
                        }
                    }
                    double val = this.method_5(num, num3);
                    double val2 = this.method_5(num, num4);
                    double val3 = this.method_5(num2, num3);
                    double val4 = this.method_5(num2, num4);
                    result = Math.Min(Math.Min(val, val2), Math.Min(val3, val4));
                }
            }
            return result;
        }

        private void method_4(IMap map)
        {
            this.m_arrDstPipeline = new ArrayList();
            this.m_arrDstPipeline.Clear();
            CommonUtils.ThrougAllLayer(map, new CommonUtils.DealLayer(this.method_7));
        }

        private double method_5(double num, double num2)
        {
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            switch (this.m_nHeightFlagBase)
            {
                case 0:
                    num4 = 0.001 * (double)this.m_dDiameter;
                    goto IL_91;
                case 2:
                    num3 = 0.001 * (double)this.m_dDiameter;
                    goto IL_91;
            }
            num3 = 0.0005 * (double)this.m_dDiameter;
            num4 = 0.0005 * (double)this.m_dDiameter;
            IL_91:
            switch (this.m_nHeightFlagDst)
            {
                case 0:
                    num6 = 0.001 * (double)this.m_dDiameter;
                    goto IL_FA;
                case 2:
                    num5 = 0.001 * (double)this.m_dDiameter;
                    goto IL_FA;
            }
            num5 = 0.0005 * (double)this.m_dDiameter;
            num6 = 0.0005 * (double)this.m_dDiameter;
            IL_FA:
            double result;
            if (num >= num2)
            {
                result = Math.Abs(num - num2 - num4 - num5);
            }
            else
            {
                result = Math.Abs(num2 - num - num3 - num6);
            }
            return result;
        }

        private string method_6(object obj)
        {
            return ((Convert.IsDBNull(obj) ? false : obj != null) ? obj.ToString() : "");
        }

        private void method_7(ILayer layer)
        {
            string str;
            string str1;
            string str2;
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.Visible)
                {
                    IGeometry geometry = ((ITopologicalOperator)this.m_pBaseLine).Buffer(this.m_dBufferRadius);
                    ISpatialFilter spatialFilterClass = new SpatialFilter();
                    spatialFilterClass.Geometry=(geometry);
                    spatialFilterClass.SpatialRel=(esriSpatialRelEnum) (1);
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    IBasicLayerInfo lineConfig = PipeConfig.GetBasicLayerInfo(featureClass.AliasName) as IBasicLayerInfo;
                    if (lineConfig != null)
                    {
                        IFeatureCursor featureCursor = featureClass.Search(spatialFilterClass, false);
                        IFeature feature = featureCursor.NextFeature();
                        while (feature != null)
                        {
                            if ((!feature.HasOID || feature == null ? false : feature.FeatureType == (esriFeatureType) 8))
                            {
                                IGeometry shape = feature.Shape;
                                int num = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                                str = (num == -1 ? "" : feature.get_Value(num).ToString());
                                num = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                                str1 = (num == -1 ? "" : feature.get_Value(num).ToString());
                                string str3 = "";
                                if (str != "")
                                {
                                    str3 = str;
                                }
                                if (str1 != "")
                                {
                                    str3 = str1;
                                }
                                this.m_dDiameterDst = this.GetDiameterFromString(str3.Trim());
                                if (shape.GeometryType == (esriGeometryType) 3)
                                {
                                    double num1 = ((IProximityOperator)this.m_pBaseLine).ReturnDistance(shape);
                                    if (num1 > 1E-07)
                                    {
                                        num1 = Math.Abs(num1 - 0.0005 * (double)this.m_dDiameter - 0.0005 * (double)this.m_dDiameterDst);
                                    }
                                    if (!(this.m_nAnalyseType != DistAnalyseType.emHrzDist ? true : num1 >= 1E-08))
                                    {
                                        feature = featureCursor.NextFeature();
                                    }
                                    else if ((this.m_nAnalyseType != DistAnalyseType.emVerDist ? true : num1 <= 1E-08))
                                    {
                                        double num2 = 0;
                                        //this.m_nHeightFlagBase = this.PipeConfig.getLineConfig_HeightFlag(this.m_strLayerName);
                                        //this.m_nHeightFlagDst = this.PipeConfig.getLineConfig_HeightFlag(feature.Class.AliasName);

                                        this.m_nHeightFlagBase = (int) lineConfig.HeightType;
                                        this.m_nHeightFlagDst = (int)lineConfig.HeightType;
                                        if ((this.m_nAnalyseType == DistAnalyseType.emVerDist ? true : this.m_nAnalyseType == DistAnalyseType.emHitDist))
                                        {
                                            num2 = (num1 >= 1E-07 ? this.method_3(this.m_pBaseLine, (IPolyline)shape) : this.method_2(this.m_pBaseLine, (IPolyline)shape));
                                        }
                                        if (feature.FeatureType == (esriFeatureType) 8)
                                        {
                                            IEdgeFeature edgeFeature = (IEdgeFeature)feature;
                                            if (edgeFeature != null)
                                            {
                                                int fromJunctionEID = edgeFeature.FromJunctionEID;
                                                int toJunctionEID = edgeFeature.ToJunctionEID;
                                                string str4 =
                                                    lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ);
                                                string str5 = "";
                                                int num3 = feature.Fields.FindField(str4);
                                                if (num3 != -1)
                                                {
                                                    object value = feature.get_Value(num3);
                                                    str5 = ((value == null ? false : !Convert.IsDBNull(value)) ? value.ToString() : "");
                                                }
                                                // int num4 = feature.Fields.FindField("埋设方式");
                                                int num4 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.MSFS));
                                                str2 = (num4 == -1 ? "" : this.method_6(feature.get_Value(num4)));
                                                if (this.method_1(this.m_nBaseLineFromID, this.m_nBaseLineToID, fromJunctionEID, toJunctionEID))
                                                {
                                                    DstLineItem dstLineItem = new DstLineItem()
                                                    {
                                                        m_pPolyline = CommonUtils.GetPolylineDeepCopy((IPolyline)shape),
                                                        m_strLayerName = str5,
                                                        m_nFID = (int)feature.get_Value(0)
                                                    };
                                                    if (num1 <= 1E-07)
                                                    {
                                                        dstLineItem.m_dResultDistH = 0f;
                                                    }
                                                    else
                                                    {
                                                        float single = Math.Abs((float)num1 - (float)this.m_dDiameter / 2000f);
                                                        dstLineItem.m_dResultDistH = CommonUtils.GetFloatThreePoint(single);
                                                    }
                                                    if (num2 <= 1E-07)
                                                    {
                                                        dstLineItem.m_dResultDistV = 0f;
                                                    }
                                                    else
                                                    {
                                                        float single1 = Math.Abs((float)num2);
                                                        dstLineItem.m_dResultDistV = CommonUtils.GetFloatThreePoint(single1);
                                                    }
                                                    dstLineItem.m_nDstLineFromID = fromJunctionEID;
                                                    dstLineItem.m_nDstLineToID = toJunctionEID;
                                                    dstLineItem.m_dPipeWidth = 0;
                                                    dstLineItem.m_dPipeHeight = 0;
                                                    if (str != "")
                                                    {
                                                        dstLineItem.m_strPipeWidthAndHeight = str3;
                                                    }
                                                    if (str1 != "")
                                                    {
                                                        dstLineItem.m_strPipeWidthAndHeight = str3;
                                                    }
                                                    if (!this.HighOpAnother(this.m_pBaseLine, dstLineItem.m_pPolyline))
                                                    {
                                                        dstLineItem.m_dTolDistH = CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(ipipeConfig_0,CommonUtils.GetSmpClassName(feature.Class.AliasName), CommonUtils.GetSmpClassName(this.m_strLayerName), feature, this.m_pFeature);
                                                        dstLineItem.m_dTolDistV = CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(ipipeConfig_0,CommonUtils.GetSmpClassName(feature.Class.AliasName), CommonUtils.GetSmpClassName(this.m_strLayerName), str2, this.m_strBuryKind);
                                                    }
                                                    else
                                                    {
                                                        dstLineItem.m_dTolDistH = CommonUtils.GetPipeLineAlarmHrzDistByFeatureClassName2(ipipeConfig_0,CommonUtils.GetSmpClassName(this.m_strLayerName), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.m_pFeature, feature);
                                                        dstLineItem.m_dTolDistV = CommonUtils.GetPipeLineAlarmVerDistByFeatureClassName(ipipeConfig_0,CommonUtils.GetSmpClassName(this.m_strLayerName), CommonUtils.GetSmpClassName(feature.Class.AliasName), this.m_strBuryKind, str2);
                                                    }
                                                    this.m_arrDstPipeline.Add(dstLineItem);
                                                }
                                                feature = featureCursor.NextFeature();
                                            }
                                            else
                                            {
                                                feature = featureCursor.NextFeature();
                                            }
                                        }
                                        else
                                        {
                                            feature = featureCursor.NextFeature();
                                        }
                                    }
                                    else
                                    {
                                        feature = featureCursor.NextFeature();
                                    }
                                }
                                else
                                {
                                    feature = featureCursor.NextFeature();
                                }
                            }
                            else
                            {
                                feature = featureCursor.NextFeature();
                            }
                        }
                    }
                }
            }
        }

        public void RefreshResultGrid(DataGridView dgv)
        {
            int count = this.m_arrDstPipeline.Count;
            dgv.Rows.Clear();
            for (int i = 0; i < count; i++)
            {
                DstLineItem item = (DstLineItem)this.m_arrDstPipeline[i];
                dgv.Rows.Add(new object[] { "" });
                dgv[0, i].Value = i + 1;
                dgv[1, i].Value = item.m_strLayerName;
                dgv[2, i].Value = item.m_nFID;
                DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.Red
                };
                if (this.m_nAnalyseType == DistAnalyseType.emHrzDist)
                {
                    dgv[3, i].Value = item.m_dResultDistH;
                    dgv[4, i].Value = item.m_dTolDistH;
                    if ((item.m_dTolDistH == -1f ? false : item.m_dResultDistH < item.m_dTolDistH))
                    {
                        dgv[3, i].Style = dataGridViewCellStyle;
                    }
                }
                if (this.m_nAnalyseType == DistAnalyseType.emVerDist)
                {
                    dgv[3, i].Value = item.m_dResultDistV;
                    dgv[4, i].Value = item.m_dTolDistV;
                    if ((item.m_dTolDistV == -1f ? false : item.m_dResultDistV < item.m_dTolDistV))
                    {
                        dgv[3, i].Style = dataGridViewCellStyle;
                    }
                }
                if (this.m_nAnalyseType == DistAnalyseType.emHitDist)
                {
                    dgv[3, i].Value = item.m_dResultDistH;
                    dgv[4, i].Value = item.m_dTolDistH;
                    dgv[5, i].Value = item.m_dResultDistV;
                    dgv[6, i].Value = item.m_dTolDistV;
                    dgv[7, i].Value = item.m_strPipeWidthAndHeight;
                    if ((item.m_dTolDistH == -1f ? false : item.m_dResultDistH < item.m_dTolDistH))
                    {
                        dgv[3, i].Style = dataGridViewCellStyle;
                    }
                    if ((item.m_dTolDistV == -1f ? false : item.m_dResultDistV < item.m_dTolDistV))
                    {
                        dgv[5, i].Style = dataGridViewCellStyle;
                    }
                }
            }
        }
    }
}