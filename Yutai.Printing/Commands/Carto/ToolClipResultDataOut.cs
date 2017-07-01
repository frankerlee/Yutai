using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;

namespace Yutai.Plugins.Printing.Commands
{
    internal class ToolClipResultDataOut : YutaiTool
    {
        private INewPolygonFeedback inewPolygonFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.FocusMap != null; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "实时";
            this.m_category = "工具";

            this.m_message = "裁剪输出";
            this.m_toolTip = "裁剪输出";
            base.m_bitmap = Properties.Resources.icon_clip_circle2;
            base.m_name = "Printing_ClipResultDataOut";
            _key = "Printing_ClipResultDataOut";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolClipResultDataOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }


        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 != 2)
            {
                IPoint point = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (this.inewPolygonFeedback_0 == null)
                {
                    this.inewPolygonFeedback_0 = new NewPolygonFeedback();
                    this.inewPolygonFeedback_0.Display = this._context.ActiveView.ScreenDisplay;
                    this.inewPolygonFeedback_0.Start(point);
                }
                else
                {
                    this.inewPolygonFeedback_0.AddPoint(point);
                }
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.inewPolygonFeedback_0 != null)
            {
                IPoint point = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.inewPolygonFeedback_0.MoveTo(point);
            }
        }

        public override void OnDblClick()
        {
            if (this.inewPolygonFeedback_0 != null)
            {
                try
                {
                    IPolygon polygon = this.inewPolygonFeedback_0.Stop();
                    polygon.SpatialReference = this._context.FocusMap.SpatialReference;
                    (polygon as ITopologicalOperator).Simplify();
                    new frmDir
                    {
                        FocusMap = this._context.FocusMap,
                        ClipGeometry = polygon
                    }.ShowDialog();
                }
                catch (Exception exception_)
                {
                    CErrorLog.writeErrorLog(this, exception_, "");
                }
                this.inewPolygonFeedback_0 = null;
            }
        }
    }
}