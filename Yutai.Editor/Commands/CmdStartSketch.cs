using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
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
using Cursor = System.Windows.Forms.Cursor;


namespace Yutai.Plugins.Editor.Commands
{
    public class CmdStartSketch : YutaiTool, IToolContextMenu
    {
        private ICommandFlow pCommandFlow;

        private ILayer ilayer_0 = null;

        private IPoint ipoint_0;

        private bool bool_0 = true;

        private IActiveViewEvents_Event pActiveViewEvents = null;

        private bool bool_1 = false;

        private IPoint ipoint_1 = null;

        private string string_0 = "";


        public string CommandLines
        {
            set { this.string_0 = value; }
        }

        public string CommandName
        {
            get { return "_Sketch"; }
        }


        public object ContextMenu
        {
            get
            {
                string[] strArrays = new string[]
                {
                    "SnapToSegment", "SnapToVertex", "SnapToMidPoint", "SnapToEndPoint", "_DirectionTool",
                    "_LengthTool", "_DirectionLengthTool", "_AbsoluteXYTool", "_DeltaXYTool", "CompletePartTool",
                    "DeleteSketchCommand"
                };
                return strArrays;
            }
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (SketchToolAssist.CurrentTask == null)
                {
                    this.m_enabled = false;
                    if (!SketchShareEx.m_HasLicense)
                    {
                        flag = false;
                    }
                    else if (_context.FocusMap == null)
                    {
                        flag = false;
                    }
                    else if (!Yutai.ArcGIS.Common.Editor.Editor.EnableSketch)
                    {
                        if (_context.CurrentTool == this)
                        {
                            _context.CurrentTool = null;
                        }
                        flag = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    {
                        if (Yutai.ArcGIS.Common.Editor.Editor.EditMap == null)
                        {
                            this.EndTool();
                        }
                        flag = false;
                    }
                    else if ((_context.FocusMap.LayerCount == 0 || Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null
                        ? false
                        : Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null))
                    {
                        this.m_enabled = true;
                        flag = true;
                    }
                    else
                    {
                        this.EndTool();
                        if (_context.CurrentTool == this)
                        {
                            _context.CurrentTool = null;
                        }
                        flag = false;
                    }
                }
                else
                {
                    flag = true;
                }
                return flag;
            }
        }


        public CmdStartSketch(IAppContext context)
        {
            _context = context;
            this.m_caption = "草图";
            this.m_toolTip = "创建要素";
            this.m_category = "编辑器";
            this.m_message = "创建要素";
            this.m_name = "Editor_Start_Sketch";
            this._key = "Editor_Start_Sketch";
            this._itemType = RibbonItemType.Tool;
            this.m_bitmap = Properties.Resources.icon_pencil;
            this.m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
            this.pCommandFlow = new SketchCommandFlow();
            SketchShareEx.m_pSketchCommandFolw = this.pCommandFlow as SketchCommandFlow;
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.pCommandFlow.AppContext = hook as IAppContext;
            if (hook is IApplicationEvents)
            {
                (hook as IApplicationEvents).OnCurrentLayerChange +=
                    new OnCurrentLayerChangeHandler(this.AppEvents_OnCurrentLayerChange);
                (hook as IApplicationEvents).OnActiveHookChanged +=
                    new OnActiveHookChangedHandler(this.AppEvents_OnActiveHookChanged);
            }
            this.pActiveViewEvents = _context.FocusMap as IActiveViewEvents_Event;
            try
            {
                if (this.pActiveViewEvents != null)
                {
                    this.pActiveViewEvents.AfterDraw +=
                        new IActiveViewEvents_AfterDrawEventHandler(this.ActiveViewEvent_AfterDraw);
                }
            }
            catch
            {
            }
        }

        public void ActiveCommand()
        {
            if (SketchToolAssist.CurrentTask == null)
            {
                if (SketchToolAssist.Feedback != null)
                {
                    this.pCommandFlow.ShowCommandLine();
                }
                else if (!this.Enabled)
                {
                    Yutai.ArcGIS.Common.Editor.Editor.EnableUndoRedo = true;
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    {
                        _context.ShowCommandString("还未启动编辑，请先启动编辑", CommandTipsType.CTTUnKnown);
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    {
                        _context.ShowCommandString("当前地图不可编辑", CommandTipsType.CTTUnKnown);
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer != null)
                    {
                        _context.ShowCommandString("不能处理该命令", CommandTipsType.CTTUnKnown);
                    }
                    else
                    {
                        _context.ShowCommandString("使用草图工具前，需要先设置当前层", CommandTipsType.CTTUnKnown);
                    }
                }
                else
                {
                    _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                    _context.ShowCommandString("草图工具", CommandTipsType.CTTInput);
                    _context.ShowCommandString(
                        string.Concat("在", Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.Name,
                            "创建要素"), CommandTipsType.CTTInput);
                    this.pCommandFlow.ShowCommandLine();
                }
            }
        }

        public void Cancel()
        {
            if (SketchToolAssist.Feedback != null)
            {
                _context.ShowCommandString("*取消创建要素*", CommandTipsType.CTTEnd);
                SketchToolAssist.Feedback = null;
                SketchShareEx.IsFixDirection = false;
                SketchShareEx.IsFixLength = false;
                SketchShareEx.LastPoint = null;
                SketchShareEx.PointCount = 0;
                SketchShareEx.m_pAnchorPoint = null;
                SketchShareEx.m_bInUse = false;
                _context.CurrentTool = null;
            }
        }

        public void EndTool()
        {
            if (SketchShareEx.m_pSketchCommandFolw != null)
            {
                SketchShareEx.m_pSketchCommandFolw.EndFlow();
            }
        }

        public void HandleCommandParameter(string string_1)
        {
            this.pCommandFlow.HandleCommand(string_1);
        }


        private void AppEvents_OnActiveHookChanged(object object_0)
        {
            if (this.pActiveViewEvents != null)
            {
                try
                {
                    this.pActiveViewEvents.AfterDraw -=
                        new IActiveViewEvents_AfterDrawEventHandler(this.ActiveViewEvent_AfterDraw);
                }
                catch
                {
                }
            }
            try
            {
                this.pActiveViewEvents = _context.FocusMap as IActiveViewEvents_Event;
                if (this.pActiveViewEvents != null)
                {
                    this.pActiveViewEvents.AfterDraw +=
                        new IActiveViewEvents_AfterDrawEventHandler(this.ActiveViewEvent_AfterDraw);
                }
            }
            catch
            {
            }
        }

        private bool ValidateLayer(IFeatureLayer pFeatureLayer)
        {
            bool flag;
            if (pFeatureLayer == null)
            {
                flag = false;
            }
            else if (pFeatureLayer.FeatureClass != null)
            {
                flag = ((pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline
                    ? false
                    : pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                    ? false
                    : true);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void method_2(IPoint ipoX)
        {
            string unit;
            double num;
            string[] str;
            string str1;
            double num1;
            string str2;
            SketchShareEx.IsFixDirection = false;
            SketchShareEx.IsFixLength = false;
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            if (!SketchShareEx.m_bInUse)
            {
                if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer != null)
                {
                    IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                    if (featureLayer.FeatureClass == null)
                    {
                        return;
                    }
                    if (featureLayer.FeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        switch (featureLayer.FeatureClass.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                            {
                                CreateFeatureTool.CreateFeature(ipoX, _context.FocusMap as IActiveView,
                                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                                break;
                            }
                            case esriGeometryType.esriGeometryMultipoint:
                            {
                                SketchShareEx.m_bInUse = true;
                                SketchToolAssist.Feedback = new NewMultiPointFeedback();
                                INewMultiPointFeedback feedback = (INewMultiPointFeedback) SketchToolAssist.Feedback;
                                SketchShareEx.m_pPointCollection = new Multipoint();
                                feedback.Start(SketchShareEx.m_pPointCollection, ipoX);
                                break;
                            }
                            case esriGeometryType.esriGeometryPolyline:
                            {
                                SketchShareEx.m_bInUse = true;
                                SketchToolAssist.Feedback = new NewLineFeedback();
                                ((INewLineFeedback) SketchToolAssist.Feedback).Start(ipoX);
                                SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                                if (!this.bool_0)
                                {
                                    break;
                                }
                                unit = Common.GetUnit(_context.FocusMap.MapUnits);
                                num = Common.measureLength(ipoX, 1, ref SketchShareEx.m_pLastPoint1,
                                    ref SketchShareEx.m_pEndPoint1, ref SketchShareEx.m_totalLength);
                                str = new string[]
                                {
                                    "距离 = ", num.ToString("0.###"), unit, ", 总长度 = ",
                                    SketchShareEx.m_totalLength.ToString("0.###"), unit
                                };
                                str1 = string.Concat(str);
                                _context.SetStatus(str1);
                                break;
                            }
                            case esriGeometryType.esriGeometryPolygon:
                            {
                                SketchShareEx.m_bInUse = true;
                                SketchToolAssist.Feedback = new NewPolygonFeedback();
                                ((INewPolygonFeedback) SketchToolAssist.Feedback).Start(ipoX);
                                SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                                if (!this.bool_0)
                                {
                                    break;
                                }
                                unit = string.Concat(" 平方", Common.GetUnit(_context.FocusMap.MapUnits));
                                num1 = Common.measureArea(ipoX, 1, ref SketchToolAssist.m_pPointColn);
                                if (num1 <= 0)
                                {
                                    break;
                                }
                                str2 = string.Concat("总面积 = ", num1.ToString("0.###"), unit);
                                _context.SetStatus(str2);
                                break;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                            IFeature feature = featureLayer.FeatureClass.CreateFeature();
                            ITextElement textElement = CreateFeatureTool.MakeTextElement("文本", 0, ipoX);
                            IAnnotationFeature2 annotationFeature2 = feature as IAnnotationFeature2;
                            annotationFeature2.LinkedFeatureID = -1;
                            annotationFeature2.AnnotationClassID = 0;
                            annotationFeature2.Annotation = textElement as IElement;
                            EditorEvent.NewRow(feature);
                            feature.Store();
                            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                            EditorEvent.AfterNewRow(feature);
                            focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            _context.FocusMap.ClearSelection();
                            _context.FocusMap.SelectFeature(featureLayer, feature);
                            focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        }
                        catch (Exception exception)
                        {
                            CErrorLog.writeErrorLog(this, exception, "");
                        }
                    }
                    if (SketchToolAssist.Feedback != null)
                    {
                        SketchToolAssist.Feedback.Display = focusMap.ScreenDisplay;
                    }
                }
                else
                {
                    return;
                }
            }
            else if (SketchToolAssist.Feedback is INewMultiPointFeedback)
            {
                object value = Missing.Value;
                SketchShareEx.m_pPointCollection.AddPoint(ipoX, ref value, ref value);
            }
            else if (SketchToolAssist.Feedback is INewLineFeedback)
            {
                ((INewLineFeedback) SketchToolAssist.Feedback).AddPoint(ipoX);
                SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                if (this.bool_0)
                {
                    unit = Common.GetUnit(_context.FocusMap.MapUnits);
                    num = Common.measureLength(ipoX, 1, ref SketchShareEx.m_pLastPoint1, ref SketchShareEx.m_pEndPoint1,
                        ref SketchShareEx.m_totalLength);
                    str = new string[]
                    {
                        "距离 = ", num.ToString("0.###"), unit, ", 总长度 = ", SketchShareEx.m_totalLength.ToString("0.###"),
                        unit
                    };
                    str1 = string.Concat(str);
                    _context.SetStatus(str1);
                }
            }
            else if (SketchToolAssist.Feedback is INewPolygonFeedback)
            {
                ((INewPolygonFeedback) SketchToolAssist.Feedback).AddPoint(ipoX);
                SketchShareEx.PointCount = SketchShareEx.PointCount + 1;
                if (this.bool_0)
                {
                    unit = string.Concat(" 平方", Common.GetUnit(_context.FocusMap.MapUnits));
                    num1 = Common.measureArea(ipoX, 1, ref SketchToolAssist.m_pPointColn);
                    if (num1 > 0)
                    {
                        str2 = string.Concat("总面积 = ", num1.ToString("0.###"), unit);
                        _context.SetStatus(str2);
                    }
                }
            }
            SketchShareEx.LastPoint = new ESRI.ArcGIS.Geometry.Point();
            SketchShareEx.LastPoint.PutCoords(ipoX.X, ipoX.Y);
        }

        private void SnapFeature(out IFeature pFeature)
        {
            pFeature = null;
            IFeatureCache featureCacheClass = new FeatureCache();
            featureCacheClass.Initialize(SketchShareEx.m_pAnchorPoint, 6);
            IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
            if (featureLayer.FeatureClass != null)
            {
                featureCacheClass.AddFeatures(featureLayer.FeatureClass);
                if (featureCacheClass.Count != 0)
                {
                    Yutai.ArcGIS.Common.Editor.Editor.GetClosestSelectedFeature(featureCacheClass,
                        SketchShareEx.m_pAnchorPoint, out pFeature);
                }
            }
        }

        private void AppEvents_OnCurrentLayerChange(ILayer ilayer_1, ILayer ilayer_2)
        {
            if (this.m_enabled)
            {
                if (SketchToolAssist.Feedback != null)
                {
                    _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                    if (ilayer_1 != null)
                    {
                        _context.ShowCommandString(string.Concat("取消在", ilayer_1.Name, "上未创建完的要素"),
                            CommandTipsType.CTTLog);
                    }
                    this.pCommandFlow.Reset();
                    SketchToolAssist.Feedback = null;
                    SketchShareEx.m_bInUse = false;
                }
                if (SketchToolAssist.Feedback == null && _context.CurrentTool == this)
                {
                    _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                    if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer != null &&
                        this.IsEditableLayer(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer,
                            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace as IWorkspace))
                    {
                        _context.ShowCommandString(
                            string.Concat("切换到", Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.Name,
                                "上创建要素"), CommandTipsType.CTTInput);
                        this.ShowCommandLine();
                    }
                }
            }
        }

        private bool IsEditableLayer(IFeatureLayer pFeatureLayer, IWorkspace pWorkspace)
        {
            bool flag;
            if (pWorkspace != null)
            {
                IWorkspace workspace = (pFeatureLayer.FeatureClass as IDataset).Workspace;
                if (workspace.ConnectionProperties.IsEqual(pWorkspace.ConnectionProperties))
                {
                    if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        flag = true;
                        return flag;
                    }
                    else
                    {
                        if (!(workspace is IVersionedWorkspace) ||
                            !(pFeatureLayer.FeatureClass as IVersionedObject).IsRegisteredAsVersioned)
                        {
                            flag = false;
                            return flag;
                        }
                        flag = true;
                        return flag;
                    }
                }
            }
            flag = false;
            return flag;
        }

        private void ActiveViewEvent_AfterDraw(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
        {
            if (this.Enabled && esriViewDrawPhase_0 == esriViewDrawPhase.esriViewForeground)
            {
                SketchShareEx.Draw(idisplay_0);
                if (SketchToolAssist.Feedback != null)
                {
                    SketchToolAssist.Feedback.Refresh(0);
                }
            }
        }

        public override void OnClick()
        {
            if (SketchToolAssist.CurrentTask == null)
            {
                _context.SetCurrentTool(this);
                this.ActiveCommand();
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnDblClick()
        {
            if (SketchToolAssist.Feedback != null)
            {
                if (SketchToolAssist.CurrentTask == null)
                {
                    SketchShareEx.EndSketch(false, _context.FocusMap as IActiveView,
                        Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    _context.ShowCommandString("完成", CommandTipsType.CTTInput);
                    this.pCommandFlow.Reset();
                    Yutai.ArcGIS.Common.Editor.Editor.EnableUndoRedo = true;
                    _context.UpdateUI();
                }
                else
                {
                    SketchShareEx.EndSketch(false, _context.FocusMap as IActiveView, null);
                }
            }
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            IEnvelope extent;
            if (keyCode != 16) //shift
            {
                if (keyCode == 72) //H
                {
                    this.bool_0 = !this.bool_0;
                    _context.SetStatus("");
                }
                else if (keyCode == 49) //1
                {
                    if (SketchToolAssist.Feedback is INewPolylineFeedback)
                    {
                        (SketchToolAssist.Feedback as INewPolylineFeedback).ChangeLineType(enumLineType.LTLine);
                    }
                    if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
                    {
                        (SketchToolAssist.Feedback as INewPolygonFeedbackEx).ChangeLineType(enumLineType.LTLine);
                    }
                }
                else if (keyCode == 67) //C
                {
                    if (SketchToolAssist.Feedback is INewPolylineFeedback)
                    {
                        (SketchToolAssist.Feedback as INewPolylineFeedback).Close();
                    }
                }
                else if (keyCode == 68) //D
                {
                    if (SketchToolAssist.Feedback is INewPolylineFeedback)
                    {
                        IPoint point = (SketchToolAssist.Feedback as INewPolylineFeedback).ReverseOrientation();
                        if (point != null)
                        {
                            SketchShareEx.LastPoint = point;
                        }
                    }
                }
                else if (keyCode == 70) //F
                {
                    if (SketchToolAssist.Feedback != null)
                    {
                        this.OnDblClick();
                    }
                }
                else if (keyCode == 119) //F8
                {
                    StreamingFlag streamingFlag = new StreamingFlag(_context);
                    if (streamingFlag.Enabled)
                    {
                        streamingFlag.OnClick();
                    }
                }
                else if (keyCode == 79) //O
                {
                    CmdSquareFinishTool squareAndFinishCommand = new CmdSquareFinishTool(_context);
                    if (squareAndFinishCommand.Enabled)
                    {
                        squareAndFinishCommand.OnClick();
                    }
                }
                else if (keyCode == 81) //Q
                {
                    CmdReverseLineOrientation reverseLineOrientationCommand = new CmdReverseLineOrientation(_context);

                    if (reverseLineOrientationCommand.Enabled)
                    {
                        reverseLineOrientationCommand.OnClick();
                    }
                }
                else if (keyCode == 83) //S
                {
                    if (SketchToolAssist.m_pPointColn is IPolygon && SketchToolAssist.m_pPointColn.PointCount > 2)
                    {
                        object value = Missing.Value;
                        IPolygon polygon = (SketchToolAssist.m_pPointColn as IClone).Clone() as IPolygon;
                        (polygon as IPointCollection).AddPoint(SketchShareEx.m_pAnchorPoint, ref value, ref value);
                        polygon.Close();
                        frmStaticByGeometry _frmStaticByGeometry = new frmStaticByGeometry()
                        {
                            Polygon = polygon,
                            FocusMap = _context.FocusMap
                        };
                        _frmStaticByGeometry.ShowDialog();
                    }
                }
                else if (keyCode != 84) //T
                {
                    if (keyCode == 50) //2
                    {
                        if (SketchToolAssist.Feedback is INewPolylineFeedback)
                        {
                            (SketchToolAssist.Feedback as INewPolylineFeedback).ChangeLineType(
                                enumLineType.LTCircularArc);
                        }
                        if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
                        {
                            (SketchToolAssist.Feedback as INewPolygonFeedbackEx).ChangeLineType(
                                enumLineType.LTCircularArc);
                        }
                    }
                    else if (keyCode == 51) //3
                    {
                        SketchShareEx.IsCreateParrel = !SketchShareEx.IsCreateParrel;
                    }
                    else if (!(keyCode == 80 ? false : keyCode != 113))
                    {
                        if (SketchShareEx.m_pAnchorPoint != null)
                        {
                            extent = (_context.FocusMap as IActiveView).Extent;
                            extent.CenterAt(SketchShareEx.m_pAnchorPoint);
                            (_context.FocusMap as IActiveView).Extent = extent;
                            (_context.FocusMap as IActiveView).Refresh();
                        }
                    }
                    else if (!(keyCode == 45 ? false : keyCode != 189))
                    {
                        extent = (_context.FocusMap as IActiveView).Extent;
                        extent.Expand(1.5, 1.5, true);
                        (_context.FocusMap as IActiveView).Extent = extent;
                        (_context.FocusMap as IActiveView).Refresh();
                    }
                    else if (!(keyCode == 43 ? false : keyCode != 187))
                    {
                        extent = (_context.FocusMap as IActiveView).Extent;
                        extent.Expand(0.5, 0.5, true);
                        (_context.FocusMap as IActiveView).Extent = extent;
                        (_context.FocusMap as IActiveView).Refresh();
                    }
                    else if (keyCode == 8)
                    {
                        if (SketchToolAssist.Feedback is IOperation)
                        {
                            (SketchToolAssist.Feedback as IOperation).Undo();
                        }
                    }
                    else if (keyCode == 52)
                    {
                        if ((SketchToolAssist.Feedback is INewPolylineFeedback
                            ? true
                            : SketchToolAssist.Feedback is NewPolygonFeedbackEx))
                        {
                            this.bool_1 = !this.bool_1;
                        }
                    }
                    else if (keyCode == 53)
                    {
                        if ((SketchToolAssist.Feedback is INewPolylineFeedback
                            ? true
                            : SketchToolAssist.Feedback is NewPolygonFeedbackEx))
                        {
                            frmSetUndoStep _frmSetUndoStep = new frmSetUndoStep();
                            if (_frmSetUndoStep.ShowDialog() == DialogResult.OK)
                            {
                                if (SketchToolAssist.Feedback is INewPolylineFeedback)
                                {
                                    (SketchToolAssist.Feedback as NewPolylineFeedback).UndoToStep(_frmSetUndoStep.Step);
                                }
                                if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
                                {
                                    (SketchToolAssist.Feedback as NewPolygonFeedbackEx).UndoToStep(_frmSetUndoStep.Step);
                                }
                            }
                        }
                    }
                    else if (keyCode == 27)
                    {
                        SketchShareEx.m_pLastPoint1 = null;
                        SketchShareEx.m_pEndPoint1 = null;
                        SketchShareEx.m_totalLength = 0;
                        SketchShareEx.m_bInUse = false;
                        SketchShareEx.LastPoint = null;
                        SketchToolAssist.Feedback = null;
                        SketchShareEx.PointCount = 0;
                        SketchShareEx.m_bInUse = false;
                        SketchShareEx.m_LastPartGeometry = null;
                        _context.ActiveView.Refresh();
                        _context.ShowCommandString("取消创建要素", CommandTipsType.CTTInput);
                        this.pCommandFlow.Reset();
                    }
                }
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            string str;
            if (Button == 1)
            {
                IActiveView focusMap = (IActiveView) _context.FocusMap;
                if (SketchToolAssist.CurrentTask == null)
                {
                    this.ipoint_0 = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    if (SketchToolAssist.Feedback == null)
                    {
                        this.bool_1 = false;
                    }
                    if (this.bool_1)
                    {
                        bool point = false;
                        if (this.ipoint_1 != null)
                        {
                            if (SketchToolAssist.Feedback is NewPolylineFeedback)
                            {
                                point = (SketchToolAssist.Feedback as NewPolylineFeedback).UndoToPoint(this.ipoint_1);
                            }
                            else if (SketchToolAssist.Feedback is NewPolygonFeedbackEx)
                            {
                                point = (SketchToolAssist.Feedback as NewPolygonFeedbackEx).UndoToPoint(this.ipoint_1);
                            }
                        }
                        if (point)
                        {
                            this.bool_1 = false;
                            this.m_cursor.Dispose();
                            this.m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
                        }
                    }
                    else if ((!SketchShareEx.m_bInStreaming
                        ? true
                        : !this.ValidateLayer(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer)))
                    {
                        string str1 = SketchShareEx.m_pAnchorPoint.X.ToString();
                        double y = SketchShareEx.m_pAnchorPoint.Y;
                        string str2 = string.Concat(str1, ",", y.ToString());
                        _context.ShowCommandString(str2, CommandTipsType.CTTInput);
                        if (SketchToolAssist.CurrentTask == null)
                        {
                            if (this.pCommandFlow == null)
                            {
                                str = SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, focusMap,
                                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                                if (this.bool_0)
                                {
                                    _context.SetStatus(str);
                                }
                            }
                            else
                            {
                                Yutai.ArcGIS.Common.Editor.Editor.EnableUndoRedo = false;
                                this.pCommandFlow.HandleCommand(str2);
                            }
                            _context.UpdateUI();
                        }
                        else
                        {
                            str = SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, focusMap, null);
                        }
                    }
                }
                else
                {
                    SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, focusMap, null);
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            double x;
            double y;
            double num;
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            if (SketchToolAssist.CurrentTask != null)
            {
                SketchShareEx.SketchMouseMove(mapPoint, _context.FocusMap, _context.Config.EngineSnapEnvironment);
            }
            else if (this.bool_1)
            {
                double searchTolerance = 8;

                searchTolerance = (double) _context.Config.SelectionEnvironment.SearchTolerance;

                searchTolerance = Common.ConvertPixelsToMapUnits((IActiveView) _context.FocusMap, searchTolerance);
                bool flag = false;
                if (SketchToolAssist.Feedback is NewPolylineFeedback)
                {
                    flag = (SketchToolAssist.Feedback as NewPolylineFeedback).HitTest(mapPoint, searchTolerance,
                        out this.ipoint_1);
                }
                else if (SketchToolAssist.Feedback is NewPolygonFeedbackEx)
                {
                    flag = (SketchToolAssist.Feedback as NewPolygonFeedbackEx).HitTest(mapPoint, searchTolerance,
                        out this.ipoint_1);
                }
                if (!flag)
                {
                    this.m_cursor.Dispose();
                    this.m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
                }
                else
                {
                    this.m_cursor.Dispose();
                    this.m_cursor =
                        new Cursor(
                            base.GetType()
                                .Assembly.GetManifestResourceStream(
                                    "Yutai.Plugins.Editor.Resources.Cursor.DeleteVertex.cur"));
                }
            }
            else if ((!SketchShareEx.m_bInStreaming
                ? true
                : !this.ValidateLayer(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer)))
            {
                if (SketchShareEx.IsFixDirection)
                {
                    x = mapPoint.X - SketchShareEx.LastPoint.X;
                    y = mapPoint.Y - SketchShareEx.LastPoint.Y;
                    num = Common.azimuth(SketchShareEx.LastPoint, mapPoint);
                    double num1 = Math.Sqrt(x*x + y*y);
                    double num2 = num1*Math.Cos(SketchShareEx.FixDirection*3.14159265358979/180);
                    double num3 = num1*Math.Sin(SketchShareEx.FixDirection*3.14159265358979/180);
                    if (!(SketchShareEx.FixDirection < 0 ? true : SketchShareEx.FixDirection >= 90))
                    {
                        if ((num < 90 + SketchShareEx.FixDirection ? false : num < 270 + SketchShareEx.FixDirection))
                        {
                            num2 = -num2;
                            num3 = -num3;
                        }
                    }
                    else if (!(SketchShareEx.FixDirection < 90 ? true : SketchShareEx.FixDirection >= 270))
                    {
                        if ((num < SketchShareEx.FixDirection - 90 ? true : num >= SketchShareEx.FixDirection + 90))
                        {
                            num2 = -num2;
                            num3 = -num3;
                        }
                    }
                    else if ((num < SketchShareEx.FixDirection - 270 ? false : num < SketchShareEx.FixDirection - 90))
                    {
                        num2 = -num2;
                        num3 = -num3;
                    }
                    x = SketchShareEx.LastPoint.X + num2;
                    y = SketchShareEx.LastPoint.Y + num3;
                    mapPoint.PutCoords(x, y);
                }
                else if (SketchShareEx.IsFixLength)
                {
                    num = Common.azimuth(SketchShareEx.LastPoint, mapPoint);
                    x = SketchShareEx.FixLength*Math.Cos(num*3.14159265358979/180);
                    y = SketchShareEx.FixLength*Math.Sin(num*3.14159265358979/180);
                    x = SketchShareEx.LastPoint.X + x;
                    y = SketchShareEx.LastPoint.Y + y;
                    mapPoint.PutCoords(x, y);
                }
                string str = SketchShareEx.SketchMouseMove(mapPoint, _context.FocusMap,
                    _context.Config.EngineSnapEnvironment);
                if (this.bool_0)
                {
                    _context.SetStatus(str);
                }
            }
            else if (Common.distance(mapPoint, SketchShareEx.LastPoint) > SketchShareEx.m_MinDis)
            {
                string str1 = SketchShareEx.m_pAnchorPoint.X.ToString();
                double y1 = SketchShareEx.m_pAnchorPoint.Y;
                string str2 = string.Concat(str1, ",", y1.ToString());
                _context.ShowCommandString(str2, CommandTipsType.CTTInput);
                this.ShowCommandLine();
                string str3 = SketchShareEx.SketchMouseDown(mapPoint, _context.ActiveView,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                if (this.bool_0)
                {
                    _context.SetStatus(str3);
                }
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (SketchShareEx.m_bInUse && SketchToolAssist.Feedback is INewTextFeedback)
            {
                SketchShareEx.m_bInUse = false;
                ((INewTextFeedback) SketchToolAssist.Feedback).Stop();
                IActiveView focusMap = (IActiveView) _context.FocusMap;
                focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            }
        }

        protected void ShowCommandLine()
        {
            this.pCommandFlow.ShowCommandLine();
        }

        public string[] ContextMenuKeys
        {
            get
            {
                string[] keys = new string[]
                {
                    "Edit_Snap_SnapSegment", "Edit_Snap_SnapVertex", "Edit_Snap_SnapMidPoint", "Edit_Snap_SnapEndPoint",
                    "-", "Edit_DirectionTool", "Edit_LengthTool", "Edit_DirectionLengthTool",
                    "Edit_AbsoluteXYTool", "Edit_DeltaXYTool", "Edit_CompletePartTool",
                    "Edit_SquareFinishTool", "Edit_DeleteSketchTool", "Edit_CompleteSketchTool"
                };

                return keys;
            }
        }
    }
}