using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Path = ESRI.ArcGIS.Geometry.Path;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdFollowAlong : YutaiTool
    {
        private IPoint ipoint_0 = null;

        private ILine iline_0 = null;

        private ISegmentGraphCursor isegmentGraphCursor_0;

        private IConstructCurve iconstructCurve_0 = null;

        private ISimpleLineSymbol isimpleLineSymbol_0;

        private IPolyline ipolyline_0;

        private bool bool_0;

        private bool bool_1;

        private bool bool_2;

        private bool bool_3;

        private bool bool_4 = false;

        private int int_0 = 0;

        private double double_0 = 0.0;

        private int int_1 = 5;

        private ISegmentGraph isegmentGraph_0 = null;

        private bool bool_5 = true;

        private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;

        private bool bool_6 = false;

        private SnapHelper snapHelper_0;

        private IPoint ipoint_1 = null;

        private ISegmentCollection isegmentCollection_0 = null;

        public override bool Enabled
        {
            get
            {
                bool result;
                try
                {
                    if (_context.FocusMap == null)
                    {
                        result = false;
                    }
                    //else if (_context.CanEdited)
                    //{
                    //    result = false;
                    //}
                    else if (_context.FocusMap.SelectionCount == 0)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass != null && (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

 
     

        public CmdFollowAlong(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
           OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_caption = "跟踪";
            this.m_category = "编辑器";
            this.m_name = "Edit_FollowAlong";
            this._key = "Edit_FollowAlong";
            this.m_message = "跟踪";
            this.m_toolTip = "跟踪";
            this.m_bitmap = Properties.Resources.icon_edit_track;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resource.Digitise));
            this.bool_2 = false;
            this.iconstructCurve_0 = new Polyline() as IConstructCurve;
            this.isimpleLineSymbol_0 = new SimpleLineSymbol();
            this.isimpleLineSymbol_0.Width = 1.0;
            this.isegmentGraph_0 = new SegmentGraph();
           
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;

            if (_context is IApplicationEvents)
            {
                (_context as IApplicationEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.AppContext_ActiveHookChanged);
            }
            if (this.iactiveViewEvents_Event_0 != null)
            {
                try
                {
                    this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(ActiveView_AfterDraw);
                }
                catch
                {
                }
            }
            if (_context.FocusMap != null)
            {
                this.iactiveViewEvents_Event_0 = (_context.ActiveView as IActiveViewEvents_Event);
                this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(ActiveView_AfterDraw);
                this.snapHelper_0 = new SnapHelper(_context);
            }
        }

        public override void OnDblClick()
        {
            this.method_0();
        }

        public override void OnMouseDown(int int_2, int int_3, int int_4, int int_5)
        {
            if (int_2 == 1)
            {
                if (!this.bool_6)
                {
                    this.method_5();
                }
                if (this.bool_4)
                {
                    object value = Missing.Value;
                    object obj = this.int_1;
                    if (!this.bool_2)
                    {
                        this.ipoint_1 = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
                        this.isegmentGraphCursor_0 = this.isegmentGraph_0.GetCursor(this.ipoint_0);
                        this.ipolyline_0 = this.isegmentGraphCursor_0.CurrentTrace;
                        this.iconstructCurve_0.ConstructOffset(this.ipolyline_0, this.double_0, ref obj, ref value);
                        if (SketchShareEx.LastPoint != null && (this.iconstructCurve_0 as IPointCollection).PointCount > 0)
                        {
                            this.iline_0 = new Line();
                            this.iline_0.PutCoords(SketchShareEx.LastPoint, (this.iconstructCurve_0 as IPointCollection).get_Point(0));
                        }
                        this.method_4();
                        this.int_0 = 0;
                        this.bool_2 = true;
                    }
                    else
                    {
                        if (this.bool_5)
                        {
                            this.iline_0 = null;
                            this.ipolyline_0 = this.isegmentGraphCursor_0.CurrentTrace;
                            this.iconstructCurve_0.ConstructOffset(this.ipolyline_0, this.double_0, ref obj, ref value);
                        }
                        else
                        {
                            if (!this.isegmentGraphCursor_0.MoveTo(this.snapHelper_0.AnchorPoint))
                            {
                                this.isegmentGraphCursor_0.FinishMoveTo(this.snapHelper_0.AnchorPoint);
                            }
                            this.iline_0 = null;
                            this.ipolyline_0 = this.isegmentGraphCursor_0.CurrentTrace;
                            this.iconstructCurve_0.ConstructOffset(this.ipolyline_0, this.double_0, ref obj, ref value);
                        }
                        this.method_1();
                        this.method_2(this.iconstructCurve_0 as IPolyline);
                    }
                }
            }
        }

        public override void OnClick()
        {
            if (SketchToolAssist.Feedback != null)
            {
                SketchToolAssist.Feedback.Refresh(0);
            }
            _context.ShowCommandString("", CommandTipsType.CTTActiveEnd);
        }

        public override void OnMouseMove(int int_2, int int_3, int int_4, int int_5)
        {
            IPoint anchorPoint = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
            this.snapHelper_0.AnchorPoint = anchorPoint;
            IActiveView arg_3A_0 = _context.FocusMap as IActiveView;
            this.snapHelper_0.Snap2Point(anchorPoint, this.snapHelper_0.AnchorPoint, esriSimpleMarkerStyle.esriSMSDiamond);
            if (this.bool_5)
            {
                this.bool_1 = true;
                if (this.bool_2)
                {
                    this.int_0++;
                    if (this.int_0 >= 2)
                    {
                        this.int_0 = 0;
                        this.ipoint_0 = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
                        this.method_4();
                        if (!this.isegmentGraphCursor_0.MoveTo(this.ipoint_0))
                        {
                            this.isegmentGraphCursor_0.FinishMoveTo(this.ipoint_0);
                        }
                        this.ipolyline_0 = this.isegmentGraphCursor_0.CurrentTrace;
                        object value = Missing.Value;
                        object obj = this.int_1;
                        this.iconstructCurve_0.ConstructOffset(this.ipolyline_0, this.double_0, ref obj, ref value);
                        ISegmentCollection segmentCollection = this.iconstructCurve_0 as ISegmentCollection;
                        if (this.isegmentCollection_0 != null && this.isegmentCollection_0.SegmentCount != 0)
                        {
                            this.iline_0.PutCoords(this.ipoint_1, segmentCollection.get_Segment(0).FromPoint);
                            ISegment segment = this.iline_0 as ISegment;
                            segmentCollection.InsertSegments(0, 1, ref segment);
                        }
                        this.method_4();
                    }
                }
            }
        }

        private void method_0()
        {
            this.EndSketch();
            _context.ShowCommandString("完成", CommandTipsType.CTTInput);
        }

        public void CreateFeature(IGeometry igeometry_0)
        {
            try
            {
                if (igeometry_0 != null)
                {
                    IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                    IDataset dataset = (IDataset)featureLayer.FeatureClass;
                    IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)dataset.Workspace;
                    int index = featureLayer.FeatureClass.FindField(featureLayer.FeatureClass.ShapeFieldName);
                    IGeometryDef geometryDef = featureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
                    if (geometryDef.HasZ)
                    {
                        IZAware iZAware = (IZAware)igeometry_0;
                        iZAware.ZAware = true;
                        IZ iZ = (IZ)igeometry_0;
                        double constantZ;
                        double num;
                        geometryDef.SpatialReference.GetZDomain(out constantZ, out num);
                        iZ.SetConstantZ(constantZ);
                    }
                    if (geometryDef.HasM)
                    {
                        IMAware iMAware = (IMAware)igeometry_0;
                        iMAware.MAware = true;
                    }
                    workspaceEdit.StartEditOperation();
                    IFeature feature = featureLayer.FeatureClass.CreateFeature();
                    if (igeometry_0 is ITopologicalOperator)
                    {
                        (igeometry_0 as ITopologicalOperator).Simplify();
                    }
                    feature.Shape = igeometry_0;
                    try
                    {
                        IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
                        rowSubtypes.InitDefaultValues();
                    }
                    catch (Exception exception_)
                    {
                        CErrorLog.writeErrorLog(this, exception_, "");
                    }
                    feature.Store();
                    workspaceEdit.StopEditOperation();
                    EditorEvent.AddFeature(featureLayer, feature);
                    IActiveView activeView = (IActiveView)_context.FocusMap;
                    _context.FocusMap.ClearSelection();
                    _context.FocusMap.SelectFeature(featureLayer, feature);
                    if (igeometry_0.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        double distance = Common.ConvertPixelsToMapUnits((IActiveView)_context.FocusMap, 30.0);
                        ITopologicalOperator topologicalOperator = (ITopologicalOperator)igeometry_0;
                        topologicalOperator.Buffer(distance);
                        activeView.Refresh();
                    }
                    else
                    {
                        activeView.Refresh();
                    }
                }
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147220936)
                {
                    System.Windows.Forms.MessageBox.Show("坐标值或量测值超出范围!", "创建要素", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                }
            }
        }

        public void EndSketch()
        {
            try
            {
                IGeometry geometry = null;
                if (SketchToolAssist.Feedback is INewLineFeedback)
                {
                    INewLineFeedback newLineFeedback = (INewLineFeedback)SketchToolAssist.Feedback;
                    IPolyline polyline = newLineFeedback.Stop();
                    if (polyline != null)
                    {
                        IPointCollection pointCollection = (IPointCollection)polyline;
                        if (pointCollection.PointCount >= 2)
                        {
                            geometry = (IGeometry)pointCollection;
                            if (SketchShareEx.m_LastPartGeometry != null && SketchShareEx.m_LastPartGeometry is IPolyline)
                            {
                                IGeometryCollection geometryCollection = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                                geometryCollection.AddGeometryCollection(polyline as IGeometryCollection);
                                geometry = (geometryCollection as IGeometry);
                                SketchShareEx.m_LastPartGeometry = null;
                            }
                        }
                    }
                }
                else if (SketchToolAssist.Feedback is INewPolygonFeedback)
                {
                    INewPolygonFeedback newPolygonFeedback = (INewPolygonFeedback)SketchToolAssist.Feedback;
                    IPolygon polygon = newPolygonFeedback.Stop();
                    if (polygon != null)
                    {
                        IPointCollection pointCollection = (IPointCollection)polygon;
                        if (pointCollection.PointCount >= 3)
                        {
                            geometry = (IGeometry)pointCollection;
                            if (SketchShareEx.m_LastPartGeometry != null && SketchShareEx.m_LastPartGeometry is IPolygon)
                            {
                                IGeometryCollection geometryCollection = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                                geometryCollection.AddGeometryCollection(polygon as IGeometryCollection);
                                geometry = (geometryCollection as IGeometry);
                                SketchShareEx.m_LastPartGeometry = null;
                            }
                        }
                    }
                }
                if (geometry == null)
                {
                    return;
                }
                this.CreateFeature(geometry);
            }
            catch (Exception ex2)
            {
                COMException ex = ex2 as COMException;
                if (ex.ErrorCode == -2147220936)
                {
                    System.Windows.Forms.MessageBox.Show("坐标值或量测值超出范围!", "创建要素", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                }
                CErrorLog.writeErrorLog(this, ex2, "");
            }
            _context.ActiveView.Refresh();
            SketchShareEx.m_bInUse = false;
            SketchShareEx.LastPoint = null;
            SketchToolAssist.Feedback = null;
            SketchShareEx.PointCount = 0;
        }

        private void method_1()
        {
            if (this.isegmentGraphCursor_0 != null)
            {
                this.isegmentGraphCursor_0 = null;
            }
            if (this.isegmentGraph_0 != null)
            {
                this.isegmentGraph_0.SetEmpty();
            }
            this.bool_2 = false;
            this.bool_6 = false;
        }

        private void method_2(IPolyline ipolyline_1)
        {
            esriGeometryType shapeType = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType;
            if (SketchToolAssist.Feedback == null)
            {
                if (shapeType == esriGeometryType.esriGeometryPolyline)
                {
                    SketchToolAssist.Feedback = new NewPolylineFeedback();
                    SketchToolAssist.Feedback.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
                    SketchShareEx.m_bInUse = true;
                }
                else if (shapeType == esriGeometryType.esriGeometryPolygon)
                {
                    SketchToolAssist.Feedback = new NewPolygonFeedbackEx();
                    SketchToolAssist.Feedback.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
                    SketchShareEx.m_bInUse = true;
                }
            }
            if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
            {
                (SketchToolAssist.Feedback as INewPolygonFeedbackEx).AddPart(ipolyline_1);
                SketchShareEx.PointCount += (ipolyline_1 as IPointCollection).PointCount;
            }
            else if (SketchToolAssist.Feedback is INewPolylineFeedback)
            {
                (SketchToolAssist.Feedback as INewPolylineFeedback).AddPart(ipolyline_1);
                SketchShareEx.PointCount += (ipolyline_1 as IPointCollection).PointCount;
            }
            ISegmentCollection segmentCollection = null;
            this.bool_3 = true;
            if (this.isegmentCollection_0 == null)
            {
                if (shapeType == esriGeometryType.esriGeometryPolyline)
                {
                    this.isegmentCollection_0 = new Polyline() as ISegmentCollection;
                }
                else if (shapeType == esriGeometryType.esriGeometryPolygon)
                {
                    this.isegmentCollection_0 = new Polygon() as ISegmentCollection;
                }
            }
            if (shapeType == esriGeometryType.esriGeometryPolyline)
            {
                segmentCollection = new Path() as ISegmentCollection;
            }
            else if (shapeType == esriGeometryType.esriGeometryPolygon)
            {
                segmentCollection = new Ring() as ISegmentCollection;
            }
            IGeometryCollection geometryCollection = this.isegmentCollection_0 as IGeometryCollection;
            IGeometry geometry = segmentCollection as IGeometry;
            geometryCollection.InsertGeometries(-1, 1, ref geometry);
            if (shapeType == esriGeometryType.esriGeometryPolygon)
            {
                IPolygon polygon = this.isegmentCollection_0 as IPolygon;
                polygon.Close();
            }
            this.bool_3 = false;
        }

        private void method_3()
        {
            IInvalidArea invalidArea = new InvalidAreaClass
            {
                Display = _context.ActiveView.ScreenDisplay
            };
            invalidArea.Add(this.iconstructCurve_0);
            invalidArea.Invalidate(0);
        }

        private void method_4()
        {
            if (this.bool_2)
            {
                IDisplay screenDisplay = _context.ActiveView.ScreenDisplay;
                screenDisplay.StartDrawing(screenDisplay.hDC, -1);
                screenDisplay.SetSymbol(this.isimpleLineSymbol_0 as ISymbol);
                screenDisplay.DrawPolyline(this.iconstructCurve_0 as IGeometry);
                if (this.iline_0 != null)
                {
                    object value = Missing.Value;
                    IPolyline polyline = new Polyline() as IPolyline;
                    (polyline as ISegmentCollection).AddSegment(this.iline_0 as ISegment, ref value, ref value);
                    screenDisplay.DrawPolyline(polyline);
                }
                screenDisplay.FinishDrawing();
            }
        }

        private void method_5()
        {
            this.bool_6 = true;
            this.bool_1 = false;
            IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
            IGeometryCollection geometryCollection = new GeometryBag() as IGeometryCollection;
            enumFeature.Reset();
            IFeature feature = enumFeature.Next();
            object value = Missing.Value;
            while (feature != null)
            {
                esriGeometryType geometryType = feature.Shape.GeometryType;
                if (geometryType == esriGeometryType.esriGeometryPolygon || geometryType == esriGeometryType.esriGeometryPolyline)
                {
                    IClone clone = feature.Shape as IClone;
                    geometryCollection.AddGeometry(clone.Clone() as IGeometry, ref value, ref value);
                    this.bool_4 = true;
                }
                feature = enumFeature.Next();
            }
            if (this.bool_2)
            {
                this.isegmentGraph_0.SetEmpty();
                this.isegmentGraphCursor_0 = null;
            }
            this.isegmentGraph_0.Load(geometryCollection as IEnumGeometry, false, true);
            this.bool_2 = false;
        }

        private void ActiveView_AfterDraw(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
        {
            if (this.bool_2 && this.iconstructCurve_0 != null && (this.iconstructCurve_0 as IPointCollection).PointCount <= 1)
            {
                IDisplay screenDisplay = _context.ActiveView.ScreenDisplay;
                screenDisplay.StartDrawing(screenDisplay.hDC, -1);
                screenDisplay.SetSymbol(this.isimpleLineSymbol_0 as ISymbol);
                screenDisplay.DrawPolyline(this.iconstructCurve_0 as IGeometry);
                screenDisplay.FinishDrawing();
            }
        }

        public void Cancel()
        {
        }

        public void HandleCommandParameter(string string_0)
        {
        }

        public void ActiveCommand()
        {
            if (SketchToolAssist.Feedback != null)
            {
                SketchToolAssist.Feedback.Refresh(0);
            }
            _context.ShowCommandString("", CommandTipsType.CTTActiveEnd);
        }

        public override void OnKeyDown(int int_2, int int_3)
        {
            if (int_2 != 27)
            {
                switch (int_2)
                {
                    case 68:
                        {
                            frmTrackSet frmTrackSet = new frmTrackSet();
                            frmTrackSet.offset = this.double_0;
                            frmTrackSet.ConstructOffset = this.int_1;
                            if (frmTrackSet.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                this.double_0 = frmTrackSet.offset;
                                this.int_1 = frmTrackSet.ConstructOffset;
                                if (this.bool_2)
                                {
                                    this.ipolyline_0 = this.isegmentGraphCursor_0.CurrentTrace;
                                    object value = Missing.Value;
                                    object obj = esriConstructOffsetEnum.esriConstructOffsetSimple;
                                    this.iconstructCurve_0.ConstructOffset(this.ipolyline_0, this.double_0, ref obj, ref value);
                                    this.method_4();
                                }
                            }
                            break;
                        }
                    case 69:
                        break;
                    case 70:
                        {
                            this.iline_0 = null;
                            this.ipolyline_0 = this.isegmentGraphCursor_0.CurrentTrace;
                            object value = Missing.Value;
                            object obj = this.int_1;
                            this.iconstructCurve_0.ConstructOffset(this.ipolyline_0, this.double_0, ref obj, ref value);
                            this.method_1();
                            this.method_2(this.iconstructCurve_0 as IPolyline);
                            if (SketchToolAssist.Feedback != null)
                            {
                                this.method_0();
                            }
                            break;
                        }
                    default:
                        if (int_2 == 119)
                        {
                            this.bool_5 = !this.bool_5;
                        }
                        break;
                }
            }
            else
            {
                this.iline_0 = null;
                this.ipolyline_0 = null;
                this.iconstructCurve_0 = new Polyline() as IConstructCurve;
                IInvalidArea invalidArea = new InvalidAreaClass
                {
                    Display = _context.ActiveView.ScreenDisplay
                };
                invalidArea.Add(this.iconstructCurve_0);
                invalidArea.Invalidate(0);
                this.method_1();
                SketchShareEx.m_pLastPoint1 = null;
                SketchShareEx.m_pEndPoint1 = null;
                SketchShareEx.m_totalLength = 0.0;
                SketchShareEx.m_bInUse = false;
                SketchShareEx.LastPoint = null;
                SketchToolAssist.Feedback = null;
                SketchShareEx.PointCount = 0;
                SketchShareEx.m_bInUse = false;
                SketchShareEx.m_LastPartGeometry = null;
                _context.ActiveView.Refresh();
                string string_ = "取消创建要素";
                _context.ShowCommandString(string_, CommandTipsType.CTTInput);
                _context.ShowCommandString("", CommandTipsType.CTTActiveEnd);
            }
        }

        private void AppContext_ActiveHookChanged(object object_0)
        {
            if (this.iactiveViewEvents_Event_0 != null)
            {
                try
                {
                    this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(ActiveView_AfterDraw);
                }
                catch
                {
                }
                this.iactiveViewEvents_Event_0 = null;
            }
            try
            {
                this.iactiveViewEvents_Event_0 = (_context.ActiveView as IActiveViewEvents_Event);
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(ActiveView_AfterDraw);
                }
            }
            catch (Exception exception_)
            {
               // CErrorLog.writeErrorLog(this, exception_, "");
            }
        }
    }
}
