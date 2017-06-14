using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSquareAndFinish : YutaiCommand
    {
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
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer == null)
                {
                    result = false;
                }
                else if (SketchToolAssist.Feedback == null)
                {
                    result = false;
                }
                else if (SketchToolAssist.Feedback is INewPolylineFeedback)
                {
                    result = (SketchToolAssist.Feedback as INewPolylineFeedback).CanSquareAndFinish;
                }
                else
                {
                    result = (SketchToolAssist.Feedback is INewPolygonFeedbackEx && (SketchToolAssist.Feedback as INewPolygonFeedbackEx).CanSquareAndFinish);
                }
                return result;
            }
        }

        public CmdSquareAndFinish(IAppContext context)
        {
           OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_name = "Editor_SquareAndFinish";
            this._key = "Editor_SquareAndFinish";
            base.m_category = "Editor";
            this.m_caption = "直角结束";
            this.m_toolTip = "直角结束";
            this.m_category = "编辑器";
            this.m_message = "直角结束";
           
        }

        public override void OnClick()
        {
            ISpatialReference spatialReference = (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer as IGeoDataset).SpatialReference;
            IGeometry geometry = null;
            if (SketchToolAssist.Feedback is INewPolylineFeedback)
            {
                geometry = (SketchToolAssist.Feedback as INewPolylineFeedback).SquareAndFinish(spatialReference);
            }
            else if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
            {
                geometry = (SketchToolAssist.Feedback as INewPolygonFeedbackEx).SquareAndFinish(spatialReference);
            }
            if (geometry != null)
            {
                SketchShareEx.EndSketch(geometry, _context.FocusMap as IActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                SketchToolAssist.Feedback = null;
                SketchShareEx.PointCount = 0;
                SketchShareEx.m_bInUse = false;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
          OnClick();
        }
    }
}
