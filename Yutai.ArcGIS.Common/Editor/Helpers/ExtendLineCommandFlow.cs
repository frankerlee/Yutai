using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class ExtendLineCommandFlow : ICommandFlow
    {
        private double double_0 = -1;

        private double double_1 = -1;

        private int int_0 = 0;

        private ICommandFlow icommandFlow_0 = null;

        private IDisplayFeedback idisplayFeedback_0 = null;

        private IFeature ifeature_0 = null;

        private IPoint ipoint_0 = null;

        private int int_1 = 0;


        private bool bool_0 = false;

        private bool bool_1 = false;
        private IAppContext _appContext;


        public IAppContext AppContext
        {
            set { _appContext = value; }
        }

        public string CurrentCommandInfo
        {
            get { return ""; }
        }

        public bool IsFinished
        {
            get { return this.bool_0; }
        }

        public ExtendLineCommandFlow()
        {
        }

        public ExtendLineCommandFlow(ICommandFlow icommandFlow_1)
        {
            this.icommandFlow_0 = icommandFlow_1;
        }

        public bool HandleCommand(string string_0)
        {
            bool flag;
            char[] chrArray;
            string[] strArrays;
            double num;
            double num1;
            IPoint pointClass;
            IEnvelope envelope;
            int selectionCount;
            double mapUnits;
            IFeatureLayer featureLayer;
            IFeature hitLineFeature;
            IWorkspaceEdit workspace;
            IPolyline polyline;
            if (!this.bool_0)
            {
                try
                {
                    if (string_0 == "ESC")
                    {
                        this._appContext.ShowCommandString("取消延长线操作", CommandTipsType.CTTActiveEnd);
                        this._appContext.ClearCurrentTool();
                        this.bool_0 = true;
                        flag = true;
                        return flag;
                    }
                    else if (this.int_0 == 0)
                    {
                        string_0 = string_0.Trim();
                        if (string_0.Length != 0)
                        {
                            chrArray = new char[] {','};
                            strArrays = string_0.Split(chrArray);
                            if ((int) strArrays.Length < 2)
                            {
                                this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                                this.ShowCommandLine();
                            }
                            else
                            {
                                num = 0;
                                num1 = 0;
                                pointClass = new ESRI.ArcGIS.Geometry.Point();
                                num = Convert.ToDouble(strArrays[0]);
                                pointClass.PutCoords(num, Convert.ToDouble(strArrays[1]));
                                if (this.idisplayFeedback_0 == null)
                                {
                                    mapUnits =
                                        CommonHelper.ConvertPixelsToMapUnits(
                                            this._appContext.MapControl.Map as IActiveView, 6);
                                    hitLineFeature = Editor.GetHitLineFeature(this._appContext.MapControl.Map,
                                        pointClass, mapUnits, out featureLayer);
                                    if (hitLineFeature == null)
                                    {
                                        this.idisplayFeedback_0 = new NewEnvelopeFeedback()
                                        {
                                            Display = (this._appContext.MapControl.Map as IActiveView).ScreenDisplay
                                        };
                                        (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(pointClass);
                                        this.double_0 = pointClass.X;
                                        this._appContext.ShowCommandString("指定下一个角点:", CommandTipsType.CTTCommandTip);
                                    }
                                    else
                                    {
                                        this.int_0 = 1;
                                        this._appContext.MapControl.Map.SelectFeature(featureLayer, hitLineFeature);
                                        (this._appContext.MapControl.Map as IActiveView).PartialRefresh(
                                            esriViewDrawPhase.esriViewGeoSelection, null, null);
                                        this.ShowCommandLine();
                                    }
                                }
                                else
                                {
                                    envelope = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                                    this.idisplayFeedback_0 = null;
                                    this.double_1 = pointClass.X;
                                    this.method_2(envelope);
                                    if (this.method_1())
                                    {
                                        this.int_0 = 1;
                                    }
                                    IAppContext appContext = this._appContext;
                                    selectionCount = this._appContext.MapControl.Map.SelectionCount;
                                    appContext.ShowCommandString(string.Concat("找到", selectionCount.ToString(), "个对象"),
                                        CommandTipsType.CTTLog);
                                    this.ShowCommandLine();
                                }
                            }
                        }
                        else
                        {
                            if (this.method_1())
                            {
                                this.int_0 = 1;
                            }
                            this.ShowCommandLine();
                        }
                    }
                    else if (this.int_0 == 1)
                    {
                        if (string_0.Length == 0)
                        {
                            this.int_0 = 2;
                            if (this.int_1 != 1)
                            {
                                this._appContext.ShowCommandString("不产生交点", CommandTipsType.CTTLog);
                            }
                            else
                            {
                                this._appContext.ShowCommandString("产生交点", CommandTipsType.CTTLog);
                            }
                        }
                        else if (string_0.ToUpper() == "Y")
                        {
                            this.int_0 = 2;
                            this.int_1 = 1;
                            this._appContext.ShowCommandString("产生交点", CommandTipsType.CTTLog);
                        }
                        else if (string_0.ToUpper() != "N")
                        {
                            this.int_0 = 2;
                            if (this.int_1 != 1)
                            {
                                this._appContext.ShowCommandString("不产生交点", CommandTipsType.CTTLog);
                            }
                            else
                            {
                                this._appContext.ShowCommandString("产生交点", CommandTipsType.CTTLog);
                            }
                            this.HandleCommand(string_0);
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            this.int_0 = 2;
                            this.int_1 = 0;
                            this._appContext.ShowCommandString("不产生交点", CommandTipsType.CTTLog);
                        }
                        this.ShowCommandLine();
                    }
                    else if (this.int_0 == 2)
                    {
                        chrArray = new char[] {','};
                        strArrays = string_0.Split(chrArray);
                        if ((int) strArrays.Length >= 2)
                        {
                            num = 0;
                            num1 = 0;
                            pointClass = new ESRI.ArcGIS.Geometry.Point();
                            num = Convert.ToDouble(strArrays[0]);
                            pointClass.PutCoords(num, Convert.ToDouble(strArrays[1]));
                            if (this.idisplayFeedback_0 == null)
                            {
                                mapUnits =
                                    CommonHelper.ConvertPixelsToMapUnits(
                                        this._appContext.MapControl.Map as IActiveView, 6);
                                hitLineFeature = Editor.GetHitLineFeature(this._appContext.MapControl.Map, pointClass,
                                    mapUnits, out featureLayer);
                                if (hitLineFeature == null)
                                {
                                    this.idisplayFeedback_0 = new NewEnvelopeFeedback()
                                    {
                                        Display = (this._appContext.MapControl.Map as IActiveView).ScreenDisplay
                                    };
                                    (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(pointClass);
                                    this.double_0 = pointClass.X;
                                    this._appContext.ShowCommandString("指定下一个角点:", CommandTipsType.CTTCommandTip);
                                }
                                else
                                {
                                    polyline = null;
                                    polyline = (this.int_1 != 0
                                        ? Editor.ExtendPolyLineEx(this._appContext.MapControl.Map, hitLineFeature.Shape)
                                        : Editor.ExtendPolyLine(this._appContext.MapControl.Map, hitLineFeature.Shape));
                                    if (polyline != null && !polyline.IsEmpty &&
                                        (polyline as IPointCollection).PointCount > 1)
                                    {
                                        (polyline as ITopologicalOperator).Simplify();
                                        workspace = (hitLineFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                                        workspace.StartEditOperation();
                                        hitLineFeature.Shape = polyline;
                                        hitLineFeature.Store();
                                        workspace.StopEditOperation();
                                        this._appContext.MapControl.ActiveView.Refresh();
                                    }
                                    this.ShowCommandLine();
                                }
                            }
                            else
                            {
                                envelope = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                                this.idisplayFeedback_0 = null;
                                this.double_1 = pointClass.X;
                                IAppContext application = this._appContext;
                                selectionCount = this._appContext.MapControl.Map.SelectionCount;
                                application.ShowCommandString(string.Concat("找到", selectionCount.ToString(), "个对象"),
                                    CommandTipsType.CTTLog);
                                mapUnits =
                                    CommonHelper.ConvertPixelsToMapUnits(
                                        this._appContext.MapControl.Map as IActiveView, 6);
                                IList<IFeature> intersectsLineFeatures =
                                    Editor.GetIntersectsLineFeatures(this._appContext.MapControl.Map, envelope);
                                workspace = null;
                                for (int i = 0; i < intersectsLineFeatures.Count; i++)
                                {
                                    IWorkspaceEdit workspaceEdit =
                                        (intersectsLineFeatures[i].Class as IDataset).Workspace as IWorkspaceEdit;
                                    if (workspaceEdit.IsBeingEdited())
                                    {
                                        if (workspace == null)
                                        {
                                            workspace = workspaceEdit;
                                            workspace.StartEditOperation();
                                        }
                                        polyline = null;
                                        polyline = (this.int_1 != 0
                                            ? Editor.ExtendPolyLineEx(this._appContext.MapControl.Map,
                                                intersectsLineFeatures[i].Shape)
                                            : Editor.ExtendPolyLine(this._appContext.MapControl.Map,
                                                intersectsLineFeatures[i].Shape));
                                        if (polyline != null && !polyline.IsEmpty &&
                                            (polyline as IPointCollection).PointCount > 1)
                                        {
                                            (polyline as ITopologicalOperator).Simplify();
                                            intersectsLineFeatures[i].Shape = polyline;
                                            intersectsLineFeatures[i].Store();
                                        }
                                    }
                                }
                                if (workspace != null)
                                {
                                    workspace.StopEditOperation();
                                    this._appContext.MapControl.ActiveView.Refresh();
                                }
                                this.ShowCommandLine();
                            }
                        }
                    }
                }
                catch
                {
                    this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                    this.ShowCommandLine();
                    flag = true;
                    return flag;
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void method_0(IEnvelope ienvelope_0, double double_2, IFeature ifeature_1)
        {
            try
            {
                IPolyline polyline = Editor.TrimPolyLine(this._appContext.MapControl.Map, ifeature_1.Shape, ienvelope_0,
                    double_2);
                if (polyline != null && !polyline.IsEmpty && (polyline as IPointCollection).PointCount > 1)
                {
                    (polyline as ITopologicalOperator).Simplify();
                    ifeature_1.Shape = polyline;
                    ifeature_1.Store();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private bool method_1()
        {
            bool flag;
            IEnumFeature featureSelection = this._appContext.MapControl.Map.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            IFeature feature = featureSelection.Next();
            while (true)
            {
                if (feature == null)
                {
                    flag = false;
                    break;
                }
                else if (feature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                {
                    flag = false;
                    break;
                }
                else if (Editor.CheckWorkspaceEdit(feature.Class as IDataset, "IsBeingEdited"))
                {
                    flag = true;
                    break;
                }
                else
                {
                    feature = featureSelection.Next();
                }
            }
            return flag;
        }

        private void method_2(IEnvelope ienvelope_0)
        {
            IGeometry ienvelope0 = ienvelope_0;
            ienvelope0.SpatialReference = this._appContext.MapControl.Map.SpatialReference;
            IMap focusMap = this._appContext.MapControl.Map;
            IActiveView activeView = focusMap as IActiveView;
            ISelectionEnvironment selectionEnvironment = this._appContext.Config.SelectionEnvironment;
            selectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
            esriSpatialRelEnum linearSelectionMethod = selectionEnvironment.LinearSelectionMethod;
            esriSpatialRelEnum areaSelectionMethod = selectionEnvironment.AreaSelectionMethod;
            if (ienvelope0 is IEnvelope)
            {
                if (this.double_0 == -1)
                {
                    this.double_0 = -1;
                }
                else if (this.double_0 <= this.double_1)
                {
                    selectionEnvironment.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
                    selectionEnvironment.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    selectionEnvironment.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
                    selectionEnvironment.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
                    this.double_1 = -1;
                }
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            try
            {
                focusMap.SelectByShape(ienvelope0, selectionEnvironment, false);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147467259)
                {
                    Logger.Current.Error("", cOMException, "");
                }
                else
                {
                    MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            selectionEnvironment.LinearSelectionMethod = linearSelectionMethod;
            selectionEnvironment.AreaSelectionMethod = areaSelectionMethod;
        }

        public void OnMouseMove(IPoint ipoint_1)
        {
            if (this.idisplayFeedback_0 != null)
            {
                (this.idisplayFeedback_0 as INewEnvelopeFeedback).MoveTo(ipoint_1);
            }
        }

        public void Reset()
        {
            this.int_0 = 0;
            this.idisplayFeedback_0 = null;
            this.bool_0 = false;
            this.bool_1 = false;
        }

        public void ShowCommandLine()
        {
            if ((this.bool_1 || this.int_0 != 0 ? false : this.method_1()))
            {
                this.int_0 = 1;
            }
            this.bool_1 = true;
            switch (this.int_0)
            {
                case 0:
                {
                    this._appContext.ShowCommandString("请选择对象:", CommandTipsType.CTTCommandTip);
                    break;
                }
                case 1:
                {
                    this._appContext.ShowCommandString(
                        string.Concat("生成交点[Y/N]<", (this.int_1 == 0 ? "N" : "Y"), ">:"), CommandTipsType.CTTCommandTip);
                    break;
                }
                case 2:
                {
                    this._appContext.ShowCommandString("请选择要延长的对象:", CommandTipsType.CTTCommandTip);
                    break;
                }
            }
        }
    }
}