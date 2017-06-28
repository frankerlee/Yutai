using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class VerSection : Section
    {
        private ArrayList arrayList_0 = new ArrayList();

        private ArrayList arrayList_1 = new ArrayList();

        private ArrayList arrayList_2 = new ArrayList();

        private ArrayList arrayList_3 = new ArrayList();

        private ArrayList arrayList_4 = new ArrayList();

        private char[] char_0 = new char[] {'x', 'X', 'Х'};

        public VerSection(object objForm, IAppContext pApp, IPipelineConfig config) : base(objForm, pApp, config)
        {
        }

        public override void Paint()
        {
            base.Paint();
            this.m_pSectionDisp.DrawVerSection(this.arrayList_3, this.arrayList_4, 0);
        }

        public override void GetSelectedData()
        {
            this.GetSortInfos(this.arrayList_0);
            this.method_1(this.arrayList_0);
            this.method_2();
        }

        public override void PrintPage(object objPrint)
        {
            base.PrintPage(objPrint);
            this.m_pSectionDisp.DrawVerSection(this.arrayList_3, this.arrayList_4, 0);
        }

        public void GetSortInfos(ArrayList pSortInfos)
        {
            pSortInfos.Clear();
            IMap map = m_context.FocusMap;
            IEnumFeature enumFeature = (IEnumFeature) map.FeatureSelection;
            IFeature feature = enumFeature.Next();
            if (feature != null)
            {
                do
                {
                    if (feature.FeatureType == esriFeatureType.esriFTSimpleEdge)
                    {
                        IPolyline egLine = feature.Shape as IPolyline;
                        IPoint newCenter = new PointClass();
                        egLine.QueryPoint(esriSegmentExtension.esriNoExtension, 0.01, true, newCenter);
                        IEdgeFeature pEgFeature = feature as IEdgeFeature;
                        IFeatureClass pClass = feature.Class as IFeatureClass;
                        INetworkClass pNetworkClass = pClass as INetworkClass;
                        INetElements network = pNetworkClass.GeometricNetwork.Network as INetElements;
                        IEnumFeature enumFeatures = pNetworkClass.GeometricNetwork.SearchForNetworkFeature(newCenter,
                            esriFeatureType.esriFTSimpleEdge);

                        IEdgeFeature edgeFeature = (IEdgeFeature) enumFeatures.Next();
                        ISimpleEdgeFeature simpedgeFeature = (ISimpleEdgeFeature) edgeFeature;
                        pSortInfos.Add(new SortInfo
                        {
                            SmID = simpedgeFeature.EID,
                            SmFNode = edgeFeature.FromJunctionEID,
                            SmTNode = edgeFeature.ToJunctionEID
                        });
                    }
                    //IEdgeFeature edgeFeature = (IEdgeFeature) feature;
                    //pSortInfos.Add(new SortInfo
                    //{
                    //    SmID = Convert.ToInt32(feature.get_Value(0).ToString()),
                    //    SmFNode = edgeFeature.FromJunctionEID,
                    //    SmTNode = edgeFeature.ToJunctionEID
                    //});
                    feature = enumFeature.Next();
                } while (feature != null);
            }
        }

        private void method_1(ArrayList arrayList)
        {
            int count = arrayList.Count;
            for (int i = 0; i < count - 1; i++)
            {
                SortInfo sortInfo = (SortInfo) arrayList[i];
                SortInfo sortInfo2 = (SortInfo) arrayList[i + 1];
                int pointInfoRleation = sortInfo.GetPointInfoRleation(sortInfo2);
                if (pointInfoRleation == 21)
                {
                    sortInfo.bRightDirection = true;
                    sortInfo2.bRightDirection = true;
                }
                if (pointInfoRleation == 22)
                {
                    sortInfo.bRightDirection = true;
                    sortInfo2.bRightDirection = false;
                }
                if (pointInfoRleation == 12)
                {
                    sortInfo.bRightDirection = false;
                    sortInfo2.bRightDirection = false;
                }
                if (pointInfoRleation == 11)
                {
                    sortInfo.bRightDirection = false;
                    sortInfo2.bRightDirection = true;
                }
            }
        }

        //!在这儿进行修改，将对高程埋深数据存储在不同位置进行统一处理
        private void method_2()
        {
            IMap map = m_context.FocusMap;
            IEnumFeature enumFeature = (IEnumFeature) map.FeatureSelection;
            IFeature feature = enumFeature.Next();
            bool isMUsing = false;
            int qdgcIndex = -1;
            int qdmsIndex = -1;
            int zdgcIndex = -1;
            int zdmsIndex = -1;

            if (feature == null) return;
            if (feature.FeatureType != esriFeatureType.esriFTSimpleEdge) return;

            this.arrayList_1.Clear();
            this.arrayList_2.Clear();
            int num = 0;
            while (feature != null)
            {
                IPolyline egLine = feature.Shape as IPolyline;
                IPoint newCenter = new PointClass();
                egLine.QueryPoint(esriSegmentExtension.esriNoExtension, 0.01, true, newCenter);
                IEdgeFeature pEgFeature = feature as IEdgeFeature;
                IFeatureClass pClass = feature.Class as IFeatureClass;
                INetworkClass pNetworkClass = pClass as INetworkClass;
                INetElements network = pNetworkClass.GeometricNetwork.Network as INetElements;
                IEnumFeature enumFeatures = pNetworkClass.GeometricNetwork.SearchForNetworkFeature(newCenter,
                    esriFeatureType.esriFTSimpleEdge);

                ISimpleEdgeFeature simpleEdgeFeature = enumFeatures.Next() as ISimpleEdgeFeature;

                IFeature realFeature = null;

                int userClassID, userID, userSubID;

                network.QueryIDs(simpleEdgeFeature.EID, esriElementType.esriETEdge, out userClassID, out userID,
                    out userSubID);
                if (pClass.FeatureClassID == userClassID)
                {
                    realFeature = pClass.GetFeature(userID);
                }
                else
                {
                    IEnumDataset dses = pNetworkClass.FeatureDataset.Subsets;
                    dses.Reset();
                    IDataset ds = dses.Next();
                    while (ds != null)
                    {
                        if (ds is IFeatureClass)
                        {
                            IFeatureClass pClass2 = ds as IFeatureClass;
                            if (pClass2.FeatureClassID == userClassID)
                            {
                                realFeature = pClass2.GetFeature(userID);
                                break;
                            }
                        }
                    }
                }

                IMAware mAware = realFeature.Shape as IMAware;
                isMUsing = mAware.MAware;
                IFeatureLayer pLayer = MapHelper.GetLayerByFeature(map as IBasicMap, realFeature);
                IBasicLayerInfo lineConfig =
                    PipeConfig.GetBasicLayerInfo(realFeature.Class.AliasName) as IBasicLayerInfo;
              
                
                if (!isMUsing)
                {
                    qdgcIndex =
                        realFeature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.QDGC));
                    qdmsIndex =
                        realFeature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                    zdgcIndex =
                        realFeature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.ZDGC));
                    zdmsIndex =
                        realFeature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));
                }
                PipeLine pipeLine = new PipeLine();
                IPolyline polyline = (IPolyline) feature.Shape;
                IPointCollection pointCollection = (IPointCollection) polyline;
                int pointCount = pointCollection.PointCount;
                pipeLine.Clear();
                if (isMUsing)
                {
                    for (int i = 0; i < pointCount; i++)
                    {
                        IPoint point = new ESRI.ArcGIS.Geometry.Point();
                        if (((SortInfo) this.arrayList_0[num]).bRightDirection)
                        {
                            point = pointCollection.get_Point(i);
                        }
                        else
                        {
                            point = pointCollection.get_Point(pointCount - i - 1);
                        }

                        if (double.IsNaN(point.M))
                        {
                            pipeLine.PushBack(point.X, point.Y, point.Z, point.Z + 1.0);
                        }
                        else
                        {
                            pipeLine.PushBack(point.X, point.Y, point.Z - point.M, point.Z);
                        }
                    }
                }
                else
                {
                    double height = 0;
                    double qdgc = GetDoubleValue(realFeature, qdgcIndex, out height);
                    double zdgc = GetDoubleValue(realFeature, zdgcIndex, out height);
                    double qdms = GetDoubleValue(realFeature, qdmsIndex, out height);
                    double zdms = GetDoubleValue(realFeature, zdmsIndex, out height);
                    if (qdms == 0) qdms = 1;
                    if (zdms == 0) zdms = 1;
                    IPoint startPoint = pointCollection.Point[0];
                    IPoint endPoint = pointCollection.Point[pointCollection.PointCount - 1];
                    pipeLine.PushBack(startPoint.X, startPoint.Y, qdgc - qdms, qdgc);
                    pipeLine.PushBack(endPoint.X, endPoint.Y, zdgc - zdms, zdgc);
                }
                //string text = "管线性质";
                string text = lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ) == ""
                    ? "管线性质"
                    : lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GDXZ);
               
                int num2 = realFeature.Fields.FindField(text);
                string text2 = "";
                if (num2 != -1)
                {
                    object obj = realFeature.get_Value(num2);
                    if (obj == null || Convert.IsDBNull(obj))
                    {
                        text2 = "";
                    }
                    else
                    {
                        text2 = obj.ToString();
                    }
                }
                pipeLine.ID = Convert.ToInt32(realFeature.get_Value(0).ToString());
                pipeLine.DatasetName = text2;
                int num3 = realFeature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GXCZ));
                pipeLine.Material=num3==-1?"": realFeature.get_Value(num3).ToString();
             
                //管径
                num3 = realFeature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.GJ));
                string text3= num3==-1?"": realFeature.get_Value(num3).ToString();
               
                //断面尺寸
                num3 = feature.Fields.FindField(lineConfig.GetFieldName(PipeConfigWordHelper.LineWords.DMCC));
                string text4= num3 == -1?"" : realFeature.get_Value(num3).ToString();
              
                Color featureColor = CommonUtils.GetFeatureColor(map, realFeature.Class.AliasName, realFeature);
                pipeLine.Red = (int) featureColor.R;
                pipeLine.Green = (int) featureColor.G;
                pipeLine.Blue = (int) featureColor.B;
                if (text3 != "")
                {
                    pipeLine.PipeWidthHeight = text3;
                }
                if (text4 != "")
                {
                    pipeLine.PipeWidthHeight = text4;
                }
                if (pipeLine.PipeWidthHeight == null)
                {
                    pipeLine.PipeWidthHeight = "";
                }
                this.arrayList_1.Add(pipeLine);
                IFeature feature2 = (IFeature) ((IEdgeFeature)realFeature).FromJunctionFeature;
                IFeature feature3 = (IFeature) ((IEdgeFeature)realFeature).ToJunctionFeature;
                IFeature feature4;
                if (num == 0)
                {
                    PipePoint pipePoint = new PipePoint();
                    if (((SortInfo) this.arrayList_0[num]).bRightDirection)
                    {
                        feature4 = feature2;
                    }
                    else
                    {
                        feature4 = feature3;
                    }
                    pipePoint.nID = Convert.ToInt32(feature4.get_Value(0));
                    pipePoint.nAtPipeSegID = pipeLine.ID;
                    pipePoint.bstrDatasetName = text2;

                    IBasicLayerInfo pointConfig =
                        PipeConfig.GetBasicLayerInfo(feature4.Class.AliasName) as IBasicLayerInfo;
                    num3 = feature4.Fields.FindField(pointConfig.GetFieldName(PipeConfigWordHelper.PointWords.FSW));
                    if (num3 == -1)
                    {
                        pipePoint.bstrPointKind = "";
                    }
                    else
                    {
                        pipePoint.bstrPointKind = feature4.get_Value(num3).ToString();
                    }
                    Color featureColor2 = CommonUtils.GetFeatureColor(map, feature4.Class.AliasName, feature4);
                    pipePoint.Red = (int) featureColor2.R;
                    pipePoint.Green = (int) featureColor2.G;
                    pipePoint.Blue = (int) featureColor2.B;
                    this.arrayList_2.Add(pipePoint);
                }
                PipePoint pipePoint2 = new PipePoint();
                if (((SortInfo) this.arrayList_0[num]).bRightDirection)
                {
                    feature4 = feature3;
                }
                else
                {
                    feature4 = feature2;
                }
                pipePoint2.nID = Convert.ToInt32(feature4.get_Value(0));
                pipePoint2.nAtPipeSegID = pipeLine.ID;
                pipePoint2.bstrDatasetName = text2;
                IBasicLayerInfo pointConfig3 =
                    PipeConfig.GetBasicLayerInfo(feature4.Class.AliasName) as IBasicLayerInfo;
                num3 = feature4.Fields.FindField(pointConfig3.GetFieldName(PipeConfigWordHelper.PointWords.FSW));
                if (num3 == -1)
                {
                    pipePoint2.bstrPointKind = "";
                }
                else
                {
                    pipePoint2.bstrPointKind = feature4.get_Value(num3).ToString();
                }
                Color featureColor3 = CommonUtils.GetFeatureColor(map, feature4.Class.AliasName, feature4);
                pipePoint2.Red = (int) featureColor3.R;
                pipePoint2.Green = (int) featureColor3.G;
                pipePoint2.Blue = (int) featureColor3.B;
                this.arrayList_2.Add(pipePoint2);
                feature = enumFeature.Next();
                num++;
            }
            this.method_3(this.arrayList_2, this.arrayList_1);
            this.method_4(this.arrayList_3, this.arrayList_1);
            this.method_5(this.arrayList_4, this.arrayList_2);
            this.method_6();
            this.method_7(this.arrayList_3);
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


        private void method_3(ArrayList arrayList, ArrayList arrayList2)
        {
            int count = arrayList2.Count;
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine) arrayList2[i];
                int num2 = pipeLine.Size();
                if (i == 0)
                {
                    PipePoint pipePoint = (PipePoint) arrayList[num];
                    pipePoint.x = pipeLine[i].X;
                    pipePoint.y = pipeLine[i].Y;
                    pipePoint.z = pipeLine[i].Z;
                    pipePoint.m = pipeLine[i].M;
                    pipePoint.strPipeWidthHeight = pipeLine.PipeWidthHeight;
                    num++;
                }
                PipePoint pipePoint2 = (PipePoint) arrayList[num];
                pipePoint2.x = pipeLine[num2 - 1].X;
                pipePoint2.y = pipeLine[num2 - 1].Y;
                pipePoint2.z = pipeLine[num2 - 1].Z;
                pipePoint2.m = pipeLine[num2 - 1].M;
                pipePoint2.strPipeWidthHeight = pipeLine.PipeWidthHeight;
                num++;
            }
        }

        private void method_4(ArrayList arrayList, ArrayList arrayList2)
        {
            int count = arrayList2.Count;
            arrayList.Clear();
            for (int i = 0; i < count; i++)
            {
                PipeLine deepCopy = ((PipeLine) arrayList2[i]).GetDeepCopy();
                arrayList.Add(deepCopy);
            }
        }

        private void method_5(ArrayList arrayList, ArrayList arrayList2)
        {
            int count = arrayList2.Count;
            arrayList.Clear();
            for (int i = 0; i < count; i++)
            {
                PipePoint deepCopy = ((PipePoint) arrayList2[i]).GetDeepCopy();
                arrayList.Add(deepCopy);
            }
        }

        private void method_6()
        {
            int count = this.arrayList_3.Count;
            GPoint gPoint = new GPoint();
            double num = 0.0;
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine) this.arrayList_3[i];
                int num2 = pipeLine.Size();
                for (int j = 0; j < num2; j++)
                {
                    GPoint gPoint2 = pipeLine[j];
                    double num3;
                    if (j < num2 - 1)
                    {
                        GPoint deepCopy = pipeLine[j + 1].GetDeepCopy();
                        num3 = gPoint2.DistanceToPt(deepCopy);
                    }
                    else
                    {
                        num3 = 0.0;
                    }
                    if (i == 0)
                    {
                        if (j != 0)
                        {
                            gPoint2.X = gPoint.X + num;
                        }
                    }
                    else if (j == 0)
                    {
                        gPoint2.X = gPoint.X;
                    }
                    else
                    {
                        gPoint2.X = gPoint.X + num;
                    }
                    num = num3;
                    gPoint = gPoint2.GetDeepCopy();
                }
                this.method_3(this.arrayList_4, this.arrayList_3);
            }
            for (int k = 0; k < count; k++)
            {
                PipeLine pipeLine2 = (PipeLine) this.arrayList_1[k];
                int num4 = pipeLine2.Size();
                for (int l = 0; l < num4; l++)
                {
                }
            }
            for (int m = 0; m < count; m++)
            {
                PipeLine pipeLine3 = (PipeLine) this.arrayList_3[m];
                int num5 = pipeLine3.Size();
                for (int n = 0; n < num5; n++)
                {
                }
            }
        }

        public override void SectionInfo(out ArrayList pArrInfo)
        {
            pArrInfo = new ArrayList();
            int count = this.arrayList_1.Count;
            if (this.m_nSelectIndex != -1)
            {
                if (this.m_nSelectIndex < count)
                {
                    PipeLine pipeLine = (PipeLine) this.arrayList_1[this.m_nSelectIndex];
                    SectionInfoStore sectionInfoStore;
                    sectionInfoStore.strField = "数据集名称";
                    sectionInfoStore.strVal = pipeLine.DatasetName;
                    pArrInfo.Add(sectionInfoStore);
                    SectionInfoStore sectionInfoStore2;
                    sectionInfoStore2.strField = "ID";
                    sectionInfoStore2.strVal = pipeLine.ID.ToString();
                    pArrInfo.Add(sectionInfoStore2);
                    SectionInfoStore sectionInfoStore3;
                    sectionInfoStore3.strField = "材质";
                    sectionInfoStore3.strVal = pipeLine.Material;
                    pArrInfo.Add(sectionInfoStore3);
                    SectionInfoStore sectionInfoStore4;
                    sectionInfoStore4.strField = "规格";
                    sectionInfoStore4.strVal = pipeLine.PipeWidthHeight;
                    pArrInfo.Add(sectionInfoStore4);
                    SectionInfoStore sectionInfoStore5;
                    sectionInfoStore5.strField = "管点数";
                    sectionInfoStore5.strVal = pipeLine.Size().ToString();
                    pArrInfo.Add(sectionInfoStore5);
                    SectionInfoStore sectionInfoStore6;
                    sectionInfoStore6.strField = "管线长度";
                    sectionInfoStore6.strVal = pipeLine.Length.ToString();
                    pArrInfo.Add(sectionInfoStore6);
                }
                else
                {
                    PipePoint pipePoint = (PipePoint) this.arrayList_2[this.m_nSelectIndex - count];
                    SectionInfoStore sectionInfoStore7;
                    sectionInfoStore7.strField = "数据集名称";
                    sectionInfoStore7.strVal = pipePoint.bstrDatasetName;
                    pArrInfo.Add(sectionInfoStore7);
                    SectionInfoStore sectionInfoStore8;
                    sectionInfoStore8.strField = "ID";
                    sectionInfoStore8.strVal = pipePoint.nID.ToString();
                    pArrInfo.Add(sectionInfoStore8);
                    SectionInfoStore sectionInfoStore9;
                    sectionInfoStore9.strField = "点性";
                    sectionInfoStore9.strVal = pipePoint.bstrPointKind;
                    pArrInfo.Add(sectionInfoStore9);
                    SectionInfoStore sectionInfoStore10;
                    sectionInfoStore10.strField = "横坐标";
                    sectionInfoStore10.strVal = pipePoint.x.ToString("f3");
                    pArrInfo.Add(sectionInfoStore10);
                    SectionInfoStore sectionInfoStore11;
                    sectionInfoStore11.strField = "纵坐标";
                    sectionInfoStore11.strVal = pipePoint.y.ToString("f3");
                    pArrInfo.Add(sectionInfoStore11);
                    SectionInfoStore sectionInfoStore12;
                    sectionInfoStore12.strField = "地面高程";
                    sectionInfoStore12.strVal = pipePoint.m.ToString("f3");
                    pArrInfo.Add(sectionInfoStore12);
                }
            }
        }

        public override void SaveSectionFile(string strFileName)
        {
            int count = this.arrayList_1.Count;
            XmlTextWriter xmlTextWriter = new XmlTextWriter(strFileName, null);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteComment("SectionSaveFile");
            xmlTextWriter.WriteStartElement("VerSectionAnalyse");
            xmlTextWriter.WriteAttributeString("SectionType", "VerSectionType");
            string title = this.m_pSectionDisp.Title;
            string roadName = this.m_pSectionDisp.RoadName;
            string sectionNo = this.m_pSectionDisp.SectionNo;
            xmlTextWriter.WriteAttributeString("Title", title);
            xmlTextWriter.WriteAttributeString("RoadName", roadName);
            xmlTextWriter.WriteAttributeString("SectionNo", sectionNo);
            xmlTextWriter.WriteStartElement("PipeLines");
            xmlTextWriter.WriteAttributeString("PipeLineCount", count.ToString());
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine) this.arrayList_1[i];
                int num = pipeLine.Size();
                xmlTextWriter.WriteStartElement("PipeLine");
                xmlTextWriter.WriteAttributeString("DatasetName", pipeLine.DatasetName);
                xmlTextWriter.WriteAttributeString("ID", pipeLine.ID.ToString());
                xmlTextWriter.WriteAttributeString("Material", pipeLine.Material);
                xmlTextWriter.WriteAttributeString("PipeWidthAndHeight", pipeLine.PipeWidthHeight);
                xmlTextWriter.WriteAttributeString("PointCount", pipeLine.Size().ToString());
                xmlTextWriter.WriteAttributeString("Red", pipeLine.Red.ToString());
                xmlTextWriter.WriteAttributeString("Green", pipeLine.Green.ToString());
                xmlTextWriter.WriteAttributeString("Blue", pipeLine.Blue.ToString());
                for (int j = 0; j < num; j++)
                {
                    GPoint gPoint = pipeLine[j];
                    xmlTextWriter.WriteStartElement("Point");
                    xmlTextWriter.WriteAttributeString("X", gPoint.X.ToString("f3"));
                    xmlTextWriter.WriteAttributeString("Y", gPoint.Y.ToString("f3"));
                    xmlTextWriter.WriteAttributeString("Z", gPoint.Z.ToString("f3"));
                    xmlTextWriter.WriteAttributeString("M", gPoint.M.ToString("f3"));
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();
            }
            xmlTextWriter.WriteEndElement();
            int count2 = this.arrayList_2.Count;
            xmlTextWriter.WriteStartElement("PipePoints");
            xmlTextWriter.WriteAttributeString("PipePointCount", count2.ToString());
            for (int k = 0; k < count2; k++)
            {
                PipePoint pipePoint = (PipePoint) this.arrayList_2[k];
                xmlTextWriter.WriteStartElement("PipePoint");
                xmlTextWriter.WriteAttributeString("X", pipePoint.x.ToString("f3"));
                xmlTextWriter.WriteAttributeString("Y", pipePoint.y.ToString("f3"));
                xmlTextWriter.WriteAttributeString("Z", pipePoint.z.ToString("f3"));
                xmlTextWriter.WriteAttributeString("M", pipePoint.m.ToString("f3"));
                xmlTextWriter.WriteAttributeString("DatasetName", pipePoint.bstrDatasetName);
                xmlTextWriter.WriteAttributeString("ID", pipePoint.nID.ToString());
                xmlTextWriter.WriteAttributeString("PipePointKind", pipePoint.bstrPointKind);
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
            xmlReader.ReadToFollowing("VerSectionAnalyse");
            string attribute = xmlReader.GetAttribute("SectionType");
            if (attribute != "VerSectionType")
            {
                MessageBox.Show("不是纵断面保存文件！");
            }
            else
            {
                this.m_pSectionDisp.Title = xmlReader.GetAttribute("Title");
                this.m_pSectionDisp.RoadName = xmlReader.GetAttribute("RoadName");
                this.m_pSectionDisp.SectionNo = xmlReader.GetAttribute("SectionNo");
                xmlReader.ReadToFollowing("PipeLines");
                int num = Convert.ToInt32(xmlReader.GetAttribute("PipeLineCount"));
                this.arrayList_1.Clear();
                for (int i = 0; i < num; i++)
                {
                    xmlReader.ReadToFollowing("PipeLine");
                    PipeLine pipeLine = new PipeLine();
                    pipeLine.DatasetName = xmlReader.GetAttribute("DatasetName");
                    pipeLine.ID = Convert.ToInt32(xmlReader.GetAttribute("ID"));
                    pipeLine.Material = xmlReader.GetAttribute("Material");
                    pipeLine.PipeWidthHeight = xmlReader.GetAttribute("PipeWidthAndHeight");
                    pipeLine.Red = Convert.ToInt32(xmlReader.GetAttribute("Red"));
                    pipeLine.Green = Convert.ToInt32(xmlReader.GetAttribute("Green"));
                    pipeLine.Blue = Convert.ToInt32(xmlReader.GetAttribute("Blue"));
                    int num2 = Convert.ToInt32(xmlReader.GetAttribute("PointCount"));
                    for (int j = 0; j < num2; j++)
                    {
                        xmlReader.ReadToFollowing("Point");
                        pipeLine.PushBack(new GPoint
                        {
                            X = (double) Convert.ToSingle(xmlReader.GetAttribute("X")),
                            Y = (double) Convert.ToSingle(xmlReader.GetAttribute("Y")),
                            Z = (double) Convert.ToSingle(xmlReader.GetAttribute("Z")),
                            M = (double) Convert.ToSingle(xmlReader.GetAttribute("M"))
                        });
                    }
                    this.arrayList_1.Add(pipeLine);
                }
                xmlReader.ReadToFollowing("PipePoints");
                int num3 = Convert.ToInt32(xmlReader.GetAttribute("PipePointCount"));
                this.arrayList_2.Clear();
                for (int k = 0; k < num3; k++)
                {
                    xmlReader.ReadToFollowing("PipePoint");
                    PipePoint pipePoint = new PipePoint();
                    pipePoint.x = (double) Convert.ToSingle(xmlReader.GetAttribute("X"));
                    pipePoint.y = (double) Convert.ToSingle(xmlReader.GetAttribute("Y"));
                    pipePoint.z = (double) Convert.ToSingle(xmlReader.GetAttribute("Z"));
                    pipePoint.m = (double) Convert.ToSingle(xmlReader.GetAttribute("M"));
                    pipePoint.bstrDatasetName = xmlReader.GetAttribute("DatasetName");
                    pipePoint.nID = Convert.ToInt32(xmlReader.GetAttribute("ID"));
                    pipePoint.bstrPointKind = xmlReader.GetAttribute("PipePointKind");
                    pipePoint.Red = Convert.ToInt32(xmlReader.GetAttribute("Red"));
                    pipePoint.Green = Convert.ToInt32(xmlReader.GetAttribute("Green"));
                    pipePoint.Blue = Convert.ToInt32(xmlReader.GetAttribute("Blue"));
                    this.arrayList_2.Add(pipePoint);
                }
                this.method_4(this.arrayList_3, this.arrayList_1);
                this.method_5(this.arrayList_4, this.arrayList_2);
                this.method_6();
                this.method_7(this.arrayList_3);
                base.Paint();
            }
        }

        private void method_7(ArrayList arrayList)
        {
            int count = arrayList.Count;
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine) arrayList[i];
                int num = pipeLine.Size();
                for (int j = 0; j < num; j++)
                {
                    GPoint gPoint = pipeLine[j];
                    if (i == 0 && j == 0)
                    {
                        this.m_dMinX = (this.m_dMaxX = gPoint.X);
                        this.m_dMinY = Math.Min(gPoint.Z, gPoint.M);
                        this.m_dMaxY = Math.Max(gPoint.Z, gPoint.M);
                    }
                    else
                    {
                        this.m_dMinX = Math.Min(this.m_dMinX, gPoint.X);
                        this.m_dMaxX = Math.Max(this.m_dMaxX, gPoint.X);
                        this.m_dMinY = Math.Min(this.m_dMinY, Math.Min(gPoint.Z, gPoint.M));
                        this.m_dMaxY = Math.Max(this.m_dMaxY, Math.Max(gPoint.Z, gPoint.M));
                    }
                }
            }
            this.m_pSectionDisp.SetDataBound(this.m_dMinX, this.m_dMaxX, this.m_dMinY, this.m_dMaxY);
            for (int k = 0; k < count; k++)
            {
                PipeLine pipeLine2 = (PipeLine) this.arrayList_3[k];
                int num2 = pipeLine2.Size();
                for (int l = 0; l < num2; l++)
                {
                }
            }
            int count2 = this.arrayList_4.Count;
            for (int m = 0; m < count2; m++)
            {
            }
        }
    }
}