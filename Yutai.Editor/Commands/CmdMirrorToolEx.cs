using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdMirrorToolEx : YutaiTool
    {
        private ISymbol isymbol_0;

        private int int_0 = 0;

        private double double_0 = -1.0;

        private double double_1 = -1.0;

     
        private IPoint ipoint_0 = null;

        private IDisplayFeedback idisplayFeedback_0 = null;

        private IDisplayFeedback idisplayFeedback_1 = null;
        

        private bool bool_0 = false;

        private bool bool_1 = true;

        private IFeature ifeature_0 = null;

        private IPointCollection ipointCollection_0 = null;

        
        public override bool Enabled
        {
            get
            {
                return _context.FocusMap != null && _context.FocusMap.LayerCount != 0 && Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null && (Yutai.ArcGIS.Common.Editor.Editor.EditMap == null || Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap);
            }
        }

        public CmdMirrorToolEx(IAppContext context)
        {
            OnCreate(context);
        }

        public void ActiveCommand()
        {
            this.bool_1 = true;
            this.int_0 = 0;
            this.bool_0 = false;
            this.idisplayFeedback_0 = null;
            this.idisplayFeedback_1 = null;

        }

    
        public override void OnClick()
        {
            this.ActiveCommand();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "镜像工具";
            this.m_category = "编辑";
            this.m_toolTip = "镜像工具";
            this.m_name = "Edit_MirrorToolEx";

            this._key = "Edit_MirrorToolEx";
            this.m_message = "镜像工具";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Tool;

        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            if (int_0 == 27)
            {
                this.bool_1 = true;
                this.int_0 = 0;
                this.bool_0 = false;
                this.idisplayFeedback_0 = null;
                this.idisplayFeedback_1 = null;

            }
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            string str = mapPoint.X.ToString();
            double y = mapPoint.Y;
            string str1 = string.Concat(str, ",", y.ToString());
            _context.ShowCommandString("", CommandTipsType.CTTInput);
            TryPrase(mapPoint);
        }
       
        private void StartQuery(IEnvelope ienvelope_0)
        {
            ISpatialReference spatialReference = _context.FocusMap.SpatialReference;
            ienvelope_0.SpatialReference = spatialReference;
            IMap focusMap = _context.FocusMap;
            IActiveView activeView = focusMap as IActiveView;
            ISelectionEnvironment selectionEnvironment =_context.Config.SelectionEnvironment;
            selectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
            esriSpatialRelEnum linearSelectionMethod = selectionEnvironment.LinearSelectionMethod;
            esriSpatialRelEnum areaSelectionMethod = selectionEnvironment.AreaSelectionMethod;
            if (ienvelope_0 is IEnvelope)
            {
                if (this.double_0 != -1.0)
                {
                    if (this.double_0 > this.double_1)
                    {
                        selectionEnvironment.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
                        selectionEnvironment.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
                        this.double_1 = -1.0;
                    }
                    else
                    {
                        selectionEnvironment.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
                        selectionEnvironment.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                }
                else
                {
                    this.double_0 = -1.0;
                }
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            try
            {
                focusMap.SelectByShape(ienvelope_0, selectionEnvironment, false);
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467259)
                {
                    System.Windows.Forms.MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                }
                else
                {
                    //CErrorLog.writeErrorLog(this, ex, "");
                }
            }
            catch (Exception exception_)
            {
                //CErrorLog.writeErrorLog(this, exception_, "");
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            selectionEnvironment.LinearSelectionMethod = linearSelectionMethod;
            selectionEnvironment.AreaSelectionMethod = areaSelectionMethod;
        }
        private void TryPrase(IPoint point)
        {
            if (this.int_0 == 0)
            {
                    
                if (this.idisplayFeedback_0 != null)
                {
                    IEnvelope ienvelope_ = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                    this.idisplayFeedback_0 = null;
                    this.double_1 = point.X;
                    this.StartQuery(ienvelope_);
                    int_0 = 1;
                }
                else
                {
                    double num = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView, 6.0);
                    IFeatureLayer layer;
                    IFeature hitFeature = Yutai.ArcGIS.Common.Editor.Editor.GetHitFeature(_context.FocusMap, point, num, out layer);
                    if (hitFeature != null)
                    {
                        _context.FocusMap.SelectFeature(layer, this.ifeature_0);
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        int_0 = 1;
                    }
                    else
                    {
                        this.idisplayFeedback_0 = new NewEnvelopeFeedback();
                        this.idisplayFeedback_0.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
                        (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(point);
                        this.double_0 = point.X;
                            
                    }
                }
                return;
            }
            else if (this.int_0 == 1)
            {
               
                 
                this.idisplayFeedback_1 = new NewLineFeedback();
                this.idisplayFeedback_1.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
                (this.idisplayFeedback_1 as INewLineFeedback).Start(point);
                this.int_0 = 2;

                return;

            }
            else if (this.int_0 == 2)
            {
              
                (this.idisplayFeedback_1 as INewLineFeedback).AddPoint(point);
                IPolyline ipolyline_ = (this.idisplayFeedback_1 as INewLineFeedback).Stop();
                this.idisplayFeedback_1 = null;
                this.MirrorCopy(ipolyline_);
              
                this.bool_0 = true;
                return;
            }
        }


        private void MirrorCopy(IPolyline ipolyline_0)
        {
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
            enumFeature.Reset();
            for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
            {
                try
                {
                    IGeometry shape = GeometryOperator.Mirror(feature.Shape, ipolyline_0);
                    IRow row = RowOperator.CreatRowByRow(feature);
                    (row as IFeature).Shape = shape;
                    row.Store();
                }
                catch
                {
                }
            }
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
            (_context.FocusMap as IActiveView).Refresh();
        }



        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            if (this.idisplayFeedback_0 != null)
            {
                (this.idisplayFeedback_0 as INewEnvelopeFeedback).MoveTo(mapPoint);
            }
            else if (this.idisplayFeedback_1 != null)
            {
                (this.idisplayFeedback_1 as INewLineFeedback).MoveTo(mapPoint);
            }
        }
    }
}