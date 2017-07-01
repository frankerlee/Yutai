using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolNewClipBounds : YutaiTool
    {
        private INewPolygonFeedback inewPolygonFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.FocusMap != null && !(this._context.ActiveView is IPageLayout); }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "边界";
            this.m_category = "工具";
            this.m_name = "SetupClip";
            this.m_message = "设置裁剪边界";
            this.m_toolTip = "设置裁剪边界";
            this.m_checked = false;
            base.m_bitmap = Properties.Resources.icon_clip_rectangle;
            base.m_name = "Printing_SetupClipBounds";
            _key = "Printing_SetupClip";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolNewClipBounds(IAppContext context)
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


        public override void OnMouseDown(int Button, int int_1, int int_2, int int_3)
        {
            if (Button != 2)
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

        public override void OnMouseMove(int Button, int int_1, int int_2, int int_3)
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
                    (polygon as ITopologicalOperator).Simplify();
                    polygon.SpatialReference = this._context.FocusMap.SpatialReference;
                    (this._context.FocusMap as IMapAdmin2).ClipBounds = polygon;
                    if (this._context.Hook is IApplication)
                    {
                        (this._context.Hook as IApplication).MapClipChanged(polygon);
                        (this._context.Hook as IApplication).CurrentTool = null;
                    }
                    (this._context.ActiveView.FocusMap as IActiveView).Refresh();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                this.inewPolygonFeedback_0 = null;
            }
        }
    }
}