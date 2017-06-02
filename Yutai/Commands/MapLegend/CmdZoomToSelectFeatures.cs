// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdZoomToSelectFeatures.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/02  15:11
// 更新时间 :  2017/06/02  15:11

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdZoomToSelectFeatures : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;

        public CmdZoomToSelectFeatures(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "缩放到选择要素";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "dropSelection.mnuZoomToSelectFeatures";
            base._key = "dropSelection.mnuZoomToSelectFeatures";
            base.m_toolTip = "缩放到选择要素";
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
                    if (_view.SelectedLayer is IFeatureLayer)
                    {
                        MapHelper.Zoom2SelectedFeature(pActiveView, _view.SelectedLayer as IFeatureLayer);
                    }
                }
            }
        }
    }
}