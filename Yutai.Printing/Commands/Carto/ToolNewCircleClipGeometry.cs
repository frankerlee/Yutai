using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class ToolNewCircleClipGeometry : YutaiTool
    {
        private IDisplayFeedback idisplayFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this._context.FocusMap.LayerCount > 0; }
        }

        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "圆形";
            this.m_message = "鼠标设置圆形裁剪区";
            this.m_toolTip = "鼠标设置圆形裁剪区";
            this.m_name = "SetCircleClipTool";
            base.m_bitmap = Properties.Resources.icon_clip_circle;
            base.m_name = "Printing_NewCircleClipGeometry";
            _key = "Printing_NewCircleClipGeometry";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolNewCircleClipGeometry(IAppContext context)
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
            if (Button == 1)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (this.idisplayFeedback_0 == null)
                {
                    this.idisplayFeedback_0 = new NewCircleFeedback();
                    this.idisplayFeedback_0.Display = activeView.ScreenDisplay;
                    (this.idisplayFeedback_0 as INewCircleFeedback).Start(point);
                }
                else
                {
                    try
                    {
                        ICircularArc circularArc = (this.idisplayFeedback_0 as INewCircleFeedback).Stop();
                        this.idisplayFeedback_0 = null;
                        ISegmentCollection segmentCollection = new Polygon() as ISegmentCollection;
                        object value = Missing.Value;
                        segmentCollection.AddSegment(circularArc as ISegment, ref value, ref value);
                        (segmentCollection as IGeometry).SpatialReference = this._context.FocusMap.SpatialReference;
                        this._context.FocusMap.ClipGeometry = (segmentCollection as IGeometry);
                        (this._context.FocusMap as IActiveView).Extent = (segmentCollection as IGeometry).Envelope;
                        this._context.ActiveView.Refresh();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public override void OnMouseMove(int Button, int int_1, int int_2, int int_3)
        {
            if (this.idisplayFeedback_0 != null)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.idisplayFeedback_0.MoveTo(point);
            }
        }

        public override void OnMouseUp(int Button, int int_1, int int_2, int int_3)
        {
            if (Button == 2)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                this.idisplayFeedback_0 = null;
                activeView.Refresh();
            }
        }
    }
}