using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class TranSection : Section
    {
        public ArrayList m_arrPipePoints = new ArrayList();

        public ArrayList m_arrPipePointsDraw = new ArrayList();

        public TranSection(object objForm, IAppContext pApp) : base(objForm, pApp)
        {
        }

        public override void Paint()
        {
            base.Paint();
            this.m_pSectionDisp.DrawTranSection(this.m_arrPipePointsDraw);
        }

        public override void PrintPage(object objPrint)
        {
            base.PrintPage(objPrint);
            this.m_pSectionDisp.DrawTranSection(this.m_arrPipePointsDraw);
        }

        public override void SectionInfo(out ArrayList pArrInfo)
        {
            pArrInfo = new ArrayList();
            int count = this.m_arrPipePoints.Count;
            if (this.m_nSelectIndex != -1)
            {
                int num = 0;
                for (int i = 0; i < count; i++)
                {
                    PipePoint pipePoint = (PipePoint)this.m_arrPipePoints[i];
                    if (pipePoint.PointType == PipePoint.SectionPointType.sptPipe)
                    {
                        if (num == this.m_nSelectIndex)
                        {
                            SectionInfoStore sectionInfoStore;
                            sectionInfoStore.strField = "数据集名称";
                            sectionInfoStore.strVal = pipePoint.bstrDatasetName;
                            pArrInfo.Add(sectionInfoStore);
                            SectionInfoStore sectionInfoStore2;
                            sectionInfoStore2.strField = "ID";
                            sectionInfoStore2.strVal = pipePoint.nID.ToString();
                            pArrInfo.Add(sectionInfoStore2);
                            SectionInfoStore sectionInfoStore3;
                            sectionInfoStore3.strField = "材质";
                            sectionInfoStore3.strVal = pipePoint.strMaterial.Trim();
                            pArrInfo.Add(sectionInfoStore3);
                            SectionInfoStore sectionInfoStore4;
                            sectionInfoStore4.strField = "规格";
                            sectionInfoStore4.strVal = pipePoint.strPipeWidthHeight.Trim();
                            pArrInfo.Add(sectionInfoStore4);
                            SectionInfoStore sectionInfoStore5;
                            sectionInfoStore5.strField = "横坐标";
                            sectionInfoStore5.strVal = pipePoint.x.ToString("f3");
                            pArrInfo.Add(sectionInfoStore5);
                            SectionInfoStore sectionInfoStore6;
                            sectionInfoStore6.strField = "纵坐标";
                            sectionInfoStore6.strVal = pipePoint.y.ToString("f3");
                            pArrInfo.Add(sectionInfoStore6);
                            SectionInfoStore sectionInfoStore7;
                            sectionInfoStore7.strField = "管线高程";
                            sectionInfoStore7.strVal = pipePoint.z.ToString("f3");
                            pArrInfo.Add(sectionInfoStore7);
                            SectionInfoStore sectionInfoStore8;
                            sectionInfoStore8.strField = "地面高程";
                            sectionInfoStore8.strVal = pipePoint.m.ToString("f3");
                            pArrInfo.Add(sectionInfoStore8);
                            break;
                        }
                        num++;
                    }
                }
            }
        }

        public override void SaveSectionFile(string strFileName)
        {
            int count = this.m_arrPipePoints.Count;
            XmlTextWriter xmlTextWriter = new XmlTextWriter(strFileName, null);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteComment("SectionSaveFile");
            xmlTextWriter.WriteStartElement("TranSectionAnalyse");
            xmlTextWriter.WriteAttributeString("SectionType", "TranSectionType");
            string title = this.m_pSectionDisp.Title;
            string roadName = this.m_pSectionDisp.RoadName;
            string sectionNo = this.m_pSectionDisp.SectionNo;
            xmlTextWriter.WriteAttributeString("Title", title);
            xmlTextWriter.WriteAttributeString("RoadName", roadName);
            xmlTextWriter.WriteAttributeString("SectionNo", sectionNo);
            xmlTextWriter.WriteStartElement("PipePoints");
            xmlTextWriter.WriteAttributeString("PipePointCount", count.ToString());
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)this.m_arrPipePoints[i];
                xmlTextWriter.WriteStartElement("PipePoint");
                xmlTextWriter.WriteAttributeString("PointType", ((int)pipePoint.PointType).ToString());
                xmlTextWriter.WriteAttributeString("X", pipePoint.x.ToString("f3"));
                xmlTextWriter.WriteAttributeString("Y", pipePoint.y.ToString("f3"));
                xmlTextWriter.WriteAttributeString("Z", pipePoint.z.ToString("f3"));
                xmlTextWriter.WriteAttributeString("M", pipePoint.m.ToString("f3"));
                xmlTextWriter.WriteAttributeString("DatasetName", pipePoint.bstrDatasetName);
                xmlTextWriter.WriteAttributeString("ID", pipePoint.nID.ToString());
                xmlTextWriter.WriteAttributeString("PipePointKind", pipePoint.bstrPointKind);
                xmlTextWriter.WriteAttributeString("Material", pipePoint.strMaterial);
                xmlTextWriter.WriteAttributeString("PipeWidthAndHeight", pipePoint.strPipeWidthHeight);
                xmlTextWriter.WriteAttributeString("Red", pipePoint.Red.ToString());
                xmlTextWriter.WriteAttributeString("Green", pipePoint.Green.ToString());
                xmlTextWriter.WriteAttributeString("Blue", pipePoint.Blue.ToString());
                xmlTextWriter.WriteEndElement();
            }
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
        }

        public override void OpenSectionFile(string strFileName)
        {
            XmlReader xmlReader = XmlReader.Create(strFileName);
            xmlReader.ReadToFollowing("TranSectionAnalyse");
            string attribute = xmlReader.GetAttribute("SectionType");
            if (attribute != "TranSectionType")
            {
                MessageBox.Show("不是横断面保存文件！");
            }
            else
            {
                this.m_pSectionDisp.Title = xmlReader.GetAttribute("Title");
                this.m_pSectionDisp.RoadName = xmlReader.GetAttribute("RoadName");
                this.m_pSectionDisp.SectionNo = xmlReader.GetAttribute("SectionNo");
                xmlReader.ReadToFollowing("PipePoints");
                int num = Convert.ToInt32(xmlReader.GetAttribute("PipePointCount"));
                this.m_arrPipePoints.Clear();
                for (int i = 0; i < num; i++)
                {
                    xmlReader.ReadToFollowing("PipePoint");
                    PipePoint pipePoint = new PipePoint();
                    PipePoint.SectionPointType pointType = (PipePoint.SectionPointType)Convert.ToInt32(xmlReader.GetAttribute("PointType"));
                    pipePoint.PointType = pointType;
                    pipePoint.nID = Convert.ToInt32(xmlReader.GetAttribute("ID"));
                    pipePoint.x = Convert.ToDouble(xmlReader.GetAttribute("X"));
                    pipePoint.y = Convert.ToDouble(xmlReader.GetAttribute("Y"));
                    pipePoint.z = Convert.ToDouble(xmlReader.GetAttribute("Z"));
                    pipePoint.m = Convert.ToDouble(xmlReader.GetAttribute("M"));
                    pipePoint.bstrDatasetName = xmlReader.GetAttribute("DatasetName");
                    pipePoint.nID = Convert.ToInt32(xmlReader.GetAttribute("ID"));
                    pipePoint.strMaterial = xmlReader.GetAttribute("Material");
                    pipePoint.strPipeWidthHeight = xmlReader.GetAttribute("PipeWidthAndHeight");
                    pipePoint.Red = Convert.ToInt32(xmlReader.GetAttribute("Red"));
                    pipePoint.Green = Convert.ToInt32(xmlReader.GetAttribute("Green"));
                    pipePoint.Blue = Convert.ToInt32(xmlReader.GetAttribute("Blue"));
                    this.m_arrPipePoints.Add(pipePoint);
                }
                this.method_5(this.m_arrPipePointsDraw, this.m_arrPipePoints);
                this.method_4();
                this.method_7(this.m_arrPipePointsDraw);
                base.Paint();
            }
        }

        public IPolyline PolygonToPolyline(IPolygon pPolygon)
        {
            
            IGeometryCollection geometryCollection = new Polyline() as IGeometryCollection;
            IClone clone = (IClone)pPolygon;
            IGeometryCollection geometryCollection2 = (IGeometryCollection)clone.Clone();
            object missing = Type.Missing;
            for (int i = 0; i < geometryCollection2.GeometryCount; i++)
            {
                ISegmentCollection segmentCollection = new Path() as ISegmentCollection;
                segmentCollection.AddSegmentCollection((ISegmentCollection)geometryCollection2.get_Geometry(i));
                geometryCollection.AddGeometry((IGeometry)segmentCollection, ref missing, ref missing);
            }
            return geometryCollection as IPolyline;
        }

        public override void GetSelectedData()
        {
            this.method_3();
            IMap map = this.m_context.FocusMap;
            ITopologicalOperator topologicalOperator = (ITopologicalOperator)this.m_pBaseLine;
            IGeometry geometry = topologicalOperator.Buffer(0.0);
            map.ClearSelection();
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(geometry, selectionEnvironment, false);
            IEnumFeature enumFeature = (IEnumFeature)map.FeatureSelection;
            IFeature feature = enumFeature.Next();
            while (feature != null)
            {
                string smpClassName = CommonUtils.GetSmpClassName(feature.Class.AliasName);
                if (!base.PipeConfig.IsPipeLine(feature.Class.AliasName) && !smpClassName.ToUpper().Contains("JT_JT_L") && !smpClassName.ToUpper().Contains("SY_ZX_L") && !smpClassName.ToUpper().Contains("ZB_LD_R"))
                {
                    feature = enumFeature.Next();
                }
                else
                {
                    IGeometry shape = feature.Shape;
                    if (shape.GeometryType != (esriGeometryType) 3 && shape.GeometryType != (esriGeometryType) 4)
                    {
                        feature = enumFeature.Next();
                    }
                    else
                    {
                        IPolyline polyline;
                        if (shape.GeometryType == (esriGeometryType) 4)
                        {
                            polyline = this.PolygonToPolyline((IPolygon)shape);
                        }
                        else
                        {
                            polyline = (IPolyline)shape;
                        }
                        GPoints gPoints = this.method_6(this.m_pBaseLine, polyline);
                        string text = "管线性质";
                        string bstrDatasetName = "";
                        int num = feature.Fields.FindField(text);
                        if (num != -1)
                        {
                            object obj = feature.get_Value(num);
                            if (obj == null || Convert.IsDBNull(obj))
                            {
                                bstrDatasetName = "";
                            }
                            else
                            {
                                bstrDatasetName = obj.ToString();
                            }
                        }
                        if (gPoints == null)
                        {
                            feature = enumFeature.Next();
                        }
                        else
                        {
                            int num2 = gPoints.Size();
                            for (int i = 0; i < num2; i++)
                            {
                                GPoint gPoint = gPoints[i];
                                PipePoint pipePoint = new PipePoint();
                                if (smpClassName.ToUpper().Contains("JT_JT_L"))
                                {
                                    pipePoint.PointType = PipePoint.SectionPointType.sptRoadBorder;
                                }
                                else if (smpClassName.ToUpper().Contains("SY_ZX_L"))
                                {
                                    pipePoint.PointType = PipePoint.SectionPointType.sptMidRoadLine;
                                }
                                else if (smpClassName.ToUpper().Contains("ZB_LD_R"))
                                {
                                    pipePoint.PointType = PipePoint.SectionPointType.sptMidGreen;
                                }
                                else
                                {
                                    pipePoint.PointType = PipePoint.SectionPointType.sptPipe;
                                }
                                pipePoint.x = gPoint.X;
                                pipePoint.y = gPoint.Y;
                                pipePoint.z = gPoint.Z;
                                pipePoint.m = gPoint.M;
                                pipePoint.bstrDatasetName = bstrDatasetName;
                                int num3 = feature.Fields.FindField(base.PipeConfig.get_Material());
                                if (num3 == -1)
                                {
                                    pipePoint.strMaterial = "";
                                }
                                else
                                {
                                    object obj2 = feature.get_Value(num3);
                                    if (obj2 != null)
                                    {
                                        pipePoint.strMaterial = feature.get_Value(num3).ToString();
                                    }
                                    else
                                    {
                                        pipePoint.strMaterial = "";
                                    }
                                }
                                num3 = feature.Fields.FindField(base.PipeConfig.get_Diameter());
                                string text2;
                                if (num3 != -1 && feature.get_Value(num3) != null)
                                {
                                    text2 = feature.get_Value(num3).ToString();
                                }
                                else
                                {
                                    text2 = "";
                                }
                                num3 = feature.Fields.FindField(base.PipeConfig.get_Section_Size());
                                string text3;
                                if (num3 != -1 && feature.get_Value(num3) != null)
                                {
                                    text3 = feature.get_Value(num3).ToString();
                                }
                                else
                                {
                                    text3 = "";
                                }
                                if (text2 != "")
                                {
                                    pipePoint.strPipeWidthHeight = text2;
                                }
                                if (text3 != "")
                                {
                                    pipePoint.strPipeWidthHeight = text3;
                                }
                                Color featureColor = CommonUtils.GetFeatureColor(map, feature.Class.AliasName, feature);
                                pipePoint.Red = (int)featureColor.R;
                                pipePoint.Green = (int)featureColor.G;
                                pipePoint.Blue = (int)featureColor.B;
                                this.m_arrPipePoints.Add(pipePoint);
                            }
                            feature = enumFeature.Next();
                        }
                    }
                }
            }
            map.ClearSelection();
            IPointCollection pointCollection = (IPointCollection)this.m_pBaseLine;
            int pointCount = pointCollection.PointCount;
            if (pointCount != 0)
            {
                for (int j = 0; j < pointCount; j++)
                {
                    IPoint point = pointCollection.get_Point(j);
                    double x = point.X;
                    double y = point.Y;
                    PipePoint pipePoint2 = new PipePoint();
                    pipePoint2.x = x;
                    pipePoint2.y = y;
                    pipePoint2.PointType = PipePoint.SectionPointType.sptDrawPoint;
                    this.m_arrPipePoints.Add(pipePoint2);
                }
                this.fEswZsmwIx((PipePoint)this.m_arrPipePoints[this.m_arrPipePoints.Count - 2]);
                this.method_1();
                if (this.method_2())
                {
                    this.method_5(this.m_arrPipePointsDraw, this.m_arrPipePoints);
                    this.method_4();
                    this.method_7(this.m_arrPipePointsDraw);
                }
            }
        }

        private void method_1()
        {
            for (int i = 0; i < this.m_arrPipePoints.Count; i++)
            {
                if ((((PipePoint)this.m_arrPipePoints[i]).PointType == PipePoint.SectionPointType.sptRoadBorder || ((PipePoint)this.m_arrPipePoints[i]).PointType == PipePoint.SectionPointType.sptMidRoadLine ? true : ((PipePoint)this.m_arrPipePoints[i]).PointType == PipePoint.SectionPointType.sptMidGreen))
                {
                    if (i == 1)
                    {
                        int num = 1;
                        while (num < this.m_arrPipePoints.Count)
                        {
                            if (((PipePoint)this.m_arrPipePoints[num]).PointType == PipePoint.SectionPointType.sptPipe)
                            {
                                ((PipePoint)this.m_arrPipePoints[i]).m = ((PipePoint)this.m_arrPipePoints[num]).m;
                                ((PipePoint)this.m_arrPipePoints[i]).z = ((PipePoint)this.m_arrPipePoints[num]).z;
                                continue;
                            }
                            else
                            {
                                num++;
                            }
                        }
                    }
                    else if (i != this.m_arrPipePoints.Count - 2)
                    {
                        float item = 0f;
                        float single = 0f;
                        bool flag = false;
                        bool flag1 = false;
                        int num1 = i - 1;
                        while (true)
                        {
                            if (num1 <= -1)
                            {
                                break;
                            }
                            else if (((PipePoint)this.m_arrPipePoints[num1]).PointType == PipePoint.SectionPointType.sptPipe)
                            {
                                item = (float)((PipePoint)this.m_arrPipePoints[num1]).m;
                                flag = true;
                                break;
                            }
                            else
                            {
                                num1--;
                            }
                        }
                        int num2 = i + 1;
                        while (true)
                        {
                            if (num2 >= this.m_arrPipePoints.Count)
                            {
                                break;
                            }
                            else if (((PipePoint)this.m_arrPipePoints[num2]).PointType == PipePoint.SectionPointType.sptPipe)
                            {
                                single = (float)((PipePoint)this.m_arrPipePoints[num2]).m;
                                flag1 = true;
                                break;
                            }
                            else
                            {
                                num2++;
                            }
                        }
                        if (!flag1)
                        {
                            ((PipePoint)this.m_arrPipePoints[i]).m = (double)item;
                            ((PipePoint)this.m_arrPipePoints[i]).z = (double)item;
                        }
                        else if (flag)
                        {
                            ((PipePoint)this.m_arrPipePoints[i]).m = (double)((item + single) / 2f);
                            ((PipePoint)this.m_arrPipePoints[i]).z = (double)((item + single) / 2f);
                        }
                        else
                        {
                            ((PipePoint)this.m_arrPipePoints[i]).m = (double)single;
                            ((PipePoint)this.m_arrPipePoints[i]).z = (double)single;
                        }
                    }
                    else
                    {
                        int count = this.m_arrPipePoints.Count - 2;
                        while (count > -1)
                        {
                            if (((PipePoint)this.m_arrPipePoints[count]).PointType == PipePoint.SectionPointType.sptPipe)
                            {
                                ((PipePoint)this.m_arrPipePoints[i]).m = ((PipePoint)this.m_arrPipePoints[count]).m;
                                ((PipePoint)this.m_arrPipePoints[i]).z = ((PipePoint)this.m_arrPipePoints[count]).z;
                                break;
                            }
                            else
                            {
                                count--;
                            }
                        }
                    }
                }
            }
        }

        private bool method_2()
        {
            bool flag;
            int count = this.m_arrPipePoints.Count;
            if (count >= 3)
            {
                bool flag1 = false;
                int num = 0;
                while (true)
                {
                    if (num >= this.m_arrPipePoints.Count)
                    {
                        break;
                    }
                    else if (((PipePoint)this.m_arrPipePoints[num]).PointType == PipePoint.SectionPointType.sptPipe)
                    {
                        flag1 = true;
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }
                if (flag1)
                {
                    ((PipePoint)this.m_arrPipePoints[0]).z = ((PipePoint)this.m_arrPipePoints[1]).z;
                    ((PipePoint)this.m_arrPipePoints[0]).m = ((PipePoint)this.m_arrPipePoints[1]).m;
                    ((PipePoint)this.m_arrPipePoints[count - 1]).z = ((PipePoint)this.m_arrPipePoints[count - 2]).z;
                    ((PipePoint)this.m_arrPipePoints[count - 1]).m = ((PipePoint)this.m_arrPipePoints[count - 2]).m;
                    flag = true;
                }
                else
                {
                    MessageBox.Show("没有划中管线！");
                    flag = false;
                }
            }
            else
            {
                MessageBox.Show("没有划中管线！");
                flag = false;
            }
            return flag;
        }

        private void fEswZsmwIx(PipePoint ppDst)
        {
            if (this.m_arrPipePoints.Count != 0)
            {
                int count = this.m_arrPipePoints.Count;
                for (int i = count - 1; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        PipePoint pipePoint = (PipePoint)this.m_arrPipePoints[j];
                        PipePoint pipePoint2 = (PipePoint)this.m_arrPipePoints[j + 1];
                        double num = pipePoint.DistanceToPipePoint(ppDst);
                        double num2 = pipePoint2.DistanceToPipePoint(ppDst);
                        if (num > num2)
                        {
                            PipePoint value = pipePoint;
                            this.m_arrPipePoints[j] = pipePoint2;
                            this.m_arrPipePoints[j + 1] = value;
                        }
                    }
                }
            }
        }

        private void method_3()
        {
            IPointCollection pointCollection = (IPointCollection)this.m_pBaseLine;
            int pointCount = pointCollection.PointCount;
            if (pointCount != 0)
            {
                IPoint point = pointCollection.get_Point(0);
                IPoint point2 = pointCollection.get_Point(pointCount - 1);
                GPoint gPoint = new GPoint();
                GPoint gPoint2 = new GPoint();
                gPoint.X = point.X;
                gPoint.Y = point.Y;
                gPoint2.X = point2.X;
                gPoint2.Y = point2.Y;
                double angleToPt = gPoint.GetAngleToPt(gPoint2);
                this.m_pSectionDisp.TranAngle = angleToPt;
            }
        }

        private void method_4()
        {
            int count = this.m_arrPipePointsDraw.Count;
            PipePoint pipePoint = new PipePoint();
            double num = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint2 = (PipePoint)this.m_arrPipePointsDraw[i];
                if (i < count - 1)
                {
                    PipePoint deepCopy = ((PipePoint)this.m_arrPipePointsDraw[i + 1]).GetDeepCopy();
                    num = pipePoint2.DistanceToPipePoint(deepCopy);
                }
                if (i != 0)
                {
                    pipePoint2.x = pipePoint.x + num2;
                }
                num2 = num;
                pipePoint = pipePoint2.GetDeepCopy();
            }
            for (int j = 0; j < count; j++)
            {
            }
            for (int k = 0; k < count; k++)
            {
            }
        }

        private void method_5(ArrayList arrayList, ArrayList arrayList2)
        {
            int count = arrayList2.Count;
            arrayList.Clear();
            for (int i = 0; i < count; i++)
            {
                PipePoint deepCopy = ((PipePoint)arrayList2[i]).GetDeepCopy();
                arrayList.Add(deepCopy);
            }
        }

        private GPoints method_6(IPolyline polyline, IPolyline polyline2)
        {
            IPointCollection pointCollection = (IPointCollection)polyline;
            int pointCount = pointCollection.PointCount;
            GPoints result;
            if (pointCount == 0)
            {
                result = null;
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
                    double z = point.Z - point.M;
                    double z2 = point.Z;
                    gPolyLine.PushBack(new GPoint
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        M = z2
                    });
                }
                pointCollection = (IPointCollection)polyline2;
                pointCount = pointCollection.PointCount;
                if (pointCount == 0)
                {
                    result = null;
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
                        double z3 = point2.Z - point2.M;
                        double m;
                        if (double.IsNaN(point2.M))
                        {
                            m = 1.0 + point2.Z;
                        }
                        else
                        {
                            m = point2.Z;
                        }
                        gPolyLine2.PushBack(new GPoint
                        {
                            X = x2,
                            Y = y2,
                            Z = z3,
                            M = m
                        });
                    }
                    new GPoints();
                    result = gPolyLine.GetInterPtsToPolyLineWithHeightForTransect(gPolyLine2);
                }
            }
            return result;
        }

        private void method_7(ArrayList arrayList)
        {
            int count = arrayList.Count;
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                if (pipePoint.PointType == PipePoint.SectionPointType.sptPipe || pipePoint.PointType == PipePoint.SectionPointType.sptDrawPoint)
                {
                    if (num == 0)
                    {
                        this.m_dMinX = (this.m_dMaxX = pipePoint.x);
                        if (pipePoint.z.ToString() == "非数字" || pipePoint.m.ToString() == "非数字")
                        {
                            num++;
                        }
                        else
                        {
                            this.m_dMinY = Math.Min(pipePoint.z, pipePoint.m);
                            this.m_dMaxY = Math.Max(pipePoint.z, pipePoint.m);
                            num++;
                        }
                    }
                    else
                    {
                        this.m_dMinX = Math.Min(this.m_dMinX, pipePoint.x);
                        this.m_dMaxX = Math.Max(this.m_dMaxX, pipePoint.x);
                        if (pipePoint.z.ToString() == "非数字" || pipePoint.m.ToString() == "非数字")
                        {
                            num++;
                        }
                        else
                        {
                            this.m_dMinY = Math.Min(this.m_dMinY, Math.Min(pipePoint.z, pipePoint.m));
                            this.m_dMaxY = Math.Max(this.m_dMaxY, Math.Max(pipePoint.z, pipePoint.m));
                            num++;
                        }
                    }
                }
            }
            this.m_pSectionDisp.SetDataBound(this.m_dMinX, this.m_dMaxX, this.m_dMinY, this.m_dMaxY);
        }
    }
}