using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdExpandAllLayer : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;
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

        public void OnClick()
        {

            IEnumLayer pEnumLayer = _view.SelectedMap.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            ILegendInfo pLengendInfo;
            ILegendGroup pLengendGroup;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (pLayer is ILegendInfo)
                {
                    pLengendInfo = pLayer as ILegendInfo;
                    for (int i = 0; i < pLengendInfo.LegendGroupCount; i++)
                    {
                        pLengendGroup = pLengendInfo.get_LegendGroup(i);
                        pLengendGroup.Visible = true;
                    }
                }
            }
            _view.TocControl.Update();
        }
    }
}