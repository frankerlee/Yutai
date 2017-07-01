using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class SketchShareEx
    {
        private static IPointSnapper m_psnaper;

        public static int m_CurrentStep;

        public static SysGrants m_SysGrants;

        public static SketchCommandFlow m_pSketchCommandFolw;

        public static bool IsFixLength;

        public static bool IsFixDirection;

        public static double FixLength;

        public static double FixDirection;

        public static IPoint StartPoint;

        public static IPoint LastPoint;

        public static bool IsCreateParrel;

        public static double m_offset;

        public static int ConstructOffset;

        public static int PointCount;

        public static IPoint m_pAnchorPoint;

        public static bool m_bInUse;

        public static IPointCollection m_pPointCollection;

        public static IPoint m_pLastPoint1;

        public static IPoint m_pEndPoint1;

        public static bool m_bShowVlaue;

        public static double m_totalLength;

        private static int m_LineType;

        private static int m_SegmentPtCount;

        public static bool m_bSnapSuccessful;

        private static ISimpleMarkerSymbol m_pSym;

        private static IPoint m_pPoint;

        public static bool m_HasLicense;

        public static IGeometry m_LastPartGeometry;

        public static bool m_bInStreaming;

        public static double m_MinDis;

        static SketchShareEx()
        {
            SketchShareEx.old_acctor_mc();
        }

        public SketchShareEx()
        {
        }

        public static void Draw(IDisplay idisplay_0)
        {
            if (SketchToolAssist.m_pAP != null)
            {
                SketchToolAssist.m_pAP.Draw(idisplay_0);
            }
        }

        public static void EndSketch(bool bool_0, IActiveView iactiveView_0, IFeatureLayer ifeatureLayer_0)
        {
            INewLineFeedback feedback;
            IPointCollection pointCollection;
            IGeometryCollection mLastPartGeometry;
            try
            {
                if (SketchToolAssist.m_pPointColn != null)
                {
                    SketchToolAssist.m_pPointColn.RemovePoints(0, SketchToolAssist.m_pPointColn.PointCount);
                }
                IPoint mPAnchorPoint = null;
                if (SketchToolAssist.Feedback != null)
                {
                    if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Line)
                    {
                        feedback = (INewLineFeedback) SketchToolAssist.Feedback;
                        mPAnchorPoint = SketchShareEx.m_pAnchorPoint;
                        if (bool_0)
                        {
                            feedback.AddPoint(mPAnchorPoint);
                        }
                        SketchToolAssist.TempLine = feedback.Stop();
                        SketchShareEx.m_bInUse = false;
                        if (SketchToolAssist.CurrentTask != null)
                        {
                            SketchToolAssist.CurrentTask.Excute();
                        }
                        SketchToolAssist.Feedback = null;
                        return;
                    }
                    else if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Fill)
                    {
                        INewPolygonFeedbackEx newPolygonFeedbackEx = (INewPolygonFeedbackEx) SketchToolAssist.Feedback;
                        mPAnchorPoint = SketchShareEx.m_pAnchorPoint;
                        if (bool_0)
                        {
                            newPolygonFeedbackEx.AddPoint(mPAnchorPoint);
                        }
                        SketchToolAssist.TempLine = newPolygonFeedbackEx.Stop();
                        SketchShareEx.m_bInUse = false;
                        if (SketchToolAssist.CurrentTask != null)
                        {
                            SketchToolAssist.CurrentTask.Excute();
                        }
                        SketchToolAssist.Feedback = null;
                        return;
                    }
                    else if (SketchToolAssist.IsDrawTempLine != DrawTempGeometry.Point)
                    {
                        mPAnchorPoint = SketchShareEx.m_pAnchorPoint;
                        IGeometry mPPointCollection = null;
                        if (SketchToolAssist.Feedback is INewMultiPointFeedback)
                        {
                            ((INewMultiPointFeedback) SketchToolAssist.Feedback).Stop();
                            mPPointCollection = (IGeometry) SketchShareEx.m_pPointCollection;
                        }
                        else if (SketchToolAssist.Feedback is INewLineFeedback)
                        {
                            feedback = (INewLineFeedback) SketchToolAssist.Feedback;
                            if (bool_0)
                            {
                                feedback.AddPoint(mPAnchorPoint);
                            }
                            IPolyline polyline = feedback.Stop();
                            if (polyline != null)
                            {
                                pointCollection = (IPointCollection) polyline;
                                if (pointCollection.PointCount >= 2)
                                {
                                    mPPointCollection = (IGeometry) pointCollection;
                                    if (SketchShareEx.m_LastPartGeometry != null &&
                                        SketchShareEx.m_LastPartGeometry is IPolyline)
                                    {
                                        mLastPartGeometry = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                                        mLastPartGeometry.AddGeometryCollection(polyline as IGeometryCollection);
                                        mPPointCollection = mLastPartGeometry as IGeometry;
                                        SketchShareEx.m_LastPartGeometry = null;
                                    }
                                }
                                SketchShareEx.m_pLastPoint1 = null;
                                SketchShareEx.m_pEndPoint1 = null;
                                SketchShareEx.m_totalLength = 0;
                                if ((mPPointCollection == null ? false : SketchShareEx.IsCreateParrel))
                                {
                                    object value = Missing.Value;
                                    object constructOffset = SketchShareEx.ConstructOffset;
                                    IConstructCurve polylineClass = new Polyline() as IConstructCurve;
                                    polylineClass.ConstructOffset(mPPointCollection as IPolycurve,
                                        SketchShareEx.m_offset, ref constructOffset, ref value);
                                    IGeometryCollection geometryCollection = new Polyline() as IGeometryCollection;
                                    geometryCollection.AddGeometryCollection(polylineClass as IGeometryCollection);
                                    polylineClass = new Polyline() as IConstructCurve;
                                    polylineClass.ConstructOffset(mPPointCollection as IPolycurve,
                                        -SketchShareEx.m_offset, ref constructOffset, ref value);
                                    geometryCollection.AddGeometryCollection(polylineClass as IGeometryCollection);
                                    mPPointCollection = geometryCollection as IGeometry;
                                }
                            }
                        }
                        else if (SketchToolAssist.Feedback is INewPolygonFeedback)
                        {
                            INewPolygonFeedback newPolygonFeedback = (INewPolygonFeedback) SketchToolAssist.Feedback;
                            if (bool_0)
                            {
                                newPolygonFeedback.AddPoint(mPAnchorPoint);
                            }
                            IPolygon polygon = newPolygonFeedback.Stop();
                            if (polygon != null)
                            {
                                pointCollection = (IPointCollection) polygon;
                                if (pointCollection.PointCount >= 3)
                                {
                                    mPPointCollection = (IGeometry) pointCollection;
                                    if (!(mPPointCollection as ITopologicalOperator).IsSimple)
                                    {
                                        (mPPointCollection as ITopologicalOperator).Simplify();
                                    }
                                    if (SketchShareEx.m_LastPartGeometry != null &&
                                        SketchShareEx.m_LastPartGeometry is IPolygon)
                                    {
                                        mLastPartGeometry = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                                        mLastPartGeometry.AddGeometryCollection(polygon as IGeometryCollection);
                                        mPPointCollection = mLastPartGeometry as IGeometry;
                                        SketchShareEx.m_LastPartGeometry = null;
                                    }
                                }
                                SketchToolAssist.m_pPointColn = null;
                            }
                        }
                        CreateFeatureTool.CreateFeature(mPPointCollection, iactiveView_0, ifeatureLayer_0);
                    }
                    else
                    {
                        SketchToolAssist.TempLine = SketchShareEx.m_pAnchorPoint;
                        SketchShareEx.m_bInUse = false;
                        if (SketchToolAssist.CurrentTask != null)
                        {
                            SketchToolAssist.CurrentTask.Excute();
                        }
                        SketchToolAssist.Feedback = null;
                        return;
                    }
                }
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147220936)
                {
                    Logger.Current.Error("", cOMException, "");
                }
                else
                {
                    MessageBox.Show("坐标值或量测值超出范围!", "创建要素", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, ifeatureLayer_0, null);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, ifeatureLayer_0, null);
                Logger.Current.Error("", exception, "");
            }
            SketchShareEx.m_bInUse = false;
            SketchShareEx.LastPoint = null;
            SketchToolAssist.Feedback = null;
            SketchShareEx.PointCount = 0;
            SketchShareEx.m_bSnapSuccessful = false;
            SketchToolAssist.m_pAP = null;
        }

        public static void EndSketch(IGeometry igeometry_0, IActiveView iactiveView_0, IFeatureLayer ifeatureLayer_0)
        {
            IGeometryCollection mLastPartGeometry;
            try
            {
                if (igeometry_0.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline igeometry0 = igeometry_0 as IPolyline;
                    if (SketchShareEx.m_LastPartGeometry != null && SketchShareEx.m_LastPartGeometry is IPolyline)
                    {
                        mLastPartGeometry = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                        mLastPartGeometry.AddGeometryCollection(igeometry0 as IGeometryCollection);
                        igeometry_0 = mLastPartGeometry as IGeometry;
                        SketchShareEx.m_LastPartGeometry = null;
                    }
                    SketchShareEx.m_pLastPoint1 = null;
                    SketchShareEx.m_pEndPoint1 = null;
                    SketchShareEx.m_totalLength = 0;
                    if ((igeometry_0 == null ? false : SketchShareEx.IsCreateParrel))
                    {
                        object value = Missing.Value;
                        object constructOffset = SketchShareEx.ConstructOffset;
                        IConstructCurve polylineClass = new Polyline() as IConstructCurve;
                        polylineClass.ConstructOffset(igeometry_0 as IPolycurve, SketchShareEx.m_offset,
                            ref constructOffset, ref value);
                        IGeometryCollection geometryCollection = new Polyline() as IGeometryCollection;
                        geometryCollection.AddGeometryCollection(polylineClass as IGeometryCollection);
                        polylineClass = new Polyline() as IConstructCurve;
                        polylineClass.ConstructOffset(igeometry_0 as IPolycurve, -SketchShareEx.m_offset,
                            ref constructOffset, ref value);
                        geometryCollection.AddGeometryCollection(polylineClass as IGeometryCollection);
                        igeometry_0 = geometryCollection as IGeometry;
                    }
                }
                else if (igeometry_0.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    IPolygon polygon = igeometry_0 as IPolygon;
                    if (SketchShareEx.m_LastPartGeometry != null && SketchShareEx.m_LastPartGeometry is IPolygon)
                    {
                        mLastPartGeometry = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                        mLastPartGeometry.AddGeometryCollection(polygon as IGeometryCollection);
                        igeometry_0 = mLastPartGeometry as IGeometry;
                        SketchShareEx.m_LastPartGeometry = null;
                    }
                    SketchToolAssist.m_pPointColn = null;
                }
                CreateFeatureTool.CreateFeature(igeometry_0, iactiveView_0, ifeatureLayer_0);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147220936)
                {
                    Logger.Current.Error("", cOMException, "");
                }
                else
                {
                    MessageBox.Show("坐标值或量测值超出范围!", "创建要素", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            iactiveView_0.Refresh();
            SketchShareEx.m_bInUse = false;
            SketchShareEx.LastPoint = null;
            SketchToolAssist.Feedback = null;
            SketchShareEx.PointCount = 0;
            SketchShareEx.m_pAnchorPoint = null;
        }

        private static void old_acctor_mc()
        {
            SketchShareEx.m_psnaper = new PointSnapper();
            SketchShareEx.m_CurrentStep = 0;
            SketchShareEx.m_SysGrants = new SysGrants(AppConfigInfo.UserID);
            SketchShareEx.m_pSketchCommandFolw = new SketchCommandFlow();
            SketchShareEx.IsFixLength = false;
            SketchShareEx.IsFixDirection = false;
            SketchShareEx.FixLength = 0;
            SketchShareEx.FixDirection = 0;
            SketchShareEx.StartPoint = null;
            SketchShareEx.LastPoint = null;
            SketchShareEx.IsCreateParrel = false;
            SketchShareEx.m_offset = 1;
            SketchShareEx.ConstructOffset = 5;
            SketchShareEx.PointCount = 0;
            SketchShareEx.m_pAnchorPoint = null;
            SketchShareEx.m_bInUse = false;
            SketchShareEx.m_pPointCollection = null;
            SketchShareEx.m_pLastPoint1 = null;
            SketchShareEx.m_pEndPoint1 = null;
            SketchShareEx.m_bShowVlaue = true;
            SketchShareEx.m_totalLength = 0;
            SketchShareEx.m_bSnapSuccessful = false;
            SketchShareEx.m_pSym = new SimpleMarkerSymbol();
            SketchShareEx.m_pPoint = null;
            SketchShareEx.m_HasLicense = false;
            SketchShareEx.m_LastPartGeometry = null;
            SketchShareEx.m_bInStreaming = false;
            SketchShareEx.m_MinDis = 0;
            SketchShareEx.m_HasLicense = EditorLicenseProviderCheck.Check();
            SketchShareEx.m_pSym.Style = esriSimpleMarkerStyle.esriSMSCircle;
            SketchShareEx.m_pSym.Size = 8;
            SketchShareEx.m_pSym.Outline = true;
            SketchShareEx.m_pSym.Color = ColorManage.GetRGBColor(0, 255, 255);
        }

        public static string SketchMouseDown(IPoint ipoint_0, IActiveView iactiveView_0, IFeatureLayer ifeatureLayer_0)
        {
            INewPolylineFeedback feedback;
            string str;
            INewPolygonFeedbackEx newPolygonFeedbackEx;
            object value;
            INewPolylineFeedback newPolylineFeedback;
            double num;
            string[] strArrays;
            double num1;
            double length;

            if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Line)
            {
                if (SketchToolAssist.Feedback != null)
                {
                    feedback = (INewPolylineFeedback) SketchToolAssist.Feedback;
                    feedback.AddPoint(SketchShareEx.m_pAnchorPoint);
                    object obj = Missing.Value;
                    object value1 = Missing.Value;
                    SketchToolAssist.m_pPointColn.AddPoint(SketchShareEx.m_pAnchorPoint, ref obj, ref value1);
                }
                else
                {
                    SketchShareEx.m_bInUse = true;
                    SketchToolAssist.Feedback = new NewPolylineFeedback();
                    feedback = (INewPolylineFeedback) SketchToolAssist.Feedback;
                    SketchToolAssist.Feedback.Display = iactiveView_0.ScreenDisplay;
                    feedback.ChangeLineType((enumLineType) SketchToolAssist.LineType);
                    feedback.Start(SketchShareEx.m_pAnchorPoint);
                    SketchToolAssist.m_pPointColn = new Polyline();
                    object obj1 = Missing.Value;
                    object value2 = Missing.Value;
                    SketchToolAssist.m_pPointColn.AddPoint(SketchShareEx.m_pAnchorPoint, ref obj1, ref value2);
                }
                str = "";
            }
            else if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Fill)
            {
                if (SketchToolAssist.Feedback != null)
                {
                    newPolygonFeedbackEx = (INewPolygonFeedbackEx) SketchToolAssist.Feedback;
                    newPolygonFeedbackEx.AddPoint(SketchShareEx.m_pAnchorPoint);
                    object obj2 = Missing.Value;
                    object value3 = Missing.Value;
                    SketchToolAssist.m_pPointColn.AddPoint(SketchShareEx.m_pAnchorPoint, ref obj2, ref value3);
                }
                else
                {
                    SketchShareEx.m_bInUse = true;
                    SketchToolAssist.Feedback = new NewPolygonFeedbackEx();
                    newPolygonFeedbackEx = (INewPolygonFeedbackEx) SketchToolAssist.Feedback;
                    SketchToolAssist.Feedback.Display = iactiveView_0.ScreenDisplay;
                    newPolygonFeedbackEx.ChangeLineType((enumLineType) SketchToolAssist.LineType);
                    newPolygonFeedbackEx.Start(SketchShareEx.m_pAnchorPoint);
                    SketchToolAssist.m_pPointColn = new Polygon();
                    object obj3 = Missing.Value;
                    object value4 = Missing.Value;
                    SketchToolAssist.m_pPointColn.AddPoint(SketchShareEx.m_pAnchorPoint, ref obj3, ref value4);
                }
                str = "";
            }
            else if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Point)
            {
                SketchToolAssist.TempLine = SketchShareEx.m_pAnchorPoint;
                str = "";
            }
            else if (ifeatureLayer_0 == null)
            {
                str = "";
            }
            else if (ifeatureLayer_0.FeatureClass != null)
            {
                string str1 = "";
                string unit = "";
                SketchShareEx.IsFixDirection = false;
                SketchShareEx.IsFixLength = false;
                if (SketchToolAssist.Feedback == null)
                {
                    if (ifeatureLayer_0.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        try
                        {
                            Editor.EditWorkspace.StartEditOperation();
                            IFeature feature = ifeatureLayer_0.FeatureClass.CreateFeature();
                            int num2 = 0;
                            object fieldValue = Editor.CurrentEditTemplate.GetFieldValue("SymbolID");
                            if (fieldValue != null)
                            {
                                num2 = Convert.ToInt32(fieldValue);
                            }
                            ITextElement textElement = CreateFeatureTool.MakeTextElement("文本", 0, ipoint_0, num2);
                            IAnnotationFeature2 annotationFeature2 = feature as IAnnotationFeature2;
                            annotationFeature2.LinkedFeatureID = -1;
                            annotationFeature2.AnnotationClassID = 0;
                            annotationFeature2.Annotation = textElement as IElement;
                            EditorEvent.NewRow(feature);
                            feature.Store();
                            Editor.EditWorkspace.StopEditOperation();
                            EditorEvent.AfterNewRow(feature);
                            iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            iactiveView_0.FocusMap.ClearSelection();
                            iactiveView_0.FocusMap.SelectFeature(ifeatureLayer_0, feature);
                            iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, "");
                        }
                    }
                    else if (ifeatureLayer_0.FeatureClass.FeatureType != esriFeatureType.esriFTDimension)
                    {
                        value = Missing.Value;
                        switch (ifeatureLayer_0.FeatureClass.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                            {
                                CreateFeatureTool.CreateFeature(ipoint_0, iactiveView_0, ifeatureLayer_0);
                                break;
                            }
                            case esriGeometryType.esriGeometryMultipoint:
                            {
                                SketchShareEx.m_bInUse = true;
                                SketchToolAssist.Feedback = new NewMultiPointFeedback();
                                INewMultiPointFeedback newMultiPointFeedback =
                                    (INewMultiPointFeedback) SketchToolAssist.Feedback;
                                SketchToolAssist.Feedback.Display = iactiveView_0.ScreenDisplay;
                                SketchShareEx.m_pPointCollection = new Multipoint();
                                newMultiPointFeedback.Start(SketchShareEx.m_pPointCollection, ipoint_0);
                                break;
                            }
                            case esriGeometryType.esriGeometryPolyline:
                            {
                                SketchShareEx.m_bInUse = true;
                                SketchToolAssist.Feedback = new NewPolylineFeedback();
                                newPolylineFeedback = (INewPolylineFeedback) SketchToolAssist.Feedback;
                                SketchToolAssist.Feedback.Display = iactiveView_0.ScreenDisplay;
                                newPolylineFeedback.Start(ipoint_0);
                                SketchShareEx.PointCount = 1;
                                SketchShareEx.StartPoint = ipoint_0;
                                SketchToolAssist.m_pPointColn = new Polyline();
                                SketchToolAssist.m_pPointColn.AddPoint(ipoint_0, ref value, ref value);
                                unit = CommonHelper.GetUnit(iactiveView_0.FocusMap.MapUnits);
                                num = CommonHelper.measureLength(ipoint_0, 1, ref SketchShareEx.m_pLastPoint1,
                                    ref SketchShareEx.m_pEndPoint1, ref SketchShareEx.m_totalLength);
                                strArrays = new string[]
                                {
                                    "距离 = ", num.ToString("0.###"), unit, ", 总长度 = ",
                                    SketchShareEx.m_totalLength.ToString("0.###"), unit
                                };
                                str1 = string.Concat(strArrays);
                                break;
                            }
                            case esriGeometryType.esriGeometryPolygon:
                            {
                                SketchShareEx.m_bInUse = true;
                                SketchToolAssist.Feedback = new NewPolygonFeedbackEx();
                                INewPolygonFeedbackEx feedback1 = (INewPolygonFeedbackEx) SketchToolAssist.Feedback;
                                SketchToolAssist.Feedback.Display = iactiveView_0.ScreenDisplay;
                                feedback1.Start(ipoint_0);
                                SketchShareEx.PointCount = 0;
                                SketchToolAssist.m_pPointColn = new Polygon();
                                SketchShareEx.StartPoint = ipoint_0;
                                unit = CommonHelper.GetUnit(iactiveView_0.FocusMap.MapUnits);
                                num1 = CommonHelper.measureArea(ipoint_0, 1, ref SketchToolAssist.m_pPointColn);
                                length = (SketchToolAssist.m_pPointColn as IPolygon).Length;
                                if (num1 <= 0)
                                {
                                    break;
                                }
                                str1 = string.Format("周长 = {0:0.###} {1} ，总面积 = {2:0.###} 平方{1}", length, unit, num1);
                                break;
                            }
                        }
                    }
                    else
                    {
                        SketchToolAssist.Feedback = new NewDimensionFeedback();
                        try
                        {
                            (SketchToolAssist.Feedback as INewDimensionFeedback).ReferenceScale =
                                (iactiveView_0 as IMap).ReferenceScale;
                            (SketchToolAssist.Feedback as INewDimensionFeedback).ReferenceScaleUnits =
                                (iactiveView_0 as IMap).MapUnits;
                        }
                        catch
                        {
                        }
                        SketchToolAssist.Feedback.Display = iactiveView_0.ScreenDisplay;
                        (SketchToolAssist.Feedback as INewDimensionFeedback).Start(ipoint_0);
                        SketchShareEx.PointCount = 1;
                    }
                }
                else if (SketchToolAssist.Feedback is INewDimensionFeedback)
                {
                    SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                    (SketchToolAssist.Feedback as INewDimensionFeedback).AddPoint(ipoint_0);
                    if (SketchShareEx.PointCount == 3)
                    {
                        IDimensionShape dimensionShape = (SketchToolAssist.Feedback as INewDimensionFeedback).Stop();
                        CreateFeatureTool.CreateDimensionFeature(dimensionShape,
                            (SketchToolAssist.Feedback as INewDimensionFeedback).DimensionType, iactiveView_0,
                            ifeatureLayer_0);
                    }
                }
                else if (SketchToolAssist.Feedback is INewMultiPointFeedback)
                {
                    value = Missing.Value;
                    SketchShareEx.m_pPointCollection.AddPoint(ipoint_0, ref value, ref value);
                    (SketchToolAssist.Feedback as INewMultiPointFeedback).Start(SketchShareEx.m_pPointCollection,
                        ipoint_0);
                }
                else if (SketchToolAssist.Feedback is INewLineFeedback)
                {
                    newPolylineFeedback = (INewPolylineFeedback) SketchToolAssist.Feedback;
                    value = Missing.Value;
                    newPolylineFeedback.AddPoint(ipoint_0);
                    SketchToolAssist.m_pPointColn.AddPoint(ipoint_0, ref value, ref value);
                    SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                    unit = CommonHelper.GetUnit(iactiveView_0.FocusMap.MapUnits);
                    num = CommonHelper.measureLength(ipoint_0, 1, ref SketchShareEx.m_pLastPoint1,
                        ref SketchShareEx.m_pEndPoint1, ref SketchShareEx.m_totalLength);
                    strArrays = new string[]
                    {
                        "距离 = ", num.ToString("0.###"), unit, ", 总长度 = ", SketchShareEx.m_totalLength.ToString("0.###"),
                        unit
                    };
                    str1 = string.Concat(strArrays);
                }
                else if (SketchToolAssist.Feedback is INewPolygonFeedback)
                {
                    ((INewPolygonFeedbackEx) SketchToolAssist.Feedback).AddPoint(ipoint_0);
                    SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                    unit = CommonHelper.GetUnit(iactiveView_0.FocusMap.MapUnits);
                    num1 = CommonHelper.measureArea(ipoint_0, 1, ref SketchToolAssist.m_pPointColn);
                    length = (SketchToolAssist.m_pPointColn as IPolygon).Length;
                    if (num1 > 0)
                    {
                        str1 = string.Format("周长 = {0:0.###} {1} ，总面积 = {2:0.###} 平方{1}", length, unit, num1);
                    }
                }
                if (SketchShareEx.LastPoint == null)
                {
                    SketchShareEx.LastPoint = new ESRI.ArcGIS.Geometry.Point();
                }
                SketchShareEx.LastPoint.PutCoords(ipoint_0.X, ipoint_0.Y);
                str = str1;
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static string SketchMouseMove(IPoint ipoint_0, IMap imap_0,
            IEngineSnapEnvironment iengineSnapEnvironment_0)
        {
            string str;
            string unit;
            if (imap_0 != null)
            {
                string str1 = "";
                SketchShareEx.m_pAnchorPoint = ipoint_0;
                SketchShareEx.Snap2Point(ipoint_0, SketchShareEx.m_pAnchorPoint, esriSimpleMarkerStyle.esriSMSDiamond,
                    imap_0 as IActiveView, iengineSnapEnvironment_0);
                if (SketchToolAssist.Feedback != null)
                {
                    SketchToolAssist.Feedback.MoveTo(SketchShareEx.m_pAnchorPoint);
                }
                if (SketchToolAssist.CurrentTask != null)
                {
                    str = "";
                }
                else if (SketchShareEx.m_bInUse)
                {
                    SketchShareEx.m_pPoint = new Point()
                    {
                        X = ipoint_0.X,
                        Y = ipoint_0.Y
                    };
                    if (SketchShareEx.m_bShowVlaue)
                    {
                        if (SketchToolAssist.Feedback is INewLineFeedback)
                        {
                            unit = CommonHelper.GetUnit(imap_0.MapUnits);
                            double num = CommonHelper.measureLength(ipoint_0, 2, ref SketchShareEx.m_pLastPoint1,
                                ref SketchShareEx.m_pEndPoint1, ref SketchShareEx.m_totalLength);
                            string[] strArrays = new string[]
                            {
                                "距离 = ", num.ToString("0.###"), unit, ", 总长度 = ",
                                SketchShareEx.m_totalLength.ToString("0.###"), unit
                            };
                            str1 = string.Concat(strArrays);
                        }
                        else if (SketchToolAssist.Feedback is INewPolygonFeedback)
                        {
                            unit = CommonHelper.GetUnit(imap_0.MapUnits);
                            double num1 = CommonHelper.measureArea(ipoint_0, 2, ref SketchToolAssist.m_pPointColn);
                            try
                            {
                                double length = (SketchToolAssist.m_pPointColn as IPolygon).Length;
                                if (num1 > 0)
                                {
                                    str1 = string.Format("周长 = {0:0.###} {1} ，总面积 = {2:0.###} 平方{1}", length, unit, num1);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    str = str1;
                }
                else
                {
                    str = "";
                }
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static void Snap2Point(IPoint ipoint_0, IPoint ipoint_1, esriSimpleMarkerStyle esriSimpleMarkerStyle_0,
            IActiveView iactiveView_0, IEngineSnapEnvironment iengineSnapEnvironment_0)
        {
            IHitTest mPPointColn;
            double num;
            int num1;
            int num2;
            bool flag;
            double mapUnits;
            IPoint pointClass;
            if (!Editor.UseOldSnap)
            {
                ISnappingResult snappingResult = SketchToolAssist.m_psnaper.Snap(ipoint_0);
                if (snappingResult == null)
                {
                    if (SketchToolAssist.m_pAP == null)
                    {
                        SketchToolAssist.m_pAP = new AnchorPoint()
                        {
                            Symbol = SketchShareEx.m_pSym as ISymbol
                        };
                    }
                    SketchToolAssist.m_pAP.MoveTo(SketchShareEx.m_pAnchorPoint, iactiveView_0.ScreenDisplay);
                }
                else
                {
                    SketchShareEx.m_pAnchorPoint = snappingResult.Location;
                    if (SketchToolAssist.m_pAP != null)
                    {
                        SketchToolAssist.m_pAP.MoveTo(snappingResult.Location, iactiveView_0.ScreenDisplay);
                    }
                    else
                    {
                        SketchToolAssist.AddNewAnchorPt(snappingResult.Location, esriSimpleMarkerStyle_0, iactiveView_0);
                    }
                }
            }
            else
            {
                SketchShareEx.m_pSym.Style = esriSimpleMarkerStyle_0;
                if (iengineSnapEnvironment_0 is ISnapEnvironment)
                {
                    ISnapEnvironment iengineSnapEnvironment0 = iengineSnapEnvironment_0 as ISnapEnvironment;
                    if ((iengineSnapEnvironment0 == null || !ApplicationRef.AppContext.Config.UseSnap
                        ? true
                        : !iengineSnapEnvironment0.SnapPoint(SketchShareEx.LastPoint, ipoint_0)))
                    {
                        if (ApplicationRef.AppContext.Config.IsSnapSketch)
                        {
                            mPPointColn = SketchToolAssist.m_pPointColn as IHitTest;
                            if (mPPointColn != null)
                            {
                                num = 0;
                                num1 = 0;
                                num2 = 0;
                                flag = false;
                                mapUnits = CommonHelper.ConvertPixelsToMapUnits(iactiveView_0,
                                    iengineSnapEnvironment0.SnapTolerance);
                                if (mapUnits == 0)
                                {
                                    mapUnits = 3;
                                }
                                pointClass = new ESRI.ArcGIS.Geometry.Point();
                                if (
                                    !mPPointColn.HitTest(ipoint_0, mapUnits,
                                        esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num, ref num1,
                                        ref num2, ref flag))
                                {
                                    SketchShareEx.m_bSnapSuccessful = false;
                                    if (SketchToolAssist.m_pAP != null)
                                    {
                                        SketchToolAssist.m_pAP.MoveTo(ipoint_0, iactiveView_0.ScreenDisplay);
                                    }
                                    else
                                    {
                                        SketchToolAssist.AddNewAnchorPt(ipoint_0, esriSimpleMarkerStyle_0, iactiveView_0);
                                    }
                                    return;
                                }
                                ipoint_0.PutCoords(pointClass.X, pointClass.Y);
                                SketchShareEx.m_bSnapSuccessful = true;
                                if (SketchToolAssist.m_pAP != null)
                                {
                                    SketchToolAssist.m_pAP.MoveTo(ipoint_0, iactiveView_0.ScreenDisplay);
                                    return;
                                }
                                else
                                {
                                    SketchToolAssist.AddNewAnchorPt(ipoint_0, esriSimpleMarkerStyle_0, iactiveView_0);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        SketchShareEx.m_bSnapSuccessful = true;
                        if (SketchToolAssist.m_pAP != null)
                        {
                            SketchToolAssist.m_pAP.MoveTo(ipoint_0, iactiveView_0.ScreenDisplay);
                        }
                        else
                        {
                            SketchToolAssist.AddNewAnchorPt(ipoint_0, esriSimpleMarkerStyle_0, iactiveView_0);
                        }
                    }
                }
                else if ((iengineSnapEnvironment_0 == null || !ApplicationRef.AppContext.Config.UseSnap
                    ? true
                    : !iengineSnapEnvironment_0.SnapPoint(ipoint_0)))
                {
                    if (ApplicationRef.AppContext.Config.IsSnapSketch)
                    {
                        mPPointColn = SketchToolAssist.m_pPointColn as IHitTest;
                        if (mPPointColn != null)
                        {
                            num = 0;
                            num1 = 0;
                            num2 = 0;
                            flag = false;
                            mapUnits = iengineSnapEnvironment_0.SnapTolerance;
                            if (iengineSnapEnvironment_0.SnapToleranceUnits ==
                                esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
                            {
                                mapUnits = CommonHelper.ConvertPixelsToMapUnits(iactiveView_0,
                                    iengineSnapEnvironment_0.SnapTolerance);
                            }
                            if (mapUnits == 0)
                            {
                                mapUnits = 3;
                            }
                            pointClass = new ESRI.ArcGIS.Geometry.Point();
                            if (
                                !mPPointColn.HitTest(ipoint_0, mapUnits, esriGeometryHitPartType.esriGeometryPartVertex,
                                    pointClass, ref num, ref num1, ref num2, ref flag))
                            {
                                SketchShareEx.m_bSnapSuccessful = false;
                                if (SketchToolAssist.m_pAP != null)
                                {
                                    SketchToolAssist.m_pAP.MoveTo(ipoint_0, iactiveView_0.ScreenDisplay);
                                }
                                else
                                {
                                    SketchToolAssist.AddNewAnchorPt(ipoint_0, esriSimpleMarkerStyle_0, iactiveView_0);
                                }
                                return;
                            }
                            ipoint_0.PutCoords(pointClass.X, pointClass.Y);
                            SketchShareEx.m_bSnapSuccessful = true;
                            if (SketchToolAssist.m_pAP != null)
                            {
                                SketchToolAssist.m_pAP.MoveTo(ipoint_0, iactiveView_0.ScreenDisplay);
                                return;
                            }
                            else
                            {
                                SketchToolAssist.AddNewAnchorPt(ipoint_0, esriSimpleMarkerStyle_0, iactiveView_0);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    SketchShareEx.m_bSnapSuccessful = true;
                    if (SketchToolAssist.m_pAP != null)
                    {
                        SketchToolAssist.m_pAP.MoveTo(ipoint_0, iactiveView_0.ScreenDisplay);
                    }
                    else
                    {
                        SketchToolAssist.AddNewAnchorPt(ipoint_0, esriSimpleMarkerStyle_0, iactiveView_0);
                    }
                }
            }
        }
    }
}