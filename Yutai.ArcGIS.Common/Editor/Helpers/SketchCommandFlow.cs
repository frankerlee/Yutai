using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class SketchCommandFlow : ICommandFlow
    {
        private string string_0 = "第一点";

        private int int_0 = 0;

        private ICommandFlow icommandFlow_0 = null;

        private string string_1 = "";

        private bool bool_0 = false;

        private IAppContext _appContext = null;

        private bool bool_1 = false;

        public IAppContext AppContext
        {
            set { this._appContext = value; }
        }

        public string CurrentCommandInfo
        {
            get { return this.string_0; }
        }

        public bool IsFinished
        {
            get { return this.bool_1; }
        }

        public SketchCommandFlow()
        {
        }

        public void Cancel()
        {
            if (this.int_0 != 0)
            {
                this.int_0 = 0;
                this._appContext.ShowCommandString("*取消创建要素*", CommandTipsType.CTTEnd);
                this._appContext.MapControl.ActiveView.Refresh();
                SketchToolAssist.Feedback = null;
                SketchShareEx.IsFixDirection = false;
                SketchShareEx.IsFixLength = false;
                SketchShareEx.LastPoint = null;
                SketchShareEx.PointCount = 0;
                SketchShareEx.m_pAnchorPoint = null;
                SketchShareEx.m_bInUse = false;
                this.ShowCommandLine();
            }
        }

        public void EndFlow()
        {
            if (!this.bool_0)
            {
                this.bool_0 = true;
                if (this.int_0 != 0)
                {
                    this.int_0 = 0;
                    this._appContext.ShowCommandString("*取消创建要素*", CommandTipsType.CTTEnd);
                    this._appContext.MapControl.ActiveView.Refresh();
                    SketchToolAssist.Feedback = null;
                    SketchShareEx.IsFixDirection = false;
                    SketchShareEx.IsFixLength = false;
                    SketchShareEx.LastPoint = null;
                    SketchShareEx.PointCount = 0;
                    SketchShareEx.m_pAnchorPoint = null;
                    SketchShareEx.m_bInUse = false;
                }
                this._appContext.ShowCommandString("*结束创建要素命令*", CommandTipsType.CTTEnd);
            }
        }

        public bool HandleCommand(string string_2)
        {
            bool flag;
            char[] chrArray;
            string[] strArrays;
            double num;
            double num1;
            double num2;
            IPoint pointClass;
            this.bool_0 = false;
            if (string_2 == "ESC")
            {
                this.icommandFlow_0 = null;
                this.Cancel();
                flag = true;
            }
            else if (this.int_0 != 0)
            {
                if (this.icommandFlow_0 != null)
                {
                    if (!this.icommandFlow_0.IsFinished)
                    {
                        flag = this.icommandFlow_0.HandleCommand(string_2);
                        return flag;
                    }
                    if ((this.icommandFlow_0 is ThreePointArcCommandFlow
                        ? true
                        : this.icommandFlow_0 is ConstructTangentArcCommandFlow))
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
                    this.icommandFlow_0 = null;
                }
                try
                {
                    this.string_1 = string_2.Trim();
                    if (this.string_1.Length != 0)
                    {
                        if (this.string_1.Length != 1)
                        {
                            string string1 = this.string_1;
                            chrArray = new char[] {','};
                            strArrays = string1.Split(chrArray);
                            this.string_1 = "";
                            num = 0;
                            num1 = 0;
                            num2 = 0;
                            pointClass = new ESRI.ArcGIS.Geometry.Point();
                            if ((int) strArrays.Length < 2)
                            {
                                this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                            }
                            else
                            {
                                SketchCommandFlow int0 = this;
                                int0.int_0 = int0.int_0 + 1;
                                num = Convert.ToDouble(strArrays[0]);
                                pointClass.PutCoords(num, Convert.ToDouble(strArrays[1]));
                                if ((int) strArrays.Length >= 3)
                                {
                                    pointClass.Z = Convert.ToDouble(strArrays[2]);
                                }
                                SketchShareEx.SketchMouseDown(pointClass, this._appContext.MapControl.Map as IActiveView,
                                    Editor.CurrentEditTemplate.FeatureLayer);
                            }
                        }
                        else
                        {
                            string lower = this.string_1.ToLower();
                            if (lower != null)
                            {
                                switch (lower)
                                {
                                    case "c":
                                    {
                                        if (SketchToolAssist.Feedback is INewPolylineFeedback)
                                        {
                                            (SketchToolAssist.Feedback as INewPolylineFeedback).Close();
                                        }
                                        this.method_0();
                                        break;
                                    }
                                    case "u":
                                    {
                                        if (SketchToolAssist.Feedback is IOperation)
                                        {
                                            (SketchToolAssist.Feedback as IOperation).Undo();
                                        }
                                        SketchCommandFlow sketchCommandFlow = this;
                                        sketchCommandFlow.int_0 = sketchCommandFlow.int_0 - 1;
                                        break;
                                    }
                                    case "r":
                                    {
                                        if (SketchToolAssist.Feedback is IOperation)
                                        {
                                            (SketchToolAssist.Feedback as IOperation).Redo();
                                        }
                                        SketchCommandFlow int01 = this;
                                        int01.int_0 = int01.int_0 + 1;
                                        break;
                                    }
                                    case "a":
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
                                        SketchCommandFlow sketchCommandFlow1 = this;
                                        sketchCommandFlow1.int_0 = sketchCommandFlow1.int_0 + 1;
                                        this.icommandFlow_0 = new ThreePointArcCommandFlow(this)
                                        {
                                            AppContext = this._appContext
                                        };
                                        break;
                                    }
                                    case "t":
                                    {
                                        if (SketchToolAssist.Feedback is INewPolylineFeedback)
                                        {
                                            (SketchToolAssist.Feedback as INewPolylineFeedback).ChangeLineType(
                                                enumLineType.LTTangentCircularArc);
                                        }
                                        if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
                                        {
                                            (SketchToolAssist.Feedback as INewPolygonFeedbackEx).ChangeLineType(
                                                enumLineType.LTTangentCircularArc);
                                        }
                                        SketchCommandFlow int02 = this;
                                        int02.int_0 = int02.int_0 + 1;
                                        this.icommandFlow_0 = new ConstructTangentArcCommandFlow(this)
                                        {
                                            AppContext = this._appContext
                                        };
                                        break;
                                    }
                                    case "j":
                                    {
                                        SketchCommandFlow sketchCommandFlow2 = this;
                                        sketchCommandFlow2.int_0 = sketchCommandFlow2.int_0 + 1;
                                        this.icommandFlow_0 = new GDCommandFlow()
                                        {
                                            AppContext = this._appContext
                                        };
                                        break;
                                    }
                                    case "g":
                                    {
                                        SketchCommandFlow int03 = this;
                                        int03.int_0 = int03.int_0 + 1;
                                        int pointCount = SketchToolAssist.m_pPointColn.PointCount;
                                        IPoint point = SketchToolAssist.m_pPointColn.Point[pointCount - 2];
                                        IPoint point1 = SketchToolAssist.m_pPointColn.Point[pointCount - 1];
                                        IPoint point2 = SketchToolAssist.m_pPointColn.Point[0];
                                        pointClass = CommonHelper.GetGD(point, point1, point2);
                                        SketchShareEx.SketchMouseDown(pointClass,
                                            this._appContext.MapControl.Map as IActiveView,
                                            Editor.CurrentEditTemplate.FeatureLayer);
                                        this.method_0();
                                        break;
                                    }
                                    case "d":
                                    {
                                        SketchCommandFlow sketchCommandFlow3 = this;
                                        sketchCommandFlow3.int_0 = sketchCommandFlow3.int_0 + 1;
                                        if (!(SketchToolAssist.Feedback is INewPolylineFeedback))
                                        {
                                            break;
                                        }
                                        pointClass =
                                            (SketchToolAssist.Feedback as INewPolylineFeedback).ReverseOrientation();
                                        if (pointClass == null)
                                        {
                                            break;
                                        }
                                        SketchShareEx.LastPoint = pointClass;
                                        break;
                                    }
                                    case "f":
                                    {
                                        SketchShareEx.EndSketch(false, this._appContext.MapControl.Map as IActiveView,
                                            Editor.CurrentEditTemplate.FeatureLayer);
                                        this.int_0 = 0;
                                        this._appContext.ShowCommandString("完成", CommandTipsType.CTTInput);
                                        this.Reset();
                                        Editor.EnableUndoRedo = true;
                                        flag = true;
                                        return flag;
                                    }
                                    default:
                                    {
                                        this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                                        this.string_1 = "";
                                        if (this.icommandFlow_0 != null)
                                        {
                                            this.icommandFlow_0.ShowCommandLine();
                                            flag = true;
                                            return flag;
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                            }

                            this.string_1 = "";
                            if (this.icommandFlow_0 != null)
                            {
                                this.icommandFlow_0.ShowCommandLine();
                                flag = true;
                                return flag;
                            }
                        }
                    }
                    else if (SketchToolAssist.Feedback == null)
                    {
                        flag = false;
                        return flag;
                    }
                    else if (this._appContext != null)
                    {
                        SketchShareEx.EndSketch(false, this._appContext.MapControl.Map as IActiveView,
                            Editor.CurrentEditTemplate.FeatureLayer);
                    }
                }
                catch
                {
                    this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                }
                this.ShowCommandLine();
                flag = true;
            }
            else
            {
                chrArray = new char[] {','};
                strArrays = string_2.Split(chrArray);
                this.string_1 = "";
                num = 0;
                num1 = 0;
                num2 = 0;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                try
                {
                    if ((int) strArrays.Length < 2)
                    {
                        this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                    }
                    else
                    {
                        num = Convert.ToDouble(strArrays[0]);
                        pointClass.PutCoords(num, Convert.ToDouble(strArrays[1]));
                        if ((int) strArrays.Length >= 3)
                        {
                            pointClass.Z = Convert.ToDouble(strArrays[2]);
                        }
                        SketchShareEx.SketchMouseDown(pointClass, this._appContext.MapControl.Map as IActiveView,
                            Editor.CurrentEditTemplate.FeatureLayer);
                        if ((Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType ==
                             esriGeometryType.esriGeometryPoint
                            ? false
                            : Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.FeatureType !=
                              esriFeatureType.esriFTAnnotation))
                        {
                            SketchCommandFlow int04 = this;
                            int04.int_0 = int04.int_0 + 1;
                        }
                    }
                }
                catch
                {
                    this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                }
                this.ShowCommandLine();
                flag = true;
            }
            return flag;
        }

        private void method_0()
        {
            SketchShareEx.EndSketch(false, this._appContext.MapControl.Map as IActiveView,
                Editor.CurrentEditTemplate.FeatureLayer);
            this.int_0 = 0;
            this._appContext.ShowCommandString("完成", CommandTipsType.CTTInput);
            this.Reset();
            Editor.EnableUndoRedo = true;
        }

        public void Reset()
        {
            this.int_0 = 0;
            this.icommandFlow_0 = null;
            this.ShowCommandLine();
        }

        public void ShowCommandLine()
        {
            esriGeometryType shapeType = Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType;
            switch (this.int_0)
            {
                case 0:
                {
                    if ((shapeType == esriGeometryType.esriGeometryPoint
                        ? false
                        : Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.FeatureType !=
                          esriFeatureType.esriFTAnnotation))
                    {
                        this._appContext.ShowCommandString("第一点:", CommandTipsType.CTTCommandTip);
                        break;
                    }
                    else
                    {
                        this._appContext.ShowCommandString("指定点:", CommandTipsType.CTTCommandTip);
                        break;
                    }
                }
                case 1:
                {
                    this._appContext.ShowCommandString("下一点或[撤销(U)/重做(R)/三点弧(A)]:", CommandTipsType.CTTCommandTip);
                    break;
                }
                case 2:
                {
                    this._appContext.ShowCommandString("下一点或[撤销(U)/重做(R)/三点弧(A)/切线弧(T)/隔一点(J)/隔点闭合(G)/结束(F)/线反向(D)]:",
                        CommandTipsType.CTTCommandTip);
                    break;
                }
                default:
                {
                    if (shapeType != esriGeometryType.esriGeometryPolygon)
                    {
                        this._appContext.ShowCommandString(
                            "下一点或[闭合(C)/撤销(U)/重做(R)/三点弧(A)/切线弧(T)/隔一点(J)/隔点闭合(G)/结束(F)/线反向(D)]:",
                            CommandTipsType.CTTCommandTip);
                        break;
                    }
                    else
                    {
                        this._appContext.ShowCommandString(
                            "下一点或[撤销(U)/重做(R)/三点弧(A)/切线弧(T)/隔一点(J)/隔点闭合(G)/结束(F)/线反向(D)]:",
                            CommandTipsType.CTTCommandTip);
                        break;
                    }
                }
            }
        }
    }
}