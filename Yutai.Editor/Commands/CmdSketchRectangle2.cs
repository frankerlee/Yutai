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
    public class CmdSketchRectangle2 : YutaiTool, IShapeConstructorTool
    {
      
        private IPointSnapper pointSnapper = new PointSnapper();
        private ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
        private ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
        private INewRectangleFeedback rectangleFeedback;
        private ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
        private IActiveView activeView;
        private IAnchorPoint anchorPoint;
        private int _order = 0;


        public CmdSketchRectangle2(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "三点矩形工具";
            base.m_category = "Editor";
            base.m_bitmap = Properties.Resources.icon_sketch_rectangle;
            m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
            base.m_name = "Editor_Sketch_Rectangle2";
            base._key = "Editor_Sketch_Rectangle2";
            base.m_toolTip = "三点矩形工具";
            base._itemType = RibbonItemType.Tool;
            this.simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            this.simpleMarkerSymbol.Size = 8;
            this.simpleMarkerSymbol.Outline = true;
            this.simpleMarkerSymbol.Color = ColorManage.GetRGBColor(0, 255, 255);

            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            simpleFillSymbol.Color = ColorManage.GetRGBColor(0, 255, 255);
            simpleFillSymbol.Outline = new SimpleLineSymbol()
            {
                Width = 1,
                Color = ColorManage.GetRGBColor(255, 0, 0)
            };
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            simpleLineSymbol.Width = 1;
            simpleLineSymbol.Color = ColorManage.GetRGBColor(255, 0, 0);
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
            activeView = _context.FocusMap as IActiveView;
            _order = 0;
            rectangleFeedback = null;
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
                _order = 0;
            }
        }


        public override void OnMouseDown(int int_0, int Shift, int int_2, int int_3)
        {
            if (int_0 != 1)
            {
                return;
            }
            IPoint mapPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            ISnappingResult snappingResult = this.pointSnapper.Snap(mapPoint);
            if (anchorPoint == null)
            {
                anchorPoint = new AnchorPoint()
                {
                    Symbol = simpleMarkerSymbol as ISymbol
                };
            }
            if (snappingResult != null)
            {
                mapPoint = snappingResult.Location;
             
               // anchorPoint.MoveTo(mapPoint, activeView.ScreenDisplay);
               
            }
          
            if (_order == 0)
            {
                rectangleFeedback = new NewRectangleFeedback() as INewRectangleFeedback;
                rectangleFeedback.Display = activeView.ScreenDisplay;
                rectangleFeedback.Start(mapPoint);
                _order = 1;
                return;
            }
            if (_order == 1)
            {
                rectangleFeedback.SetPoint(mapPoint);
                //rectangleFeedback.Refresh(activeView.ScreenDisplay.hDC);
                _order = 2;
                return;
            }


            IPolygon rectangle = rectangleFeedback.Stop(mapPoint) as IPolygon;
            
           
            IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
            if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                IPointCollection polylineClass  = new Polyline() as IPointCollection;
                object value = Missing.Value;
                IPointCollection segmentCollection=rectangle as IPointCollection;
                polylineClass.AddPointCollection(segmentCollection);
                CreateFeatureTool.CreateFeature(polylineClass as IGeometry, _context.FocusMap as IActiveView,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            }
            else if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                return;
            }
            else
            {
              
                CreateFeatureTool.CreateFeature(rectangle as IGeometry, _context.FocusMap as IActiveView,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            }
          


            _order = 0;
            rectangleFeedback = null;
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (_order == 0) return;
           
            IPoint mapPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            ISnappingResult snappingResult = this.pointSnapper.Snap(mapPoint);
            if (anchorPoint == null)
            {
                anchorPoint = new AnchorPoint()
                {
                    Symbol = simpleMarkerSymbol as ISymbol
                };
            }
           
            if (snappingResult != null)
            {
                mapPoint = snappingResult.Location;
            }
            anchorPoint.MoveTo(mapPoint, activeView.ScreenDisplay);
            rectangleFeedback.MoveTo(mapPoint);
           
        }
    }
}