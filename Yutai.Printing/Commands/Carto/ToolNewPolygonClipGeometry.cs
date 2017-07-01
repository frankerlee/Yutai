using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class ToolNewPolygonClipGeometry : YutaiTool
    {
        private IDisplayFeedback _displayFeedback = null;

        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this._context.FocusMap.LayerCount > 0; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "多边形";
            this.m_message = "鼠标设置多边形裁剪区";
            this.m_toolTip = "鼠标设置多边形裁剪区";
            base.m_bitmap = Properties.Resources.icon_clip_polygon;
            base.m_name = "Printing_NewPolygonClipGeometry";
            _key = "Printing_NewPolygonClipGeometry";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolNewPolygonClipGeometry(IAppContext context)
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

        public override void OnDblClick()
        {
            if (this._displayFeedback != null)
            {
                IPolygon polygon = (this._displayFeedback as INewPolygonFeedback).Stop();
                this._displayFeedback = null;
                if ((polygon as IArea).Area != 0.0)
                {
                    if ((polygon as ITopologicalOperator).IsSimple)
                    {
                        polygon.SpatialReference = this._context.FocusMap.SpatialReference;
                        this._context.FocusMap.ClipGeometry = polygon;
                        (this._context.FocusMap as IActiveView).Extent = polygon.Envelope;
                        this._context.ActiveView.Refresh();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("多边形非简单多边形!");
                    }
                }
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 1)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                if (this._displayFeedback == null)
                {
                    this._displayFeedback = new NewPolygonFeedback();
                    this._displayFeedback.Display = activeView.ScreenDisplay;
                    (this._displayFeedback as INewPolygonFeedback).Start(point);
                }
                else
                {
                    (this._displayFeedback as INewPolygonFeedback).AddPoint(point);
                }
            }
            else
            {
                this._displayFeedback = null;
            }
        }

        public override void OnMouseMove(int Button, int int_1, int int_2, int int_3)
        {
            if (this._displayFeedback != null)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this._displayFeedback.MoveTo(point);
            }
        }

        public override void OnMouseUp(int Button, int int_1, int int_2, int int_3)
        {
            if (Button == 2)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                this._displayFeedback = null;
                activeView.Refresh();
            }
        }
    }
}