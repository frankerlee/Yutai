using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSketchCircle : YutaiTool, IShapeConstructorTool
    {
        private IPoint ipoint_0;

        private bool bool_0;

        private IAnchorPoint ianchorPoint_0;

        private ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();


        public CmdSketchCircle(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "圆形工具";
            base.m_category = "Editor";
            base.m_bitmap = Properties.Resources.icon_sketch_circle;
            m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
            base.m_name = "Editor_Sketch_Circle";
            base._key = "Editor_Sketch_Circle";
            base.m_toolTip = "圆形工具";
            base._itemType = RibbonItemType.Tool;
            this.simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            this.simpleMarkerSymbol.Size = 8;
            this.simpleMarkerSymbol.Outline = true;
            this.simpleMarkerSymbol.Color = ColorManage.GetRGBColor(0, 255, 255);
        }

        public esriGeometryType GeometryType
        {
            get { return esriGeometryType.esriGeometryPolyline; }
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                         Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                    result = (featureLayer.FeatureClass != null &&
                              featureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTSimple &&
                              featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint &&
                              featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint);
                }
                else
                {
                    result = false;
                }
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnDblClick()
        {
        }

        public override void OnKeyDown(int int_0, int Shift)
        {
            if (int_0 == 27)
            {
                SketchToolAssist.Feedback = null;
                ((IActiveView) _context.FocusMap).Refresh();
            }
        }


        public override void OnMouseDown(int int_0, int Shift, int int_2, int int_3)
        {
            if (int_0 == 1)
            {
                IActiveView activeView = _context.FocusMap as IActiveView;
                IPoint mapPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (SketchToolAssist.Feedback != null)
                {
                    try
                    {
                        ICircularArc circularArc = (SketchToolAssist.Feedback as INewCircleFeedback).Stop();
                        ISegmentCollection polylineClass = null;
                        IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                        if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            polylineClass = new Polyline() as ISegmentCollection;
                        }
                        else if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                        {
                            return;
                        }
                        else
                        {
                            polylineClass = new Polygon() as ISegmentCollection;
                        }
                        object value = Missing.Value;
                        polylineClass.AddSegment(circularArc as ISegment, ref value, ref value);
                        CreateFeatureTool.CreateFeature(polylineClass as IGeometry, _context.FocusMap as IActiveView,
                            Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    }
                    catch (Exception exception)
                    {
                        //CErrorLog.writeErrorLog(this, exception, "");
                    }
                    SketchShareEx.m_bInUse = false;
                    SketchShareEx.LastPoint = null;
                    SketchShareEx.PointCount = 0;
                    SketchToolAssist.Feedback = null;
                    SketchShareEx.m_LastPartGeometry = null;
                }
                else
                {
                    SketchShareEx.m_bInUse = true;
                    SketchToolAssist.Feedback = new NewCircleFeedback();
                    SketchToolAssist.Feedback.Display = activeView.ScreenDisplay;
                    (SketchToolAssist.Feedback as INewCircleFeedback).Start(mapPoint);
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            SketchShareEx.SketchMouseMove(mapPoint, _context.FocusMap, _context.Config.SnapEnvironment);
            //base.OnMouseMove(Button, Shift, int_2, int_3);
        }
    }
}