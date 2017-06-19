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
    public class CmdSketchRectangle : YutaiTool, IShapeConstructorTool
    {
        private IPoint ipoint_0;
        private string string_0 = null;


        public CmdSketchRectangle(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "矩形工具";
            base.m_category = "Editor";
            base.m_bitmap = Properties.Resource.uncheck;
            m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
            base.m_name = "Editor_Sketch_Rectangle";
            base._key = "Editor_Sketch_Rectangle";
            base.m_toolTip = "矩形工具";
            base._itemType = RibbonItemType.Tool;
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
            double num;
            double num1;
            double num2;
            double num3;
            if (int_0 == 1)
            {
                IActiveView activeView = _context.FocusMap as IActiveView;
                IPoint mPAnchorPoint = SketchShareEx.m_pAnchorPoint;
                if (SketchToolAssist.Feedback != null)
                {
                    try
                    {
                        IEnvelope envelope = (SketchToolAssist.Feedback as INewEnvelopeFeedback).Stop();
                        IPointCollection polylineClass = null;
                        IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                        if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            polylineClass = new Polyline();
                        }
                        else if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                        {
                            return;
                        }
                        else
                        {
                            polylineClass = new Polygon();
                        }
                        envelope.QueryCoords(out num, out num1, out num2, out num3);
                        object value = Missing.Value;
                        IPoint pointClass = new Point();
                        pointClass.PutCoords(num, num1);
                        polylineClass.AddPoint(pointClass, ref value, ref value);
                        pointClass = new Point();
                        pointClass.PutCoords(num, num3);
                        polylineClass.AddPoint(pointClass, ref value, ref value);
                        pointClass = new Point();
                        pointClass.PutCoords(num2, num3);
                        polylineClass.AddPoint(pointClass, ref value, ref value);
                        pointClass = new Point();
                        pointClass.PutCoords(num2, num1);
                        polylineClass.AddPoint(pointClass, ref value, ref value);
                        pointClass = new Point();
                        pointClass.PutCoords(num, num1);
                        polylineClass.AddPoint(pointClass, ref value, ref value);
                        CreateFeatureTool.CreateFeature(polylineClass as IGeometry, _context.FocusMap as IActiveView,
                            Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                    SketchShareEx.m_bInUse = false;
                    SketchShareEx.LastPoint = null;
                    SketchShareEx.PointCount = 0;
                    SketchToolAssist.Feedback = null;
                    SketchShareEx.m_LastPartGeometry = null;
                }
                else
                {
                    SketchToolAssist.Feedback = new NewEnvelopeFeedback();
                    SketchToolAssist.Feedback.Display = activeView.ScreenDisplay;
                    (SketchToolAssist.Feedback as INewEnvelopeFeedback2).Start(mPAnchorPoint);
                    SketchShareEx.m_bInUse = true;
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            SketchShareEx.SketchMouseMove(mapPoint, _context.FocusMap, _context.Config.EngineSnapEnvironment);
            //base.OnMouseMove(Button, Shift, int_2, int_3);
        }
    }
}