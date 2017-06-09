// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdSelectAllFeatures.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/02  15:03
// 更新时间 :  2017/06/02  15:03

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdSelectAllFeatures : YutaiCommand
    {
      
        private IMapLegendView _view;

        public CmdSelectAllFeatures(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "选择所有要素";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "dropSelection.mnuSelectAllFeatures";
            base._key = "dropSelection.mnuSelectAllFeatures";
            base.m_toolTip = "选择所有要素";
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
                IActiveView pActiveView = _context.MapControl.Map as IActiveView;
                if (pActiveView != null)
                {
                    if (_view.SelectedLayer is IFeatureLayer)
                    {
                        ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
                        if ((_view.SelectedLayer as IFeatureLayer).FeatureClass.FeatureCount(null) <= (pSelectionEnvironment as ISelectionEnvironmentThreshold).WarningThreshold || MessageBox.Show("所选择记录较多，执行将花较长时间，是否继续？", "选择", MessageBoxButtons.YesNo) != DialogResult.No)
                        {
                            IFeatureSelection featureSelection = _view.SelectedLayer as IFeatureSelection;
                            if (featureSelection != null)
                            {
                                featureSelection.SelectFeatures(null, esriSelectionResultEnum.esriSelectionResultAdd, false);
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, featureSelection, null);
                            }
                        }
                    }
                }
            }
        }
        
    }
}