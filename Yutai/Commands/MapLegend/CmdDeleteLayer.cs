using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdDeleteLayer : YutaiCommand
    {
        private IMapLegendView _view;

        public CmdDeleteLayer(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        public override bool Enabled
        {
            get
            {
                if (_view == null) return false;
                if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (_view.SelectedLayer == null) return false;
                    if (_view.SelectedLayer is IGroupLayer) return false;
                    else
                        return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void OnCreate()
        {
            base.m_caption = "删除所选图层";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuDeleteLayer";
            base._key = "mnuDeleteLayer";
            base.m_toolTip = "删除所选图层";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }

        private string GetLayerName(IBasicMap map, string basic)
        {
            string lyrName = basic;
            int num = 1;
            for (int i = 0; i < map.LayerCount; i++)
            {
                if (map.Layer[i].Name == lyrName)
                {
                    lyrName = basic + num.ToString();
                    num++;
                }
            }
            return lyrName;
        }

        public void OnClick()
        {
            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemMap)
            {
                return;
            }
            else if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer)
            {
                _view.SelectedMap.DeleteLayer(_view.SelectedLayer);

                (_view.SelectedMap as IActiveView).Refresh();
            }
            if (_view.SelectedMap.LayerCount == 0)
            {
                IMap pMap = _view.SelectedMap as IMap;
                pMap.ClearLayers();
                pMap.SpatialReferenceLocked = false;
                pMap.MapUnits = esriUnits.esriUnknownUnits;
                pMap.DistanceUnits = esriUnits.esriUnknownUnits;
                (_view.SelectedMap as IActiveView).Refresh();
            }
        }
    }
}