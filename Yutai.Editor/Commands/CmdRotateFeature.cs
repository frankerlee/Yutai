using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Path = ESRI.ArcGIS.Geometry.Path;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdRotateFeature : YutaiTool
    {
        private bool bool_0;

        private IAnchorPoint ianchorPoint_0;

        private ISimpleMarkerSymbol isimpleMarkerSymbol_0 = new SimpleMarkerSymbol();

        private IPoint ipoint_0;

        private System.Windows.Forms.Cursor cursor_0;

        private System.Windows.Forms.Cursor cursor_1;

        private SnapHelper snapHelper_0 = null;

        private IRotateTrackerFeedback irotateTrackerFeedback_0 = null;

        private IPoint ipoint_1 = null;

        private bool bool_1 = false;

        private bool bool_2 = false;

        public override bool Enabled
        {
            get
            {
                return _context.FocusMap != null && _context.FocusMap.LayerCount != 0 &&
                       ( Yutai.ArcGIS.Common.Editor.Editor.EditMap == null ||
                        Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap) &&
                       (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null && _context.FocusMap.SelectionCount > 0);
            }
        }




        public CmdRotateFeature(IAppContext context)
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
            this.m_caption = "旋转要素";
            this.m_category = "编辑器";
            this.m_name = "Edit_RotateFeature";
            this._key = "Edit_RotateFeature";
            this.m_message = "旋转要素";
            this.m_toolTip = "旋转要素";
            this.m_bitmap = Properties.Resources.icon_edit_rotate;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resource.Digitise));
            this.bool_2 = false;

            this.cursor_0 =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.RotateFeature.cur"));

            this.isimpleMarkerSymbol_0.Style = esriSimpleMarkerStyle.esriSMSCircle;
            this.isimpleMarkerSymbol_0.Size = 8.0;
            this.isimpleMarkerSymbol_0.Outline = true;
            this.isimpleMarkerSymbol_0.Color = ColorManage.GetRGBColor(0, 255, 255);
            this.cursor_1 =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.VertexEdit.cur"));
            this.m_cursor = this.cursor_0;

            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
            this.snapHelper_0 = new SnapHelper(_context.FocusMap as IActiveView,
                _context.Config.EngineSnapEnvironment);

        }

        public override void OnClick()
        {
            IActiveView activeView = _context.ActiveView;
            try
            {
                this.irotateTrackerFeedback_0 = new RotateTrackFeedback();
                this.irotateTrackerFeedback_0.Display = activeView.ScreenDisplay;
            }
            catch
            {
            }
        }

       

        public override void OnMouseDown(int int_2, int int_3, int int_4, int int_5)
        {
            if (this.irotateTrackerFeedback_0 != null)
            {
                IActiveView activeView = _context.ActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (this.ipoint_0 != null)
                {
                    point.PutCoords(this.ipoint_0.X, this.ipoint_0.Y);
                }
                this.irotateTrackerFeedback_0.ClearGeometry();
                this.irotateTrackerFeedback_0.Origin = EditTools.SelectionSetAnchorPoint;
                this.bool_1 = true;
                this.irotateTrackerFeedback_0.Start(point);
                IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                enumFeature.Reset();
                for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
                {
                    this.irotateTrackerFeedback_0.AddGeometry(feature.ShapeCopy);
                }
            }
        }



        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.irotateTrackerFeedback_0 != null)
            {
                IActiveView activeView = _context.FocusMap as IActiveView;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (int_0 == 0)
                {
                    double num = _context.Config.EngineSnapEnvironment.SnapTolerance;
                    if (_context.Config.EngineSnapEnvironment.SnapToleranceUnits ==
                        esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
                    {
                        num = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView, num);
                    }
                    double num2 = (point as IProximityOperator).ReturnDistance(EditTools.SelectionSetAnchorPoint);
                    if (num2 < num)
                    {
                        this.bool_2 = true;
                        this.m_cursor = this.cursor_1;
                        return;
                    }
                    this.bool_2 = false;
                }
                else if (int_0 == 1 && this.bool_2)
                {
                    EditTools.SelectionSetAnchorPoint.PutCoords(point.X, point.Y);
                    return;
                }
                this.m_cursor = this.cursor_0;
                this.ipoint_0 = point;
                if (int_0 == 1 && this.irotateTrackerFeedback_0 != null && this.bool_1)
                {
                    this.irotateTrackerFeedback_0.MoveTo(this.ipoint_0);
                }
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.irotateTrackerFeedback_0 != null)
            {
                if (int_0 == 1 && this.bool_2)
                {
                    _context.ActiveView.Refresh();
                }
                else if (this.bool_1)
                {
                    this.bool_1 = false;
                    if (this.irotateTrackerFeedback_0 != null)
                    {
                        this.irotateTrackerFeedback_0.ClearGeometry();
                        if (this.irotateTrackerFeedback_0.Angle != 0)
                        {
                            IEnvelope envelope = null;
                            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                            featureSelection.Reset();
                            for (IFeature i = featureSelection.Next(); i != null; i = featureSelection.Next())
                            {
                                ITransform2D shapeCopy = i.ShapeCopy as ITransform2D;
                                if (envelope != null)
                                {
                                    envelope.Union((shapeCopy as IGeometry).Envelope);
                                }
                                else
                                {
                                    envelope = (shapeCopy as IGeometry).Envelope;
                                }
                                if (shapeCopy != null)
                                {
                                    shapeCopy.Rotate(this.irotateTrackerFeedback_0.Origin,
                                        this.irotateTrackerFeedback_0.Angle);
                                    Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation(i.Class as IDataset);
                                    try
                                    {
                                        i.Shape = shapeCopy as IGeometry;
                                        i.Store();
                                        envelope.Union((shapeCopy as IGeometry).Envelope);
                                    }
                                    catch (Exception exception)
                                    {
                                        CErrorLog.writeErrorLog(this, exception, "");
                                    }
                                    Yutai.ArcGIS.Common.Editor.Editor.StopEditOperation(i.Class as IDataset);
                                }
                            }
                            if (envelope != null)
                            {
                                double mapUnits = Common.ConvertPixelsToMapUnits(_context.ActiveView, 10);
                                envelope.Expand(mapUnits, mapUnits, false);
                                _context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null,
                                    envelope);
                            }
                            _context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                                null);
                            this.irotateTrackerFeedback_0.Origin = null;
                        }
                    }
                }
            }

        }
    }
}