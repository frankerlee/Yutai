using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdExpandAllLayer : YutaiCommand
    {
        
        private IMapLegendView _view;
      
        public CmdExpandAllLayer(IAppContext context, IMapLegendView view)
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
                    else return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void OnCreate()
        {
            base.m_caption = "展开所有图层";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuExpandAllLayer";
            base._key = "mnuExpandAllLayer";
            base.m_toolTip = "展开所有图层";
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

        public void OnClick()
        {
            switch (_view.SelectedItemType)
            {
                case esriTOCControlItem.esriTOCControlItemMap:
                    {
                        IEnumLayer pEnumLayer = _view.SelectedMap.Layers;

                        if (pEnumLayer == null) return;
                        ILayer pLayer;
                        pEnumLayer.Reset();
                        for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
                        {
                            ExpandedLayers(pLayer, true);
                        }
                    }
                    break;
                case esriTOCControlItem.esriTOCControlItemLayer:
                    {
                        ExpandedLayers(_view.SelectedLayer, true);
                    }
                    break;
                case esriTOCControlItem.esriTOCControlItemHeading:
                    break;
                case esriTOCControlItem.esriTOCControlItemLegendClass:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _view.TocControl.Update();
        }


        private void ExpandedLayers(ILayer layer, bool expended)
        {
            if (layer is IGroupLayer)
            {
                ICompositeLayer pCompositeLayer = (ICompositeLayer)layer;
                for (int i = 0; i < pCompositeLayer.Count; i++)
                {
                    ILayer pLayer = pCompositeLayer.Layer[i];
                    ExpandedLayers(pLayer, expended);
                }
                ((IGroupLayer)layer).Expanded = expended;
            }
            else if (layer is ILegendInfo)
            {
                ILegendInfo pLegendInfo = layer as ILegendInfo;
                for (int j = 0; j < pLegendInfo.LegendGroupCount; j++)
                {
                    ILegendGroup pLegendGroup = pLegendInfo.LegendGroup[j];
                    pLegendGroup.Visible = expended;
                }
            }
        }
    }
}