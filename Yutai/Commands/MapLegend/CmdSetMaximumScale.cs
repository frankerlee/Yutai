// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdSetMaximumScale.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/02  14:31
// 更新时间 :  2017/06/02  14:31

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
    public class CmdSetMaximumScale : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;

        public CmdSetMaximumScale(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "设置最大比例";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "dropVisibleScaleRange.mnuSetMaximumScale";
            base._key = "dropVisibleScaleRange.mnuSetMaximumScale";
            base.m_toolTip = "设置最大比例";
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
            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer)
            {
                IMap map = _view.SelectedMap as IMap;
                if (map != null)
                    _view.SelectedLayer.MaximumScale = map.MapScale;
            }
        }

    }
}