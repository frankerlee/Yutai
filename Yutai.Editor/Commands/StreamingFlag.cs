using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class StreamingFlag : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    SketchShareEx.m_bInStreaming = false;
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
                else if (_context.CurrentTool is CmdStartSketch &&
                         this.ValidateLayer(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer))
                {
                    result = true;
                }
                else
                {
                    SketchShareEx.m_bInStreaming = false;
                    result = false;
                }
                return result;
            }
        }

        public StreamingFlag(IAppContext context)
        {
            OnCreate(context);
        }

        private bool ValidateLayer(IFeatureLayer pFeatureLayer)
        {
            bool flag;
            if (pFeatureLayer == null)
            {
                flag = false;
            }
            else if (pFeatureLayer.FeatureClass != null)
            {
                flag = ((pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline
                    ? false
                    : pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                    ? false
                    : true);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_name = "Edit_StreamingFlag";
            this._key = "Edit_StreamingFlag";
            base.m_category = "Editor";
            this.m_caption = "启用流模式";
            this.m_toolTip = "启用流模式";
            this.m_message = "启用流模式";
            this._itemType = RibbonItemType.Button;
            this.m_bitmap = Properties.Resource.StartLogging;
        }

        public override void OnClick()
        {
            SketchShareEx.m_bInStreaming = !SketchShareEx.m_bInStreaming;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}