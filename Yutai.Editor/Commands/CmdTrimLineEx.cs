using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdTrimLineEx : YutaiTool
    {
      

        private IPointCollection ipointCollection_0 = null;

        private double double_0 = -1.0;

        private double double_1 = -1.0;

        private int int_0 = 0;

        private ICommandFlow icommandFlow_0 = null;

        private IDisplayFeedback idisplayFeedback_0 = null;

        private IFeature ifeature_0 = null;

        private IPoint ipoint_0 = null;

        private bool bool_0 = false;

        private bool bool_1 = false;

        public override bool Enabled
        {
            get
            {
                bool flag;
                try
                {
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditMap == null)
                    {
                        flag = false;
                    }
                    else if ( _context.Config.CanEdited)
                    {
                        IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                        featureSelection.Reset();
                        IFeature feature = featureSelection.Next();
                        while (feature != null)
                        {
                            if (feature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                            {
                                flag = false;
                                return flag;
                            }
                            else if (Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset, "IsBeingEdited"))
                            {
                                flag = true;
                                return flag;
                            }
                            else
                            {
                                feature = featureSelection.Next();
                            }
                        }
                        flag = false;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch
                {
                    flag = false;
                }
                return flag;
            }
        }

        public CmdTrimLineEx(IAppContext context)
        {
            OnCreate(context);
        }

        public void ActiveCommand()
        {
            if (!this.Enabled)
            {
                _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                if ( !_context.Config.IsInEdit)
                {
                    _context.ShowCommandString("还未启动编辑，请先启动编辑", CommandTipsType.CTTUnKnown);
                }
            }
            else
            {
                _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                _context.ShowCommandString("裁剪工具", CommandTipsType.CTTInput);
                // this.trimCommandFlow_0.Reset();
                //  this.method_0();
            }
        }

        public override void OnClick()
        {
            this.ActiveCommand();
            _context.SetCurrentTool(this);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object object_0)
        {
            this.m_caption = "裁剪线";
            this.m_category = "编辑器";
            this.m_name = "Edit_TrimLineEx";
            this.m_message = "裁剪线";
            this.m_toolTip = "裁剪线";
            
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.ExtendLine.cur"));

            _context = object_0 as IAppContext;
            this._key = "Edit_TrimLineEx";
            
            this.m_bitmap = Properties.Resources.icon_edit_trimline;
            base._itemType = RibbonItemType.Tool;
            // this.trimCommandFlow_0.Application = object_0 as IApplication;
        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            if (int_0 == 27)
            {
                this.int_0 = 0;
                this.idisplayFeedback_0 = null;
                this.bool_0 = false;
                this.bool_1 = false;
            }
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            TryTrim(mapPoint);
        }

        private void TryTrim(IPoint point)
        {
            if (this.idisplayFeedback_0 != null)
            {
                IEnvelope ienvelope_ = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                this.idisplayFeedback_0 = null;
                this.double_1 = point.X;
                //this.iapplication_0.ShowCommandString("找到" + _context.FocusMap.SelectionCount.ToString() + "个对象", CommandTipsType.CTTLog);
                double double_ = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView, 6.0);
                System.Collections.Generic.IList<IFeature> intersectsLineFeatures = Yutai.ArcGIS.Common.Editor.Editor.GetIntersectsLineFeatures(_context.FocusMap, ienvelope_);
                IWorkspaceEdit workspaceEdit = null;
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
                        this.method_0(ienvelope_, double_, intersectsLineFeatures[i]);
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
                double double_ = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView, 6.0);
                IFeatureLayer layer;
                IFeature hitLineFeature = Yutai.ArcGIS.Common.Editor.Editor.GetHitLineFeature(_context.FocusMap, point, double_, out layer);
                if (hitLineFeature != null)
                {
                    IPolyline polyline = Yutai.ArcGIS.Common.Editor.Editor.TrimPolyLine(_context.FocusMap, hitLineFeature.Shape, point, double_);
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
                    // this.ShowCommandLine();
                }
                else
                {
                    this.idisplayFeedback_0 = new NewEnvelopeFeedback();
                    this.idisplayFeedback_0.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
                    (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(point);
                    //this.double_0 = point.X;
                    //this.iapplication_0.ShowCommandString("指定下一个角点:", CommandTipsType.CTTCommandTip);
                }
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            if (this.idisplayFeedback_0 != null)
            {
                (this.idisplayFeedback_0 as INewEnvelopeFeedback).MoveTo(mapPoint);
            }
        }

        private void method_0(IEnvelope ienvelope_0, double double_2, IFeature ifeature_1)
        {
            try
            {
                IPolyline polyline = Yutai.ArcGIS.Common.Editor.Editor.TrimPolyLine(_context.FocusMap, ifeature_1.Shape, ienvelope_0, double_2);
                if (polyline != null && !polyline.IsEmpty && (polyline as IPointCollection).PointCount > 1)
                {
                    (polyline as ITopologicalOperator).Simplify();
                    ifeature_1.Shape = polyline;
                    ifeature_1.Store();
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}