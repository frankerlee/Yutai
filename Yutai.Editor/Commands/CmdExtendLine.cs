using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdExtendLine : YutaiTool
    {
        private static double dblSearchRadius;
        private IFeature ifeature_0 = null;
        private IPointCollection ipointCollection_0 = null;
        private int int_0 = 0;
        private double double_0 = -1.0;
        private double double_1 = -1.0;
        private IDisplayFeedback idisplayFeedback_0 = null;
        private IPoint ipoint_0 = null;
        private int int_1 = 0;
        private IApplication iapplication_0 = null;
        private bool bool_0 = false;
        private bool bool_1 = false;
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
                    else if ( Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    {
                        result = false;
                    }
                    else if (_context.FocusMap.SelectionCount == 0)
                    {
                        result = false;
                    }
                    else
                    {
                        IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                        enumFeature.Reset();
                        for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
                        {
                            if (feature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                            {
                                result = false;
                                return result;
                            }
                            if (Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset, "IsBeingEdited"))
                            {
                                result = true;
                                return result;
                            }
                        }
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

      
        public CmdExtendLine(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object object_0)
        {
            this.m_category = "编辑器";
            this.m_caption = "延长线";
            this.m_name = "Edit_ExtendLineEx";
            this._key = "Edit_ExtendLineEx";
            _context = object_0 as IAppContext;
            this.m_message = "延长线";
            this.m_toolTip = "延长线";
            this.m_bitmap = Properties.Resources.icon_edit_extendline;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.ExtendLine.cur"));
          
        }

       
        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        
        public override void OnKeyDown(int int_1, int int_2)
        {
            if (int_1 == 27)
            {
                ifeature_0 = null;
                _context.MapControl.Map.ClearSelection();
                if (idisplayFeedback_0 != null) idisplayFeedback_0 = null;
            }
        }

        public override void OnMouseDown(int int_1, int int_2, int int_3, int int_4)
        {
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_3, int_4);
            TryExtend(mapPoint);
        }

        private void TryExtend(IPoint point)
        {
            bool isAddNode = false;
            if (this.idisplayFeedback_0 != null)
            {
                IEnvelope ienvelope_ = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                this.idisplayFeedback_0 = null;
                this.double_1 = point.X;
                //this.iapplication_0.ShowCommandString("找到" + _context.FocusMap.SelectionCount.ToString() + "个对象", CommandTipsType.CTTLog);
                double num = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView, 6.0);
                System.Collections.Generic.IList<IFeature> intersectsLineFeatures = Yutai.ArcGIS.Common.Editor.Editor.GetIntersectsLineFeatures(_context.FocusMap, ienvelope_);
                IWorkspaceEdit workspaceEdit = null;
                if (intersectsLineFeatures.Count > 0)
                {
                    isAddNode = MessageService.Current.Ask("在相交处产生交点吗?");
                }
                for (int i = 0; i < intersectsLineFeatures.Count; i++)
                {
                    IWorkspaceEdit workspaceEdit2 = (intersectsLineFeatures[i].Class as IDataset).Workspace as IWorkspaceEdit;
                    if (workspaceEdit2.IsBeingEdited())
                    {
                        if (workspaceEdit == null)
                        {
                            workspaceEdit = workspaceEdit2;
                            workspaceEdit.StartEditOperation();
                        }
                        IPolyline polyline;
                        if (isAddNode==false)
                        {
                            polyline = Yutai.ArcGIS.Common.Editor.Editor.ExtendPolyLine(_context.FocusMap, intersectsLineFeatures[i].Shape);
                        }
                        else
                        {
                            polyline = Yutai.ArcGIS.Common.Editor.Editor.ExtendPolyLineEx(_context.FocusMap, intersectsLineFeatures[i].Shape);
                        }
                        if (polyline != null && !polyline.IsEmpty && (polyline as IPointCollection).PointCount > 1)
                        {
                            (polyline as ITopologicalOperator).Simplify();
                            intersectsLineFeatures[i].Shape = polyline;
                            intersectsLineFeatures[i].Store();
                        }
                    }
                }
                if (workspaceEdit != null)
                {
                    workspaceEdit.StopEditOperation();
                    _context.ActiveView.Refresh();
                }
               
            }
            else
            {
                double num = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView, 6.0);
                IFeatureLayer layer;
                IFeature hitLineFeature = Yutai.ArcGIS.Common.Editor.Editor.GetHitLineFeature(_context.FocusMap, point, num, out layer);
                if (hitLineFeature != null)
                {
                  
                    isAddNode = MessageService.Current.Ask("在相交处产生交点吗?");
                   
                    IPolyline polyline;
                    if (isAddNode==false)
                    {
                        polyline = Yutai.ArcGIS.Common.Editor.Editor.ExtendPolyLine(_context.FocusMap, hitLineFeature.Shape);
                    }
                    else
                    {
                        polyline = Yutai.ArcGIS.Common.Editor.Editor.ExtendPolyLineEx(_context.FocusMap, hitLineFeature.Shape);
                    }
                    if (polyline != null && !polyline.IsEmpty && (polyline as IPointCollection).PointCount > 1)
                    {
                        (polyline as ITopologicalOperator).Simplify();
                        IWorkspaceEdit workspaceEdit = (hitLineFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                        workspaceEdit.StartEditOperation();
                        hitLineFeature.Shape = polyline;
                        hitLineFeature.Store();
                        workspaceEdit.StopEditOperation();
                        _context.ActiveView.Refresh();
                    }
                   
                }
                else
                {
                    this.idisplayFeedback_0 = new NewEnvelopeFeedback();
                    this.idisplayFeedback_0.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
                    (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(point);
                    this.double_0 = point.X;
                   
                }
            }
        }
        public override void OnMouseMove(int int_1, int int_2, int int_3, int int_4)
        {
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_3, int_4);
            if (this.idisplayFeedback_0 != null)
            {
                (this.idisplayFeedback_0 as INewEnvelopeFeedback).MoveTo(mapPoint);
            }
        }

       

    }
}