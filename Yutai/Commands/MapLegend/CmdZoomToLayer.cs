// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdZoomToLayer.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/02  14:48
// 更新时间 :  2017/06/02  14:48

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
    public class CmdZoomToLayer : YutaiCommand
    {
       
        private IMapLegendView _view;
       

        public CmdZoomToLayer(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "缩放到图层";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuZoomToLayer";
            base._key = "mnuZoomToLayer";
            base.m_toolTip = "缩放到图层";
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
            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer && _view.SelectedLayer != null)
            {
                IActiveView pActiveView = _view.SelectedMap as IActiveView;
                if (pActiveView != null)
                {
                    pActiveView.Extent = _view.SelectedLayer.AreaOfInterest;
                    pActiveView.Refresh();
                }
            }
        }

    }
}