// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdPanToSelectedFeature.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/02  15:40
// 更新时间 :  2017/06/02  15:40

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdPanToSelectedFeature : YutaiCommand
    {
       
        private IMapLegendView _view;
    

        public CmdPanToSelectedFeature(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "移动到选择要素";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "dropSelection.mnuPanToSelectedFeature";
            base._key = "dropSelection.mnuPanToSelectedFeature";
            base.m_toolTip = "移动到选择要素";
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
            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer && _view.SelectedLayer != null)
            {
                IActiveView pActiveView = _view.SelectedMap as IActiveView;
                if (pActiveView != null)
                {
                    if (_view.SelectedLayer is IFeatureLayer)
                    {
                        ICursor pCursor;
                        IFeatureSelection featureSelection = _view.SelectedLayer as IFeatureSelection;
                        if (featureSelection != null)
                        {
                            featureSelection.SelectionSet.Search(null, false, out pCursor);
                            IRow i = pCursor.NextRow();
                            IEnvelope extent = (i as IFeature).Extent;
                            while ((i = pCursor.NextRow()) != null)
                            {
                                extent.Union((i as IFeature).Extent);
                            }
                            IPoint point = new PointClass();
                            point.PutCoords((extent.XMin + extent.XMax)/2, (extent.YMin + extent.YMax)/2);
                            IEnvelope envelope = pActiveView.Extent;
                            envelope.CenterAt(point);
                            pActiveView.Extent = envelope;
                            pActiveView.Refresh();
                        }
                    }
                }
            }
        }
    }
}