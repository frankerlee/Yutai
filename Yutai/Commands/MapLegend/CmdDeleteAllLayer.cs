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
    public class CmdDeleteAllLayer : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;
        public CmdDeleteAllLayer(IAppContext context, IMapLegendView view)
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
                if (_view.SelectedMap == null)
                {
                    if (_view.SelectedLayer == null) return false;
                    if (_view.SelectedLayer is IGroupLayer) return true;
                    else
                        return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void OnCreate()
        {
            base.m_caption = "删除所有图层";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuDeleteAllLayer";
            base._key = "mnuCreateLayerBySelect";
            base.m_toolTip = "mnuDeleteAllLayer";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;

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
                IMap pMap = _view.SelectedMap as IMap;
                pMap.ClearLayers();
                pMap.SpatialReferenceLocked = false;
                pMap.MapUnits= esriUnits.esriUnknownUnits;
                pMap.DistanceUnits= esriUnits.esriUnknownUnits;
                (pMap as IActiveView).Extent = (pMap as IActiveView).FullExtent;
                (pMap as IActiveView).Refresh();
            }
            else if(_view.SelectedItemType== esriTOCControlItem.esriTOCControlItemLayer)
            {
                if (_view.SelectedLayer is IGroupLayer)
                {
                    ((IGroupLayer)_view.SelectedLayer).Clear();
                    (_view.SelectedMap as IActiveView).Refresh();

                }
                
            }
        }
    }
}