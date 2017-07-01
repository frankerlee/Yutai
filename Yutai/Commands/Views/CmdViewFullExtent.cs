using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Views
{
    public class CmdViewFullExtent : YutaiCommand
    {
        public CmdViewFullExtent(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IActiveView activeView = (IActiveView) this._context.FocusMap;
            UID uID = new UIDClass();
            uID.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
            IEnumLayer enumLayer = this._context.FocusMap.get_Layers(uID, true);
            enumLayer.Reset();
            ILayer layer = enumLayer.Next();
            IEnvelope envelope = null;
            while (layer != null)
            {
                if (layer is IFeatureLayer)
                {
                    IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;
                    if (featureClass != null && featureClass.FeatureCount(null) > 0)
                    {
                        if (envelope == null)
                        {
                            envelope = layer.AreaOfInterest;
                        }
                        else
                        {
                            envelope.Union(layer.AreaOfInterest);
                        }
                    }
                }
                else if (envelope == null)
                {
                    envelope = layer.AreaOfInterest;
                }
                else
                {
                    envelope.Union(layer.AreaOfInterest);
                }
                layer = enumLayer.Next();
            }
            if (envelope == null)
            {
                activeView.Extent = activeView.FullExtent;
            }
            else
            {
                activeView.Extent = envelope;
            }
            activeView.Refresh();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "全图";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_zoom_max_extents;
            base.m_name = "View_FullExtent";
            base._key = "View_FullExtent";
            base.m_toolTip = "全图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}