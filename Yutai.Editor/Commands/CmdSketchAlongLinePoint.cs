using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSketchAlongLinePoint : YutaiTool, IShapeConstructorTool
    {
        private ISnappingEnvironment snappingEnvironment = new Snapping();
        private ControlsEditingSketchTool sketchTool = new ControlsEditingSketchTool();
        private ISnappingFeedback snappingFeedback = new SnappingFeedback();
        private IPointSnapper pointSnapper = new PointSnapper();
        private IAnchorPoint anchorPoint = null;
        private IPoint pPoint;
        private ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
        private frmAlongLineProperties frmProperties;


        public CmdSketchAlongLinePoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            SketchToolAssist.LineType = enumLineType.LTLine;
            if (SketchToolAssist.Feedback is INewPolylineFeedback)
            {
                (SketchToolAssist.Feedback as INewPolylineFeedback).ChangeLineType(enumLineType.LTLine);
            }
            else if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
            {
                (SketchToolAssist.Feedback as INewPolygonFeedbackEx).ChangeLineType(enumLineType.LTLine);
            }
            _context.SetCurrentTool(this);
        }

        public override void OnDblClick()
        {
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            if (SketchToolAssist.CurrentTask == null)
            {
                SketchToolAssist.EndSketch(false, focusMap,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            }
            else
            {
                SketchToolAssist.EndSketch(false, focusMap, null);
            }
            if (SketchToolAssist.TempLine != null)
            {
                if (frmProperties == null)
                {
                    frmProperties = new frmAlongLineProperties(_context);
                }
                frmProperties.SetPolyline((IPolyline) SketchToolAssist.TempLine);
                DialogResult result = frmProperties.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                IPolyline polyline = SketchToolAssist.TempLine as IPolyline;

                double dbp = 0;
                if (frmProperties.IsNOP)
                    dbp = polyline.Length/(frmProperties.NOP + 1);
                else
                    dbp = frmProperties.Distance;


                int createCount = 0;
                bool isClear = true;
                //create points at distance between points up to total length
                for (double d = dbp; d < polyline.Length; d += dbp)
                {
                    IConstructPoint contructionPoint = new Point() as IConstructPoint;
                    contructionPoint.ConstructAlong(polyline, esriSegmentExtension.esriNoExtension, d, false);
                    isClear = createCount == 0 ? true : false;
                    CreateFeatureTool.CreateFeature((IGeometry) contructionPoint, focusMap,
                        Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer, isClear);
                    createCount++;
                }

                if (frmProperties.IsEnds)
                {
                    isClear = createCount == 0 ? true : false;
                    CreateFeatureTool.CreateFeature(polyline.FromPoint, focusMap,
                        Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer, isClear);
                    CreateFeatureTool.CreateFeature(polyline.ToPoint, focusMap,
                        Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer, false);
                }
            }
        }

        public override void OnKeyDown(int int_0, int Shift)
        {
            if (int_0 == 27)
            {
                SketchToolAssist.Feedback = null;
                ((IActiveView) _context.FocusMap).Refresh();
            }
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null)
                {
                    esriGeometryType shapeType =
                        ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType;
                    flag = ((shapeType == esriGeometryType.esriGeometryMultipoint
                        ? false
                        : shapeType != esriGeometryType.esriGeometryPoint)
                        ? false
                        : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "沿线建点工具";
            base.m_category = "Editor";
            base.m_bitmap = Properties.Resources.icon_along_line;
            m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
            base.m_name = "Editor_Sketch_AlongLinePoint";
            base._key = "Editor_Sketch_AlongLinePoint";
            base.m_toolTip = "沿线建点工具";
            base._itemType = RibbonItemType.Tool;
            this.simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            this.simpleMarkerSymbol.Size = 8;
            this.simpleMarkerSymbol.Outline = true;
            this.simpleMarkerSymbol.Color = ColorManage.GetRGBColor(0, 255, 255);
        }

        public esriGeometryType GeometryType
        {
            get { return esriGeometryType.esriGeometryPoint; }
        }

        public override void OnMouseDown(int int_0, int Shift, int int_2, int int_3)
        {
            if (int_0 == 1)
            {
                IActiveView focusMap = (IActiveView) _context.FocusMap;
                if (SketchToolAssist.CurrentTask == null)
                {
                    SketchToolAssist.IsDrawTempLine = DrawTempGeometry.Line;
                    SketchToolAssist.SketchMouseDown(focusMap,
                        ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    //SketchToolAssist.SketchMouseDown(focusMap, ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                }
                else
                {
                    SketchToolAssist.SketchMouseDown(focusMap, null);
                }
            }
            //base.OnMouseDown(Button, Shift, int_2, int_3);
        }

        public override void OnMouseMove(int Button, int Shift, int int_2, int int_3)
        {
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            this.pPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            SketchToolAssist.SketchMouseMove(this.pPoint);
            //base.OnMouseMove(Button, Shift, int_2, int_3);
        }
    }
}