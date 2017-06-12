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
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSketchPoint:YutaiTool,IShapeConstructorTool
    {
        private ISnappingEnvironment snappingEnvironment=new Snapping();
        private ControlsEditingSketchTool sketchTool=new ControlsEditingSketchTool();
        private ISnappingFeedback snappingFeedback=new SnappingFeedback();
        private IPointSnapper pointSnapper=new PointSnapper();
        private IAnchorPoint anchorPoint = null;
        private IPoint pPoint;
        private ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();

        
        public CmdSketchPoint(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick()
        {
            (this.pointSnapper as PointSnapper).Map = _context.FocusMap;
            
            base.OnClick();
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null)
                {
                    esriGeometryType shapeType = ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType;
                    flag = ((shapeType == esriGeometryType.esriGeometryMultipoint ? false : shapeType != esriGeometryType.esriGeometryPoint) ? false : true);
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
            base.m_caption = "创建点";
            base.m_category = "Editor";
            base.m_bitmap = Properties.Resources.icon_sketch_point;
            m_cursor = new Cursor(new MemoryStream(Resource.Digitise));
            
            base.m_name = "Editor_Sketch_Point";
            base._key = "Editor_Sketch_Point";
            base.m_toolTip = "创建点";
            base._itemType = RibbonItemType.Tool;
        }

        public esriGeometryType GeometryType { get {return esriGeometryType.esriGeometryPoint;} }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 == 1)
            {
                IActiveView focusMap = (IActiveView)_context.FocusMap;
                pPoint = SketchShareEx.m_pAnchorPoint;
                CreateFeatureTool.CreateFeature(this.pPoint, focusMap,Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            }
            base.OnMouseDown(int_0, int_1, int_2, int_3);
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {

            IActiveView focusMap = (IActiveView)_context.FocusMap;
            this.pPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            ISnappingResult snappingResult = this.pointSnapper.Snap(this.pPoint);
            if (snappingResult == null)
            {
                if (this.anchorPoint == null)
                {
                    this.anchorPoint  = new AnchorPoint()
                    {
                        Symbol = this.simpleMarkerSymbol as ISymbol
                    };
                }
                this.anchorPoint.MoveTo(this.pPoint, focusMap.ScreenDisplay);
            }
            else
            {
                this.pPoint = snappingResult.Location;
                if (this.anchorPoint == null)
                {
                    this.anchorPoint = new AnchorPoint()
                    {
                        Symbol = this.simpleMarkerSymbol as ISymbol
                    };
                }
                this.anchorPoint.MoveTo(this.pPoint, focusMap.ScreenDisplay);
            }
            base.OnMouseMove(int_0, int_1, int_2, int_3);
        }
    }
}
