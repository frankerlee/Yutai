using System;
using System.IO;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdPointSplitLine : YutaiTool
    {
        private IPoint ipoint_0;

        private IPoint ipoint_1;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                         Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
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
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline &&
                            Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset,
                                "IsBeingEdited"))
                        {
                            result = true;
                            return result;
                        }
                    }
                    result = false;
                }
                return result;
            }
        }

        public CmdPointSplitLine(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "点打断线";
            this.m_name = "Edit_PointSplitLine";
            this.m_toolTip = "点打断线";
            this.m_category = "编辑器";
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resource.Digitise));

            this._key = "Edit_PointSplitLine";
            this.m_message = "镜像工具";
            this.m_bitmap = Properties.Resources.icon_edit_splittool;
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Tool;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IFeature feature;
            int i;
            IPoint point;
            if (int_0 == 1)
            {
                IActiveView focusMap = (IActiveView) _context.FocusMap;
                IPoint anchorPoint = SketchToolAssist.AnchorPoint;
                double mapUnits = Common.ConvertPixelsToMapUnits(_context.FocusMap as IActiveView,
                    _context.Config.EngineSnapEnvironment.SnapTolerance);
                Yutai.ArcGIS.Common.Editor.Editor.GetClosesFeature(_context.FocusMap, anchorPoint, mapUnits,
                    esriGeometryType.esriGeometryPolyline, out feature);
                if (feature == null)
                {
                    MessageService.Current.Warn("没有点击的要素上，请设置较大的捕捉范围!");
                }
                else
                {
                    IPolycurve2 shape = feature.Shape as IPolycurve2;
                    IEnvelope envelope = shape.Envelope;
                    object value = Missing.Value;
                    IHitTest hitTest = shape as IHitTest;
                    IPoint pointClass = new Point();
                    double num = 0;
                    int num1 = -1;
                    int num2 = -1;
                    bool flag = false;
                    if (hitTest.HitTest(anchorPoint, mapUnits, esriGeometryHitPartType.esriGeometryPartBoundary,
                        pointClass, ref num, ref num1, ref num2, ref flag))
                    {
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                        IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                        for (i = 0; i < num1; i++)
                        {
                            polylineClass.AddGeometry((shape as IGeometryCollection).Geometry[i], ref value, ref value);
                        }
                        IPointCollection geometry = (shape as IGeometryCollection).Geometry[num1] as IPointCollection;
                        IPointCollection pathClass = new ESRI.ArcGIS.Geometry.Path();
                        for (i = 0; i < num2 + 1; i++)
                        {
                            point = (geometry.Point[i] as IClone).Clone() as IPoint;
                            pathClass.AddPoint(point, ref value, ref value);
                        }
                        pathClass.AddPoint((pointClass as IClone).Clone() as IPoint, ref value, ref value);
                        polylineClass.AddGeometry(pathClass as IGeometry, ref value, ref value);
                        if ((polylineClass as IPointCollection).PointCount > 1)
                        {
                            Yutai.ArcGIS.Common.Editor.Editor.SetGeometryZM(polylineClass as IGeometry, shape);
                            feature.Shape = polylineClass as IGeometry;
                            feature.Store();
                        }
                        pathClass = new ESRI.ArcGIS.Geometry.Path();
                        pathClass.AddPoint((pointClass as IClone).Clone() as IPoint, ref value, ref value);
                        for (i = num2 + 1; i < geometry.PointCount; i++)
                        {
                            point = (geometry.Point[i] as IClone).Clone() as IPoint;
                            pathClass.AddPoint(point, ref value, ref value);
                        }
                        polylineClass = new Polyline() as IGeometryCollection;
                        polylineClass.AddGeometry(pathClass as IGeometry, ref value, ref value);
                        for (i = num1 + 1; i < (shape as IGeometryCollection).GeometryCount; i++)
                        {
                            polylineClass.AddGeometry((shape as IGeometryCollection).Geometry[i], ref value, ref value);
                        }
                        if ((polylineClass as IPointCollection).PointCount > 1)
                        {
                            IFeature feature1 = RowOperator.CreatRowByRow(feature as Row) as IFeature;
                            Yutai.ArcGIS.Common.Editor.Editor.SetGeometryZM(polylineClass as IGeometry, shape);
                            feature1.Shape = polylineClass as IGeometry;
                            feature1.Store();
                        }
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null,
                            envelope);
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                            null);
                    }
                }
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            this.ipoint_1 = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            SketchToolAssist.SketchMouseMove(this.ipoint_1);
            // base.OnMouseMove(int_0, int_1, int_2, int_3);
        }
    }
}